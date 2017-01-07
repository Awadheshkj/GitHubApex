<%@ Page Title="" Language="VB" MasterPageFile="~/Apex.master" AutoEventWireup="false" CodeFile="Employee.aspx.vb" Inherits="Employees" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script lang="javascript">
        function call(id) {
            location.href = "EmployeeManager.aspx?mode=edit&lid=" + id;
        }
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Employees 
        </h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">Employees</li>
        </ol>
    </section>

    <!-- Main content -->
    <section class="content">
        <div class="row">
            <div class="col-xs-12">
                <!-- .box -->
                <div class="box">
                    <div class="box-header">
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                        <h3>
                            <asp:Button ID="btnAdd" runat="server" Text="Add New Employee" class="btn btn-primary pull-right" PostBackUrl="EmployeeManager.aspx?mode=add" />
                        </h3>
                        <hr />
                        <div class="span8">


                            <div>
                                <asp:GridView ID="gvEmployee" runat="server" AutoGenerateColumns="false" CssClass="table table-hover table-bordered" Width="100%" AllowPaging="true" PageSize="15">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Row">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Row")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="" Visible="false">
                                            <ItemTemplate>
                                                <%-- <%# DataBinder.Eval(Container.DataItem, "Client")%>--%>
                                                <asp:HiddenField ID="hdnEmployeeID" runat="server" Value='<%#Bind("UserDetailsID")%>' />

                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "Name")%></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "Designation")%></ItemTemplate>
                                        </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Employee code">
                                            <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "Empcode")%></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Contact">
                                            <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "Contact")%></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Email Id">
                                            <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "EmailID")%></ItemTemplate>
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
