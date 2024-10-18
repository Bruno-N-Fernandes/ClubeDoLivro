using ClubeDoLivro.Abstractions.Interfaces;

namespace ClubeDoLivro.Domains
{
    public class Usuario : IEntity
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Senha { get; set; }
    }
}
