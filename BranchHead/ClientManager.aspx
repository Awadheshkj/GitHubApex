<%@ Page Title="" Language="VB" MasterPageFile="~/Apex.master" AutoEventWireup="false" CodeFile="ClientManager.aspx.vb" Inherits="Masters_ClientManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function IsNumeric(evt) {
            var Charcode = evt.code
            if (Charcode >= 48 && Charcode <= 57) {
                return true;
            }
            else {
                alert('Please fill only numeric value !')
                return false;
            }

        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Client Details</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">Client Details</li>
        </ol>
    </section>

    <!-- Main content -->
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

                        <div id="div1" runat="server">
                            <div class="bs-example form-horizontal">
                                <div class="form-group">
                                    <div class="col-lg-2">
                                        <label for="inputEmail" class="control-label">Client Name*</label>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:TextBox ID="txtClient" runat="server" Class="form-control" MaxLength="40"></asp:TextBox>

                                    </div>
                                    <div class="col-lg-1">
                                        <asp:RequiredFieldValidator ID="rvdClient" SetFocusOnError="true" runat="server" ErrorMessage="*" ControlToValidate="txtClient" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-lg-2">
                                        <label for="inputEmail" class="control-label">Address</label>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" Class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-lg-2">
                                        <label for="inputEmail" class="control-label">Industry*</label>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:TextBox ID="txtIndustry" runat="server" Class="form-control" MaxLength="40"></asp:TextBox>

                                    </div>
                                    <div class="col-lg-1">
                                        <asp:RequiredFieldValidator ID="rvdIndustry" SetFocusOnError="true" runat="server" ErrorMessage="*" ControlToValidate="txtIndustry" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-lg-2">
                                        <label for="inputEmail" class="control-label">Annual Turnover</label>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:TextBox ID="txtAnnTurn" runat="server" Class="form-control" MaxLength="15"></asp:TextBox>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-lg-2">
                                        <label for="inputEmail" class="control-label">Is Active</label>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:CheckBox ID="ChkActive" runat="server" Checked="True" />
                                    </div>

                                </div>
                                <div class="form-group">
                                    <label for="inputEmail" class="col-lg-2 control-label"></label>
                                    <div class="col-lg-3">
                                        <asp:Button ID="btnCancel" runat="server" Text="Back" CausesValidation="false" CssClass="btn btn-sm btn-primary" />
                                        <asp:Button ID="btnAdd" runat="server" Text="Save" CssClass="btn btn-sm btn-primary" />
                                        <asp:Button ID="btnEdit" runat="server" Text="Save" CssClass="btn btn-sm btn-primary" />
                                        <asp:Button ID="btnReset" runat="server" Text="Reset" CausesValidation="false" CssClass="btn btn-sm btn-primary" />
                                    </div>
                                </div>

                                <div>
                                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                                </div>


                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <asp:HiddenField ID="hdnClientId" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpFooter" runat="Server">
</asp:Content>

