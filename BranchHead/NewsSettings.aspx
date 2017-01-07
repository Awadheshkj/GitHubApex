<%@ Page Title="" Language="VB" MasterPageFile="~/Apex.master" AutoEventWireup="false" CodeFile="NewsSettings.aspx.vb" Inherits="Settings_News" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>News Manager</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>

            <li class="active">News Manager</li>
        </ol>
    </section>

    <!-- Main content -->
    <section class="content">
        <div class="row">
            <div class="col-lg-xs-12">
                <div class="box box-primary">
                    <div class="box-header">
                    </div>

                    <div class="box-body">
                        <div class="subMenuStrip"></div>
                        <h1>News Details</h1>

                        <div class="span8">
                            <div class="text-right">
                                <div class="control-group">
                                    <div class="btn-group">


                                        <asp:Button ID="btnAdd" runat="server" Text="Add New News" class="btn btn-primary pull-right" PostBackUrl="NewsManager.aspx?mode=add" />
                                    </div>

                                </div>
                            </div>
                            <div>
                                <asp:GridView ID="gvDesignation" runat="server" AutoGenerateColumns="false" CssClass="table table-hover table-bordered" Width="100%" AllowPaging="true" PageSize="15">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Row">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Row")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="" Visible="false">
                                            <ItemTemplate>
                                                <%-- <%# DataBinder.Eval(Container.DataItem, "Client")%>--%>
                                                <asp:HiddenField ID="hdnNewsID" runat="server" Value='<%#Bind("NewsID")%>' />

                                            </ItemTemplate>
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Title">
                                            <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "Title")%></ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "NewsDate")%></ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Description" ItemStyle-Width="450px">
                                            <ItemTemplate>
                                                <%--<%# DataBinder.Eval(Container.DataItem, "Description").ToString().Substring(0, 10)%>--%>

                                                <%# Eval("Description").ToString() %>

                                                <%--<asp:Label ID="Label1" runat="server" Text='<%#Eval("Description").ToString().Length >= 25 %>'></asp:Label>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%-- <asp:TemplateField HeaderText="Image">
                        <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "ImageURL")%></ItemTemplate>
                    </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="Is Active" ItemStyle-Width="60px">
                                            <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "IsActive")%></ItemTemplate>
                                        </asp:TemplateField>



                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpFooter" runat="Server">
    <script lang="javascript">
        function call(id) {
            location.href = "NewsManager.aspx?mode=edit&lid=" + id;
        }
    </script>

</asp:Content>


