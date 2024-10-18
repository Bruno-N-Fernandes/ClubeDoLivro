using ClubeDoLivro.Abstractions.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ClubeDoLivro.Domains
{
    public class Livro : IEntity
	{
		public int Id { get; set; }
		public string Nome { get; set; }
		public string Volume { get; set; }
		public string Edicao { get; set; }
		public string ISBN { get; set; }
		public int Paginas { get; set; }
		private List<Autor> Autores { get; set; }
		public int QuantidadeAutores => Autores.Count;

		public bool FoiEscritoPelo(Autor autor)
		{
			return Autores.Any(x => x.Id == autor.Id);
		}

		public Livro()
		{
			Autores = new List<Autor>();
		}

		public void AdicionarAutor(Autor autor)
		{
			if (!Autores.Any(x => x.Id == autor.Id))
			{
				Autores.Add(autor);
				autor.AdicionarLivro(this);
			}
		}

		public bool EhValido()
		{
			return !string.IsNullOrWhiteSpace(Nome)
				&& !string.IsNullOrWhiteSpace(Volume)
				&& !string.IsNullOrWhiteSpace(Edicao)
				&& !string.IsNullOrWhiteSpace(ISBN)
				&& Paginas > 0;
		}

        public Livro Clone() => MemberwiseClone() as Livro;

        public void Alterar(Livro livroAlterado)
        {
            Nome = livroAlterado.Nome;
            Volume = livroAlterado.Volume;
            Edicao = livroAlterado.Edicao;
            ISBN = livroAlterado.ISBN;
            Paginas = livroAlterado.Paginas;
        }
    }
}
