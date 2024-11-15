using ClubeDoLivro.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ClubeDoLivro.Domains
{
	public class LoginRequest
	{
		[Required, EmailAddress]
		public string EMail { get; set; }

		[Required, StringLength(20, MinimumLength = 4)]
		public string Senha { get; set; }

		[JsonIgnore]
		public bool IsValid => !string.IsNullOrWhiteSpace(EMail) && !string.IsNullOrWhiteSpace(Senha);
	
		public LoginRequest Criptografar()
		{
			return new LoginRequest
			{
				EMail = EMail,
				Senha = Criptografia.Criptografar(Senha),
			};
		}
	}
}
