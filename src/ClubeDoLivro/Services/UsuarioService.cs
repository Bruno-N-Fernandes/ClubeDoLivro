using ClubeDoLivro.Abstractions;
using ClubeDoLivro.Domains;
using ClubeDoLivro.Repositories;
using System;
using System.Threading.Tasks;

namespace ClubeDoLivro.Services
{
    public class UsuarioService : AbstractService<Usuario>
	{
		private readonly UsuarioRepository UsuarioRepository;
		private readonly IJwtService JwtService;

		public UsuarioService(IServiceProvider serviceProvider) : base(serviceProvider)
		{
			UsuarioRepository = (UsuarioRepository)Repository;
			JwtService = GetService<IJwtService>();
		}

		public async Task<AccessToken> EfetuarLogin(LoginRequest loginRequest)
		{
			var usuario = await UsuarioRepository.ObterPorEMailESenha(loginRequest);
			if (usuario == null)
				throw new Exception("Usuario não encontrado com este e-mail e senha!");

			return JwtService.GerarJwtToken(usuario);
		}
	}
}