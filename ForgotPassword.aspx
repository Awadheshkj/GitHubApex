<%@ Page Language="VB" AutoEventWireup="false" CodeFile="forgotpassword.aspx.vb" Inherits="forgotpassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
      <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>apex</title>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/Kestone.css" rel="stylesheet" />
    <script src="js/jquery.js"></script>
    <style>
        .container-login
        {
            margin-top: 20px;
        }

        .form-signin
        {
            max-width: 320px;
            padding: 19px 29px 26px;
            margin: 0 auto 20px;
            background-color: #fff;
            border: 1px solid #e5e5e5;
            -webkit-border-radius: 5px;
            -moz-border-radius: 5px;
            border-radius: 5px;
            -webkit-box-shadow: 0 1px 2px rgba(0,0,0,.05);
            -moz-box-shadow: 0 1px 2px rgba(0,0,0,.05);
            box-shadow: 0 1px 2px rgba(0,0,0,.05);
        }

            .form-signin .form-signin-heading, .form-signin .checkbox
            {
                margin-bottom: 10px;
            }

            .form-signin input[type="text"], .form-signin input[type="password"]
            {
                font-size: 16px;
                height: auto;
                margin-bottom: 15px;
                padding: 7px 9px;
            }
    </style>
    <script>
        function validEmail() {
            var regex=/^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,6}$/i;

                return regex.test($('#txtUsername').val());

            }
        

        $(document).ready(function () {
            $('#btnLogin').click(function () {
                if (validEmail() == false) {
                    $('#lblMsg').html("Invalid Email Address");
                    return false;
                }
                else {
                    return true;
                }
            });
        });
    </script>
</head>
<body>
      <div class="container-login row-fluid">
        <div class="col-1"></div>
        <div class="col-6">
            <h1>Kestone Apex</h1>
            <div>Apex is an internal tool for the use of employees and clients of Kestone only. In case of any usage problem, please contact the administrator of Kestone.</div>
        </div>
        <div class="col-4 pull-right">
            <form id="form1" runat="server" class="form-signin col-12" >
                <div style="width: 100%; text-align: center;">
                   <a href="Default.aspx"><img src="Images/KestoneLoginIcon.jpg" /></a> 
                </div>
                <br />
                <div class="text-center alert">
                    <small><asp:Label ID="lblMsg" runat="server" Text="Please enter Email Address"></asp:Label></small> 
                </div>
                <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Email Address"></asp:TextBox>

                    <div class="col-4 pull-right">
                        <asp:Button runat="server" ID="btnLogin" CssClass="btn btn-default" Text="Submit" />
                    </div>
            </form>
        </div>
    </div>
</body>
</html>
