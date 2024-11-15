using ClubeDoLivro.Blazor.Pages.Autenticacao;
using System.Net;

namespace ClubeDoLivro.Blazor.Code
{
	public class ApiException : Exception
	{
		public Message ApiErrorResponse { get; }
		public HttpStatusCode HttpStatusCode { get; }

		public ApiException() { }

		public ApiException(string message) : base(message) { }

		public ApiException(string message, Exception innerException) : base(message, innerException) { }

		public ApiException(Message apiErrorResponse, HttpStatusCode httpStatusCode)
		{
			ApiErrorResponse = apiErrorResponse;
			HttpStatusCode = httpStatusCode;
		}

		public override string Message => $"Status: {HttpStatusCode}; Messages: {ApiErrorResponse.GetMessages()}";
	}
}