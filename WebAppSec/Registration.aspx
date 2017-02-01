<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="WebAppSec.Registration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registration</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.0/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"></script>
</head>
<body>
    <nav class="navbar navbar-inverse">
        <div class="container-fluid">
            <div class="navbar-header">
                <a class="navbar-brand" href="#">WebAppSec</a>
            </div>
            <ul class="nav navbar-nav">
                <li class="active"><a href="Index.aspx">Home</a></li>
                <li><a href="#">Page 1</a></li> 
            </ul>
            <ul class="nav navbar-nav navbar-right">
                <li><a href="#"><span class="glyphicon glyphicon-user"></span> Sign Up</a></li>
                <li><a href="Index.aspx"><span class="glyphicon glyphicon-log-in"></span> Login</a></li>
            </ul>
        </div>
    </nav>

    <div class="container">  
    <form class="form-horizontal" role="form" id="form1" runat="server">
        <div class="form-group">
            <div class="col-sm-offset-2 col-sm-10">
                <asp:Label ID="LabelFirstName" runat="server" Text="First name"></asp:Label>
            </div>
            <div class="col-sm-offset-2 col-sm-10">
                <asp:TextBox ID="TextBoxFirstName" runat="server" Width="200px"></asp:TextBox>
            &nbsp;<asp:Label ID="LabelErrFirstName" runat="server" ForeColor="Red"></asp:Label>
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-offset-2 col-sm-10">
                <asp:Label ID="LabelLastName" runat="server" Text="Last name"></asp:Label>
            </div>
            <div class="col-sm-offset-2 col-sm-10">
                <asp:TextBox ID="TextBoxLastName" runat="server" Width="200px"></asp:TextBox>
            &nbsp;<asp:Label ID="LabelErrLastName" runat="server" ForeColor="Red"></asp:Label>
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-offset-2 col-sm-10">
                <asp:Label ID="LabelEmail" runat="server" Text="Email"></asp:Label>
            </div>
            <div class="col-sm-offset-2 col-sm-10">
                <asp:TextBox ID="TextBoxEmail" runat="server" TextMode="Email" Width="200px"></asp:TextBox>
                &nbsp;<asp:Label ID="LabelErrEmail" runat="server" ForeColor="Red"></asp:Label>
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-offset-2 col-sm-10">        
                <asp:Label ID="LabelUserName" runat="server" Text="Username"></asp:Label>
            </div>
            <div class="col-sm-offset-2 col-sm-10"> 
                <asp:TextBox ID="TextBoxUserName" runat="server" Width="200px"></asp:TextBox>
                &nbsp;<asp:Label ID="LabelErrUserName" runat="server" ForeColor="Red"></asp:Label>
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-offset-2 col-sm-10">        
                <asp:Label ID="LabelPassword" runat="server" Text="Password"></asp:Label>
            </div>
            <div class="col-sm-offset-2 col-sm-10">        
                <asp:TextBox ID="TextBoxPassword" runat="server" TextMode="Password" Width="200px"></asp:TextBox>
                &nbsp;<asp:RegularExpressionValidator ID="RegExValidatorPassword" runat="server" ControlToValidate="TextBoxPassword" EnableClientScript="False" EnableTheming="True" ErrorMessage="Password must contain: Minimum 11 characters at least 1 UpperCase letter, 1 LowerCase letter, 1 Number and 1 Special Character" ForeColor="Red" ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&amp;])[A-Za-z\d$@$!%*?&amp;]{11,20}" Display="Dynamic"></asp:RegularExpressionValidator>
                &nbsp;<asp:Label ID="LabelErrPassword" runat="server" ForeColor="Red"></asp:Label>
            </div>
        </div>    
        <div class="form-group">
            <div class="col-sm-offset-2 col-sm-10">        
                <asp:Label ID="LabelConfirmPassword" runat="server" Text="Confirm Password"></asp:Label>
            </div>
            <div class="col-sm-offset-2 col-sm-10">        
                <asp:TextBox ID="TextBoxConfirmPassword" runat="server" TextMode="Password" Width="200px"></asp:TextBox>
                
            &nbsp;<asp:Label ID="LabelErrComparePassword" runat="server" ForeColor="Red"></asp:Label>
                
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-offset-2 col-sm-10">
                <asp:Button class="btn btn-default" ID="ButtonRegister" runat="server" Text="Register" OnClick="registerEventMethod"/>
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
