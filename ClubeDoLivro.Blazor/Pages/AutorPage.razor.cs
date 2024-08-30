using ClubeDoLivro.Domains;
using Microsoft.AspNetCore.Components;

namespace ClubeDoLivro.Blazor.Pages
{
    public partial class AutorPage
    {
        [Inject]
        public HttpClient HttpClient { get; set; }

       public Autor autor { get; set; }

        protected override async Task OnInitializedAsync()
        {
            autor = new Autor {};

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