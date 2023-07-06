<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="restaurantSearch.aspx.cs" Inherits="HW5.restaurantSearch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">


        #TextArea1 {
            height: 332px;
            width: 811px;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
             <h1 style="background-color:powderblue;font-size:25px;">Search Restaurant by location, radius, categories and budget and output the restaurant that match with top 3 of its reviews.</h1>
             <p style="color:red"><b>(If nothing enter then the Default value is Tempe, 20000,food,15)</b></p><b><asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></b>
            <br />
            <p>
                Location:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                &nbsp;</p>
            <p>
                (Optional | Default: Tempe | Ex. Tempe, NYC, Phoinex etc. or any place or city name.)</p>
            <p>
                Radius :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                &nbsp;</p>
            <p>
                (Optional | Default: 20000 | radius is in meter and must be less than 40000 meter, which is around 25 miles.)</p>
            <p>
                Categories:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
            </p>
            <p>
                (Optional | Would allow users enter 1 to 3 categories. The APIs itself could take more | Ex: pizza, breakfast, burger, chinese, italian, curry etc.)</p>
            <p>
                Budget:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                &nbsp;</p>
            <p>
                (Optional | Default: 15 | Enter interger, Ex: less and equal to 20 = $, 20&nbsp; &lt; x &lt;= 80 = $$, 80&nbsp; &lt; x &lt;= 150 = $$$, x &gt;150&nbsp; equal to $$$$)</p>
            <p>
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Search" />
            </p>
            <p>
                <textarea id="TextArea1" runat="server" name="S1"></textarea></p>
             <p>
                 <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Back" />
             </p>
        </div>
    </form>
</body>
</html>
