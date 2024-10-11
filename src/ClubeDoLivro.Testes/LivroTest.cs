using ClubeDoLivro.Domains;
using FluentAssertions;

namespace ClubeDoLivro.Testes
{
	public class LivroTest
	{
		private readonly Livro _livro;

		public LivroTest()
        {
			_livro = new Livro();
        }


		[Fact]
		public void QuandoEuCrioUmLivro_OLivroDeveSerCriadoCorretamente()
		{
			//Arrange

			//Act

			//Assert
			Assert.NotNull(_livro);
		}

		[Fact]
		public void QuandoEuCrioUmLivro_AQuantidadeDeAutoresDeveSerZero()
		{
			//Arrange

			//Act

			//Assert
			_livro.QuantidadeAutores.Should().Be(0);
		}

		[Fact]
		public void QuandoEuCrioUmLivro_APropriedadePaginasDeveSerNula()
		{
			//Arrange

			//Act

			//Assert
			_livro.Paginas.Should().Be(0);
		}

		[Fact]
		public void QuandoEuCrioUmLivro_APropriedadeEdicaoDeveSerNula()
		{
			//Arrange

			//Act

			//Assert
			Assert.Null(_livro.Edicao);
		}

		[Fact]
		public void QuandoEuCrioUmLivro_APropriedadeNomeDoLivroDeveSerNula()
		{
			//Arrange

			//Act

			//Assert
			Assert.Null(_livro.Nome);
		}

		[Fact]
		public void QuandoEuCrioUmLivro_APropriedadeCodigoISBNDeveSerNula()
		{
			//Arrange

			//Act

			//Assert
			Assert.Null(_livro.ISBN);
		}

		[Fact]
		public void QuandoEuCrioUmLivro_APropriedadeVolumeDeveSerNula()
		{
			//Arrange

			//Act

			//Assert
			Assert.Null(_livro.Volume);
		}

		[Fact]
		public void QuandoEuCrioUmLivro_APropriedadeIdDeveSerZero()
		{
			//Arrange

			//Act

			//Assert
			Assert.Equal(0, _livro.Id);
		}


		[Fact]
		public void QuandoEuAdicionoUmAutorParaOLivro_EsseAutorPrecisaAtualizarAListaDeLivrosEscritos()
		{
			//Arrange
			var autor = new Autor();
			var livro = TesteLivroFactory.ObterLivro(1);

			//Act
			livro.AdicionarAutor(autor);

			//Assert
			livro.QuantidadeAutores.Should().Be(1);

			autor.Livros.Should().NotBeEmpty();
			autor.Livros.Should().HaveCount(1);
		}
	}
}