using Microsoft.AspNetCore.Components;

namespace ClubeDoLivro.Blazor.Pages
{
    public partial class Weather
    {
        [Inject]
        public HttpClient HttpClient { get; set; }

        private WeatherForecast[] forecasts;

        protected override async Task OnInitializedAsync()
        {
            forecasts = await HttpClient.GetFromJsonAsync<WeatherForecast[]>("sample-data/weather.json");
        }

        public class WeatherForecast
        {
            public DateOnly Date { get; set; }

            public int TemperatureC { get; set; }

            public string? Summary { get; set; }

            public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        }
    }
}