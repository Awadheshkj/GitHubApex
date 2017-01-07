<%@ Page Title="" Language="VB" MasterPageFile="~/Apex.master"
    AutoEventWireup="false" CodeFile="EmployeeManager.aspx.vb" Inherits="EmployeeManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="content-header">
        <h1>Employee Manager
        </h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">Employee Manager</li>
        </ol>
    </section>

    <section class="content">

        <div class="row">
            <div class="col-xs-12">
                <!-- .box -->
                <div class="box">
                    <div class="box-header">
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

                        <div class="bs-example form-horizontal">
                            <div class="form-group">
                                <label for="inputEmail" class="col-lg-2 control-label">
                                    First Name*</label>
                                <div class="col-lg-3">
                                    <asp:TextBox ID="txtFname" runat="server" class="form-control" MaxLength="50"></asp:TextBox>
                                </div>
                                <div class="col-lg-1">
                                    <asp:RequiredFieldValidator ID="rvdTxtFname" runat="server" ControlToValidate="txtFname"
                                        ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                </div>
                                <label for="inputEmail" class="col-lg-2 control-label">
                                    Last Name*</label>
                                <div class="col-lg-3">
                                    <asp:TextBox ID="txtLName" runat="server" class="form-control" MaxLength="50"></asp:TextBox>
                                </div>
                                <div class="col-lg-1">
                                    <asp:RequiredFieldValidator ID="rvdTxtLname" runat="server" ControlToValidate="txtLName"
                                        ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail" class="col-lg-2 control-label">
                                    Designation*</label>
                                <div class="col-lg-3">
                                    <asp:DropDownList ID="ddlDesignation" runat="server" class="form-control">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-1">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFname"
                                        ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                </div>

                                <label for="inputEmail" class="col-lg-2 control-label">
                                    Emp Code*</label>
                                <div class="col-lg-3">
                                    <asp:TextBox ID="txtempcode" runat="server" class="form-control" MaxLength="50"></asp:TextBox>
                                </div>
                                <div class="col-lg-1">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtempcode"
                                        ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                </div>

                            </div>
                            <div class="form-group">
                                <label for="inputEmail" class="col-lg-2 control-label">
                                    Superior</label>
                                <div class="col-lg-3">
                                    <asp:DropDownList ID="ddlSuperior" runat="server" class="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail" class="col-lg-2 control-label">
                                    Mobile1*</label>
                                <div class="col-lg-3">
                                    <asp:TextBox ID="txtMobile1" runat="server" class="form-control" MaxLength="10"></asp:TextBox>
                                </div>
                                <div class="col-lg-1">
                                    <asp:RequiredFieldValidator ID="rvdTxtmob1" runat="server" ControlToValidate="txtMobile1"
                                        ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid"
                                        ControlToValidate="txtMobile1" Display="Dynamic" ForeColor="Red" ValidationExpression="\d{10}"></asp:RegularExpressionValidator>
                                </div>
                                <label for="inputEmail" class="col-lg-2 control-label">
                                    Mobile2</label>
                                <div class="col-lg-3">
                                    <asp:TextBox ID="txtMobile2" runat="server" class="form-control" MaxLength="10"></asp:TextBox>

                                </div>
                                <div class="col-lg-1">
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage=""
                                        ControlToValidate="txtMobile2" Display="Dynamic" ForeColor="Red" ValidationExpression="\d{10}"></asp:RegularExpressionValidator>

                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail" class="col-lg-2 control-label">
                                    Official Email*</label>
                                <div class="col-lg-3">
                                    <asp:TextBox ID="txtOfficialEmail" runat="server" class="form-control" AutoPostBack="True"
                                        MaxLength="100"></asp:TextBox>
                                </div>
                                <div class="col-lg-1">
                                    <asp:RequiredFieldValidator ID="rvdTxtOffEmail" runat="server" ControlToValidate="txtOfficialEmail"
                                        ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtOfficialEmail"
                                        Display="Dynamic" ErrorMessage="Invalid" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                </div>
                                <label for="inputEmail" class="col-lg-2 control-label">
                                    Personal Email</label>
                                <div class="col-lg-3">
                                    <asp:TextBox ID="txtPersonalEmail" runat="server" class="form-control" MaxLength="100"></asp:TextBox>
                                </div>
                                <div class="col-lg-1">
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtPersonalEmail"
                                        Display="Dynamic" ErrorMessage="Invalid" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail" class="col-lg-2 control-label">
                                    Landline 1</label>
                                <div class="col-lg-3">
                                    <asp:TextBox ID="txtLandLine1" runat="server" MaxLength="13" class="form-control"></asp:TextBox>
                                </div>
                                <div class="col-lg-1"></div>
                                <label for="inputEmail" class="col-lg-2 control-label">
                                    Landline 2</label>
                                <div class="col-lg-3">
                                    <asp:TextBox ID="txtLandLine2" runat="server" class="form-control" MaxLength="13"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail" class="col-lg-2 control-label">
                                    ResidentAddressline1*</label>
                                <div class="col-lg-3">
                                    <asp:TextBox ID="txtResAdd1" runat="server" MaxLength="100" class="form-control"></asp:TextBox>
                                </div>
                                <div class="col-lg-1">
                                    <asp:RequiredFieldValidator ID="rvdTxtResAdd1" runat="server" ControlToValidate="txtResAdd1"
                                        ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                </div>
                                <label for="inputEmail" class="col-lg-2 control-label">
                                    ResidentAddressline2</label>
                                <div class="col-lg-3">
                                    <asp:TextBox ID="txtResAdd2" runat="server" class="form-control" MaxLength="100"></asp:TextBox>
                                </div>

                            </div>
                            <div class="form-group">
                                <label for="inputEmail" class="col-lg-2 control-label">
                                    Resident State*</label>
                                <div class="col-lg-3">
                                    <asp:DropDownList ID="ddlResState" runat="server" AutoPostBack="True" class="form-control">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-1">
                                    <asp:RequiredFieldValidator ID="rvdddlresState" runat="server" ControlToValidate="ddlResState"
                                        ForeColor="Red" InitialValue="0" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                </div>
                                <label for="inputEmail" class="col-lg-2 control-label">
                                    Resident City*</label>
                                <div class="col-lg-3">
                                    <asp:DropDownList ID="ddlResCity" runat="server" class="form-control">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-1">
                                    <asp:RequiredFieldValidator ID="rvdddlResCity" runat="server" ControlToValidate="ddlResCity"
                                        ForeColor="Red" InitialValue="0" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail" class="col-lg-2 control-label">
                                    Resident Pin Code*</label>
                                <div class="col-lg-3">

                                    <asp:TextBox ID="txtResPcode" runat="server" Class="form-control" MaxLength="6"></asp:TextBox>
                                </div>
                                <div class="col-lg-1">
                                    <asp:RequiredFieldValidator ID="rvdTxtResAdd3" runat="server" ControlToValidate="txtResPcode"
                                        ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtResPcode"
                                        ErrorMessage="Invalid" ForeColor="Red" ValidationExpression="\d{6}"></asp:RegularExpressionValidator>

                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail" class="col-lg-2 control-label">
                                    Same As Above</label>
                                <div class="col-lg-3">
                                    <asp:CheckBox ID="chkAsAbove" runat="server" AutoPostBack="True" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail" class="col-lg-2 control-label">
                                    PermanentAddressline1*</label>
                                <div class="col-lg-3">
                                    <asp:TextBox ID="txtPermAdd1" runat="server" class="form-control" MaxLength="100"></asp:TextBox>
                                </div>
                                <div class="col-lg-1">
                                    <asp:RequiredFieldValidator ID="rvdTxtpemadd1" runat="server" ControlToValidate="txtPermAdd1"
                                        ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                </div>
                                <label for="inputEmail" class="col-lg-2 control-label">
                                    PermanentAddressline2</label>
                                <div class="col-lg-3">
                                    <asp:TextBox ID="txtPemAdd2" runat="server" class="form-control" MaxLength="100"></asp:TextBox>
                                </div>

                            </div>
                            <div class="form-group">
                                <label for="inputEmail" class="col-lg-2 control-label">
                                    Permanent State*</label>
                                <div class="col-lg-3">
                                    <asp:DropDownList ID="ddlPemState" runat="server" AutoPostBack="True" class="form-control">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-1">
                                    <asp:RequiredFieldValidator ID="rvdddlPemState" runat="server" ControlToValidate="ddlPemState"
                                        ForeColor="Red" InitialValue="0" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                </div>
                                <label for="inputEmail" class="col-lg-2 control-label">
                                    Permanent City*</label>
                                <div class="col-lg-3">
                                    <asp:DropDownList ID="ddlPemCity" runat="server" class="form-control">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-1">
                                    <asp:RequiredFieldValidator ID="rvdddlPemCity" runat="server" ControlToValidate="ddlPemCity"
                                        ForeColor="Red" InitialValue="0" SetFocusOnError="True">*</asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail" class="col-lg-2 control-label">
                                    Permanent Pin Code*</label>
                                <div class="col-lg-3">
                                    <asp:TextBox ID="txtPemPcode" runat="server" class="form-control" MaxLength="6"></asp:TextBox>
                                </div>
                                <div class="col-lg-1">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPemPcode"
                                        ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="Invalid"
                                        ControlToValidate="txtPemPcode" ForeColor="Red" ValidationExpression="\d{6}"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail" class="col-lg-2 control-label">
                                    IsActive
                                </label>
                                <div class="col-lg-3">
                                    <asp:CheckBox ID="chkIsActive" runat="server" Checked="True" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail" class="col-lg-2 control-label">
                                </label>
                                <div class="col-lg-4">
                                    <asp:Button ID="btnCancel" runat="server" Text="Back" CausesValidation="false" CssClass="btn btn-primary" />
                                    <asp:Button ID="btnAdd" runat="server" Text="Save" CssClass="btn btn-primary" />
                                    <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn btn-primary" />
                                    <asp:Button ID="btnReset" runat="server" Text="Reset" CausesValidation="false" CssClass="btn btn-primary" />
                                </div>
                            </div>
                        </div>

                        <asp:HiddenField ID="hdnEmployee" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpFooter" runat="Server">
</asp:Content>
