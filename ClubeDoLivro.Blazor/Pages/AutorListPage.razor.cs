using ClubeDoLivro.Domains;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ClubeDoLivro.Blazor.Pages
{
    [Route("/Autor"), Route("/Autor/{id}")]
    public partial class AutorListPage
    {
        [Inject]
        public HttpClient HttpClient { get; set; }

        [Inject]
        private IDialogService DialogService { get; set; }

        [Inject]
        private NavigationManager Navigation { get; set; }

        [Parameter]
        public string id { get; set; }

        public List<Autor> Autores { get; set; }

        protected override async Task OnInitializedAsync()
        {
            //autores = await HttpClient.GetFromJsonAsync<Autor[]>("");

            Autores = [
                new() { Id = 1, Nome = "Bruno", Sobrenome = "Fernandes", Livros = [] },
                new() { Id = 2, Nome = "Walmir", Sobrenome = "Oliveira", Livros = [] }
            ];

            await base.OnInitializedAsync();
        }

        public async Task OpenPopup(Autor autor = null, bool podeRemover = false)
        {
            var parameters = new DialogParameters
            {
                { "autor", autor?.Clone() ?? new Autor() },
                { "podeRemover", podeRemover }
            };

            var options = new DialogOptions { CloseOnEscapeKey = true, FullWidth = true };
            var dialog = DialogService.Show<AutorPage>("Autor", parameters, options);
            var result = await dialog.Result;

            if (!result.Canceled)
            {
                if (podeRemover)
                    Autores = Autores.Where(n => n.Id != autor.Id).ToList();
                else
                {
                    if (autor == null)
                    {
                        Autores.Add(result.Data as Autor);
                    }
                    else
                    {
                        var autorAlterado = result.Data as Autor;
                        autor.Nome = autorAlterado.Nome;
                        autor.Sobrenome = autorAlterado.Sobrenome;
                    }

                }

                StateHasChanged();
            }
        }
    }
}