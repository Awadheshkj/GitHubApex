<%@ Page Title="" Language="VB" MasterPageFile="~/Apex.master" AutoEventWireup="false" EnableEventValidation="false" CodeFile="Task.aspx.vb" Inherits="Task" %>

<%--<%@ Page Title="" Language="VB" MasterPageFile="~/KestoneMaster.master" AutoEventWireup="false" CodeFile="PMTaskList.aspx.vb" Inherits="PMTaskList" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <link href="dist/css/bootstrap-datepicker3.css" rel="stylesheet" />
   
    <style type="text/css">
        .auto-style1 {
            height: 30px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Create Task
        </h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">Create Task</li>
        </ol>
    </section>

    <!-- Main content -->
    <section class="content">
        <div class="row">
            <div class="col-xs-12">
                <!-- .box -->
                <div class="box">
                    <div class="box-header">
                        <h3 class="box-title"></h3>
                        <asp:Button ID="btnSubTask" runat="server" CssClass="pull-right btn btn-primary btn-sm" Text="Manage Sub Task"  />
                       
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
                        <div class="alert alert-success" id="MessageDiv" visible="false" runat="server">
                            <strong>Message: </strong>
                            <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>

                        </div>
                        <div id="divContent" runat="server">
                            <div class="inner-Content-container">
                                <%--chander working--%>

                                <div class="InnerContentData">
                                    <div class="col-lg-12" style="display: block;">
                                        <div class="form-group col-lg-12">
                                            <span class="col-lg-3">Activity Type*</span>
                                            <span class="col-lg-4">
                                                <asp:Label ID="lblActivity" runat="server"></asp:Label>
                                                <asp:DropDownList runat="server" ID="ddlActivity" TabIndex="1" CssClass="form-control"></asp:DropDownList>
                                            </span>
                                            <span class="col-lg-5">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" SetFocusOnError="true" ControlToValidate="ddlActivity" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                            </span>
                                        </div>
                                    </div>
                                    <div class="col-lg-12">
                                        <div class="form-group col-lg-12">
                                            <span class="col-lg-3">Category*</span>
                                            <span class="col-lg-4">
                                                <asp:Label ID="lblCategory" runat="server" Visible="false"></asp:Label>
                                                <asp:DropDownList runat="server" ID="ddlCategory" CssClass="form-control" TabIndex="2"></asp:DropDownList>

                                            </span>
                                            <span class="col-lg-5">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" SetFocusOnError="true" ControlToValidate="ddlCategory" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                            </span>
                                        </div>
                                    </div>

                                    <div class="col-lg-12">
                                        <div class="form-group col-lg-12">
                                            <span class="col-lg-3">
                                                <asp:Label runat="server" ID="Label2" Text="Sub Category*"></asp:Label></span>
                                            <span class="col-lg-4">
                                                <asp:Label ID="lblSubCategory" runat="server" Visible="false"></asp:Label>
                                                <asp:DropDownList runat="server" ID="ddlSubCategory" CssClass="form-control" TabIndex="3">
                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </span>
                                            <span class="col-lg-5">
                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" SetFocusOnError="true" ControlToValidate="ddlCategory" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                            </span>
                                        </div>
                                    </div>

                                    <div class="col-lg-12">
                                        <div class="form-group col-lg-12">
                                            <span class="col-lg-3">Task Name*</span>
                                            <span class="col-lg-4">
                                                <asp:Label ID="lblTitle" runat="server"></asp:Label>
                                                <asp:TextBox runat="server" ID="txtTitle" MaxLength="40" TabIndex="4" CssClass="form-control"></asp:TextBox>

                                            </span>
                                            <span class="col-lg-5">
                                                <asp:RequiredFieldValidator runat="server" ID="rfv" ControlToValidate="txtTitle" ErrorMessage="*" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </span>
                                        </div>
                                    </div>

                                    <div class="col-lg-12">
                                        <div class="form-group col-lg-12">
                                            <span class="col-lg-3">Job Code</span>
                                            <span class="col-lg-4">

                                                <asp:Label ID="lblJobCode" CssClass="form-control" runat="server"></asp:Label>


                                            </span>
                                            <span class="col-lg-5"></span>
                                        </div>
                                    </div>

                                    <div class="col-lg-12">
                                        <div class="form-group col-lg-12">
                                            <span class="col-lg-3">Start Date*</span>
                                            <span class="col-lg-4">
                                                <asp:Label ID="lblStartDate" runat="server"></asp:Label>
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtStartDate" onkeypress="return false" onkeyup="return false" onkeydown="return false" TabIndex="6" onchange="javascript:return checkDates();"></asp:TextBox>
                                            </span>
                                            <span class="col-lg-5">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" SetFocusOnError="true" ControlToValidate="txtStartDate" ForeColor="Red"></asp:RequiredFieldValidator>

                                            </span>
                                        </div>
                                    </div>
                                    <div class="col-lg-12">
                                        <div class="form-group col-lg-12">
                                            <span class="col-lg-3">End Date*</span>
                                            <span class="col-lg-4">
                                                <asp:Label ID="lblEndDate" runat="server"></asp:Label>
                                                <asp:TextBox runat="server" ID="txtEndDate" TabIndex="7" CssClass="form-control" onkeypress="return false" onkeyup="return false" onkeydown="return false" onchange="javascript:return checkDates();"></asp:TextBox>

                                            </span>
                                            <span class="col-lg-5">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" SetFocusOnError="true" ControlToValidate="txtEndDate" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </span>
                                        </div>
                                    </div>
                                    <%-- <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate></ContentTemplate>
                </asp:UpdatePanel>--%>
                                    <div class="col-lg-12">
                                        <div class="form-group col-lg-12">
                                            <span class="col-lg-3">Vendor</span>
                                            <span class="col-lg-4">
                                                <asp:Label ID="lblVendor" runat="server"></asp:Label>
                                                <asp:DropDownList runat="server" ID="ddlVendor" CssClass="form-control" onchange="showvdetail(this.value)" TabIndex="8"></asp:DropDownList>


                                            </span>
                                            <span class="col-lg-5"></span>
                                        </div>
                                    </div>

                                    <div class="col-lg-12">
                                        <div class="form-group col-lg-12">
                                            <table id="tblVendor" style="width: 60%; border: 1px solid #808080;">
                                                <tr>
                                                    <td colspan="4" style="padding: 5px 0px 5px 50px;" class="auto-style1">Add vendor details</td>
                                                </tr>

                                                <tr>
                                                    <td style="padding: 5px 0px 5px 50px;">
                                                        <asp:DropDownList ID="ddlVendorCategory" runat="server" Width="150px" ValidationGroup="CT" CssClass="form-control" TabIndex="6"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" SetFocusOnError="true" runat="server" ErrorMessage="Vendor Category Required" ControlToValidate="ddlVendorCategory" ValidationGroup="CT" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator></td>
                                                    <td>
                                                        <asp:TextBox ID="txtVendorName" runat="server" Width="150px" CssClass="form-control" MaxLength="40" TabIndex="7"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator43" runat="server" ErrorMessage="Please Enter Vendor Name" ControlToValidate="txtVendorName" InitialValue="Enter Vendor Name" ValidationGroup="CT" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td style="padding: 5px 0px 5px 50px; text-align: left;">
                                                        <asp:Button ID="btnAddClient" runat="server" Text="Add" ValidationGroup="CT" CssClass="btn btn-primary" TabIndex="9" />
                                                        <%--<asp:Button ID="bntcancle" runat="server" Text="cancel" ValidationGroup="CTt" CssClass="btn btn-primary" TabIndex="10" OnClientClick="clear()" />--%>
                                                        <%--<button class ="btn btn-primary close" onclick="clear()">Close</button>--%>
                                                        <div class="closedata btn btn-primary">Close</div>

                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>

                                    <div class="col-lg-12">
                                        <div class="form-group col-lg-12">
                                            <span class="col-lg-3">Contact Person Name</span>
                                            <span class="col-lg-4">
                                                <asp:Label ID="lblContactPerson" runat="server"></asp:Label>
                                                <asp:DropDownList runat="server" ID="ddlContactPersonName" CssClass="form-control" onchange="showaddnew()" TabIndex="9"></asp:DropDownList></td>
                            


                                            </span>
                                            <span class="col-lg-5"></span>
                                        </div>
                                    </div>

                                    <div class="col-lg-12">
                                        <div class="form-group col-lg-12">
                                            <table id="tblVendorContacts" style="width: 80%; border: 1px solid #808080">
                                                <tr>
                                                    <td colspan="4" style="padding: 5px 0px 5px 50px;">Add Client contact person</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px 0px 5px 30px;">
                                                        <asp:TextBox ID="txtContactPerson" runat="server" Width="150px" CssClass="form-control" MaxLength="40" TabIndex="11"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" SetFocusOnError="true" runat="server" ErrorMessage="Name Required" InitialValue="Enter Name" ControlToValidate="txtContactPerson" ValidationGroup="CP" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td style="padding: 5px 0px 5px 30px;">
                                                        <asp:TextBox ID="txtContactOfficialEmail" runat="server" Width="200px" CssClass="form-control" MaxLength="40" TabIndex="12"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" SetFocusOnError="true" runat="server" ErrorMessage="Enter Contact Official Email" InitialValue="Enter Contact Official Email" ControlToValidate="txtContactOfficialEmail" ValidationGroup="CP" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" SetFocusOnError="true" runat="server" ControlToValidate="txtContactOfficialEmail" Display="Dynamic" ErrorMessage="Invalid Email" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                                    </td>
                                                    <td style="padding: 5px 0px 5px 30px;">
                                                        <asp:TextBox ID="txtContactMobile1" runat="server" Width="150px" MaxLength="10" CssClass="form-control" TabIndex="13"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" SetFocusOnError="true" runat="server" ErrorMessage="Enter Mobile Number" InitialValue="Enter Mobile No" ControlToValidate="txtContactMobile1" ValidationGroup="CP" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" SetFocusOnError="true" runat="server" ControlToValidate="txtContactMobile1" Display="Dynamic" ErrorMessage="Invalid Mobile No" ForeColor="Red" ValidationExpression="\d{10}"></asp:RegularExpressionValidator>
                                                    </td>
                                                    <td style="padding: 5px 0px 5px 50px; width: 250px">
                                                        <asp:Button ID="btnContactPerson" runat="server" Text="Add" ValidationGroup="CP" CssClass="btn btn-primary" TabIndex="14" />
                                                        <%--<asp:Button ID="Button1" runat="server" Text="cancel" ValidationGroup="CTq" CssClass="btn btn-primary" TabIndex="15" />--%>
                                                        <%--<button class ="btn btn-primary" id="btnclose" >Close</button>
                                    <span onclick="clear()">Close</span>--%>
                                                        <div class="closedata1 btn btn-primary">Close</div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>

                                    <div class="col-lg-12">
                                        <div class="form-group col-lg-12">
                                            <span class="col-lg-3">Description*</span>
                                            <span class="col-lg-4">
                                                <asp:Label ID="lblDescription" runat="server"></asp:Label>
                                                <asp:TextBox runat="server" ID="txtDescription" CssClass="form-control" onKeyPress="return charLimit(this,'lbl_Descriptionwarning','200')" onKeyUp="return characterCount(this,'200')" TextMode="MultiLine" MaxLength="400" TabIndex="10"></asp:TextBox>



                                            </span>
                                            <span class="col-lg-5">

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*" ControlToValidate="txtDescription" ForeColor="Red"></asp:RequiredFieldValidator>
                                                <span id="lbl_Descriptionwarning" class="lblLimit"></span>

                                            </span>
                                        </div>
                                    </div>

                                    <div class="col-lg-12">
                                        <div class="form-group col-lg-12">
                                            <span class="col-lg-3">Remarks</span>
                                            <span class="col-lg-4">
                                                <asp:Label ID="lblRemarks" runat="server"></asp:Label>
                                                <asp:TextBox runat="server" ID="txtRemarks" onKeyPress="return charLimit(this,'lbl_Remarkswarning','400')" onKeyUp="return characterCount(this,'400')" TextMode="MultiLine" CssClass="form-control" MaxLength="400" TabIndex="11"></asp:TextBox>
                                                <span id="lbl_Remarkswarning" class="lblLimit"></span>




                                            </span>
                                            <span class="col-lg-5">

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ControlToValidate="txtDescription" ForeColor="Red"></asp:RequiredFieldValidator>
                                                <span id="Span1" class="lblLimit"></span>

                                            </span>
                                        </div>
                                    </div>
                                    <div class="form-group col-9">
                                        <center>
                         <asp:Button ID="btnCancel" runat="server" Text="Back" CssClass="btn btn-primary" CausesValidation="false" TabIndex="12" />
                    <%--<asp:Button runat="server" ID="btnNext" Text="Next" CssClass="btn btn-primary" TabIndex="13" />--%>
                    <asp:Button runat="server" ID="btnEditNext" Text="Next" ValidationGroup="1" CssClass="btn btn-primary" TabIndex="14" />
                        <asp:Button ID="btnn" runat="server" Text="Next" ValidationGroup="1" CssClass="btn btn-primary" TabIndex="13"></asp:Button>

                       <%-- <asp:Button ID="btnCancel" runat="server" Text="Back" Visible="false" CausesValidation="false" CssClass="btn btn-primary" TabIndex="30" />
                        <asp:Button ID="btnCancelHome" runat="server" Text="Back" CausesValidation="false" CssClass="btn btn-primary" TabIndex="30" />
                        <asp:Button ID="btnAdd" runat="server" Text="Save" CssClass="btn btn-primary" TabIndex="31" />
                        <asp:Button ID="btnEdit" runat="server" Text="Save" CssClass="btn btn-primary" TabIndex="32" />

                        <asp:Button ID="btnBrief" runat="server" Text="Convert to Brief" CausesValidation="false" CssClass="btn btn-primary" TabIndex="34" />--%>
                            </center>
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
    <asp:HiddenField ID="hdncategory" runat="server" />

    <asp:HiddenField ID="hdnJobCardID" runat="server" />
    <asp:HiddenField ID="hdnBriefID" runat="server" />

    <%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cpFooter" runat="Server">
     <script src="dist/js/bootstrap-datepicker.js"></script>

    <script type="text/javascript">
        $(function () {
            $('input[id$=txtStartDate]').datepicker({ format: 'dd-mm-yyyy' });
            $('input[id$=txtEndDate]').datepicker({ format: 'dd-mm-yyyy' });
            $("#tblVendor").hide();
            $("#tblVendorContacts").hide();

            $(".closedata").click(function () {
                $("#<%=ddlVendor.ClientID%>").val("0");
                showvdetail(0)
                clear()
                //$('#ddlVendor').val('0');

            });
            $(".closedata1").click(function () {
                clear()
                //$('#ddlVendor').val('0');
                //$("#<%=ddlVendor.ClientID%>").val("0");
                $("#<%=ddlContactPersonName.ClientID%>").val("0");
            });
        });
        function clear() {
            //$("select[id$=ddlVendorCategory]").empty();
            $("#tblVendor").hide();
            $("#tblVendorContacts").hide();
            $("[id$=txtVendorName]").empty();
            $("[id$=txtContactPerson]").empty();
            $("[id$=txtContactOfficialEmail]").empty();
            $("[id$=txtContactMobile1]").empty();

        }

        //$(document).ready(function () {
        //    $('#ddlVendor').change(function () {
        //        $("#ddlVendor option:selected").text();
        //        alert('Value change to ' + $(this).attr('value'));
        //    });
        //});
        function showaddnew() {
            if ($('[id$=ddlContactPersonName]').find('option:selected').text() != "Non Existing(New Person)") {
                $("#tblVendorContacts").hide();
            } else {
                $("#tblVendorContacts").show();
            }
        }
        function showvdetail(vid) {
            //if ($("#ddlVendor").val != "Non Existing(New Vendor)") {
            if ($('[id$=ddlVendor]').find('option:selected').text() != "Non Existing(New Vendor)") {
                $("#tblVendor").hide();
                $.ajax({
                    url: "AjaxCalls/AX_Client.aspx?call=3&id=" + vid,
                    context: document.body,
                    success: function (Result) {
                        if (Result != "") {
                            var JSONworkTypes = JSON.parse(Result);
                            $("select[id$=ddlContactPersonName]").empty();
                            //add=1

                            $("select[id$=ddlContactPersonName]").append($("<option></option>").val("0").text("Select"));

                            for (var i = 0; i < JSONworkTypes.Table.length; i++) {

                                $("select[id$=ddlContactPersonName]").append($("<option></option>").val(JSONworkTypes.Table[i].VendorContactID.toString()).text(JSONworkTypes.Table[i].ContactName.toString()));

                            }

                            $("select[id$=ddlContactPersonName]").append($("<option></option>").val("Non Existing(New Person)").text("Non Existing(New Person)"));

                            //                            $('#ddlContactPerson').val('2');
                        }

                    }
                });

            }
            else {
                $("#tblVendor").show();
                //alert(1);
            }

        }

        function checkDates() {
            var objDate = new Date();
            if (document.getElementById("<%=txtStartDate.ClientID %>").value != "" && document.getElementById("<%=txtEndDate.ClientID %>").value != "") {
                var selectedDate = document.getElementById("<%=txtStartDate.ClientID %>").value;
                var selectedDate2 = document.getElementById("<%=txtEndDate.ClientID %>").value;

                //var currentDate = $.datepicker.formatDate('dd/mm/yy', new Date());
                //alert(currentDate);
                if (process(selectedDate) > process(selectedDate2)) {
                    $('input[id$=txtStartDate]').val("");
                    $('input[id$=txtEndDate]').val("");
                    alert("End Date should be later than Start Date!");
                    return false;
                }
                else {
                    return true;
                }
            }
        }

        function process(date) {
            var parts = date.split("-");
            var date = new Date(parts[1] + "/" + parts[0] + "/" + parts[2]);
            return date.getTime();
        }


        function ValidRemarks() {

            //var textBox = $('input[id$=txtRemarks]').val()
            //var textLength = textBox.value.length;
            alert($('#ctl00$CPH_Main$txtRemarks').val().length);
            if ($('#ctl00$CPH_Main$txtRemarks').val().length >= 2) {
                alert("Maximum limit 400 characters for Remarks field");
                return false;
            }
            else {
                return true;
            }
        }

        $(document).ready(function () {
            var watermark1 = 'Enter Vendor Name';
            var txt = $('input[id$=txtVendorName]').val();
            //init, set watermark text and class
            $('input[id$=txtVendorName]').val(watermark1).addClass('watermark');
            //if blur and no value inside, set watermark text and class again.
            $('input[id$=txtVendorName]').blur(function () {
                if ($('input[id$=txtVendorName]').val() == 0) {
                    $('input[id$=txtVendorName]').val(watermark1).addClass('watermark');
                }
            });

            //if focus and text is watermrk, set it to empty and remove the watermark class
            $('input[id$=txtVendorName]').focus(function () {
                if ($('input[id$=txtVendorName]').val() == watermark1) {
                    $('input[id$=txtVendorName]').val('').removeClass('watermark');
                }
            });

            var watermark4 = 'Enter Name';
            var txt = $('input[id$=txtContactPerson]').val();
            //init, set watermark text and class
            $('input[id$=txtContactPerson]').val(watermark4).addClass('watermark');
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

        })
    </script>
</asp:Content>
