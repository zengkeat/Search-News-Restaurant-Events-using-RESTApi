using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace HW5
{
    public partial class eventSearch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie myCookies = Request.Cookies["eventSearch"];
            if ((myCookies == null) || (myCookies["zip"] == "" && myCookies["loc"] == ""))
            {
                Label1.Text = "Welcome, look like you havent search anything yet.";
            }
            else if (String.IsNullOrEmpty(TextBox9.Text) == true && String.IsNullOrEmpty(TextBox8.Text) == true)
            {
                Label1.Text = "Welcome, your search history is inside the text box using cookies.";
                TextBox9.Text = myCookies["zip"];
                TextBox8.Text = myCookies["loc"];
            }

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string zipCode = String.IsNullOrEmpty(TextBox9.Text) ? "85281" : TextBox9.Text;
            string google_event = String.IsNullOrEmpty(TextBox8.Text) ? "Tempe" : TextBox8.Text;

            string url = "https://localhost:44336/api/Event?zipcode=" + zipCode + "&place=" + google_event;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                WebResponse response = request.GetResponse();
                Console.WriteLine(request);
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);
                string restRes = reader.ReadToEnd();
                response.Close();

                List<all_events> newsObject = JsonConvert.DeserializeObject<List<all_events>>(restRes);

                string temp;
                string result = "";

                //loop over the events and print out all them 
                foreach (all_events events in newsObject)
                {
                    temp = "Name: " + events.name + "\r\n" +
                            "Start Date: " + events.dates + "\r\n" +
                            "URL: " + events.url + "\r\n" +
                            "Descriptions: " + events.description + "\r\n" +
                            "location: " + events.address + "\r\n" +
                            "TimeZone: " + events.timezone + "\r\n\n";

                    result += temp;

                    TextArea2.InnerText = result;

                }
            }
            catch (WebException ex)
            {

                using (var stream = ex.Response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    TextArea2.InnerText = "Something is wrong here. You might enter an invalid zip code! Try again!";
                }

            }

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }

    public class all_events
    {
        public String name { get; set; }
        public String url { get; set; }
        public String dates { get; set; }
        public String description { get; set; }
        public String address { get; set; }
        public String timezone { get; set; }

    }
}