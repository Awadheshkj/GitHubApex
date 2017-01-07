<%@ Page Title="" Language="VB" MasterPageFile="~/Apex.master" AutoEventWireup="false" CodeFile="PrePnLApproval.aspx.vb" Inherits="OpenPrePnLManager" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Pre P&L Manager
        </h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">Pre P&L Manager</li>
        </ol>
    </section>

    <!-- Main content -->
    <section class="content">
        <div class="row">
            <div class="col-lg-xs-12">
                <div class="box box-primary">
                    <%--<div class="box-header">
                        <h3>Pre P&L Manager</h3>
                    </div>--%>

                    <div class="box-body">

                        <div id="divError" runat="server" class="ui-state-error ui-corner-all divError">
                            <p>
                                <span class="ui-icon ui-icon-alert" style="float: left; margin-right: .3em;"></span>
                                <strong>Warning: </strong>
                                <asp:Label ID="lblError" runat="server"></asp:Label>
                            </p>
                        </div>
                        <div class="alert alert-success" id="divMsgAlert" runat="server">
                            <strong>Message: </strong>
                            <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                        </div>
                        <div id="MessageDiv" runat="server" class="ui-state-msg ui-corner-all divError">
                            <p>

                                <strong>Message: </strong>
                                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                            </p>
                        </div>
                        <div id="divContent" runat="server">
                            <div class="inner-Content-container">
                                <div class="InnerContentData">
                                    <div class="modal fade" id="dialog-box-Brief">
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
                                                                        <asp:Label ID="lblbrcclient" runat="server"></asp:Label>

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

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="panel panel-info">
                                        <div class="panel-heading clearfix">
                                            Brief Manager
                                                <button type="button" class="btn btn-primary btn-sm pull-right" data-toggle="modal" data-target="#dialog-box-Brief">View more</button>
                                        </div>
                                        <div class="panel-body">
                                            <div class="col-lg-3">
                                                <span>Brief Name : </span>
                                                <asp:Label runat="server" ID="lblbriefname" Text=""></asp:Label>
                                            </div>
                                            <div class="col-lg-3">
                                                <span>Client : </span>
                                                <asp:Label runat="server" ID="lblbriefClient" Text=""></asp:Label>
                                            </div>

                                            <div class="col-lg-3">
                                                <span>Primary Activity : </span>
                                                <asp:Label runat="server" ID="lblbriefPrimaryActivity" Text=""></asp:Label>
                                            </div>
                                            <div class="col-lg-3">
                                                <span>Budget : </span>
                                                <asp:Label runat="server" ID="lblbriefBudget" Text=""></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-12">

                                        <div class="form-group">
                                            <label class="col-lg-1 control-label" for="head"></label>
                                            <div class="col-lg-10" style="display: none;">
                                                <div class="bs-example form-horizontal">

                                                    <div class="form-group">
                                                        <div class="col-lg-4">
                                                            <label for="inputEmail" class="control-label">Client Name</label>
                                                        </div>
                                                        <div class="col-lg-2">
                                                            <asp:Label ID="lblClient" runat="server"></asp:Label>
                                                            <asp:DropDownList ID="ddlClient" runat="server" ValidationGroup="PE" Enabled="False" TabIndex="1" CssClass="form-control">
                                                                

                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="ddlClient" ForeColor="Red" InitialValue="0" ValidationGroup="PE"></asp:RequiredFieldValidator>

                                                        </div>
                                                    </div>



                                                    <div class="form-group">
                                                        <div class="col-lg-4">
                                                            <label for="inputEmail" class="control-label">Activity Name</label>
                                                        </div>
                                                        <div class="col-lg-2">
                                                            <asp:Label ID="lblEventName" runat="server"></asp:Label>
                                                            <asp:TextBox ID="txtEventName" runat="server" CssClass="form-control" MaxLength="100" TabIndex="2"></asp:TextBox>

                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <div class="col-lg-4">
                                                            <label for="inputEmail" class="control-label">Activity Date</label>

                                                        </div>

                                                        <div class="col-lg-2">
                                                            <asp:Label ID="lblEventDate" runat="server"></asp:Label>
                                                            <asp:TextBox ID="txtEventDate" runat="server" onkeypress="return false" onkeyup="return false" onkeydown="return false" CssClass="form-control" TabIndex="3"></asp:TextBox>
                                                        </div>
                                                    </div>


                                                    <div class="form-group">
                                                        <div class="col-lg-4">
                                                            <label for="inputEmail" class="control-label">Activity Venue</label>

                                                        </div>
                                                        <div class="col-lg-2">
                                                            <asp:Label ID="lblEventVenue" runat="server"></asp:Label>
                                                            <asp:TextBox ID="txtEventVenue" runat="server" CssClass="form-control" TabIndex="4"></asp:TextBox>
                                                        </div>
                                                    </div>


                                                    <div class="form-group">
                                                        <div class="col-lg-4">
                                                            <label for="inputEmail" class="control-label">PO No/Agreement Signed/Mail Approval</label>

                                                        </div>
                                                        <div class="col-lg-2">
                                                            <asp:Label ID="lblApprovalNo" runat="server"></asp:Label>
                                                            <asp:TextBox ID="txtApprovalNo" runat="server" CssClass="form-control" MaxLength="20" TabIndex="5"></asp:TextBox>
                                                        </div>

                                                        <div class="col-lg-2">
                                                            <asp:FileUpload ID="fupl_Mail" runat="server" onchange="javascript:return CheckUpl();" />
                                                            <asp:HyperLink ID="lblfulpl_Mail" runat="server" Text="Download" Target="_blank"></asp:HyperLink>
                                                        </div>
                                                    </div>


                                                    <div class="form-group">
                                                        <div class="col-lg-4">
                                                            <label for="inputEmail" class="control-label">
                                                                Credit Period with Client(in days)</label>

                                                        </div>
                                                        <div class="col-lg-2">
                                                            <asp:Label ID="lblCreditPeriod" runat="server"></asp:Label>
                                                            <asp:TextBox ID="txtCreditPeriod" runat="server" CssClass="form-control" MaxLength="3" onkeyup="return CheckDays()" TabIndex="6" autocomplete="off"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div>
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
                                    </div>
                                    <div id="tbl2" runat="server" class="col-12">
                                        <asp:GridView EnableModelValidation="true" ID="gvPrePnLCost" runat="server" DataKeyNames="Row" AutoGenerateColumns="false" ShowFooter="false" PageSize="5"
                                            OnRowCancelingEdit="gvPrePnLCost_RowCancelingEdit" OnRowDeleting="gvPrePnLCost_RowDeleting"
                                            OnRowEditing="gvPrePnLCost_RowEditing" sonrowcommand="gvPrePnLCost_RowCommand" CssClass="table table-bordered">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No.">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                        <asp:HiddenField ID="hdnPrePnLCostID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "PrePnLCostID")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Category">
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="gv_ddlCategory" runat="server" CssClass="form-control input-small"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredValidator2" ValidationGroup="Editvalidation" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="gv_ddlCategory" InitialValue="0"></asp:RequiredFieldValidator>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container.DataItem, "Category")%>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control input-small"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredValidator20" ValidationGroup="PC" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="ddlCategory" InitialValue="0"></asp:RequiredFieldValidator>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Nature of Expenses">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="gv_txtNatureOfExpenses" runat="server" Text='<%#Eval("NatureOfExpenses") %>' CssClass="form-control input-small" />
                                                        <asp:RequiredFieldValidator ID="RFV_gv_txtNatureOfExpenses" ValidationGroup="Editvalidation" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="gv_txtNatureOfExpenses"></asp:RequiredFieldValidator>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container.DataItem, "NatureOfExpenses")%>
                                                    </ItemTemplate>

                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtNature" runat="server" CssClass="form-control input-small" ValidationGroup="PC" MaxLength="40" TabIndex="14"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="PC" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="txtNature"></asp:RequiredFieldValidator>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Quantity">
                                                    <EditItemTemplate>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="gv_txtquantity" CssClass="form-control input-small" onkeyup="javasript:Calc_Grid2(this)" runat="server" MaxLength="7" Text='<%#Eval("Qty")%>' />
                                                        <asp:RequiredFieldValidator ID="RFV_gv_txtquantity" ValidationGroup="Editvalidation" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="gv_txtquantity"></asp:RequiredFieldValidator>

                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox runat="server" ID="txtquantity" CssClass="form-control input-small" onkeyup="javascript:Calc_Grid();" MaxLength="10" TabIndex="3" autocomplete="off"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="txtquantity" ForeColor="Red" ValidationGroup="EG" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Unit Price">
                                                    <EditItemTemplate>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="gv_txtRate" CssClass="form-control input-small" onkeyup="javasript:Calc_Grid2(this)" runat="server" MaxLength="10" Text='<%#Eval("Rt")%>' />
                                                        <asp:RequiredFieldValidator ID="RFV_gv_txtRate" ValidationGroup="Editvalidation" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="gv_txtRate"></asp:RequiredFieldValidator>

                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox runat="server" ID="txtRate" CssClass="form-control input-small" onkeyup="javascript:Calc_Grid();" MaxLength="10" TabIndex="4" autocomplete="off"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ControlToValidate="txtRate" ForeColor="Red" ValidationGroup="EG" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="City/Days">
                                                    <EditItemTemplate>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="gv_txtDays" CssClass="form-control input-small" onkeyup="javasript:Calc_Grid2(this)" runat="server" MaxLength="10" Text='<%#Eval("Dy")%>' />
                                                        <asp:RequiredFieldValidator ID="RFV_gv_txtDays" ValidationGroup="Editvalidation" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="gv_txtDays"></asp:RequiredFieldValidator>

                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox runat="server" ID="txtDays" onkeyup="javascript:Calc_Grid();" CssClass="form-control input-small" MaxLength="3" TabIndex="5" autocomplete="off"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtDays" ErrorMessage="*" ForeColor="Red" ValidationGroup="EG" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    </FooterTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Pre Event Cost">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="gv_txtPreEventCost" CssClass="form-control input-small" MaxLength="10" onkeyup="javascript:Calc_Grid2(this)" runat="server" Text='<%#Eval("PreEventCost") %>' />
                                                        <asp:HiddenField ID="hdnpreeventcost" runat="server" Value='<%#Eval("PreEventCost") %>' />
                                                        <asp:RequiredFieldValidator ID="RF_gv_txtPreEventCost" ValidationGroup="Editvalidation" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="gv_txtPreEventCost"></asp:RequiredFieldValidator>

                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container.DataItem, "PreEventCost")%>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtPreEventCost" runat="server" CssClass="form-control input-small" onkeyup="javascript:Calc_Grid()" MaxLength="10" TabIndex="16"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="txtPreEventCost" ValidationGroup="PC"></asp:RequiredFieldValidator>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Service Tax (in %)" Visible="false">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="gv_txtPreEventServiceTax" CssClass="form-control input-small" onkeyup="javasript:Calc_Grid2(this)" runat="server" MaxLength="10" Text="0" />
                                                        <asp:RequiredFieldValidator ID="RFV_gv_txtPreEventServiceTax" ValidationGroup="Editvalidation" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="gv_txtPreEventServiceTax"></asp:RequiredFieldValidator>

                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container.DataItem, "PreEventServiceTax")%>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtPreEventServiceTax" runat="server" CssClass="form-control input-small" onkeyup="javasript:Calc_Grid()" ValidationGroup="PC" MaxLength="10" TabIndex="17"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="txtPreEventServiceTax" ValidationGroup="PC"></asp:RequiredFieldValidator>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total" Visible="false">
                                                    <EditItemTemplate>
                                                        <asp:Label ID="gv_txtPreEventTotal" CssClass="form-control input-small" runat="server" Text='<%#Eval("PreEventTotal") %>'></asp:Label>
                                                    </EditItemTemplate>

                                                    <ItemTemplate>

                                                        <%# DataBinder.Eval(Container.DataItem, "PreEventTotal")%>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtPreEventTotal" runat="server" Text="0" ReadOnly="true" CssClass="form-control input-small" MaxLength="10" TabIndex="18"></asp:TextBox>

                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action" ItemStyle-Width="300px">
                                                    <EditItemTemplate>
                                                        <asp:Button ValidationGroup="Editvalidation" ID="imgbtnUpdate" CommandName="Update" runat="server" Text="Update" CssClass="btn btn-info" />
                                                        <asp:Button CausesValidation="false" ID="imgbtnCancel" runat="server" CommandName="Cancel" Text="Cancel" CssClass="btn btn-danger" />

                                                    </EditItemTemplate>
                                                    <ItemTemplate>

                                                        <%--<%# IIf(Eval("REquestAmt") = "0.00", "N/A", "some text")%>--%>

                                                        <%-- <%# If(Eval("REquestAmt").ToString() = "0.00", " ", Eval("REquestAmt").ToString())%> CssClass='btn btn-info'--%>
                                                        <asp:HiddenField ID="hdnapprovalaMt" runat="server" Value='<%# Eval("prevEventTotalCUR")%>' />
                                                        <%--<asp:TextBox ID="TextBox1" runat="server"  Text='<%# Eval("prevEventTotalCUR")  %>' Width="80px" onkeyup="javascript:caltotal(this)"></asp:TextBox>--%>
                                                        <asp:Button CausesValidation='false' ID='imgbtnEdit' runat='server' Text='<%# If(Eval("REquestAmt").ToString() = "0.00", "N/A", Eval("REquestAmt").ToString())%>' Enabled='<%# If(Eval("REquestAmt").ToString() = "0.00", "True", "false")%>' />
                                                        <asp:TextBox ID="txtamt" runat="server" Enabled="false" Text='<%# Eval("REquestAmt")  %>' Width="80px" onkeyup="javascript:caltotal(this)"></asp:TextBox>
                                                        <asp:DropDownList ID="ddlStatus" runat="server">
                                                            <%--<asp:ListItem Value="N">Pending</asp:ListItem>--%>
                                                            <asp:ListItem Value="Y">Approved</asp:ListItem>
                                                            <asp:ListItem Value="R">Reject</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:Button CausesValidation="false" ID="imgbtnDelete" OnClientClick="javascript:return ConfirmationBox()" CommandName="Delete" Text="Delete" runat="server" CssClass="btn btn-danger" Visible="false" />
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Button ID="imgbtnAdd" runat="server" CommandName="Add" ValidationGroup="PC" TabIndex="19" Text="Add" CssClass="btn btn-info" />

                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                            </Columns>

                                        </asp:GridView>

                                    </div>
                                    <div class="alert alert-default pull-right" id="div1" runat="server">
                                        <strong>Why you Increase the prepnl amount ? </strong>
                                        <br />
                                        <strong>Reason: </strong>
                                        <asp:Label runat="server" ID="lblreson"></asp:Label>

                                        <br />
                                        <br />
                                        <div class="pull-right">
                                            <asp:Button ID="btnsendapproval" runat="server" Visible="true" CssClass="btn btn-primary btn-sm" Text="Approved" />
                                        </div>
                                    </div>

                                    <div style="float: right; padding-right: 50px;">
                                    </div>
                                    <div class="form-group">
                                        <label class="col-lg-1 control-label" for="head"></label>
                                        <div class="col-lg-10">
                                            <div class="bs-example form-horizontal">

                                                <div class="form-group" style="display: none;">

                                                    <div class="col-lg-4">
                                                        <label for="inputEmail" class="control-label">Pre Event Quote</label>
                                                    </div>
                                                    <div class="col-lg-2">
                                                        <asp:Label ID="lblPreEventQuote" runat="server"></asp:Label>
                                                        <asp:TextBox ID="txtPreEventQuote" runat="server" CssClass="form-control" MaxLength="10" onkeyup="javascript:CheckPreEventQuote()" TabIndex="20" autocomplete="off">0</asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="col-lg-4">
                                                        <label for="inputEmail" class="control-label">Total Pre Event Cost (Before Approved)</label>
                                                    </div>

                                                    <div class="col-lg-2">
                                                        <asp:Label ID="lblTCost" runat="server"></asp:Label>
                                                        <asp:TextBox ID="lblTotalCost" runat="server" ReadOnly="true" CssClass="form-control" TabIndex="21"></asp:TextBox>
                                                        <asp:Label ID="lblcost" Visible="false" runat="server" Text="00"></asp:Label>
                                                    </div>
                                                </div>


                                                <div class="form-group" style="display: none;">
                                                    <div class="col-lg-4">
                                                        <label for="inputEmail" class="control-label">
                                                            Total Service Tax</label>
                                                    </div>
                                                    <div class="col-lg-2">
                                                        <asp:Label ID="lblTSTax" runat="server"></asp:Label>
                                                        <asp:TextBox ID="lblTotalServiceTax" runat="server" ReadOnly="true" CssClass="form-control" TabIndex="22"></asp:TextBox>
                                                    </div>
                                                </div>



                                                <div class="form-group" style="display: none;">
                                                    <div class="col-lg-4">
                                                        <label for="inputEmail" class="control-label">Pre Event Total</label>
                                                    </div>
                                                    <div class="col-lg-2">
                                                        <asp:Label ID="lblPETotal" runat="server"></asp:Label>
                                                        <asp:TextBox ID="lblPreEventTotal" runat="server" ReadOnly="true" CssClass="form-control" TabIndex="23"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="form-group" style="display: none;">
                                                    <div class="col-lg-4">
                                                        <label for="inputEmail" class="control-label">Pre Event Profit (in %)</label>
                                                    </div>

                                                    <div class="col-lg-2">
                                                        <asp:Label ID="lblPreEventProfit" runat="server" CssClass="form-control" Text="0"></asp:Label>
                                                        <asp:Label ID="lblPreEPr" runat="server"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="col-lg-4" id="trmsg" runat="server">
                                                        <label for="inputEmail" class="control-label">
                                                            <asp:Label runat="server" ForeColor="red" Text="Your Pre P&L Sent Sucessfully For Estimate" ID="lblnotifivationMsg"></asp:Label></label>
                                                    </div>



                                                </div>

                                                <div class="form-group col-lg-6" style="display: block">
                                                    <h3 class="col-lg-12">Estimated v/s Actual Sheet</h3>
                                                    <div class="col-lg-12">
                                                        <table border="1" class="table table-bordered table-striped">
                                                            <tr>
                                                                <td class="tdhead">Estimate</td>
                                                                <td class="tdhead">Pre P&L(After Approved)</td>
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
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="col-lg-4" style="display: none;">
                                                        <asp:Button ID="btnEdit" runat="server" Text="Save" CssClass="btn btn-primary" TabIndex="28" />
                                                        <asp:Button ID="btnCancelKamHome" runat="server" Text="Cancel" CausesValidation="false" CssClass="btn btn-primary" />
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

    <asp:HiddenField ID="hdnPnLID" runat="server" />
    <asp:HiddenField ID="hdnBriefID" runat="server" />
    <asp:HiddenField ID="hdnJobCardID" runat="server" />
    <asp:HiddenField ID="hdnNodificationID" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpFooter" runat="Server">

    <script>
        $(function () {
            $('input[id$=txtEventDate]').datepicker({ dateFormat: 'dd-mm-yyyy' });
        });
        $(document).ready(function () {
            $('input[id$=txtNature]').focus();
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
                $('input[id$=txtamt]').val(total);
            }
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
                //alert(total);
                //  alert(total);
                $('span[id$=txtamt' + postfix + ']').val(total);
                $('span[id$=txtamt' + postfix + ']').text(total);

                $('input[id$=txtamt' + postfix + ']').val(total);
            }
        }

        function CheckDays() {

            if ($('input[id$=txtCreditPeriod]').val() != "") {

                var numberRegex = /^[+-]?\d+(\.\d+)?([eE][+-]?\d+)?$/;
                if (numberRegex.test($('input[id$=txtCreditPeriod]').val())) {
                    return true;
                }
                else {
                    alert('Enter numeric data only');
                    $('input[id$=txtCreditPeriod]').val("");
                    return false;
                }
            }
        }

        function CheckPreEventQuote() {

            if ($('input[id$=txtPreEventQuote]').val() != "") {

                var numberRegex = /^\d{1,10}(\.\d{0,2})?$/;
                if (numberRegex.test($('input[id$=txtPreEventQuote]').val())) {
                    var PreEventQuote = document.getElementById('<%=txtPreEventQuote.ClientID%>').value;
                    var PreEventCost = document.getElementById('<%=lblTotalCost.ClientID%>').value;
                    var prepnlprofit = ((parseFloat(PreEventQuote) - parseFloat(PreEventCost)) / parseFloat(PreEventQuote)) * 100;
                    //alert(prepnlprofit);
                    document.getElementById('<%=lblPreEventProfit.ClientID%>').textContent = prepnlprofit.toFixed(2);
                }
                else {
                    alert('Enter numeric data only');
                    $('input[id$=txtPreEventQuote]').val("");
                    return false;
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
                // $('input[id$=txtUploads]').val("");
            }
            else {
                // $('input[id$=txtUploads]').val(onlyname);

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

            // if user resize the window, call the same function again
            // to make sure the overlay fills the screen and dialogbox aligned to center	
            $(window).resize(function () {

                //only do it if the dialog box is not hidden
                if (!$('#dialog-box').is(':hidden')) popup();
            });


        });
    </script>
    <script type="text/javascript">

        function caltotal(obj) {
            var val1 = false;
            var val2 = false;
            //alert(obj.id);

            if ($('input[id$=' + obj.id + ']').val() != "") {

                var numberRegex = /^\d{1,10}(\.\d{0,2})?$/;
                if (numberRegex.test($('input[id$=' + obj.id + ']').val())) {
                    val1 = true


                }
                else {
                    alert('Enter numeric data only');
                    $('input[id$=' + obj.id + ']').val("");
                    val1 = false;
                }
            }
            else {
                //alert('Enter value');
                //$('input[id$=txtQuantity]').val("0");
            }
        }

    </script>

</asp:Content>
