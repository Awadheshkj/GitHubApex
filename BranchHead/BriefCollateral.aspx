<%@ Page Title="" Language="VB" MasterPageFile="~/Apex.master" AutoEventWireup="false" CodeFile="BriefCollateral.aspx.vb" Inherits="BriefCollateral" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Collaterals</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="leads.aspx"><i class="fa fa-th"></i>&nbsp;Settings</a></li>
            <li class="active">Collaterals</li>
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


                        <div class="col-12">

                            <div class="alert alert-success" id="divMsgAlert" runat="server"></div>


                            <div class="panel panel-info">
                                <div class="panel-body">
                                    <label for="inputEmail" class="col-lg-4 control-label">Upload supported documents (if any)</label>
                                    <div class="col-lg-3">
                                        <asp:FileUpload ID="FUpld_Documents" runat="server" onchange="CheckUpl();" />

                                    </div>
                                    <div class="col-lg-3">

                                        <strong>Title:</strong>
                                        <asp:TextBox ID="txtUploads" runat="server" ValidationGroup="UP" MaxLength="40" placeholder="Title"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="txtUploads" ErrorMessage="*" ForeColor="Red" ValidationGroup="UP"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg-2">
                                        <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="btn btn-sm btn-primary" ValidationGroup="UP" OnClientClick="return checkVal()" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-lg-12" id="divUploads">
                                <asp:GridView ID="gvFileUploads" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No.">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Row")%>
                                                <asp:HiddenField ID="hdnColletralID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "CollateralID")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Collateral Name">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "CollateralName")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <%--<a href="<%# DataBinder.Eval(Container.DataItem, "CollateralPath")%>" target="_blank">
                                                    <input type="button" class="btn btn-sm btn-primary" value="Download" />
                                                                                                </a>--%>
                                                <asp:LinkButton ID="lnkDownload" class="btn btn-sm btn-primary" Text="Download" CommandArgument='<%# Eval("CollateralPath")%>' runat="server" OnClick="DownloadFile"></asp:LinkButton>

                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="delete" CssClass="btn btn-sm btn-primary" CausesValidation="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="col-lg-12">
                                <label for="inputEmail" class="col-lg-3 control-label"></label>
                                <div class="form-group col-lg-6 text-center">
                                    <asp:Button ID="btnCancel" runat="server" Text="Back" CausesValidation="false" CssClass="btn btn-sm btn-primary" Visible="false" />
                                    <a href='javascript:history.go(-1)' class="btn btn-sm btn-primary">Back</a>
                                    <asp:Button ID="btnBrief" runat="server" Text="Prepare Pre P&L" CssClass="btn btn-sm btn-primary" />
                                </div>
                            </div>


                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>



    <asp:HiddenField ID="hdnBriefID" runat="server" />
    <asp:HiddenField ID="hdnJobCardID" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpFooter" runat="Server">
    <script>
        function CheckUpl() {
            var filename = $('input[id$=FUpld_Documents]').val();
            var fileInput = $('input[id$=FUpld_Documents]')[0];
            var sizeinbytes = fileInput.files[0].size;

            var fSExt = new Array('Bytes', 'KB', 'MB', 'GB');
            fSize = sizeinbytes;
            i = 0;
            while (fSize > 900) {
                fSize /= 1024; i++;
            }

            if (sizeinbytes >= 10485760) {

                $('input[id$=FUpld_Documents]').val("");
                $('input[id$=txtUploads]').val("");
                alert("File size not more than 10MB");

                return false;
            }


            if (window.chrome || $.browser.msie || $.browser.safari || window.opera) {
                var onlyname = filename.substring(12, filename.lastIndexOf('.'));
                var extname = filename.substring(onlyname.length + 13, filename.length);
            }
            else {
                var onlyname = filename.substring(filename.lastIndexOf('/') + 1, filename.lastIndexOf('.'));
                var extname = filename.substring(onlyname.length + 1, filename.length);
            }

            if (extname == "bat" || extname == "sys" || extname == "exe") {
                alert('File upload not allowed for this file type');
                $('input[id$=FUpld_Documents]').val("");
                $('input[id$=txtUploads]').val("");
            }
            else {
                $('input[id$=txtUploads]').val(onlyname);

            }
        }

        function checkVal() {
            if ($('input[id$=FUpld_Documents]').val() == "") {
                alert('No file to upload')
                return false;
            }
        }
    </script>

    <script>

        function pullModal() {

            $('#ClientModel').modal({
                keyboard: false
            });

        }
        function rediect() {
            window.location.href = '';
        }

    </script>
</asp:Content>

