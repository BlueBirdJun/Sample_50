using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Sample_50
{
    public class WebClientService
    {
        private readonly HttpClient httpClient;
        private readonly ServiceSettings settings;
        public WebClientService(HttpClient httpClient ,IOptions<ServiceSettings> options)
        {
            this.httpClient = httpClient;            
            settings = options.Value;
            //this.httpClient.BaseAddress = settings.OpenWeatherhost;
        }

        public record Weather(string description);      
        public record Main(decimal temp, decimal temp_min);        
        public record Forecast(Weather[]  weather,Main main,long dt);

        public async Task<Forecast> GetCurrentWeatherAsync(string city)
        {
            //var forecast =await httpClient.GetFromJsonAsync<Forecast>("https://some.co.kr/sometrend/analysis/trend/transition?sources=13&categories=2046&endDate=20201113&startDate=20201107&keyword=%ED%85%90%EB%B0%94%EC%9D%B4%ED%85%90&period=0");
            //var forecast = await httpClient.GetFromJsonAsync<Forecast>("api.openweathermap.org/data/2.5/weather?q=London,uk&appid=9127ef1c1f5312246715613ff1baefa9");
            var forecast = await httpClient.GetFromJsonAsync<Forecast>($"https://{settings.OpenWeatherhost}/data/2.5/weather?q={city},uk&appid={settings.ApiKey}");
            return forecast;

        }
    }
}
