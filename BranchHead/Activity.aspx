<%@ Page Title="" Language="VB" MasterPageFile="~/Apex.master" AutoEventWireup="false" CodeFile="Activity.aspx.vb" Inherits="Activity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Activity Manager</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">Activity Manager</li>
        </ol>
    </section>

    <!-- Main content -->
    <section class="content">
        <div class="row">
            <div class="col-xs-12">
                <!-- .box -->
                <div class="box">
                    <div class="box-header">
                        <h3 class="box-title">List Of Activities</h3>
                        <asp:Button ID="btnAdd" runat="server" Text="Add New Activity" class="btn btn-primary pull-right" PostBackUrl="ActivityManager.aspx?mode=add" />
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                        <div class="span8">
                            <div class="text-right">
                                <div class="control-group">
                                    <div class="btn-group">
                                    </div>
                                </div>
                            </div>


                            <div class="">
                                <asp:GridView ID="gvActivity" runat="server" AutoGenerateColumns="false" CssClass="table table-hover table-bordered" Width="100%" AllowPaging="true" PageSize="5">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Row">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Row")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="ActivityId" Visible="false">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdnActivityID" runat="server" Value='<%#Bind("ProjectTypeID")%>' />

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText=" Activity">
                                            <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "ProjectType")%></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText=" Allow profit(%)">
                                            <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "Allowprofit")%></ItemTemplate>
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
    <asp:HiddenField ID="hdnActivityID" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpFooter" runat="Server">
    <script lang="javascript">
        function call(id) {
            location.href = "ActivityManager.aspx?mode=edit&lid=" + id;
        }
    </script>

</asp:Content>

