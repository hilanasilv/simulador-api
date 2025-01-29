using PizzariaAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;
using PizzariaAPI.DTOs;
using PizzariaAPI.Infra;

namespace PizzariaAPI.Domain.Repositories
{
    public class PizzaRepository : IPizzaRepository
    {
        private readonly ConnectionContext _context = new Infra.ConnectionContext();

        // Adiciona um novo sabor de pizza
        public Pizza Add(PizzaRequest pizzaRequest)
        {
            var pizza = new Pizza
            {
                Sabor = pizzaRequest.Sabor,
                TempoPreparo = pizzaRequest.TempoPreparo
            };

            _context.Pizzas.Add(pizza);
            _context.SaveChanges();
            return pizza;
        }

        // Obtém a lista com todas as pizzas
        public List<Pizza> GetAll()
        {
            return _context.Pizzas.ToList();
        }

        // Busca uma pizza pelo ID
        public Pizza? GetById(int id)
        {
            return _context.Pizzas.Find(id);
        }

        // Busca uma ou mais pizzas pelo nome
        public List<Pizza> GetByName(string nome)
        {
            return _context.Pizzas
                .Where(p => p.Sabor.Contains(nome))
                .ToList();
        }

        // Atualiza uma pizza existente
        public Pizza? Update(int id, PizzaRequest pizzaRequest)
        {
            var existingPizza = _context.Pizzas.Find(id);
            if (existingPizza != null)
            {
                existingPizza.Sabor = pizzaRequest.Sabor;
                existingPizza.TempoPreparo = pizzaRequest.TempoPreparo;

                _context.Pizzas.Update(existingPizza);
                _context.SaveChanges();
                return existingPizza;
            }
            return null;
        }

        // Remove uma pizza pelo ID
        public void Delete(int id)
        {
            var pizza = _context.Pizzas.Find(id);
            if (pizza == null)
            {
                throw new KeyNotFoundException("Sabor de pizza não econtrado.");
            }

            _context.Pizzas.Remove(pizza);
            _context.SaveChanges();
        }
    }
}
