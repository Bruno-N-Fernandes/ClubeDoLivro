using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ClubeDoLivro.Blazor.Layout
{
	public partial class MainLayout
	{
		private const string DarkModeName = "DarkModeThemeEnabled";

		[Inject]
		private ILocalStorageService LocalStorageService { get; set; }

		private MudThemeProvider _mudThemeProvider;

		private bool _menuIsOpen = true;

		private bool _darkModeThemeEnabled;

		private bool DarkModeThemeEnabled
		{
			get => _darkModeThemeEnabled;
			set => SaveDarkMode(_darkModeThemeEnabled = value).GetAwaiter();
		}

		protected override async Task OnParametersSetAsync()
		{
			if (await LocalStorageService.ContainKeyAsync(DarkModeName))
				DarkModeThemeEnabled = await LocalStorageService.GetItemAsync<bool>(DarkModeName);
			else
				DarkModeThemeEnabled = await _mudThemeProvider.GetSystemPreference();
		}

		private void DrawerToggle() => _menuIsOpen = !_menuIsOpen;

		private async Task SaveDarkMode(bool darkModeThemeEnabled)
		{
			await LocalStorageService.SetItemAsync(DarkModeName, darkModeThemeEnabled);

			StateHasChanged();
		}
	}
}