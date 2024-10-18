using ClubeDoLivro.Abstractions;
using ClubeDoLivro.Domains;
using System;

namespace ClubeDoLivro.Repositories
{
    public class UsuarioRepository : AbstractRepository<Usuario>
    {
        public UsuarioRepository(IServiceProvider serviceProvider) : base(serviceProvider) { }
    }
}
