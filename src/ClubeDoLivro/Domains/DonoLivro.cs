namespace ClubeDoLivro.Domains
{
    public class DonoLivro
    {
        public int Id { get; set; }
        public Usuario Usuario { get; set; }
        public Livro Livro { get; set; }
        public Versao Versao { get; set; }
    }
}
