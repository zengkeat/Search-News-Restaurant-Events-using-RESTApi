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
    public partial class news : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie myCookies = Request.Cookies["newsSearch"];
            if ((myCookies == null) || (myCookies["topic1"] == "" && myCookies["topic2"] == ""))
            {
                Label1.Text = "Welcome, look like you havent search anything yet.";
            }
            else if(String.IsNullOrEmpty(TextBox1.Text) == true && String.IsNullOrEmpty(TextBox2.Text) == true)
            {
                Label1.Text = "Welcome, your search history is inside the text box using cookies.";
                TextBox1.Text =  myCookies["topic1"];
                TextBox2.Text =  myCookies["topic2"];
            }

           
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                string topic1 = String.IsNullOrEmpty(TextBox1.Text) ? "-" : TextBox1.Text;
                string topic2 = String.IsNullOrEmpty(TextBox2.Text) ? "-" : TextBox2.Text;
                string topic3 = String.IsNullOrEmpty(TextBox3.Text) ? "-" : TextBox3.Text;
                string topic4 = String.IsNullOrEmpty(TextBox4.Text) ? "-" : TextBox4.Text;
                string topic5 = String.IsNullOrEmpty(TextBox5.Text) ? "-" : TextBox5.Text;

                string url = "https://localhost:44336/api/News?" +
                    "topics[0]={" + topic1 + "}&" +
                    "topics[1]={" + topic2 + "}&" +
                    "topics[2]={" + topic3 + "}&" +
                    "topics[3]={" + topic4 + "}&" +
                    "topics[4]={" + topic5 + "}&";


                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                WebResponse response = request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);
                string restRes = reader.ReadToEnd();
                response.Close();

                var newsObject = JsonConvert.DeserializeObject<List<article>>(restRes);

                string temp;
                string result = "";


                //loop over the newsObject and print out all the news 
                for (int i = 0; i < newsObject.Count; i++)
                {
                    temp = "Title: " + newsObject[i].title + "\r\n" +
                                    "author: " + newsObject[i].author + "\r\n" +
                                    "URL: " + newsObject[i].url + "\r\n\n";

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

    public class article
    {
        //the method name must be same with the key value in the json struture 
        public String title { get; set; }
        public String author { get; set; }
        public String url { get; set; }

    }

    public class RootObject
    {
        // since article is an array, so we make an array of the same name as the key 
        public List<article> articles { get; set; } = new List<article>();
    }
}