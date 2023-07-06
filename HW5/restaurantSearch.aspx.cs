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
    public partial class restaurantSearch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie myCookies = Request.Cookies["restaurantSearch"];
            if ((myCookies == null) || (myCookies["location"] == "" && myCookies["cat"] == ""))
            {
                Label1.Text = "Welcome, look like you havent enter anything yet.";

            }
            else if (String.IsNullOrEmpty(TextBox1.Text) == true && String.IsNullOrEmpty(TextBox3.Text) == true)
            {
                Label1.Text = "Welcome, your search history is inside the text box using cookies";
                TextBox1.Text = myCookies["location"];
                TextBox3.Text = myCookies["cat"];
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            List<string> food = new List<string>();

            string location = String.IsNullOrEmpty(TextBox1.Text) ? "tempe" : TextBox1.Text;
            Int32 radius = (String.IsNullOrEmpty(TextBox2.Text) || Int32.Parse(TextBox2.Text) >= 40000) ? 20000 : Int32.Parse(TextBox2.Text);

            if (!String.IsNullOrEmpty(TextBox3.Text)) food.Add(TextBox3.Text);
            if (!String.IsNullOrEmpty(TextBox5.Text)) food.Add(TextBox5.Text);
            if (!String.IsNullOrEmpty(TextBox6.Text)) food.Add(TextBox6.Text);

            Int32 budget = String.IsNullOrEmpty(TextBox4.Text) ? 15 : Int32.Parse(TextBox4.Text);

            //remove the duplicate categories
            var foods = food.Distinct().ToList();
            string url;

            if (foods.Count == 1)
            {

                url = "https://localhost:44336/api/Restaurant?" +
               "location=" + location + "&" +
               "radius=" + radius + "&" +
               "categories[0]=" + foods[0] + "&" +
               "price=" + budget;

            }
            else if (foods.Count == 2)
            {
                url = "https://localhost:44336/api/Restaurant?" +
              "location=" + location + "&" +
              "radius=" + radius + "&" +
              "categories[0]=" + foods[0] + "&" +
              "categories[1]=" + foods[1] + "&" +
              "price=" + budget;

            }
            else if (foods.Count == 3)
            {

                url = "https://localhost:44336/api/Restaurant?" +
                 "location=" + location + "&" +
                 "radius=" + radius + "&" +
                 "categories[0]=" + foods[0] + "&" +
                 "categories[1]=" + foods[1] + "&" +
                  "categories[2]=" + foods[2] + "&" +
                 "price=" + budget;

            }
            else
            {

                url = "https://localhost:44336/api/Restaurant?" +
                    "location=" + location + "&" +
                    "radius=" + radius + "&" +
                    "categories[0]=food&" +
                    "price=" + budget;

            }

            string temp;
            string result = "";

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                WebResponse response = request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);
                string restRes = reader.ReadToEnd();
                response.Close();

                List<restaurant> newsObject = JsonConvert.DeserializeObject<List<restaurant>>(restRes);

                if (newsObject.Count == 0) TextArea1.InnerText = "There are no match!";

                //loop over the restaurant and print out all them 
                foreach (restaurant rest in newsObject)
                {
                    string review = "";
                    string tempReview;

                    //get the review for each restaurant
                    for (int i = 0; i < rest.reviews.Count; i++)
                    {

                        int num = i + 1;
                        tempReview = "Review" + num + ":" + " \r\n" +
                            "-Name: " + rest.reviews[i].user.name + "\r\n" +
                            "-Rating: " + rest.reviews[i].rating + "\r\n" +
                            "-Text: " + rest.reviews[i].text + "\r\n" +
                            "-Time_Created: " + rest.reviews[i].time_created + "\r\n\n";

                        review += tempReview;

                    }

                    //combined all the information together
                    temp = "Name: " + rest.name + "\r\n" +
                            "Rating: " + rest.rating + "\r\n" +
                            "Price: " + rest.price + "\r\n" +
                            "Location: " + rest.location.address1 + ", " + rest.location.city + ", " +
                            rest.location.state + ", " + rest.location.zip_code + "\r\n" +
                            "Categories: " + rest.categories[0].alias + "\r\n\n" +
                            review + "------------------------------------" + "\r\n\n";


                    result += temp;

                }

                TextArea1.InnerText = result;
            }
            catch (WebException ex)
            {
                
                using (var stream = ex.Response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                  TextArea1.InnerText = "500 remote Internal server Error! Nothing I can do about it! Refresh the page or click the button again!";
                 }

            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }

    public class restaurant
    {
        //the method name must be same with the key value in the json struture 
        public decimal rating { get; set; }
        public String price { get; set; }
        public String name { get; set; }
        public address location { get; set; }
        public List<review> reviews { get; set; } = new List<review>();
        public List<categories> categories { get; set; } = new List<categories>();

    }

    public class categories
    {
        public String alias { get; set; }
        public String title { get; set; }

    }

    public class address
    {

        public String city { get; set; }
        public String country { get; set; }
        public String address2 { get; set; }
        public String address3 { get; set; }
        public String state { get; set; }
        public String address1 { get; set; }
        public String zip_code { get; set; }

    }

    public class review
    {

        public user user { get; set; }
        public int rating { get; set; }
        public String text { get; set; }
        public String time_created { get; set; }

    }

    public class user
    {
        public String name { get; set; }
    }
}