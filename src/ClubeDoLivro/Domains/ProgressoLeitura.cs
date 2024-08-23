namespace ClubeDoLivro.Domains
{
    public class ProgressoLeitura
    {
        public int Id { get; set; }
        public Usuario Usuario { get; set; }
        public Livro Livro { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }
        public int PaginaAtual { get; set; }
    }
}
