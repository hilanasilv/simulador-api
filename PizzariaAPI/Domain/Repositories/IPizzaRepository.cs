using PizzariaAPI.Domain.Models;
using PizzariaAPI.DTOs;

namespace PizzariaAPI.Domain.Repositories
{
    public interface IPizzaRepository
    {
        Pizza Add(PizzaRequest pizzaRequest);
        List<Pizza> GetAll();
        Pizza? GetById(int id);
        List<Pizza> GetByName(string nome);
        Pizza? Update(int id, PizzaRequest pizzaRequest);
        void Delete(int id);
    }
}
