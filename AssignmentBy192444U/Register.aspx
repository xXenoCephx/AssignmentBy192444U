<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="AssignmentBy192444U.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Assignment</title>
    <script type="text/javascript">
        function validatePw() {
            var pass = document.getElementById('<%=TbPass.ClientID%>').value;
            pass.innerHTML = ""
            if (pass.length == 0) {
                return -1;
            }
            else if (pass.length < 8) {
                return 0;
            }
            else if (pass.search(/[a-z]/) == -1) {
                return 1;
            }
            else if (pass.search(/[A-Z]/) == -1) {
                return 1;
            }
            else if (pass.search(/[0-9]/) == -1) {
                return 1;
            }
            else if (pass.search(/[$@#&*!+=-]/) == -1) {
                return 1;
            } else if (pass.length > 16) {
                return 3;
            } 
            
            return 2;
        }
        function gradePw() {
            var result = validatePw()
            if (result == -1) {
                document.getElementById('LbPassChecker').innerHTML = "Your password cannot empty leh.";
                document.getElementById('LbPassChecker').style.color = "Red";
            } else if (result == 1) {
                document.getElementById('LbPassChecker').innerHTML = "Sorry ah, you need big and small letter, number and some of these kind of symbol ('$' '@' '&''!' '+' '-') rojak together";
                document.getElementById('LbPassChecker').style.color = "Red";
            } else if (result == 2) {
                document.getElementById('LbPassChecker').innerHTML = "Not bad eh, will be even better if you make it longer.";
                document.getElementById('LbPassChecker').style.color = "Green";
            } else if (result == 3) {
                document.getElementById('LbPassChecker').innerHTML = "Ups lah! your password not bad sia.";
                document.getElementById('LbPassChecker').style.color = "Darkgreen";
            } else {
                document.getElementById('LbPassChecker').innerHTML = "At least 8 characters lah. Later people hack say easier than eating how?";
                document.getElementById('LbPassChecker').style.color = "Red";
            }
        }
        function checkCard() {
            var cardNum = document.getElementById('<%=TbCardNum.ClientID%>').value;
            var lbErrors = document.getElementById('<%=LbErrors.ClientID%>');
            lbErrors.innerHTML = "";
            if (cardNum.length == 0) {
                lbErrors.innerHTML += "Card number cannot empty.";
            } else if (cardNum.length != 16) {
                lbErrors.innerHTML += "Card number sala leh. Enter again please?";
            }
        }
        function checkSecNum() {
            var secNum = document.getElementById('<%=TbSecNum.ClientID%>').value;
            var lbErrors = document.getElementById('<%=LbErrors.ClientID%>');
            lbErrors.innerHTML = "";
            if (secNum.length == 0) {
                lbErrors.innerHTML += "Security number also cannot empty.";
            } else if (secNum.length != 3) {
                lbErrors.innerHTML += "Security number not right. Tolong, can enter again?";
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server" padding="20dp">
        <div>
            <asp:Label ID="LbFName" runat="server" Text="First Name:"></asp:Label>
            <asp:TextBox ID="TbFName" runat="server"></asp:TextBox>
        </div>
        <div>
            <asp:Label ID="LbLName" runat="server" Text="Last Name:"></asp:Label>
            <asp:TextBox ID="TbLName" runat="server"></asp:TextBox>
        </div>
        <div>
            <asp:Label ID="LbPass" runat="server" Text="Password:"></asp:Label>
            <asp:TextBox ID="TbPass" runat="server" TextMode="Password" onKeyUp="javascript:gradePw()"></asp:TextBox> <asp:Label ID="LbPassChecker" runat="server" Text=""></asp:Label>
        </div>
        <div>
            <asp:Label ID="LbEmail" runat="server" Text="Email:"></asp:Label>
            <asp:TextBox ID="TbEmail" runat="server" TextMode="Email"></asp:TextBox>
        </div>
        <div>
            <asp:Label ID="LbDOB" runat="server" Text="Date of Birth:"></asp:Label>
            <asp:TextBox ID="TbDOB" runat="server" TextMode="Date"></asp:TextBox>
        </div>
        <div>
            <asp:Label ID="LbCardNum" runat="server" Text="Card Number:"></asp:Label>
            <asp:TextBox ID="TbCardNum" runat="server" onKeyUp="javascript:checkCard()" ></asp:TextBox>
        </div>
        <div>
            <asp:Label ID="LbSecNum" runat="server" Text="Security Number:"></asp:Label>
            <asp:TextBox ID="TbSecNum" runat="server" onKeyUp="javascript:checkSecNum()"></asp:TextBox>
        </div>
        <div>
            <asp:Label ID="LbErrors" runat="server" Text="" ForeColor="Red"></asp:Label>
            <br />
            <asp:Button ID="BtnRegister" runat="server" Text="Register" OnClick="BtnRegister_Click" />
        </div>
        
    </form>
</body>
</html>
