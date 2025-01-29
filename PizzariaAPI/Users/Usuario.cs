namespace PizzariaAPI.Users
{
    public class Usuario
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string Senha { get; set; }
        public required string Role { get; set; }
    }
}