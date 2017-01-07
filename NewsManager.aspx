<%@ Page Title="" Language="VB" MasterPageFile="~/Apex.master" AutoEventWireup="false" CodeFile="NewsManager.aspx.vb" Inherits="DesignationManager" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <link href="dist/css/bootstrap-datepicker3.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>News Manager</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>

            <li class="active">News Manager</li>
        </ol>
    </section>

    <!-- Main content -->
    <section class="content">
        <div class="row">
            <div class="col-lg-xs-12">
                <div class="box box-primary">
                    <div class="box-header">
                    </div>

                    <div class="box-body">

                        <div class="subMenuStrip"></div>

                        <div class="alert alert-success" id="divError" runat="server">
                            <strong>Warning: </strong>
                            <asp:Label ID="lblError" runat="server"></asp:Label>

                        </div>
                        <div id="divContent" runat="server">


                            <div class="bs-example form-horizontal">


                                <div class="form-group">
                                    <label for="inputEmail" class="col-lg-2 control-label">News Title*</label>
                                    <div class="col-lg-3">
                                        <asp:TextBox ID="txtNewstitle" runat="server" class="form-control" MaxLength="40"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-1">
                                        <asp:RequiredFieldValidator ID="rvdnewstitle" runat="server" ErrorMessage="*" ControlToValidate="txtNewstitle" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail" class="col-lg-2 control-label">News Date*</label>
                                    <div class="col-lg-3">
                                        <asp:TextBox ID="txtnewsdate" runat="server" class="form-control" MaxLength="40"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-1">
                                        <asp:RequiredFieldValidator ID="Rvdnewsdate" runat="server" ErrorMessage="*" ControlToValidate="txtnewsdate" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group" style="display: none;">
                                    <label for="inputEmail" class="col-lg-2 control-label">News Image</label>
                                    <div class="col-lg-3">
                                        <asp:FileUpload ID="FileUpload1" class="form-control" runat="server" />
                                    </div>
                                    <div class="col-lg-1">
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail" class="col-lg-2 control-label">News Type*</label>
                                    <div class="col-lg-3">
                                        <asp:DropDownList ID="ddlnewstype" class="form-control" runat="server">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="Internal">Internal</asp:ListItem>
                                            <asp:ListItem Value="External">External</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-1">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="ddlnewstype" InitialValue="0" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail" class="col-lg-2 control-label">News Discription*</label>
                                    <div class="col-lg-3">
                                        <asp:TextBox ID="txtnewsDiscription" runat="server" CssClass="form-control" TextMode="MultiLine" MaxLength="400" TabIndex="12"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-1">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="txtnewsDiscription" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
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
                                        <asp:Button ID="btnCancel" runat="server" Text="Back" CausesValidation="false" CssClass="btn  btn-primary" />
                                        <asp:Button ID="btnAdd" runat="server" Text="Save" CssClass="btn btn-primary" />
                                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn btn-primary" />
                                        <asp:Button ID="btnReset" runat="server" Text="Reset" CausesValidation="false" CssClass="btn btn-primary" />
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <asp:HiddenField ID="hdnNews" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpfooter" runat="Server">
     <script src="dist/js/bootstrap-datepicker.js"></script>
    <script type="text/javascript" src="../js/tiny_mce/tiny_mce.js"></script>

    <script type="text/javascript">
        tinyMCE.init({
            mode: "textareas",
            theme: "simple",
            theme: 'advanced',
            theme_advanced_buttons3_add: 'spellchecker',
            plugins: 'spellchecker',
            spellchecker_languages: '+English=en',
            theme_advanced_path: false,
        });

    </script>
    <script type="text/javascript">

        $(function () {
            $('input[id$=txtnewsdate]').datepicker({ format: 'dd-mm-yyyy', autoclose: true });
        });
    </script>


</asp:Content>

