using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jaimePersonalSite.Model
{
    public class Response
    {
        public string Section { get; set; }
        public int Num_Results { get; set; }
        public NewsArticle[] Results {get;set;}
    }
    public class ThumbNail
    {
        public string URL { get; set; }
        public string Caption { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public string Type { get; set; }
    }
    public class NewsArticle
    {
        public string Section { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; }
        public ThumbNail[] Multimedia { get; set; }
        public string Url { get; set; }
    }
}
