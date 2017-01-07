<%@ Page Title="" Language="VB" MasterPageFile="~/Apex.master" AutoEventWireup="false" CodeFile="VendorContactManager.aspx.vb" Inherits="Masters_VendorContactManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        $(function () {
            $('input[id$=txtDob]').datepicker({ dateFormat: 'dd-mm-yy' });
            $('input[id$=txtAnniv]').datepicker({ dateFormat: 'dd-mm-yy' });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Vendor Contact Manager
        </h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">Vendor Contact Manager</li>
        </ol>
    </section>

    <!-- Main content -->
    <section class="content">
        <div class="row">
            <div class="col-xs-12">
                <div class="box">
                    <div class="box-header">
                        <h3 class="box-title">Vendor Contact Manager</h3>

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
                <label for="inputEmail" class="col-lg-2 control-label">Vendor*</label>
                <div class="col-lg-3">
                    <asp:DropDownList ID="ddlVendor" runat="server" class="form-control"></asp:DropDownList>
                </div>
                <div class="col-lg-1">
                    <asp:RequiredFieldValidator ID="rvdVendor" runat="server" ControlToValidate="ddlVendor" ForeColor="Red" SetFocusOnError="True" InitialValue="0">*</asp:RequiredFieldValidator>
                </div>
            </div>


            <div class="form-group">
                <label for="inputEmail" class="col-lg-2 control-label">Contact Name*</label>
                <div class="col-lg-3">
                    <asp:TextBox ID="txtContactName" runat="server" class="form-control" MaxLength="180"></asp:TextBox>
                </div>
                <div class="col-lg-1">
                    <asp:RequiredFieldValidator ID="rvdTxtCname" runat="server" ControlToValidate="txtContactName" ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                </div>
            </div>

            <div class="form-group">
                <label for="inputEmail" class="col-lg-2 control-label">Contact Designation*</label>
                <div class="col-lg-3">
                    <asp:TextBox ID="txtContactDesignation" runat="server" class="form-control" MaxLength="180"></asp:TextBox>
                </div>
                <div class="col-lg-1">
                    <asp:RequiredFieldValidator ID="rvdTxtCntDesig" runat="server" ControlToValidate="txtContactDesignation" ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                </div>

                <label for="inputEmail" class="col-lg-2 control-label">Contact Department*</label>
                <div class="col-lg-3">
                    <asp:TextBox ID="txtDept" runat="server" class="form-control" MaxLength="180"></asp:TextBox>
                </div>
                <div class="col-lg-1">
                    <asp:RequiredFieldValidator ID="rvdDept" runat="server" ControlToValidate="txtDept" ForeColor="Red" SetFocusOnError="True" ErrorMessage="*">*</asp:RequiredFieldValidator>
                </div>

            </div>


            <div class="form-group">
                <label for="inputEmail" class="col-lg-2 control-label">Mobile1*</label>
                <div class="col-lg-3">
                    <asp:TextBox ID="txtMobile1" runat="server" class="form-control" MaxLength="10"></asp:TextBox>
                </div>
                <div class="col-lg-1">
                    <asp:RequiredFieldValidator ID="rvdTxtmob1" runat="server" ControlToValidate="txtMobile1" ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Mobile" ControlToValidate="txtMobile1" ValidationExpression="\d{10}" ForeColor="Red" Display="Dynamic"></asp:RegularExpressionValidator>
                </div>

                <label for="inputEmail" class="col-lg-2 control-label">Mobile2</label>
                <div class="col-lg-3">
                    <asp:TextBox ID="txtMobile2" runat="server" class="form-control" MaxLength="10"></asp:TextBox>
                </div>
                <div class="col-lg-1">
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Invalid Mobile" ControlToValidate="txtMobile2" ValidationExpression="\d{10}" ForeColor="Red" Display="Dynamic"></asp:RegularExpressionValidator>
                </div>
            </div>


            <div class="form-group">
                <label for="inputEmail" class="col-lg-2 control-label">Official Email*</label>
                <div class="col-lg-3">
                    <asp:TextBox ID="txtOfficialEmail" runat="server" AutoPostBack="True" class="form-control" MaxLength="150"></asp:TextBox>
                </div>
                <div class="col-lg-1">
                    <asp:RequiredFieldValidator ID="rvdTxtOffEmail" runat="server" ControlToValidate="txtOfficialEmail" ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtOfficialEmail" Display="Dynamic" ErrorMessage="Invalid Email" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                </div>

                <label for="inputEmail" class="col-lg-2 control-label">Personal Email</label>
                <div class="col-lg-3">
                    <asp:TextBox ID="txtPersonalEmail" runat="server" class="form-control" MaxLength="150"></asp:TextBox>
                </div>
                <div class="col-lg-1">
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtPersonalEmail" Display="Dynamic" ErrorMessage="Invalid Email" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                </div>
            </div>



            <div class="form-group">
                <label for="inputEmail" class="col-lg-2 control-label">Landline 1</label>
                <div class="col-lg-3">
                    <asp:TextBox ID="txtLandLine1" runat="server" MaxLength="13" class="form-control"></asp:TextBox>
                </div>
                <div class="col-lg-1">
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="Invalid Landline" ControlToValidate="txtLandLine1" ValidationExpression="\d{11,13}" ForeColor="Red" Display="Dynamic"></asp:RegularExpressionValidator>
                </div>

                <label for="inputEmail" class="col-lg-2 control-label">Landline 2</label>
                <div class="col-lg-3">
                    <asp:TextBox ID="txtLandLine2" runat="server" class="form-control" MaxLength="13"></asp:TextBox>
                </div>
                <div class="col-lg-1">
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="Invalid Landline" ControlToValidate="txtLandLine2" ValidationExpression="\d{11,13}" ForeColor="Red" Display="Dynamic"></asp:RegularExpressionValidator>
                </div>
            </div>


            <div class="form-group">
                <label for="inputEmail" class="col-lg-2 control-label">Extension</label>
                <div class="col-lg-3">
                    <asp:TextBox ID="txtExt" runat="server" MaxLength="6" class="form-control"></asp:TextBox>
                </div>
                <div class="col-lg-1">
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ErrorMessage="Invalid Extension" ControlToValidate="txtExt" ValidationExpression="\d{3,6}" ForeColor="Red" Display="Dynamic"></asp:RegularExpressionValidator>
                </div>

            </div>

            <div class="form-group">
                <label for="inputEmail" class="col-lg-2 control-label">Date Of Birth</label>
                <div class="col-lg-3">
                    <asp:TextBox ID="txtDob" runat="server" class="form-control" onkeypress="return false"></asp:TextBox>
                </div>

            </div>

            <div class="form-group">
                <label for="inputEmail" class="col-lg-2 control-label">Spouse Name</label>
                <div class="col-lg-3">
                    <asp:TextBox ID="txtSpouse" runat="server" class="form-control" MaxLength="150"></asp:TextBox>
                </div>
                <div class="col-lg-1">
                </div>
                <label for="inputEmail" class="col-lg-2 control-label">Aniversary</label>
                <div class="col-lg-3">
                    <asp:TextBox ID="txtAnniv" runat="server" class="form-control" onkeypress="return false"></asp:TextBox>
                </div>
            </div>


            <div class="form-group">
                <label for="inputEmail" class="col-lg-2 control-label">IsActive</label>
                <div class="col-lg-3">
                    <asp:CheckBox ID="chkIsActive" runat="server" Checked="True" />
                </div>

            </div>

            <div class="form-group">
                <label for="inputEmail" class="col-lg-2 control-label"></label>
                <div class="col-lg-3">

                    <asp:Button ID="btnCancel" runat="server" Text="Back" CausesValidation="false" CssClass="btn btn-primary" />
                    <asp:Button ID="btnAdd" runat="server" Text="Save" CssClass="btn btn-primary" />
                    <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn btn-primary" />
                    <asp:Button ID="btnReset" runat="server" Text="Reset" CausesValidation="false" CssClass="btn btn-primary" />

                </div>

            </div>

        </div>



    </div>
    <asp:HiddenField ID="hdnVendorContactID" runat="server" />
    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpFooter" runat="Server">
</asp:Content>

