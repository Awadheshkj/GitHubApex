<%@ Page Title="" Language="VB" MasterPageFile="~/Apex.master" AutoEventWireup="false" CodeFile="JobInvoice.aspx.vb" Inherits="JobInvoice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .auto-style1
        {
            width: 100%;
        }
    </style>

    <script type="text/javascript">

        $(function () {
            $('input[id$=txtDated]').datepicker({ dateFormat: 'dd-mm-yyyy' });
            $('input[id$=txtBuyerDate]').datepicker({ dateFormat: 'dd-mm-yyyy' });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="subMenuStrip"></div>
    <h1>Invoice</h1>
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

                <table class="auto-style1">
                    <tr>
                        <td rowspan="3">
                            <b>
                                <asp:Label ID="lblName" runat="server" Text="[Name]"></asp:Label></b>
                            <br />
                            <asp:Label ID="lblAddress" runat="server" Text="[Address]"></asp:Label>
                            <br />
                            <asp:Label ID="lblContact" runat="server" Text="[Contact]"></asp:Label>
                        </td>
                        <td class="tdhead">Invoice No.*</td>
                        <td>
                            <asp:TextBox ID="txtInvoiceNo" runat="server" MaxLength="20" CssClass="textbox"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtInvoiceNo" ErrorMessage="*" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <asp:Label ID="lblInvoiceNo" runat="server"></asp:Label>
                        </td>
                        <td class="tdhead">Dated*</td>
                        <td>
                            <asp:TextBox ID="txtDated" runat="server" onkeyup="return false" onkeydown="return false" CssClass="textbox"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDated" ErrorMessage="*" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <asp:Label ID="lblDated" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdhead">Supplier&apos;s Reference*</td>
                        <td>
                            <asp:TextBox ID="txtSupplierRef" runat="server" MaxLength="100" CssClass="textbox"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtSupplierRef" ErrorMessage="*" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <asp:Label ID="lblSupplierReference" runat="server"></asp:Label>
                        </td>
                        <td class="tdhead">Other Reference*</td>
                        <td>
                            <asp:TextBox ID="txtOtherRef" runat="server" MaxLength="100" CssClass="textbox"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtOtherRef" ErrorMessage="*" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <asp:Label ID="lblOtherReference" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdhead">Buyer&apos;s Order No.*</td>
                        <td>
                            <asp:TextBox ID="txtBuyer" runat="server" MaxLength="10" CssClass="textbox"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtBuyer" ErrorMessage="*" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <asp:Label ID="lblBuyer" runat="server"></asp:Label>
                        </td>
                        <td class="tdhead">Date*</td>
                        <td>
                            <asp:TextBox ID="txtBuyerDate" runat="server" onkeyup="return false" onkeydown="return false" CssClass="textbox"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtBuyerDate" ErrorMessage="*" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <asp:Label ID="lblBuyerDated" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table style="width: 100%;">
                                <tr>
                                    <td class="tdhead">ConsigneeAddress:
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdhead">
                                        <asp:Label ID="lblClient" runat="server" Text="[ClientName]"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>
                                        <%--<asp:Label ID="lblClientAddress" runat="server" Text="[ClientAddress]"></asp:Label>--%>
                                        <b>Client Address:</b>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblClientAddress" runat="server" Text="[Address]"></asp:Label>
                                    </td>
                                </tr>
                    </table>

                        </td>
                        <td colspan="3">
                            <table style="width: 100%">
                                <tr>
                                    <td><b>REMIT WIRE PAYMENT TO:</b></td>
                                </tr>
                                <tr>
                                    <td class="tdhead">
                                        <asp:Label ID="lblBankName" runat="server" Text="[BankName]"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblBankAddress" runat="server" Text="[BankAddress]"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td><b>Account No:</b>
                                        <asp:Label ID="lblAccountNo" runat="server" Text="[AccountNo]"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td><b>IFSC Code -</b>
                                        <asp:Label ID="lblIFSC" runat="server" Text="[IFSCNo]"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td><b>Swift Code -</b>
                                        <asp:Label ID="lblSwiftCode" runat="server" Text="[SWIFTNo]"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <asp:GridView ID="gvEstimate" runat="server" HeaderStyle-BackColor="#333333" HeaderStyle-ForeColor="White" Width="100%">
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="tdhead">Total Expenses</td>
                        <td>
                            <asp:Label ID="lblTotal" runat="server" Text="[Total]"></asp:Label></td>
                    </tr>
                    <tr>
                        <td colspan="2" class="tdhead">Agency Fee</td>
                        <td>
                            <asp:Label ID="lblAgencyFee" runat="server" Text="[AgencyFee]"></asp:Label></td>
                    </tr>
                    <tr>
                        <td colspan="2" class="tdhead">Service Tax (<asp:Label ID="lblServiceTax" runat="server" Text="[ServiceTax]"></asp:Label>%)</td>
                        <td>
                            <asp:Label ID="lblServiceTaxAmount" runat="server" Text="[ServiceTaxAmount]"></asp:Label></td>
                    </tr>
                    <tr>
                        <td colspan="2" class="tdhead">Total</td>
                        <td>
                            <asp:Label ID="lblTotalFee" runat="server" Text="[TotalFee]"></asp:Label></td>
                    </tr>
                    <%--<tr>
                        <td colspan="4">Primary Education Cess @ (<asp:Label ID="lblPrimary" runat="server"></asp:Label>)</td>
                        <td><asp:Label ID="Label2" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td colspan="4">Secondary Education Cess @ (<asp:Label ID="Label3" runat="server"></asp:Label>)</td>
                        <td><asp:Label ID="Label4" runat="server"></asp:Label></td>
                    </tr>--%>
                    <tr>
                        <td colspan="5">
                            <asp:Label ID="lblAmountWords" runat="server" Text="[AmountWords]"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="tdhead">PAN</td>
                        <td>
                            <asp:Label ID="lblPAN" runat="server" Text="[PANNo]"></asp:Label></td>
                        <td class="tdhead">TIN #</td>
                        <td colspan="2">
                            <asp:Label ID="lblTin" runat="server" Text="[TINNo]"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="tdhead">Service Tax No:</td>
                        <td>
                            <asp:Label ID="lblSTaxNo" runat="server" Text="[ServiceTaxNo]"></asp:Label></td>
                        <td class="tdhead">Service Tax Category</td>
                        <td colspan="2">
                            <asp:TextBox ID="lblSTaxCategory" runat="server" Text=""></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*" ControlToValidate="lblSTaxCategory"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
                <p>
                    <i>Declaration</i>
                </p>
                <p>
                    <i>We declare that this invoice shows the actual price described and that all particulars are true and correct.</i>
                </p>
                <div style="text-align: right; font-weight: bold;">
                    <p>for Kestone Integrated Marketing Services Pvt Ltd.</p>
                    <br />
                    <br />

                </div>
                <div style="padding-left: 70%">
                    <p>Authorized Signatory</p>
                </div>
                <hr />
               <%-- <div style="text-align: center">
                    <p>
                        <asp:Label ID="lblKName" runat="server" Text="[Kestone]"></asp:Label>
                    </p>
                    <p>
                        <asp:Label ID="lblKAddress" runat="server" Text="[KestoneAddress]"></asp:Label>
                    </p>
                </div>--%>
                <div style="text-align: center;">
                    <asp:Button ID="btnCancel" runat="server" Text="Back" CausesValidation="false" CssClass="small-button" />
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="small-button" />
                    <asp:Button ID="btnReset" runat="server" Text="Reset" CausesValidation="false" CssClass="small-button" />
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdnJobCardID" runat="server" />
    <asp:HiddenField ID="hdnNotificationID" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH_Right" runat="Server">
</asp:Content>

