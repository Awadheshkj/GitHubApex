<%@ Page Title="" Language="VB" MasterPageFile="~/Apex.master" AutoEventWireup="false" CodeFile="Vendor.aspx.vb" Inherits="Masters_Vendor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script lang="javascript">
        function call(id) {
            location.href = "VendorManager.aspx?mode=edit&lid=" + id;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
       <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Vendor Details
        </h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">Vendor Details</li>
        </ol>
    </section>

    <!-- Main content -->
    <section class="content">
        <div class="row">
            <div class="col-xs-12">
                <div class="box">
                    <div class="box-header">
                        <h3 class="box-title">Vendor Details</h3>

                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                    
    

    <div class="span8">
        <div class="text-right">
            <div class="control-group">
                <div class="btn-group">

                    <asp:Button ID="btnAdd" runat="server" Text="Add New Vendor" class="btn btn-primary pull-right" PostBackUrl="VendorManager.aspx?mode=add" />
                </div>
            </div>
        </div>

        <div>
            <asp:GridView ID="gvVendor" runat="server" AutoGenerateColumns="false" CssClass="table table-hover table-bordered" Width="100%" AllowPaging="true" PageSize="15">
                <Columns>
                    <asp:TemplateField HeaderText="Row">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Row")%>
                        </ItemTemplate>
                    </asp:TemplateField>




                    <asp:TemplateField HeaderText="" Visible="false">
                        <ItemTemplate>
                            <%-- <%# DataBinder.Eval(Container.DataItem, "Client")%>--%>
                            <asp:HiddenField ID="hdnVendorID" runat="server" Value='<%#Bind("VendorID")%>' />

                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Vendor">
                        <ItemTemplate>
                            <%-- <%# DataBinder.Eval(Container.DataItem, "Client")%>--%>
                            <%# DataBinder.Eval(Container.DataItem, "VendorName")%>
                        </ItemTemplate>
                    </asp:TemplateField>



                    <asp:TemplateField HeaderText="Is Active">
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
</asp:Content>

