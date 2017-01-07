<%@ Page Title="" Language="VB" MasterPageFile="~/Apex.master" AutoEventWireup="false" CodeFile="VendorManager.aspx.vb" Inherits="Masters_VendorManager" %>

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


                        <div id="divError" runat="server" class="ui-state-error ui-corner-all divError">
                            <p>
                                <span class="ui-icon ui-icon-alert" style="float: left; margin-right: .3em;"></span>
                                <strong>Warning: </strong>
                                <asp:Label ID="lblError" runat="server"></asp:Label>
                            </p>
                        </div>
                        <div id="divContent" runat="server">
                            <div class="inner-Content-container">
                                <div class="InnerContentData">
                                    <table style="width: 99%">

                                        <tr id="tblNewClient" runat="server">
                                            <td>
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td colspan="2">&nbsp;</td>
                                                    </tr>

                                                    <tr>
                                                        <td class="tdhead">Vendor Category*</td>
                                                        <td>
                                                            <asp:DropDownList runat="server" ID="ddlvendorCategory" Width="150px" TabIndex="1"></asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="ddlvendorCategory" InitialValue="Select" ForeColor="Red"></asp:RequiredFieldValidator></td>

                                                    </tr>
                                                    <tr>
                                                        <td class="tdhead">Vendor Name*</td>
                                                        <td>
                                                            <asp:TextBox ID="txtVendor" runat="server" Width="150px" CssClass="textbox" MaxLength="100"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rvdVendor" runat="server" ErrorMessage="*" ControlToValidate="txtVendor" ForeColor="Red"></asp:RequiredFieldValidator></td>

                                                    </tr>
                                                    <tr>
                                                        <td class="tdhead">Is Active</td>
                                                        <td>
                                                            <asp:CheckBox ID="ChkActive" runat="server" Checked="True" />
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="text-align: center;">
                                                            <asp:Button ID="btnCancel" runat="server" Text="Back" CausesValidation="false" CssClass="small-button" />
                                                            <asp:Button ID="btnAdd" runat="server" Text="Save" CssClass="small-button" />
                                                            <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="small-button" />
                                                            <asp:Button ID="btnReset" runat="server" Text="Reset" CausesValidation="false" CssClass="small-button" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="text-align: center;">
                                                            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>

                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
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
