<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WebAppSec.Index" %>
<%@ Register Assembly="BotDetect" Namespace="BotDetect.Web.UI" TagPrefix="BotDetect" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Index</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.0/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"></script>
    <style type="text/css">
        .auto-style1 {
            right: 973px;
        }
    </style>
</head>
<body>
    <nav class="navbar navbar-inverse">
        <div class="container-fluid">
            <div class="navbar-header">
                <a class="navbar-brand" href="#">WebAppSec</a>
            </div>
            <ul class="nav navbar-nav">
                <li class="active"><a href="#">Home</a></li>
                <li><a href="#">Page 1</a></li> 
            </ul>
            <ul class="nav navbar-nav navbar-right">
                <li><a href="Registration.aspx"><span class="glyphicon glyphicon-user"></span> Sign Up</a></li>
                <li><a href="#"><span class="glyphicon glyphicon-log-in"></span> Login</a></li>
            </ul>
        </div>
    </nav>

    <div class="container">
    <form class="form-horizontal" role="form" id="form1" runat="server"> 
        <div class="form-group">
            <div class="col-sm-offset-2 col-sm-10">
                <asp:Label ID="LabelName" runat="server" Text="Username"></asp:Label>
            </div>
            <div class="col-sm-offset-2 col-sm-10">
                <asp:TextBox ID="TextBoxUserName" runat="server" ToolTip="Enter username" ValidateRequestMode="Enabled"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-offset-2 col-sm-10">
                <asp:Label ID="LabelPassword" runat="server" Text="Password"></asp:Label>
            </div>
            <div class="col-sm-offset-2 col-sm-10">
                <asp:TextBox ID="TextBoxPassword" runat="server" TextMode="Password" ToolTip="Enter password"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">        
            <div class="col-sm-offset-2 col-sm-10">
                <asp:CheckBox ID="CheckBoxRememberMe" runat="server" Text=" Remember me" ToolTip="Remember me checkbox" />
            </div>
        </div>
        <div class="form-group">        
            <div class="col-sm-offset-2 col-sm-10">
                <BotDetect:WebFormsCaptcha ID="ExampleCaptcha" runat="server" />
            </div>
        </div>
        <div class="form-group">        
            <div class="col-sm-offset-2 col-sm-10">
                <asp:Label ID="CaptchaLabel" runat="server" AssociatedControlID="CaptchaCodeTextBox" Text="Retype the characters from the picture:"></asp:Label>

                <br />

                <asp:TextBox ID="CaptchaCodeTextBox" runat="server" ToolTip="Enter Captcha" />
                <asp:Label ID="CaptchaErrorLabel" runat="server"/>
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-offset-2 col-sm-10">
                <asp:Button class="btn btn-default" ID="ButtonSubmit" runat="server" Text="Log in" OnClick="submitEventMethod"/>
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-offset-2 col-sm-10">
                <asp:Label ID="LabelMessage" runat="server"></asp:Label>
            </div>
        </div>
     </form>
     </div>
</body>
</html>
