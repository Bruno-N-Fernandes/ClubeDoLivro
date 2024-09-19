using ClubeDoLivro.Domains;
using Microsoft.AspNetCore.Components;
using MudBlazor;

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
			MudDialog.Close(DialogResult.Ok(Autor));
			await Task.CompletedTask;
		}

        public async Task Cancelar()
        {
			MudDialog.Close(DialogResult.Cancel());
			await Task.CompletedTask;
        }
    }
}