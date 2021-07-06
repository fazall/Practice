using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace BackGroundService
{
    public class TimerWorkerService : IHostedService, IDisposable 
    {
        private readonly ILogger<TimerWorkerService> logger;
        private Timer _timer;
        public TimerWorkerService(ILogger<TimerWorkerService> logger)
        {
            this.logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(OnTimer, cancellationToken, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(5));
            return Task.CompletedTask;
        }

        private void OnTimer(object state)
        {
            string city = string.Format(" https://countriesnow.space/api/v0.1/countries/population/cities");
            WebRequest requestObject = WebRequest.Create(city);
            requestObject.Method = "GET";
            HttpWebResponse webResponse = null;
            webResponse = (HttpWebResponse)requestObject.GetResponse();

            string strresulttest = null;
            using (Stream stream = requestObject.GetRequestStream())
            {
                StreamReader sr = new StreamReader(stream);
                strresulttest = sr.ReadToEnd();
                sr.Close();
            }

            //WebRequest request = WebRequest.Create(.get.AbsoluteUri + args);
            //.Method = "GET";
            //using (WebResponse response = requestObject.GetRequestStream())
            //{
            //    using (Stream stream = response.GetResponseStream())
            //    {
            //        StreamReader sr = new StreamReader(stream);
            //        strresulttest = sr.ReadToEnd();
            //        sr.Close();
            //    }
            //}

            Console.WriteLine(city);
            string url = string.Format("http://api.openweathermap.org/data/2.5.forecast/daily?q={0}&units=metric&cnt={1}","Kabul");
            logger.LogInformation("OnTimer Event called !!!");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Stop called");
            _timer.Change(Timeout.Infinite, 0);
            await Task.Delay(100);
        }

        public void Dispose()
        {
            _timer?.Dispose(); 
        }
    }
}
