using ClubeDoLivro.Blazor.Code;
using ClubeDoLivro.Blazor.Popups;
using ClubeDoLivro.Domains;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ClubeDoLivro.Blazor.Pages
{
    [Route("/Livro")]
	[Authorize]
	public partial class LivroPage
    {
        [Inject]
        public HttpClient HttpClient { get; set; }

        [Inject]
        private IDialogService DialogService { get; set; }

        [Inject]
        private NavigationManager Navigation { get; set; }

        public List<Livro> Livros { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Livros = await HttpClient.GetFromJsonAsync<List<Livro>>("Livro");
            await base.OnInitializedAsync();
        }

        public async Task OpenPopup(Livro livro = null, Mode mode = Mode.Incluir)
        {
            var parameters = new DialogParameters
            {
                { "mode", mode },
                { "livro", livro?.Clone() ?? new Livro() },
            };

            var options = new DialogOptions { CloseOnEscapeKey = true, FullWidth = true };
            var dialog = DialogService.Show<LivroPopup>("Livro", parameters, options);
            var result = await dialog.Result;

            if (!result.Canceled)
            {
                switch (mode)
                {
                    case Mode.Incluir:
                        Livros.Add(result.Data as Livro);
                        break;
                    case Mode.Alterar:
                        var livroAlterado = result.Data as Livro;
                        livro.Alterar(livroAlterado);
                        break;
                    case Mode.Excluir:
                        Livros = Livros.Where(n => n.Id != livro.Id).ToList();
                        break;
                }

                StateHasChanged();
            }
        }
    }
}