<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Apex_Down.aspx.vb" Inherits="Apex_Down" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <title>Apex | Kestone</title>
    <meta content='width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no' name='viewport'>
    <!-- Bootstrap 3.3.4 -->
    <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <!-- Font Awesome Icons -->
    <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <!-- Ionicons -->
    <link href="https://code.ionicframework.com/ionicons/2.0.1/css/ionicons.min.css" rel="stylesheet" type="text/css" />
    <!-- jvectormap -->
    <link href="plugins/jvectormap/jquery-jvectormap-1.2.2.css" rel="stylesheet" type="text/css" />
    <!-- Theme style -->
    <link href="dist/css/AdminLTE.min.css" rel="stylesheet" type="text/css" />
    <!-- AdminLTE Skins. Choose a skin from the css/skins
         folder instead of downloading all of them to reduce the load. -->
    <link href="dist/css/skins/_all-skins.min.css" rel="stylesheet" type="text/css" />

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
        <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->

    <style>
        .titel {
            font-size: 14px;
            font-weight: bold;
            color: #808080;
        }

        .containt {
            font-size: 12px;
            min-height: 50px;
            color: #808080;
        }

        .Footerdate {
            width: 85%;
            float: left;
            font-size: 12px;
            color: #808080;
        }

        .Footerviewlink {
            width: 15%;
            float: left;
            font-size: 13px;
        }

        hr {
            margin: 2px;
        }

        .alert-default {
            background: #F4F4F5;
            margin: 5px;
        }
    </style>




</head>
<body class="layout-top-nav skin-blue-light">
    <form id="form1" runat="server">
        <div>
            <div class="wrapper">

                <header class="main-header">
                    <nav class="navbar navbar-static-top">

                        <div class="navbar-header">

                            <!-- Logo -->
                            <a href="Dashboard.aspx" class="logo">
                                <!-- logo for regular state and mobile devices -->
                                <span class="logo-lg">
                                    <img src="dist/img/apexlogo1.png" class="img-responsive" /></span>
                            </a>
                            <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar-collapse">
                                <i class="fa fa-bars"></i>
                            </button>
                        </div>
                        <!-- Collect the nav links, forms, and other content for toggling -->
                        <div class="collapse navbar-collapse pull-left" id="navbar-collapse">
                        </div>

                        <!-- Navbar Right Menu -->
                        <div class="navbar-custom-menu">
                        </div>

                    </nav>
                </header>
            </div>
            <div class="content-wrapper" style="min-height: 439px;  width: 100%;">
                <div class="row" id="Leftmenu" runat="server">
                    <div class="col-lg-12">
                        <div class="col-lg-12 " style="text-align:center;">
                            <h2>Apex is down from 26th - 30th Sep 2016 due to half yearly audit.
                                <br />
                                So plan accordingly.
                            </h2>
                        </div>
                    </div>
                </div>
            </div>
    </form>
</body>
</html>
