using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;
using System.Xml;
using Newtonsoft.Json;


namespace HW5Services.Controllers
{
    public class NewsController : ApiController
    {
        [HttpGet]
        public List<article> NewsFocus([FromUri] string[] topics)
        {

            RootObject2 newsObject = new RootObject2();
            RootObject2 newsObject2 = new RootObject2();

            string date = DateTime.Now.ToString("yyyy’-‘MM’-‘dd’");
            for (int i = 0; i < topics.Length; i++)
            {

                string url1 = @"https://newsapi.org/v2/everything?" +
                          "q=" + topics[i] + "&" +
                          "searchIn=title&"+
                          "from=" +date +"&" +
                          "sortBy=popularity&" +
                          "pageSize=10&"+
                          "apiKey=b919146750f9435f9839e9d7e1fbbcb1";

                string url2 = @"https://newsapi.org/v2/everything?q=apple&from=2023-07-05&to=2023-07-05&sortBy=popularity&apiKey=b919146750f9435f9839e9d7e1fbbcb1";


                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url1);
                request.ContentType = "application/json; charset=utf-8";
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader sreader = new StreamReader(dataStream);
                string responsereader = sreader.ReadToEnd();
                response.Close();

                newsObject = JsonConvert.DeserializeObject<RootObject2>(responsereader);

                newsObject2.articles.AddRange(newsObject.articles);

            }


            return newsObject2.articles;
            
        }
    }

    public class article
    {
        //the method name must be same with the key value in the json struture 
        public String title { get; set; }
        public String author { get; set; }
        public String url { get; set; }

    }

    public class RootObject2
    {
        // since article is an array, so we make an array of the same name as the key 
        public List<article> articles { get; set; } = new List<article>();

    }

   
}