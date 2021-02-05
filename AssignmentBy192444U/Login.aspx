<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AssignmentBy192444U.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="LbError" runat="server" Text=""></asp:Label>
        </div>
        <div>
            <asp:Label ID="LbUserName" runat="server" Text="Email"></asp:Label>
            <asp:TextBox ID="TbUserName" runat="server"></asp:TextBox>
        </div>
        <div>
            <asp:Label ID="LbLoginPass" runat="server"  Text="Password" ></asp:Label>
            <asp:TextBox ID="TbLoginPass" runat="server" TextMode="Password"></asp:TextBox>
        </div>
        <div>
            <asp:Button ID="BtnLogin" runat="server" Text="Button" OnClick="BtnLogin_Click" />
        </div>
    </form>
</body>
</html>
