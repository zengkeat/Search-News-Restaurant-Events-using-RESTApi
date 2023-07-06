<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="eventSearch.aspx.cs" Inherits="HW5.eventSearch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">

        #TextArea2 {
            height: 316px;
            width: 813px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1 style="background-color:powderblue;">
                Second service: search nearby event based on zipcode or place
            </h1>
            <p style="color:red;"><b>
                - This service have two parameter which is zipcode and Place, it will output upcoming event nearby.</b></p>
            <p style="color:red;"><b>
                - You can search two place, one by zipcode, another by place name.</b></p>
            <p>
                <b><asp:Label ID="Label1" runat="server"></asp:Label></b>
            </p>
            <p>
                Zip code:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="TextBox9" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp; (Optional | Default is 85281| You can enter other state zipcode)</p>
            <p>
                Place:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp; (Optional | Default is Tempe | Ex. Events in NYC, OR just a place name like Tempe)</p>
            <p>
                <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Search" />
            </p>
            <p>
                <textarea id="TextArea2" runat="server" name="S2"></textarea></p>
        </div>
        <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Back" />
    </form>
</body>
</html>
