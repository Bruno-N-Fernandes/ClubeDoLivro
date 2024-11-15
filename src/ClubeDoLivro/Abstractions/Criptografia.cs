using System;
using System.Security.Cryptography;
using System.Text;

namespace ClubeDoLivro.Abstractions
{
	public static class Criptografia
	{
		public static string Criptografar(string texto)
		{
			using var sha256 = SHA256.Create();
			var bytes = Encoding.UTF8.GetBytes(texto);
			var cryptText = sha256.ComputeHash(bytes);
			return Convert.ToBase64String(cryptText);
		}
	}
}