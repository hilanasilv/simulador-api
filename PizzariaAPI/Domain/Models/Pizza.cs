namespace PizzariaAPI.Domain.Models
{
    public class Pizza
    {
        public int Id { get; set; }
        public required string Sabor { get; set; }

        public int TempoPreparo { get; set; }

    }
}
