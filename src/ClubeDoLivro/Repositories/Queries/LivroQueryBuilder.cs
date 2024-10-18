using ClubeDoLivro.Abstractions.Interfaces;
using ClubeDoLivro.Abstractions.Queries;
using ClubeDoLivro.Domains;

namespace ClubeDoLivro.Repositories.Queries
{
    public class LivroQueryBuilder : QueryBuilder<Livro>
    {
        public LivroQueryBuilder(IDialect dialect) : base(dialect)
        {
            For("Livro", "Id")
                .Add(c => c.Id)
                .Add(c => c.Nome)
                .Add(c => c.Volume)
                .Add(c => c.Edicao)
                .Add(c => c.ISBN)
                .Add(c => c.Paginas)
                .Build(this);
        }
    }
}