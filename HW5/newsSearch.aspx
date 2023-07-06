<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="newsSearch.aspx.cs" Inherits="HW5.news" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">

        #TextArea1 {
            height: 296px;
            width: 938px;
        }
        </style>
</head>
<body>
    <form id="form2" runat="server">
        <div>
           <h1 style="background-color:powderblue;"> This web service take a list of topics and output related news that match</h1>
            <p style="color:red;"><b>Note: you can enter 1 to 5 topics which should be a word(string), each topics will return 10 result</b></p>
            <b><asp:Label ID="Label1" runat="server"></asp:Label></b>
        </div>
        <p>
            Topic 1:
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        </p>
        <p>
            Topic 2:
            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        </p>
        <p>
            Topic 3:
            <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
        </p>
        <p>
            Topic 4:
            <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
        </p>
        <p>
            Topic 5:
            <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
        </p>
        <p dir="auto">
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Search" />
        </p>
        <p dir="auto">
            <textarea id="TextArea1" runat="server" name="S1"></textarea></p>
        <p>
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Back" />
        </p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
    </form>
</body>
</html>
