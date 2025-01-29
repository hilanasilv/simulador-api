using Microsoft.EntityFrameworkCore;
using PizzariaAPI.Domain.Models;

namespace PizzariaAPI.Infra
{
    public class ConnectionContext : DbContext
    {
        public DbSet<Pizza> Pizzas { get; set; }

        // Configuração do banco de dados
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                "Server=localhost;" +
                "Port=3306;" +
                "Database=pizzaria_db;" +
                "User Id=root;" +
                "Password=admin;",
                new MySqlServerVersion(new Version(8, 0, 41))
            );
        }
    }
}
