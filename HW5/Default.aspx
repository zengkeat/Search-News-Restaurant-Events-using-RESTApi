<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HW5.Default" %>
<%@ OutputCache Duration="20" VaryByParam="*" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #TextArea1 {
            height: 256px;
            width: 856px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
           <h1 style="background-color:powderblue;"> Welcome to my web application</h1>
            <p style="color:red;font-size:15px;"><b>
            You can search news articles, restaurant and events in here!<br />
            All cookies expired in 60 seconds( I used cookies to prefill the text box when redirect to another page)</b></p>
            <br />
            <b>
            What news you are interested?
                </b>
            <br />
            Topic1:&nbsp;&nbsp;
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
            Topic2:&nbsp;&nbsp; <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
            &nbsp;(Ex. murder, facebook, ukraine)<br />
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Search News" />
            <br />
            <asp:Label ID="Label1" runat="server"></asp:Label>
            <br />
            <br />
            <b>What to eat?</b><br />
            Location :&nbsp;&nbsp;
            <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
&nbsp;&nbsp;(Ex.Tempe, New york, Las veags)<br />
            Categories: <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
            &nbsp; (Ex. chinese, italian, indian, japanese, pizza, breakfast)<br />
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Search Restaurant" />
            <br />
            <asp:Label ID="Label2" runat="server"></asp:Label>
            <br />
            <br />
           <b> Looking for events?</b>
            <br />
            (You can search by location and zipcode, both text box doesnt related to each other.
            <br />
            Ex. location could be Las vegas, zipcode could be 85281. Then it will show events in both Las Vegas and 85281)<br />
            Location:&nbsp;&nbsp;&nbsp; <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
&nbsp;&nbsp;(Ex. New York, Tempe, Las Vegas, Mahathan)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
            <br />
            ZipCode:&nbsp;&nbsp;&nbsp; <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
            &nbsp; (Ex. 85281, 10001, 10002)<br />
            <asp:Button ID="Button3" runat="server" Text="Search Events" OnClick="Button3_Click" />
            <br />
            <asp:Label ID="Label3" runat="server"></asp:Label>
            <br />
            <br />
            <b>Click the button to show events in Tempe using data store in Cache<br />
            <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="Show Events" />
            </b>
        </div>
    </form>
    <p>
        <textarea id="TextArea1" name="S1" runat="server"></textarea></p>
</body>
</html>
