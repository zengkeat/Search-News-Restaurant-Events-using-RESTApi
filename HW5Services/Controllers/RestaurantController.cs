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
using System.Threading.Tasks;

namespace HW5Services.Controllers
{
    public class RestaurantController : ApiController
    {
        [HttpGet]
        public async Task<List<restaurant>> restaurantSearch(string location, int radius, [FromUri] string[] categories, int price) {

            string prices;

            //evaluate the price from users
            if (price >0 && price <= 20) {

                prices = "1";

            } else if (price > 20 && price <= 80) {

                prices = "2";

            } else if (price > 80 && price <= 150) {

                prices = "3";

            }else{

                prices = "4";
            }
            RootObject Object1 = new RootObject();
            RootObject Object2 = new RootObject();

            //the token to access the api
            string token = "2FF1jcE7r4f0L7LybC4SUhaghjOkkQmc8CJdaLp8eQ5PTWyogD_gVvp_W39oWDQrePqyWTleVjqU7SSBUJIwCJlcqgq3GDO6JyOAlrRpgNRGagmc_zD8jvqsUoY-YnYx";

            //get all the restaurant that match
            for (int i =0; i< categories.Length; i++) {
               
                string url = "https://api.yelp.com/v3/businesses/search?" +
                    "term=restaurants&"+
                      "location=" + location + "&" +
                     "radius=" + radius + "&" +
                     "categories=" + categories[i] + "&" +
                     "price=" + prices;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
                request.Headers.Add("Authorization", "Bearer " + token);
                WebResponse response = request.GetResponse();
                Console.WriteLine(request);
                Stream dataStream = response.GetResponseStream();
                StreamReader sreader = new StreamReader(dataStream);
                string responsereader = sreader.ReadToEnd();
                response.Close();

                Object1 = JsonConvert.DeserializeObject<RootObject>(responsereader);

                Object2.businesses.AddRange(Object1.businesses);
                await Task.Delay(300);

            }

           //find the review for each of the restaurant
            for (int i = 0; i < Object2.businesses.Count; i++)
            {
                RootObject object3 = new RootObject();

                string url = "https://api.yelp.com/v3/businesses/"+Object2.businesses[i].id+"/reviews";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Headers.Add("Authorization", "Bearer " + token);
                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader sreader = new StreamReader(dataStream);
                string responsereader = sreader.ReadToEnd();
                response.Close();

                object3 = JsonConvert.DeserializeObject<RootObject>(responsereader);
                Object2.businesses[i].reviews = object3.reviews;
                await Task.Delay(200);
            }

            return Object2.businesses;

        }

    }


    public class restaurant
    {
        //the method name must be same with the key value in the json struture 
        public decimal rating { get; set; }
        public String price { get; set; }
        public String id { get; set; }
        public String name { get; set; }
        public address location { get; set; }
        public List<review> reviews { get; set; } = new List<review>();
        public List<categories> categories { get; set; } = new List<categories>();

    }

    public class RootObject
    {
        // since article is an array, so we make an array of the same name as the key 
        public List<restaurant> businesses { get; set; } = new List<restaurant>();
        public List<review> reviews { get; set; } = new List<review>();
    }

    public class categories {
        public String alias { get; set; }
        public String title { get; set; }

    }

    public class address {

        public String city { get; set; }
        public String country { get; set; }
        public String address2 { get; set; }
        public String address3 { get; set; }
        public String state { get; set; }
        public String address1 { get; set; }
        public String zip_code { get; set; }

    }

    public class review {

        public user user { get; set; }
        public int rating{ get; set; }
        public String text { get; set; }    
        public String time_created  { get; set; }

    }

    public class user { 
    
        public String name { get; set; }    
    }
}