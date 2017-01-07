<%@ Page Title="" Language="VB" ValidateRequest="false" MasterPageFile="~/Apex.master" AutoEventWireup="false" CodeFile="ActivityManager.aspx.vb" Inherits="ActivityManager" %>

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
                        <h3 class="box-title">Add New Activity</h3>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                        <div class="subMenuStrip"></div>

                        <div class="alert alert-success" id="divError" runat="server">
                            <strong>Warning: </strong>
                            <asp:Label ID="lblError" runat="server"></asp:Label>
                        </div>
                        <div id="divContent" runat="server">
                            <div class="bs-example form-horizontal">
                                <div class="form-group">
                                    <label for="inputEmail" class="col-lg-2 control-label">Activity Name*</label>
                                    <div class="col-lg-3">
                                        <asp:TextBox ID="txtActivity" runat="server" class="form-control" MaxLength="40"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rvdActivity" runat="server" ErrorMessage="*" ControlToValidate="txtActivity" ForeColor="Red"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail" class="col-lg-2 control-label">Allow Profit(%)*</label>
                                    <div class="col-lg-3">

                                        <asp:TextBox ID="txtprofit" runat="server" Text="0.00" class="form-control" MaxLength="3"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtprofit" ForeColor="Red"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail" class="col-lg-2 control-label">Is Active</label>
                                    <div class="col-lg-3">
                                        <asp:CheckBox ID="ChkActive" runat="server" Checked="True" />


                                    </div>




                                </div>
                                <div class="form-group">
                                    <label for="inputEmail" class="col-lg-2 control-label"></label>
                                    <div class="col-lg-3">
                                        <asp:Button ID="btnCancel" runat="server" Text="Back" CausesValidation="false" CssClass="btn btn-primary" />
                                        <asp:Button ID="btnAdd" runat="server" Text="Save" CssClass="btn btn-primary" />
                                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn btn-primary" />
                                        <asp:Button ID="btnReset" runat="server" Text="Reset" CausesValidation="false" CssClass="btn btn-primary" />
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <asp:HiddenField ID="hdnActivity" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpFooter" runat="Server">
</asp:Content>

