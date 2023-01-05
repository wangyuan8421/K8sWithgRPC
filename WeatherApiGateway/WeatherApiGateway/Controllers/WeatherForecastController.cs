using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using TempService;

namespace WeatherApiGateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            using (var httpClientHandler = new HttpClientHandler())
            {
                // by pass ssl check
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    #region by pass ssl check
                    var opt = new GrpcChannelOptions();
                    opt.HttpClient= httpClient;
                    using var channel = GrpcChannel.ForAddress("https://172.21.36.96:443", opt);
                    #endregion
                    var client = new Temp.TempClient(channel);

                    var reply = await client.GetTempAsync(new TempRequest { Name = "TempRequest" });

                    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                    {
                        Date = DateTime.Now.AddDays(index),
                        TemperatureC = Random.Shared.Next(-20, 55),
                        Summary = reply.Message
                    })
                    .ToArray();
                }
            }
            
        }
    }
}