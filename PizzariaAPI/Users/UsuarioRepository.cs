namespace PizzariaAPI.Users
{
    public static class UsuarioRepository
    {
        public static Usuario Get(string nome, string senha)
        {
            var usuarios = new List<Usuario>();
            usuarios.Add(new Usuario { Id = 1, Nome = "Nayla", Senha = "nayla", Role = "garçom" });
            usuarios.Add(new Usuario { Id = 2, Nome = "Hilana", Senha = "hilana", Role = "cozinheiro" });

            return usuarios.FirstOrDefault(x =>
                x.Nome == nome
                && x.Senha == senha);
        }
    }
}
