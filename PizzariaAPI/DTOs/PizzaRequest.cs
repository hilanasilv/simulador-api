namespace PizzariaAPI.DTOs
{
    public class PizzaRequest
    {
        public required string Sabor { get; set; } 
        public int TempoPreparo { get; set; }
    }
}
