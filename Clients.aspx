<%@ Page Title="" Language="VB" MasterPageFile="~/Apex.master" AutoEventWireup="false" CodeFile="Clients.aspx.vb" Inherits="Clients" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script lang="javascript">
        function call(id) {
            location.href = "ClientManager.aspx?mode=edit&lid=" + id;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Client Details</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">Clients</li>
        </ol>
    </section>

    <!-- Main content -->
    <section class="content">
        <div class="row">
            <div class="col-xs-12">
                <!-- .box -->
                <div class="box">
                    <div class="box-header">
                        <h3 class="box-title">List Of Clients</h3>
                        <asp:Button ID="btnAdd" runat="server" Text="Add New Client" class="btn btn-sm btn-primary pull-right" PostBackUrl="ClientManager.aspx?mode=add" />
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">

                        <div class="span8">
                            <div class="">
                                <asp:GridView ID="gvClient" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped" Width="100%" AllowPaging="true" PageSize="15">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Row">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Row")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="" Visible="false">
                                            <ItemTemplate>
                                                <%-- <%# DataBinder.Eval(Container.DataItem, "Client")%>--%>
                                                <asp:HiddenField ID="hdnClientID" runat="server" Value='<%#Bind("ClientID")%>' />

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Client">
                                            <ItemTemplate>
                                                <%-- <%# DataBinder.Eval(Container.DataItem, "Client")%>--%>
                                                <%# DataBinder.Eval(Container.DataItem, "Client")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Industry">
                                            <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "Industry")%></ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Annual Turnover">
                                            <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "AnnualTurnover")%></ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Is Active">
                                            <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "IsActive")%></ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="View Contact">
                                            <ItemTemplate>
                                                <a href='ClientContact.aspx?cid=<%# DataBinder.Eval(Container.DataItem, "ClientID")%>'>View</a>

                                            </ItemTemplate>
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
</asp:Content>

