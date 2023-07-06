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
     public class EventController : ApiController
     {
 
         [HttpGet]
         public List<all_events> eventSearch(string zipcode, string place)
         {

            List<all_events> all_events = new List<all_events>();

            //******ticket master event search ****
            RootObjects Object = new RootObjects();

            string url = "https://app.ticketmaster.com/discovery/v2/events.json?" +
                "postalCode="+zipcode + "&"+
                "sort=date,asc&" +
                "apikey=qTvcgXT785WRlab7kpzC7CeR3yvohvS8";
 

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader sreader = new StreamReader(dataStream);
            string responsereader = sreader.ReadToEnd();
            response.Close();

            Object = JsonConvert.DeserializeObject<RootObjects>(responsereader);

            //after getting all the information, split it and put into new class that i design
            for (int i =0; i < Object._embedded.events.Count; i++) { 

                all_events event_node = new all_events();

                event_node.name = Object._embedded.events[i].name;
                event_node.url = Object._embedded.events[i].url;
                event_node.dates = Object._embedded.events[i].dates.start.dateTime;
                event_node.description = "none";
                event_node.address = Object._embedded.events[i]._embedded.venues[0].address.line1 + ", " +
                                    Object._embedded.events[i]._embedded.venues[0].city.name + ", " +
                                        Object._embedded.events[i]._embedded.venues[0].state.name + ", " +
                                        Object._embedded.events[i]._embedded.venues[0].postalCode + ", " +
                                        Object._embedded.events[i]._embedded.venues[0].country.name;

                event_node.timezone = Object._embedded.events[i]._embedded.venues[0].timezone;

                all_events.Add(event_node);
            }


            //******google event search
            RootObjects Object2 = new RootObjects();

            string url2 = "https://serpapi.com/search.json?engine=google_events" +
                "&q=" +place +
                "&hl=en" +
                "&gl=us" +
                "&api_key=e84e061f951c3c43dc39df1a7b704a4e36e572c4b84a3883e4b303797cd0b76e";
                

            HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(url2);
            WebResponse response2 = request2.GetResponse();
            Stream dataStream2 = response2.GetResponseStream();
            StreamReader sreader2 = new StreamReader(dataStream2);
            string responsereader2 = sreader2.ReadToEnd();
            response2.Close();

            Object2 = JsonConvert.DeserializeObject<RootObjects>(responsereader2);

            //after getting all the information from api, reformat those information and put into 
            // the struture i design 
            for (int i = 0; i < Object2.events_results.Count; i++)
            {

                all_events event_node = new all_events();

                event_node.name = Object2.events_results[i].title;
                event_node.dates = Object2.events_results[i].date.start_date + "\r\n"+
                                  "When: "+ Object2.events_results[i].date.when;
                event_node.url = Object2.events_results[i].link;
                event_node.description = Object2.events_results[i].description;
                event_node.address = Object2.events_results[i].address[0] + ", " +
                                    Object2.events_results[i].address[1];

                event_node.timezone = "America/" + Object2.events_results[i].address[1];

                all_events.Add(event_node);
            }

            return all_events;
         }
     }

    public class all_events {
        public String name { get; set; }
        public String url { get; set; }
        public String dates { get; set; }
        public String description { get; set; } 
        public String address { get; set; }
        public String timezone { get; set; }

    }
     public class events
     {
        public String name { get; set; }
        public String url { get; set; }
        public dates dates { get; set; }
        public _embedded _embedded { get; set; }

    }

    public class dates { 
        public start start { get; set; }
    }

    public class start {
        public String dateTime { get; set; }

    }
     public class RootObjects
     {
        public List<google_events> events_results { get; set; } = new List<google_events>();
         public _embedded _embedded { get; set; }
    }

    public class _embedded {
        public List<events> events { get; set; } = new List<events>();
        public List<venues> venues { get; set; } = new List<venues>();

    }
    public class venues {
        public addresses address { get; set; }
        public city city { get; set; }
        public state state { get; set; }
        public country country { get; set; }
        public String postalCode { get; set; }
        public string timezone { get; set; }

    }

    public class addresses {
        public String line1 { get; set; }
    }

    public class city
    {
        public String name { get; set; }

    }

    public class state
    {
        public String stateCode { get; set; }
        public String name { get; set; }

    }

    public class country
    {
        public String countryCode { get; set; }
        public String name { get; set; }

    }

    public class location { 
        int longitude { get; set; }
        int latitude { get; set; }
    }

    public class google_events { 
        public string title { get; set; }
        public google_date  date   { get; set; }    
        public List<string> address { get; set; } = new List<string>();
        public String link { get; set; }    
        public String description { get; set; } 

    }

    public class google_date { 
        
        public String start_date { get; set; }  
        public String when { get; set; }    
    }

}