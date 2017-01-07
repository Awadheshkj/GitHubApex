<%@ Page Title="" Language="VB" MasterPageFile="~/Apex.master" AutoEventWireup="false" ValidateRequest="false" CodeFile="RembursementClaimApprovalView.aspx.vb" Inherits="RembursementClaimApproval" %>

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
                        <div style="float: right; padding-top: 40px; width: 220px;">
                            <%--<input type="button" value="print" onclick="PrintDiv();" />--%>
                            <%--<a href="#" target="_blank" onclick="PrintDiv()" >--%>
                            <span class="btn btn-sm btn-primary" onclick="PrintDiv();"><i class="fa fa-print"></i>&nbsp;Print</span>
                            <%--<img src="Images/print_button_icon1.png" style="cursor:pointer" />--%>
                            <%--            </a>--%>
                        </div>
                        <div id="divToPrint">

                            <div class="subMenuStrip"></div>
                            <h3><u>Claim Sheet</u><asp:Label ID="lblclaimID" runat="server" Text=""></asp:Label></h3>

                            <div id="divError" runat="server" class="ui-state-error ui-corner-all divError">
                                <p>
                                    <span class="ui-icon ui-icon-alert" style="float: left; margin-right: .3em;"></span>
                                    <strong>Warning: </strong>
                                    <asp:Label ID="lblError" runat="server"></asp:Label>
                                </p>
                            </div>

                            <div id="divContent" runat="server">
                                <div class="inner-Content-container">
                                    <div class="InnerContentData">
                                        <div class="col-lg-12">
                                            <div class="form-group col-lg-12">
                                                <span class="col-lg-2">Claim Status :</span>
                                                <span class="col-lg-6">
                                                    <asp:Label ID="lblclaimstatus" Font-Bold="true" Font-Size="16px" runat="server"></asp:Label></span>
                                            </div>
                                            <div class="form-group col-lg-12">
                                                <span class="col-lg-2">Employee Name :</span>
                                                <span class="col-lg-6">
                                                    <asp:Label ID="lblEmployeeName" runat="server"></asp:Label></span>
                                            </div>
                                            <div class="form-group col-lg-12">
                                                <span class="col-lg-2">Employee Code :</span><span class="col-lg-6"><asp:Label ID="lblEmployeeCode" runat="server"></asp:Label></span>
                                            </div>
                                            <div class="form-group col-lg-12">
                                                <span class="col-lg-2">Expense Period /Month :</span>
                                                <span class="col-lg-6">
                                                    <asp:TextBox ID="txtExpensePeriodfrom" Enabled="false" runat="server"></asp:TextBox>&nbsp; TO &nbsp;
                            <asp:TextBox ID="txtExpensePeriodto" Enabled="false" runat="server"></asp:TextBox>

                                                </span>
                                            </div>
                                            <div class="form-group col-lg-12">
                                                <span class="col-lg-2">Expense Place&nbsp; :</span><span class="col-lg-6"><asp:TextBox ID="txtExpensePlace" runat="server" Enabled="false"></asp:TextBox>

                                                </span>
                                            </div>

                                            <div class="form-group col-lg-12">
                                                <span class="col-lg-2">Job Code :</span>
                                                <span class="col-lg-6">
                                                    <asp:Label ID="lblJobCode" runat="server"></asp:Label></span>
                                            </div>

                                             <div class="form-group col-lg-12" >
                                            <span class="col-lg-2">Project Name</span>
                                            <span class="col-lg-6">
                                                <asp:Label ID="lblprojectName" runat="server"></asp:Label></span>
                                            
                                        </div>
                                            <div class="form-group col-lg-12">
                                                <span class="col-lg-2">KAM :</span>
                                                <span class="col-lg-6">
                                                    <asp:Label ID="lblKAM" runat="server"></asp:Label></span>
                                            </div>
                                            <div class="form-group col-lg-12">
                                                <span class="col-lg-2">Project Manager :</span>
                                                <span class="col-lg-6">
                                                    <asp:Label ID="lblManager" runat="server"></asp:Label></span>
                                            </div>

                                            <div class="form-group col-lg-12">
                                                <span class="col-lg-2">Contact Number :</span>
                                                <span class="col-lg-6">
                                                    <asp:Label ID="lblContactNumber" runat="server"></asp:Label></span>
                                            </div>
                                            <div class="form-group col-lg-12">
                                                <span class="col-lg-2">E-Mail ID :</span>
                                                <span class="col-lg-6">
                                                    <asp:Label ID="lblEMailID" runat="server"></asp:Label></span>
                                            </div>


                                            <div class="form-group col-lg-12" style="display: none;">
                                                <span class="col-lg-2">Task</span>
                                                <span class="col-lg-6">
                                                    <asp:Label ID="lblTaskTitle" runat="server"></asp:Label></span>
                                            </div>
                                            <div class="form-group col-lg-12" style="display: none;">
                                                <span class="col-lg-2">Task Particulars</span>
                                                <span class="col-lg-6">
                                                    <asp:Label ID="lblparticulars" runat="server"></asp:Label></span>
                                            </div>
                                            <div class="form-group col-lg-12" style="display: none;">
                                                <span class="col-lg-2">Amount</span>
                                                <span class="col-lg-6">
                                                    <asp:Label ID="lblAmount" runat="server"></asp:Label></span>
                                            </div>

                                            <%-- <div class="form-group col-lg-12">
                        <span class="col-lg-2">Total Budget</span>
                        <span class="col-lg-2">Total Requested Amt.</span>
                        <span class="col-lg-2">Total Approval Amt.</span>
                        <span class="col-lg-2">Total Pending Approvals</span>

                    </div>--%>
                                            <br />
                                            <div class="col-lg-12">
                                                <asp:GridView runat="server" ID="GridView1" AutoGenerateColumns="false" Width="90%" AllowPaging="true" PageSize="15" CssClass="table table-bordered">
                                                    <Columns>
                                                        <asp:BoundField HeaderText="Total Budget" DataField="TotalB" />
                                                        <asp:BoundField HeaderText="Total Requested Amt." DataField="RequestedT" />
                                                        <asp:BoundField HeaderText="Total Approved Amt." DataField="ApprovedB" />
                                                        <asp:BoundField HeaderText="Total Pending Approvals" DataField="PendingB" />

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
                                            </div>
                                            <div style="float: right; width: 20%; padding-right: 5%">
                                                <asp:LinkButton ID="lnkshowhide" Font-Bold="true" Font-Size="16px" runat="server" Text=""></asp:LinkButton>
                                            </div>
                                            <div id="divshow" runat="server" visible="true">
                                                <h3>Details :-</h3>
                                                <div class="col-lg-12">
                                                    <asp:GridView runat="server" ID="gdvClaims" AutoGenerateColumns="false" Width="90%" ShowFooter="true" AllowPaging="true" PageSize="15" CssClass="table table-bordered">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="ID">
                                                                <ItemTemplate>
                                                                    <%# DataBinder.Eval(Container.Dataitem,"ID") %>
                                                                    <asp:HiddenField ID="hdnClaimID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "ClaimMasterID")%>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <%--  <asp:BoundField DataField="Totalbudget" HeaderText="Total Budget" />--%>
                                                            <%-- <asp:BoundField DataField="Allowbudget" HeaderText="Allowted Budget" />
                                        <asp:BoundField DataField="CategoryTask" HeaderText="CategoryTask" />
                                        <asp:BoundField DataField="Working Person" HeaderText="Task Manager" />

                                        <asp:TemplateField HeaderText="Particulars">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>


                                                            <asp:BoundField HeaderText="EXP_Category" DataField="Exp_Type" />
                                                            <asp:BoundField HeaderText="Claim Prepration Date" DataField="Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                                            <asp:BoundField HeaderText="Description" DataField="Description" />
                                                            <%--<asp:BoundField HeaderText="Hotel" DataField="Hotel" />
                                                            <asp:BoundField HeaderText="No of Days" DataField="Days" />
                                                            <asp:BoundField HeaderText="Qty" DataField="Qty" />
                                                            <asp:BoundField HeaderText="Rate" DataField="Rate" />
                                                            <asp:BoundField HeaderText="From (city)" DataField="From_City" />
                                                            <asp:BoundField HeaderText="To (city)" DataField="Tocity" />
                                                            --%>
                                                            <asp:TemplateField HeaderText="Employee/Vendor Name">
                                                                <ItemTemplate>
                                                                    <%# DataBinder.Eval(Container.DataItem, "VenderName")%>
                                                                </ItemTemplate>
                                                                <FooterTemplate>

                                                                    <asp:Label ID="Label6" runat="server" Font-Bold="true" Text="Total :"></asp:Label>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Amount">
                                                                <ItemTemplate>
                                                                    <%# DataBinder.Eval(Container.DataItem, "ApprovedAmt")%>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lbltotalamt" runat="server" Font-Bold="true" Font-Size="14px" Text=""></asp:Label>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="Remarks" DataField="Remarks" />
                                                            
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                            <div class="form-group col-lg-12">
                                                <div class="col-lg-12">
                                                    <div id="trUpload2" runat="server" class="col-lg-12" style="display: none">
                                                        <asp:GridView ID="gvFileUploads" runat="server" Width="80%" AutoGenerateColumns="false" Visible="false" CssClass="table table-bordered">
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
                                            <div class="form-group col-lg-12" style="display: none;">
                                                <span class="form-2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Documents</span>
                                                <span class="form-6">
                                                    <asp:HyperLink ID="lblfulpl_Mail" runat="server" Text="Download" Target="_blank"></asp:HyperLink></span>
                                            </div>
                                            <div class="form-group col-lg-12" style="display: none;">
                                                <span class="col-lg-2">Remarks given by claimant</span>
                                                <span class="col-lg-6">
                                                    <asp:Label ID="lblSRemarks" runat="server"></asp:Label></span>
                                            </div>
                                            <div class="form-group col-lg-12">
                                                <span class="col-lg-2"><%--Remarks--%></span>
                                                <span class="col-lg-6">
                                                    <asp:TextBox ID="txtRemarks" Visible="false" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox></span>
                                            </div>
                                            <div class="col-lg-12" style="text-align: center; display: none;">
                                                <asp:Button ID="btnCancel" runat="server" Text="Back" CssClass="btn btn-primary" />
                                                <asp:Button ID="btnApproval" runat="server" Text="Approve" CssClass="btn btn-primary" />
                                                <asp:Button ID="btnExceed" runat="server" Text="Request to Exceed Amount" CssClass="btn btn-primary" />

                                                <asp:Button ID="btnRejected" runat="server" Text="Reject" CssClass="btn btn-primary" OnClientClick="javascript:return RejectConfirmationBox()" />
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>

                            <div id="duplicateCopy" style="-webkit-transform: rotate(-0deg); -moz-transform: rotate(-0deg); -o-transform: rotate(-0deg); transform: rotate(-0deg); color: #6cc; font-weight: bold; letter-spacing: 100px; position: absolute; z-index: 1000; top: 5%; left: 18%; opacity: 0.4; filter: alpha(opacity=50); width: 700px; top: 150px; left: 350px;">
                                <img src="http://www.kestoneapps.com/Images/logo111.JPG" />
                            </div>
                            <%--<div id="duplicateCopy" style="-webkit-transform: rotate(-5deg); -moz-transform: rotate(-5deg); -o-transform: rotate(-5deg); transform: rotate(-5deg); color: #6cc; font-weight: bold; letter-spacing: 100px; position: absolute; z-index: 1000; top: 5%; left: 18%; opacity: 0.2; filter: alpha(opacity=50);  width:850px;">
            <!--chander mohan singh-->

            <img src="http://www.kestoneapps.com/Images/Kestone-Template-v1_012.jpg" />
            <img src="http://www.kestoneapps.com/Images/Kestone-Template-v1_012.jpg" />
             <img src="http://www.kestoneapps.com/Images/Kestone-Template-v1_012.jpg" />
            <img src="http://www.kestoneapps.com/Images/Kestone-Template-v1_012.jpg" />
             <img src="http://www.kestoneapps.com/Images/Kestone-Template-v1_012.jpg" />
            <img src="http://www.kestoneapps.com/Images/Kestone-Template-v1_012.jpg" />
             <img src="http://www.kestoneapps.com/Images/Kestone-Template-v1_012.jpg" />
            <img src="http://www.kestoneapps.com/Images/Kestone-Template-v1_012.jpg" />
             <img src="http://www.kestoneapps.com/Images/Kestone-Template-v1_012.jpg" />
            <img src="http://www.kestoneapps.com/Images/Kestone-Template-v1_012.jpg" />
             <img src="http://www.kestoneapps.com/Images/Kestone-Template-v1_012.jpg" />
             <img src="http://www.kestoneapps.com/Images/Kestone-Template-v1_012.jpg" />
            <img src="http://www.kestoneapps.com/Images/Kestone-Template-v1_012.jpg" />
             <img src="http://www.kestoneapps.com/Images/Kestone-Template-v1_012.jpg" />
            <img src="http://www.kestoneapps.com/Images/Kestone-Template-v1_012.jpg" />
             <img src="http://www.kestoneapps.com/Images/Kestone-Template-v1_012.jpg" />
            <img src="http://www.kestoneapps.com/Images/Kestone-Template-v1_012.jpg" />
            <img src="http://www.kestoneapps.com/Images/Kestone-Template-v1_012.jpg" />
            <img src="http://www.kestoneapps.com/Images/Kestone-Template-v1_012.jpg" />
             <img src="http://www.kestoneapps.com/Images/Kestone-Template-v1_012.jpg" />
            <img src="http://www.kestoneapps.com/Images/Kestone-Template-v1_012.jpg" />
             <img src="http://www.kestoneapps.com/Images/Kestone-Template-v1_012.jpg" />
            <img src="http://www.kestoneapps.com/Images/Kestone-Template-v1_012.jpg" />
            <img src="http://www.kestoneapps.com/Images/Kestone-Template-v1_012.jpg" />
            <img src="http://www.kestoneapps.com/Images/Kestone-Template-v1_012.jpg" />
             <img src="http://www.kestoneapps.com/Images/Kestone-Template-v1_012.jpg" />
            <img src="http://www.kestoneapps.com/Images/Kestone-Template-v1_012.jpg" />
             <img src="http://www.kestoneapps.com/Images/Kestone-Template-v1_012.jpg" />
            <img src="http://www.kestoneapps.com/Images/Kestone-Template-v1_012.jpg" />
            <img src="http://www.kestoneapps.com/Images/Kestone-Template-v1_012.jpg" />
           
        </div>--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <asp:HiddenField ID="hdnJobCardID" runat="server" />
    <asp:HiddenField ID="hdnNotificationID" runat="server" />
    <asp:HiddenField ID="hdnType" runat="server" />
    <asp:HiddenField ID="hdnID" runat="server" />
    <asp:HiddenField ID="hdnTaskID" runat="server" />
    <asp:HiddenField ID="hdnSubTaskID" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpFooter" runat="Server">
     <script src="dist/js/tiny_mce/tiny_mce.js"></script>

    <script type="text/javascript">
        tinyMCE.init({
            mode: "textareas",
            theme: "simple"
        });
    </script>
    <script src="js/jQuery.print.js"></script>
    <script type="text/javascript">

        function PrintDiv() {
            $("#divToPrint").print()
            //var divToPrint = document.getElementById('divToPrint');
            //var popupWin = window.open('', '_blank', 'width=300,height=300');
            //popupWin.document.open();
            //popupWin.document.write('<html><body onload="window.print()">' + divToPrint.innerHTML + '</html>');
            //popupWin.document.close();
        }

        $(document).ready(function () {
            getTotalEmployeeSalary()
        });

        function getTotalEmployeeSalary() {
            var sum = 0.00;
            $("#<%=gdvClaims.ClientID%> tr:has(td)").each(function () {

                if ($.trim($(this).find("td:eq(5)").text()) != "" && !isNaN($(this).find("td:eq(5)").text()))

                    sum = sum + parseInt($(this).find("td:eq(5)").text());
                $(this).addClass('highlightRow');
            });

            $("#<%=gdvClaims.ClientID%> [id*=lbltotalamt]").text(sum.toFixed(2));



        }
    </script>
</asp:Content>

