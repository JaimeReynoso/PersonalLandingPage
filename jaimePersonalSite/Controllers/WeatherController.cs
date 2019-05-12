using jaimePersonalSite.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace jaimePersonalSite.Controllers
{
    [Route("api/Weather")]
    public class WeatherController: Controller
    {
        private IConfiguration _Config;

        public WeatherController(IConfiguration Config)
        {
            _Config = Config;
        }
        
        [HttpGet("[action]/{gLat}/{gLong}")]
        public IEnumerable<Period> GetWeatherForecast(string gLat, string gLong)
        {
            var url = GetForecastUrl(gLat, gLong);
            List<Period> result;
            var webRequest = (HttpWebRequest) WebRequest.Create(url);
            if (webRequest == null) return null;

            webRequest.ContentType = "application/json";
            webRequest.UserAgent = "personal";
            using(var s = webRequest.GetResponse().GetResponseStream())
            {
                using(var sr = new StreamReader(s))
                {
                    var periodJSON =  JsonConvert.DeserializeObject<dt>(sr.ReadToEnd());
                    result = periodJSON.Properties.Periods.ToList();
                }
            }

            return result;
        }

        private string GetForecastUrl(string gLat, string gLong)
        {
            string url = $"{_Config["WeatherURL"]}{gLat},{gLong}";

            var webRequest = (HttpWebRequest)WebRequest.Create(url);
            if (webRequest == null) return null;


            webRequest.ContentType = "application/json";
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            webRequest.UserAgent = "personal";
            using (var s = webRequest.GetResponse().GetResponseStream())
            {
                using (var sr = new StreamReader(s))
                {
                    var obj = JObject.Parse(sr.ReadToEnd());
                    url = (string)obj["properties"]["forecast"];
                }
            }

            return url;
        }
    }public class dt
    {
        public Property Properties { get; set; }
   
    }
    public class Property {
        public Period[] Periods;
    }
    public class Period
    {
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Temperature { get; set; }
        public string TemperatureUnit { get; set; }
        public string Icon { get; set; }
        public string ShortForecast { get; set; }
    }
}
