using PizzariaAPI.Domain.Models;

namespace PizzariaAPI.Services
{
    public class PedidoService
    {
        public (bool sucesso, string mensagem, int tempoTotal) CalcularTempoPedido(List<int> pizzaIds, List<Pizza> todasAsPizzas)
        {
            // Obtém as pizzas com base nos IDs fornecidos
            var pizzasSolicitadas = todasAsPizzas.Where(pizza => pizzaIds.Contains(pizza.Id)).ToList();

            // Verifica se algum sabor não foi encontrado
            if (pizzasSolicitadas.Count != pizzaIds.Count)
            {
                return (false, "Um ou mais sabores do pedido estão em falta.", 0);
            }

            // Calcula o tempo total com base no maior tempo de preparo entre os sabores pedidos
            var tempoTotal = pizzasSolicitadas.Max(p => p.TempoPreparo);

            return (true, $"O tempo total de preparo do pedido é de {tempoTotal} minutos", tempoTotal);
        }
    }
}
