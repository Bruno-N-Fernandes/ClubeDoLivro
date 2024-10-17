using ClubeDoLivro.Domains;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http;

namespace ClubeDoLivro.Blazor.Components
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
        public bool PodeRemover { get; set; } = false;


        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }


        public async Task Confirmar()
        {
			if (Autor.Id == 0)
			{
				var httpResponse = await HttpClient.PostAsJsonAsync("Autor", Autor);
				Autor = await httpResponse.Content.ReadFromJsonAsync<Autor>();
			}
			else
				await HttpClient.PutAsJsonAsync($"Autor/{Autor.Id}", Autor);

			MudDialog.Close(DialogResult.Ok(Autor));
			StateHasChanged();
		}

        public async Task Remover()
        {
			if (Autor.Id > 0)
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