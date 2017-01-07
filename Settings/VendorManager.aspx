<%@ Page Title="" Language="VB" MasterPageFile="settingmaster.master" AutoEventWireup="false" CodeFile="VendorManager.aspx.vb" Inherits="Masters_VendorManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_content" runat="Server">

    <div class="subMenuStrip"></div>
    <h1>Vendor Manager</h1>
    <div id="divError" runat="server" class="ui-state-error ui-corner-all divError">
        <p>
            <span class="ui-icon ui-icon-alert" style="float: left; margin-right: .3em;"></span>
            <strong>Warning: </strong><asp:Label ID="lblError" runat="server"></asp:Label>
        </p>
    </div>
    <div id="divContent" runat="server">
        <div class="inner-Content-container" >
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_footer" runat="Server">
</asp:Content>
