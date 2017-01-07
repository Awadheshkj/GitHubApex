<%@ Page Title="" Language="VB" MasterPageFile="~/apex.master" AutoEventWireup="false" CodeFile="RembursementClaim.aspx.vb" Inherits="RembursementClaim" %>

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
                        <h3>Claims</h3>
                        <div id="divError" runat="server" class="ui-state-error ui-corner-all divError">
                            <p>
                                <span class="ui-icon ui-icon-alert" style="float: left; margin-right: .3em;"></span>
                                <strong>Warning: </strong>
                                <asp:Label ID="lblError" runat="server"></asp:Label>
                            </p>
                        </div>
                        <div class="alert alert-success" id="MessageDiv" visible="false" runat="server">
                            <strong>Message: </strong>
                           <%-- <asp:Label ID="Label1" runat="server" Text=""></asp:Label>--%>
                            <asp:Literal ID="Label1" runat="server" ></asp:Literal>

                        </div>
                        <%--    <asp:Button ID="Button1" runat="server" ValidationGroup="sdsd" Text="Button" />--%>
                        <div id="divContent" runat="server">
                            <div class="inner-Content-container">
                                <div class="InnerContentData">

                                    <div class="col-12">
                                        <div class="form-group col-12">
                                            <span class="col-2">Employee Name</span>
                                            <span class="col-6">
                                                <asp:Label ID="lblEmployeeName" runat="server"></asp:Label></span>
                                        </div>
                                        <div class="form-group col-12">
                                            <span class="col-2">Employee Code </span>
                                            <span class="col-6">
                                                <asp:Label ID="lblEmployeeCode" runat="server"></asp:Label></span>
                                        </div>
                                        <div class="form-group col-12">
                                            <span class="col-2">Expense Period /Month</span>
                                            <span class="col-6">
                                                <asp:TextBox ID="txtExpensePeriodfrom" runat="server"></asp:TextBox>&nbsp; TO &nbsp;
                            <asp:TextBox ID="txtExpensePeriodto" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="" SetFocusOnError="true" ControlToValidate="txtExpensePeriodfrom" Display="Dynamic"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="" SetFocusOnError="true" ControlToValidate="txtExpensePeriodto" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </span>
                                        </div>
                                        <div class="form-group col-12">
                                            <span class="col-2">Expense Place </span>
                                            <span class="col-6">
                                                <asp:TextBox ID="txtExpensePlace" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Expense Place Required" ForeColor="Red" SetFocusOnError="true" ControlToValidate="txtExpensePlace" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </span>
                                        </div>
                                        <div class="form-group col-12" style="display: block;">
                                            <span class="col-2">Project Name</span>
                                            <span class="col-6">
                                                <asp:Label ID="lblprojectName" runat="server"></asp:Label></span>
                                            
                                        </div>
                                        <div class="form-group col-12">
                                            <span class="col-2">KAM</span>
                                            <span class="col-6">
                                                <asp:Label ID="lblKAM" runat="server"></asp:Label></span>
                                        </div>
                                        <div class="form-group col-12">
                                            <span class="col-2">Project Manager</span>
                                            <span class="col-6">
                                                <asp:Label ID="lblManager" runat="server"></asp:Label></span>
                                        </div>

                                        <div class="form-group col-12">
                                            <span class="col-2">Job Code</span>
                                            <span class="col-6">
                                                <asp:Label ID="lblJobCode" runat="server"></asp:Label></span>
                                        </div>

                                        <div class="form-group col-12">
                                            <span class="col-2">E-Mail ID</span>
                                            <span class="col-6">
                                                <asp:Label ID="txtEMailID" runat="server"></asp:Label></span>
                                        </div>
                                        <div class="form-group col-12">
                                            <span class="col-2">Contact Number</span>
                                            <span class="col-6">
                                                <asp:Label ID="lblContactNumber" runat="server"></asp:Label></span>
                                        </div>

                                        <div class="form-group col-12" style="display: none;">
                                            <span class="col-2">Task</span>
                                            <span class="col-6">
                                                <asp:Label ID="lblTaskTitle" runat="server"></asp:Label></span>
                                        </div>
                                        <div class="form-group col-12" style="display: none;">
                                            <span class="col-2">Task Particulars</span>
                                            <span class="col-6">
                                                <asp:Label ID="lblparticulars" runat="server"></asp:Label></span>
                                        </div>
                                        <div class="form-group col-12" style="display: block;">
                                            <span class="col-2">Amount</span>
                                            <span class="col-6">
                                                <asp:Label ID="lblAmount" runat="server"></asp:Label></span>
                                            <asp:HiddenField ID="hdnTaskAccountID" runat="server" Value="0" />
                                        </div>
                                        <div class="col-12" style="width: 98%; height: 325px; overflow: scroll; scrollbar-arrow-color: blue; scrollbar-base-color: Background;">
                                            <asp:GridView ID="gvClaims" runat="server" Width="97%" AutoGenerateColumns="false" CssClass="table table-bordered">
                                                <Columns>
                                                    <asp:TemplateField HeaderText=" S.No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="EXP_Category">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlexp_category" runat="server" CssClass="textbox" Width="120px">
                                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--<asp:TemplateField HeaderText="Claim Prepration Date">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtdate" runat="server" MaxLength="50" Width="120px" CssClass="textbox"></asp:TextBox>
                                                                                                                    </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate><span style="color: red;">*</span>Description</HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtParticular" runat="server" MaxLength="100" CssClass="textbox"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                 <%--    <asp:TemplateField HeaderText="Hotel">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txthotel" runat="server" MaxLength="100" Width="120px" CssClass="textbox"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="No of Days">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtnumberofdays" runat="server" MaxLength="8" onkeyup="javascript:caltotal(this)" Width="50" CssClass="textbox"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Qty">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtqty" runat="server" MaxLength="8" onkeyup="javascript:caltotal(this)" Width="50" CssClass="textbox"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Rate">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtrate" runat="server" MaxLength="8" onkeyup="javascript:caltotal(this)" Width="50" CssClass="textbox"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="From (city)">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtfrom" Width="80" runat="server" MaxLength="100" CssClass="textbox"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="To (city)">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtto" runat="server" Width="80" MaxLength="100" CssClass="textbox"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField >
                                                        <HeaderTemplate><span style="color: red;">*</span>Employee/Vendor Name</HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtvendername" runat="server" Width="125px" MaxLength="100" CssClass="textbox"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate><span style="color: red;">*</span>Amount</HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtAmount" runat="server" MaxLength="8" onkeyup="javascript:caltotal(this)" CssClass="right_textbox" Width="50"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remarks">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtremarks" runat="server" MaxLength="100" CssClass="textbox"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>

                                        <div class="form-group col-12" style="padding-top: 20px;">
                                            <span class="col-2">Upload Documents
                                            </span>
                                            <span class="col-6">
                                                <asp:FileUpload ID="FUpld_Documents" runat="server" onchange="return CheckUpl();" />
                                            </span>
                                        </div>
                                        <div class="form-group col-12">
                                            <span class="col-2">Remarks
                                            </span>
                                            <span class="col-6">
                                                <asp:TextBox ID="txtRemarks" runat="server" onKeyPress="return charLimit(this,'lbl_Remarkswarning','490')" onKeyUp="return characterCount(this,'490')" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                                <br />
                                                <span id="lbl_Remarkswarning" class="lblLimit"></span>
                                            </span>
                                        </div>
                                        <div class="col-12" style="text-align: center">
                                            <asp:Button ID="btnCancel" runat="server" Text="Back" CssClass="btn btn-primary" CausesValidation="false" />
                                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" />
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

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpFooter" runat="Server">
    <script src="dist/js/bootstrap-datepicker.js"></script>

    <script type="text/javascript">
        $(function DatePicker(obj1) {

            $('input[id$=txtExpensePeriodfrom]').datepicker({ format: 'dd-mm-yyyy', autoclose: true });
            //$('input[id$=' + obj1.id + ']').datepicker({ format: 'dd-mm-yyyy' });
            $('input[id$=txtExpensePeriodfrom]').datepicker({ format: 'dd-mm-yyyy', autoclose: true });
            $('input[id$=txtExpensePeriodto]').datepicker({ format: 'dd-mm-yyyy', autoclose: true });

            //$('input[id$=txtExpensePeriodto]').datepicker({ format: 'dd-mm-yyyy' });
            $('input[id$=ContentPlaceHolder1_gvClaims_txtdate_0]').datepicker({ format: 'dd-mm-yyyy', autoclose: true });
            $('input[id$=ContentPlaceHolder1_gvClaims_txtdate_1]').datepicker({ format: 'dd-mm-yyyy', autoclose: true });
            $('input[id$=ContentPlaceHolder1_gvClaims_txtdate_2]').datepicker({ format: 'dd-mm-yyyy', autoclose: true });
            $('input[id$=ContentPlaceHolder1_gvClaims_txtdate_3]').datepicker({ format: 'dd-mm-yyyy', autoclose: true });
            $('input[id$=ContentPlaceHolder1_gvClaims_txtdate_4]').datepicker({ format: 'dd-mm-yyyy', autoclose: true });
            $('input[id$=ContentPlaceHolder1_gvClaims_txtdate_5]').datepicker({ format: 'dd-mm-yyyy', autoclose: true });
            $('input[id$=ContentPlaceHolder1_gvClaims_txtdate_6]').datepicker({ format: 'dd-mm-yyyy', autoclose: true });
            $('input[id$=ContentPlaceHolder1_gvClaims_txtdate_7]').datepicker({ format: 'dd-mm-yyyy', autoclose: true });
            $('input[id$=ContentPlaceHolder1_gvClaims_txtdate_8]').datepicker({ format: 'dd-mm-yyyy', autoclose: true });
            $('input[id$=ContentPlaceHolder1_gvClaims_txtdate_9]').datepicker({ format: 'dd-mm-yyyy', autoclose: true });
            $('input[id$=ContentPlaceHolder1_gvClaims_txtdate_10]').datepicker({ format: 'dd-mm-yyyy', autoclose: true });
            $('input[id$=ContentPlaceHolder1_gvClaims_txtdate_11]').datepicker({ format: 'dd-mm-yyyy', autoclose: true });
            $('input[id$=ContentPlaceHolder1_gvClaims_txtdate_12]').datepicker({ format: 'dd-mm-yyyy', autoclose: true });
            $('input[id$=ContentPlaceHolder1_gvClaims_txtdate_13]').datepicker({ format: 'dd-mm-yyyy', autoclose: true });
            $('input[id$=ContentPlaceHolder1_gvClaims_txtdate_14]').datepicker({ format: 'dd-mm-yyyy', autoclose: true });
            $('input[id$=ContentPlaceHolder1_gvClaims_txtdate_15]').datepicker({ format: 'dd-mm-yyyy', autoclose: true });
            $('input[id$=ContentPlaceHolder1_gvClaims_txtdate_16]').datepicker({ format: 'dd-mm-yyyy', autoclose: true });
            $('input[id$=ContentPlaceHolder1_gvClaims_txtdate_17]').datepicker({ format: 'dd-mm-yyyy', autoclose: true });
            $('input[id$=ContentPlaceHolder1_gvClaims_txtdate_18]').datepicker({ format: 'dd-mm-yyyy', autoclose: true });
            $('input[id$=ContentPlaceHolder1_gvClaims_txtdate_19]').datepicker({ format: 'dd-mm-yyyy', autoclose: true });
            $('input[id$=ContentPlaceHolder1_gvClaims_txtdate_20]').datepicker({ format: 'dd-mm-yyyy', autoclose: true });


        });



    </script>
    <script>
        function CheckUpl() {
            var filename = $('input[id$=FUpld_Documents]').val();
            var fileInput = $('input[id$=FUpld_Documents]')[0];
            var sizeinbytes = fileInput.files[0].size;

            var fSExt = new Array('Bytes', 'KB', 'MB', 'GB');
            fSize = sizeinbytes;
            i = 0;
            while (fSize > 900) {
                fSize /= 1024; i++;
            }

            if (sizeinbytes >= 10485760) {

                $('input[id$=FUpld_Documents]').val("");
                //$('input[id$=txtUploads]').val("");
                alert("File size not more than 10MB");

                return false;
            }
            //var isOpera = !!(window.opera && window.opera.version);  // Opera 8.0+
            //var isFirefox = testCSS('MozBoxSizing');                 // FF 0.8+
            //var isSafari = Object.prototype.toString.call(window.HTMLElement).indexOf('Constructor') > 0;
            // At least Safari 3+: "[object HTMLElementConstructor]"
            //var isChrome = !isSafari && testCSS('WebkitTransform');  // Chrome 1+
            //var isIE = /*@cc_on!@*/false || testCSS('msTransform');  // At least IE6


            if (window.chrome || $.browser.msie || $.browser.safari || window.opera) {
                var onlyname = filename.substring(12, filename.lastIndexOf('.'));
            }
            else {
                var onlyname = filename.substring(filename.lastIndexOf('/') + 1, filename.lastIndexOf('.'));
            }


            if (window.chrome || $.browser.msie || $.browser.safari || window.opera) {
                var extname = filename.substring(filename.lastIndexOf('.') + 1, filename.length);
            }
            else {
                var extname = filename.substring(onlyname.length + 1, filename.length);
            }

            if (extname == "bat" || extname == "sys" || extname == "exe") {
                alert('File upload not allowed for this file type');
                $('input[id$=FUpld_Documents]').val("");
                //$('input[id$=txtUploads]').val("");
                return false;
            }
            else {
                //$('input[id$=txtUploads]').val(onlyname);
                return true;
            }
        }

        function checkVal() {
            if ($('input[id$=FUpld_Documents]').val() == "") {
                alert('No file to upload')
                return false;
            }
        }
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

