using ClubeDoLivro.Domains;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubeDoLivro.Testes
{
	public class AutorTest
	{
		private readonly Autor _autor;
		public AutorTest()
		{
			_autor = new Autor();
		}

		[Fact]
		public void QuandoEuCrioUmAutor_OAutorDeveSerCriadoCorretamente()
		{
			//Arrange

			//Act

			//Assert
			Assert.NotNull(_autor);
			Assert.Equal(0, _autor.Id);
			Assert.NotNull(_autor.Livros);
			Assert.Null(_autor.Nome);
			Assert.Null(_autor.Sobrenome);
		}

		[Fact]
		public void QuandoEuAdicionoUmLivroNoAutor_AQuantidadeDeLivrosEscritosDeveAumentarEm1Unidade()
		{
			//Arrange
			var livro = TesteLivroFactory.ObterLivro();
			var quantidadeDeLivrosAntes = _autor.LivrosEscritos;
			var quantidadeEsperada = quantidadeDeLivrosAntes + 1;

			//Act
			_autor.AdicionarLivro(livro);

			//Assert
			Assert.Equal(quantidadeEsperada, _autor.LivrosEscritos);
		}

		[Fact]
		public void QuandoEuAdicionoUmLivroNoAutor_OLivroPrecisaConterOAutorNaListaDeAutores()
		{
			//Arrange
			var livro = TesteLivroFactory.ObterLivro();
			var quantidadeDeLivrosAntes = _autor.LivrosEscritos;
			var quantidadeEsperada = quantidadeDeLivrosAntes + 1;

			//Act
			_autor.AdicionarLivro(livro);

			//Assert
			Assert.True(livro.QuantidadeAutores == 1);
			livro.FoiEscritoPelo(_autor).Should().BeTrue();
		}

		[Fact]
		public void QuandoEuAdicionoDoisLivrosNoAutor_AutorPrecisaTerDoisLivros()
		{
			//Arrange
			var livro1 = TesteLivroFactory.ObterLivro(1);
			var livro2 = TesteLivroFactory.ObterLivro(2);
			var quantidadeDeLivrosAntes = _autor.LivrosEscritos;
			var quantidadeEsperada = quantidadeDeLivrosAntes + 2;

			//Act
			_autor.AdicionarLivro(livro1);
			_autor.AdicionarLivro(livro2);

			//Assert
			Assert.Contains(livro1, _autor.Livros);
			Assert.Contains(livro2, _autor.Livros);
			_autor.LivrosEscritos.Should().Be(quantidadeEsperada);
		}

		[Fact]
		public void QuandoEuDigoQueUmAutorEscreveuUmLivro_EsseLivroDeveConterOAutorEmSuaListaDeAutores()
		{
			//Arrange
			var livro1 = TesteLivroFactory.ObterLivro();

			//Act
			_autor.AdicionarLivro(livro1);

			//Assert
			_autor.Livros.Should().NotBeEmpty();
			_autor.Livros.Should().HaveCount(1);

			livro1.QuantidadeAutores.Should().Be(1);
		}
	}

	public class TesteLivroFactory
	{
		public static Livro ObterLivro(int id = 0)
		{
			return new Livro { Id = id, CodigoISBN = "113121", Edicao = "1", NomeDoLivro = "C#", Paginas = 1, Volume = "1" };
		}
	}
}
