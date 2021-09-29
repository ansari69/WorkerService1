using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace WorkerService1
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private List<string> urls = new List<string> { "https://www.google.com/" };

        IHttpClientFactory _httpClientFactory;

        public Worker(ILogger<Worker> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            //    await Task.Delay(1000, stoppingToken);
            //}

            while (!stoppingToken.IsCancellationRequested)
            {


                try
                {
                    await aa();
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex , "sssssssssssssss");

                }
                finally
                {
                    await Task.Delay(1000, stoppingToken);

                }


            }








        }


        private async Task aa()
        {

            var tasks = new List<Task>();

            
            foreach(var url in urls)
            {
                tasks.Add(bb(url));
            }

            await Task.WhenAll(tasks);

        }

        private async Task bb(string url)
        {

            try
            {
                var client = _httpClientFactory.CreateClient();
                var r = await client.GetAsync(url);

                if (r.IsSuccessStatusCode)
                    _logger.LogInformation("{Url} onlion ", url);
                else
                    _logger.LogWarning("{Url} offline ", url);

            }
            catch(Exception ex)
            {
                _logger.LogWarning(ex, "sssssssssssssss");

            }

        }
    }
}
