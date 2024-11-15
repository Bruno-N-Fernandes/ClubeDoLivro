using ClubeDoLivro.Blazor.Code;
using ClubeDoLivro.Blazor.Pages.Autenticacao;
using MudBlazor;

namespace ClubeDoLivro.Blazor.Application.ClientServices
{
	public class AbstractServiceClient
	{
		private readonly HttpClient _httpClient;
		private readonly ISnackbar _snackbar;

		public AbstractServiceClient(IServiceProvider serviceProvider)
		{
			_httpClient = serviceProvider.GetRequiredService<HttpClient>();
			_snackbar = serviceProvider.GetRequiredService<ISnackbar>();
		}

		protected async Task<TClass> GetFromJsonAsync<TClass>(string resource)
		{
			var httpResponse = await _httpClient.GetAsync(resource);
			if (httpResponse.IsSuccessStatusCode)
				return await httpResponse.Content.ReadFromJsonAsync<TClass>();

			var apiResponse = await httpResponse.Content.ReadFromJsonAsync<Message>();
			var exception = new ApiException(apiResponse, httpResponse.StatusCode);
			if (_snackbar != null)
			{
				_snackbar.Add(apiResponse.GetMessages(), Severity.Error);
				return default;
			}

			throw exception;
		}
	}
}