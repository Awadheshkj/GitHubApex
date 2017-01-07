<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MobileAppPosteventLinks.aspx.vb" Inherits="MobileAppFeedback" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Feedback</title>
    <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
         <div class="alert alert-danger" id="divError" runat="server">
                <strong>Warning: </strong>
                <asp:Label ID="lblError" runat="server"></asp:Label>


            </div>

            <div class="alert alert-success" id="MessageDiv" runat="server">
                <strong>Message: </strong>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>

            </div>
        <div class="container" id="content" runat="server" >
           
            <div class="col-md-12">
                <div class="row">
                    Download Report                    
                </div>
                <div class="row">
                    <div class="col-md-2">
                       <a href="#">Link</a>
                    </div>

                </div>
            </div>
        

            
            <div class="col-md-12">
                <div class="row">
                    &nbsp;<br />
                    <br />
                    <asp:HiddenField ID="hdnloginID" runat="server" />
                    <asp:HiddenField ID="hdnjobcardID" runat="server" />
                </div>
                <div class="row">
                    <div class="col-md-2">
                    </div>

                </div>
            </div>
        </div>
    </form>
</body>
</html>
