using ClubeDoLivro.Abstractions;
using ClubeDoLivro.Domains;
using System;

namespace ClubeDoLivro.Services
{
    public class UsuarioService : AbstractService<Usuario>
    {
        public UsuarioService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}