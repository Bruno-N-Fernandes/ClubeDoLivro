using ClubeDoLivro.Blazor.Code;
using ClubeDoLivro.Domains;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ClubeDoLivro.Blazor.Popups
{
	public partial class AutorPopup
    {

		[CascadingParameter]
		public MudDialogInstance MudDialog { get; set; }

		[Inject]
        public HttpClient HttpClient { get; set; }
  
        [Parameter]
        public Autor Autor { get; set; }

		[Parameter]
		public Mode Mode { get; set; } = Mode.Incluir;

        public async Task Incluir()
        {
			var httpResponse = await HttpClient.PostAsJsonAsync("Autor", Autor);
			Autor = await httpResponse.Content.ReadFromJsonAsync<Autor>();
			MudDialog.Close(DialogResult.Ok(Autor));
			StateHasChanged();
		}

		public async Task Alterar()
		{
			await HttpClient.PutAsJsonAsync($"Autor/{Autor.Id}", Autor);
			MudDialog.Close(DialogResult.Ok(Autor));
			StateHasChanged();
		}

        public async Task Excluir()
        {
			await HttpClient.DeleteAsync($"Autor/{Autor.Id}");
			MudDialog.Close(DialogResult.Ok(Autor));
			StateHasChanged();
		}

		public async Task Cancelar()
        {
			MudDialog.Close(DialogResult.Cancel());
			await Task.CompletedTask;
        }
	}
}