<%@ Page Title="" Language="VB" MasterPageFile="~/Apex.master" AutoEventWireup="false" CodeFile="EmployeeAdvance.aspx.vb" Inherits="EmployeeAdvance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="dist/css/bootstrap-datepicker3.css" rel="stylesheet" />
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
                        <div class="subMenuStrip"></div>
                        <h3>EMPLOYEE'S ADVANCE</h3>
                        <div id="divError" runat="server" class="ui-state-error ui-corner-all divError">
                            <p>
                                <span class="ui-icon ui-icon-alert" style="float: left; margin-right: .3em;"></span>
                                <strong>Warning: </strong>
                                <asp:Label ID="lblError" runat="server"></asp:Label>
                            </p>
                        </div>
                        <div class="alert alert-success" id="MessageDiv" visible="false" runat="server">
                            <strong>Message: </strong>
                            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>

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
                                        <div class="form-group col-lg-12">
                                            <span class="col-lg-2">Task Particulars</span>
                                            <span class="col-lg-6">
                                                <asp:Label ID="lblparticulars" runat="server"></asp:Label></span>
                                        </div>
                                        <div class="form-group col-lg-12">
                                            <span class="col-lg-2">Amount</span>
                                            <span class="col-lg-6">
                                                <asp:Label ID="lblAmount" runat="server"></asp:Label></span>
                                        </div>
                                        <div class="col-lg-12">
                                            <table class="table table-bordered" width="97%">
                                                <tr>
                                                    <td align="left" width="49%">Employee Name as Per Salary Sheet</td>
                                                    <td align="left" width="49%">
                                                        <asp:TextBox ID="txtEmployeeName" runat="server" MaxLength="50" CssClass="right_textbox"></asp:TextBox>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" width="49%">Employee Code</td>
                                                    <td align="left" width="49%">
                                                        <asp:TextBox ID="txtempcode" runat="server" MaxLength="20" CssClass="right_textbox"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td align="left" width="49%">Event Name</td>
                                                    <td align="left" width="49%">
                                                        <asp:TextBox ID="txtEventName" runat="server" MaxLength="50" CssClass="right_textbox"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td align="left" width="49%">Event City</td>
                                                    <td align="left" width="49%">
                                                        <asp:TextBox ID="txteventCity" runat="server" MaxLength="50" CssClass="right_textbox"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td align="left" width="49%">Event Date</td>
                                                    <td align="left" width="49%">
                                                        <asp:TextBox ID="txteventDate" runat="server" MaxLength="50" CssClass="right_textbox"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td align="left" width="49%">Client Name</td>
                                                    <td align="left" width="49%">
                                                        <asp:TextBox ID="txtclientName" runat="server" MaxLength="50" CssClass="right_textbox"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td align="left" width="49%">PM Name</td>
                                                    <td align="left" width="49%">
                                                        <asp:TextBox ID="txtPmName" runat="server" MaxLength="50" CssClass="right_textbox"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td align="left" width="49%">KAM Name/Account Manager</td>
                                                    <td align="left" width="49%">
                                                        <asp:TextBox ID="txtKamName" runat="server" MaxLength="50" CssClass="right_textbox"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td align="left" width="49%">JC #</td>
                                                    <td align="left" width="49%">
                                                        <asp:TextBox ID="txtJCNo" runat="server" MaxLength="50" CssClass="right_textbox"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td align="left" width="49%">Advance Amount Request</td>
                                                    <td align="left" width="49%">
                                                        <asp:TextBox ID="txtadvanceAmount" runat="server" MaxLength="10" onkeyup="javascript:caltotal(this)" CssClass="right_textbox"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="submit" runat="server" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true" ControlToValidate="txtadvanceAmount">

                                                        </asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" width="49%">Payment to be done on date</td>
                                                    <td align="left" width="49%">
                                                        <asp:TextBox ID="txtpaymenttobedone" runat="server" MaxLength="50" CssClass="right_textbox"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="submit" runat="server" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true" ControlToValidate="txtpaymenttobedone">

                                                        </asp:RequiredFieldValidator>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" width="49%">budget amount allocated</td>
                                                    <td align="left" width="49%">
                                                        <asp:TextBox ID="txtbugetamountallocated" runat="server" MaxLength="50" CssClass="right_textbox"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td align="left" width="49%">Expected date of expenses/claims submission</td>
                                                    <td align="left" width="49%">
                                                        <asp:TextBox ID="txtexpecteddateofexpences" runat="server" MaxLength="50" CssClass="right_textbox"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="submit" runat="server" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true" ControlToValidate="txtexpecteddateofexpences">

                                                        </asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>

                                                    <%--  <table class="table table-bordered" width="97%">
                                    <tr>
                                        <td width="20%">Number of Expense</td>
                                        <td>
                                            <asp:TextBox ID="txtNOE" AutoPostBack="true" runat="server" MaxLength="2" CssClass="right_textbox"></asp:TextBox></td>
                                    </tr>
                                </table>--%>
                                                    <%--  <asp:GridView ID="gvClaims" runat="server" Width="97%" AutoGenerateColumns="false" CssClass="table table-bordered" ShowFooter="true">
                                    <Columns>
                                        <asp:TemplateField HeaderText=" S.No.">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Expense Head" ItemStyle-Width="350px">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtGV_Particular" runat="server" Width="300px" MaxLength="100" CssClass="textbox"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "ExpenseHead")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtParticular" runat="server" Width="300px" MaxLength="100" CssClass="textbox"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Advance Amt. Req">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtGV_advanceamt" runat="server" MaxLength="100" CssClass="textbox"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "AdvanceAmtReq")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtadvanceamt" onkeypress="javascript:caltotal(this)" runat="server" MaxLength="10" CssClass="textbox"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Budget Amt. Allocated">

                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "BudgetAmtAllocated")%>
                                            </ItemTemplate>

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Action">
                                            <EditItemTemplate>
                                                <asp:Button ValidationGroup="Editvalidation" ID="imgbtnUpdate" CommandName="Update" runat="server" Text="Update" CssClass="btn-small btn-info" />
                                                <asp:Button CausesValidation="false" ID="imgbtnCancel" runat="server" CommandName="Cancel" Text="Cancel" CssClass="btn-small btn-danger" />

                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Button CausesValidation="false" ID="imgbtnEdit" CommandName="Edit" runat="server" Text="Edit" CssClass="btn-small btn-info" />
                                                <asp:Button CausesValidation="false" ID="imgbtnDelete" OnClientClick="javascript:return ConfirmationBox()" CommandName="Delete" Text="Delete" runat="server" CssClass="btn-small btn-danger" />
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Button ID="imgbtnAdd" runat="server" CommandName="Add" ValidationGroup="PC" TabIndex="19" CssClass="btn-small btn-info" Text="Add" />

                                            </FooterTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>--%>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <table class="table table-bordered">
                                                <tr>
                                                    <th>Expense Head</th>
                                                    <th>Advance Amt. Req.</th>
                                                    <th>Budget Amt. Allocated</th>

                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblexpensehead" runat="server" Text=""></asp:Label>

                                                    </td>
                                                    <td style="text-align: right;">
                                                        <asp:Label ID="lbladvanceamountrequest" runat="server" Text="0"></asp:Label></td>
                                                    <td style="text-align: right;">
                                                        <asp:Label ID="lblbudgetamtallocate" runat="server" Text="0"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">&nbsp;
                                                    </td>
                                                    <%-- <td></td>
                                <td></td>--%>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">&nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>

                                        <div class="col-lg-12" style="text-align: center">
                                            <asp:Button ID="btnCancel" runat="server" Text="Back" CssClass="btn btn-primary" CausesValidation="false" />
                                            <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="submit" CssClass="btn btn-primary" />
                                            <asp:Button ID="btnReset" runat="server" Text="Reset" Visible="false" CssClass="btn btn-primary" />
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

    <asp:HiddenField ID="hdnTaskID" runat="server" />
    <asp:HiddenField ID="hdnSubTaskID" runat="server" />
    <asp:HiddenField ID="hdnJobCardID" runat="server" />
    <asp:HiddenField ID="hdnClaimMasterID" runat="server" />
    <asp:HiddenField ID="hdnNotificationID" runat="server" />
    <asp:HiddenField ID="hdnEmpTableID" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpFooter" runat="Server">

    <script src="dist/js/bootstrap-datepicker.js"></script>

    <script type="text/javascript">

        $(function () {
            $('input[id$=txtpaymenttobedone]').datepicker({ format: 'dd-mm-yyyy' });
            $('input[id$=txtexpecteddateofexpences]').datepicker({ format: 'dd-mm-yyyy' });

        });


        function caltotal(obj) {
            var val1 = false;
            var val2 = false;
            //alert(obj.id);

            if ($('input[id$=' + obj.id + ']').val() != "") {

                var numberRegex = /^\d{1,10}(\.\d{0,2})?$/;
                if (numberRegex.test($('input[id$=' + obj.id + ']').val())) {
                    val1 = true
                    if (parseInt($('input[id$=' + obj.id + ']').val()) > parseInt($('[id$=txtbugetamountallocated]').val())) {
                        alert('Amount exceeded the job budget');
                        $('[id$=lbladvanceamountrequest]').text($('[id$=txtbugetamountallocated]').val());
                        $('input[id$=' + obj.id + ']').val($('[id$=txtbugetamountallocated]').val());
                    }
                    else {

                        $('[id$=lbladvanceamountrequest]').text($('input[id$=' + obj.id + ']').val());
                    }


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
        $(document).ready(function () {
            //$("#lbladvanceamountrequest").val("250");
            var txtbugetamountallocated = $('[id$=txtbugetamountallocated]').val();

            $('[id$=lblbudgetamtallocate]').text(txtbugetamountallocated);

        });
    </script>
</asp:Content>

