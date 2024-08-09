
namespace ClubeDoLivro.Domains
{
	public class Autor
	{
		public int Id { get; set; }
		public string Nome { get; set; }
		public string Sobrenome { get; set; }
		public List<Livro> Livros { get; set; }

		public int LivrosEscritos => Livros.Count;


		public Autor()
		{
			Livros = new List<Livro>();
		}

		public void AdicionarLivro(Livro livro)
		{
			Livros.Add(livro);
			livro.Autores.Add(this);
		}
	}
}
