<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="js/jquery-1.11.3.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:TextBox runat="server" ID="txtusername" ></asp:TextBox><br />
        <asp:TextBox runat="server" ID="txtpassword" ></asp:TextBox><br />
        <div id="GetAccess" style="border:1px solid #000;background:#CCCCCC;width:100px;text-align:center;padding:4px;margin:10px;" onclick="GetAccess()">Get Access</div>
        <div id="login"></div>
    </div>
        <div>
            <br />
            <br />
            <br />
        </div>
        <div>
            <div onclick="GetProjectList()" style="border:1px solid #000;background:#CCCCCC;width:150px;text-align:center;padding:4px;margin:10px;">Get Project List</div>
            <div id="projects"></div>
        </div>
    </form>
    <script type="text/javascript">
        function GetAccess() {
            $('#login').html("Access Requested. Please Wait.");
            var uri = "login.aspx?"
            var _u = $('#txtusername').val();
            var _p = $('#txtpassword').val();

            uri += "U=" + _u + "&P=" + _p;
            alert(uri);
            $.ajax({
                url: uri,
                context: document.body,
                success: function (Result) {
                    if (!Result == "") {
                        $('#login').html(Result);
                    }
                }
            });
        }

        function GetProjectList()
        {
            $('#projects').html("Access Requested. Please Wait.");
            $.ajax({
                url: "projects.aspx",
                crossOrigin: true,
               context: document.body,
                success: function (Result) {
                    if (!Result == "") {
                        $('#projects').html(Result);
                    }
                }
            });
        }
    </script>
</body>
</html>
