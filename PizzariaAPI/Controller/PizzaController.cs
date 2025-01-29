using Microsoft.AspNetCore.Mvc;
using PizzariaAPI.Domain.Models;
using PizzariaAPI.DTOs;
using PizzariaAPI.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using PizzariaAPI.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace PizzariaAPI.Controller
{
    [Route("api/pizza")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private readonly IPizzaRepository _pizzaRepository;
        private readonly PedidoService _pedidoService;

        public PizzaController(IPizzaRepository pizzaRepository, PedidoService pedidoService)
        {
            _pizzaRepository = pizzaRepository;
            _pedidoService = pedidoService;
        }

        //CRUD de sabores de pizza
        [HttpPost]
        [Authorize(Roles = "cozinheiro")]
        [SwaggerOperation(Summary = "Adiciona um novo sabor de pizza", Description = "Cria um novo registro de sabor de pizza no sistema.")]
        [SwaggerResponse(200, "Sabor de pizza adicionado com sucesso", typeof(Pizza))] 
        [SwaggerResponse(401, "Você precisa se autenticar para acessar esse recurso", typeof(string))] 
        [SwaggerResponse(403, "Você não tem permissão para acessar este recurso", typeof(string))] 
        public IActionResult Add([FromBody] PizzaRequest pizzaRequest)
        {

            var pizza = new Pizza
            {
                Sabor = pizzaRequest.Sabor,
                TempoPreparo = pizzaRequest.TempoPreparo
            };

            var createdPizza = _pizzaRepository.Add(pizzaRequest);
            return Ok(createdPizza);
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "cozinheiro")]
        [SwaggerOperation(Summary = "Atualiza um sabor de pizza existente", Description = "Atualiza um sabor de pizza no sistema.")]
        [SwaggerResponse(200, "Sabor de pizza atualizado com sucesso", typeof(Pizza))] 
        [SwaggerResponse(401, "Você precisa se autenticar para acessar esse recurso", typeof(string))] 
        [SwaggerResponse(403, "Você não tem permissão para acessar este recurso", typeof(string))] 
        [SwaggerResponse(404, "Sabor de pizza não encontrado", typeof(string))]
        public IActionResult Update(int id, [FromBody] PizzaRequest pizzaRequest)
        {


            var pizza = new Pizza
            {
                Id = id,
                Sabor = pizzaRequest.Sabor,
                TempoPreparo = pizzaRequest.TempoPreparo
            };

            var updatePizza = _pizzaRepository.Update(id, pizzaRequest);
            return updatePizza == null ? NotFound("Sabor de pizza não encontrado") : Ok(updatePizza);
        }

        [HttpGet]
        [Authorize]
        [SwaggerOperation(Summary = "Obtém todos os sabores de pizza", Description = "Retorna todos os sabores de pizza cadastrados.")]
        [SwaggerResponse(200, "Lista de sabores de pizza retornada com sucesso", typeof(IEnumerable<Pizza>))]
        [SwaggerResponse(401, "Você precisa se autenticar para acessar esse recurso", typeof(string))]
        public IActionResult GetAll()
        {
            var pizzas = _pizzaRepository.GetAll();
            return Ok(pizzas);
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize]
        [SwaggerOperation(Summary = "Obtém um sabor de pizza pelo ID", Description = "Retorna um sabor de pizza específico baseado no ID fornecido.")]
        [SwaggerResponse(200, "Sabor de pizza encontrado com sucesso", typeof(Pizza))]
        [SwaggerResponse(404, "Sabor de pizza não encontrado", typeof(string))]
        [SwaggerResponse(401, "Você precisa se autenticar para acessar esse recurso", typeof(string))]
        public IActionResult GetById(int id)
        {
            var pizza = _pizzaRepository.GetById(id);
            return pizza == null ? NotFound("Sabor de pizza não encontrado.") : Ok(pizza);
        }

        [HttpGet]
        [Route("sabores")]
        [Authorize]
        [SwaggerOperation(Summary = "Obtém sabores de pizza por nome", Description = "Retorna sabores de pizza com base no nome fornecido.")]
        [SwaggerResponse(200, "Sabores de pizza encontrados", typeof(IEnumerable<Pizza>))]
        [SwaggerResponse(404, "Nenhum sabor de pizza encontrado", typeof(string))]
        [SwaggerResponse(401, "Você precisa se autenticar para acessar esse recurso", typeof(string))]

        public IActionResult GetByName([FromQuery] string nome)
        {
            var pizzas = _pizzaRepository.GetByName(nome);
            return pizzas == null || !pizzas.Any() ? NotFound("Nenhum sabor de pizza encontrado.") : Ok(pizzas);
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "cozinheiro")]
        [SwaggerOperation(Summary = "Deleta um sabor de pizza", Description = "Remove um sabor de pizza do sistema a partir do ID.")]
        [SwaggerResponse(200, "Sabor de pizza excluído com sucesso", typeof(string))]
        [SwaggerResponse(404, "Sabor de pizza não encontrado", typeof(string))]
        [SwaggerResponse(401, "Você precisa se autenticar para acessar esse recurso", typeof(string))]
        [SwaggerResponse(403, "Você não tem permissão para acessar este recurso", typeof(string))]
        public IActionResult Delete(int id)
        {
            try
            {
                _pizzaRepository.Delete(id);
                return Ok("Sabor de pizza excluído com sucesso.");
            }

            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Route("tempo-pedido")]
        [Authorize]
        [SwaggerOperation(Summary = "Calcula o tempo total de preparo de um pedido", Description = "Calcula o tempo total de preparo das pizzas de um pedido, considerando o maior tempo de preparo.")]
        [SwaggerResponse(200, "Tempo do pedido calculado com sucesso", typeof(string))]
        [SwaggerResponse(400, "Um ou mais sabores do pedido estão em falta", typeof(string))]
        [SwaggerResponse(401, "Você precisa se autenticar para acessar esse recurso", typeof(string))]
        public IActionResult CalcularTempoPedido([FromBody] PedidoRequest pedidoRequest)
        {
            // Busca as pizzas no repositório com base nos IDs recebidos
            var todasAsPizzas = _pizzaRepository.GetAll();

            // Passa a lista de pizzas completas para o PedidoService
            var (sucesso, mensagem, tempoTotal) = _pedidoService.CalcularTempoPedido(pedidoRequest.Pizzas, todasAsPizzas);

            if (!sucesso)
            {
                return BadRequest(new { message = mensagem });
            }

            return Ok(new { message = mensagem });
        }

    }
}
