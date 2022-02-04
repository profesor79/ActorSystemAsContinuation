using Microsoft.AspNetCore.Mvc;
using Akka.Actor;
using Akka;
namespace ActorSystemAsAContinuation.Controllers
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
        public IEnumerable<WeatherForecast> Get()
        {

            var returnValue =
            Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();

            
            var t = Task.Factory.StartNew(() =>
            {
                DoSmth(returnValue[0].Summary + " internal " + DateTime.Now.ToString("ss.ffff"));
            });

            ActorSystemSetupAndReferences.ContiueActorRef.Tell(returnValue[0].Summary
                + " actor " + DateTime.Now.ToString("ss.ffff"));
            return returnValue;
        }

        private async void DoSmth(string msg) {
            for (int i = 0; i < 10; i++)
            {
                if(i%60 == 0)
                Console.Write(".");
                Thread.Sleep(1000);
            }

            Console.WriteLine();
            Console.WriteLine("Finished        api" + msg);
        }
    }
    
}