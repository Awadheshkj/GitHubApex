<%@ Page Title="" ValidateRequest="false" Language="VB" MasterPageFile="~/Apex.master" AutoEventWireup="false" CodeFile="SubcategoryManager.aspx.vb" Inherits="Masters_SubcategoryManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Sub-Category Manager
        </h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="leads.aspx"><i class="fa fa-th"></i>&nbsp;Settings</a></li>
            <li class="active">SubCategory Manager</li>
        </ol>
    </section>

    <!-- Main content -->
    <section class="content">
        <div class="row">
            <div class="col-lg-xs-12">
                <div class="box box-primary">
                    <div class="box-header">
                        Add New Sub-Category
                    </div>
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
                                                        <td class="tdhead">Category*</td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control" Width="150px">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rvdCategory" runat="server" ErrorMessage="*" ControlToValidate="ddlCategory" ForeColor="Red" SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator></td>

                                                    </tr>
                                                    <tr>
                                                        <td class="tdhead">Subcategory*</td>
                                                        <td>
                                                            <asp:TextBox ID="txtSubcategory" runat="server" Width="150px" CssClass="form-control" MaxLength="48"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rvdSubcategory" runat="server" ErrorMessage="*" ControlToValidate="txtSubcategory" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator></td>

                                                    </tr>
                                                    <tr>
                                                        <td class="tdhead">Is Active</td>
                                                        <td>
                                                            <asp:CheckBox ID="ChkActive" runat="server" Checked="True" />
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="text-align: center;">
                                                            <asp:Button ID="btnCancel" runat="server" Text="Back" CausesValidation="false" CssClass="btn btn-default btn-sm" />
                                                            <asp:Button ID="btnAdd" runat="server" Text="Save" CssClass="btn btn-primary btn-sm" />
                                                            <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn btn-primary btn-sm" />
                                                            <asp:Button ID="btnReset" runat="server" Text="Reset" CausesValidation="false" CssClass="btn btn-default btn-sm" />
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
                    </div>
                </div>
            </div>
        </div>
    </section>
    <asp:HiddenField ID="hdnSubCategory" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpFooter" runat="Server">
</asp:Content>

