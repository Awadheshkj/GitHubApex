<%@ Page Title="" Language="VB" MasterPageFile="~/Apex.master" AutoEventWireup="false" ValidateRequest="false" CodeFile="RembursementClaimPMReject.aspx.vb" Inherits="RembursementClaimApproval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <%-- <h1>Leads
           
        </h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">Leads</li>
        </ol>--%>
    </section>

    <!-- Main content -->
    <section class="content">
        <div class="row">
            <div class="col-xs-12">
                <!-- .box -->
                <div class="box">
                    <div class="box-header">
                        <h3 class="box-title">Reimbursment Claim</h3>
                        <%--<a class="pull-right btn btn-primary btn-sm" href="LeadManager.aspx?mode=add"><i class="fa fa-th"></i>&nbsp;Add Lead</a>--%>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                        <div id="divToPrint">

                            <div class="subMenuStrip"></div>
                            <h3>Claims</h3>

                            <div id="divError" runat="server" class="ui-state-error ui-corner-all divError">
                                <p>
                                    <span class="ui-icon ui-icon-alert" style="float: left; margin-right: .3em;"></span>
                                    <strong>Warning: </strong>
                                    <asp:Label ID="lblError" runat="server"></asp:Label>
                                </p>
                            </div>

                            <div class="alert alert-success" id="MessageDiv" visible="false" runat="server">
                                <strong>Message: </strong>
                                <asp:Label ID="Label2" runat="server" Text=""></asp:Label>

                            </div>

                            <div id="divContent" runat="server">
                                <div class="inner-Content-container">
                                    <div class="InnerContentData">
                                        <div class="col-lg-12">
                                            <div class="form-group col-lg-12">
                                                <span class="col-lg-2">Job Code</span>
                                                <span class="col-lg-6">
                                                    <asp:Label ID="lblJobCode" runat="server"></asp:Label></span>
                                            </div>
                                            <div class="form-group col-lg-12">
                                                <span class="col-lg-2">Task</span>
                                                <span class="col-lg-6">
                                                    <asp:Label ID="lblTaskTitle" runat="server"></asp:Label></span>
                                            </div>
                                            <%-- <div class="form-group col-lg-12">
                        <span class="col-lg-2">Total Budget</span>
                        <span class="col-lg-2">Total Requested Amt.</span>
                        <span class="col-lg-2">Total Approval Amt.</span>
                        <span class="col-lg-2">Total Pending Approvals</span>

                    </div>--%>
                                            <div class="col-lg-12">
                                                <asp:GridView runat="server" ID="GridView1" AutoGenerateColumns="false" Width="90%" AllowPaging="true" PageSize="15" CssClass="table table-bordered">
                                                    <Columns>
                                                        <asp:BoundField HeaderText="PM Budget For Task" DataField="TotalB" />
                                                        <asp:BoundField HeaderText="Total Requested Amt." DataField="RequestedT" />
                                                        <asp:BoundField HeaderText="Total Approved Amt." DataField="ApprovedB" />
                                                        <asp:BoundField HeaderText="Total Pending Approvals" DataField="PendingB" />
                                                        <%--<asp:BoundField HeaderText="Allowted Budget" DataField="PendingB" />--%>
                                                    </Columns>

                                                </asp:GridView>

                                                <span class="col-lg-2" style="text-align: center;">
                                                    <asp:Label ID="lbltB" runat="server" Text="0" Visible="false"></asp:Label></span>
                                                <span class="col-lg-2" style="text-align: center;">
                                                    <asp:Label ID="lblTotalRequestedamt" runat="server" Visible="false" Text="0"></asp:Label></span>
                                                <span class="col-lg-2" style="text-align: center;">
                                                    <asp:Label ID="lblTotalApproval" runat="server" Visible="false" Text="0"></asp:Label></span>
                                                <span class="col-lg-2" style="text-align: center;">
                                                    <asp:Label ID="lblTotalPendingApprovals" Visible="false" runat="server" Text="0"></asp:Label></span>
                                                <asp:HiddenField ID="hdntotalpmallow" runat="server" />
                                                <asp:HiddenField ID="hdntotal" runat="server" />
                                            </div>
                                            <div style="float: right; width: 20%; padding-right: 5%">
                                                <asp:LinkButton ID="lnkshowhide" Font-Bold="true" Font-Size="16px" runat="server" Text="- Hide Details"></asp:LinkButton>
                                            </div>
                                            <div id="divshow" runat="server" visible="true">
                                                <h3>Details :-</h3>
                                                <div class="col-lg-12">
                                                    <asp:GridView runat="server" ID="gdvClaims" AutoGenerateColumns="false" Width="90%" AllowPaging="true" PageSize="15" CssClass="table table-bordered">


                                                        <Columns>
                                                            <asp:TemplateField HeaderText=" S.No.">
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:BoundField HeaderText="EXP_Category" DataField="Exp_Type" />
                                                            <asp:BoundField HeaderText="Claim Prepration Date" DataField="Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                                            <asp:BoundField HeaderText="Description" DataField="Description" />
                                                        <%--    <asp:BoundField HeaderText="Hotel" DataField="Hotel" />
                                                            <asp:BoundField HeaderText="No of Days" DataField="Days" />
                                                            <asp:BoundField HeaderText="Qty" DataField="Qty" />
                                                            <asp:BoundField HeaderText="Rate" DataField="Rate" />
                                                            <asp:BoundField HeaderText="From (city)" DataField="From_City" />
                                                            <asp:BoundField HeaderText="To (city)" DataField="Tocity" />--%>
                                                            <asp:BoundField HeaderText="Employee/Vendor Name" DataField="VenderName" />
                                                            <%--<asp:BoundField HeaderText="Amount" DataField="Amount" />--%>
                                                            <asp:TemplateField HeaderText="Amount">
                                                                <ItemTemplate>
                                                                    <asp:HiddenField ID="hdnid" runat="server" Value='<%# Eval("ClaimTransactionID")%>' />
                                                                    <asp:Label ID="lblamt" runat="server" Text='<%# Eval("Amount")%>'></asp:Label>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="Remarks" DataField="Remarks" />
                                                            <asp:TemplateField HeaderText="PM Approve Amt">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtapprovalamt" Width="70" AutoPostBack="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "RequestAmt")%>' Enabled="false" OnTextChanged="txtapprovalamt_TextChanged1"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Sent to KAM Approval" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtapprovalamtKam" Width="70" runat="server" Text='0.00' Enabled="true" AutoPostBack="true" OnTextChanged="txtapprovalamt_TextChanged1"></asp:TextBox>
                                                            
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            
                                                        </Columns>
                                                        
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                            <div class="form-group col-lg-12">
                                                <div class="col-lg-12">
                                                    <div id="trUpload2" runat="server" class="col-lg-12">
                                                        <asp:GridView ID="gvFileUploads" runat="server" Width="80%" AutoGenerateColumns="false" CssClass="table table-bordered">
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
                                                                        <a href="<%# DataBinder.Eval(Container.DataItem, "CollateralPath")%>" target="_blank">
                                                                            <input type="button" class="btn-small btn-info" value="Download" /></a>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group col-lg-12">
                                                <span class="form-2">Documents</span>
                                                <span class="form-6">
                                                    <asp:HyperLink ID="lblfulpl_Mail" runat="server" Text="Download" Target="_blank"></asp:HyperLink></span>
                                            </div>
                                            <div class="form-group col-lg-12">
                                                <span class="col-lg-2">Remarks given by claimant</span>
                                                <span class="col-lg-6">
                                                    <asp:Label ID="lblSRemarks" runat="server"></asp:Label></span>
                                            </div>
                                            <div class="form-group col-lg-12">
                                                <span class="col-lg-2">Remarks</span>
                                                <span class="col-lg-6">
                                                    <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox></span>
                                            </div>
                                            <div class="col-lg-12" style="text-align: center; display: none;">
                                                <asp:Button ID="btnCancel" runat="server" Visible="false" Text="Back" CssClass="btn btn-primary" />
                                                <%--<asp:Button ID="btnsend" runat="server" Text="Send" CssClass="btn btn-primary" />--%>
                                                <asp:Button ID="btnApproval" runat="server" Text="Approve" CssClass="btn btn-primary" />
                                                <asp:Button ID="btnExceed" runat="server" Text="Request to Exceed Amount" Visible="false" CssClass="btn btn-primary" />
                                            </div>
                                            <div class="col-lg-12" style="text-align: center;">
                                                <asp:Button ID="btnRejected" runat="server" Text="Cancel" CssClass="btn btn-primary" OnClientClick="javascript:return CancelConfirmationBox()" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <asp:HiddenField ID="hdnapprovalAmt" runat="server" />
    <asp:HiddenField ID="hdnNotificationID" runat="server" />
    <asp:HiddenField ID="hdnType" runat="server" />
    <asp:HiddenField ID="hdnID" runat="server" />
    <asp:HiddenField ID="hdnTaskID" runat="server" />
    <asp:HiddenField ID="hdnSubTaskID" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpFooter" runat="Server">
    <script type="text/javascript" src="Scripts/tiny_mce/tiny_mce.js"></script>
    <script type="text/javascript">
        tinyMCE.init({
            mode: "textareas",
            theme: "simple"
        });
    </script>

    <script type="text/javascript">
        function PrintDiv() {
            var divToPrint = document.getElementById('divToPrint');
            var popupWin = window.open('', '_blank', 'width=300,height=300');
            popupWin.document.open();
            popupWin.document.write('<html><body onload="window.print()">' + divToPrint.innerHTML + '</html>');
            popupWin.document.close();
        }

        function caltotal2(obj) {
            var val1 = false;
            var val2 = false;
            var varid = obj.id
            var stringArray = new Array();
            stringArray = varid.split("_");


            var postfix = stringArray[stringArray.length - 1];
            postfix = "_" + postfix
            if ($('input[id$=txtQuantity' + postfix + ']').val() != "") {

                var numberRegex = /^\d{1,10}(\.\d{0,2})?$/;
                if (numberRegex.test($('input[id$=txtQuantity' + postfix + ']').val())) {
                    val1 = true


                }
                else {
                    alert('Enter numeric data only');
                    $('input[id$=txtQuantity' + postfix + ']').val("");
                    val1 = false;
                }
            }


            if ($('input[id$=txtAmount' + postfix + ']').val() != "") {

                var numberRegex = /^\d{1,10}(\.\d{0,2})?$/;
                if (numberRegex.test($('input[id$=txtAmount' + postfix + ']').val())) {
                    val2 = true;
                }
                else {
                    alert('Enter numeric data only');
                    $('input[id$=txtAmount' + postfix + ']').val("");
                    val2 = false;
                }
            }

            if (val1 == true && val2 == true) {
                var qty = parseFloat($('input[id$=txtQuantity' + postfix + ']').val());
                var amt = parseFloat($('input[id$=txtAmount' + postfix + ']').val());

                var total = qty * amt;
                $('span[id$=txtTotal' + postfix + ']').val(total.toFixed(2));
                $('span[id$=txtTotal' + postfix + ']').text(total.toFixed(2));

            }
        }
    </script>

</asp:Content>

