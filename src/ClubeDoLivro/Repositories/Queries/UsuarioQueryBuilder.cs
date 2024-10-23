using ClubeDoLivro.Abstractions.Interfaces;
using ClubeDoLivro.Abstractions.Queries;
using ClubeDoLivro.Domains;

namespace ClubeDoLivro.Repositories.Queries
{
    public class UsuarioQueryBuilder : QueryBuilder<Usuario>
    {
        public UsuarioQueryBuilder(IDialect dialect) : base(dialect)
        {
            For("Usuario", "Id")
                .Add(c => c.Id)
                .Add(c => c.Nome)
                .Add(c => c.EMail)
                .Add(c => c.Telefone)
                .Add(c => c.Senha)
                .Build(this);
        }
    }
}