<%@ Page Title="" Language="VB" MasterPageFile="~/Apex.master" AutoEventWireup="false" CodeFile="Designation.aspx.vb" Inherits="Designation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script lang="javascript">
        function call(id) {
            location.href = "DesignationManager.aspx?mode=edit&lid=" + id;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Designation Manager
        </h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="leads.aspx"><i class="fa fa-th"></i>&nbsp;Settings</a></li>
            <li class="active">Designation Manager</li>
        </ol>
    </section>

    <!-- Main content -->
    <section class="content">
        <div class="row">
            <div class="col-lg-xs-12">
                <div class="box box-primary">
                    <div class="box-header">
                        <h3 class="box-title">List of Designations</h3>
                        <asp:Button ID="btnAdd" runat="server" Text="Add New Designation" CssClass="btn btn-primary btn-small pull-right" PostBackUrl="DesignationManager.aspx?mode=add" />
                    </div>
                </div>
            </div>
            <div class="box-body">

                <asp:GridView ID="gvDesignation" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped" AllowPaging="true" PageSize="15">
                    <Columns>
                        <asp:TemplateField HeaderText="Row">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "Row")%>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="" Visible="false">
                            <ItemTemplate>
                                <%-- <%# DataBinder.Eval(Container.DataItem, "Client")%>--%>
                                <asp:HiddenField ID="hdnDesignationID" runat="server" Value='<%#Bind("DesignationID")%>' />

                            </ItemTemplate>
                        </asp:TemplateField>



                        <asp:TemplateField HeaderText="Designation">
                            <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "Designation")%></ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Is Active">
                            <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "IsActive")%></ItemTemplate>
                        </asp:TemplateField>



                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpFooter" runat="Server">
</asp:Content>

