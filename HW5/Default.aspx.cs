using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Caching;
using System.IO;

namespace HW5
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie myCookies = Request.Cookies["newsSearch"];
            if (myCookies == null || (myCookies["topic1"] == "" && myCookies["topic2"] == ""))
            {
                Label1.Text = "No Cookies yet";

            }else if (myCookies != null)
            {
                Label1.Text = "Current Cookies: " + myCookies["topic1"] +", "+ myCookies["topic2"];

            }

            HttpCookie myCookies2 = Request.Cookies["restaurantSearch"];
            if (myCookies2 == null || (myCookies2["location"] == "" && myCookies2["cat"] == ""))
            {
                Label2.Text = "No Cookies yet";

            }
            else if (myCookies2 != null)
            {
                Label2.Text = "Current Cookies: " + myCookies2["location"] + ", " + myCookies2["cat"];
            }

            HttpCookie myCookies3 = Request.Cookies["eventSearch"];
            if (myCookies3 == null || (myCookies3["zip"] == "" && myCookies3["loc"] == ""))
            {
                Label3.Text = "No Cookies yet";

            }
            else if (myCookies3 != null)
            {
                Label3.Text = "Current Cookies: " + myCookies3["loc"] + ", " + myCookies3["zip"];

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            HttpCookie myCookies = Request.Cookies["newsSearch"];
            if (myCookies == null)
            {
                myCookies = new HttpCookie("newsSearch");
                myCookies.Expires = DateTime.Now.AddSeconds(60);

                myCookies["topic1"] = TextBox1.Text;
                myCookies["topic2"] = TextBox2.Text;

                Response.Cookies.Add(myCookies);
                Response.Redirect("newsSearch.aspx");
            }
            else if (String.IsNullOrEmpty(TextBox1.Text) == true && String.IsNullOrEmpty(TextBox2.Text) == true) {
                Response.Redirect("newsSearch.aspx");
            }
            else{
                // if the cookies already have been created then update whatever in the textbox
                myCookies["topic1"] = TextBox1.Text;
                myCookies["topic2"] = TextBox2.Text;
                myCookies.Expires = DateTime.Now.AddSeconds(60);
                Response.Cookies.Add(myCookies);
                Response.Redirect("newsSearch.aspx");
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {

            HttpCookie myCookies = Request.Cookies["restaurantSearch"];
            if (myCookies == null)
            {
                myCookies = new HttpCookie("restaurantSearch");
                myCookies.Expires = DateTime.Now.AddSeconds(60);

                myCookies["location"] = TextBox3.Text;
                myCookies["cat"] = TextBox4.Text;

                Response.Cookies.Add(myCookies);
                Response.Redirect("restaurantSearch.aspx");

            } else if (String.IsNullOrEmpty(TextBox3.Text) == true && String.IsNullOrEmpty(TextBox4.Text) == true)
            {
                Response.Redirect("restaurantSearch.aspx");
            }else
            {
                // if the cookies already have been created then update whatever in the textbox
                myCookies["location"] = TextBox3.Text;
                myCookies["cat"] = TextBox4.Text;
                myCookies.Expires = DateTime.Now.AddSeconds(60);
                Response.Cookies.Add(myCookies);
                Response.Redirect("restaurantSearch.aspx");
            }

        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            if (Cache["cachedData"] == null)
            {
                string d = File.ReadAllText(Server.MapPath("eventsFile.txt"));
                TextArea1.InnerText = d;
                //Cache.Insert("cachedData", d, new CacheDependency(Server.MapPath("cacheData.txt")), 
                //DateTime.Now.AddSeconds(200),Cache.NoSlidingExpiration);

                Cache.Insert("cachedData", d, null, DateTime.Now.AddSeconds(10), Cache.NoSlidingExpiration);

            }
            else
            {
                TextArea1.InnerText = Cache["cachedData"].ToString();

            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {

            HttpCookie myCookies = Request.Cookies["eventSearch"];
            if (myCookies == null)
            {
                myCookies = new HttpCookie("eventSearch");
                myCookies.Expires = DateTime.Now.AddSeconds(60);

                myCookies["loc"] = TextBox5.Text;
                myCookies["zip"] = TextBox6.Text;

                Response.Cookies.Add(myCookies);
                Response.Redirect("eventSearch.aspx");

            }
            else if (String.IsNullOrEmpty(TextBox5.Text) == true && String.IsNullOrEmpty(TextBox6.Text) == true)
            {
                Response.Redirect("eventSearch.aspx");
            }
            else
            {
                // if the cookies already have been created then update whatever in the textbox
                myCookies["loc"] = TextBox5.Text;
                myCookies["zip"] = TextBox6.Text;
                myCookies.Expires = DateTime.Now.AddSeconds(60);
                Response.Cookies.Add(myCookies);
                Response.Redirect("eventSearch.aspx");
            }

        }
    }
}