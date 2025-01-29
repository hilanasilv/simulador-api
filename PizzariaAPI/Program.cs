using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PizzariaAPI.Domain.Repositories;
using PizzariaAPI.Services;
using System.Text;

namespace PizzariaAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddTransient<PedidoService>();
            builder.Services.AddControllers();
            builder.Services.AddAuthorization();

            // Configura��o Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Insira o token JWT no formato: Bearer {seu token}"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });

                options.EnableAnnotations();
            });

            // Adicionando o reposit�rio
            builder.Services.AddTransient<IPizzaRepository, PizzaRepository>();

            // Configura��o do Kestrel
            builder.WebHost.ConfigureKestrel(options =>
            {
                options.ListenAnyIP(5000); // Define a porta HTTP
            });

            // Configura��o do JWT
            var key = Encoding.ASCII.GetBytes(JwtConfig.ChaveSecreta);
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };

                    // Mensagens personalizadas nos StatusCode 401 e 403
                    options.Events = new JwtBearerEvents
                    {
                        OnChallenge = context =>
                        {
                            context.HandleResponse();
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            context.Response.ContentType = "application/json";
                            var result = new { aviso = "Voc� precisa se autenticar para acessar este recurso." };
                            return context.Response.WriteAsJsonAsync(result);
                        },
                        OnForbidden = context =>
                        {
                            context.Response.StatusCode = StatusCodes.Status403Forbidden;
                            context.Response.ContentType = "application/json";
                            var result = new { aviso = "Voc� n�o tem permiss�o para acessar este recurso." };
                            return context.Response.WriteAsJsonAsync(result);
                        }
                    };
                });

            var app = builder.Build();

            // Configura��o Swagger para Desenvolvimento
            if (app.Environment.IsDevelopment() || Environment.GetEnvironmentVariable("ENABLE_SWAGGER") == "true")
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Configura��o de redirecionamento seguro
            app.UseHttpsRedirection();

            // Middleware de Autentica��o e Autoriza��o
            app.UseAuthentication();
            app.UseAuthorization();

            // Mapear controladores
            app.MapControllers();

            app.Run();
        }
    }
}
