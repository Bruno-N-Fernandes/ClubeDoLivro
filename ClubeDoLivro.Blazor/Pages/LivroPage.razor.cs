using ClubeDoLivro.Domains;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ClubeDoLivro.Blazor.Pages
{
    [Route("/Livro")]
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

		public async Task OpenPopup(Livro livro = null, bool podeRemover = false)
		{
			//var parameters = new DialogParameters
			//{
			//	{ "livro", livro?.Clone() ?? new Livro() },
			//	{ "podeRemover", podeRemover }
			//};

			//var options = new DialogOptions { CloseOnEscapeKey = true, FullWidth = true };
			//var dialog = DialogService.Show<LivroPopup>("Autor", parameters, options);
			//var result = await dialog.Result;

			//if (!result.Canceled)
			//{
			//	if (podeRemover)
			//		Livros = Livros.Where(n => n.Id != livro.Id).ToList();
			//	else
			//	{
			//		if (livro == null)
			//		{
			//			Livros.Add(result.Data as Livro);
			//		}
			//		else
			//		{
			//			var autorAlterado = result.Data as Livro;
			//			livro.Nome = autorAlterado.Nome;
			//			livro.Sobrenome = autorAlterado.Sobrenome;
			//		}

			//	}

			//	StateHasChanged();
			//}
			await Task.CompletedTask;

		}
	}
}