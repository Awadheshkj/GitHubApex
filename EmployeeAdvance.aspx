<%@ Page Title="" Language="VB" MasterPageFile="~/Apex.master" AutoEventWireup="false" CodeFile="EmployeeAdvance.aspx.vb" Inherits="EmployeeAdvance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>EMPLOYEE'S ADVANCE
        </h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">EMPLOYEE'S ADVANCE</li>
        </ol>
    </section>

    <!-- Main content -->
    <section class="content">
        <div class="row">
            <div class="col-xs-12">
                <div class="box">
                    <div class="box-header">
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                        <div class="subMenuStrip"></div>

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
                                <div class="InnerContentData">

                                    <div class="col-lg-12">

                                        <div class="col-lg-6">
                                            <table class="table table-bordered">
                                                <tr>
                                                    <td align="left" width="49%">Employee Name as Per Salary Sheet</td>
                                                    <td align="left" width="49%">
                                                        <asp:TextBox ID="txtEmployeeName" runat="server" MaxLength="50" Enabled="false" CssClass="form-control"></asp:TextBox>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" width="49%">Employee Code</td>
                                                    <td align="left" width="49%">
                                                        <asp:TextBox ID="txtempcode" runat="server" MaxLength="20" Enabled="false" CssClass="form-control"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td align="left" width="49%">Event Name</td>
                                                    <td align="left" width="49%">
                                                        <asp:TextBox ID="txtEventName" runat="server" MaxLength="50" Enabled="false" CssClass="form-control"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td align="left" width="49%">Event City</td>
                                                    <td align="left" width="49%">
                                                        <asp:TextBox ID="txteventCity" runat="server" MaxLength="50" Enabled="false" CssClass="form-control"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td align="left" width="49%">Event Date</td>
                                                    <td align="left" width="49%">
                                                        <asp:TextBox ID="txteventDate" runat="server" MaxLength="50" Enabled="false" CssClass="form-control"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td align="left" width="49%">Client Name</td>
                                                    <td align="left" width="49%">
                                                        <asp:TextBox ID="txtclientName" runat="server" MaxLength="50" Enabled="false" CssClass="form-control"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td align="left" width="49%">PM Name</td>
                                                    <td align="left" width="49%">
                                                        <asp:TextBox ID="txtPmName" runat="server" MaxLength="50" Enabled="false" CssClass="form-control"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td align="left" width="49%">KAM Name/Account Manager</td>
                                                    <td align="left" width="49%">
                                                        <asp:TextBox ID="txtKamName" runat="server" MaxLength="50" Enabled="false" CssClass="form-control"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td align="left" width="49%">JC #</td>
                                                    <td align="left" width="49%">
                                                        <asp:TextBox ID="txtJCNo" runat="server" MaxLength="50" Enabled="false" CssClass="form-control"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td align="left" width="49%">Advance Amount Request
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="submit" runat="server" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true" ControlToValidate="txtadvanceAmount">

                                                        </asp:RequiredFieldValidator></td>
                                                    <td align="left" width="49%">
                                                        <asp:TextBox ID="txtadvanceAmount" runat="server" MaxLength="10" Enabled="false" CssClass="form-control"></asp:TextBox>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" width="49%">Payment to be done on date 
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="submit" runat="server" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true" ControlToValidate="txtpaymenttobedone">

                                                        </asp:RequiredFieldValidator></td>
                                                    <td align="left" width="49%">
                                                        <asp:TextBox ID="txtpaymenttobedone" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>


                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" width="49%">budget amount allocated</td>
                                                    <td align="left" width="49%">
                                                        <asp:TextBox ID="txtbugetamountallocated" runat="server" Enabled="false" MaxLength="50" CssClass="form-control"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td align="left" width="49%">Expected date of expenses/claims submission
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="submit" runat="server" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true" ControlToValidate="txtexpecteddateofexpences">

                                                        </asp:RequiredFieldValidator></td>
                                                    <td align="left" width="49%">
                                                        <asp:TextBox ID="txtexpecteddateofexpences" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>

                                                    </td>
                                                </tr>
                                            </table>


                                            <table class="table table-bordered" style="display: none;">
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
                                        <div class="col-lg-6">
                                        </div>
                                    </div>
                                    <div class="col-lg-12">
                                        <asp:GridView EnableModelValidation="true" ID="gvempadvance" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No.">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                        <asp:HiddenField  ID="hdntaskID" runat="server" Value='<%# Eval("AccountID") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField DataField="title" HeaderText="Expense Head" />--%>
                                                <asp:TemplateField HeaderText="Expense Head">
                                                    <ItemTemplate>
                                                        <asp:Label  ID="lbltitle" runat="server" Text='<%# Eval("title") %>' Enabled="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Advance Amt. Req.">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtadvancereq" CssClass="form-control" onkeyup="javascript:caltotal(this)"  MaxLength="10" Width="40%" runat="server" Text='<%# Eval("Amount") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Budget Amt. Allocated">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="lblbudget" runat="server" Text='<%# Eval("BAmount") %>' Enabled="false" CssClass="form-control" Width="40%" MaxLength="50"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField DataField="Amount" HeaderText="Budget Amt. Allocated" />--%>
                                            </Columns>
                                        </asp:GridView>



                                    </div>

                                    <div class="col-12" style="text-align: center">
                                        <asp:Button ID="btnCancel" runat="server" Text="Back" CssClass="btn btn-primary" CausesValidation="false" />
                                        <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="submit" CssClass="btn btn-primary" />
                                        <%--OnClientClick="javascript:GVcalculation(0)"--%>
                                        <asp:Button ID="btnReset" runat="server" Text="Reset" Visible="false" CssClass="btn btn-primary" />

                                         <asp:Button ID="btnapproved" runat="server" Text="Approve" CssClass="btn btn-primary" />
                                         <asp:Button ID="btnReject" runat="server" Text="Reject" CssClass="btn btn-primary" />

                                         <asp:Button ID="btnpaymentproccess" runat="server" Text="Payment Proccess" CssClass="btn btn-primary" />

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
 <asp:HiddenField ID="hdnKamID" runat="server" />
 <asp:HiddenField ID="hdnPMID" runat="server" />
 <asp:HiddenField ID="hdnEmployeeadvanceID" runat="server" />
<asp:HiddenField id="hdnadvanceamount" runat="server" Value="0" />  
    <asp:HiddenField  ID="hdnNodificationID" runat="server" />  

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpFooter" runat="Server">
    <script src="dist/js/bootstrap-datepicker.js"></script>

    <script type="text/javascript">

        $(function DatePicker(obj1) {
            $('input[id$=txtpaymenttobedone]').datepicker({ format: 'dd-mm-yyyy', autoclose: true });
            $('input[id$=txtexpecteddateofexpences]').datepicker({ format: 'dd-mm-yyyy', autoclose: true });

        });


        function caltotal(obj) {
            var val1 = false;
            var val2 = false;


            if ($('input[id$=' + obj.id + ']').val() != "") {


                var numberRegex = /^\d{1,10}(\.\d{0,2})?$/;
                if (numberRegex.test($('input[id$=' + obj.id + ']').val())) {
                    val1 = true
                    $('#<%=divError.ClientID%>').hide();
                    $('#<%=divError.ClientID%>').html('');
                    GVcalculation(obj.id)

                }
                else {
                   alert('Enter numeric data only');
                    $('#<%=divError.ClientID%>').show();
                    $('#<%=divError.ClientID%>').html('Enter numeric data only');
                    $('html,body').animate({
                        scrollTop: 0
                    }, 700);
                    $('input[id$=' + obj.id + ']').val("0");
                    val1 = false;
                }
            }
            else {
               // alert('Enter value');
                //$('input[id$=txtQuantity]').val("0");
                $('#<%=btnSave.ClientID%>').hide();
                $('#<%=divError.ClientID%>').show();
                $('#<%=divError.ClientID%>').html('Advance cant not be blank');
                $('html,body').animate({
                    scrollTop: 0
                }, 700);
                $('input[id$=' + obj.id + ']').val("0");
                GVcalculation(obj.id)
            }
        }
        $(document).ready(function () {
            //$("#lbladvanceamountrequest").val("250");
            //var txtbugetamountallocated = $('[id$=txtbugetamountallocated]').val();

            //$('[id$=lblbudgetamtallocate]').text(txtbugetamountallocated);
          $('#<%=hdnadvanceamount.ClientID%>').val($('#<%=txtadvanceAmount.ClientID%>').val());
            $('#<%=divError.ClientID%>').hide();
            //$('#<%=btnSave.ClientID%>').hide();


        });

        function GVcalculation(Advancetxt) {
            var GVMaintainReceiptMaster = document.getElementById('<%= gvempadvance.ClientID%>');
            var advanceamountReq = "0";
            var AllowtedBudget = "0";
            var txtadvanceamountReq = 0;
            for (var rowId = 1; rowId < GVMaintainReceiptMaster.rows.length ; rowId++) {
                advanceamountReq = GVMaintainReceiptMaster.rows[rowId].cells[2].children[0];
                AllowtedBudget = GVMaintainReceiptMaster.rows[rowId].cells[3].children[0];
                //alert(advanceamountReq.value)

                txtadvanceamountReq = txtadvanceamountReq + Math.round(advanceamountReq.value)

                $('#<%=txtadvanceAmount.ClientID%>').val(txtadvanceamountReq);
                $('#<%=hdnadvanceamount.ClientID%>').val(txtadvanceamountReq);

                  if (advanceamountReq.value == "") {
                      alert('blank');
                      //$('input[id$=' + obj.id + ']').val("0");
                      $('#<%=btnSave.ClientID%>').hide();
                    $('#<%=divError.ClientID%>').show();
                    $('#<%=divError.ClientID%>').html('Advance cant not be blank');
                    $('html,body').animate({
                        scrollTop: 0
                    }, 700);
                }
                else {

                    if (Math.round(advanceamountReq.value) > Math.round(AllowtedBudget.value)) {

                        $('#<%=divError.ClientID%>').show();
                        $('#<%=divError.ClientID%>').html('Advance Amount can not greater than the budget amount for each head.');
                        $('html,body').animate({
                            scrollTop: 0
                        }, 700);
                        $('input[id$=' + Advancetxt + ']').val("");
                    }
                    else {

                        // alert(Math.round(advanceamountReq.value) + Math.round(advanceamountReq.value))

                        $('#<%=btnSave.ClientID%>').show();

                    }
                }
            }
        }
    </script>
</asp:Content>

