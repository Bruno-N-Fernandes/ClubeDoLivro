using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;

namespace ClubeDoLivro.Blazor.Layout
{
	public partial class AuthLayout
	{
		[Inject]
		private ILocalStorageService LocalStorageService { get; set; }

		public bool IsDarkMode { get; set; }

		protected override async Task OnParametersSetAsync()
		{
			await base.OnParametersSetAsync();

			if (await LocalStorageService.ContainKeyAsync("DarkModeThemeEnabled"))
				IsDarkMode = await LocalStorageService.GetItemAsync<bool>("DarkModeThemeEnabled");

			StateHasChanged();
		}
	}
}