<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <title>APEX | Kestone</title>
    <meta content='width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no' name='viewport'>
    <!-- Bootstrap 3.3.4 -->
    <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <!-- Font Awesome Icons -->
    <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <!-- Theme style -->
    <link href="dist/css/AdminLTE.min.css" rel="stylesheet" type="text/css" />
    <!-- iCheck -->
    <link href="plugins/iCheck/square/blue.css" rel="stylesheet" type="text/css" />
    <link href="dist/css/apex.css" rel="stylesheet" type="text/css" />
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
        <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->

</head>
<body class="login-page">

    <div class="login-box">
        <div class="login-logo">
            <img src="dist/img/apexlogoblack.png" />
        </div>
        <!-- /.login-logo -->
        <div class="login-box-body">
            <p class="login-box-msg">Sign in to start your session</p>
            <p class="login-box-msg1"><a class="badge" href="http://kestoneapps.com/Apex_15_16/">Click here</a> to view FY 2015-16 of Kestone Apex.</p>
            <form id="form1" runat="server">
                <div class="form-group has-feedback">
                    <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Username"></asp:TextBox>
                    <span class="glyphicon glyphicon-envelope form-control-feedback"></span>
                </div>
                <div class="form-group has-feedback">
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Password"
                        data-toggle="tooltip" data-trigger="manual" data-title="Caps lock is on">
                    </asp:TextBox>
                    <span class="glyphicon glyphicon-lock form-control-feedback"></span>
                </div>
                <div class="row text-center">
                    <label>
                        <asp:Label ID="lblMsg" runat="server" Text="Please enter Username and Password" CssClass="text-danger"></asp:Label>
                    </label>
                </div>
                <div class="row">
                    <div class="col-xs-8">
                    </div>
                    <!-- /.col -->
                    <div class="col-xs-4">
                        <asp:Button runat="server" CssClass="btn btn-primary btn-block btn-flat" ID="btnLogin" Text="Log In" />
                    </div>
                    <!-- /.col -->
                </div>
            </form>

            <!-- /.social-auth-links -->

            <a href="#">I forgot my password</a><br>
        </div>
        <!-- /.login-box-body -->
    </div>
    <!-- /.login-box -->
    <!-- jQuery 2.1.4 -->
    <script src="plugins/jQuery/jQuery-2.1.4.min.js"></script>
    <!-- Bootstrap 3.3.2 JS -->
    <script src="bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <!-- iCheck -->
    <script src="plugins/iCheck/icheck.min.js" type="text/javascript"></script>
    <script>
        $(function () {
            $('input').iCheck({
                checkboxClass: 'icheckbox_square-blue',
                radioClass: 'iradio_square-blue',
                increaseArea: '20%' // optional
            });

        });

        $('[type=password]').keypress(function (e) {
            var $password = $(this),
                tooltipVisible = $('.tooltip').is(':visible'),
                s = String.fromCharCode(e.which);

            //Check if capslock is on. No easy way to test for this
            //Tests if letter is upper case and the shift key is NOT pressed.
            if (s.toUpperCase() === s && s.toLowerCase() !== s && !e.shiftKey) {
                if (!tooltipVisible)
                    $password.tooltip('show');
            } else {
                if (tooltipVisible)
                    $password.tooltip('hide');
            }

            //Hide the tooltip when moving away from the password field
            $password.blur(function (e) {
                $password.tooltip('hide');
            });
        });

    </script>
    <script>
        $(document).ready(function () {


            $('.badge').blink(); // default is 500ms blink interval.
        });



        // Source: http://www.antiyes.com/jquery-blink-plugin
        // http://www.antiyes.com/jquery-blink/jquery-blink.js
        (function ($) {
            $.fn.blink = function (options) {
                var defaults = {
                    delay: 500
                };
                var options = $.extend(defaults, options);

                return this.each(function () {
                    var obj = $(this);
                    setInterval(function () {
                        if ($(obj).css("visibility") == "visible") {
                            $(obj).css('visibility', 'hidden');
                        }
                        else {
                            $(obj).css('visibility', 'visible');
                        }
                    }, options.delay);
                });
            }
        }(jQuery))

    </script>
</body>
</html>
