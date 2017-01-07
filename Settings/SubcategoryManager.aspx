<%@ Page Title="" ValidateRequest="false" Language="VB" MasterPageFile="settingmaster.master" AutoEventWireup="false" CodeFile="SubcategoryManager.aspx.vb" Inherits="Masters_SubcategoryManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_content" runat="Server">

    <div class="subMenuStrip"></div>
    <h1>Subcategory Manager</h1>
    <div id="divError" runat="server" class="ui-state-error ui-corner-all divError">
        <p>
            <span class="ui-icon ui-icon-alert" style="float: left; margin-right: .3em;"></span>
            <strong>Warning: </strong>
            <asp:Label ID="lblError" runat="server"></asp:Label>
        </p>
    </div>

    <div id="divContent" runat="server">
        <div class="bs-example form-horizontal">

            <div class="form-group">
                <label for="inputEmail" class="col-lg-2 control-label">Category*</label>
                <div class="col-lg-3">
                    <asp:DropDownList ID="ddlCategory" runat="server" class="form-control">
                    </asp:DropDownList>

                </div>

                <div class="col-lg-1">

                    <asp:RequiredFieldValidator ID="rvdCategory" runat="server" ErrorMessage="*" ControlToValidate="ddlCategory" ForeColor="Red" SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                </div>
            </div>


            <div class="form-group">
                <label for="inputEmail" class="col-lg-2 control-label">Subcategory*</label>
                <div class="col-lg-3">
                    <asp:TextBox ID="txtSubcategory" runat="server" class="form-control" MaxLength="48"></asp:TextBox>
                </div>
                <div class="col-lg-1">
                    <asp:RequiredFieldValidator ID="rvdSubcategory" runat="server" ErrorMessage="*" ControlToValidate="txtSubcategory" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>


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
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                    <asp:Button ID="btnCancel" runat="server" Text="Back" CausesValidation="false" CssClass="btn btn-primary" />
                    <asp:Button ID="btnAdd" runat="server" Text="Save" CssClass="btn btn-primary" />
                    <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn btn-primary" />
                    <asp:Button ID="btnReset" runat="server" Text="Reset" CausesValidation="false" CssClass="btn btn-primary" />

                </div>
            </div>

        </div>


    </div>
    <asp:HiddenField ID="hdnSubCategory" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_footer" runat="Server">
</asp:Content>

