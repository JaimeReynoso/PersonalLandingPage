using jaimePersonalSite.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
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
    [Route("api/News")]
    public class NewsController: Controller
    {
        private IConfiguration _Config;
        public NewsController(IConfiguration config)
        {
            _Config = config;
        }

        [HttpGet("[action]")]
        public IEnumerable<NewsArticle> GetTopNews()
        {
            Response response;

            var webRequest = WebRequest.Create($"{_Config["NYTimesURL"]}/svc/topstories/v2/home.json?api-key={_Config["NYTimesAPIKey"]}");
            if (webRequest == null) return null;


            webRequest.ContentType = "application/json";
 
            using (var s = webRequest.GetResponse().GetResponseStream())
            {
                using (var sr = new StreamReader(s))
                {
                    response = JsonConvert.DeserializeObject<Response>(sr.ReadToEnd());
                }
            }

            return response.Results.ToList();
        }
    }
}
