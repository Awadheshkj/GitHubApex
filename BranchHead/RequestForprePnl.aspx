﻿<%@ Page Title="" Language="VB" MasterPageFile="~/Apex.master" AutoEventWireup="false" CodeFile="RequestForprePnl.aspx.vb" Inherits="RequestForprePnl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Request For Pre P&L</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="leads.aspx"><i class="fa fa-th"></i>&nbsp;Settings</a></li>
            <li class="active">Request For Pre P&L</li>
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
                        <div class="alert alert-danger" id="divError" runat="server">

                            <strong>Warning: </strong>
                            <asp:Label ID="lblError" runat="server"></asp:Label>
                        </div>
                        <div class="alert alert-success" id="MessageDiv" runat="server">
                            <strong>Message: </strong>
                            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                        </div>


                        <div class="inner-Content-container">
                            <div class="InnerContentData">

                                <div id="divContent" runat="server">
                                    <div id="maindiv" runat="server">
                                        <div class="bs-example form-horizontal">
                                            <div class="form-group">
                                                <label for="inputEmail" class="col-lg-3 control-label">Select Project Manager*</label>
                                                <div class="col-lg-3">
                                                    <asp:DropDownList ID="ddlProjectManager" runat="server" class="form-control" TabIndex="1"></asp:DropDownList>

                                                </div>
                                                <div class="col-lg-1">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="ddlProjectManager" ErrorMessage="*" ForeColor="Red" InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label for="inputEmail" class="col-lg-3 control-label"></label>
                                                <div class="col-lg-3 text-center">

                                                    <asp:Button ID="btnSendRequert" runat="server" Text="Send Request for Pre P&L" CssClass="btn btn-primary btn-sm" TabIndex="2" />

                                                </div>
                                            </div>
                                            <div style="text-align: center;">
                                                <asp:Button ID="btnCancel" runat="server" Text="Back to Home" CausesValidation="false" CssClass="btn-primary btn-sm" />
                                            </div>
                                            <div>
                                                <asp:Label runat="server" ID="lblmsg" ForeColor="Red" Text=""></asp:Label>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <asp:HiddenField runat="server" ID="hdnBrefID" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpFooter" runat="Server">
    <div id="TWContent" runat="server"></div>
    <div id="TWManageTask" runat="server"></div>
    <div id="TWNotifications" runat="server"></div>
</asp:Content>

