using ClubeDoLivro.Abstractions.Interfaces;

namespace ClubeDoLivro.Domains
{
    public class Usuario : IEntity
    {
        public static Usuario Anonymous => AnonymouUser.Instance;
		public int Id { get; set; }
        public string Nome { get; set; }
        public string EMail { get; set; }
        public string Telefone { get; set; }
        public string Senha { get; set; }

		private sealed class AnonymouUser : Usuario
		{
			internal static readonly AnonymouUser Instance = new AnonymouUser();
			private AnonymouUser()
			{
				Id = int.MinValue;
				Nome = "User";
				EMail = "user@anonymo.us";
				Telefone = "00 0 0000-0000";
				Senha = "***";
			}
		}
	}
}
