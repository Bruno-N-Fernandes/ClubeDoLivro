using Blazored.LocalStorage;
using ClubeDoLivro.Abstractions;
using ClubeDoLivro.Blazor.Application.ClientServices.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace ClubeDoLivro.Blazor.Components
{
	public partial class LoginDisplay : ComponentBase
	{
		[Inject]
		public ILocalStorageService LocalStorageService { get; set; }

		[Inject]
		public IAuthenticationService AuthenticationService { get; set; }

		[Inject]
		public NavigationManager NavigationManager { get; set; }

		[CascadingParameter]
		public Task<AuthenticationState> AuthenticationState { get; set; }

		private string _nomeUsuario;

		protected override async Task OnParametersSetAsync()
		{
			_nomeUsuario = (await AuthenticationState)?.User?.GetName();
			var expiryDate = await AuthenticationService.GetExpiryDate();
			if (_nomeUsuario == null || expiryDate < DateTime.UtcNow)
				await GoToLogout();
		}

		private async Task GoToLogin()
		{
			NavigationManager.NavigateTo("login");
			await Task.CompletedTask;
		}

		private async Task GoToLogout()
		{
			await AuthenticationService.SetLogout();
			NavigationManager.NavigateTo("login");
		}
	}
}