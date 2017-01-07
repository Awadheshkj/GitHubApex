<%@ Page Title="" Language="VB" MasterPageFile="~/Apex.master" AutoEventWireup="false" CodeFile="ViewJobInvoice.aspx.vb" Inherits="ViewJobInvoice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="dist/css/bootstrap-datepicker3.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Job Invoice
        </h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">Job Invoice</li>
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


                        <div id="divError" runat="server" class="alert alert-warning">
                            <span class="ui-icon ui-icon-alert" style="float: left; margin-right: .3em;"></span>
                            <strong>Warning: </strong>
                            <asp:Label ID="lblError" runat="server"></asp:Label>
                        </div>
                        <div id="MessageDiv" runat="server" class="alert alert-success">

                            <strong>Message: </strong>
                            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                            <asp:HiddenField ID="hdnestimateID" runat="server" />
                            <asp:HiddenField ID="hdnmaxestimateID" runat="server" />

                        </div>
                        <div id="divContent" runat="server">
                            <div class="inner-Content-container">
                                <div class="InnerContentData">
                                    <asp:Panel runat="server" ID="pnlforKAM">
                                        <div class="form-group col-lg-12">
                                            <div class="col-lg-12">

                                                <span class="col-lg-4"><b>Job Card No</b></span>
                                                <span class="col-lg-6">
                                                    <asp:Label ID="lblJobCardNo" runat="server" Text=""></asp:Label></span>
                                            </div>
                                            <div class="col-lg-12">
                                                <br />
                                                <span class="col-lg-4"><b>Project Name</b></span>
                                                <span class="col-lg-6">
                                                    <%--<asp:Label ID="lblProjectName" runat="server" Text=""></asp:Label>--%>
                                                    <asp:TextBox ID="lblProjectName" CssClass="form-control input-small" runat="server"></asp:TextBox>

                                                </span>
                                            </div>

                                            <div class="col-lg-12">
                                                <br />
                                                <span class="col-lg-4"><b>Profatibility (in %)</b></span>
                                                <span class="col-lg-6">
                                                    <asp:Label ID="txtProfatibility" runat="server" Text=""></asp:Label></span>
                                            </div>

                                            <div class="col-lg-12">
                                                <br />
                                                <span class="col-lg-4"><b>Execution Dates</b></span>
                                                <span class="col-lg-6">
                                                    <asp:Label ID="txtExecutionDate" Visible="false" runat="server" Text=""></asp:Label>
                                                    <asp:TextBox ID="txtExecutionDatefrom" CssClass="form-control input-small" Width="200" runat="server"></asp:TextBox>&nbsp;To&nbsp;
                                <asp:TextBox ID="txtExecutionDateTo" CssClass="form-control input-small" Width="200" runat="server"></asp:TextBox>
                                                </span>
                                            </div>

                                            <div class="col-lg-12">
                                                <br />
                                                <span class="col-lg-4"><b>Total Billing Amount (in Rs.)</b></span>
                                                <span class="col-lg-6">
                                                    <asp:TextBox ID="txtBillingAmount" onkeyup="javascript:checkvalues(this)" runat="server" Text="" CssClass="form-control input-small"></asp:TextBox></span>
                                            </div>

                                            <div class="col-lg-12">
                                                <br />
                                                <span class="col-lg-4"><b>Expenses (in Rs.)</b></span>
                                                <span class="col-lg-6">
                                                    <asp:TextBox ID="txtExpenses" onkeyup="javascript:checkvalues(this)" runat="server" Text="" CssClass="form-control input-small"></asp:TextBox></span>
                                            </div>
                                            <div class="col-lg-12">
                                                <br />
                                                <span class="col-lg-4"><b>Agency Fees (in Rs.)</b></span>
                                                <span class="col-lg-6">
                                                    <asp:TextBox ID="txtAgencyFees" onkeyup="javascript:checkvalues(this)" runat="server" Text="" CssClass="form-control input-small"></asp:TextBox></span>
                                            </div>

                                            <div class="col-lg-12">
                                                <br />
                                                <span class="col-lg-4"><b>Service Tax (this amount is included in the Total billing amount)</b></span>
                                                <span class="col-lg-6">
                                                    <asp:TextBox onkeyup="javascript:checkvalues(this)" ID="txtServiceTax" runat="server" Text="" CssClass="form-control input-small"></asp:TextBox></span>
                                            </div>


                                            <div class="col-lg-12">
                                                <br />
                                                <span class="col-lg-4"><b>Client Billing Address </b></span>
                                                <span class="col-lg-6">
                                                    <%--<asp:Label ID="lblClientBilling" Visible="false"  runat="server" Text=""></asp:Label>--%>
                                                    <asp:TextBox ID="lblClientBilling" runat="server" CssClass="form-control input-small" TextMode="MultiLine"></asp:TextBox>
                                                </span>
                                            </div>
                                            <div class="col-lg-12">
                                                <br />
                                                <span class="col-lg-4"><b>Client Name to whom the invoice has to be addressed (wherever applicable)</b></span>
                                                <span class="col-lg-6">
                                                    <%--<asp:Label ID="lblClientName" runat="server" Text=""></asp:Label>--%>
                                                    <asp:TextBox ID="lblClientName" runat="server" CssClass="form-control input-small"></asp:TextBox>
                                                </span>
                                            </div>
                                            <div class="col-lg-12">
                                                <br />
                                                <span class="col-lg-4"><b>PO#</b></span>
                                                <span class="col-lg-6">
                                                    <asp:TextBox ID="lblPONo" runat="server" CssClass="form-control input-small"></asp:TextBox>
                                                    <%--<asp:Label ID="lblPONo" runat="server" Text=""></asp:Label>--%>

                                                </span>
                                            </div>

                                            <div class="col-lg-12" style="padding-top: 20px;">
                                                <span class="col-lg-4"><b>Upload Docs</b></span>
                                                <span class="col-lg-3">
                                                    <asp:FileUpload ID="fupl_Mail" runat="server" onchange="javascript:return CheckUpl();" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="fupl_Mail" runat="server" SetFocusOnError="true" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    <%--<asp:HyperLink ID="lblfulpl_Mail" runat="server" Text="Download"  Target="_blank"></asp:HyperLink>--%>
                                                    <%--<asp:Button ID="lblfulpl_Mail" runat="server"  Text="Download" />--%>
                                                    <asp:LinkButton ID="lblfulpl_Mail" runat="server" ValidationGroup="sdada">Download</asp:LinkButton>
                                                    <asp:HiddenField ID="hdnlblfulpl_Mail" runat="server" />
                                                </span>
                                                <span class="col-lg-3">(If more than one file please make a zip file and upload.)
                                                </span>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                    <asp:Panel runat="server" ID="pnlforFM">
                                        <div class="form-group col-lg-12">
                                            <div class="col-lg-12">
                                                <br />
                                                <span class="col-lg-4"><b>Job Card No</b></span>
                                                <span class="col-lg-6">
                                                    <asp:Label ID="lblJobCardNoFM" runat="server" Text=""></asp:Label></span>
                                            </div>
                                            <div class="col-lg-12">
                                                <br />
                                                <span class="col-lg-4"><b>Project Name</b></span>
                                                <span class="col-lg-6">
                                                    <asp:Label ID="lblProjectNameFM" runat="server" Text=""></asp:Label></span>
                                            </div>

                                            <div class="col-lg-12">
                                                <br />
                                                <span class="col-lg-4"><b>Profatibility (in %)</b></span>
                                                <span class="col-lg-6">
                                                    <asp:Label ID="lblProfatibility" runat="server" Text=""></asp:Label></span>
                                            </div>

                                            <div class="col-lg-12">
                                                <br />
                                                <span class="col-lg-4"><b>Execution Dates</b></span>
                                                <span class="col-lg-6">
                                                    <asp:Label ID="lblExecutionDate" runat="server" Text=""></asp:Label></span>
                                            </div>

                                            <div class="col-lg-12">
                                                <br />
                                                <span class="col-lg-4"><b>Total Billing Amount (in Rs.)</b></span>
                                                <span class="col-lg-6">
                                                    <asp:Label ID="lblBillingAmount" runat="server" Text=""></asp:Label></span>
                                            </div>

                                            <div class="col-lg-12">
                                                <br />
                                                <span class="col-lg-4"><b>Expenses (in Rs.)</b></span>
                                                <span class="col-lg-6">
                                                    <asp:Label ID="lblExpenses" runat="server" Text=""></asp:Label></span>
                                            </div>
                                            <div class="col-lg-12">
                                                <br />
                                                <span class="col-lg-4"><b>Agency Fees (in Rs.)</b></span>
                                                <span class="col-lg-6">
                                                    <asp:Label ID="lblAgencyFees" runat="server" Text=""></asp:Label></span>
                                            </div>
                                            <div class="col-lg-12">
                                                <br />
                                                <span class="col-lg-4"><b>Service Tax (this amount is included in the Total billing amount) @ 12.36%</b></span>
                                                <span class="col-lg-6">
                                                    <asp:Label ID="lblServiceTax" runat="server" Text=""></asp:Label></span>
                                            </div>


                                            <div class="col-lg-12">
                                                <br />
                                                <span class="col-lg-4"><b>Client Billing Address </b></span>
                                                <span class="col-lg-6">
                                                    <asp:Label ID="lblClientBillingFM" runat="server" Text=""></asp:Label></span>
                                            </div>
                                            <div class="col-lg-12">
                                                <br />
                                                <span class="col-lg-4"><b>Client Name to whom the invoice has to be addressed (wherever applicable)</b></span>
                                                <span class="col-lg-6">
                                                    <asp:Label ID="lblClientNameFM" runat="server" Text=""></asp:Label></span>
                                            </div>
                                            <div class="col-lg-12" style="visibility: hidden;">
                                                <br />
                                                <span class="col-lg-4"><b>PO#</b></span>
                                                <span class="col-lg-6">
                                                    <asp:Label ID="lblPONoFM" runat="server" Text=""></asp:Label></span>
                                            </div>


                                        </div>
                                    </asp:Panel>
                                    <asp:Panel ID="Panelfinace" runat="server">
                                        <div style="padding-left: 10px;">
                                            <div class="col-lg-12">
                                                <br />
                                                <span class="col-lg-4"><b>Sent to Client</b></span>
                                                <span class="col-lg-6">
                                                    <asp:DropDownList ID="ddlSenttoclient" runat="server" CssClass="form-control input-small">
                                                        <asp:ListItem Value="No">No</asp:ListItem>
                                                        <asp:ListItem Value="Yes">Yes</asp:ListItem>

                                                    </asp:DropDownList></span>
                                            </div>
                                            <div class="col-lg-12">
                                                <br />
                                                <span class="col-lg-4"><b>Payment Received Date</b></span>
                                                <span class="col-lg-6">
                                                    <asp:TextBox ID="txtpaymentReceiveDate" onkeypress="return false" onkeyup="return false" onkeydown="return false" runat="server" Text="" CssClass="form-control input-small"></asp:TextBox>
                                                    <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtpaymentReceiveDate" ValidationGroup="fm" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>


                                                </span>
                                            </div>
                                            <div class="col-lg-12">
                                                <br />
                                                <span class="col-lg-4"><b>Payment Received Amount</b></span>
                                                <span class="col-lg-6">
                                                    <asp:TextBox ID="txtpaymentReceiveAmount" MaxLength="15" onkeyup="javascript:checkvalues(this)" runat="server" Text="0" CssClass="form-control input-small"></asp:TextBox>
                                                    <asp:RequiredFieldValidator runat="server" ID="RFV_amount" ControlToValidate="txtpaymentReceiveAmount" ValidationGroup="fm" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>

                                                </span>

                                            </div>
                                            <div class="col-lg-12">
                                                <br />
                                                <span class="col-lg-4"><b>Invoice Number</b></span>
                                                <span class="col-lg-6">
                                                    <asp:TextBox ID="txtInvoicenumber" MaxLength="30" runat="server" Text="" CssClass="form-control input-small"></asp:TextBox>
                                                    <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtInvoicenumber" ValidationGroup="fm" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>

                                                </span>

                                            </div>

                                        </div>
                                    </asp:Panel>

                                    <div class="btn-group col-lg-12 text-center">
                                        <div style="display: none;">
                                            <asp:Button ID="btnCancel" runat="server" Text="Back" CssClass="btn btn-primary" CausesValidation="false" Visible="false" />
                                        </div>
                                        <%-- <a href='javascript:history.go(-1)' class="btn btn-primary">Back</a>
                    <asp:Button ID="btnsave" runat="server" Text="Save" CssClass="btn btn-primary" CausesValidation="false" />
                    <asp:Button ID="btnEdit" runat="server" Text="Save" CssClass="btn btn-primary" CausesValidation="false" />--%>

                                        <asp:Button ID="btnsave" runat="server" Text="Save" CssClass="btn btn-primary" CausesValidation="false" />
                                        <asp:Button ID="btnEdit" runat="server" Text="Update" CssClass="btn btn-primary" CausesValidation="false" />
                                        <asp:Button ID="btnEditforFM" runat="server" Text="Save" ValidationGroup="fm" CssClass="btn btn-primary" />

                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <asp:HiddenField ID="hdnJobCardID" runat="server" />
    <asp:HiddenField ID="hdnNotificationID" runat="server" />
    <asp:HiddenField ID="hdnBrief" runat="server" />
    <asp:HiddenField runat="server" ID="hdnClientID" />
    <asp:HiddenField runat="server" ID="hdnjobinvoiceID" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpFooter" runat="Server">
    <script src="dist/js/bootstrap-datepicker.js"></script>
    <script>
        $(function () {
            $('input[id$=txtpaymentReceiveDate]').datepicker({ format: 'dd-mm-yyyy', autoclose: true });
        });
        function checkvalues(obj) {
            var varid = obj.id
            if ($('#' + varid).val() != "") {

                var numberRegex = /^\d{1,10}(\.\d{0,2})?$/;
                if (numberRegex.test($('#' + varid).val())) {

                }
                else {
                    alert('Enter numeric data only');
                    $('#' + varid).val("");

                }
            }
        }

        function CheckUpl() {
            var filename = $('input[id$=fupl_Mail]').val();
            var fileInput = $('input[id$=fupl_Mail]')[0];
            var sizeinbytes = fileInput.files[0].size;

            var fSExt = new Array('Bytes', 'KB', 'MB', 'GB');
            fSize = sizeinbytes;
            i = 0;
            while (fSize > 900) {
                fSize /= 1024; i++;
            }

            if (sizeinbytes >= 10485760) {

                $('input[id$=fupl_Mail]').val("");
                //$('input[id$=txtUploads]').val("");
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
                $('input[id$=fupl_Mail]').val("");
                // $('input[id$=FUpld_Documents]').val("");
            }
            else {
                //$('input[id$=FUpld_Documents]').val(onlyname);

            }
        }
    </script>
    <script type="text/javascript">
        $(function DatePicker(obj1) {
            //$('input[id$=' + obj1.id + ']').datepicker({ dateFormat: 'dd-mm-yyyy' });
            $('input[id$=txtExecutionDatefrom]').datepicker({ format: 'dd-mm-yyyy' });
            $('input[id$=txtExecutionDateTo]').datepicker({ format: 'dd-mm-yyyy' });
        });
    </script>

    <style type="text/css">
        .auto-style2 {
            width: 48%;
        }

        .auto-style3 {
            width: 49%;
        }
    </style>
</asp:Content>
