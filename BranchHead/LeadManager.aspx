<%@ Page Title="" Language="VB" MasterPageFile="~/Apex.master" AutoEventWireup="false" ValidateRequest="false" CodeFile="LeadManager.aspx.vb" Inherits="LeadManager" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="dist/css/bootstrap-datepicker3.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Lead Manager
           
        </h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">Lead Manager</li>
        </ol>
    </section>

    <!-- Main content -->
    <section class="content">
        <div class="row">
            <div class="col-xs-12">
                <!-- .box -->
                <div class="box">
                    <div class="box-header">
                        <h3 class="box-title">Lead Manager</h3>
                        
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                        <div id="divError" runat="server" class="alert alert-danger">
                            <p>
                                <strong>Warning: </strong>
                                <asp:Label ID="lblError" runat="server"></asp:Label>
                            </p>
                        </div>
                        <div id="divContent" runat="server">
                            <div class="inner-Content-container">
                                <div class="InnerContentData">
                                    <div class="col-lg-12">
                                        <div class="form-group col-lg-12">
                                            <span class="col-lg-3">Client*</span>
                                            <span class="col-lg-4">
                                                <asp:Label ID="lblClient" runat="server"></asp:Label>
                                                <asp:DropDownList ID="ddlClient" runat="server" TabIndex="1" CssClass="form-control" onchange="BindContactPerson(this.value)"></asp:DropDownList>
                                                <%--<select id="ddlClient" class="form-control" runat="server" onchange="BindContactPerson(this.value)">
                                                            </select>--%>
                            
                                            </span>
                                            <span onclick="pullCModel()"><a href="#">+ Add New</a></span>
                                            <span class="col-lg-5">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" SetFocusOnError="true" runat="server" ControlToValidate="ddlClient" ErrorMessage="*" InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" SetFocusOnError="true" runat="server" ControlToValidate="ddlClient" ErrorMessage="*" InitialValue="99999" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </span>
                                            <%--<span onclick="pullCModel()">Add New</span>--%>
                                        </div>

                                        <div class="form-group col-lg-12">
                                            <span class="col-lg-3">Client Contact Person</span>
                                            <span class="col-lg-4">
                                                <asp:Label ID="lblContactPerson" runat="server"></asp:Label>
                                                <asp:DropDownList ID="ddlContactPerson" CssClass="form-control" runat="server" TabIndex="2" onchange="BindContactLabels(this.value)"></asp:DropDownList>
                                            </span>
                                            <span id="contactp" onclick="pullCCModel()"><a href="#">+ Add New</a></span>
                                            <span class="col-5">
                                                <asp:Label ID="lblCEmail" runat="server"></asp:Label>
                                                &nbsp;&nbsp; 
                            <asp:Label ID="lblCContact" runat="server"></asp:Label>
                                            </span>
                                        </div>

                                        <div class="form-group col-lg-12">
                                            <span class="col-lg-3">Lead Name*</span>
                                            <span class="col-lg-4">
                                                <asp:Label ID="lblLeadName" runat="server"></asp:Label>
                                                <asp:TextBox ID="txtLeadName" runat="server" CssClass="form-control" placeholder="Lead Name" MaxLength="50" TabIndex="3"></asp:TextBox>

                                            </span>
                                            <span class="col-lg-5">
                                                <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtLeadName" ErrorMessage="*" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </span>
                                        </div>
                                        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <div class="form-group col-lg-12">
                                                    <span class="col-lg-3">Primary Activity</span>
                                                    <span class="col-lg-4">
                                                        <asp:Label ID="lblActivity" runat="server"></asp:Label>

                                                        <asp:DropDownList ID="ddlActivity" runat="server" AutoPostBack="true" CssClass="form-control" TabIndex="4"></asp:DropDownList>
                                                    </span>
                                                </div>
                                                <div class="form-group col-lg-12">
                                                    <span class="col-lg-3">Sub Activity</span>
                                                    <span class="col-lg-4">
                                                        <asp:Label ID="lblActivityType" runat="server"></asp:Label>
                                                        <asp:CheckBoxList ID="chklActivityType" runat="server" RepeatDirection="Horizontal" TabIndex="5"></asp:CheckBoxList>
                                                    </span>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                        <div class="form-group col-lg-12">
                                            <span class="col-lg-3">Lead Brief</span>
                                            <span class="col-lg-4">
                                                <asp:Label ID="lblLeadBrief" runat="server"></asp:Label>
                                                <asp:TextBox ID="txtLeadBrief" runat="server" TextMode="MultiLine" TabIndex="6" CssClass="form-control" placeholder="Lead Brief"></asp:TextBox>
                                            </span>
                                        </div>
                                        <div class="form-group col-lg-12">
                                            <span class="col-lg-3">Budget</span>
                                            <span class="col-lg-4">
                                                <asp:Label ID="lblBudget" runat="server"></asp:Label>
                                                <asp:TextBox ID="txtBudget" runat="server" CssClass="form-control" MaxLength="20" TabIndex="7" placeholder="Budget"></asp:TextBox>
                                            </span>
                                            <span class="col-5">
                                                <asp:RangeValidator ID="RangeValidator2" SetFocusOnError="true" runat="server" ControlToValidate="txtBudget" Display="Dynamic" ErrorMessage="Invalid Budget" ForeColor="Red" MaximumValue="999999999999999999999" MinimumValue="0" Type="Double"></asp:RangeValidator>
                                            </span>
                                        </div>
                                        <div class="form-group col-lg-12">
                                            <span class="col-lg-3">Execution Man days</span>
                                            <span class="col-lg-4">
                                                <asp:Label ID="lblExecution" runat="server"></asp:Label>
                                                <asp:TextBox ID="txtExecution" runat="server" CssClass="form-control" MaxLength="3" TabIndex="8" placeholder="Execution Man Days"></asp:TextBox>
                                            </span>
                                            <span class="col-5">
                                                <asp:RegularExpressionValidator SetFocusOnError="true" ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtExecution" Display="Dynamic" ErrorMessage="Invalid Man Days" ForeColor="Red" ValidationExpression="\d{1,4}"></asp:RegularExpressionValidator>
                                            </span>
                                        </div>
                                        <div class="form-group col-lg-12">
                                            <span class="col-lg-3">Lead Closure Date</span>
                                            <span class="col-lg-4">
                                                <asp:Label ID="lblClosureDateTime" runat="server"></asp:Label>
                                                <asp:TextBox ID="txtClosureDateTime" runat="server" onkeypress="return false" onkeyup="return false" onkeydown="return false" CssClass="form-control" TabIndex="9"></asp:TextBox>
                                            </span>
                                            <span class="col-5">
                                                <asp:CheckBox ID="chkDateRequired" runat="server" Checked="true" Text="No Closure DateTime yet decided" TabIndex="10" onchange="DisableDate()" /></span>
                                        </div>
                                        <div class="form-group col-lg-12">
                                            <span class="col-lg-3">Closure Probability (%)</span>
                                            <span class="col-lg-4">
                                                <asp:Label ID="lblClosureProbability" runat="server"></asp:Label>
                                                <asp:TextBox ID="txtClosureProbability" runat="server" CssClass="form-control" MaxLength="6" TabIndex="11" placeholder="Closure Probability"></asp:TextBox>
                                            </span>
                                            <span class="col-5">
                                                <asp:RangeValidator ID="RangeValidator1" SetFocusOnError="true" runat="server" ControlToValidate="txtClosureProbability" Display="Dynamic" ErrorMessage="Between 0 to 100" ForeColor="Red" MaximumValue="100" MinimumValue="0" Type="Double"></asp:RangeValidator>
                                            </span>
                                        </div>
                                        <div class="form-group col-lg-12">
                                            <span class="col-lg-3">Lead Type</span>
                                            <span class="col-lg-4">
                                                <asp:Label ID="lblLeadStatus" runat="server"></asp:Label>
                                                <asp:DropDownList ID="ddlLeadStatus" runat="server" CssClass="form-control" TabIndex="12"></asp:DropDownList>
                                            </span>
                                        </div>
                                        <div class="form-group col-lg-12">
                                            <span class="col-lg-3">Kestone Contact</span>
                                            <span class="col-lg-4">
                                                <asp:Label ID="lblContact" runat="server"></asp:Label>
                                            </span>
                                        </div>
                                        <div class="form-group col-lg-12">
                                            <span class="col-lg-3">Remarks</span>
                                            <span class="col-lg-4">
                                                <asp:Label ID="lblRemarks" runat="server"></asp:Label>
                                                <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" CssClass="form-control" TabIndex="13" placeholder="Remarks"></asp:TextBox></span>
                                        </div>
                                        <div class="form-group col-9">
                                            <center>
                        <asp:Button ID="btnCancel" runat="server" Text="
                            " Visible="false" CausesValidation="false" CssClass="btn btn-primary" TabIndex="30" />
                        <asp:Button ID="btnCancelHome" runat="server" Text="Back" CausesValidation="false" CssClass="btn btn-primary" TabIndex="30" />
                        <asp:Button ID="btnAdd" runat="server" Text="Save" CssClass="btn btn-primary" TabIndex="14" />
                        <asp:Button ID="btnEdit" runat="server" Text="Save" CssClass="btn btn-primary" TabIndex="14" />

                        <asp:Button ID="btnBrief" runat="server" Text="Convert to Brief" CausesValidation="false" CssClass="btn btn-primary" TabIndex="34" />
                            </center>
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

    <asp:HiddenField ID="hdnMode" runat="server" />
    <asp:HiddenField ID="hdnLeadsID" runat="server" />
    <asp:HiddenField ID="hdnContactPersonID" runat="server" />
    <asp:HiddenField ID="hdnclientID" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpFooter" runat="Server">
    <script src="dist/js/bootstrap-datepicker.js"></script>
    <script type="text/javascript" src="dist/js/tiny_mce/tiny_mce.js"></script>

    <input type="hidden" id="clientid" runat="server" />
    <!-- Modal -->
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title">Add new client contact person</h4>

                </div>
                <div class="modal-body">


                    <table class="table table-bordered">
                        <tr>
                            <th>Select Client*</th>
                            <th>
                                <select id="cbo_client" class="form-control" disabled="disabled">
                                </select>

                            </th>

                        </tr>
                        <tr>
                            <th>Name*</th>
                            <th>
                                <input id="txtContactPerson" maxlength="100" class="form-control" />

                            </th>

                        </tr>
                        <tr>
                            <th>Official Email*</th>
                            <th>
                                <input id="txtContactOfficialEmail" onblur="javascript:CheckEmail(this)" maxlength="100" class="form-control" />

                            </th>
                        </tr>
                        <tr>
                            <th>Contact*</th>
                            <th>
                                <input id="txtContactMobile1" onkeyup="javascript:checknum(this)" maxlength="11" class="form-control" />

                            </th>
                        </tr>
                    </table>

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>

                    <a id="modalbutton" class="btn btn-primary" href="javascript:saveclientcontact();">Save</a>


                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <div class="modal fade" id="clientModel" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title">Add new client details</h4>

                </div>
                <div class="modal-body">


                    <table class="table table-bordered">

                        <tr>
                            <th>Client Name*</th>
                            <th>
                                <input id="txtClientname" maxlength="100" class="form-control" />

                            </th>

                        </tr>
                        <tr>
                            <th>Industry</th>
                            <th>
                                <input id="txtIndustry" maxlength="50" class="form-control" />


                            </th>
                        </tr>
                        <tr>
                            <th>Anual Turnover</th>
                            <th>
                                <input id="txtAnnualTurnover" maxlength="13" class="form-control" />
                                <%--onkeyup="javascript:checknum(this)"--%>


                            </th>
                        </tr>
                    </table>




                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    <a id="A1" class="btn btn-primary" href="javascript:saveclient();">Save</a>


                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <!-- /.modal -->

    <script type="text/javascript">
        tinyMCE.init({
            mode: "textareas",
            theme: "simple",
            theme: 'advanced',
            theme_advanced_buttons3_add: 'spellchecker',
            plugins: 'spellchecker',
            spellchecker_languages: '+English=en',
            theme_advanced_path: false,
        });


        $(function () {
            $('input[id$=txtClosureDateTime]').datepicker({ format: 'dd-mm-yyyy' });

        });

        function BindContactLabels(cpid) {
            if (cpid == "Add new") {

                pullCCModel();
            }
            else {
                if (cpid > 0) {
                    $('#<%= lblCEmail.ClientID%>').html("");
                    $('#<%= lblCContact.ClientID%>').html("");

                    $.ajax({
                        url: "AjaxCalls/AX_Client.aspx?call=2&id=" + cpid,
                        context: document.body,
                        success: function (Result) {
                            if (Result != "") {
                                var JSONworkTypes = JSON.parse(Result);
                                $('#<%= lblCEmail.ClientID%>').html(JSONworkTypes.Table[0].ContactOfficialEmailID.toString());
                                $('#<%= lblCContact.ClientID%>').html(JSONworkTypes.Table[0].Mobile1.toString());
                                $('#<%= hdnContactPersonID.ClientID()%>').val(cpid);


                            }
                        }
                    });

                }
                else {
                    $('#<%= lblCEmail.ClientID%>').html("");
                    $('#<%= lblCContact.ClientID%>').html("");
                }
            }

        }



        function BindContactPerson(cid) {

            if (cid > 0) {
                $("#contactp").show();
            }
            else {
                $("#contactp").hide();
            }

            $('#<%= hdnclientID.ClientID()%>').val(cid);
            $('span[id$=lblCEmail]').html("");
            $('span[id$=lblCContact]').html("");

            $("select[id$=ddlContactPerson]").empty()
            $("select[id$=ddlContactPerson]").append($("<option></option>")
             .val("0")
               .text("Select"));

            if (cid > 0) {
                $("input[id$=clientid]").val(cid);
                if (cid == 9999999) {

                    pullCModel();
                }
                else {

                    $.ajax({
                        url: "AjaxCalls/AX_Client.aspx?call=1&id=" + cid,
                        context: document.body,
                        success: function (Result) {

                            if (Result != "") {

                                //$('#<%= hdnclientID.ClientID()%>').val(cid);
                                var JSONworkTypes = JSON.parse(Result);
                                $("select[id$=ddlContactPerson]").empty();
                                //add=1
                                //if (add != 1) {
                                $("select[id$=ddlContactPerson]").append($("<option></option>").val("0").text("Select"));
                                //}
                                for (var i = 0; i < JSONworkTypes.Table.length; i++) {

                                    $("select[id$=ddlContactPerson]").append($("<option></option>").val(JSONworkTypes.Table[i].ContactID.toString()).text(JSONworkTypes.Table[i].ContactName.toString()));
                                    //if (JSONworkTypes.Table[i].ContactName.toString() == name) {

                                    //    $('select[id$=ddlContactPerson] option:last-child').attr('selected', 'selected');
                                    //}
                                }

                                //$("select[id$=ddlContactPerson]").append($("<option></option>").val("Add new").text("Add new"));
                                // $('#ddlContactPerson').val('2');

                                //if (add == 1) {
                                //    $('select[id$=ddlContactPerson] option:last-child').attr('selected', 'selected');
                                //    var cid = $("select[id$='ddlContactPerson'] option:selected").val();
                                //  BindContactLabels(cid);
                                //}
                            }
                            //$("select[id$=ddlContactPerson]").append($("<option></option>").val("Add new").text("Add new"));

                        }
                    });
                }
            }
            else {


            }
        }

        function BindContactPerson_aftersave(cid, name) {

            if (cid > 0) {
                $("#contactp").show();
            }
            else {
                $("#contactp").hide();
            }

            $('#<%= hdnclientID.ClientID()%>').val(cid);
            $('span[id$=lblCEmail]').html("");
            $('span[id$=lblCContact]').html("");

            $("select[id$=ddlContactPerson]").empty()
            $("select[id$=ddlContactPerson]").append($("<option></option>")
             .val("0")
               .text("Select"));

            if (cid > 0) {
                $("input[id$=clientid]").val(cid);
                if (cid == 9999999) {

                    pullCModel();
                }
                else {

                    $.ajax({
                        url: "AjaxCalls/AX_Client.aspx?call=1&id=" + cid,
                        context: document.body,
                        success: function (Result) {

                            if (Result != "") {

                                //$('#<%= hdnclientID.ClientID()%>').val(cid);
                                var JSONworkTypes = JSON.parse(Result);
                                $("select[id$=ddlContactPerson]").empty();
                                //add=1
                                //  if (add != 1) {
                                $("select[id$=ddlContactPerson]").append($("<option></option>").val("0").text("Select"));
                                //}
                                for (var i = 0; i < JSONworkTypes.Table.length; i++) {

                                    $("select[id$=ddlContactPerson]").append($("<option></option>").val(JSONworkTypes.Table[i].ContactID.toString()).text(JSONworkTypes.Table[i].ContactName.toString()));
                                    if (JSONworkTypes.Table[i].ContactName.toString() == name) {

                                        $('select[id$=ddlContactPerson] option:last-child').attr('selected', 'selected');
                                        BindContactLabels(JSONworkTypes.Table[i].ContactID.toString());
                                    }
                                }




                            }


                        }
                    });
                }
            }
            else {


            }
        }


        function DisableDate() {
            if ($('#<%= chkDateRequired.ClientID%>').is(':checked')) {
                $('#<%= txtClosureDateTime.ClientID%>').attr("disabled", "disabled");
            }
            else {
                $('#<%= txtClosureDateTime.ClientID%>').removeAttr("disabled");
            }
        }

    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#contactp").hide();
            getclientdata("");
            bindclient();

            $("select[id$=ddlActivity]").change(function (event) {

                selectsubActivity(this.value);
            });
        });
        function pullCModel() {

            $('#clientModel').modal({
                keyboard: false
            })
        }
        function pullCCModel(id) {
            $('#myModal').modal({
                keyboard: false
            })
            var cid = $("input[id$=clientid]").val();

            $('select[id$=cbo_client] option[value=0]').attr('selected', 'selected');
            //alert(cid);
            $("select[id$=cbo_client] option[value='" + cid + "']").attr("Selected", "Selected");
        }

        function bindclient() {
            var JSONworkTypes = "";
            $.ajax({
                url: "AjaxCalls/AX_leads.aspx?call=4",
                context: document.body,
                success: function (Result) {
                    JSONworkTypes = JSON.parse(Result);
                    var myselect = document.getElementById('cbo_client');
                    myselect.options[myselect.length] = new Option("Select", "0")
                    for (var i = 0; i < JSONworkTypes.Table.length; i++) {
                        myselect.options[myselect.length] = new Option(JSONworkTypes.Table[i].Client.toString(), JSONworkTypes.Table[i].ClientID.toString())

                    }
                }
            });

        }

        function saveclientcontact() {
            var contactper = $("input[id$=txtContactPerson]").val();
            var email = $("input[id$=txtContactOfficialEmail]").val();
            var mobile = $("input[id$=txtContactMobile1]").val();
            var cid = $("select[id$='cbo_client'] option:selected").val();
            if (contactper.length == 0) {
                setTimeout("ShowMsg('Please enter contact person name')", 1)
                $("input[id$=txtContactPerson]").focus();
            }
            else
                if (email.length == 0) {
                    setTimeout("ShowMsg('Please enter email')", 1)
                    $("input[id$=txtContactOfficialEmail]").focus();
                }
                else if (mobile.length == 0) {
                    setTimeout("ShowMsg('Please enter mobile number')", 1)
                    $("input[id$=txtContactMobile1]").focus();
                }
                    //else if (cid.length)
                    //{ setTimeout("ShowMsg('Please select client')", 1) }
                else {

                    if (cid > 0) {
                        $.ajax({
                            url: "AjaxCalls/AX_leads.aspx?call=6&cid=" + cid + "&name=" + contactper + "&email=" + email + "&mob=" + mobile,
                            context: document.body,
                            success: function (Result) {
                                // alert(Result);
                                if (Result == "True") {

                                    //BindContactPerson(cid, contactper);
                                    //alert(cid);
                                    $('#myModal').modal('hide');
                                    BindContactPerson_aftersave(cid, contactper);
                                    $("input[id$=txtContactPerson]").val("");
                                    $("input[id$=txtContactOfficialEmail]").val("");
                                    $("input[id$=txtContactMobile1]").val("");
                                    alert("Save");
                                    //BindContactLabels(cid);

                                }
                                else {
                                    setTimeout("ShowMsg('" + Result.toString() + "')", 2)
                                    $("input[id$=txtContactOfficialEmail]").focus();
                                }


                            }
                        });
                    }
                }
        }
        function saveclient() {
            var name = $("input[id$=txtClientname]").val();
            var industry = $("input[id$=txtIndustry]").val();
            var turnover = $("input[id$=txtAnnualTurnover]").val();
            if (name.length == 0) {
                setTimeout("ShowMsg('Please enter name')", 1)
                $("input[id$=txtClientname]").focus();
            }
            else if (industry.length == 0) {
                setTimeout("ShowMsg('Please enter industry')", 1)
                $("input[id$=txtIndustry]").focus();
            }
            else if (turnover.length == 0) {
                setTimeout("ShowMsg('Please enter turnover')", 1)
                $("input[id$=txtAnnualTurnover]").focus();
            }
            else {
                $.ajax({
                    url: "AjaxCalls/AX_leads.aspx?call=5&name=" + name + "&industry=" + industry + "&turnover=" + turnover,
                    context: document.body,
                    success: function (Result) {
                        // alert(Result);
                        if (Result == "True") {

                            $('#clientModel').modal('hide');
                            $("input[id$=txtClientname]").val("");
                            $("input[id$=txtIndustry]").val("");
                            $("input[id$=txtAnnualTurnover]").val("");
                            alert("Save");
                            $('#ddlClient').html("");
                            getclientdata(name)
                            $("#contactp").show();
                            // alert($("input[id$=txtClientname]").val());

                        }
                        else {
                            //alert(Result);
                            setTimeout("ShowMsg('" + Result.toString() + "')", 1)
                            $("input[id$=txtClientname]").focus();
                        }


                    }
                });

            }
        }

        function getclientdata(name) {
            //$('#ddlClient1').empty();
           // alert(1);
            $.ajax({
                url: "AjaxCalls/AX_leads.aspx?call=4", cache: false,
                context: document.body,
                success: function (Result) {
                    JSONworkTypes = JSON.parse(Result);
                    //var myselect = document.getElementById('ddlClient');
                    //myselect.options[myselect.length] = new Option("Select", "0")
                    $('#ddlClient').html("");
                    $("select[id$=ddlClient]").append($("<option></option>").val("0").text("Select"));

                    for (var i = 0; i < JSONworkTypes.Table.length; i++) {
                        //myselect.options[myselect.length] = new Option(JSONworkTypes.Table[i].Client.toString(), JSONworkTypes.Table[i].ClientID.toString())

                        $("select[id$=ddlClient]").append($("<option></option>").val(JSONworkTypes.Table[i].ClientID.toString()).text(JSONworkTypes.Table[i].Client.toString()));
                        if (JSONworkTypes.Table[i].Client.toString() == name) {
                            $('select[id$=ddlClient] option:last-child').attr('selected', 'selected');
                        }
                    }
                  
                    var cid = $("select[id$='ddlClient'] option:selected").val();
                    // alert(cid);
                    $("input[id$=clientid]").val(cid);
                    //$("select[id$=ddlClient]").append($("<option></option>").val("9999999").text("Non Existing (New Client)"));

                    bindclient()

                    //alert(name);
                }
            });

        }
        function selectsubActivity(Activity) {

            $(':input[type="checkbox"]').each(function () {
                if ($(this).val() == Activity) {

                    $(this).attr("checked", "checked");
                }

            });

        }
    </script>
</asp:Content>
