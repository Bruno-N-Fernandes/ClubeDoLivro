namespace ClubeDoLivro.Domains
{
    public class Autor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public List<Livro> Livros { get; set; }
    }
}
