<%@ Page Title="" Language="VB" MasterPageFile="~/Apex.master" AutoEventWireup="false" CodeFile="PostPnLManager.aspx.vb" Inherits="PostPnLManager" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="dist/css/bootstrap-datepicker3.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Post PnL Manager</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="leads.aspx"><i class="fa fa-th"></i>&nbsp;Settings</a></li>
            <li class="active">Post PnL Manager</li>
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
                        <div id="divError" runat="server" class="alert alert-danger">
                            <p>
                                <span class="ui-icon ui-icon-alert" style="float: left; margin-right: .3em;"></span>
                                <strong>Warning: </strong>
                                <asp:Label ID="lblError" runat="server"></asp:Label>
                            </p>
                        </div>
                        <div id="MessageDiv" runat="server" class="alert alert-success">
                            <p>

                                <strong>Message: </strong>
                                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                            </p>
                        </div>
                        <div id="divContent" runat="server">
                            <div class="modal fade" id="dialog-box-prePnl">
                                <div class="modal-dialog">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                            <h4 class="modal-title">Modal title</h4>
                                        </div>
                                        <div class="modal-body">
                                            <div id="dialog-message2">
                                                <h4>Pre P&L</h4>

                                                <table id="Table1" runat="server" width="99%">
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
                                                <table id="Table2" runat="server" style="width: 99%">
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
                                                                    <%--<asp:TemplateField HeaderText="Pre Event Cost">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "PreEventCost")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="gv_txtPreEventCost" runat="server" Text='<%#Eval("PreEventCost")%>' />
                                            </EditItemTemplate>
                                        </asp:TemplateField>--%>

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

                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-lg-12">
                                <div class="panel panel-warning">
                                    <div class="panel-heading clearfix">
                                        Pre P&L Manager
                                                 <button type="button" class="btn btn-primary btn-sm pull-right" data-toggle="modal" data-target="#dialog-box-prePnl">View more</button>
                                    </div>
                                    <div class="panel-body">
                                        <div class="col-lg-3">
                                            <b>Client : </b>
                                            <asp:Label runat="server" ID="lblclient1" Text=""></asp:Label>
                                        </div>
                                        <div class="col-lg-3">
                                            <b>Project Manager : </b>
                                            <asp:Label runat="server" ID="lblPM" Text=""></asp:Label>
                                        </div>
                                        <div class="col-lg-3">
                                            <b>KAM : </b>
                                            <asp:Label runat="server" ID="lblKAM" Text=""></asp:Label>
                                        </div>
                                        <div class="col-lg-3">
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
                            <div class="col-lg-12" id="tbl1" runat="server">

                                <div class="form-group">
                                    <%--<label class="col-lg-1 control-label" for="head"></label>--%>
                                    <div class=" col-lg-10" style="display:none;">
                                        <div class="form-group col-lg-12">

                                            <label for="inputEmail" class="col-lg-3">Client Name</label>
                                            <div class="col-lg-3">
                                                <asp:Label ID="lblClient" runat="server"></asp:Label>
                                                <asp:DropDownList ID="ddlClient" runat="server" ValidationGroup="PE" Enabled="False" TabIndex="1" CssClass="form-control"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="ddlClient" ForeColor="Red" InitialValue="0" ValidationGroup="PE"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-12">
                                            <label for="inputEmail" class="col-lg-3">Activity Name</label>
                                            <div class="col-lg-3">
                                                <asp:Label ID="lblEventName" runat="server"></asp:Label>
                                                <asp:TextBox ID="txtEventName" runat="server" CssClass="form-control" MaxLength="100" TabIndex="2"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-12">
                                            <label for="inputEmail" class="col-lg-3">Activity Date</label>
                                            <div class="col-lg-3">
                                                <asp:Label ID="lblEventDate" runat="server"></asp:Label>
                                                <asp:TextBox ID="txtEventDate" runat="server" onkeypress="return false" onkeyup="return false" onkeydown="return false" CssClass="form-control" TabIndex="3"></asp:TextBox>

                                            </div>

                                            <div class="col-lg-3">
                                                <label for="inputEmail" class="col-lg-3">Activity Venue</label>
                                            </div>
                                            <div class="col-lg-3">
                                                <asp:Label ID="lblEventVenue" runat="server"></asp:Label>
                                                <asp:TextBox ID="txtEventVenue" runat="server" CssClass="form-control" TabIndex="4"></asp:TextBox>
                                            </div>

                                        </div>
                                        <div class="form-group col-lg-12" id="POnumber" runat="server">

                                            <label for="inputEmail" class="col-lg-5">PO No/Agreement Signed/Mail Approval</label>
                                            <div class="col-lg-5">
                                                <asp:Label ID="lblApprovalNo" runat="server"></asp:Label>
                                                <asp:TextBox ID="txtApprovalNo" runat="server" CssClass="form-control" MaxLength="20" TabIndex="5"></asp:TextBox>
                                                <asp:FileUpload ID="fupl_Mail" runat="server" onchange="javascript:return CheckUpl();" />
                                                <asp:HyperLink ID="lblfulpl_Mail" runat="server" Text="Download" Target="_blank"></asp:HyperLink>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-12" id="creditperiod" runat="server">
                                            <label for="inputEmail" class="col-lg-5">Credit Period with Client(in days)</label>
                                            <div class="col-lg-5">
                                                <asp:Label ID="lblCreditPeriod" runat="server"></asp:Label>
                                                <asp:TextBox ID="txtCreditPeriod" runat="server" CssClass="form-control" MaxLength="3" onkeyup="return CheckDays()" TabIndex="6" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div id="tbl2" runat="server" class="col-lg-12">
                                <div class="col-lg-12">
                                <asp:Button runat="server" ID="btnExcel" Text="Export To Excel" CssClass="btn btn-primary btn-sm pull-right" CausesValidation="false" />
                                    </div>
                                <div class="col-lg-12 table-responsive">
                                    <asp:GridView EnableModelValidation="true" ID="gvPostPnLCost" runat="server" DataKeyNames="Row" AutoGenerateColumns="false" ShowFooter="true" PageSize="5"
                                        OnRowCancelingEdit="gvPostPnLCost_RowCancelingEdit" OnRowDeleting="gvPostPnLCost_RowDeleting"
                                        OnRowEditing="gvPostPnLCost_RowEditing" OnRowCommand="gvPostPnLCost_RowCommand" CssClass="table table-bordered table-striped">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No.">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                    <asp:HiddenField ID="hdnPostPnLCostID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "PostPnLCostID")%>' />
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
                                                    <asp:DropDownList ID="ddlCategory" runat="server" TabIndex="7" CssClass="form-control input-small"></asp:DropDownList>
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
                                                    <asp:TextBox ID="txtNature" runat="server" CssClass="form-control input-small" ValidationGroup="PC" MaxLength="40" TabIndex="8"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="PC" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="txtNature"></asp:RequiredFieldValidator>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Supplier Name" Visible="false">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="gv_txtSupplierName" CssClass="form-control input-small" MaxLength="40" runat="server" Text='<%#Eval("SupplierName") %>' />
                                                    <asp:RequiredFieldValidator ID="RFV_gv_txtSupplierName" ValidationGroup="Editvalidation11" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="gv_txtSupplierName"></asp:RequiredFieldValidator>

                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "SupplierName")%>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtSupplier" runat="server" CssClass="form-control input-small" ValidationGroup="PC" MaxLength="40" TabIndex="9"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="PC" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="txtSupplier"></asp:RequiredFieldValidator>
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
                                                    <asp:TextBox runat="server" ID="txtquantity" CssClass="form-control input-small" onkeyup="javascript:Calc_Grid();" MaxLength="10" TabIndex="10" autocomplete="off"></asp:TextBox>
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
                                                    <asp:TextBox runat="server" ID="txtRate" CssClass="form-control input-small" onkeyup="javascript:Calc_Grid();" MaxLength="10" TabIndex="11" autocomplete="off"></asp:TextBox>
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
                                                    <asp:TextBox runat="server" ID="txtDays" onkeyup="javascript:Calc_Grid();" CssClass="form-control input-small" MaxLength="3" TabIndex="12" autocomplete="off"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtDays" ErrorMessage="*" ForeColor="Red" ValidationGroup="EG" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Post Event Cost">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="gv_txtPostEventCost" CssClass="form-control input-small" ReadOnly="true" MaxLength="10" onkeyup="javascript:Calc_Grid2(this)" runat="server" Text='<%#Eval("PostEventTotal") %>' />
                                                    <asp:RequiredFieldValidator ID="RF_gv_txtPreEventCost" ValidationGroup="Editvalidation" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="gv_txtPostEventCost"></asp:RequiredFieldValidator>

                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "PostEventCost")%>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtPostEventCost" runat="server" ReadOnly="true" CssClass="form-control input-small" onkeyup="javascript:Calc_Grid()" MaxLength="10" TabIndex="13"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="txtPostEventCost" ValidationGroup="PC"></asp:RequiredFieldValidator>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Service Tax (in %)" Visible="false">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="gv_txtPostEventServiceTax" CssClass="form-control input-small" onkeyup="javasript:Calc_Grid2(this)" runat="server" MaxLength="10" Text="0" />
                                                    <asp:RequiredFieldValidator ID="RFV_gv_txtPreEventServiceTax" ValidationGroup="Editvalidation" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="gv_txtPostEventServiceTax"></asp:RequiredFieldValidator>

                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "PostEventServiceTax")%>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtPostEventServiceTax" runat="server" CssClass="form-control input-small" onkeyup="javasript:Calc_Grid()" ValidationGroup="PC" MaxLength="10" TabIndex="8"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="txtPostEventServiceTax" ValidationGroup="PC"></asp:RequiredFieldValidator>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total" Visible="false">
                                                <EditItemTemplate>
                                                    <asp:Label ID="gv_txtPostEventTotal" CssClass="form-control input-small" runat="server" Text='<%#Eval("PostEventTotal")%>'></asp:Label>
                                                </EditItemTemplate>

                                                <ItemTemplate>

                                                    <%# DataBinder.Eval(Container.DataItem, "PostEventTotal")%>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtPostEventTotal" runat="server" Text="0" ReadOnly="true" CssClass="form-control input-small" MaxLength="10" TabIndex="9"></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action">
                                                <EditItemTemplate>
                                                    <%--<asp:Button ValidationGroup="Editvalidation" ID="imgbtnUpdate" CommandName="Update" runat="server" Text="Update" CssClass="btn btn-sm btn-info" />--%>
                                                    <asp:Button ValidationGroup="Editvalidation" ID="Button1" runat="server" CommandName="Update" Text="Update" CssClass="btn btn-sm btn-info" />
                                                    <asp:Button CausesValidation="false" ID="imgbtnCancel" runat="server" CommandName="Cancel" Text="Cancel" CssClass="btn btn-sm btn-danger" />

                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Button CausesValidation="false" ID="imgbtnEdit" CommandName="Edit" runat="server" Text="Edit" CssClass="btn btn-sm btn-info" />
                                                    <asp:Button CausesValidation="false" ID="imgbtnDelete" OnClientClick="javascript:return ConfirmationBox()" CommandName="Delete" runat="server" Text="Delete" CssClass="btn btn-sm btn-danger" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Button ID="imgbtnAdd" runat="server" CommandName="Add" ValidationGroup="PC" TabIndex="14" Text="Add" CssClass="btn btn-sm btn-info" />

                                                </FooterTemplate>
                                            </asp:TemplateField>
                                        </Columns>

                                    </asp:GridView>
                                    <asp:GridView ID="gvDisplay" runat="server" AutoGenerateColumns="false" PageSize="5" CssClass="table table-bordered">
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
                                            <%--<asp:TemplateField HeaderText="Supplier Name">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "SupplierName")%>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
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

                                            <asp:TemplateField HeaderText="City/Days">

                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem,"Days") %>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Post Event Cost">
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "PostEventCost")%>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="gv_txtPostEventCost" runat="server" Text='<%#Eval("PostEventCost")%>' />
                                                </EditItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Service Tax / VAT (in %)" Visible="false">
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "PostEventServiceTax")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total" Visible="false">
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "PostEventTotal")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            <div class="form-group">
                                <%-- <label class="col-lg-1 control-label" for="head"></label>--%>
                                <div class=" col-lg-10">
                                    <div class="form-group col-lg-12" style="display:none;">
                                        <label for="inputEmail" class="col-lg-3">Post Event Quote</label>
                                        <div class="col-lg-3">
                                            <asp:Label ID="lblPostEventQuote" runat="server"></asp:Label>
                                            <asp:TextBox ID="txtPostEventQuote" runat="server" CssClass="form-control" MaxLength="10" onkeyup="javascript:CheckPostEventQuote()" TabIndex="20" autocomplete="off">0</asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-12">
                                        <label for="inputEmail" class="col-lg-3">Total Post Event Cost</label>
                                        <div class="col-lg-3">
                                            <asp:Label ID="lblTCost" runat="server"></asp:Label>
                                            <asp:TextBox ID="lblTotalCost" runat="server" ReadOnly="true" CssClass="form-control" TabIndex="21"></asp:TextBox></span>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-12" style="display: none;">
                                        <label for="inputEmail" class="col-lg-3">Total Service Tax</label>
                                        <div class="col-lg-3">
                                            <asp:Label ID="lblTSTax" runat="server"></asp:Label>
                                            <asp:TextBox ID="lblTotalServiceTax" runat="server" ReadOnly="true" CssClass="form-control" TabIndex="22"></asp:TextBox></span>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-12" style="display: none;">
                                        <label for="inputEmail" class="col-lg-3">Post Event Total</label>
                                        <div class="col-lg-3">
                                            <asp:Label ID="lblPETotal" runat="server"></asp:Label>
                                            <asp:TextBox ID="lblPostEventTotal" runat="server" ReadOnly="true" CssClass="form-control" TabIndex="23"></asp:TextBox></span>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-12" style="display:none;">
                                        <label for="inputEmail" class="col-lg-3">Post Event Profit (in %)</label>

                                        <div class="col-lg-3">
                                            <asp:Label ID="lblPostEventProfit" runat="server" CssClass="form-control" Text="0"></asp:Label>
                                            <asp:Label ID="lblPostEPr" runat="server"></asp:Label>
                                        </div>
                                    </div>


                                    <div class="form-group col-lg-12" id="trAppRemarks" runat="server">
                                        <label for="inputEmail" class="col-lg-3">Remarks:</label>
                                        <div class="col-lg-3">
                                            <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" TabIndex="24" CssClass="form-control"></asp:TextBox></span>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-12" id="trAppButtons" runat="server">
                                        <label for="inputEmail" class="col-lg-3"></label>
                                        <div class="col-lg-3">
                                            <asp:Button ID="btnApproval" runat="server" Text="Approve" CausesValidation="false" CssClass="btn btn-sm btn-primary" TabIndex="25" />
                                            <asp:Button ID="btnRejected" runat="server" Text="Reject" OnClientClick="javascript:return RejectConfirmationBox()" CausesValidation="false" CssClass="btn btn-sm btn-danger" TabIndex="26" />
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-12" id="trmsg" runat="server">
                                        <label for="inputEmail" class="col-lg-3">
                                            <asp:Label runat="server" ForeColor="red" Text="Your Post P&L Sent Sucessfully For Estimate" ID="lblnotifivationMsg"></asp:Label>
                                        </label>

                                    </div>


                                    <div class="col-lg-12" style="text-align: center">
                                        <label for="inputEmail" class="col-lg-3"></label>
                                        <div class="col-lg-3">
                                            <asp:Button ID="btnTblCancel" runat="server" Text="Back" CausesValidation="false" CssClass="btn btn-sm btn-primary" TabIndex="27" />
                                            <asp:Button ID="btnAdd" runat="server" Text="Save" CssClass="btn btn-sm btn-primary" TabIndex="28" />
                                            <asp:Button ID="btnFinallize" runat="server" Text="Close" CssClass="btn btn-sm btn-primary" TabIndex="29" />
                                            <asp:Button ID="btnReset" runat="server" Text="Reset" CausesValidation="false" CssClass="btn btn-sm btn-primary" TabIndex="30" />
                                            <asp:Button ID="btnHomeCancel" runat="server" Text="Back" CssClass="btn btn-sm btn-primary" CausesValidation="false" />
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
    <script src="dist/js/bootstrap-datepicker.js"></script>
        <script>



            $(function () {
                $('input[id$=txtEventDate]').datepicker({ format: 'dd-mm-yyyy',autoclose:false });
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
                    $('input[id$=txtPostEventCost]').val(total);
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
                    $('span[id$=gv_txtPostEventCost' + postfix + ']').val(total);
                    $('span[id$=gv_txtPostEventCost' + postfix + ']').text(total);

                    $('input[id$=gv_txtPostEventCost' + postfix + ']').val(total);
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

            function CheckPostEventQuote() {

                if ($('input[id$=txtPostEventQuote]').val() != "") {

                    var numberRegex = /^\d{1,10}(\.\d{0,2})?$/;
                    if (numberRegex.test($('input[id$=txtPostEventQuote]').val())) {
                        var PostEventQuote = document.getElementById('<%=txtPostEventQuote.ClientID%>').value;
                    var PostEventCost = document.getElementById('<%=lblTotalCost.ClientID%>').value;
                    var postpnlprofit = ((parseFloat(PostEventQuote) - parseFloat(PostEventCost)) / parseFloat(PostEventQuote)) * 100;
                    //alert(prepnlprofit);
                    document.getElementById('<%=lblPostEventProfit.ClientID%>').textContent = postpnlprofit.toFixed(2);
                }
                else {
                    alert('Enter numeric data only');
                    $('input[id$=txtPostEventQuote]').val("");
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

</asp:Content>
