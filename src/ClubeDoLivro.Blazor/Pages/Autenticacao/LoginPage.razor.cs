using Blazored.LocalStorage;
using ClubeDoLivro.Abstractions;
using ClubeDoLivro.Blazor.Application.ClientServices.Interfaces;
using ClubeDoLivro.Blazor.Code;
using ClubeDoLivro.Blazor.Layout;
using ClubeDoLivro.Domains;
using ClubeDoLivro.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ClubeDoLivro.Blazor.Pages.Autenticacao
{
	[Layout(typeof(AuthLayout))]
	[Route("/login")]
	public partial class LoginPage
	{
		[Inject]
		public IAuthenticationService AuthenticationService { get; set; }

		[Inject]
		public NavigationManager NavigationManager { get; set; }

		[Inject]
		public ISnackbar Snackbar { get; set; }

		public LoginRequest LoginRequest { get; set; } = new LoginRequest();

		private bool _loginInProgress = false;

		public async Task EfetuarLogin()
		{
			_loginInProgress = true;
			try
			{
				var authenticationState = await AuthenticationService.Authenticate(LoginRequest);
				var nome = authenticationState.User.GetName();
				Snackbar.Add($"Seja bem vindo, {nome}", Severity.Success);
				NavigationManager.NavigateTo("/");
			}
			catch (ApiException ex)
			{
				Snackbar.Add(ex.Message, Severity.Warning);
			}
			catch (Exception ex)
			{
				Snackbar.Add(ex.Message, Severity.Error);
			}

			_loginInProgress = false;
		}
	}


	public class Message
	{
		public List<string> Messages { get; set; }

		public Message() => Messages = [];

		public Message(string message) => Messages = [message];

		public Message(IEnumerable<string> message) => Messages = new List<string>(message);

		public string GetMessages(string join = "\r\n") => string.Join(join, Messages);
	}
}