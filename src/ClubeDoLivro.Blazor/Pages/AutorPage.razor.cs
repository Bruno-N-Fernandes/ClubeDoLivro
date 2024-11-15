using ClubeDoLivro.Blazor.Code;
using ClubeDoLivro.Blazor.Popups;
using ClubeDoLivro.Domains;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ClubeDoLivro.Blazor.Pages
{
	[Route("/Autor")]
	[Authorize]
	public partial class AutorPage
	{
		[Inject]
		public HttpClient HttpClient { get; set; }

		[Inject]
		private IDialogService DialogService { get; set; }

		[Inject]
		private NavigationManager Navigation { get; set; }

		public List<Autor> Autores { get; set; }

		protected override async Task OnInitializedAsync()
		{
			Autores = await HttpClient.GetFromJsonAsync<List<Autor>>("Autor");

			await base.OnInitializedAsync();
		}

		public async Task OpenPopup(Autor autor = null, Mode mode = Mode.Incluir)
		{
			var parameters = new DialogParameters
			{
				{ "mode", mode },
				{ "autor", autor?.Clone() ?? new Autor() },
			};

			var options = new DialogOptions { CloseOnEscapeKey = true, FullWidth = true };
			var dialog = DialogService.Show<AutorPopup>("Autor", parameters, options);
			var result = await dialog.Result;

			if (!result.Canceled)
			{
				switch (mode)
				{
					case Mode.Incluir:
						Autores.Add(result.Data as Autor);
						break;
					case Mode.Alterar:
						var autorAlterado = result.Data as Autor;
						autor.Alterar(autorAlterado);
						break;
					case Mode.Excluir:
						Autores = Autores.Where(n => n.Id != autor.Id).ToList();
						break;
				}

				StateHasChanged();
			}
		}
	}
}