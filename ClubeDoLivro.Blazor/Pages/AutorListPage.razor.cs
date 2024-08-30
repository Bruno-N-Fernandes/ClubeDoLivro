using ClubeDoLivro.Domains;
using Microsoft.AspNetCore.Components;

namespace ClubeDoLivro.Blazor.Pages
{
    public partial class AutorListPage
    {
        [Inject]
        public HttpClient HttpClient { get; set; }

        private List<Autor> autores;

        protected override async Task OnInitializedAsync()
        {
            //autores = await HttpClient.GetFromJsonAsync<Autor[]>("");

            autores = new List<Autor> {
                new Autor{ Id=1, Nome = "Bruno", Sobrenome = "Fernandes", Livros = new List<Livro>() },
                new Autor{ Id=2, Nome = "Walmir", Sobrenome = "Oliveira", Livros = new List<Livro>() }
            };

            await base.OnInitializedAsync();
        }

        public async Task abrirTelaParaCadastrarAutor()
        {
            await Task.CompletedTask;
        }
    }
}