using ClubeDoLivro.Abstractions;
using ClubeDoLivro.Domains;
using Dapper;
using System;
using System.Threading.Tasks;

namespace ClubeDoLivro.Repositories
{
	public class UsuarioRepository : AbstractRepository<Usuario>
    {
        public UsuarioRepository(IServiceProvider serviceProvider) : base(serviceProvider) { }

		public async Task<Usuario> ObterPorEMailESenha(LoginRequest loginRequest)
		{
			var cmdSql = _querybuilder.GetCmdSqlSelectBy(" Where (Email = @EMail) And (Senha = @Senha);");
			var loginRequestCriptografado = loginRequest.Criptografar();
			return await _connection.QuerySingleOrDefaultAsync<Usuario>(cmdSql, loginRequestCriptografado);
		}
	}
}
