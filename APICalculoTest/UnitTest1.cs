using PizzariaAPI.Domain.Models;
using PizzariaAPI.DTOs;
using PizzariaAPI.Services;

namespace APICalculoTest
{
    public class Tests
    {
        private PedidoService _pedidoService;

        [SetUp]
        public void Setup()
        {
            _pedidoService = new PedidoService();
        }

        [Test]
        public void CalcularTempoPedido_DeveCalcularTempoCorretamenteQuandoTodosOsSaboresEstiveremDisponiveis()
        {
            // Cria uma lista de pizzas
            var pizzas = new List<Pizza>
            {
                new Pizza { Id = 1, Sabor = "Margherita", TempoPreparo = 30 },
                new Pizza { Id = 2, Sabor = "Pepperoni", TempoPreparo = 40 },
                new Pizza { Id = 3, Sabor = "Frango", TempoPreparo = 35 }
            };

            // Realiza o pedido com IDs das pizzas
            var pedidoRequest = new PedidoRequest
            {
                Pizzas = new List<int> { 1, 2, 3 }
            };

            // Chama o método para calcular o tempo
            var (sucesso, mensagem, tempoTotal) = _pedidoService.CalcularTempoPedido(pedidoRequest.Pizzas, pizzas);

            // Valida os resultados
            Assert.That(sucesso, Is.True);
            Assert.That(mensagem, Is.EqualTo("O tempo total de preparo do pedido é de 40 minutos")); // O tempo total será o maior tempo de preparo
        }

        [Test]
        public void CalcularTempoPedido_DeveRetornarErroQuandoAlgumSaborEstiverFaltando()
        {
            // Cria uma lista de pizzas
            var pizzas = new List<Pizza>
            {
                new Pizza { Id = 1, Sabor = "Margherita", TempoPreparo = 30 },
                new Pizza { Id = 2, Sabor = "Pepperoni", TempoPreparo = 40 }
            };

            // Realiza o pedido com um ID que não existe
            var pedidoRequest = new PedidoRequest
            {
                Pizzas = new List<int> { 1, 3 }
            };

            // Chama o método para calcular o tempo
            var (sucesso, mensagem, tempoTotal) = _pedidoService.CalcularTempoPedido(pedidoRequest.Pizzas, pizzas);

            // Valida os resultados
            Assert.That(sucesso, Is.False);
            Assert.That(mensagem, Is.EqualTo("Um ou mais sabores do pedido estão em falta."));
        }
    }
}