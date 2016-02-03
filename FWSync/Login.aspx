<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>

        登录页面
        <br />
        用户名：<asp:TextBox ID="tbxName" runat="server"></asp:TextBox>
        <br />
        密码：<asp:TextBox ID="tbxPassword" runat="server" TextMode="Password"></asp:TextBox>
        <br />
        <asp:Button ID="btnLogin" runat="server" Text="登录" onclick="btnLogin_Click" />
        <br />
        <br />
        还没注册？点击<asp:HyperLink ID="hlkRegister" runat="server" NavigateUrl="~/Register.aspx">此处</asp:HyperLink>
       
    
    </div>
    </form>
</body>
</html>
