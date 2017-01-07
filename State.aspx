<%@ Page Title="" Language="VB" MasterPageFile="~/Apex.master" AutoEventWireup="false" CodeFile="State.aspx.vb" Inherits="Masters_State" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script lang="javascript">
        function call(id) {
            location.href = "StateManager.aspx?mode=edit&lid=" + id;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>State Manager
        </h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="leads.aspx"><i class="fa fa-th"></i>&nbsp;Settings</a></li>
            <li class="active">State Manager</li>
        </ol>
    </section>

    <!-- Main content -->
    <section class="content">
        <div class="row">
            <div class="col-lg-xs-12">
                <div class="box box-primary">
                    <div class="box-header">
                        <h3 class="box-title">List of States</h3>
                        <asp:Button ID="btnAdd" runat="server" Text="Add New State" CssClass="btn btn-primary btn-small pull-right" PostBackUrl="StateManager.aspx?mode=add" />
                    </div>
                </div>
                <div class="box-body">
                    <asp:GridView ID="gvState" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped" AllowPaging="true" PageSize="15">
                        <Columns>
                            <asp:TemplateField HeaderText="Row">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "Row")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="" Visible="false">
                                <ItemTemplate>
                                    <%-- <%# DataBinder.Eval(Container.DataItem, "Client")%>--%>
                                    <asp:HiddenField ID="hdnStateID" runat="server" Value='<%#Bind("StateID")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="State">
                                <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "State")%></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Is Active">
                                <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "IsActive")%></ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </section>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpFooter" runat="Server">
</asp:Content>

