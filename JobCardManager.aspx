<%@ Page Title="" Language="VB" MasterPageFile="~/Apex.master" AutoEventWireup="false" CodeFile="JobCardManager.aspx.vb" Inherits="JobCardManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="dist/css/bootstrap-datepicker3.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Job Card Manager
           
        </h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">Job Card Manager</li>
        </ol>
    </section>

    <!-- Main content -->
    <section class="content">
        <div class="row">
            <div class="col-xs-12">
                <!-- .box -->
                <div class="box">
                    <div class="box-header">
                        <h3 class="box-title">List Of Leads</h3>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">

                        <div id="divError" runat="server" class="alert alert-danger divError">
                            <p>
                                <span class="ui-icon ui-icon-alert" style="float: left; margin-right: .3em;"></span>
                                <strong>Warning: </strong>
                                <asp:Label ID="lblError" runat="server"></asp:Label>
                            </p>
                        </div>
                        <div id="MessageDiv" runat="server" class="alert alert-success divError">
                            <p>

                                <strong>Message: </strong>
                                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                            </p>
                        </div>

                        <div id="divContent" runat="server">

                            <div class="modal fade" id="dialog-box-Brief">
                                <div class="modal-dialog">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                            <h4 class="modal-title">Modal title</h4>
                                        </div>
                                        <div class="modal-body">
                                            <div class="dialog-content">
                                                <div id="dialog-message" style="width: 450px;">
                                                    <h4>Brief Manager</h4>
                                                    <div style="width: 480px; height: 400px; overflow: auto;">
                                                        <table>
                                                            <tr>
                                                                <td class="tdhead" style="vertical-align: top;">Brief Name</td>
                                                                <td colspan="2">
                                                                    <asp:Label ID="lblBrief" runat="server"></asp:Label>

                                                                </td>

                                                            </tr>
                                                            <tr>
                                                                <td class="tdhead">Primary Activity</td>
                                                                <td colspan="2">
                                                                    <asp:Label ID="lblActivity" runat="server"></asp:Label>
                                                                    <asp:DropDownList Visible="false" ID="ddlActivity" runat="server" Width="150px" TabIndex="2"></asp:DropDownList>


                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="tdhead">Sub Activity</td>
                                                                <td colspan="2">
                                                                    <asp:Label ID="lblchklActivityType" runat="server"></asp:Label>

                                                                    <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatDirection="Horizontal" CssClass="CheckBoxList" TabIndex="3"></asp:CheckBoxList>

                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="tdhead">Activity Date</td>
                                                                <td colspan="2">
                                                                    <asp:Label ID="lblActivityDate" runat="server"></asp:Label>


                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="tdhead">Client</td>
                                                                <td colspan="2">
                                                                    <asp:Label ID="lblbrClient" runat="server"></asp:Label>

                                                                </td>
                                                            </tr>

                                                            <tr>
                                                                <td class="tdhead">Client Contact Person</td>
                                                                <td>
                                                                    <asp:Label ID="lblContactPerson" runat="server"></asp:Label>
                                                                    <asp:DropDownList Visible="false" ID="ddlContactPerson" runat="server" Width="150px" AutoPostBack="True" TabIndex="10"></asp:DropDownList>
                                                                </td>
                                                                <td style="font-size: 11px;">
                                                                    <asp:Label ID="Label2" runat="server"></asp:Label>
                                                                    <br />
                                                                    <asp:Label ID="Label3" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>

                                                            <tr>
                                                                <td class="tdhead" style="vertical-align: top; width: 120px;">Scope of work</td>
                                                                <td colspan="2">
                                                                    <asp:Label ID="lblScope" runat="server"></asp:Label>

                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="tdhead" style="vertical-align: top; width: 120px;">Target Audience
                                                                </td>
                                                                <td colspan="2">
                                                                    <asp:Label ID="lblTargetAudience" runat="server"></asp:Label>

                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="tdhead" style="vertical-align: top; width: 120px;">Measurement Matrix
                                                                </td>
                                                                <td colspan="2">
                                                                    <asp:Label ID="lblMeasurementMatrix" runat="server"></asp:Label>

                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="tdhead" style="vertical-align: top; width: 120px;">Activity Details</td>
                                                                <td colspan="2">
                                                                    <asp:Label ID="lblActivityDetails" runat="server"></asp:Label>

                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="tdhead" style="vertical-align: top; width: 120px;">Key Challenges for execution</td>
                                                                <td colspan="2">
                                                                    <asp:Label ID="lblKeyChallenges" runat="server"></asp:Label>

                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="tdhead" style="vertical-align: top; width: 120px;">Timeline for revert</td>
                                                                <td colspan="2">
                                                                    <asp:Label ID="lblTimeline" runat="server"></asp:Label>

                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="tdhead">Budget</td>
                                                                <td colspan="2">
                                                                    <asp:Label ID="lblBudget" runat="server"></asp:Label>


                                                                </td>
                                                            </tr>

                                                        </table>
                                                    </div>

                                                </div>
                                                <a href="#" class="button">Close</a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-lg-12">

                                <div class="panel panel-info">
                                    <div class="panel-heading clearfix">
                                        Brief Manager
                                        <button type="button" class="btn btn-primary btn-sm pull-right" data-toggle="modal" data-target="#dialog-box-Brief">View more</button>
                                    </div>
                                    <div class="panel-body">
                                        <div class="col-lg-3">
                                            <b>Brief Name : </b>
                                            <asp:Label runat="server" ID="lblbriefname" Text=""></asp:Label>
                                        </div>
                                        <div class="col-lg-3">
                                            <b>Client : </b>
                                            <asp:Label runat="server" ID="lblbriefClient" Text=""></asp:Label>
                                        </div>
                                        <div class="col-lg-3">
                                            <b>Primary Activity : </b>
                                            <asp:Label runat="server" ID="lblbriefPrimaryActivity" Text=""></asp:Label>
                                        </div>
                                        <div class="col-lg-3">
                                            <b>Budget : </b>
                                            <asp:Label runat="server" ID="lblbriefBudget" Text=""></asp:Label>
                                        </div>
                                    </div>

                                </div>
                            </div>

                            <div class="modal fade" id="dialog-box-PrePnl">
                                <div class="modal-dialog">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                            <h4 class="modal-title">Modal title</h4>
                                        </div>
                                        <div class="modal-body">
                                            <div class="dialog-content">
                                                <div id="dialog-message2">
                                                    <h4>Pre P&L Manager</h4>

                                                    <table id="tbl1" runat="server" width="99%">
                                                        <tr>
                                                            <td class="tdhead">Client Name</td>
                                                            <td colspan="3">
                                                                <asp:Label ID="lblplclient" runat="server"></asp:Label>


                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="tdhead">Activity Name</td>
                                                            <td colspan="3">
                                                                <asp:Label ID="lblplActivity" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="tdhead">Activity Date</td>

                                                            <td>
                                                                <asp:Label ID="lblplEventDate" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="tdhead">Activity Venue</td>
                                                            <td>
                                                                <asp:Label ID="lblplEventVenue" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="tdhead">PO No/Agreement Signed/Mail Approval</td>
                                                            <td colspan="3">
                                                                <asp:Label ID="lblplApprovalNo" runat="server"></asp:Label>

                                                                <asp:HyperLink ID="hypPrePL" runat="server" Text="Download" Target="_blank"></asp:HyperLink>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="tdhead">Credit Period with Client(in days)</td>
                                                            <td colspan="3">
                                                                <asp:Label ID="lblplCreditPeriod" runat="server"></asp:Label>
                                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ControlToValidate="txtCreditPeriod" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4">&nbsp;</td>
                                                        </tr>
                                                    </table>
                                                    <table id="tbl2" runat="server" style="width: 99%">
                                                        <tr>
                                                            <td colspan="4">

                                                                <asp:GridView ID="gv_prepnl" runat="server" AutoGenerateColumns="false" HeaderStyle-BackColor="#333333" Width="90%" HeaderStyle-ForeColor="White" PageSize="5">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="S.No.">
                                                                            <ItemTemplate>
                                                                                <%# Container.DataItemIndex + 1 %>
                                                                                <asp:HiddenField ID="hdnPrePnLCostID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "PrePnLCostID")%>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Category">
                                                                            <ItemTemplate>
                                                                                <%# DataBinder.Eval(Container.DataItem, "Category")%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Nature of Expenses">
                                                                            <ItemTemplate>
                                                                                <%# DataBinder.Eval(Container.DataItem, "NatureOfExpenses")%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Supplier Name">
                                                                            <ItemTemplate>
                                                                                <%# DataBinder.Eval(Container.DataItem, "SupplierName")%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Pre Event Cost">
                                                                            <ItemTemplate>
                                                                                <%# DataBinder.Eval(Container.DataItem, "PreEventCost")%>
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                                <asp:TextBox ID="gv_txtPreEventCost" runat="server" Text='<%#Eval("PreEventCost")%>' />
                                                                            </EditItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <%-- <asp:TemplateField HeaderText="Service Tax / VAT (in %)">
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "PreEventServiceTax")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total">
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "PreEventTotal")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td class="tdhead">Pre Event Quote</td>
                                                            <td colspan="3">
                                                                <asp:Label ID="lblplPreEventQuote" runat="server"></asp:Label>
                                                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*" ControlToValidate="txtPreEventQuote" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="tdhead">Total Pre Event Cost</td>
                                                            <td colspan="3">
                                                                <asp:Label ID="lblplTCost" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="tdhead"><%--Total Service Tax / VAT--%></td>
                                                            <td colspan="3">
                                                                <asp:Label Visible="false" ID="lblplTSTax" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="tdhead"><%--Pre Event Total--%></td>
                                                            <td colspan="3">
                                                                <asp:Label Visible="false" ID="lblplPETotal" runat="server"></asp:Label>


                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="tdhead">Pre Event Profit (in %)</td>
                                                            <td colspan="3">

                                                                <asp:Label ID="lblPreEPr" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>

                                                        <tr id="tr1" runat="server">
                                                            <td class="tdhead"></td>
                                                            <td colspan="3">
                                                                <asp:Label Visible="false" ID="lblplremarls" runat="server"></asp:Label>

                                                            </td>
                                                        </tr>


                                                    </table>

                                                </div>
                                                <a href="#" class="button">Close</a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-lg-12">
                                <div class="panel panel-warning">
                                    <div class="panel-heading clearfix">
                                        Pre P&L Manager
                                        <button type="button" class="btn btn-primary btn-sm pull-right" data-toggle="modal" data-target="#dialog-box-PrePnl">View more</button>
                                    </div>
                                    <div class="panel-body">
                                        <div class="col-lg-3">
                                            <b>Client : </b>
                                            <asp:Label runat="server" ID="lblClient" Text=""></asp:Label>
                                        </div>
                                        <div class="col-lg-3">
                                            <b>Project Manager : </b>
                                            <asp:Label runat="server" ID="lblPM" Text=""></asp:Label>
                                        </div>
                                        <div class="col-lg-3">
                                            <b>KAM : </b>
                                            <asp:Label runat="server" ID="lblKAM" Text=""></asp:Label>
                                        </div>
                                        <div class="col-lg-3" style="display: none;">
                                            <b>Pre Event Quote : </b>
                                            <asp:Label runat="server" ID="lblPEQ" Text=""></asp:Label>
                                        </div>
                                        <div class="col-lg-3">
                                            <b>Pre Event Total : </b>
                                            <asp:Label runat="server" ID="lblPreEventtotal" Text=""></asp:Label>
                                        </div>

                                    </div>

                                </div>
                            </div>

                            <div class="modal fade" id="dialog-box-estimate">
                                <div class="modal-dialog">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                            <h4 class="modal-title">Modal title</h4>
                                        </div>
                                        <div class="modal-body">
                                            <div class="dialog-content">
                                                <div id="dialog-message3">
                                                    <h4>Estimate</h4>


                                                    <table id="Table1" runat="server" style="width: 99%;">
                                                        <tr>
                                                            <td colspan="4">

                                                                <asp:GridView runat="server" ID="gvDisplay" AutoGenerateColumns="false" HeaderStyle-BackColor="#333333" HeaderStyle-ForeColor="White" Width="90%" AllowPaging="true" PageSize="15" ShowFooter="false">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="ID">
                                                                            <ItemTemplate>
                                                                                <%# Container.DataItemIndex + 1 %>
                                                                                <asp:HiddenField runat="server" ID="hdnEstimateID" Value='<%# Bind("EstimateID")%>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Category">
                                                                            <ItemTemplate>
                                                                                <%# DataBinder.Eval(Container.DataItem, "Category")%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Particulars">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblParticulars" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Particulars")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Quantity">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblQuantity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Quantity")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Unit Price">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblRate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Rate")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Days">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDays" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Days")%>'></asp:Label>
                                                                            </ItemTemplate>

                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Estimate">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblEstimate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Estimate") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Remarks">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblRemarks" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Remarks") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td class="tdhead">

                                                                <asp:Label runat="server" ID="lbl1">Sub Total :</asp:Label></td>
                                                            <td>
                                                                <asp:Label runat="server" ID="lblSubTotal"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td class="tdhead">Management Fees :(<asp:TextBox ID="txtMFeePer" Visible="false" runat="server" CssClass="small_textbox_grid" TabIndex="8" autocomplete="off" onkeyup="javascript:CalTotalMFeePer();"></asp:TextBox>
                                                                <asp:Label ID="lblMFeePer" runat="server"></asp:Label>%)</td>
                                                            <td>
                                                                <asp:Label ID="lblMangnmtFees" runat="server"></asp:Label>
                                                                <asp:TextBox Visible="false" runat="server" ID="txtMangnmtFees" onkeyup="javascript:CalTotal();" MaxLength="10" CssClass="textbox" autocomplete="off"></asp:TextBox>

                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="tdhead">
                                                                <asp:Label runat="server" ID="Label4">Total :</asp:Label></td>
                                                            <td>
                                                                <asp:Label runat="server" ID="lblTotal"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td class="tdhead">Service Tax / VAT : (<asp:TextBox Visible="false" ID="txtServiceTax" runat="server" TabIndex="9" CssClass="right_small_textbox_grid" onkeyup="CalTotal1();" autocomplete="off"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ErrorMessage="*" ControlToValidate="txtServiceTax" ForeColor="red"></asp:RequiredFieldValidator><asp:Label ID="lblServiceTaxPer" runat="server" SetFocusOnError="true"></asp:Label>
                                                                %) </td>
                                                            <td>
                                                                <asp:Label ID="lblServiceTax" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="tdhead">
                                                                <asp:Label runat="server" ID="Label5">Grand Total :</asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="lblGrandTotal"></asp:Label></td>

                                                        </tr>


                                                    </table>

                                                </div>
                                                <a href="#" class="button">Close</a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-lg-12">
                                <div class="panel panel-success">
                                    <div class="panel-heading clearfix">
                                        Estimate
                                        <button type="button" class="btn btn-primary btn-sm pull-right" data-toggle="modal" data-target="#dialog-box-estimate">View more</button>
                                    </div>
                                    <div class="panel-body">

                                        <div class="col-lg-3">
                                            <b>Client : </b>
                                            <asp:Label runat="server" ID="lblEstClient" Text=""></asp:Label>
                                        </div>

                                        <div class="col-lg-3">
                                            <b>Project Manager : </b>
                                            <asp:Label runat="server" ID="lblEstPM" Text=""></asp:Label>
                                        </div>


                                        <div class="col-lg-3">
                                            <b>KAM : </b>
                                            <asp:Label runat="server" ID="lblEstKAM" Text=""></asp:Label>
                                        </div>

                                        <div class="col-lg-3">
                                            <b>Sub Total : </b>
                                            <asp:Label runat="server" ID="lblEstSubtotal" Text=""></asp:Label>
                                        </div>
                                    </div>

                                </div>
                            </div>

                            <div class="col-lg-12">
                                &nbsp;
                            </div>
                            <div class="col-lg-12" id="jobcard">
                                <div class="col-lg-12">
                                    <div class="form-group col-lg-12">
                                        <span class="col-lg-2">Job Code*</span>
                                        <span class="col-lg-3">
                                            <asp:Label ID="lblJobCode" runat="server"></asp:Label>
                                            <asp:TextBox ID="txtJobCode" runat="server" CssClass="form-control" MaxLength="7" TabIndex="1"></asp:TextBox>
                                            <asp:RegularExpressionValidator ForeColor="Red" ID="RegularExpVal" ControlToValidate="txtJobCode" ValidationExpression="KIMS\d{7}|\d{7}" runat="server" ErrorMessage="Invalid"></asp:RegularExpressionValidator>
                                            <asp:RequiredFieldValidator ID="RfvJobCode" runat="server" ControlToValidate="txtJobCode" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </span>
                                        <span class="col-lg-2">Project Name*</span>
                                        <span class="col-lg-3">
                                            <asp:Label ID="lblProjectName" runat="server"></asp:Label>
                                            <asp:TextBox ID="txtProjectName" runat="server" CssClass="form-control" MaxLength="40" TabIndex="1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtProjectName" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </span>
                                    </div>
                                    <div class="form-group col-lg-12">
                                        <span class="col-lg-2">Primary Activity*</span>
                                        <span class="col-lg-3">
                                            <asp:Label ID="lblPrimary" runat="server"></asp:Label>
                                            <asp:DropDownList ID="ddlPrimary" runat="server" TabIndex="2" CssClass="form-control"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlPrimary" ErrorMessage="*" ForeColor="Red" InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </span>
                                        <span class="col-lg-2">Sub Activity</span>
                                        <span class="col-lg-3">
                                            <asp:Label ID="lblActivityType" runat="server"></asp:Label>
                                            <asp:CheckBoxList ID="chklActivityType" runat="server" RepeatDirection="Horizontal" TabIndex="3"></asp:CheckBoxList>
                                        </span>
                                    </div>
                                    <div class="form-group col-lg-12">
                                        <span class="col-lg-2">Project Start Date*</span>
                                        <span class="col-lg-3">
                                            <asp:Label ID="lblProjectStartDate" runat="server"></asp:Label>
                                            <asp:TextBox ID="txtProjectStartDate" runat="server" onkeypress="return false" onkeyup="return false" onkeydown="return false" CssClass="form-control" TabIndex="4"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtProjectStartDate" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </span>
                                        <span class="col-lg-2">Project End Date*</span>
                                        <span class="col-lg-3">
                                            <asp:Label ID="lblProjectEndDate" runat="server"></asp:Label>
                                            <asp:TextBox ID="txtProjectEndDate" runat="server" onkeypress="return false" onkeyup="return false" onkeydown="return false" CssClass="form-control" TabIndex="5"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtProjectEndDate" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </span>
                                    </div>
                                    <div class="form-group col-lg-12">
                                        <span class="col-lg-2">Project Manager*</span>
                                        <span class="col-lg-3">
                                            <asp:Label ID="lblProjectManager" runat="server"></asp:Label>
                                            <asp:DropDownList ID="ddlProjectManager" runat="server" CssClass="form-control" TabIndex="7"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="ddlProjectManager" ErrorMessage="*" ForeColor="Red" InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </span>
                                    </div>
                                    <div class="form-group col-lg-12">
                                        <span class="col-lg-2">Client Name*</span>
                                        <span class="col-lg-3">
                                            <asp:Label ID="lblClientName" runat="server"></asp:Label>
                                            <asp:DropDownList ID="ddlClientName" runat="server" AutoPostBack="True" Enabled="False" TabIndex="8" CssClass="form-control"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlClientName" ErrorMessage="*" ForeColor="Red" InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlClientName" ErrorMessage="*" ForeColor="Red" InitialValue="99999" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </span>
                                    </div>
                                    <div class="form-group col-lg-12" id="tblNewClient" runat="server">
                                        <table class="col-lg-12 table table-bordered">
                                            <tr>
                                                <td colspan="4" style="border: 1px solid #808080">Add new client details</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtClient" runat="server" Width="150px" ValidationGroup="CT" CssClass="textbox" onclick="javascript:txtClear()" MaxLength="40"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ControlToValidate="txtClient" ValidationGroup="CT" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator></td>
                                                <td>
                                                    <asp:TextBox ID="txtIndustry" runat="server" Width="150px" CssClass="textbox" MaxLength="40"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtAnnualTurnover" runat="server" Width="150px" CssClass="textbox" MaxLength="10"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnAddClient" runat="server" Text="Add" ValidationGroup="CT" CssClass="small-button" /></td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="form-group col-lg-12">
                                        <div class="col-lg-2">Client Budget Owner*</div>
                                        <div class="col-lg-3">
                                            <asp:Label ID="lblClientBudgetOwner" runat="server"></asp:Label>
                                            <asp:DropDownList ID="ddlClientBudgetOwner" runat="server" onchange="BindCCbudgetowner(this.value)" TabIndex="9" CssClass="form-control"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlClientBudgetOwner" ErrorMessage="*" ForeColor="Red" InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="ddlClientBudgetOwner" ErrorMessage="*" ForeColor="Red" InitialValue="99999" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-lg-7" id="tdcontactinfo">
                                            Email:<asp:Label ID="lblCBEmail" runat="server"></asp:Label>
                                            &nbsp;&nbsp; Contact:<asp:Label ID="lblCBContact" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div id="tblContactBudget" runat="server" class="col-lg-12">
                                        <table class="table table-bordered table-striped">
                                            <tr>
                                                <td colspan="4" style="border: 1px solid #808080">Add new contact person</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtBudgetContactPerson" runat="server" Width="150px" CssClass="textbox" ValidationGroup="CB" MaxLength="40"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ControlToValidate="txtBudgetContactPerson" ValidationGroup="CB" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBudgetContactOfficialEmail" runat="server" Width="150px" CssClass="textbox" ValidationGroup="CB" MaxLength="40"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="*" ControlToValidate="txtBudgetContactOfficialEmail" ValidationGroup="CB" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtBudgetContactOfficialEmail" ValidationGroup="CB" Display="Dynamic" SetFocusOnError="true" ErrorMessage="Invalid Email" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBudgetContactMobile1" runat="server" Width="150px" MaxLength="10" CssClass="textbox" ValidationGroup="CB"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ErrorMessage="*" ControlToValidate="txtBudgetContactMobile1" ValidationGroup="CB" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtBudgetContactMobile1" ValidationGroup="CB" Display="Dynamic" SetFocusOnError="true" ErrorMessage="Invalid Mobile No" ForeColor="Red" ValidationExpression="\d{10}"></asp:RegularExpressionValidator>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnBudgetContactPerson" runat="server" Text="Add" ValidationGroup="CB" CssClass="small-button" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="form-group col-lg-12">
                                        <span class="col-lg-2">Client Contact Details*</span>
                                        <span class="col-lg-3">
                                            <asp:Label ID="lblContactDetails" runat="server"></asp:Label>
                                            <asp:DropDownList ID="ddlContactDetails" runat="server" onchange="BindContactLabels(this.value)" TabIndex="10" CssClass="form-control"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlContactDetails" ErrorMessage="*" SetFocusOnError="true" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                        </span>
                                        <span id="clientcontactdetail" class="form-group col-lg-4">Email:<asp:Label ID="lblCEmail" runat="server"></asp:Label>
                                            &nbsp;&nbsp; Contact:<asp:Label ID="lblCContact" runat="server"></asp:Label>
                                        </span>
                                    </div>

                                    <div class="form-group col-lg-12">

                                        <div class="col-lg-12">
                                            <h4>Estimated v/s Actual Sheet</h4>
                                        </div>
                                        <div class="col-lg-12">
                                            <table class="col-lg-12 table table-bordered table-striped">
                                                <tr>
                                                    <th class="tdhead">Estimate</th>
                                                    <th class="tdhead">Pre P&L</th>
                                                    <th class="tdhead">Profit (%)</th>
                                                </tr>
                                                <tr style="text-align: right">
                                                    <td>
                                                        <asp:Label ID="txtEstimate" runat="server" TabIndex="12"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="txtPrePnL" runat="server"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="txtActual" runat="server" Text="0"></asp:Label></td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="col-lg-12">
                                        <div class="col-lg-6">
                                            <div class="form-group col-lg-12">
                                                <span class="col-lg-4">Client Approval</span>
                                                <span class="col-lg-8">
                                                    <asp:Label ID="lblApproval" runat="server"></asp:Label>
                                                    <asp:CheckBox ID="chkApproval" CssClass="chknos" runat="server" TabIndex="13" />
                                                    <%--<asp:CustomValidator ID="cvEventsValidator" Display="Dynamic" runat="server" ClientValidationFunction="ValidateCheckBoxList" ForeColor="red">*</asp:CustomValidator>--%>
                                                </span>
                                            </div>
                                            <div class="form-group col-lg-12">
                                                <span class="col-lg-4">Approval Date</span>
                                                <span class="col-lg-7">
                                                    <asp:Label ID="lblApprovalDate" runat="server"></asp:Label>
                                                    <asp:TextBox ID="txtApprovalDate" runat="server" onkeypress="return false" onkeyup="return false" onkeydown="return false" CssClass="form-control" TabIndex="14"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RFapprovaldate" SetFocusOnError="true" runat="server" ErrorMessage="*" ControlToValidate="txtApprovalDate" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </span>
                                            </div>
                                            <div class="form-group col-lg-12">
                                                <span class="col-lg-4">Approved By</span>
                                                <span class="col-lg-7">
                                                    <asp:Label ID="lblApprovedBy" runat="server"></asp:Label>
                                                    <asp:DropDownList ID="ddlApprovedBy" runat="server" CssClass="form-control" onchange="BindApprover(this.value)" TabIndex="15"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RFApprovedby" SetFocusOnError="true" runat="server" ErrorMessage="*" ControlToValidate="ddlApprovedBy" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                                </span>
                                                <span class="col-lg-3" id="tdApprovecontact" runat="Server">Email:
                            <asp:Label ID="lblCApproveEmail" runat="server"></asp:Label>
                                                    &nbsp;&nbsp; Contact:
                            <asp:Label ID="lblCApproveContact" runat="server"></asp:Label>
                                                </span>

                                            </div>
                                            <div class="form-group col-lg-12">
                                                <span class="col-lg-4">Approval mail</span>
                                                <span class="col-lg-7">
                                                    <asp:FileUpload ID="fupl_Mail" runat="server" onchange="javascript:return CheckUpl();" />
                                                    <asp:RequiredFieldValidator ID="RFFileupload" ControlToValidate="fupl_Mail" runat="server" SetFocusOnError="true" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>

                                                    <%--<asp:HyperLink ID="lblfulpl_Mail" runat="server" Text="Download"  Target="_blank"></asp:HyperLink>--%>
                                                    <%--<asp:Button ID="lblfulpl_Mail" runat="server"  Text="Download" />--%>

                                                    <p style="padding-top: 10px;"></p>
                                                    <asp:LinkButton ID="lblfulpl_Mail" runat="server" CssClass="btn btn-default btn-sm" ValidationGroup="sdada">Download</asp:LinkButton>
                                                    <asp:Button ID="btnverify" runat="server" Text="Verify" CssClass="btn btn-sm btn-primary" />
                                                    <asp:Button ID="btnreject" runat="server" Text="Reject" CssClass="btn btn-sm btn-primary" />
                                                    <asp:HiddenField ID="hdnlblfulpl_Mail" runat="server" />
                                                    <asp:HiddenField ID="hdnapprovalamount" runat="server" />
                                                </span>
                                            </div>
                                            <div class="form-group col-lg-12">
                                                <%--<span class="col-lg-4">Approval Amount</span>--%>
                                                <span class="col-lg-4">Activity/JC Amount</span>
                                                <span class="col-lg-7">
                                                    <asp:TextBox ID="TxtapprovlAmount" runat="server" MaxLength="10" CssClass="form-control" ValidationGroup="CB"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RFApprovalamount" runat="server" ErrorMessage="*" ControlToValidate="TxtapprovlAmount" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="TxtapprovlAmount" Display="Dynamic" SetFocusOnError="true" ErrorMessage="Invalid" ForeColor="Red" ValidationExpression="^(\d{1,18})(.\d{2})?$"></asp:RegularExpressionValidator>

                                                </span>
                                            </div>
                                            <div class="form-group col-lg-12">
                                                <span class="col-lg-4"></span>
                                                <span class="col-lg-8">This amount should not be greater than client approval amount for this Activity/JC.</span>

                                            </div>

                                        </div>
                                        <div class="col-lg-6 table-responsive" style="height:300px;">
                                            <asp:GridView ID="gridjobcardhistory" runat="server" AutoGenerateColumns="false" CssClass="table table-hover table-bordered" PageSize="5">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                            <asp:HiddenField ID="hdnpath" runat="server" Value='<%# Eval("ApprovalMail") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Documents">
                                                        <ItemTemplate>
                                                          <asp:LinkButton ID="lblfulpl_Mail1" runat="server" CssClass="btn btn-default btn-sm" ValidationGroup="sdada" OnClick="lblfulpl_Mail1_Click">Download</asp:LinkButton>
                                                         
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField  DataField="RequestedAmount" HeaderText="Requested Amount"/>
                                                    <asp:BoundField  DataField="RequestedOn" HeaderText="Requested On"/>
                                                    <asp:BoundField  DataField="Status" HeaderText="Status"/>
                                                    <asp:BoundField  DataField="ApprovedAmount" HeaderText="Approved/Rejected Amount"/>
                                                    <asp:BoundField  DataField="ApprovedOn" HeaderText="Approved/Rejected On"/>
                                                    <asp:BoundField  DataField="ApprovedBy" HeaderText="Approved/Rejected By"/>
                                                    


                                                </Columns>
                                            </asp:GridView>
                                        </div>

                                    </div>

                                    <div class="col-lg-12" style="text-align: center">
                                        <asp:Button ID="btnAdd" runat="server" Text="Save" CssClass="btn btn-primary" TabIndex="17" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" CssClass="btn btn-primary" TabIndex="16" />
                                        <asp:Button ID="btnCancelHome" runat="server" Text="Cancel" CausesValidation="false" CssClass="btn btn-primary" TabIndex="16" />
                                        <asp:Button ID="btnJobCard" runat="server" Text="Create Job Card No" CausesValidation="false" CssClass="btn btn-primary" TabIndex="19" />
                                        <asp:Button ID="btnJobCardClosure" runat="server" Text="Close Job" CausesValidation="false" CssClass="btn btn-primary" TabIndex="20" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:HiddenField ID="hdnBriefID" runat="server" />
                    <asp:HiddenField ID="hdnJobCardID" runat="server" />
                    <asp:HiddenField ID="hdn" runat="server" />
                    <asp:HiddenField ID="hdnNodificationID" runat="server" />
                    <asp:HiddenField ID="hdnhistoryID" runat="server" />

                </div>
            </div>
        </div>

    </section>



</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpFooter" runat="Server">
    <input type="hidden" id="clientid" />
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title">Add new Client contact person</h4>

                </div>
                <div class="modal-body">


                    <table class="table table-bordered">
                        <tr>
                            <th>Select Client*</th>
                            <th>
                                <select id="cbo_client" class="form-control">
                                </select>

                            </th>

                        </tr>
                        <tr>
                            <th>Name*</th>
                            <th>
                                <input type="text" id="txtContactPerson" maxlength="100" class="form-control" />

                            </th>

                        </tr>
                        <tr>
                            <th>Official Email*</th>
                            <th>
                                <input type="text" id="txtContactOfficialEmail" onblur="javascript:CheckEmail(this)" maxlength="100" class="form-control" />

                            </th>
                        </tr>
                        <tr>
                            <th>Mobile 1*</th>
                            <th>
                                <input type="text" id="txtContactMobile1" onkeyup="javascript:checknum(this)" maxlength="10" class="form-control" />

                            </th>
                        </tr>
                    </table>

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    <a id="modalbutton" class="btn btn-primary" href="javascript:saveclientcontact();">Save</a>


                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <script src="dist/js/bootstrap-datepicker.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            //alert('<%=(System.DateTime.Now).ToString()%>');

            //if ($('#chkApproval').is(":checked")) {
            //alert($(this).is(":checked"));
            var RFApprovalamount = $("[id*=RFApprovalamount]");
            ValidatorEnable(RFApprovalamount[0], $('#ContentPlaceHolder1_chkApproval').is(":checked"));

            var RFapprovaldate = $("[id*=RFapprovaldate]");
            ValidatorEnable(RFapprovaldate[0], $('#ContentPlaceHolder1_chkApproval').is(":checked"));

            var RFApprovedby = $("[id*=RFApprovedby]");
            ValidatorEnable(RFApprovedby[0], $('#ContentPlaceHolder1_chkApproval').is(":checked"));

            var RFFileupload = $("[id*=RFFileupload]");
            ValidatorEnable(RFFileupload[0], false);
            //}

            $('.chknos').click(function () {
                // alert($('#ContentPlaceHolder1_chkApproval').is(":checked"));
                var RFApprovalamount = $("[id*=RFApprovalamount]");
                ValidatorEnable(RFApprovalamount[0], $('#ContentPlaceHolder1_chkApproval').is(":checked"));

                var RFapprovaldate = $("[id*=RFapprovaldate]");
                ValidatorEnable(RFapprovaldate[0], $('#ContentPlaceHolder1_chkApproval').is(":checked"));

                var RFApprovedby = $("[id*=RFApprovedby]");
                ValidatorEnable(RFApprovedby[0], $('#ContentPlaceHolder1_chkApproval').is(":checked"));

                //var RFFileupload = $("[id*=RFFileupload]");
                //ValidatorEnable(RFFileupload[0], $('#ContentPlaceHolder1_chkApproval').is(":checked"));


            });
        });



        $(function () {
            $('input[id$=txtProjectStartDate]').datepicker({ format: 'dd-mm-yyyy', autoclose: true });
        });

        $(function () {
            $('input[id$=txtProjectEndDate]').datepicker({ format: 'dd-mm-yyyy', autoclose: true });
        });

        $(function () {
            $('input[id$=txtApprovalDate]').datepicker({ format: 'dd-mm-yyyy', autoclose: true });
        });

        $(document).ready(function () {
            $(".inner-Content-container").scrollTop(420);
            var watermark1 = 'Enter Client Name';
            var txt = $('input[id$=txtClient]').val();
            //init, set watermark text and class
            $('input[id$=txtClient]').val(watermark1).addClass('watermark');
            //if blur and no value inside, set watermark text and class again.
            $('input[id$=txtClient]').blur(function () {
                if ($('input[id$=txtClient]').val() == 0) {
                    $('input[id$=txtClient]').val(watermark1).addClass('watermark');
                }
            });


            //if focus and text is watermrk, set it to empty and remove the watermark class
            $('input[id$=txtClient]').focus(function () {
                if ($('input[id$=txtClient]').val() == watermark1) {
                    $('input[id$=txtClient]').val('').removeClass('watermark');
                }
            });


            var watermark2 = 'Enter Industry Name';
            var txt = $('input[id$=txtIndustry]').val();
            //init, set watermark text and class
            $('input[id$=txtIndustry]').val(watermark2).addClass('watermark');
            //if blur and no value inside, set watermark text and class again.
            $('input[id$=txtIndustry]').blur(function () {
                if ($('input[id$=txtIndustry]').val() == 0) {
                    $('input[id$=txtIndustry]').val(watermark2).addClass('watermark');
                }
            });

            //if focus and text is watermrk, set it to empty and remove the watermark class
            $('input[id$=txtIndustry]').focus(function () {
                if ($('input[id$=txtIndustry]').val() == watermark2) {
                    $('input[id$=txtIndustry]').val('').removeClass('watermark');
                }
            });




            var watermark3 = 'Enter Annual Turnover';
            var txt = $('input[id$=txtAnnualTurnover]').val();
            //init, set watermark text and class
            $('input[id$=txtAnnualTurnover]').val(watermark3).addClass('watermark');
            //if blur and no value inside, set watermark text and class again.
            $('input[id$=txtAnnualTurnover]').blur(function () {
                if ($('input[id$=txtAnnualTurnover]').val() == 0) {
                    $('input[id$=txtAnnualTurnover]').val(watermark3).addClass('watermark');
                }
            });

            //if focus and text is watermrk, set it to empty and remove the watermark class
            $('input[id$=txtAnnualTurnover]').focus(function () {
                if ($('input[id$=txtAnnualTurnover]').val() == watermark3) {
                    $('input[id$=txtAnnualTurnover]').val('').removeClass('watermark');
                }
            });




            var watermark4 = 'Enter Name';
            var txt = $('input[id$=txtContactPerson]').val();
            //init, set watermark text and class
            $('input[id$=txtContactPerson]').val(watermark3).addClass('watermark');
            //if blur and no value inside, set watermark text and class again.
            $('input[id$=txtContactPerson]').blur(function () {
                if ($('input[id$=txtContactPerson]').val() == 0) {
                    $('input[id$=txtContactPerson]').val(watermark4).addClass('watermark');
                }
            });

            //if focus and text is watermrk, set it to empty and remove the watermark class
            $('input[id$=txtContactPerson]').focus(function () {
                if ($('input[id$=txtContactPerson]').val() == watermark4) {
                    $('input[id$=txtContactPerson]').val('').removeClass('watermark');
                }
            });



            var watermark5 = 'Enter Contact Official Email';
            var txt = $('input[id$=txtContactOfficialEmail]').val();
            //init, set watermark text and class
            $('input[id$=txtContactOfficialEmail]').val(watermark5).addClass('watermark');
            //if blur and no value inside, set watermark text and class again.
            $('input[id$=txtContactOfficialEmail]').blur(function () {
                if ($('input[id$=txtContactOfficialEmail]').val() == 0) {
                    $('input[id$=txtContactOfficialEmail]').val(watermark5).addClass('watermark');
                }
            });

            //if focus and text is watermrk, set it to empty and remove the watermark class
            $('input[id$=txtContactOfficialEmail]').focus(function () {
                if ($('input[id$=txtContactOfficialEmail]').val() == watermark5) {
                    $('input[id$=txtContactOfficialEmail]').val('').removeClass('watermark');
                }
            });


            var watermark6 = 'Enter Mobile No';
            var txt = $('input[id$=txtContactMobile1]').val();
            //init, set watermark text and class
            $('input[id$=txtContactMobile1]').val(watermark6).addClass('watermark');
            //if blur and no value inside, set watermark text and class again.
            $('input[id$=txtContactMobile1]').blur(function () {
                if ($('input[id$=txtContactMobile1]').val() == 0) {
                    $('input[id$=txtContactMobile1]').val(watermark6).addClass('watermark');
                }
            });

            //if focus and text is watermrk, set it to empty and remove the watermark class
            $('input[id$=txtContactMobile1]').focus(function () {
                if ($('input[id$=txtContactMobile1]').val() == watermark6) {
                    $('input[id$=txtContactMobile1]').val('').removeClass('watermark');
                }
            });


            var watermark7 = 'Enter Name';
            var txt = $('input[id$=txtBudgetContactPerson]').val();
            //init, set watermark text and class
            $('input[id$=txtBudgetContactPerson]').val(watermark7).addClass('watermark');
            //if blur and no value inside, set watermark text and class again.
            $('input[id$=txtBudgetContactPerson]').blur(function () {
                if ($('input[id$=txtBudgetContactPerson]').val() == 0) {
                    $('input[id$=txtBudgetContactPerson]').val(watermark7).addClass('watermark');
                }
            });

            //if focus and text is watermrk, set it to empty and remove the watermark class
            $('input[id$=txtBudgetContactPerson]').focus(function () {
                if ($('input[id$=txtBudgetContactPerson]').val() == watermark7) {
                    $('input[id$=txtBudgetContactPerson]').val('').removeClass('watermark');
                }
            });



            var watermark8 = 'Enter Contact Official Email';
            var txt = $('input[id$=txtBudgetContactOfficialEmail]').val();
            //init, set watermark text and class
            $('input[id$=txtBudgetContactOfficialEmail]').val(watermark8).addClass('watermark');
            //if blur and no value inside, set watermark text and class again.
            $('input[id$=txtBudgetContactOfficialEmail]').blur(function () {
                if ($('input[id$=txtBudgetContactOfficialEmail]').val() == 0) {
                    $('input[id$=txtBudgetContactOfficialEmail]').val(watermark8).addClass('watermark');
                }
            });

            //if focus and text is watermrk, set it to empty and remove the watermark class
            $('input[id$=txtBudgetContactOfficialEmail]').focus(function () {
                if ($('input[id$=txtBudgetContactOfficialEmail]').val() == watermark5) {
                    $('input[id$=txtBudgetContactOfficialEmail]').val('').removeClass('watermark');
                }
            });


            var watermark9 = 'Enter Mobile No';
            var txt = $('input[id$=txtBudgetContactMobile1]').val();
            //init, set watermark text and class
            $('input[id$=txtBudgetContactMobile1]').val(watermark9).addClass('watermark');
            //if blur and no value inside, set watermark text and class again.
            $('input[id$=txtBudgetContactMobile1]').blur(function () {
                if ($('input[id$=txtBudgetContactMobile1]').val() == 0) {
                    $('input[id$=txtBudgetContactMobile1]').val(watermark9).addClass('watermark');
                }
            });

            //if focus and text is watermrk, set it to empty and remove the watermark class
            $('input[id$=txtBudgetContactMobile1]').focus(function () {
                if ($('input[id$=txtBudgetContactMobile1]').val() == watermark9) {
                    $('input[id$=txtBudgetContactMobile1]').val('').removeClass('watermark');
                }
            });


        })

        function GetList(id) {
            $.ajax({

                url: "ListingLinks.aspx?id=" + id,
                context: document.body,
                success: function (result) {
                    $('#TWContent').html(result);

                }
            });
        }
        function ValidateCheckBoxList(sender, args) {
            args.IsValid = false;

            if ($('input[id$=chkApproval]').is(":checked")) {

                args.IsValid = true;
                return;
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
    <script>

        $(document).ready(function () {

            // if user clicked on button, the overlay layer or the dialogbox, close the dialog	
            //$('a.btn-ok, #dialog-overlay, #dialog-box, #dialog-box2, #dialog-box3').click(function () {
            $('a.button').click(function () {
                $('#dialog-overlay, #dialog-box, #dialog-box2, #dialog-box3').hide();
                return false;
            });

            // if user resize the window, call the same function again
            // to make sure the overlay fills the screen and dialogbox aligned to center	
            //$(window).resize(function () {

            //    //only do it if the dialog box is not hidden
            //    //if (!$('#dialog-box').is(':hidden')) popup();
            //});


        });



    </script>

    <script>

        $(document).ready(function () {
            $("#tdcontactinfo").css("display", "none");
            $("#clientcontactdetail").css("display", "none");

            var approverid = $("input[id$=ddlApprovedBy]").val();
            if (approverid > 0) {
                $("span[id$=tdApprovecontact]").css("display", "block");
            } else {
                $("span[id$=tdApprovecontact]").css("display", "none");
            }

            $("input[id$=clientid]").val($("select[id$=ddlClientName]").val());
            $("select[id$=ddlPrimary]").change(function (event) {

                selectsubActivity(this.value);
            });
            bindclient();
        });
        function bindclient() {
            var JSONworkTypes = "";
            $.ajax({
                url: "AjaxCalls/AX_leads.aspx?call=4",
                context: document.body,
                success: function (Result) {
                    JSONworkTypes = JSON.parse(Result);
                    var myselect = document.getElementById('cbo_client');
                    myselect.options[myselect.length] = new Option("Select", "0")
                    for (var i = 0; i < JSONworkTypes.Table.length; i++) {
                        myselect.options[myselect.length] = new Option(JSONworkTypes.Table[i].Client.toString(), JSONworkTypes.Table[i].ClientID.toString())

                    }
                }
            });

        }
        function selectsubActivity(Activity) {

            $(':input[type="checkbox"]').each(function () {
                if ($(this).val() == Activity) {
                    $(this).attr("checked", "checked");
                }

            });

        }

        function BindContactLabels(cpid) {
            if (cpid == "Add new") {

                pullCCModel();
                $("#clientcontactdetail").css("display", "none");
            }
            else {
                if (cpid > 0) {

                    $('#<%= lblCEmail.ClientID%>').html("");
                    $('#<%= lblCContact.ClientID%>').html("");
                    $.ajax({
                        url: "AjaxCalls/AX_Client.aspx?call=2&id=" + cpid,
                        context: document.body,
                        success: function (Result) {
                            if (Result != "") {
                                var JSONworkTypes = JSON.parse(Result);
                                $('#<%= lblCEmail.ClientID%>').html(JSONworkTypes.Table[0].ContactOfficialEmailID.toString());
                                $('#<%= lblCContact.ClientID%>').html(JSONworkTypes.Table[0].Mobile1.toString());
                            }
                            $("#clientcontactdetail").css("display", "block");
                        }
                    });

                }
                else {
                    $("#clientcontactdetail").css("display", "none");
                    $('#<%= lblCEmail.ClientID%>').html("");
                    $('#<%= lblCContact.ClientID%>').html("");
                }
            }

        }

        function BindCCbudgetowner(cpid) {
            if (cpid == "Add new") {
                $("#tdcontactinfo").css("display", "none");
                pullCCModel();
            }
            else {
                if (cpid > 0) {

                    $('#<%= lblCBEmail.ClientID%>').html("");
                    $('#<%= lblCBContact.ClientID%>').html("");
                    $.ajax({
                        url: "AjaxCalls/AX_Client.aspx?call=2&id=" + cpid,
                        context: document.body,
                        success: function (Result) {
                            if (Result != "") {

                                var JSONworkTypes = JSON.parse(Result);
                                $('#<%= lblCBEmail.ClientID%>').html(JSONworkTypes.Table[0].ContactOfficialEmailID.toString());
                                $('#<%= lblCBContact.ClientID%>').html(JSONworkTypes.Table[0].Mobile1.toString());
                            }
                            $("#tdcontactinfo").css("display", "block");
                        }
                    });

                }
                else {
                    $("#tdcontactinfo").css("display", "none");
                    $('#<%= lblCBEmail.ClientID%>').html("");
                    $('#<%= lblCBContact.ClientID%>').html("");
                }
            }

        }



        function BindApprover(cpid) {
            if (cpid == "Add new") {
                $("span[id$=tdApprovecontact]").css("display", "none");
                pullCCModel();
            }

            else {
                if (cpid > 0) {

                    $('#<%= lblCApproveEmail.ClientID%>').html("");
                    $('#<%= lblCApproveContact.ClientID%>').html("");
                    $.ajax({
                        url: "AjaxCalls/AX_Client.aspx?call=2&id=" + cpid,
                        context: document.body,
                        success: function (Result) {
                            if (Result != "") {

                                var JSONworkTypes = JSON.parse(Result);
                                $('#<%= lblCApproveEmail.ClientID%>').html(JSONworkTypes.Table[0].ContactOfficialEmailID.toString());
                                $('#<%= lblCApproveContact.ClientID%>').html(JSONworkTypes.Table[0].Mobile1.toString());
                            }
                            $("span[id$=tdApprovecontact]").css("display", "block");
                        }
                    });

                }
                else {
                    $("span[id$=tdApprovecontact]").css("display", "none");
                    $('#<%= lblCApproveEmail.ClientID%>').html("");
                    $('#<%= lblCApproveContact.ClientID%>').html("");
                }
            }

        }

        function pullCCModel(id) {
            $('#myModal').modal({
                keyboard: false
            })
            var cid = $("input[id$=clientid]").val();
            $("select[id$=cbo_client] option[value='" + cid + "']").attr("Selected", "Selected");
        }
        function saveclientcontact() {
            var contactper = $("input[id$=txtContactPerson]").val();
            var email = $("input[id$=txtContactOfficialEmail]").val();
            var mobile = $("input[id$=txtContactMobile1]").val();
            var cid = $("select[id$='cbo_client'] option:selected").val();
            if (contactper.length == 0) {
                setTimeout("ShowMsg('Please enter contact person name')", 1)
                $("input[id$=txtContactPerson]").focus();
            }
            else if (email.length == 0) {
                setTimeout("ShowMsg('Please enter email')", 1)
                $("input[id$=txtContactOfficialEmail]").focus();
            }
            else if (mobile.length == 0) {
                setTimeout("ShowMsg('Please enter mobile number')", 1)
                $("input[id$=txtContactMobile1]").focus();
            }
            else if (cid == 0)
            { setTimeout("ShowMsg('Please select client')", 1) }
            else {

                if (cid > 0) {
                    $.ajax({
                        url: "AjaxCalls/AX_leads.aspx?call=6&cid=" + cid + "&name=" + contactper + "&email=" + email + "&mob=" + mobile,
                        context: document.body,
                        success: function (Result) {
                            // alert(Result);
                            if (Result == "True") {

                                $('#myModal').modal('hide');
                                $("input[id$=txtContactPerson]").val("");
                                $("input[id$=txtContactOfficialEmail]").val("");
                                $("input[id$=txtContactMobile1]").val("");
                                setTimeout("ShowMsg('save successfully')", 500)
                                BindContactPerson();
                            }


                        }
                    });
                }
            }
        }

        function BindContactPerson() {
            var cid = $("select[id$=ddlClientName]").val();
            if (cid > 0) {
                $.ajax({
                    url: "AjaxCalls/AX_Client.aspx?call=1&id=" + cid,
                    context: document.body,
                    success: function (Result) {
                        if (Result != "") {
                            var JSONworkTypes = JSON.parse(Result);

                            $("select[id$=ddlClientBudgetOwner]").empty();
                            $("Select[id$=ddlContactDetails]").empty();
                            $("select[id$=ddlClientBudgetOwner]").append($("<option></option>").val("0").text("Select"));
                            $("select[id$=ddlContactDetails]").append($("<option></option>").val("0").text("Select"));

                            for (var i = 0; i < JSONworkTypes.Table.length; i++) {

                                $("select[id$=ddlClientBudgetOwner]").append($("<option></option>").val(JSONworkTypes.Table[i].ContactID.toString()).text(JSONworkTypes.Table[i].ContactName.toString()));
                                $("select[id$=ddlContactDetails]").append($("<option></option>").val(JSONworkTypes.Table[i].ContactID.toString()).text(JSONworkTypes.Table[i].ContactName.toString()));

                            }

                            $("select[id$=ddlClientBudgetOwner]").append($("<option></option>").val("Add new").text("Add new"));

                        }

                    }
                });
            }
        }

    </script>
</asp:Content>
