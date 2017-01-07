<%@ Page Title="" Language="VB" MasterPageFile="~/BranchHead/Apex.master" AutoEventWireup="false" CodeFile="ChangePassword.aspx.vb" Inherits="ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Change Password
           
        </h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">Change Password</li>
        </ol>
    </section>

    <!-- Main content -->
    <section class="content">
        <div class="row">
            <div class="col-xs-12">
                <!-- .box -->
                <div class="box">
                   
                    <!-- /.box-header -->
                    <div class="box-body">

                        <div class="container">
                         
                            <div class="col-12">
                                <div class="alert alert-success" id="divMsgAlert" runat="server"></div>
                            </div>
                            <div class="bs-example form-horizontal">
                                <div class="form-group">
                                    <label for="inputEmail" class="col-lg-2 control-label">New Password</label>
                                    <div class="col-lg-3">
                                        <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" CssClass="form-control" Width="60%"></asp:TextBox>

                                    </div>
                                    <div class="col-lg-1">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNewPassword" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label for="inputEmail" class="col-lg-2 control-label">Re-enter Password</label>
                                    <div class="col-lg-3">
                                        <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" CssClass="form-control" Width="60%"></asp:TextBox>


                                    </div>
                                    <div class="col-lg-1">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtConfirmPassword" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtNewPassword" ControlToValidate="txtConfirmPassword" EnableTheming="True" ErrorMessage="Password not same" ForeColor="Red"></asp:CompareValidator>
                                    </div>
                                </div>

                                <div class="form-group">

                                    <label for="inputEmail" class="col-lg-2 control-label"></label>
                                    <div class="col-lg-3">
                                        <asp:Button ID="btnChangePassword" runat="server" Text="Change Password" CssClass="btn btn-primary" />
                                    </div>


                                </div>

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

