<%@ Page Title="" Language="VB" MasterPageFile="settingmaster.master" AutoEventWireup="false" CodeFile="VendorCategoryManager.aspx.vb" Inherits="VendorCategoryManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_content" runat="Server">

    <div class="subMenuStrip"></div>
    <h1>Vendor Manager</h1>
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_footer" runat="Server">
</asp:Content>

