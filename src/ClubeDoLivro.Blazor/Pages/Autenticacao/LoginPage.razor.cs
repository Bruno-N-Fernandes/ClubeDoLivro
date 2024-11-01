using Blazored.LocalStorage;
using ClubeDoLivro.Blazor.Application.Security;
using ClubeDoLivro.Domains;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;

namespace ClubeDoLivro.Blazor.Pages.Autenticacao
{
	public partial class LoginPage
	{
		[Inject]
		public HttpClient HttpClient { get; set; }

		[Inject]
		public NavigationManager NavigationManager { get; set; }

		[Inject]
		public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

		[Inject]
		public ILocalStorageService LocalStorageService { get; set; }

		[Inject]
		public ISnackbar Snackbar { get; set; }

		public LoginRequest LoginRequest { get; set; } = new LoginRequest();

		private bool _loginInProgress = false;

		public async Task EfetuarLogin()
		{
			_loginInProgress = true;
			var response = await HttpClient.PostAsJsonAsync("Login/Authenticate", LoginRequest);
			if (response.IsSuccessStatusCode)
			{
				var token = await response.Content.ReadFromJsonAsync<AccessToken>();
				await LocalStorageService.SetItemAsStringAsync(IJwtService.cAccessToken, token.Token);
				await LocalStorageService.SetItemAsync(IJwtService.cExpiryDate, token.ExpiresAt);

				await AuthenticationStateProvider.GetAuthenticationStateAsync();

				NavigationManager.NavigateTo("/");
			}
			else
			{
				var error = await response.Content.ReadFromJsonAsync<Message>();
				Snackbar.Add(string.Join(";", error.Messages));
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
	}

	public class LoginRequest
	{
        public string EMail { get; set; }
        public string Senha { get; set; }
    }
}
