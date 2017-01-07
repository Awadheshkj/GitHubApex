<%@ Page Title="" Language="VB" MasterPageFile="~/Apex.master" AutoEventWireup="false" CodeFile="BriefManager.aspx.vb" Inherits="BriefManager" ValidateRequest="false" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="dist/css/bootstrap-datepicker3.css" rel="stylesheet" />
    <style>
        .chkChoice td input {
            margin-left: -20px;
        }

        .chkChoice td {
            padding-left: 20px;
        }

            .chkChoice td label {
                padding-right: 10px;
                padding-left: 5px;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Brief Manager
        </h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="leads.aspx"><i class="fa fa-th"></i>&nbsp;Lead</a></li>
            <li class="active">Brief Manager</li>
        </ol>
    </section>

    <!-- Main content -->
    <section class="content">
        <div class="row">
            <div class="col-lg-xs-12">
                <div class="box box-primary">
                    <div class="box-header">
                        <h3><%--Brief--%></h3>
                    </div>
                    <div class="box-body">
                        <div id="divError" runat="server" class="ui-state-error ui-corner-all divError">
                            <p>
                                <span class="ui-icon ui-icon-alert" style="float: left; margin-right: .3em;"></span>
                                <strong>Warning: </strong>
                                <asp:Label ID="lblError" runat="server"></asp:Label>
                            </p>
                        </div>
                        <div id="divContent" runat="server">
                            <div class="inner-Content-container">
                                <div class="InnerContentData">
                                    <div class="col-lg-12">
                                        <div class="form-group col-lg-12">
                                            <span class="col-lg-3">Brief Name*</span>
                                            <span class="col-lg-6">
                                                <asp:Label ID="lblBrief" runat="server"></asp:Label>
                                                <asp:TextBox ID="txtBrief" runat="server" MaxLength="50" TabIndex="1" CssClass="form-control" placeholder="Brief Name"></asp:TextBox>
                                            </span>
                                            <span class="col-lg-5">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" SetFocusOnError="true" runat="server" ControlToValidate="txtBrief" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </span>
                                        </div>
                                        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <div class="form-group col-lg-12">
                                                    <span class="col-lg-3">Primary Activity*</span>
                                                    <span class="col-lg-6">
                                                        <asp:Label ID="lblActivity" runat="server"></asp:Label>
                                                        <asp:DropDownList ID="ddlActivity" runat="server" CssClass="form-control" AutoPostBack="true" placeholder="Primery Activity" TabIndex="2"></asp:DropDownList>
                                                    </span>
                                                    <span class="col-lg-5">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" SetFocusOnError="true" runat="server" ControlToValidate="ddlActivity" ErrorMessage="*" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                                    </span>
                                                </div>
                                                <div class="form-group col-lg-12">
                                                    <span class="col-lg-3">Sub Activity</span>
                                                    <span class="col-lg-6">
                                                        <asp:Label ID="lblchklActivityType" runat="server"></asp:Label>
                                                        <asp:CheckBoxList ID="chklActivityType" CssClass="chkChoice" runat="server" RepeatDirection="Horizontal" TabIndex="3"></asp:CheckBoxList>
                                                    </span>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <div class="form-group col-lg-12">
                                            <span class="col-lg-3">Activity Date*</span>
                                            <span class="col-lg-6">
                                                <asp:Label ID="lblActivityDate" runat="server"></asp:Label>
                                                <asp:TextBox ID="txtActivityDate" runat="server" onkeypress="return false" onkeyup="return false" onkeydown="return false" CssClass="form-control" TabIndex="4"></asp:TextBox>
                                            </span>
                                            <span class="col-lg-5">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" SetFocusOnError="true" runat="server" ControlToValidate="txtActivityDate" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </span>
                                        </div>
                                        <div class="form-group col-lg-12">
                                            <span class="col-lg-3">Client*</span>
                                            <span class="col-lg-6">
                                                <asp:Label ID="lblClient" runat="server"></asp:Label>
                                                <asp:DropDownList ID="ddlClient" runat="server" CssClass="form-control" placeholder="Client" TabIndex="5" onchange="BindContactPerson(this.value)"></asp:DropDownList>
                                            </span>
                                            <span class="col-lg-5">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" SetFocusOnError="true" runat="server" ControlToValidate="ddlClient" ErrorMessage="*" InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </span>
                                        </div>

                                        <div class="form-group col-lg-12">
                                            <span class="col-lg-3">Client Contact Person*</span>
                                            <span class="col-lg-6">
                                                <asp:Label ID="lblContactPerson" runat="server"></asp:Label>
                                                <asp:DropDownList ID="ddlContactPerson" runat="server" CssClass="form-control" TabIndex="6" onchange="BindContactLabels(this.value)"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator23" SetFocusOnError="true" runat="server" ControlToValidate="ddlContactPerson" ErrorMessage="*" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                            </span>
                                            <%--<asp:Button ID="btnReset" runat="server" Text="Reset" CausesValidation="false" CssClass="small-button" TabIndex="25"/>--%>
                                            <span class="col-lg-5">
                                                <asp:Label ID="lblCEmail" runat="server"></asp:Label>
                                                &nbsp;&nbsp;
                            <asp:Label ID="lblCContact" runat="server"></asp:Label>
                                            </span>
                                        </div>

                                        <div class="form-group col-lg-12">
                                            <span class="col-lg-3">Scope of work*</span>
                                            <span class="col-lg-6">
                                                <asp:Label ID="lblScope" runat="server"></asp:Label>
                                                <asp:TextBox ID="txtScopeOfWork" runat="server" CssClass="form-control" TextMode="MultiLine" TabIndex="7"></asp:TextBox>
                                            </span>
                                            <span class="col-lg-5">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" SetFocusOnError="true" runat="server" ControlToValidate="txtScopeOfWork" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </span>
                                        </div>
                                        <div class="form-group col-lg-12" style="display:none;">
                                            <span class="col-lg-3">Target Audience*</span>
                                            <span class="col-lg-6">
                                                <asp:Label ID="lblTargetAudience" runat="server"></asp:Label>
                                                <asp:TextBox ID="txtTargetAudience" runat="server" CssClass="form-control" TextMode="MultiLine" MaxLength="400" TabIndex="8"></asp:TextBox>
                                            </span>
                                            <span>
                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator16" SetFocusOnError="true" runat="server" ControlToValidate="txtTargetAudience" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                            </span>
                                        </div>
                                        <div class="form-group col-lg-12">
                                            <span class="col-lg-3">Measurement Matrix*</span>
                                            <span class="col-lg-6">
                                                <asp:Label ID="lblMeasurementMatrix" runat="server"></asp:Label>
                                                <asp:TextBox ID="txtMeasurementMatrix" runat="server" CssClass="form-control" TextMode="MultiLine" MaxLength="400" TabIndex="9"></asp:TextBox>
                                            </span>
                                            <span>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" SetFocusOnError="true" runat="server" ControlToValidate="txtMeasurementMatrix" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </span>
                                        </div>
                                        <div class="form-group col-lg-12" style="display:none;">
                                            <span class="col-lg-3">Activity Details*</span>
                                            <span class="col-lg-6">
                                                <asp:Label ID="lblActivityDetails" runat="server"></asp:Label>
                                                <asp:TextBox ID="txtActivityDetails" runat="server" CssClass="form-control" TextMode="MultiLine" MaxLength="400" TabIndex="10"></asp:TextBox>
                                            </span>
                                            <span>
                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator18" SetFocusOnError="true" runat="server" ControlToValidate="txtActivityDetails" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                            </span>
                                        </div>
                                        <div class="form-group col-lg-12" style="display:none;">
                                            <span class="col-lg-3">Key Challenges for execution*</span>
                                            <span class="col-lg-6">
                                                <asp:Label ID="lblKeyChallenges" runat="server"></asp:Label>
                                                <asp:TextBox ID="txtKeyChallenges" runat="server" CssClass="form-control" TextMode="MultiLine" MaxLength="400" TabIndex="11"></asp:TextBox>
                                            </span>
                                            <span>
                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator19" SetFocusOnError="true" runat="server" ControlToValidate="txtKeyChallenges" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                            </span>
                                        </div>
                                        <div class="form-group col-lg-12" style="display:none;">
                                            <span class="col-lg-3">Timeline for revert*</span>
                                            <span class="col-lg-6">
                                                <asp:Label ID="lblTimeline" runat="server"></asp:Label>
                                                <asp:TextBox ID="txtTimeline" runat="server" CssClass="form-control" TextMode="MultiLine" MaxLength="400" TabIndex="12"></asp:TextBox>
                                            </span>
                                            <span>
                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator20" SetFocusOnError="true" runat="server" ControlToValidate="txtTimeline" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                            </span>
                                        </div>
                                        <div class="form-group col-lg-12">
                                            <span class="col-lg-3">Cost*</span>
                                            <span class="col-lg-6">
                                                <asp:Label ID="lblBudget" runat="server"></asp:Label>
                                                <asp:TextBox ID="txtBudget" onkeyup="javascript:checknum(this)" runat="server" CssClass="form-control" MaxLength="10" TabIndex="13"></asp:TextBox>
                                                <span class="col-lg-5">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator21" SetFocusOnError="true" runat="server" Display="Dynamic" ControlToValidate="txtBudget" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    <asp:RangeValidator ID="RangeValidator2" SetFocusOnError="true" runat="server" ControlToValidate="txtBudget" Display="Dynamic" ErrorMessage="Invalid Budget" ForeColor="Red" MaximumValue="99999" MinimumValue="0"></asp:RangeValidator>
                                                </span>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField runat="server" ID="hdnGrand" />
                        <asp:HiddenField runat="server" ID="hdnSubTotal" />
                        <asp:HiddenField runat="server" ID="hdnTotal" />
                        <asp:HiddenField ID="hdnBrief" runat="server" />
                        <asp:HiddenField ID="hdnEstimate" runat="server" />
                        <asp:HiddenField ID="hdnJobCardID" runat="server" />
                        <asp:HiddenField ID="hdnBriefID" runat="server" />
                        <asp:HiddenField ID="hdnContactPersonID" runat="server" />
                        </span>
                    </div>
                    <div class="box-footer">
                        <div class="form-group col-lg-12 text-center">

                            <asp:Button ID="btnCancelHome" runat="server" Text="Cancel" CausesValidation="false" CssClass="btn btn-primary" TabIndex="15" />
                            <asp:Button ID="btnAdd" runat="server" Text="Save" CssClass="btn btn-primary" TabIndex="14" OnClientClick="tinyMCE.triggerSave(false,true);" />
                            <asp:Button ID="btnEdit" runat="server" Text="Save" CssClass="btn btn-primary" TabIndex="16" OnClientClick="tinyMCE.triggerSave(false,true);" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" CssClass="btn btn-primary" TabIndex="17" />
                            <%--<asp:Button ID="btnReset" runat="server" Text="Reset" CausesValidation="false" CssClass="small-button" TabIndex="25"/>--%>
                            <asp:Button ID="btnNext" runat="server" Text="View Collaterals" CssClass="btn btn-primary" TabIndex="18" />

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpFooter" runat="Server">
    <script src="dist/js/bootstrap-datepicker.js"></script>
    <script src="dist/js/tiny_mce/tiny_mce.js"></script>

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
            $('input[id$=txtActivityDate]').datepicker({ format: 'dd-mm-yyyy', autoclose: true });
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
                                var JSONworkTypes = JSON.parse(Result);
                                $("select[id$=ddlContactPerson]").empty();
                                $("select[id$=ddlContactPerson]").append($("<option></option>").val("0").text("Select"));
                                for (var i = 0; i < JSONworkTypes.Table.length; i++) {

                                    $("select[id$=ddlContactPerson]").append($("<option></option>").val(JSONworkTypes.Table[i].ContactID.toString()).text(JSONworkTypes.Table[i].ContactName.toString()));

                                }

                                $("select[id$=ddlContactPerson]").append($("<option></option>").val("Add new").text("Add new"));

                            }

                        }
                    });
                }
            }
            else {


            }
        }

    </script>

    <input type="hidden" id="clientid" />
    <!-- Modal -->
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title">Add new Client contact person</h4>

                </div>
                <div class="modal-body">


                    <table class="table table-bordered">
                        <tr>
                            <th>Select Client*</th>
                            <th>
                                <select id="cbo_client" class="form-control">
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
                                <input id="txtContactMobile1" onkeyup="javascript:checknum(this)" maxlength="10" class="form-control" />

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
                                <input id="txtAnnualTurnover" onkeyup="javascript:checknum(this)" maxlength="13" class="form-control" />


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


    <script>
        $(document).ready(function () {
            bindclient()
            $("select[id$=ddlActivity]").change(function (event) {

                // $(this.value).attr("checked", "checked");
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
            var cid = $("select[id$=ddlClient]").val();
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

        function selectsubActivity(Activity) {

            $(':input[type="checkbox"]').each(function () {
                if ($(this).val() == Activity) {
                    // alert(Activity);
                    $(this).attr("checked", "checked");
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
            else if (email.length == 0) {
                setTimeout("ShowMsg('Please enter email')", 1)
                $("input[id$=txtContactOfficialEmail]").focus();
            }
            else if (mobile.length == 0) {
                setTimeout("ShowMsg('Please enter mobile number')", 1)
                $("input[id$=txtContactMobile1]").focus();
            }
            else if (cid.length)
            { setTimeout("ShowMsg('Please select client')", 1) }
            else {
                if (cid > 0) {
                    $.ajax({
                        url: "AjaxCalls/AX_leads.aspx?call=6&cid=" + cid + "&name=" + contactper + "&email=" + email + "&mob=" + mobile,
                        context: document.body,
                        success: function (Result) {
                            // alert(Result);
                            if (Result == "True") {

                                $('#myModal').modal('hide');
                                $("input[id$=txtContactPerson]").val("");
                                $("input[id$=txtContactOfficialEmail]").val("");
                                $("input[id$=txtContactMobile1]").val("");
                                alert("Save");
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
                        }


                    }
                });
            }
        }
    </script>
</asp:Content>

