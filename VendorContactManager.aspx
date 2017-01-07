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

                        <div class="alert alert-danger" id="divError" runat="server">
                            <strong>Warning: </strong>
                            <asp:Label ID="lblError" runat="server"></asp:Label>

                        </div>

                        <div id="divContent" runat="server">

                            <div class="bs-example form-horizontal">

                                <div class="form-group">
                                    <label for="inputEmail" class="col-lg-2 control-label">Vendor Number*</label>
                                    <div class="col-lg-3">
                                        <asp:TextBox ID="txtVNo" runat="server" MaxLength="10" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-1">
                                        <asp:RequiredFieldValidator ID="rvdVendor" runat="server" ControlToValidate="txtVNo" ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                    </div>
                                    <label for="inputEmail" class="col-lg-2 control-label">Category Name*</label>
                                    <div class="col-lg-3">
                                        <asp:TextBox ID="txtCategoryName" runat="server" class="form-control" MaxLength="180"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-1">
                                        <asp:RequiredFieldValidator ID="rvdTxtCname" runat="server" ControlToValidate="txtCategoryName" ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                    </div>
                                </div>



                                <div class="form-group">
                                    <label for="inputEmail" class="col-lg-2 control-label">Vendor Name*</label>
                                    <div class="col-lg-3">
                                        <asp:TextBox ID="txtVendorName" runat="server" class="form-control" MaxLength="180"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-1">
                                        <asp:RequiredFieldValidator ID="rvdTxtCntDesig" runat="server" ControlToValidate="txtVendorName" ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                    </div>

                                    <label for="inputEmail" class="col-lg-2 control-label">Name 2</label>
                                    <div class="col-lg-3">
                                        <asp:TextBox ID="txtvendorname2" runat="server" class="form-control" MaxLength="180"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-1">
                                    </div>

                                </div>


                                <div class="form-group">
                                    <label for="inputEmail" class="col-lg-2 control-label">Address*</label>
                                    <div class="col-lg-3">
                                        <asp:TextBox ID="txtaddress" runat="server" class="form-control" MaxLength="250"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-1">
                                        <asp:RequiredFieldValidator ID="rvdTxtmob1" runat="server" ControlToValidate="txtaddress" ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>

                                    </div>

                                    <label for="inputEmail" class="col-lg-2 control-label">Address2</label>
                                    <div class="col-lg-3">
                                        <asp:TextBox ID="txtaddress2" runat="server" class="form-control" MaxLength="250"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-1">
                                    </div>
                                </div>


                                <div class="form-group">
                                    <label for="inputEmail" class="col-lg-2 control-label">Phone No.</label>
                                    <div class="col-lg-3">
                                        <asp:TextBox ID="txtphoneno" runat="server" class="form-control" MaxLength="150"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-1">
                                    </div>

                                    <label for="inputEmail" class="col-lg-2 control-label">Country*</label>
                                    <div class="col-lg-3">
                                        <asp:TextBox ID="txtcountry" runat="server" class="form-control" MaxLength="150"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-1">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtcountry" ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                    </div>
                                </div>



                                <div class="form-group">
                                    <label for="inputEmail" class="col-lg-2 control-label">State</label>
                                    <div class="col-lg-3">
                                        <asp:TextBox ID="txtstate" runat="server" MaxLength="13" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-1">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtstate" ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                    </div>

                                    <label for="inputEmail" class="col-lg-2 control-label">City*</label>
                                    <div class="col-lg-3">
                                        <asp:TextBox ID="txtcity" runat="server" class="form-control" MaxLength="150"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-1">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtcity" ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                    </div>
                                </div>


                                <div class="form-group">
                                    <label for="inputEmail" class="col-lg-2 control-label">PIN</label>
                                    <div class="col-lg-3">
                                        <asp:TextBox ID="txtpin" runat="server" MaxLength="6" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-1">
                                    </div>
                                    <label for="inputEmail" class="col-lg-2 control-label">Credit Term)</label>
                                    <div class="col-lg-3">
                                        <asp:DropDownList ID="ddlcreditterm" runat="server" CssClass="form-control" >
                                            <asp:ListItem >Select</asp:ListItem>
                                            <asp:ListItem >30 Days</asp:ListItem>
                                            <asp:ListItem >60 Days</asp:ListItem>
                                            <asp:ListItem >90 Days</asp:ListItem>
                                            <asp:ListItem >120 Days</asp:ListItem>
                                            <asp:ListItem >150 Days</asp:ListItem>
                                            <asp:ListItem >180 Days</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-1">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlcreditterm" ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label for="inputEmail" class="col-lg-2 control-label">Contact</label>
                                    <div class="col-lg-3">
                                        <asp:TextBox ID="txtcontact" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-1">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtcontact" ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                    </div>
                                    <label for="inputEmail" class="col-lg-2 control-label">Contact 2</label>
                                    <div class="col-lg-3">
                                        <asp:TextBox ID="txtcontact2" runat="server" class="form-control"></asp:TextBox>
                                    </div>

                                </div>

                                <div class="form-group">
                                    <label for="inputEmail" class="col-lg-2 control-label">Email ID</label>
                                    <div class="col-lg-3">
                                        <asp:TextBox ID="txtemailID" runat="server" class="form-control" MaxLength="150"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-1">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtemailID" ForeColor="Red" SetFocusOnError="True" Display="Dynamic">*</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Email" ControlToValidate="txtemailID" ForeColor="Red" SetFocusOnError="True" Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                    </div>
                                    <label for="inputEmail" class="col-lg-2 control-label">Website URL</label>
                                    <div class="col-lg-3">
                                        <asp:TextBox ID="txtwebsite" runat="server" class="form-control" MaxLength="250"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-3">
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtwebsite" ForeColor="Red" SetFocusOnError="True" Display="Dynamic">*</asp:RequiredFieldValidator>--%>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Invalid URL" ControlToValidate="txtwebsite" ForeColor="Red" SetFocusOnError="True" Display="Dynamic" ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail" class="col-lg-2 control-label">P.A.N. No.</label>
                                    <div class="col-lg-3">
                                        <asp:TextBox ID="txtpanno" runat="server" class="form-control" MaxLength="150"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-1">
                                    </div>
                                    <label for="inputEmail" class="col-lg-2 control-label">
                                        Service Tax
                                    </label>
                                    <div class="col-lg-3">
                                        <asp:TextBox ID="txtservicetax" runat="server" class="form-control" MaxLength="250"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label for="inputEmail" class="col-lg-2 control-label">
                                        TIN/VAT/LST
                                    </label>
                                    <div class="col-lg-3">
                                        <asp:TextBox ID="txttinVatLST" runat="server" class="form-control" MaxLength="150"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-1">
                                    </div>
                                    <label for="inputEmail" class="col-lg-2 control-label">
                                        CIN
                                    </label>
                                    <div class="col-lg-3">
                                        <asp:TextBox ID="txtCIN" runat="server" class="form-control" MaxLength="250"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label for="inputEmail" class="col-lg-2 control-label">
                                        Key concern
                                    </label>
                                    <div class="col-lg-3">
                                        <asp:TextBox ID="txtkeyconcern" runat="server" class="form-control" MaxLength="150"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-1">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtkeyconcern" ForeColor="Red" SetFocusOnError="True" Display="Dynamic">*</asp:RequiredFieldValidator>
                                    </div>
                                    <label for="inputEmail" class="col-lg-2 control-label">
                                        Remarks
                                    </label>
                                    <div class="col-lg-3">
                                        <asp:TextBox ID="txtremarks" runat="server" class="form-control" MaxLength="250"></asp:TextBox>
                                    </div>
                                </div>

                                  <div class="form-group">
                                    <label for="inputEmail" class="col-lg-2 control-label">
                                        Upload File
                                    </label>
                                    <div class="col-lg-3">
                                        <asp:FileUpload ID="fupl_Mail" runat="server" onchange="javascript:return CheckUpl();" />
                                    </div>
                                    <div class="col-lg-1">
                                        <asp:LinkButton ID="lblfulpl_Mail" runat="server" CssClass="btn btn-default btn-sm" ValidationGroup="sdada">Download</asp:LinkButton>
                                    </div>
                                    <label for="inputEmail" class="col-lg-2 control-label">
                                        Upload file 2
                                    </label>
                                      <div class="col-lg-3">
                                        <asp:FileUpload ID="FileUpload1" runat="server" onchange="javascript:return CheckUpl();" />
                                    </div>
                                    <div class="col-lg-1">
                                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-default btn-sm" ValidationGroup="sdada">Download</asp:LinkButton>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label for="inputEmail" class="col-lg-2 control-label">
                                        Upload File 3
                                    </label>
                                      <div class="col-lg-3">
                                        <asp:FileUpload ID="FileUpload2" runat="server" onchange="javascript:return CheckUpl();" />
                                    </div>
                                    <div class="col-lg-1">
                                        <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn btn-default btn-sm" ValidationGroup="sdada">Download</asp:LinkButton>
                                    </div>
                                    <label for="inputEmail" class="col-lg-2 control-label">
                                        Upload file 4
                                    </label>
                                       <div class="col-lg-3">
                                        <asp:FileUpload ID="FileUpload3" runat="server" onchange="javascript:return CheckUpl();" />
                                    </div>
                                    <div class="col-lg-1">
                                        <asp:LinkButton ID="LinkButton3" runat="server" CssClass="btn btn-default btn-sm" ValidationGroup="sdada">Download</asp:LinkButton>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label for="inputEmail" class="col-lg-2 control-label">
                                        Upload File 5
                                    </label>
                                       <div class="col-lg-3">
                                        <asp:FileUpload ID="FileUpload4" runat="server" onchange="javascript:return CheckUpl();" />
                                    </div>
                                    <div class="col-lg-1">
                                        <asp:LinkButton ID="LinkButton4" runat="server" CssClass="btn btn-default btn-sm" ValidationGroup="sdada">Download</asp:LinkButton>
                                    </div>
                                    <label for="inputEmail" class="col-lg-2 control-label">
                                        Upload file 6
                                    </label>
                                       <div class="col-lg-3">
                                        <asp:FileUpload ID="FileUpload5" runat="server" onchange="javascript:return CheckUpl();" />
                                    </div>
                                    <div class="col-lg-1">
                                        <asp:LinkButton ID="LinkButton5" runat="server" CssClass="btn btn-default btn-sm" ValidationGroup="sdada">Download</asp:LinkButton>
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

