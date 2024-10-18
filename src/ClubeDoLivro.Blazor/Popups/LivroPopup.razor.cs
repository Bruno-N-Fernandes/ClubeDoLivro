using ClubeDoLivro.Blazor.Code;
using ClubeDoLivro.Domains;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ClubeDoLivro.Blazor.Popups
{
	public partial class LivroPopup
    {

		[CascadingParameter]
		public MudDialogInstance MudDialog { get; set; }

		[Inject]
        public HttpClient HttpClient { get; set; }
  
        [Parameter]
        public Livro Livro { get; set; }

		[Parameter]
		public Mode Mode { get; set; } = Mode.Incluir;

        public async Task Incluir()
        {
			var httpResponse = await HttpClient.PostAsJsonAsync("Livro", Livro);
			Livro = await httpResponse.Content.ReadFromJsonAsync<Livro>();
			MudDialog.Close(DialogResult.Ok(Livro));
			StateHasChanged();
		}

		public async Task Alterar()
		{
			await HttpClient.PutAsJsonAsync($"Livro/{Livro.Id}", Livro);
			MudDialog.Close(DialogResult.Ok(Livro));
			StateHasChanged();
		}

        public async Task Excluir()
        {
			await HttpClient.DeleteAsync($"Livro/{Livro.Id}");
			MudDialog.Close(DialogResult.Ok(Livro));
			StateHasChanged();
		}

		public async Task Cancelar()
        {
			MudDialog.Close(DialogResult.Cancel());
			await Task.CompletedTask;
        }
	}
}