<%@ Page Title="" Language="VB" MasterPageFile="~/Apex.master" AutoEventWireup="false" CodeFile="OpenEstimate.aspx.vb" Inherits="OpenEstimate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Estimate Manager
        </h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="leads.aspx"><i class="fa fa-th"></i>&nbsp;Settings</a></li>
            <li class="active">Estimate Manager</li>
        </ol>
    </section>

    <!-- Main content -->
    <section class="content">
        <div class="row">
            <div class="col-lg-xs-12">
                <div class="box box-primary">
                    <div class="box-header">
                        <h3 class="box-title">List of Categories</h3>
                    </div>

                    <div class="box-body">
                        <div class="alert alert-danger" id="divError" runat="server">
                            <strong>Warning: </strong>
                            <asp:Label ID="lblError" runat="server"></asp:Label>


                        </div>

                        <div class="alert alert-success" id="MessageDiv" runat="server">
                            <strong>Message: </strong>
                            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>

                        </div>



                        <div id="divContent" runat="server">
                            <div class="inner-Content-container">
                                <div class="InnerContentData" id="divkamdata" runat="server">
                                    <div class="modal fade" id="dialog-box">
                                        <div class="modal-dialog">
                                            <div class="modal-content">
                                                <div class="modal-header">
                                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                                    <h4 class="modal-title">Modal title</h4>
                                                </div>
                                                <div class="modal-body">
                                                    <div class="dialog-content">
                                                        <div id="dialog-message">
                                                            <h4>Brief Manager</h4>

                                                            <table style="width: 99%">
                                                                <tr>
                                                                    <td class="tdhead">Brief Name</td>
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
                                                                        <asp:Label ID="lblbrClient" runat="server"></asp:Label>

                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="tdhead">Client Contact Person</td>
                                                                    <td>
                                                                        <asp:Label ID="lblContactPerson" runat="server"></asp:Label>
                                                                        <asp:DropDownList Visible="false" ID="ddlContactPerson" runat="server" AutoPostBack="True" TabIndex="10"></asp:DropDownList>
                                                                    </td>
                                                                    <td style="font-size: 11px;">
                                                                        <asp:Label ID="lblCEmail" runat="server"></asp:Label>
                                                                        <br />
                                                                        <asp:Label ID="lblCContact" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>

                                                                <tr>
                                                                    <td class="tdhead">Scope of work</td>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="lblScope" runat="server"></asp:Label>

                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="tdhead">Target Audience
                                                                    </td>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="lblTargetAudience" runat="server"></asp:Label>

                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="tdhead">Measurement Matrix
                                                                    </td>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="lblMeasurementMatrix" runat="server"></asp:Label>

                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="tdhead">Activity Details</td>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="lblActivityDetails" runat="server"></asp:Label>

                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="tdhead">Key Challenges for execution</td>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="lblKeyChallenges" runat="server"></asp:Label>

                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="tdhead">Timeline for revert</td>
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
                                                        <a href="#" class="button">Close</a>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-12">
                                        <div class="panel panel-info">
                                            <div class="panel-heading clearfix">
                                                Brief Manager (JC #<asp:Label ID="lbljcno" runat="server" Text=""></asp:Label>)
                                                <button type="button" class="btn btn-primary btn-sm pull-right" data-toggle="modal" data-target="#dialog-box">View more</button>
                                            </div>
                                            <div class="panel-body">
                                                <div class="col-lg-3">
                                                    <b>Brief Name :</b>
                                                    <asp:Label runat="server" ID="lblbriefname" Text=""></asp:Label>
                                                </div>
                                                <div class="col-lg-3">
                                                    <b>Client :</b>
                                                    <asp:Label runat="server" ID="lblbriefClient" Text=""></asp:Label>
                                                </div>
                                                <div class="col-lg-3">
                                                    <b>Primary Activity :</b>
                                                    <asp:Label runat="server" ID="lblbriefPrimaryActivity" Text=""></asp:Label>
                                                    <asp:Label runat="server" ID="lblprimaryactivityprofitpr" Visible="false" Text=""></asp:Label>
                                                </div>
                                                <div class="col-lg-3">
                                                    <b>Budget :</b>
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

                                                                        <asp:HyperLink ID="lblfulpl_Mail" runat="server" Text="Download" Target="_blank"></asp:HyperLink>
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
															 <div class="table-responsive">
                                                            <table id="tbl2" runat="server" class="table-responsive">
                                                                <tr>
                                                                    <td colspan="4" >

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
                                                                                <asp:TemplateField HeaderText="Supplier Name" Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <%# DataBinder.Eval(Container.DataItem, "SupplierName")%>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Quantity">
                                                                                    <EditItemTemplate>
                                                                                        <asp:TextBox ID="gv_txtquantity" CssClass="form-control input-small" onkeyup="javasript:Calc_Grid2(this)" runat="server" MaxLength="7" Text='<%#Eval("Quantity") %>' />
                                                                                        <asp:RequiredFieldValidator ID="RFV_gv_txtquantity" ValidationGroup="Editvalidation" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="gv_txtquantity"></asp:RequiredFieldValidator>

                                                                                    </EditItemTemplate>
                                                                                    <ItemTemplate>
                                                                                        <%# DataBinder.Eval(Container.DataItem, "Quantity")%>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <asp:TextBox runat="server" ID="txtquantity" CssClass="form-control input-small" onkeyup="javascript:Calc_Grid();" MaxLength="10" TabIndex="3" autocomplete="off"></asp:TextBox>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="txtquantity" ForeColor="Red" ValidationGroup="EG" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                                    </FooterTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Unit Price">
                                                                                    <EditItemTemplate>
                                                                                        <asp:TextBox ID="gv_txtRate" CssClass="form-control input-small" onkeyup="javasript:Calc_Grid2(this)" runat="server" MaxLength="10" Text='<%#Eval("Rate") %>' />
                                                                                        <asp:RequiredFieldValidator ID="RFV_gv_txtRate" ValidationGroup="Editvalidation" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="gv_txtRate"></asp:RequiredFieldValidator>

                                                                                    </EditItemTemplate>
                                                                                    <ItemTemplate>
                                                                                        <%# DataBinder.Eval(Container.DataItem, "Rate")%>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <asp:TextBox runat="server" ID="txtRate" CssClass="form-control input-small" onkeyup="javascript:Calc_Grid();" MaxLength="10" TabIndex="4" autocomplete="off"></asp:TextBox>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ControlToValidate="txtRate" ForeColor="Red" ValidationGroup="EG" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                                    </FooterTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="City/Days">
                                                                                    <EditItemTemplate>
                                                                                        <asp:TextBox ID="gv_txtDays" CssClass="form-control input-small" onkeyup="javasript:Calc_Grid2(this)" runat="server" MaxLength="10" Text='<%#Eval("Days") %>' />
                                                                                        <asp:RequiredFieldValidator ID="RFV_gv_txtDays" ValidationGroup="Editvalidation" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="gv_txtDays"></asp:RequiredFieldValidator>

                                                                                    </EditItemTemplate>
                                                                                    <ItemTemplate>
                                                                                        <%# DataBinder.Eval(Container.DataItem,"Days") %>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <asp:TextBox runat="server" ID="txtDays" onkeyup="javascript:Calc_Grid();" CssClass="form-control input-small" MaxLength="3" TabIndex="5" autocomplete="off"></asp:TextBox>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtDays" ErrorMessage="*" ForeColor="Red" ValidationGroup="EG" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                                    </FooterTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Pre Event Cost">
                                                                                    <EditItemTemplate>
                                                                                        <asp:TextBox ID="gv_txtPreEventCost" CssClass="form-control input-small" ReadOnly="true" MaxLength="10" onkeyup="javascript:Calc_Grid2(this)" runat="server" Text='<%#Eval("PreEventCost") %>' />
                                                                                        <asp:RequiredFieldValidator ID="RF_gv_txtPreEventCost" ValidationGroup="Editvalidation" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="gv_txtPreEventCost"></asp:RequiredFieldValidator>

                                                                                    </EditItemTemplate>
                                                                                    <ItemTemplate>
                                                                                        <%# DataBinder.Eval(Container.DataItem, "PreEventCost")%>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <asp:TextBox ID="txtPreEventCost" runat="server" ReadOnly="true" CssClass="form-control input-small" onkeyup="javascript:Calc_Grid()" MaxLength="10" TabIndex="16"></asp:TextBox>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="txtPreEventCost" ValidationGroup="PC"></asp:RequiredFieldValidator>
                                                                                    </FooterTemplate>
                                                                                </asp:TemplateField>


                                                                                <asp:TemplateField HeaderText="Service Tax / VAT (in %)" Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <%# DataBinder.Eval(Container.DataItem, "PreEventServiceTax")%>
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
                                                                <tr style="display: none;">
                                                                    <td class="tdhead">Total Service Tax / VAT</td>
                                                                    <td colspan="3">
                                                                        <asp:Label ID="lblplTSTax" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr style="display: none;">
                                                                    <td class="tdhead">Pre Event Total</td>
                                                                    <td colspan="3">
                                                                        <asp:Label ID="lblplPETotal" runat="server"></asp:Label>


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
</div>
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
                                                Pre-PnL Manager
                                                <button type="button" class="btn btn-primary btn-sm pull-right" data-toggle="modal" data-target="#dialog-box-PrePnl">View more</button>
                                            </div>
                                            <div class="panel-body">
                                                <div class="col-lg-3">
                                                    <b>Client :</b><asp:Label runat="server" ID="lblClient" Text=""></asp:Label>
                                                </div>
                                                <div class="col-lg-3">
                                                    <b>Project Manager :</b><asp:Label runat="server" ID="lblPM" Text=""></asp:Label>
                                                </div>
                                                <div class="col-lg-3">
                                                    <b>KAM :</b>
                                                    <asp:Label runat="server" ID="lblKAM" Text=""></asp:Label>
                                                </div>
                                                <div class="col-lg-3">
                                                    <b>Pre Event Quote :</b><asp:Label runat="server" ID="lblPEQ" Text=""></asp:Label>
                                                </div>
                                                <div class="col-lg-3">
                                                    <b>Pre Event Total :</b><asp:Label runat="server" ID="lblPreEventtotal" Text=""></asp:Label>
                                                </div>


                                            </div>

                                        </div>
                                    </div>
                                    <div class="col-lg-12">

                                        <div class="alert alert-warning" id="divwarning" runat="server">
                                            <strong>Warning: </strong>
                                            <asp:Label ID="lblwarning" runat="server"></asp:Label>


                                        </div>
                                        <div class="alert alert-success" id="Messagebranchhead" runat="server">
                                            <strong>Message:  </strong>
                                            Are you sure want to send request to Branch Head?
                          <br />
                                            <asp:TextBox ID="txtreason" runat="server" Text='' TextMode="MultiLine" placeholder="Please Specify Reason" CssClass="form-control input-small" /><asp:RequiredFieldValidator ID="RequiredFieldVal" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="txtreason" SetFocusOnError="true" ValidationGroup="SA"></asp:RequiredFieldValidator><br />
                                            <asp:Button ID="btncncl" runat="server" Text="Cancel" class="btn btn-primary btn-sm pull-right" />

                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnsendrequesttobranchhead" class="btn btn-primary btn-sm pull-right" ValidationGroup="SA" runat="server" Text="Send" />
                                        </div>
										  
                                        <div class="col-lg-12 table-responsive" id="trGridEstimate" runat="server">
                                            <asp:GridView runat="server" ID="gdvEstimate" CssClass="table table-bordered table-striped" AutoGenerateColumns="false" AllowPaging="true" PageSize="50" ShowFooter="true">
                                                <Columns>

                                                    <asp:TemplateField HeaderText="ID">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                            <asp:HiddenField runat="server" ID="hdnEstimateID" Value='<%# Bind("EstimateID")%>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Category">
                                                        <EditItemTemplate>
                                                            <asp:DropDownList ID="gv_ddlCategory" CssClass="form-control input-small" runat="server" />
                                                            <asp:RequiredFieldValidator ID="RFV_gv_txtParticulars11" ValidationGroup="Editvalidation" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="gv_ddlCategory" InitialValue="0"></asp:RequiredFieldValidator>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container.DataItem, "Category")%>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:DropDownList runat="server" ID="ddlCategory" MaxLength="40" CssClass="form-control input-small" TabIndex="1"></asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator123" runat="server" ErrorMessage="*" ControlToValidate="ddlCategory" ForeColor="Red" ValidationGroup="EG" SetFocusOnError="true" InitialValue="0"></asp:RequiredFieldValidator>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Particulars">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="gv_txtParticulars" CssClass="form-control input-small" runat="server" MaxLength="50" Text='<%#Eval("Particulars") %>' />
                                                            <asp:RequiredFieldValidator ID="RFV_gv_txtParticulars" ValidationGroup="Editvalidation" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="gv_txtParticulars"></asp:RequiredFieldValidator>

                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container.DataItem, "Particulars")%>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:TextBox runat="server" ID="txtParticulars" CssClass="form-control input-small" MaxLength="40" TabIndex="2"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtParticulars" ForeColor="Red" ValidationGroup="EG" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Quantity">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="gv_txtquantity" CssClass="form-control input-small" onkeyup="javasript:Calc_Grid2(this)" runat="server" MaxLength="7" Text='<%#Eval("Quantity") %>' />
                                                            <asp:RequiredFieldValidator ID="RFV_gv_txtquantity" ValidationGroup="Editvalidation" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="gv_txtquantity"></asp:RequiredFieldValidator>

                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container.DataItem, "Quantity")%>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:TextBox runat="server" ID="txtquantity" CssClass="form-control input-small" onkeyup="javascript:Calc_Grid();" MaxLength="10" TabIndex="3" autocomplete="off"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="txtquantity" ForeColor="Red" ValidationGroup="EG" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Unit Price">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="gv_txtRate" CssClass="form-control input-small" onkeyup="javasript:Calc_Grid2(this)" runat="server" MaxLength="10" Text='<%#Eval("Rate") %>' />
                                                            <asp:RequiredFieldValidator ID="RFV_gv_txtRate" ValidationGroup="Editvalidation" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="gv_txtRate"></asp:RequiredFieldValidator>

                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container.DataItem, "Rate")%>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:TextBox runat="server" ID="txtRate" CssClass="form-control input-small" onkeyup="javascript:Calc_Grid();" MaxLength="10" TabIndex="4" autocomplete="off"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ControlToValidate="txtRate" ForeColor="Red" ValidationGroup="EG" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="City/Days">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="gv_txtDays" CssClass="form-control input-small" onkeyup="javasript:Calc_Grid2(this)" runat="server" MaxLength="10" Text='<%#Eval("Days") %>' />
                                                            <asp:RequiredFieldValidator ID="RFV_gv_txtDays" ValidationGroup="Editvalidation" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="gv_txtDays"></asp:RequiredFieldValidator>

                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container.DataItem,"Days") %>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:TextBox runat="server" ID="txtDays" onkeyup="javascript:Calc_Grid();" CssClass="form-control input-small" MaxLength="3" TabIndex="5" autocomplete="off"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtDays" ErrorMessage="*" ForeColor="Red" ValidationGroup="EG" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Estimate">
                                                        <EditItemTemplate>
                                                            <asp:Label ID="gv_txtEstimate" CssClass="form-control input-small" runat="server" MaxLength="10" Text='<%#Eval("Estimate") %>'></asp:Label>

                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <%--<asp:Label ID="lblEstimate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Estimate") %>'></asp:Label>--%>
                                                            <asp:TextBox runat="server" ID="txtEstimate1" ReadOnly="true" MaxLength="10" TabIndex="6" autocomplete="off" Text='<%# DataBinder.Eval(Container.DataItem,"Estimate") %>' CssClass="form-control input-small"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:TextBox runat="server" ID="txtEstimate" ReadOnly="true" MaxLength="10" TabIndex="6" autocomplete="off" CssClass="form-control input-small"></asp:TextBox>
                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtEstimate" ErrorMessage="*" ForeColor="Red" ValidationGroup="EG" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Agency Fee(%)">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="gv__agencyfee" CssClass="form-control input-small" onkeyup="javasript:Calc_Grid2(this)" runat="server" MaxLength="5" Text='<%#Eval("AgencyFee") %>' />

                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txt_agencyfees" onkeyup="javascript:validdata(this)" ReadOnly="true" runat="server" Text='<%# Eval("AgencyFee")%>' MaxLength="5" CssClass="form-control input-small"></asp:TextBox>

                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:TextBox ID="txt_agencyfee" onkeyup="javascript:validdata(this)" runat="server" Text='<%# Eval("AgencyFee")%>' MaxLength="5" CssClass="form-control input-small"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txt_agencyfee" ErrorMessage="*" ForeColor="Red" ValidationGroup="EG" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Remarks">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="gv_txtRemarks" CssClass="form-control input-small" runat="server" MaxLength="50" Text='<%#Eval("Remarks") %>' />

                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRemarks" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Remarks")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:TextBox runat="server" ID="txtRemarks" MaxLength="100" CssClass="form-control input-small" TabIndex="7"></asp:TextBox>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action" ItemStyle-Width="140px">
                                                        <EditItemTemplate>
                                                            <asp:Button ValidationGroup="Editvalidation" ID="imgbtnUpdate" CommandName="Update" runat="server" Text="Update" CssClass="btn btn-sm btn-info" />
                                                            <asp:Button CausesValidation="false" ID="imgbtnCancel" runat="server" CommandName="Cancel" Text="Cancel" CssClass="btn btn-sm btn-danger" />
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Button CausesValidation="false" ID="imgbtnEdit" CommandName="Edit" runat="server" OnClientClick="javasript:GVcalculation()" Text="Edit" CssClass="btn btn-sm btn-info" />
                                                            <asp:Button CausesValidation="false" ID="imgbtnDelete" OnClientClick="javascript:return ConfirmationBox()" CommandName="delete" Text="Delete" runat="server" CssClass="btn btn-sm btn-danger" />
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Button runat="server" ID="btnAdd" CssClass="btn btn-sm btn-primary" CommandName="add" Text="Add" Width="70px" ValidationGroup="EG" TabIndex="8" /></td>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div class="col-lg-12" id="trGridDisplay" runat="server">
                                            <asp:GridView runat="server" ID="gvDisplay" CssClass="table table-bordered table-striped" AutoGenerateColumns="false" Width="100%">
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

                                                    <asp:TemplateField HeaderText="City/Days">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDays" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Days")%>'></asp:Label>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Agency Fee(%)">
                                                        <ItemTemplate>
                                                            <%--<asp:TextBox ID="txt_agencyfees" onkeyup="javascript:validdata(this)" ReadOnly="true"  runat="server" Text='<%# Eval("AgencyFee")%>' MaxLength="5" CssClass="form-control input-small"></asp:TextBox>--%>
                                                            <asp:Label ID="lblAfee" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AgencyFee")%>'></asp:Label>
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

                                        </div>

                                        <asp:Panel ID="Panel1" runat="server" Visible="true">
                                            <div class="col-lg-6">
                                                <div class="col-lg-12 form-group">
                                                    <span class="col-lg-6">

                                                        <asp:Label runat="server" ID="lbl1">Sub Total :</asp:Label></span>
                                                    <span class="col-lg-6">
                                                        <asp:Label runat="server" ID="lblSubTotal"></asp:Label></span>
                                                </div>
                                                <div class="col-lg-12 form-group">
                                                    <span class="col-lg-6">Management Fees :<asp:TextBox Visible="false" ID="txtMFeePer" runat="server" CssClass="form-control" TabIndex="8" autocomplete="off" onkeyup="javascript:CalTotalMFeePer();">(</asp:TextBox><asp:Label ID="lblMFeePer" runat="server"></asp:Label>)<%--(%)--%></span><span class="col-lg-6"><asp:Label ID="lblMangnmtFees" runat="server"></asp:Label>
                                                        <asp:TextBox runat="server" Visible="false" ID="txtMangnmtFees" Text="0" onkeyup="javascript:CalTotal();" MaxLength="10" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtMangnmtFees" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                        <%--<asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtMangnmtFees" ErrorMessage="Invalid Fee" ForeColor="Red" MaximumValue="999999999999" MinimumValue="1" Type="Double" SetFocusOnError="true"></asp:RangeValidator>--%>
                                                    </span>
                                                </div>
                                                <div class="col-lg-12 form-group">
                                                    <span class="col-lg-6">
                                                        <asp:Label runat="server" ID="Label2">Total :</asp:Label></span>
                                                    <span class="col-lg-6">
                                                        <asp:Label runat="server" ID="lblTotal"></asp:Label></span>
                                                </div>
                                                <div class="col-lg-12 form-group">
                                                    <span class="col-lg-6">Service Tax / VAT : (
                                        <%--<asp:TextBox ID="txtServiceTax" Visible="false" runat="server" TabIndex="9" CssClass="form-control input-small" onkeyup="CalTotal1();" autocomplete="off"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*" ControlToValidate="txtServiceTax" ForeColor="red"></asp:RequiredFieldValidator><asp:Label ID="lblServiceTaxPer" runat="server" Text='<%=ConfigurationManager.AppSettings("Servicetax").ToString()%>' SetFocusOnError="true"></asp:Label>--%>
                                                        <asp:DropDownList ID="ddlservicetax" onchange="GVcalculation()" runat="server">
                                                            <%--<asp:ListItem>12.36</asp:ListItem>--%>
                                                            <asp:ListItem>15.00</asp:ListItem>
                                                           <%-- <asp:ListItem>14.50</asp:ListItem>--%>
                                                        </asp:DropDownList>
                                                        %) </span>
                                                    <span class="col-lg-6">
                                                        <asp:Label ID="lblServiceTax" runat="server"></asp:Label>
                                                    </span>
                                                </div>
                                                <div class="col-lg-12 form-group">
                                                    <span class="col-lg-6">
                                                        <asp:Label runat="server" ID="Label4">Grand Total :</asp:Label>
                                                    </span>
                                                    <span class="col-lg-6">
                                                        <asp:Label runat="server" ID="lblGrandTotal"></asp:Label></span>

                                                </div>
                                                <div class="col-lg-12">
                                                    <div id="trAppRemarks" runat="server">
                                                        <div class="col-lg-6">
                                                            Remarks:
                                                        </div>

                                                        <div class="col-lg-6">
                                                            <div class="col-lg-12 pull-right">
                                                                <asp:TextBox ID="txtRemarks" runat="server" Text="" TextMode="MultiLine" CssClass="form-control" TabIndex="24"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-6">
                                                <h3 class="col-lg-12">Estimated v/s Actual Sheet</h3>
                                                <div class="col-lg-12">
                                                    <table border="1" class="table table-bordered table-striped">
                                                        <tr>
                                                            <td class="tdhead">Estimate</td>
                                                            <td class="tdhead">Pre P&L</td>
                                                            <td class="tdhead">Profit (%)</td>
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
                                                <center>
                                    
                                    <asp:HiddenField ID="hdnPnLID" runat="server" />
                                 <div style="display:none;">
                                    <asp:Button runat="server" ID="btnCancel" Text="Back" CssClass="btn btn-primary" visible="false" CausesValidation="false" TabIndex="9" />
                                    <asp:Button runat="server" ID="btnCancelApp" Text="Back" CssClass="btn btn-primary" visible="false" CausesValidation="false" TabIndex="9" />
                                    <asp:Button ID="btnCancelHome" runat="server" Text="Back" CausesValidation="false" visible="false" CssClass="btn btn-primary" TabIndex="9" />
                                     </div>
                                                   <%-- <div style="display:none;">--%>
                                 <a href='javascript:history.go(-1)' style="display:none;" class="btn btn-primary">Back</a>
                                    <asp:Button runat="server" ID="btnAdd"  Text="Save"  CssClass="btn btn-primary" TabIndex="10"  />
                                                    
                                    <asp:Button runat="server" ID="btnFinal" Text="Create Job Card" CssClass="btn btn-primary" TabIndex="11" />
                                    <asp:Button runat="server" ID="btnExcel" Text="Export To Excel" CssClass="btn btn-primary" style="display:none;"  TabIndex="12"  /> 
                                                        <asp:Button runat="server" ID="Button1" Text="Export To Excel" CssClass="btn btn-primary" TabIndex="12" Visible="false" />
                                                        <%--</div>--%> 
                                </center>
                                            </div>
                                        </asp:Panel>

                                    </div>

                                </div>
                                <div class="InnerContentData">

                                    <div id="trAppButtons" class="col-lg-12" runat="server">


                                        <div class="form-group col-lg-6" style="display: block">
                                            <h3 class="col-lg-12">Estimated v/s Actual Sheet History</h3>
                                            <div class="col-lg-12">

                                                <asp:GridView ID="gridfinalesimate" class="table table-bordered table-striped" runat="server" AutoGenerateColumns="false">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No.">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="EstimateTotal" HeaderText="Estimate" />
                                                        <asp:BoundField DataField="PrepnlTotal" HeaderText="Pre P&L" />
                                                        <asp:BoundField DataField="ProfitPerc" HeaderText="Profit (%)" />
                                                        <asp:BoundField DataField="InsertedOn" HeaderText="InsertedON" />
                                                        <asp:BoundField DataField="ReasonFromKam" HeaderText="Reason From Kam" />
                                                        <asp:BoundField DataField="IsApproved" HeaderText="Approval Status" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="col-lg-12">

                                                <asp:Button ID="btnApproval" runat="server" Text="Approve" CausesValidation="false" CssClass="btn btn-sm btn-primary" TabIndex="25" />
                                                <asp:Button ID="btnRejected" runat="server" Text="Reject" OnClientClick="javascript:return RejectConfirmationBox()" CausesValidation="false" CssClass="btn btn-sm btn-primary" TabIndex="26" />
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
        </div>
        </div>
    </section>
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

    <asp:HiddenField runat="server" ID="hdnjcAmount" />
    <asp:HiddenField runat="server" ID="hdnactual" />
    <asp:HiddenField runat="server" ID="hdntempestimateTotal" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpFooter" runat="Server">
    <script type="text/javascript">

        $(document).ready(function () {
            $('#<%=divError.ClientID%>').hide();
            $("input[id$=txtParticulars]").focus();
            GVcalculation();
        });

        function Calc_Grid() {

            var val1 = false;
            var val2 = false;
            var val3 = false;

            if ($('input[id$=txtquantity]').val() != "") {

                var numberRegex = /^\d{1,10}(\.\d{0,2})?$/;
                if (numberRegex.test($('input[id$=txtquantity]').val())) {
                    val1 = true

                }
                else {
                    alert('Enter numeric data only');
                    $('input[id$=txtquantity]').val("");
                }

            }

            if ($('input[id$=txtRate]').val() != "") {

                var numberRegex = /^\d{1,10}(\.\d{0,2})?$/;
                if (numberRegex.test($('input[id$=txtRate]').val())) {
                    val2 = true;
                }
                else {
                    alert('Enter numeric data only');
                    $('input[id$=txtRate]').val("");
                }
            }

            if ($('input[id$=txtDays]').val() != "") {

                var numberRegex = /^\d{1,10}(\.\d{0,2})?$/;
                if (numberRegex.test($('input[id$=txtDays]').val())) {
                    val3 = true;
                }
                else {
                    alert('Enter numeric data only');
                    $('input[id$=txtDays]').val("");
                }
            }

            if (val1 == true && val2 == true && val3 == true) {
                var eventcost = parseFloat($('input[id$=txtquantity]').val());
                var eventtax = parseFloat($('input[id$=txtRate]').val());
                var eventdays = parseFloat($('input[id$=txtDays]').val());

                var total = (eventcost * eventtax * eventdays).toFixed(2);
                $('input[id$=txtEstimate]').val(total);
                //$("[id*=txtPreEventTotal]").html(total);
            }

            GVcalculation();
        }


        function Calc_Grid2(obj) {

            var val1 = false;
            var val2 = false;
            var val3 = false;
            var varid = obj.id
            var stringArray = new Array();
            stringArray = varid.split("_");


            var postfix = stringArray[stringArray.length - 1];
            postfix = "_" + postfix

            if ($('input[id$=gv_txtquantity' + postfix + ']').val() != "") {

                var numberRegex = /^\d{1,10}(\.\d{0,2})?$/;
                if (numberRegex.test($('input[id$=gv_txtquantity' + postfix + ']').val())) {
                    val1 = true

                }
                else {
                    alert('Enter numeric data only');
                    $('input[id$=gv_txtquantity' + postfix + ']').val("");

                }

            }

            if ($('input[id$=gv_txtRate' + postfix + ']').val() != "") {

                var numberRegex = /^\d{1,10}(\.\d{0,2})?$/;
                if (numberRegex.test($('input[id$=gv_txtRate' + postfix + ']').val())) {
                    val2 = true;
                }
                else {
                    alert('Enter numeric data only');
                    $('input[id$=gv_txtRate' + postfix + ']').val("");
                }
            }
            //else {
            //    alert('Enter value');
            //    $('input[id$=txtRate]').val("");
            //}

            if ($('input[id$=gv_txtDays' + postfix + ']').val() != "") {

                var numberRegex = /^\d{1,10}(\.\d{0,2})?$/;
                if (numberRegex.test($('input[id$=gv_txtDays' + postfix + ']').val())) {
                    val3 = true;
                }
                else {
                    alert('Enter numeric data only');
                    $('input[id$=gv_txtDays' + postfix + ']').val("");
                }
            }

            if (val1 == true && val2 == true && val3 == true) {

                var eventcost = parseFloat($('input[id$=gv_txtquantity' + postfix + ']').val());
                var eventtax = parseFloat($('input[id$=gv_txtRate' + postfix + ']').val());
                var eventdays = parseFloat($('input[id$=gv_txtDays' + postfix + ']').val());

                var total = (eventcost * eventtax * eventdays).toFixed(2);

                //  alert(total);
                $('span[id$=gv_txtEstimate' + postfix + ']').val(total);
                $('span[id$=gv_txtEstimate' + postfix + ']').text(total);
                //$('input[id$=txtEstimate]').val(total);
            }
            GVcalculation();
        }

        function GVcalculation() {

            var GVMaintainReceiptMaster = document.getElementById('<%= gdvEstimate.ClientID%>');
            var subtotal = "0";
            var manfee = "0";
            var servicerax = $('#<%= ddlservicetax.ClientID%>').val();

            for (var rowId = 1; rowId < GVMaintainReceiptMaster.rows.length; rowId++) {
                //6 estimate
                //7 agency fee
                var txtFE = GVMaintainReceiptMaster.rows[rowId].cells[6].children[0];
                var txtAF = GVMaintainReceiptMaster.rows[rowId].cells[7].children[0];
                subtotal = Math.round(subtotal) + Math.round(txtFE.value);

                //alert($('input[id$=txtEstimate]').val());
                //alert(Math.round(txtAF.value));


                //manfee = Math.round(manfee) + ((Math.round(txtAF.value) * Math.round(txtFE.value)) / 100);
                manfee = parseFloat(manfee) + ((txtAF.value * txtFE.value) / 100);
                // alert(manfee.toFixed(2));

                $('#<%=lblSubTotal.ClientID%>').html('' + subtotal.toFixed(2) + '');
                // $('#<%=hdnsubtotal.ClientID%>').val('' + subtotal.toFixed(2) + '');
                $('#<%=lblMangnmtFees.ClientID%>').html('' + manfee.toFixed(2) + '');
                //$('#<%=hdnMangnmtFees.ClientID%>').val('' + manfee.toFixed(2) + '');
                $('#<%=lblTotal.ClientID%>').html('' + (manfee + subtotal) + '');
                //$('#<%=hdnTotal.ClientID%>').val('' + (manfee + subtotal) + '');


                $('#<%=lblServiceTax.ClientID%>').html('' + (((manfee + subtotal) * servicerax) / 100).toFixed(2) + '');
                //$('#<%=hdnServiceTax.ClientID%>').val('' + (((manfee + subtotal) * servicerax) / 100).toFixed(2) + '');
                $('#<%=lblGrandTotal.ClientID%>').html('' + ((manfee + subtotal) + (((manfee + subtotal) * servicerax) / 100)).toFixed(2) + '');
                //$('#<%=hdnGrand.ClientID()%>').val('' + ((manfee + subtotal) + (((manfee + subtotal) * servicerax) / 100)).toFixed(2) + '');

                $('#<%=txtEstimate.ClientID%>').html($('#<%=lblTotal.ClientID%>').html());
                // alert($('#<%=lblTotal.ClientID%>').html());

                $('#<%=txtActual.ClientID%>').html('' + ((($('#<%=txtEstimate.ClientID%>').html() - $('#<%=txtPrePnL.ClientID%>').html()) / $('#<%=txtEstimate.ClientID%>').html()) * 100).toFixed(2) + '')
                $('#<%=hdnactual.ClientID%>').val('' + ((($('#<%=txtEstimate.ClientID%>').html() - $('#<%=txtPrePnL.ClientID%>').html()) / $('#<%=txtEstimate.ClientID%>').html()) * 100).toFixed(2) + '')


                if (isNaN($('#<%=lblTotal.ClientID%>').html()) == false) {
                    //alert(parseInt($('#<%=lblTotal.ClientID%>').html()) > parseInt($('#<%= hdnjcAmount.ClientID%>').val()));
                    if ($('#<%=lbljcno.ClientID%>').html().charAt(0) != 'K') {
                        if (parseInt($('#<%=lblTotal.ClientID%>').html()) > parseInt($('#<%= hdnjcAmount.ClientID%>').val())) {
                            //alert('Estimate total(amount) can not be greater than client approved po (amount) to increase the estimate amount kindly upload the revised po and wait for finance approval.')

                            $('#<%=divError.ClientID%>').show();
                            $('#<%=lblError.ClientID%>').html('Estimate total Rs. ' + $('#<%=lblTotal.ClientID%>').html() + ' can not be greater than client approved PO Rs. ' + $('#<%= hdnjcAmount.ClientID%>').val() + ' to increase the estimate amount kindly upload the revised PO and wait for finance approval.')

                  <%--      var GVMaintainReceiptMaster = document.getElementById('<%= gdvEstimate.ClientID%>');
                        for (var rowId = 1; rowId < GVMaintainReceiptMaster.rows.length; rowId++) {

                            // GVMaintainReceiptMaster.rows[rowId].cells[6].children[0]

                          
                        }--%>

                            $("input[type=submit]").hide();
                            $('html,body').animate({
                                scrollTop: 0
                            }, 700);

                        }
                        else {
                            $('#<%=divError.ClientID%>').hide();
                            $('#<%=lblError.ClientID%>').html('');
                            $("input[type=submit]").show();
                        }

                    }
                };



                //alert(txtFE.value + ' and ' + txtAF.value );
            }
            // getTotalEmployeeSalary()

            return false;
        }

        function CalTotal() {
            if (CalMFee() == true) {
                var EstTotal = document.getElementById('<%=lblSubTotal.ClientID%>').textContent;
                var ManagementFees = document.getElementById('<%=txtMangnmtFees.ClientID%>').value;

                var CalculatedTotal = parseFloat(EstTotal) + parseFloat(ManagementFees)
                //alert(CalculatedTotal);
                document.getElementById('<%=lblTotal.ClientID%>').textContent = CalculatedTotal.toFixed(2);

            }
        }


        function CalMFee() {
            if ($('input[id$=txtMangnmtFees]').val() != "") {

                var numberRegex = /^\d{1,10}(\.\d{0,2})?$/;
                if (numberRegex.test($('input[id$=txtMangnmtFees]').val())) {
                    return true;
                }
                else {
                    alert('Enter numeric data only');
                    $('input[id$=txtMangnmtFees]').val("");
                    return false;
                }
            }
        }


        function CalSTAx() {
            if ($('input[id$=txtServiceTax]').val() != "") {

                var numberRegex = /^\d{1,3}(\.\d{0,2})?$/;
                if (numberRegex.test($('input[id$=txtServiceTax]').val())) {
                    return true;
                }
                else {
                    alert('Enter numeric data only');
                    $('input[id$=txtServiceTax]').val("");
                    return false;
                }
            }
            else {

                $('input[id$=txtServiceTax]').val("0.00");
                CalTotal1();
            }

        }

        function CalMFeePer() {
            if ($('input[id$=txtMFeePer]').val() != "") {

                var numberRegex = /^\d{1,10}(\.\d{0,2})?$/;
                if (numberRegex.test($('input[id$=txtMFeePer]').val())) {
                    return true;
                }
                else {
                    alert('Enter numeric data only');
                    $('input[id$=txtMFeePer]').val("");
                    return false;
                }
            }
            else {
                $('input[id$=txtMFeePer]').val("0.00");
                $('#CPH_Main_txtMangnmtFees').val("0.00");
                CalTotalMFeePer();
            }
        }

        function CalTotalMFeePer() {
            if (CalMFeePer() == true) {
                var EstTotal = document.getElementById('<%=lblSubTotal.ClientID%>').textContent;
                var ManagementFeesPer = document.getElementById('<%=txtMFeePer.ClientID%>').value;
                var rate = parseFloat(ManagementFeesPer) / 100;
                var CalculatedTotal = parseFloat(EstTotal) * rate;
                //var CTotalTrim_f = CalculatedTotal.substring(0,indexOf('.'));
                //var CTotalTrim_d = CalculatedTotal.substring(indexOf('.'));
                //alert(EstTotal.toFixed(2) + CalculatedTotal.toFixed(2));
                $('#CPH_Main_txtMangnmtFees').val(CalculatedTotal.toFixed(2));
                //alert("");
                document.getElementById('<%=lblTotal.ClientID%>').textContent = parseFloat(EstTotal) + parseFloat(CalculatedTotal);
                document.getElementById('<%=lblGrandTotal.ClientID%>').textContent = parseFloat(EstTotal) + parseFloat(CalculatedTotal);
                // CalTotal1();
                GVcalculation();

            }
        }

        function validdata(obj) {
            var id = obj.id;
            var val1 = false;
            var val2 = false;

            if ($('#' + id).val() != "") {

                var numberRegex = /^\d{1,10}(\.\d{0,2})?$/;
                if (numberRegex.test($('#' + id).val())) {
                    //return true;
                    GVcalculation()
                    return true;
                }
                else {
                    alert('Enter numeric data only');
                    $('#' + id).val("");
                    return false;
                }
            }
        }



    </script>

    <script>

        $(document).ready(function () {

            // if user clicked on button, the overlay layer or the dialogbox, close the dialog	
            $('a.button').click(function () {
                $('#dialog-overlay, #dialog-box').hide();
                return false;
            });
            $('a.button').click(function () {
                $('#dialog-overlay, #dialog-box2').hide();
                return false;
            });
            // if user resize the window, call the same function again
            // to make sure the overlay fills the screen and dialogbox aligned to center	
            $(window).resize(function () {

                //only do it if the dialog box is not hidden
                if (!$('#dialog-box').is(':hidden')) popup();
            });


        });





    </script>
</asp:Content>

