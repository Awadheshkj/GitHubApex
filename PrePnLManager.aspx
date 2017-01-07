<%@ Page Title="" Language="VB" MasterPageFile="~/Apex.master" AutoEventWireup="false" CodeFile="PrePnLManager.aspx.vb" Inherits="PrePnLManager" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="dist/css/bootstrap-datepicker3.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Pre PnL Manager</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">Pre PnL Manager</li>
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

                        <div class="alert alert-danger" id="divError" runat="server">
                            <strong>Warning: </strong>
                            <asp:Label ID="lblError" runat="server"></asp:Label>
                        </div>
                        <div class="alert alert-success" id="MessageDiv" runat="server">
                            <strong>Message: </strong>
                            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>

                        </div>
                        <div id="divContent" runat="server">
                            <div class="inner-Content-container">
                                <div class="InnerContentData">

                                    <div class="modal fade" id="dialog-box-prePnl">
                                        <div class="modal-dialog">
                                            <div class="modal-content">
                                                <div class="modal-header">
                                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                                    <h4 class="modal-title">Modal title</h4>
                                                </div>
                                                <div class="modal-body">

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
                                        </div>
                                    </div>

                                    <div class="col-lg-12">
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                Brief Manager
                                                <%--<button type="button" class="btn btn-primary btn-sm pull-right" data-toggle="modal" data-target="#dialog-box-prePnl">View more</button>--%>
                                                <asp:LinkButton ID="btnviewBrief"  runat="server" Text="View/Edit Brief" class="btn btn-primary btn-sm pull-right" />
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
                                    <div class="col-lg-12">
                                        <div class="form-group">
                                            <%--<label class="col-lg-1 control-label" for="head"></label>--%>
                                            <div class="col-lg-10" style="display: none;">
                                                <div class="bs-example form-horizontal">

                                                    <div class="form-group">
                                                        <div class="col-lg-4">
                                                            <label for="inputEmail" class="control-label">Client Name</label>
                                                        </div>
                                                        <div class="col-lg-2">
                                                            <asp:Label CssClass="form-control" runat="server" ID="lblClient"></asp:Label>
                                                            <asp:DropDownList ID="ddlClient" class="form-control" runat="server" ValidationGroup="PE" TabIndex="1">
                                                            </asp:DropDownList>

                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <div class="col-lg-4">
                                                            <label for="inputEmail" class="control-label">Activity Name</label>
                                                        </div>
                                                        <div class="col-lg-2">
                                                            <asp:Label ID="lblEventName" CssClass="form-control" runat="server"></asp:Label>
                                                            <asp:TextBox ID="txtEventName" runat="server" CssClass="form-control" MaxLength="100" TabIndex="2"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <div class="col-lg-4">
                                                            <label for="inputEmail" class="control-label">Activity Date</label>
                                                        </div>
                                                        <div class="col-lg-2">
                                                            <asp:Label ID="lblEventDate" CssClass="form-control" runat="server"></asp:Label>
                                                            <asp:TextBox ID="txtEventDate" runat="server" onkeypress="return false" onkeyup="return false" onkeydown="return false" CssClass="form-control" TabIndex="3"></asp:TextBox>
                                                        </div>

                                                        <div>
                                                            <label for="inputEmail" class="col-lg-2 control-label">Activity Venue</label>
                                                        </div>
                                                        <div class="col-lg-2">
                                                            <asp:Label ID="lblEventVenue" runat="server"></asp:Label>
                                                            <asp:TextBox ID="txtEventVenue" runat="server" CssClass="form-control" TabIndex="4"></asp:TextBox>
                                                        </div>

                                                    </div>

                                                    <div class="form-group">
                                                        <div class="col-lg-4 ">
                                                            <label for="inputEmail" class="control-label">PO No/Agreement Signed/Mail Approval</label>
                                                        </div>
                                                        <div class="col-lg-2">
                                                            <asp:Label ID="lblApprovalNo" CssClass="form-control" runat="server"></asp:Label>
                                                            <asp:TextBox ID="txtApprovalNo" runat="server" CssClass="form-control" MaxLength="20" TabIndex="5"></asp:TextBox>
                                                        </div>

                                                        <div class="col-lg-2">
                                                            <asp:FileUpload ID="fupl_Mail" runat="server" onchange="javascript:return CheckUpl();" />
                                                            <asp:HyperLink ID="lblfulpl_Mail" runat="server" Text="Download" Target="_blank"></asp:HyperLink>
                                                        </div>

                                                    </div>

                                                    <div class="form-group">
                                                        <div class="col-lg-4">
                                                            <label for="inputEmail" class="control-label">Credit Period with Client(in days)</label>
                                                        </div>
                                                        <div class="col-lg-2">
                                                            <asp:Label ID="lblCreditPeriod" CssClass="form-control" runat="server"></asp:Label>
                                                            <asp:TextBox ID="txtCreditPeriod" runat="server" CssClass="form-control" MaxLength="3" TabIndex="6" autocomplete="off"></asp:TextBox>
                                                            <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Invalid No of days" ControlToValidate="txtCreditPeriod" ForeColor="Red" MaximumValue="999" MinimumValue="0" Type="Integer"></asp:RangeValidator>
                                                        </div>

                                                    </div>

                                                </div>
                                            </div>

                                        </div>
                                    </div>


                                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

                                    <div class="form-group ">

                                        <%--<label for="inputEmail" class="col-lg-1 control-label"></label>--%>

                                        <div class="col-lg-12">

                                            <asp:HiddenField ID="hdnstate" runat="server"></asp:HiddenField>
                                            <asp:HiddenField ID="hdncity" runat="server"></asp:HiddenField>

                                            <div class="table-responsive">
                                                <table id="tbl2" runat="server" width="100%">
                                                    <tr>
                                                        <td>
                                                            <div class="panel panel-warning">
                                                                <div class="panel-heading clearfix">
                                                                    Copy Paste from Excel Utility
                                                                <div class="pull-right"><a class="badge" href="uploads/Prepnlformat.xlsx">Click here</a> to download Excel Format.</div>
                                                                </div>
                                                                <div class="panel-body">
                                                                    <div class="col-12" style="padding-bottom: 10px;">
                                                                        <asp:GridView ID="GridView1" CssClass="table table-bordered table-striped"
                                                                            runat="server" AutoGenerateColumns="false">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="id" HeaderText="id" ItemStyle-Width="30" />
                                                                                <asp:TemplateField HeaderText="Category" ItemStyle-Width="150">
                                                                                    <ItemTemplate>
                                                                                        <asp:HiddenField ID="hdncatigory" runat="server" Value='<%# Eval("Category")%>' />
                                                                                        <asp:DropDownList ID="ddlCategory" class="form-control input-small" TabIndex="1" runat="server">
                                                                                            <asp:ListItem>Select</asp:ListItem>
                                                                                            <asp:ListItem>C.E.P</asp:ListItem>
                                                                                            <asp:ListItem>Events</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                        <asp:RequiredFieldValidator ID="RequiredValidator20" ValidationGroup="impgr" runat="server" ErrorMessage="Please Select Category" Font-Size="11px" ForeColor="Red" Display="Dynamic" ControlToValidate="ddlCategory" InitialValue="0"></asp:RequiredFieldValidator>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="natureofexpenses" HeaderText="Nature Of Expenses" ItemStyle-Width="150" />
                                                                                <asp:BoundField DataField="Quantity" HeaderText="Quantity" ItemStyle-Width="30" />
                                                                                <asp:BoundField DataField="UnitPrice" HeaderText="Unit" ItemStyle-Width="30" />
                                                                                <asp:BoundField DataField="City" HeaderText="City" ItemStyle-Width="130" />


                                                                                <asp:TemplateField HeaderText="Pre EventCost">
                                                                                    <ItemTemplate>
                                                                                        <%# Eval("Quantity") * Eval("UnitPrice") * Eval("City")%>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>


                                                                            </Columns>

                                                                        </asp:GridView>
                                                                    </div>

                                                                    <div class="col-lg-12">

                                                                        <asp:TextBox ID="txtCopied" runat="server" TextMode="MultiLine" placeholder="Copy from Excel and paste here without header" AutoPostBack="true"
                                                                            OnTextChanged="PasteToGridView" Height="80" CssClass="form-control" />

                                                                    </div>
                                                                </div>
                                                                <div class="panel-footer text-right">
                                                                    <asp:Button runat="server" ID="btnimport" Visible="false" Text="Import to Final grid" CssClass="btn btn-sm btn-primary" ValidationGroup="impgr" TabIndex="6" />
                                                                    <asp:Button runat="server" ID="btncancelimport" Visible="false" Text="cancel" CssClass="btn btn-sm btn-primary" CausesValidation="false" TabIndex="6" />
                                                                    <script type="text/javascript">
                                                                        window.onload = function () {
                                                                            document.getElementById("<%=txtCopied.ClientID %>").onpaste = function () {
                                                                            var txt = this;
                                                                            setTimeout(function () {
                                                                                __doPostBack(txt.name, '');
                                                                            }, 100);
                                                                        }
                                                                    };
                                                                    </script>
                                                                </div>
                                                            </div>

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">

                                                            <asp:Button runat="server" ID="btnExcel" Text="Export To Excel" CssClass="btn btn-sm btn-primary" CausesValidation="false" TabIndex="7" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>

                                                            <asp:GridView EnableModelValidation="true" ID="gvPrePnLCost" runat="server" DataKeyNames="PrePnLCostID" AutoGenerateColumns="false" CssClass="table table-bordered table-striped" ShowFooter="true" PageSize="10"
                                                                OnRowCancelingEdit="gvPrePnLCost_RowCancelingEdit" OnRowDeleting="gvPrePnLCost_RowDeleting"
                                                                OnRowEditing="gvPrePnLCost_RowEditing" sonrowcommand="gvPrePnLCost_RowCommand">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="S.No.">
                                                                        <ItemTemplate>
                                                                            <%# Container.DataItemIndex + 1 %>
                                                                            <asp:HiddenField ID="hdnPrePnLCostID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "PrePnLCostID")%>' />
                                                                            <asp:HiddenField ID="hdnstate" runat="server" Value='<%# Eval("RefstateID")%>'></asp:HiddenField>
                                                                            <asp:HiddenField ID="hdncity" runat="server" Value='<%# Eval("RefcityID")%>'></asp:HiddenField>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Category">
                                                                        <EditItemTemplate>
                                                                            <asp:DropDownList ID="gv_ddlCategory" class="form-control input-small" runat="server"></asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="RequiredValidator2" ValidationGroup="Editvalidation" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="gv_ddlCategory" InitialValue="0"></asp:RequiredFieldValidator>
                                                                        </EditItemTemplate>
                                                                        <ItemTemplate>
                                                                            <%# DataBinder.Eval(Container.DataItem, "Category")%>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:DropDownList ID="ddlCategory" class="form-control input-small" TabIndex="1" runat="server"></asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="RequiredValidator20" ValidationGroup="PC" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="ddlCategory" InitialValue="0"></asp:RequiredFieldValidator>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Nature of Expenses">
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="gv_txtNatureOfExpenses" CssClass="form-control input-small" runat="server" Text='<%#Eval("NatureOfExpenses") %>' />
                                                                            <asp:RequiredFieldValidator ID="RFV_gv_txtNatureOfExpenses" ValidationGroup="Editvalidation" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="gv_txtNatureOfExpenses"></asp:RequiredFieldValidator>
                                                                        </EditItemTemplate>
                                                                        <ItemTemplate>
                                                                            <%# DataBinder.Eval(Container.DataItem, "NatureOfExpenses")%>
                                                                        </ItemTemplate>

                                                                        <FooterTemplate>
                                                                            <asp:TextBox ID="txtNature" runat="server" CssClass="form-control input-small" ValidationGroup="PC" MaxLength="40" TabIndex="2"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="PC" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="txtNature"></asp:RequiredFieldValidator>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Supplier Name" Visible="false">
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="gv_txtSupplierName" CssClass="form-control input-sm" MaxLength="40" runat="server" Text='<%#Eval("SupplierName") %>' />
                                                                            <asp:RequiredFieldValidator ID="RFV_gv_txtSupplierName" ValidationGroup="Editvalidation" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="gv_txtSupplierName"></asp:RequiredFieldValidator>

                                                                        </EditItemTemplate>
                                                                        <ItemTemplate>
                                                                            <%# DataBinder.Eval(Container.DataItem, "SupplierName")%>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:TextBox ID="txtSupplier" runat="server" CssClass="form-control input-sm" ValidationGroup="PC" MaxLength="40"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="PC" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="txtSupplier"></asp:RequiredFieldValidator>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>


                                                                    <asp:TemplateField HeaderText="Quantity">
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="gv_txtquantity" CssClass="form-control input-small" onkeyup="javasript:Calc_Grid2(this)" runat="server" MaxLength="3" Text='<%#Eval("Quantity") %>' />
                                                                            <asp:RequiredFieldValidator ID="RFV_gv_txtquantity" ValidationGroup="Editvalidation" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="gv_txtquantity"></asp:RequiredFieldValidator>

                                                                        </EditItemTemplate>
                                                                        <ItemTemplate>
                                                                            <%# DataBinder.Eval(Container.DataItem, "Quantity")%>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:TextBox runat="server" ID="txtquantity" CssClass="form-control input-small" onkeyup="javascript:Calc_Grid();" MaxLength="10" TabIndex="3" autocomplete="off"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="txtquantity" ForeColor="Red" ValidationGroup="PC" SetFocusOnError="true"></asp:RequiredFieldValidator>
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
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ControlToValidate="txtRate" ForeColor="Red" ValidationGroup="PC" SetFocusOnError="true"></asp:RequiredFieldValidator>
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
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtDays" ErrorMessage="*" ForeColor="Red" ValidationGroup="PC" SetFocusOnError="true"></asp:RequiredFieldValidator>
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
                                                                            <asp:TextBox ID="txtPreEventCost" runat="server" ReadOnly="true" CssClass="form-control input-small" onkeyup="javascript:Calc_Grid()" MaxLength="10" TabIndex="6"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="txtPreEventCost" ValidationGroup="PC"></asp:RequiredFieldValidator>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Service Tax (in %)" Visible="false">
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="gv_txtPreEventServiceTax" CssClass="form-control input-sm" onkeyup="javasript:Calc_Grid2(this)" runat="server" MaxLength="10" Text="0" />
                                                                            <asp:RequiredFieldValidator ID="RFV_gv_txtPreEventServiceTax" ValidationGroup="Editvalidation" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="gv_txtPreEventServiceTax"></asp:RequiredFieldValidator>

                                                                        </EditItemTemplate>
                                                                        <ItemTemplate>
                                                                            <%# DataBinder.Eval(Container.DataItem, "PreEventServiceTax")%>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:TextBox ID="txtPreEventServiceTax" runat="server" CssClass="form-control input-sm" onkeyup="javasript:Calc_Grid()" ValidationGroup="PC" MaxLength="10" TabIndex="8"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="txtPreEventServiceTax" ValidationGroup="PC"></asp:RequiredFieldValidator>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>




                                                                    <asp:TemplateField HeaderText="Total" Visible="false">
                                                                        <EditItemTemplate>
                                                                            <asp:Label ID="gv_txtPreEventTotal" CssClass="form-control input-sm" runat="server" Text='<%#Eval("PreEventTotal") %>'></asp:Label>
                                                                        </EditItemTemplate>

                                                                        <ItemTemplate>

                                                                            <%# DataBinder.Eval(Container.DataItem, "PreEventTotal")%>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:TextBox ID="txtPreEventTotal" runat="server" Text="0" ReadOnly="true" CssClass="form-control input-sm" MaxLength="10" TabIndex="9"></asp:TextBox>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Start Date" ItemStyle-Width="100px" FooterStyle-Width="100px" Visible="false">
                                                                        <EditItemTemplate>
                                                                            <%--<asp:Label ID="gv_txtstartdate" CssClass="form-control input-sm" runat="server" Text='<%#Eval("StartDate", "{0:dd/MM/yyyy}")%>'></asp:Label>--%>
                                                                            <asp:TextBox ID="gv_txtstartdate" runat="server" Text='<%#Eval("Startdate", "{0:dd/MM/yyyy}")%>' TabIndex="5" onchange="javascript:return checkDates();" CssClass="form-control input-sm" />
                                                                            <asp:RequiredFieldValidator ID="RFV_gv_txtstartdate" ValidationGroup="PC" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="gv_txtstartdate"></asp:RequiredFieldValidator>
                                                                        </EditItemTemplate>

                                                                        <ItemTemplate>

                                                                            <%# DataBinder.Eval(Container.DataItem, "StartDate", "{0:dd/MM/yyyy}")%>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:TextBox ID="txtstartdate" runat="server" Text="" CssClass="form-control input-sm" onchange="javascript:return checkDates1();" MaxLength="10" TabIndex="10"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator runat="server" ID="rfvtxtstartdate" ControlToValidate="txtstartdate" ForeColor="Red" ValidationGroup="PC" ErrorMessage="*" MaxLength="40"></asp:RequiredFieldValidator>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="End Date" ItemStyle-Width="100px" FooterStyle-Width="100px" Visible="false">
                                                                        <EditItemTemplate>
                                                                            <%--<asp:Label ID="gv_txtenddate" CssClass="form-control input-sm" runat="server" Text='<%#Eval("EndDate", "{0:dd/MM/yyyy}")%>'></asp:Label>--%>
                                                                            <asp:TextBox ID="gv_txtEnddate" runat="server" Text='<%#Eval("Enddate", "{0:dd/MM/yyyy}")%>' CssClass="form-control input-sm" onchange="javascript:return checkDates();" />
                                                                            <asp:RequiredFieldValidator ID="RFV_gv_txtEnddate" ValidationGroup="PC" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="gv_txtEnddate"></asp:RequiredFieldValidator>
                                                                        </EditItemTemplate>

                                                                        <ItemTemplate>

                                                                            <%# DataBinder.Eval(Container.DataItem, "EndDate", "{0:dd/MM/yyyy}")%>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:TextBox ID="txtEnddate" runat="server" Text="" CssClass="form-control input-sm" onchange="javascript:return checkDates1();" TabIndex="11"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator runat="server" ID="rfvEnddate" ControlToValidate="txtEnddate" ForeColor="Red" ValidationGroup="PC" ErrorMessage="*" MaxLength="40"></asp:RequiredFieldValidator>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>


                                                                    <asp:TemplateField HeaderText="Action" FooterStyle-Width="150px" ItemStyle-Width="150px">
                                                                        <EditItemTemplate>
                                                                            <%--<asp:Button ValidationGroup="Editvalidation" ID="imgbtnUpdate" CommandName="Update" runat="server"  Text="Update" CssClass="btn btn-sm btn-info"  />--%>
                                                                            <asp:Button ID="Button1" ValidationGroup="Editvalidation" runat="server" CommandName="Update" Text="Update" CssClass="btn btn-sm btn-info"></asp:Button>
                                                                            <asp:Button CausesValidation="false" ID="imgbtnCancel" runat="server" CommandName="Cancel" Text="Cancel" CssClass="btn btn-sm btn-danger" />

                                                                        </EditItemTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Button CausesValidation="false" ID="imgbtnEdit" CommandName="Edit" runat="server" Text="Edit" CssClass="btn btn-sm btn-info" />
                                                                            <asp:Button CausesValidation="false" ID="imgbtnDelete" OnClientClick="javascript:return ConfirmationBox()" CommandName="Delete" Text="Delete" runat="server" CssClass="btn btn-sm btn-danger" />
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Button ID="imgbtnAdd" runat="server" CommandName="Add" ValidationGroup="PC" TabIndex="19" CssClass="btn btn-sm btn-info" Text="Add" />

                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>

                                                            </asp:GridView>

                                                            <asp:GridView ID="gvDisplay" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped" PageSize="5">
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
                                                                   <%-- <asp:TemplateField HeaderText="Total">
                                                                        <ItemTemplate>
                                                                            <%# DataBinder.Eval(Container.DataItem, "PreEventTotal")%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>--%>
                                                                </Columns>
                                                            </asp:GridView>


                                                        </td>
                                                    </tr>

                                                </table>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-12">

                                        <%--<label for="inputEmail" class="col-lg-1"></label>--%>
                                        <div class="col-lg-10" style="display: none;">
                                            <div class="col-lg-3">
                                                <label for="inputEmail" class="">Pre Event Quote</label>
                                            </div>
                                            <div class="col-lg-8">
                                                <div class="col-lg-3 pull-right">
                                                    <asp:Label ID="lblPreEventQuote" runat="server" Text="0.00"></asp:Label>
                                                    <asp:TextBox ID="txtPreEventQuote" runat="server" CssClass="form-control" MaxLength="10" onkeyup="javascript:CheckPreEventQuote()" TabIndex="20" autocomplete="off">0</asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="form-group col-lg-12">
                                        <%--<label for="inputEmail" class="col-lg-1"></label>--%>
                                        <div class="col-lg-10">
                                            <div class="col-lg-3">
                                                <label for="inputEmail" class="">Total Pre Event Cost</label>
                                            </div>
                                            <div class="col-lg-8">
                                                <div class="col-lg-3 pull-left">
                                                    <asp:Label ID="lblTCost" runat="server"></asp:Label>
                                                    <asp:TextBox ID="lblTotalCost" runat="server" ReadOnly="true" CssClass="form-control" TabIndex="21"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-12" style="display: none;">
                                        <label for="inputEmail" class="col-lg-1"></label>
                                        <div class="col-lg-10">

                                            <div class="col-lg-2">
                                                <label for="inputEmail" class="">Total Service Tax</label>
                                            </div>
                                            <div class="col-lg-8">
                                                <div class="col-lg-3 pull-right">
                                                    <asp:Label ID="lblTSTax" runat="server"></asp:Label>
                                                    <asp:TextBox ID="lblTotalServiceTax" runat="server" ReadOnly="true" CssClass="form-control" TabIndex="22"></asp:TextBox>
                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="form-group col-lg-12" style="display: none;">
                                        <label for="inputEmail" class="col-lg-1"></label>
                                        <div class="col-lg-10">

                                            <div class="col-lg-3">
                                                <label for="inputEmail" class="">Pre Event Total</label>
                                            </div>
                                            <div class="col-lg-8">
                                                <div class="col-lg-3 pull-right">
                                                    <asp:Label ID="lblPETotal" runat="server"></asp:Label>
                                                    <asp:TextBox ID="lblPreEventTotal" runat="server" ReadOnly="true" CssClass="form-control" TabIndex="23"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="form-group col-lg-12" style="display: none;">
                                        <%--<label for="inputEmail" class="col-lg-1"></label>--%>
                                        <div class="col-lg-10">

                                            <div class="col-lg-3">
                                                <label for="inputEmail" class="">Pre Event Profit (in %)</label>
                                            </div>
                                            <div class="col-lg-8">
                                                <div class="col-lg-3 pull-right">
                                                    <asp:TextBox ID="lblPreEventProfit" runat="server" CssClass="form-control" ReadOnly="true" Text="0"></asp:TextBox>
                                                    <asp:Label ID="lblPreEPr" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="form-group col-lg-12" id="trAppRemarks" runat="server">
                                        <%--<label for="inputEmail" class="col-lg-1"></label>--%>
                                        <div class="col-lg-10">

                                            <div class="col-lg-2">
                                                <label for="inputEmail" class="">Remarks :</label>
                                            </div>
                                            <div class="col-lg-8">
                                                <div class="col-lg-3 pull-right">
                                                    <asp:TextBox ID="txtRemarks" runat="server" Text="" TextMode="MultiLine" CssClass="form-control" TabIndex="24"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="form-group col-lg-12" id="trAppButtons" runat="server">
                                        <label for="inputEmail" class="col-lg-1"></label>
                                        <div class="col-lg-10">

                                            <div class="col-lg-2">
                                                <label for="inputEmail" class=""></label>
                                            </div>
                                            <div class="col-lg-8">
                                                <div class="col-lg-3 pull-right">
                                                    <asp:Button ID="btnApproval" runat="server" Text="Approve" CausesValidation="false" CssClass="small-button" TabIndex="25" />
                                                    <asp:Button ID="btnRejected" runat="server" Text="Reject" OnClientClick="javascript:return RejectConfirmationBox()" CausesValidation="false" CssClass="small-button" TabIndex="26" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-12" id="trmsg" runat="server">
                                        <label for="inputEmail" class="col-lg-1"></label>
                                        <div class="col-lg-10">

                                            <div class="col-lg-2">
                                                <label for="inputEmail" class=""></label>
                                            </div>

                                            <div class="col-lg-8">
                                                <div class="col-lg-3">
                                                    <asp:Label runat="server" ForeColor="red" Text="Your Pre P&L Sent Sucessfully For Estimate" ID="lblnotifivationMsg"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="form-group">

                                        <label for="inputEmail" class="col-lg-5"></label>

                                        <div class="col-lg-2">
                                            <asp:Button ID="btnAdd" runat="server" Text="Save" CssClass="btn btn-primary" TabIndex="27" />
                                            <asp:Button ID="btnFinallize" runat="server" Text="Prepare Estimate" CssClass="btn btn-primary" TabIndex="27" />
                                            <asp:Button ID="btnTblCancel" runat="server" Text="Cancel" CausesValidation="false" CssClass="btn btn-primary" TabIndex="28" />
                                            <asp:Button ID="btnCancelKamHome" runat="server" Text="Cancel" CausesValidation="false" CssClass="btn btn-primary" />
                                        </div>
                                    </div>

                                    <%--  </ContentTemplate>
                                </asp:UpdatePanel>--%>
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
                $('input[id$=txtPreEventCost]').val(total);
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
                $('span[id$=gv_txtPreEventCost' + postfix + ']').val(total);
                $('span[id$=gv_txtPreEventCost' + postfix + ']').text(total);

                $('input[id$=gv_txtPreEventCost' + postfix + ']').val(total);
            }
        }

        //function CheckDays() {

        //    if ($('input[id$=txtCreditPeriod]').val() != "") {

        //        var numberRegex = /^[+-]?\d+(\.\d+)?([eE][+-]?\d+)?$/;
        //        if (numberRegex.test($('input[id$=txtCreditPeriod]').val())) {
        //            return true;
        //        }
        //        else {
        //            alert('Enter numeric data only');
        //            $('input[id$=txtCreditPeriod]').val("");
        //            return false;
        //        }
        //    }
        //}

        function CheckPreEventQuote() {

            if ($('input[id$=txtPreEventQuote]').val() != "") {

                var numberRegex = /^\d{1,10}(\.\d{0,2})?$/;
                if (numberRegex.test($('input[id$=txtPreEventQuote]').val())) {
                    var PreEventQuote = document.getElementById('<%=txtPreEventQuote.ClientID%>').value;
                     var PreEventCost = document.getElementById('<%=lblTotalCost.ClientID%>').value;
                     var prepnlprofit = ((parseFloat(PreEventQuote) - parseFloat(PreEventCost)) / parseFloat(PreEventQuote)) * 100;
                     //alert(prepnlprofit);
                     document.getElementById('<%=lblPreEventProfit.ClientID%>').value = prepnlprofit.toFixed(2);
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


</asp:Content>
