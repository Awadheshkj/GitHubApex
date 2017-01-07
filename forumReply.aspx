<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/Apex.master" CodeFile="forumReply.aspx.vb" Inherits="forum" MaintainScrollPositionOnPostback="true" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container" id="BodyContainer">

      
        <div class="container" id="ContentContainer">
            <div class="container">
                <div class="col-lg-12">
                    <h1>Issues/Suggestions Replies</h1>
                    <br />
                </div>
              
                    <div class="col-lg-12">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <asp:Literal ID="lbltopic" runat="server"></asp:Literal><div class="pull-right">
                                    <i class="fa fa-users"></i>&nbsp;
                                <asp:Literal ID="lblteamName" runat="server"></asp:Literal>
                                </div>
                            </div>
                            <div class="panel-body">
                                <div class="forum-body">
                                    <div class="post">
                                        <asp:Literal ID="lbldescription" runat="server"></asp:Literal>
                                    </div>

                                </div>
                            </div>
                            <div class="panel-footer clearfix">
                                <div class="pull-right">
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Literal ID="lblposts" runat="server" Text="0"></asp:Literal>&nbsp;Posts
                                </div>
                                <div class="pull-left">
                                    <i class="fa fa-clock-o"></i>&nbsp;<asp:Literal ID="lblinseredon" runat="server"></asp:Literal>

                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-12 ">
                        <asp:GridView ID="gvforum" runat="server" AutoGenerateColumns="false" ShowHeader="false" CssClass="col-lg-12" GridLines="None">
                            <Columns>
                                <asp:TemplateField>

                                    <ItemTemplate>
                                        <div class="alert alert-info">
                                            <div class="post">
                                                <b>Description : </b><%# Eval("Description")%>
                                            </div>
                                            <div class="alert-footer clearfix">
                                                <div class="pull-right"><i class="fa fa-users"></i>&nbsp;<%# Eval("replyname")%></div>
                                                <div class="pull-left">
                                                    <i class="fa fa-clock-o"></i>&nbsp;<%# Eval("Insertedon", "{0:dd/MM/yyyy hh:mm:ss tt}")%>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="col-lg-12 " id="rply">
                        <div class="alert alert-default clearfix">
                            <h3></h3>
                            <div class="col-lg-8 ">

                                <div class="form-group">
                                    <label for="inputPassword3" class="col-sm-2 control-label"> Name</label>

                                    <div class="col-sm-10">
                                        <asp:TextBox CssClass="form-control" ID="txtTeamName" MaxLength="50" Enabled="false" runat="server" placeholder=" Name"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Enter your Name." SetFocusOnError="true" ControlToValidate="txtTeamName" Display="Dynamic"></asp:RequiredFieldValidator>
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

                                        <asp:Button ID="btnsubmit" runat="server" CssClass="btn btn-primary pull-right" Text="Submit" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:HiddenField ID="hdnFID" Value="0" runat="server" />
               
            </div>
        </div>
        
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpFooter" runat="Server">
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

