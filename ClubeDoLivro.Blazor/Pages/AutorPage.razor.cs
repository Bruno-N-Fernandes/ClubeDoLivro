using ClubeDoLivro.Domains;
using Microsoft.AspNetCore.Components;

namespace ClubeDoLivro.Blazor.Pages
{
    public partial class AutorPage
    {
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

            await Task.CompletedTask;
        }

        public async Task Cancelar()
        {

            await Task.CompletedTask;
        }
    }
}