using ClubeDoLivro.Abstractions.Interfaces;
using ClubeDoLivro.Abstractions.Queries;
using ClubeDoLivro.Domains;

namespace ClubeDoLivro.Repositories.Queries
{
	public class AutorQueryBuilder : QueryBuilder<Autor>
	{
		public AutorQueryBuilder(IDialect dialect) : base(dialect)
		{
			For("Autor", "Id")
				.Add(c => c.Id)
				.Add(c => c.Nome)
				.Add(c => c.Sobrenome)
				.Build(this);
		}
	}
}