<%@ Page Title="" Language="VB" MasterPageFile="~/Apex.master" AutoEventWireup="false" CodeFile="VendorCategoryManager.aspx.vb" Inherits="VendorCategoryManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Vendor Manager
        </h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">Vendor Manager</li>
        </ol>
    </section>

    <!-- Main content -->
    <section class="content">
        <div class="row">
            <div class="col-xs-12">
                <div class="box">
                    <div class="box-header">
                        <h3 class="box-title">Vendor Manager</h3>

                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">


                        <div class="alert alert-success" id="divError" runat="server">
                            <strong>Warning: </strong>
                            <asp:Label ID="lblError" runat="server"></asp:Label>

                        </div>

                        <div id="divContent" runat="server">

                            <div class="bs-example form-horizontal">

                                <div class="form-group">
                                    <label for="inputEmail" class="col-lg-3 control-label">Vendor Category Name*</label>
                                    <div class="col-lg-3">
                                        <asp:TextBox ID="txtVendorCategory" runat="server" class="form-control" MaxLength="100"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-1">
                                        <asp:RequiredFieldValidator ID="rvdVendor" runat="server" ErrorMessage="*" ControlToValidate="txtVendorCategory" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>
                                </div>


                                <div class="form-group">
                                    <label for="inputEmail" class="col-lg-3 control-label">Is Active</label>
                                    <div class="col-lg-3">
                                        <asp:CheckBox ID="ChkActive" runat="server" Checked="True" />
                                    </div>
                                </div>


                                <div class="form-group">
                                    <label for="inputEmail" class="col-lg-3 control-label"></label>
                                    <div class="col-lg-3">
                                        <asp:Button ID="btnCancel" runat="server" Text="Back" CausesValidation="false" CssClass="btn btn-primary" />
                                        <asp:Button ID="btnAdd" runat="server" Text="Save" CssClass="btn btn-primary" />
                                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn btn-primary" />
                                        <asp:Button ID="btnReset" runat="server" Text="Reset" CausesValidation="false" CssClass="btn btn-primary" />

                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail" class="col-lg-2 control-label"></label>
                                    <div class="col-lg-3">
                                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <asp:HiddenField ID="hdnVendor" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpFooter" runat="Server">
</asp:Content>

