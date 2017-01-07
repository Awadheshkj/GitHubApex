<%@ Page Title="" Language="VB" MasterPageFile="~/Apex.master" AutoEventWireup="false" CodeFile="BriefManagerView.aspx.vb" Inherits="OpenPrePnLManager" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script>
        $(function () {
            $('input[id$=txtEventDate]').datepicker({ dateFormat: 'dd-mm-yy' });
        });
        $(document).ready(function () {
            $('input[id$=txtNature]').focus();
        });

        function Calc_Grid() {

            if ($('input[id$=txtPreEventCost]').val() != "") {

                var numberRegex = /^\d{1,10}(\.\d{0,2})?$/;
                if (numberRegex.test($('input[id$=txtPreEventCost]').val())) {
                    return true;
                    alert(1);
                }
                else {
                    alert('Enter numeric data only');
                    $('input[id$=txtPreEventCost]').val("");
                    return false;
                    alert(2);
                }
            }

        }

        function Calc_Grid2(obj) {
            var id = obj.id
            var prefix = id.substring(0, 22)
            var postfix = id.substring(id.length - 2, id.length)
            if ($('input[id$=gv_txtPreEventCost' + postfix + ']').val() != "") {

                var numberRegex = /^\d{1,10}(\.\d{0,2})?$/;
                if (numberRegex.test($('input[id$=gv_txtPreEventCost' + postfix + ']').val())) {
                    return true;
                }
                else {

                    alert('Enter numeric data only');
                    $('input[id$=gv_txtPreEventCost' + postfix + ']').val("");
                    return false;
                }
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

            popup('dialog-box');
        });



    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <section class="content-header">
        <h1>Brief Manager
        </h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">Brief Manager</li>
        </ol>
    </section>

    <section class="content">

        <div class="row">
            <div class="col-xs-12">
                <!-- .box -->
                <div class="box">
                    <div class="box-header">
                    </div>
                    <!-- /.box-header -->
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
                                    <div id="dialog-overlay"></div>
                                    <div id="dialog-box">
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
                                            <%-- <a href="#" class="button">Close</a>--%>
                                        </div>
                                    </div>

                                    <div style="display: none">
                                        <div class="col-lg-12">

                                            <div class="form-group">
                                                <label class="col-lg-1 control-label" for="head"></label>
                                                <div class="navbar col-lg-10">
                                                    <div class="bs-example form-horizontal">
                                                        <h3>Brief Manager</h3>
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



                                        <div class="col-lg-12">

                                            <div class="form-group">
                                                <label class="col-lg-1 control-label" for="head"></label>
                                                <div class="col-lg-10">
                                                    <div class="bs-example form-horizontal">

                                                        <div class="form-group">
                                                            <div class="col-lg-4">
                                                                <label for="inputEmail" class="control-label">Client Name</label>
                                                            </div>
                                                            <div class="col-lg-2">
                                                                <asp:Label ID="lblClient" runat="server"></asp:Label>
                                                                <asp:DropDownList ID="ddlClient" runat="server" ValidationGroup="PE" Enabled="False" TabIndex="1" CssClass="form-control"></asp:DropDownList>
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

                                        <div id="tbl2" runat="server" class="col-12">
                                            <asp:GridView ID="gvPrePnLCost" runat="server" DataKeyNames="Row" AutoGenerateColumns="false" ShowFooter="true" PageSize="5"
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
                                                    <asp:TemplateField HeaderText="Supplier Name">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="gv_txtSupplierName" CssClass="form-control input-small" MaxLength="40" runat="server" Text='<%#Eval("SupplierName") %>' />
                                                            <asp:RequiredFieldValidator ID="RFV_gv_txtSupplierName" ValidationGroup="Editvalidation" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="gv_txtSupplierName"></asp:RequiredFieldValidator>

                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container.DataItem, "SupplierName")%>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:TextBox ID="txtSupplier" runat="server" CssClass="form-control input-small" ValidationGroup="PC" MaxLength="40" TabIndex="15"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="PC" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="txtSupplier"></asp:RequiredFieldValidator>
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
                                                    <asp:TemplateField HeaderText="Action" ItemStyle-Width="200px">
                                                        <EditItemTemplate>
                                                            <asp:Button ValidationGroup="Editvalidation" ID="imgbtnUpdate" CommandName="Update" runat="server" Text="Update" CssClass="btn btn-info" />
                                                            <asp:Button CausesValidation="false" ID="imgbtnCancel" runat="server" CommandName="Cancel" Text="Cancel" CssClass="btn btn-danger" />

                                                        </EditItemTemplate>
                                                        <ItemTemplate>

                                                            <%--<%# IIf(Eval("REquestAmt") = "0.00", "N/A", "some text")%>--%>

                                                            <%-- <%# If(Eval("REquestAmt").ToString() = "0.00", " ", Eval("REquestAmt").ToString())%>--%>

                                                            <asp:Button CausesValidation='false' ID='imgbtnEdit' CommandName='Edit' runat='server' Text='<%# If(Eval("REquestAmt").ToString() = "0.00", "Edit", Eval("REquestAmt").ToString())%>' Enabled='<%# If(Eval("REquestAmt").ToString() = "0.00", "True", "false")%>' CssClass='btn btn-info' />
                                                            <%--<asp:Button CausesValidation='false' ID='imgbtnEdit' CommandName='Edit' runat='server' Text='<%# If(Eval("REquestAmt").ToString() = "0.00", "Edit", Eval("REquestAmt").ToString())%>'  CssClass='btn btn-info' /> --%>

                                                            <asp:Button CausesValidation="false" ID="imgbtnDelete" OnClientClick="javascript:return ConfirmationBox()" Text="Delete" runat="server" CssClass="btn btn-danger" Visible='<%# If(Eval("SendReq").ToString() = "N", "True", "False")%>' CommandArgument='<%# Eval("ID") %>' OnClick="imgbtnDelete_Click" />
                                                            <%--<asp:Button CausesValidation="false" ID="imgbtnDelete" OnClientClick="javascript:return ConfirmationBox()" CommandName="Delete" Text="Delete" runat="server" CssClass="btn btn-danger" />--%>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Button ID="imgbtnAdd" runat="server" CommandName="Add" ValidationGroup="PC" TabIndex="19" Text="Add" CssClass="btn btn-info" />

                                                        </FooterTemplate>

                                                        <ItemStyle Width="200px"></ItemStyle>
                                                    </asp:TemplateField>
                                                </Columns>

                                            </asp:GridView>

                                        </div>

                                        <div style="float: right; padding-right: 250px;">
                                            <asp:Button ID="btnsendapproval" runat="server" Visible="false" CssClass="btn btn-primary" Text="Send Request for Approval" />
                                        </div>

                                        <div class="form-group">
                                            <label class="col-lg-1 control-label" for="head"></label>
                                            <div class="col-lg-10">
                                                <div class="bs-example form-horizontal">

                                                    <div class="form-group">

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
                                                            <label for="inputEmail" class="control-label">Total Pre Event Cost</label>
                                                        </div>

                                                        <div class="col-lg-2">
                                                            <asp:Label ID="lblTCost" runat="server"></asp:Label>
                                                            <asp:TextBox ID="lblTotalCost" runat="server" ReadOnly="true" CssClass="form-control" TabIndex="21"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <div class="col-lg-4">
                                                            <label for="inputEmail" class="control-label">
                                                                Total Service Tax</label>
                                                        </div>
                                                        <div class="col-lg-2">
                                                            <asp:Label ID="lblTSTax" runat="server"></asp:Label>
                                                            <asp:TextBox ID="lblTotalServiceTax" runat="server" ReadOnly="true" CssClass="form-control" TabIndex="22"></asp:TextBox>
                                                        </div>
                                                    </div>



                                                    <div class="form-group">
                                                        <div class="col-lg-4">
                                                            <label for="inputEmail" class="control-label">Pre Event Total</label>
                                                        </div>
                                                        <div class="col-lg-2">
                                                            <asp:Label ID="lblPETotal" runat="server"></asp:Label>
                                                            <asp:TextBox ID="lblPreEventTotal" runat="server" ReadOnly="true" CssClass="form-control" TabIndex="23"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
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

                                                    <div class="form-group">
                                                        <div class="col-lg-4">
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


                        <asp:HiddenField ID="hdnPnLID" runat="server" />
                        <asp:HiddenField ID="hdnBriefID" runat="server" />
                        <asp:HiddenField ID="hdnJobCardID" runat="server" />
                        <asp:HiddenField ID="hdnNodificationID" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpFooter" runat="Server">
</asp:Content>
