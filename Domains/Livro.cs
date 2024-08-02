namespace ClubeDoLivro.Domains
{
    public class Livro
    {
        public int Id { get; set; }
        public string NomeDoLivro { get; set; }
        public string Volume { get; set; }
        public string Edicao { get; set; }
        public string CodigoISBN { get; set; }
        public string Paginas { get; set; }
        public List<Autor> Autores { get; set; }
    }
}
