<%@ Page Title="" Language="VB" MasterPageFile="~/Apex.master" AutoEventWireup="false" CodeFile="ClientContactManager.aspx.vb" Inherits="Masters_ClientContactManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        $(function () {
            $('input[id$=txtDob]').datepicker({ dateFormat: 'dd-mm-yyyy' });
            $('input[id$=txtAnniv]').datepicker({ dateFormat: 'dd-mm-yyyy' });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Client Contact Manager
        </h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="leads.aspx"><i class="fa fa-th"></i>&nbsp;Settings</a></li>
            <li class="active">Client Contact Manager</li>
        </ol>
    </section>

    <!-- Main content -->
    <section class="content">
        <div class="row">
            <div class="col-lg-xs-12">
                <div class="box box-primary">
                    <div class="box-header">
                        Client Contact
                    </div>
                    <div class="box-body">
                        <div class="alert alert-success" id="divError" runat="server">
                            <strong>Warning: </strong>
                            <asp:Label ID="lblError" runat="server"></asp:Label>

                        </div>
                        <div id="divContent" runat="server">
                            <div class="bs-example form-horizontal">

                                <div class="form-group">
                                    <div class="col-lg-2">
                                        <label for="inputEmail" class="control-label">Client*</label>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:DropDownList ID="ddlClients" runat="server" class="form-control">
                                        </asp:DropDownList>


                                    </div>
                                    <div class="col-lg-1">
                                        <asp:RequiredFieldValidator ID="rvdClient" runat="server" ControlToValidate="ddlClients" ForeColor="Red" SetFocusOnError="True" InitialValue="0">*</asp:RequiredFieldValidator>

                                    </div>
                                </div>


                                <div class="form-group">
                                    <div class="col-lg-2">
                                        <label for="inputEmail" class="control-label">Contact Name*</label>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:TextBox ID="txtContactName" runat="server" class="form-control" MaxLength="40"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-1">
                                        <asp:RequiredFieldValidator ID="rvdTxtCname" runat="server" ControlToValidate="txtContactName" ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-lg-2">
                                        <label for="inputEmail" class="control-label">Contact Designation*</label>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:TextBox ID="txtContactDesignation" runat="server" class="form-control" MaxLength="40"></asp:TextBox>
                                    </div>

                                    <div class="col-lg-1">
                                        <asp:RequiredFieldValidator ID="rvdTxtCntDesig" runat="server" ControlToValidate="txtContactDesignation" ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                    </div>

                                    <div class="col-lg-2">
                                        <label for="inputEmail" class="control-label">Contact Department*</label>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:TextBox ID="txtDept" runat="server" class="form-control" MaxLength="40"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-1">
                                        <asp:RequiredFieldValidator ID="rvdDept" runat="server" ControlToValidate="txtDept" ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                    </div>
                                </div>



                                <div class="form-group">
                                    <div class="col-lg-2">
                                        <label for="inputEmail" class="control-label">Mobile 1*</label>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:TextBox ID="txtMobile1" runat="server" class="form-control" MaxLength="10"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-1">
                                        <asp:RequiredFieldValidator ID="rvdTxtmob1" runat="server" ControlToValidate="txtMobile1" ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Mobile" ControlToValidate="txtMobile1" ValidationExpression="\d{10}" ForeColor="Red" Display="Dynamic"></asp:RegularExpressionValidator>
                                    </div>
                                    <div class="col-lg-2">
                                        <label for="inputEmail" class="control-label">Mobile 2</label>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:TextBox ID="txtMobile2" runat="server" class="form-control" MaxLength="10"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-1">
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Invalid Mobile" ControlToValidate="txtMobile2" ValidationExpression="\d{10}" ForeColor="Red" Display="Dynamic"></asp:RegularExpressionValidator>
                                    </div>
                                </div>



                                <div class="form-group">
                                    <div class="col-lg-2">
                                        <label for="inputEmail" class="control-label">Official Email*</label>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:TextBox ID="txtOfficialEmail" runat="server" class="form-control" AutoPostBack="True" MaxLength="100"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-1">
                                        <asp:RequiredFieldValidator ID="rvdTxtOffEmail" runat="server" ControlToValidate="txtOfficialEmail" ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtOfficialEmail" Display="Dynamic" ErrorMessage="Invalid Email" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                    </div>
                                    <div class="col-lg-2">
                                        <label for="inputEmail" class="control-label">Personal Email</label>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:TextBox ID="txtPersonalEmail" runat="server" class="form-control" MaxLength="100"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-1">
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtPersonalEmail" Display="Dynamic" ErrorMessage="Invalid Email" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                    </div>
                                </div>



                                <div class="form-group">
                                    <div class="col-lg-2">
                                        <label for="inputEmail" class="control-label">Landline 1</label>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:TextBox ID="txtLandLine1" runat="server" MaxLength="13" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-1">
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="Invalid Landline" ControlToValidate="txtLandLine1" ValidationExpression="\d{11,13}" ForeColor="Red" Display="Dynamic"></asp:RegularExpressionValidator>
                                    </div>
                                    <div class="col-lg-2">
                                        <label for="inputEmail" class="control-label">Landline 2</label>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:TextBox ID="txtLandLine2" runat="server" class="form-control" MaxLength="13"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-1">
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="Invalid Landline" ControlToValidate="txtLandLine2" ValidationExpression="\d{11,13}" ForeColor="Red" Display="Dynamic"></asp:RegularExpressionValidator>
                                    </div>
                                </div>



                                <div class="form-group">
                                    <div class="col-lg-2">
                                        <label for="inputEmail" class="control-label">Extension</label>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:TextBox ID="txtExt" runat="server" MaxLength="6" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-1">
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ErrorMessage="Invalid Extension" ControlToValidate="txtExt" ValidationExpression="\d{3,6}" ForeColor="Red" Display="Dynamic"></asp:RegularExpressionValidator>
                                    </div>

                                </div>



                                <div class="form-group">
                                    <div class="col-lg-2">
                                        <label for="inputEmail" class="control-label">Date of Birth</label>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:TextBox ID="txtDob" runat="server" class="form-control" onkeypress="return false" onkeyup="return false" onkeydown="return false"></asp:TextBox>
                                    </div>

                                </div>

                                <div class="form-group">
                                    <div class="col-lg-2">
                                        <label for="inputEmail" class="control-label">Spouse Name</label>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:TextBox ID="txtSpouse" runat="server" class="form-control" MaxLength="50"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-1">
                                    </div>
                                    <div class="col-lg-2">
                                        <label for="inputEmail" class="control-label">Aniversary</label>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:TextBox ID="txtAnniv" runat="server" class="form-control" onkeypress="return false" onkeyup="return false" onkeydown="return false"></asp:TextBox>
                                    </div>
                                </div>


                                <div class="form-group">
                                    <div class="col-lg-2">
                                        <label for="inputEmail" class="control-label">Is Active</label>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:CheckBox ID="chkIsActive" runat="server" Checked="True" />
                                    </div>

                                </div>


                                <div class="form-group">
                                    <label for="inputEmail" class="col-lg-2 control-label"></label>
                                    <div class="col-lg-3">
                                        <asp:Button ID="btnCancel" runat="server" Text="Back" CausesValidation="false" CssClass="btn btn-default btn-sm" />
                                        <asp:Button ID="btnAdd" runat="server" Text="Save" CssClass="btn btn-primary btn-sm" />
                                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn btn-primary btn-sm" />
                                        <asp:Button ID="btnReset" runat="server" Text="Reset" CausesValidation="false" CssClass="btn btn-default btn-sm" />
                                    </div>

                                </div>

                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <asp:HiddenField ID="hdnContactID" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpFooter" runat="Server">
</asp:Content>

