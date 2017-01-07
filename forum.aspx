<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/Apex.master" CodeFile="forum.aspx.vb" Inherits="forum" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container" id="BodyContainer">

        <div class="container" id="ContentContainer">
            <div class="container">
                <div class="col-lg-12">
                    <h1>Issues/Suggestions Tracker</h1>
                    <br />
                </div>
               
                    <div class="col-lg-12">
                        <asp:GridView ID="gvforum" runat="server" AutoGenerateColumns="false" ShowHeader="false" class="col-lg-12" GridLines="None">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <div class="panel panel-default">
                                            <div class="panel-heading">
                                                <%# Eval("Topic") %><div class="pull-right"><i class="fa fa-users"></i>&nbsp;<%# Eval("TeamName")%></div>
                                            </div>
                                            <div class="panel-body">
                                                <div class="forum-body">
                                                    <div class="post">
                                                        <%# Eval("Description")%>
                                                    </div>

                                                </div>
                                            </div>
                                            <div class="panel-footer clearfix">
                                                <div class="pull-right">
                                                    <%--<a href='forumReply.aspx?fid=<%# Eval("FID")%>'><%# Eval("posts")%> Posts </a>&nbsp;&nbsp;&nbsp;--%>
                                <a class="btn btn-default btn-sm" href='forumReply.aspx?fid=<%# Eval("FID")%>#rply'><i class="fa fa-reply-all"></i>&nbsp; <%# Eval("posts")%> Replies</a>
                                                </div>
                                                <div class="pull-left">
                                                    <i class="fa fa-clock-o"></i>&nbsp;<%# Eval("Insertedon", "{0:dd/MM/yyyy hh:mm:ss tt}")%></div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                        

                        <div class="alert alert-default clearfix">
                            <h3></h3>
                            <div class="col-lg-8 ">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-2 control-label">Request Type</label>

                                    <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator4" ControlToValidate="txtTopic" runat="server" ErrorMessage="Invalid Name" SetFocusOnError="true" ValidationExpression="[a-zA-Z ]*$" Display="Dynamic"></asp:RegularExpressionValidator>--%>
                                    <div class="col-sm-10">
                                        <%--<asp:TextBox class="form-control" runat="server" ID="txtTopic" MaxLength="50" placeholder="Topic"></asp:TextBox>--%>

                                        <asp:DropDownList class="form-control" ID="txttopic" runat="server" >
                                            <asp:ListItem>Issue</asp:ListItem>
                                            <asp:ListItem>Suggestions</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter your Topic." SetFocusOnError="true" ControlToValidate="txtTopic" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputPassword3" class="col-sm-2 control-label">Title</label>

                                    <div class="col-sm-10">
                                        <asp:TextBox class="form-control" ID="txtTeamName" MaxLength="50" runat="server" placeholder="Title"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Enter Title." SetFocusOnError="true" ControlToValidate="txtTeamName" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputPassword3" class="col-sm-2 control-label">Description</label>

                                    <div class="col-sm-10">
                                        <asp:TextBox class="form-control" TextMode="MultiLine" ID="txtdescription" runat="server" placeholder="Description"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Enter your Description." SetFocusOnError="true" ControlToValidate="txtdescription" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-offset-6 col-sm-6">
                                        <br />
                                        <br />
                                        <%--<button type="submit" class="btn btn-primary pull-right">Submit</button>--%>
                                        <asp:Button ID="btnsubmit" runat="server" CssClass="btn btn-primary pull-right" Text="Submit" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                
            </div>
        </div>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpFooter" runat="Server">
    <!-- Bootstrap core JavaScript
    ================================================== -->
    <!-- Placed at the end of the document so the pages load faster -->
    <script src="js/jquery-1.11.3.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <!-- IE10 viewport hack for Surface/desktop Windows 8 bug -->
    <script src="js/ie10-viewport-bug-workaround.js"></script>
    <script>
        (function (i, s, o, g, r, a, m) {
            i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
                (i[r].q = i[r].q || []).push(arguments)
            }, i[r].l = 1 * new Date(); a = s.createElement(o),
            m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
        })(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');

        ga('create', 'UA-69629660-1', 'auto');
        ga('send', 'pageview');

    </script>
</asp:Content>

