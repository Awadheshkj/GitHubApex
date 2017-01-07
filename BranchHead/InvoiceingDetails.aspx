<%@ Page Title="" Language="VB" MasterPageFile="~/BranchHead/Apex.master" AutoEventWireup="false" CodeFile="InvoiceingDetails.aspx.vb" Inherits="Estimate_VS_Actuals" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="content-header">
        <h1>Invoice Detail</h1>
       
    </section>

    <!-- Main content -->
    <section class="content">
        <div class="row">
            <div class="col-xs-12">
                <!-- .box -->
                <div class="box">
                    <div class="box-header">
                        <h2 class="box-title">JC # : <asp:Label ID="jcno" runat="server"></asp:Label></h2>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">

                        <asp:HiddenField runat="server" ID="hdnGrand" />
                        <asp:HiddenField runat="server" ID="hdnSubTotal" />
                        <asp:HiddenField runat="server" ID="hdnTotal" />
                        <asp:HiddenField ID="hdnBrief" runat="server" />
                        <asp:HiddenField ID="hdnEstimate" runat="server" />
                        <asp:HiddenField ID="hdnJobCardID" runat="server" />
                        <asp:HiddenField runat="server" ID="hdnNodificationID" />
                        <asp:HiddenField runat="server" ID="hdnClientID" />
                        <asp:HiddenField runat="server" ID="hdnMangnmtFees" />
                        <asp:HiddenField runat="server" ID="hdnServiceTax" />


                       
                        <div id="divError" runat="server" class="ui-state-error ui-corner-all divError">
                            <p>
                                <span class="ui-icon ui-icon-alert" style="float: left; margin-right: .3em;"></span>
                                <strong>Warning: </strong>
                                <asp:Label ID="lblError" runat="server"></asp:Label>
                            </p>
                        </div>

                        <div id="MessageDiv" runat="server" class="ui-state-msg ui-corner-all divError">
                            <p>

                                <strong>Message: </strong>
                                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                            </p>
                        </div>
                        <div id="dialog-box" style="display: none;">
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

                                                    <asp:CheckBoxList ID="chklActivityType" runat="server" RepeatDirection="Horizontal" CssClass="CheckBoxList" TabIndex="3"></asp:CheckBoxList>

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
                                                    <%--<asp:Label ID="lblbrcclient" runat="server"></asp:Label>--%>
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
                                                    <asp:Label ID="lblCEmail" runat="server"></asp:Label>
                                                    <br />
                                                    <asp:Label ID="lblCContact" runat="server"></asp:Label>
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
                        <asp:Panel ID="Panel1" runat="server" Visible="false">
                            <div class="col-lg-12">

                                <div class="form-group">
                                    <label class="col-lg-1 control-label" for="head"></label>
                                    <div class="navbar col-lg-10">
                                        <div class="bs-example form-horizontal" id="mainBrief">
                                            <h3>Brief Manager</h3>
                                            <div id="Brief_Status">
                                                <div class="form-group">

                                                    <label for="inputEmail" class="col-lg-3">Brief Name :</label>

                                                    <div class="col-lg-3">
                                                        <asp:Label runat="server" ID="lblbriefname" Text=""></asp:Label>
                                                    </div>


                                                    <label for="inputEmail" class="col-lg-3">Client :</label>

                                                    <div class="col-lg-3">
                                                        <asp:Label runat="server" ID="lblbriefClient" Text=""></asp:Label>

                                                    </div>

                                                </div>
                                                <div class="form-group">

                                                    <label for="inputEmail" class="col-lg-3">Primary Activity :</label>

                                                    <div class="col-lg-3">
                                                        <asp:Label runat="server" ID="lblbriefPrimaryActivity" Text=""></asp:Label>
                                                    </div>


                                                    <label for="inputEmail" class="col-lg-3">Budget :</label>

                                                    <div class="col-lg-3">
                                                        <asp:Label runat="server" ID="lblbriefBudget" Text=""></asp:Label>
                                                    </div>

                                                </div>


                                                <div class="form-group">
                                                    <div class="pull-right">
                                                        <a style="color: #c33100; text-decoration: none;" href="javascript:popup('dialog-box')">View more</a>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <div id="dialog-box2">
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
                            <div class="col-lg-12">

                                <div class="form-group">
                                    <label class="col-lg-1 control-label" for="head"></label>
                                    <div class="navbar col-lg-10">


                                        <div class="bs-example form-horizontal" id="mainPrepnl">
                                            <h3>Pre P&L Manager</h3>
                                            <div id="prepnl_status">
                                                <div class="form-group">

                                                    <label for="inputEmail" class="col-lg-3">Client :</label>

                                                    <div class="col-lg-3">
                                                        <asp:Label runat="server" ID="lblClient" Text=""></asp:Label>
                                                    </div>


                                                    <label for="inputEmail" class="col-lg-3">Project Manager :</label>

                                                    <div class="col-lg-3">
                                                        <asp:Label runat="server" ID="lblPM" Text=""></asp:Label>
                                                    </div>

                                                </div>
                                                <div class="form-group">

                                                    <label for="inputEmail" class="col-lg-3">KAM :</label>

                                                    <div class="col-lg-3">
                                                        <asp:Label runat="server" ID="lblKAM" Text=""></asp:Label>
                                                    </div>


                                                    <label for="inputEmail" class="col-lg-3">Pre Event Quote :</label>

                                                    <div class="col-lg-3">
                                                        <asp:Label runat="server" ID="lblPEQ" Text=""></asp:Label>
                                                    </div>

                                                </div>

                                                <div class="form-group">

                                                    <label for="inputEmail" class="col-lg-3">Pre Event Total :</label>

                                                    <div class="col-lg-3">
                                                        <asp:Label runat="server" ID="lblPreEventtotal" Text=""></asp:Label>
                                                    </div>



                                                </div>

                                                <div class="form-group">
                                                    <div class="pull-right">
                                                        <a style="color: #c33100; text-decoration: none;" href="javascript:popup('dialog-box2')">View more</a>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="dialog-box3">
                                <div class="dialog-content">
                                    <div id="dialog-message3">
                                        <h4>Estimate</h4>


                                        <table id="Table1" runat="server" style="width: 99%;">
                                            <tr>
                                                <td colspan="4">

                                                    <asp:GridView runat="server" ID="GridView1" AutoGenerateColumns="false" HeaderStyle-BackColor="#333333" HeaderStyle-ForeColor="White" Width="90%" AllowPaging="true" PageSize="15" ShowFooter="false">
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

                                                    <asp:Label runat="server" ID="lbl11"><b>Sub Total :</b></asp:Label></td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblSubTotal1"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td class="tdhead"><b>Management Fees :</b>(<asp:TextBox ID="txtMFeePer1" Visible="false" runat="server" CssClass="small_textbox_grid" TabIndex="8" autocomplete="off" onkeyup="javascript:CalTotalMFeePer();"></asp:TextBox>
                                                    <asp:Label ID="lblMFeePer1" runat="server"></asp:Label>%)</td>
                                                <td>
                                                    <asp:Label ID="lblMangnmtFees1" runat="server"></asp:Label>
                                                    <asp:TextBox Visible="false" runat="server" ID="txtMangnmtFees1" onkeyup="javascript:CalTotal();" MaxLength="10" CssClass="textbox" autocomplete="off"></asp:TextBox>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="tdhead">
                                                    <asp:Label runat="server" ID="Label9"><b>Total :</b></asp:Label></td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblTotal1"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td class="tdhead"><b>Service Tax / VAT : </b>(<asp:TextBox Visible="false" ID="txtServiceTax1" runat="server" TabIndex="9" CssClass="right_small_textbox_grid" onkeyup="CalTotal1();" autocomplete="off"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ErrorMessage="*" ControlToValidate="txtServiceTax" ForeColor="red"></asp:RequiredFieldValidator><asp:Label ID="lblServiceTaxPer1" runat="server" SetFocusOnError="true"></asp:Label>
                                                    %) </td>
                                                <td>
                                                    <asp:Label ID="lblServiceTax1" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="tdhead">
                                                    <asp:Label runat="server" ID="Label13"><b>Grand Total :</b></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblGrandTotal1"></asp:Label></td>

                                            </tr>


                                        </table>

                                    </div>
                                    <a href="#" class="button">Close</a>
                                </div>
                            </div>
                            <div class="col-lg-12">
                                <div class="form-group">
                                    <label class="col-lg-1 control-label" for="head"></label>
                                    <div class="navbar col-lg-10">
                                        <div class="bs-example form-horizontal" id="mainEstimate">
                                            <h3>Estimate</h3>
                                            <div id="estimate_status">
                                                <div class="form-group">
                                                    <label for="inputEmail" class="col-lg-3">Client : </label>
                                                    <div class="col-lg-3">
                                                        <asp:Label runat="server" ID="lblEstClient" Text=""></asp:Label>
                                                    </div>
                                                    <label for="inputEmail" class="col-lg-3">Project Manager :</label>
                                                    <div class="col-lg-3">
                                                        <asp:Label runat="server" ID="lblEstPM" Text=""></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label for="inputEmail" class="col-lg-3">KAM :</label>
                                                    <div class="col-lg-3">
                                                        <asp:Label runat="server" ID="lblEstKAM" Text=""></asp:Label>
                                                    </div>
                                                    <label for="inputEmail" class="col-lg-3">Sub Total :</label>
                                                    <div class="col-lg-3">
                                                        <asp:Label runat="server" ID="lblEstSubtotal" Text=""></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="form-group pull-right">

                                                    <a style="color: #c33100; text-decoration: none;" href="javascript:popup('dialog-box3')">View more</a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <div class="col-lg-12">
                                <div class="form-group">
                                    <label class="col-lg-1 control-label" for="head"></label>
                                    <div class="navbar col-lg-10">
                                        <div class="bs-example form-horizontal" id="mainpostpnl">
                                            <h3>Post P&L Manager</h3>
                                            <div id="postpnl_status">
                                                <asp:GridView ID="gvpostpnl" runat="server" AutoGenerateColumns="false" PageSize="5" ShowFooter="true" CssClass="table table-bordered">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No.">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                                <asp:HiddenField ID="hdnPostPnLCostID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "PostPnLCostID")%>' />
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
                                                            <FooterTemplate>

                                                                <asp:Label ID="Label6" runat="server" Font-Bold="true" Text="Total :"></asp:Label>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Post Event Cost">
                                                            <ItemTemplate>
                                                                <%# DataBinder.Eval(Container.DataItem, "PostEventCost")%>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="gv_txtPostEventCost" runat="server" Text='<%#Eval("PostEventCost")%>' />
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lbltotalamt" runat="server" Font-Bold="true" Font-Size="14px" Text=""></asp:Label>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Service Tax / VAT (in %)" Visible="false">
                                                            <ItemTemplate>
                                                                <%# DataBinder.Eval(Container.DataItem, "PostEventServiceTax")%>
                                                            </ItemTemplate>
                                                            <%-- <FooterTemplate>
                                        Total :
                                    </FooterTemplate>--%>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total" Visible="false">
                                                            <ItemTemplate>
                                                                <%# DataBinder.Eval(Container.DataItem, "PostEventTotal")%>
                                                            </ItemTemplate>
                                                            <%-- <FooterTemplate>
                                        
                                    </FooterTemplate>--%>
                                                        </asp:TemplateField>
                                                        <%--<asp:TemplateField>
                                    <FooterTemplate>
                                        Total
                                    </FooterTemplate>
                                </asp:TemplateField>--%>
                                                    </Columns>
                                                </asp:GridView>
                                                <asp:Label ID="Label5" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-12">
                                <div class="form-group">
                                    <label class="col-lg-1 control-label" for="head"></label>
                                    <div class="navbar col-lg-10">
                                        <div class="bs-example form-horizontal" id="mainClaim">
                                            <h3>Claim Details</h3>
                                            <div id="Claim_status">
                                                <div id="Docgrid">
                                                    <span style="font-size: 20px;">Loading...</span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <div id="divContent" runat="server">
                            <div class="inner-Content-container">
                                <div class="InnerContentData">

                                    <div class="col-12">
                                        <div id="Div1" runat="server" class="col-12">
                                            <%--<table class="table table-bordered"  style="width:400px;">
                            <tr style="font-weight: bold; font-size: 14px">
                                <td>Total Invoice Amt.</td>
                                <td>Invoiced Amt.</td>
                                <td>Due For Invoicing</td>
                            </tr>
                            <tr>
                                <td style="text-align:center ;">
                                    <asp:Label ID="lbltotalInvoice" runat="server" Text="0.00"></asp:Label>

                                </td>
                                <td style="text-align:center ;"><asp:Label ID="lblinvoiced" runat="server" Text="0.00"></asp:Label></td>
                                <td style="text-align:center ;"><asp:Label ID="lblDueinvoice" runat="server" Text="0.00"></asp:Label></td>
                            </tr>
                            


                        </table>--%>
                                            <asp:GridView runat="server" ID="GvInvoicedetail" Width="650px" AutoGenerateColumns="false" Visible="false" CssClass="table table-bordered">
                                                <Columns>
                                                    <asp:BoundField HeaderText="" DataField="Detail" />
                                                    <asp:BoundField HeaderText="Subtotal" DataField="Subtotal" />
                                                    <asp:BoundField HeaderText="Agency Fee" DataField="Managementfee" />
                                                    <asp:BoundField HeaderText="Servicetax" DataField="servicetax" />
                                                    <asp:BoundField HeaderText="GrandTotal" DataField="GrandTotal" />

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <asp:HiddenField ID="hdnisalreadyregistered" runat="server" />
                                        <div id="trGridEstimate" runat="server" class="col-12">
                                            <asp:GridView runat="server" ID="gdvEstimate" AutoGenerateColumns="false" AllowPaging="false" ShowFooter="true" CssClass="table table-bordered">
                                                <Columns>

                                                    <asp:TemplateField HeaderText="ID">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                            <%--<asp:HiddenField runat="server" ID="hdnEstimateID" Value='<%# Bind("EstimateID")%>' />
                                        <asp:HiddenField runat="server" ID="hdnID" Value='<%# Bind("ID")%>' />--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Category">

                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container.DataItem, "Category")%>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Particulars">

                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container.DataItem, "Particulars")%>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Quantity">

                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container.DataItem, "Quantity")%>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Unit Price">

                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container.DataItem, "Rate")%>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Days">

                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container.DataItem,"Days") %>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Final Estimate" Visible="false">

                                                        <ItemTemplate>

                                                            <%--  <asp:TextBox ID="lblEstimate" Enabled="false"  runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Actual")%>' MaxLength="10" CssClass="form-control input-small"></asp:TextBox>--%>
                                                            <asp:TextBox ID="lblEstimate" Enabled="false" runat="server" Text='0' MaxLength="10" CssClass="form-control input-small"></asp:TextBox>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Invoiced Amt.">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblinvoicedamt" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceAmt")%>'></asp:Label>

                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Invoice Amt" Visible="false">

                                                        <ItemTemplate>
                                                            <%--<asp:TextBox ID="txt_actual" onkeyup="javascript:validdata(this)" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"InvoiceAmt") %>' MaxLength="10" CssClass="form-control input-small"></asp:TextBox>--%>
                                                            <asp:TextBox ID="txt_actual" onkeyup="javascript:validdata(this)" runat="server" Text='0' MaxLength="10" CssClass="form-control input-small"></asp:TextBox>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Agency Fee(%)">

                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txt_agencyfee" onkeyup="javascript:validdata(this)" Enabled="false" runat="server" Text='<%# Eval("AgencyFee")%>' MaxLength="5" CssClass="form-control input-small"></asp:TextBox>
                                                            <%--<%# Eval("AgencyFee")%>--%>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Remarks">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRemarks" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Remarks")%>'></asp:Label>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>


                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div id="trGridDisplay" runat="server" class="col-12">
                                            <asp:GridView runat="server" ID="gvDisplay" AutoGenerateColumns="false" AllowPaging="true" PageSize="15" ShowFooter="true" CssClass="col-12">
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
                                                    <asp:TemplateField HeaderText="Actual">

                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container.DataItem,"Actual") %>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remarks">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRemarks" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Remarks") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>

                                        <div class="col-12 form-group">
                                            <span class="col-2">

                                                <asp:Label runat="server" ID="lbl1">Sub Total :</asp:Label></span>
                                            <span class="col-6">
                                                <asp:Label runat="server" ID="lblSubTotal"></asp:Label></span>
                                        </div>
                                        <div class="col-12 form-group">
                                            <span class="col-2">Management Fees :<asp:TextBox ID="txtMFeePer" runat="server" CssClass="form-control" TabIndex="8" autocomplete="off" onkeyup="javascript:CalTotalMFeePer();"></asp:TextBox>
                                                <asp:Label ID="lblMFeePer" Visible="false" runat="server"></asp:Label><%--(%)--%></span>
                                            <span class="col-6">
                                                <asp:Label ID="lblMangnmtFees" runat="server"></asp:Label>
                                                <asp:TextBox runat="server" ID="txtMangnmtFees" onkeyup="javascript:CalTotal();" MaxLength="10" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtMangnmtFees" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtMangnmtFees" ErrorMessage="Invalid Fee" ForeColor="Red" MaximumValue="999999999999" MinimumValue="1" Type="Double" SetFocusOnError="true"></asp:RangeValidator>
                                            </span>
                                        </div>
                                        <div class="col-12 form-group">
                                            <span class="col-2">
                                                <asp:Label runat="server" ID="Label2">Total :</asp:Label></span>
                                            <span class="col-6">
                                                <asp:Label runat="server" ID="lblTotal"></asp:Label></span>
                                        </div>
                                        <div class="col-12 form-group">
                                            <span class="col-2">Service Tax / VAT : (
                            <%--<asp:TextBox ID="txtServiceTax" runat="server" TabIndex="9" CssClass="form-control input-small" onkeyup="CalTotal1();" autocomplete="off"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*" ControlToValidate="txtServiceTax" ForeColor="red"></asp:RequiredFieldValidator><asp:Label ID="lblServiceTaxPer" runat="server" Text="12.36" SetFocusOnError="true"></asp:Label>--%>
                                                <asp:DropDownList ID="ddlservicetax" runat="server">
                                                    <asp:ListItem>12.36</asp:ListItem>
                                                    <asp:ListItem>14.00</asp:ListItem>
                                                    <asp:ListItem>14.50</asp:ListItem>
                                                </asp:DropDownList>
                                                %) </span>
                                            <span class="col-6">
                                                <asp:Label ID="lblServiceTax" runat="server"></asp:Label>
                                            </span>
                                        </div>
                                        <div class="col-12 form-group">
                                            <span class="col-2">
                                                <asp:Label runat="server" ID="Label4">Grand Total :</asp:Label>
                                            </span>
                                            <span class="col-6">
                                                <asp:Label runat="server" ID="lblGrandTotal"></asp:Label></span>

                                        </div>
                                        <div id="trAppRemarks" runat="server" class="col-12 form-control">
                                            <span class="col-2">Remarks:</span>
                                            <span class="col-6">
                                                <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" TabIndex="24" CssClass="form-control"></asp:TextBox></span>
                                        </div>

                                        <div class="col-12" style="text-align: center; display: none;">
                                            <asp:HiddenField ID="hdnPnLID" runat="server" />
                                            <a href='javascript:history.go(-1)' class="btn btn-primary">Back</a>
                                            <div style="display: none;">
                                                <asp:Button runat="server" ID="btnCancel" Text="Back" CssClass="btn btn-primary" Visible="false" CausesValidation="false" TabIndex="9" />

                                                <asp:Button runat="server" ID="btnCancelApp" Text="Back" Visible="false" CssClass="btn btn-primary" CausesValidation="false" TabIndex="9" />
                                                <asp:Button ID="btnCancelHome" runat="server" Text="Back" CausesValidation="false" Visible="false" CssClass="btn btn-primary" TabIndex="9" />
                                            </div>
                                            <asp:Button runat="server" ID="btnAdd" Text="Save" CssClass="btn btn-primary" TabIndex="10" />
                                            <asp:Button runat="server" ID="btnFinal" Text="Create Job Card" CssClass="btn btn-primary" TabIndex="11" />
                                            <asp:Button runat="server" ID="btnExcel" Text="Export To Excel" CssClass="btn btn-primary" TabIndex="12" />
                                        </div>
                                    </div>

                                </div>

                            </div>
                        </div>
                        <div id="Divback" runat="server" style="text-align: center; margin-top: 20px;">
                            <asp:Button runat="server" ID="btn_back" Text="Back" CssClass="small-button" CausesValidation="false" TabIndex="9" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpFooter" runat="Server">

    <script type="text/javascript">

        function validdata(obj) {
            var id = obj.id;
            var val1 = false;
            var val2 = false;

            if ($('#' + id).val() != "") {

                var numberRegex = /^\d{1,10}(\.\d{0,2})?$/;
                if (numberRegex.test($('#' + id).val())) {
                    //return true;
                    //GVcalculation()
                    return true;
                }
                else {
                    alert('Enter numeric data only');
                    $('#' + id).val("");
                    return false;
                }
            }
        }



        function getTotalEmployeeSalary() {
            var sum = 0.00;
            $("#<%=gvpostpnl.ClientID%> tr:has(td)").each(function () {

                if ($.trim($(this).find("td:eq(4)").text()) != "" && !isNaN($(this).find("td:eq(4)").text()))

                    sum = sum + parseInt($(this).find("td:eq(4)").text());
                // $('#<%=Label5.ClientID()%>').val(sum)

                $(this).addClass('highlightRow');
            });

            $("#<%=gvpostpnl.ClientID%> [id*=lbltotalamt]").text(sum.toFixed(2));

            // $('#<%=Label5.ClientID()%>').val(sum)

        }
    </script>
    <script>

        $(document).ready(function () {
            $("#Brief_Status").hide();
            $("#prepnl_status").hide();
            $("#estimate_status").hide();
            $("#postpnl_status").hide();
            $("#Claim_status").hide();

            $("#mainBrief").click(function () {
                $("#Brief_Status").slideToggle(600);

            });
            $("#mainPrepnl").click(function () {
                $("#prepnl_status").slideToggle(600);
            });
            $("#mainEstimate").click(function () {
                $("#estimate_status").slideToggle(600);
            });
            $("#mainpostpnl").click(function () {
                $("#postpnl_status").slideToggle(600);
            });
            $("#mainClaim").click(function () {
                $("#Claim_status").slideToggle(600);
            });
            var jc = GetParameterValues('jid');


            getTotalEmployeeSalary()
            //GVcalculation()

        });

        function fillTabPM4Details(jc) {
            var datatable = "";

            $.ajax({
                url: "AjaxCalls/PMDetails.aspx?call=19&jid=" + jc, cache: false,
                context: document.body,

                success: function (Result) {
                    if (!Result == "") {
                        //alert(Result);
                        var jsonstr = JSON.parse(Result);
                        datatable = '<table class="table table-bordered table-hover">'
                        datatable += '<thead>'
                        datatable += '<tr>'
                        datatable += '<th>' + 'ID' + '</th>'
                        datatable += '<th>' + 'Task' + '</th>'
                        datatable += '<th>' + 'Task Details' + '</th>'
                        datatable += '<th>' + 'Start date' + '</th>'
                        datatable += '<th>' + 'End date' + '</th>'
                        datatable += '<th>' + 'Cost Center' + '</th>'
                        datatable += '<th>' + 'Assign From' + '</th>'
                        datatable += '<th>' + 'Assign To' + '</th>'
                        datatable += '<th>' + 'Assign Budget' + '</th>'
                        datatable += '<th>' + 'RequestedAmt' + '</th>'
                        datatable += '<th>' + 'Claimed Amount' + '</th>'
                        datatable += '<th>' + 'Status' + '</th>'
                        datatable += '<th>' + 'Claim Details' + '</th>'
                        datatable += '</tr>'
                        datatable += '</thead>'
                        datatable += '<tbody id="tblbody">'
                        //alert(datatable);
                        for (var i = 0; i < jsonstr.Table.length; i++) {
                            var j = i + 1;
                            datatable += '<tr>'
                            datatable += '<td>' + j + '</td>'

                            datatable += '<td>' + jsonstr.Table[i].Title.toString() + '</td>'

                            datatable += '<td>' + jsonstr.Table[i].particulars.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].startdate.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].enddate.toString() + '</td>'

                            datatable += '<td>' + jsonstr.Table[i].categoryTask.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].AssignFrom.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].Name.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].Expence.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].RequestedAmt.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].Amount.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].ClaimStatus.toString() + '</td>'
                            if (jsonstr.Table[i].Amount.toString() != "0.00") {
                                datatable += '<td>' + '<a href="RembursementClaimApprovalView.aspx?type=Task&nid=0&aid=' + jsonstr.Table[i].ClaimMasterID.toString() + '">View</a>' + '</td>'
                            }
                            else {
                                datatable += '<td>N/A</td>'
                            }
                            datatable += '</tr>'
                        }
                        datatable += '</tbody>'
                        datatable += '</table><div class="pagination"></div>'

                        //$('#gridDiv').html = datatable;
                        document.getElementById('Docgrid').innerHTML = datatable;

                        $(".pagination").jPages({
                            containerID: "tblbody",
                            perPage: 10,
                            startPage: 1,
                            startRange: 1,
                            midRange: 10,
                            endRange: 1
                        });
                    }
                    else {
                        document.getElementById('Docgrid').innerHTML = "Data Not Found";
                    }
                }
            });
        }
        function GetParameterValues(param) {
            var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
            for (var i = 0; i < url.length; i++) {
                var urlparam = url[i].split('=');
                if (urlparam[0] == param) {
                    return urlparam[1];
                }
            }
        }

    </script>



</asp:Content>
