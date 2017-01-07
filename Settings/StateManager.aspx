<%@ Page Title="" Language="VB" MasterPageFile="~/Settings/settingmaster.master" AutoEventWireup="false" CodeFile="StateManager.aspx.vb" Inherits="Masters_StateManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_content" runat="Server">

    <div class="subMenuStrip"></div>
    <h1>State Manager</h1>



    <div class="alert alert-success" id="divError" runat="server">
        <strong>Warning: </strong>
        <asp:Label ID="lblError" runat="server"></asp:Label>

    </div>

    <div id="divContent" runat="server">
        <div class="bs-example form-horizontal">
            <div class="form-group">
                <label for="inputEmail" class="col-lg-2 control-label">State*</label>
                <div class="col-lg-3">

                    <asp:TextBox ID="txtState" runat="server" class="form-control" MaxLength="40"></asp:TextBox>
                    

                </div>
                <div class="col-lg-1">
                    <asp:RequiredFieldValidator ID="rvdState" runat="server" ErrorMessage="*" ControlToValidate="txtState" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
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
                    <asp:Button ID="btnCancel" runat="server" Text="Back" CausesValidation="false" class="btn btn-primary" />
                    <asp:Button ID="btnAdd" runat="server" Text="Save" class="btn btn-primary" />
                    <asp:Button ID="btnEdit" runat="server" Text="Edit" class="btn btn-primary" />
                    <asp:Button ID="btnReset" runat="server" Text="Reset" CausesValidation="false" class="btn btn-primary" />
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

    <asp:HiddenField ID="hdnStateID" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_footer" runat="Server">
</asp:Content>

