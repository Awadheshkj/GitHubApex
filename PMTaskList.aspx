<%@ Page Title="" Language="VB" MasterPageFile="~/Apex.master" AutoEventWireup="false" CodeFile="PMTaskList.aspx.vb" Inherits="PMTaskList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!-- DATA TABLES -->
    <link href="plugins/datatables/dataTables.bootstrap.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Content Header (Page header) -->
    <section class="content-header">

        <%--<h1>Notifications
        </h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">Notifications</li>
        </ol>--%>
    </section>

    <!-- Main content -->
    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <div class="box">
                    <div class="box-header">
                        <%-- <h3 class="box-title">List Of Task</h3>--%>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                        <div id="divapproval" runat="server">
                            <h3>This is financially inactive JC. Please reachout to the KAM </h3>
                            <asp:Button ID="btnverify" runat="server" Text="Click to send notification" CssClass="btn btn-sm btn-primary" />
                            <asp:HiddenField ID="hdnBriefID" runat="server" />
                            <asp:HiddenField ID="hdnJobCardID" runat="server" />
                        </div>
                        <div id="divgrid" runat="server" class="table-responsive">

                            <table class="table table-bordered table-striped" id="Docgrid">
                                <tbody>
                                    <tr>
                                        <td>
                                            <div class="overlay">
                                                <i class="fa fa-refresh fa-spin"></i>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Loading Data. Please wait... 
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <a href="javascript:void(0)" target="_blank" class="btn btn-primary" style="display: none" id="btnprintpreview" onclick="getcheckboxvalue()"><i class="fa fa-print"></i>&nbsp;Print Preview</a>
                            <a href="javascript:void(0)"  class="btn btn-primary" style="display: none" id="btnemployeeadvance" onclick="getclaimcheckboxvalue()"><i class="fa fa-print"></i>&nbsp;Employee Advance</a>
                            <a href="javascript:void(0)"  class="btn btn-primary" style="display: none" id="btnvendoradvance" onclick="getclaimcheckboxvalue()"><i class="fa fa-print"></i>&nbsp;Vendor Advance</a>
                        </div>
                    </div>
                </div>
                <!-- /.box-body -->
            </div>
        </div>
    </section>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cpFooter" runat="Server">
    <!-- DATA TABES SCRIPT -->
    <script src="plugins/datatables/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="plugins/datatables/dataTables.bootstrap.min.js" type="text/javascript"></script>
    <!-- page script -->
    <script type="text/javascript">
        function getcheckboxvalue() {

            var valuesArray = $('input[name="chekID"]:checked').map(function () {
                return this.value;
            }).get().join(",");

            if (valuesArray != "") {
                $("#btnprintpreview").show()
                $("#btnprintpreview").attr("href", "RembursementClaimApprovalView.aspx?jc=" + GetParameterValues('jc') + "&type=Task&nid=0&aid=" + valuesArray + "");
                //&type=Task&nid=0&aid=valuesArray''
            }
            else {
                $("#btnprintpreview").hide()
            }
            ;

            //    alert(valuesArray);
        }

        $(document).ready(function () {


            var jc = GetParameterValues('jc');
            if (jc == 0) {

                jc = GetParameterValues('jid');
            }

            // $('#heading').text(GetParameterValues('mode').replace("_", " "));

            if (GetParameterValues('mode') == "Task_List") {
                fillTabPM3Details(jc)
            }
            else if (GetParameterValues('mode') == "Manage_Team") {
                fillTabPM2Details(jc)
            }
            else if (GetParameterValues('mode') == "Claim_Detail") {
                fillTabPM3Details(jc)
            }
            else if (GetParameterValues('mode') == "Claims") {
                fillTabPM4Details(jc)
            }
                //Notification
            else if (GetParameterValues('mode') == "Notification") {
                fillTabKAM1Details()
            }


        });

        function fillTabPM3Details(jc) {

            var datatable = "";
            $.ajax({
                url: "AjaxCalls/PMDetails.aspx?call=7&jid=" + jc, cache: false,
                context: document.body,

                success: function (Result) {

                    if (!Result == "") {
                        var jsonstr = JSON.parse(Result);
                        //datatable = '<table class="table table-bordered table-hover">'
                        datatable += '<thead>'
                        datatable += '<tr>'
                        datatable += '<th>' + 'ID' + '</th>'
                        datatable += '<th>' + 'Job Card' + '</th>'
                        datatable += '<th>' + 'Title' + '</th>'
                        datatable += '<th width="100px"> ' + 'Start date' + '</th>'
                        datatable += '<th width="100px">' + 'End Date' + '</th>'
                        datatable += '<th>' + 'Category' + '</th>'
                        datatable += '<th>' + 'Description' + '</th>'
                        datatable += '<th>' + 'Total' + '</th>'
                        datatable += '<th>' + 'Manage Team' + '</th>'
                        datatable += '<th>' + 'Manage Checklist' + '</th>'

                        datatable += '<th>' + 'Job Status' + '</th>'
                        datatable += '<th width="160px">' + 'Get Claim' + '</th>'
                        datatable += '</tr>'
                        datatable += '</thead>'

                        datatable += '<tbody id="tblbody">'
                        for (var i = 0; i < jsonstr.Table.length; i++) {
                            var j = i + 1;
                            datatable += '<tr>'
                            datatable += '<td>' + j + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].jobcardNo.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].Title.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].startdate.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].enddate.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].Particulars.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].Quantity.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].Total.toString() + '</td>'
                            datatable += '<td>' + '<a href="teamSelect.aspx?taid=' + jsonstr.Table[i].AccountID.toString() + '&TL=Y">Manage</a></td>'
                            datatable += '<td>' + '<a href="Checklist.aspx?tid=' + jsonstr.Table[i].AccountID.toString() + '">Manage</a></td>'


                            datatable += '<td>' + jsonstr.Table[i].TaskCompleted.toString() + '</td>'
                            datatable += '<td><a href="RembursementClaim.aspx?taid=' + jsonstr.Table[i].AccountID.toString() + '">For Claim</a></td>'
                            //|<a href="RembursementClaimApproval.aspx?taid=' + jsonstr.Table[i].AccountID.toString() + '&view=P">View Claim</a>
                            datatable += '</tr>'


                        }
                        datatable += '</tbody>'
                        //datatable += '</table><div class="pagination"></div>'

                        //$('#gridDiv').html = datatable;
                        document.getElementById('Docgrid').innerHTML = datatable;
                        $('#Docgrid').dataTable();
                    }
                    else {
                        document.getElementById('Docgrid').innerHTML = "Data Not Found";
                    }
                }
            });
        }

        function fillTabPM2Details(jc) {
            var datatable = "";
            $.ajax({
                url: "AjaxCalls/PMDetails.aspx?call=6&jid=" + jc, cache: false,
                context: document.body,

                success: function (Result) {
                    if (!Result == "") {
                        var jsonstr = JSON.parse(Result);
                        //datatable = '<table class="table table-bordered table-hover">'
                        datatable += '<thead>'
                        datatable += '<tr>'
                        datatable += '<th>' + 'ID' + '</th>'
                        datatable += '<th>' + 'Title' + '</th>'
                        datatable += '<th>' + 'Job Code' + '</th>'
                        datatable += '<th>' + 'Project Manager' + '</th>'
                        datatable += '<th>' + 'Project category' + '</th>'
                        //datatable += '<th>' + 'Action' + '</th>'
                        datatable += '<th>' + 'Task Budget' + '</th>'
                        datatable += '<th>' + 'List Of Task' + '</th>'
                        datatable += '</tr>'
                        datatable += '</thead>'

                        datatable += '<tbody id="tblbody">'
                        for (var i = 0; i < jsonstr.Table.length; i++) {
                            var j = i + 1;
                            datatable += '<tr>'
                            datatable += '<td>' + j + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].JobCardName.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].JobCardNo.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].ProjectManager.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].ProjectType.toString() + '</td>'
                            if (jsonstr.Table[i].JobCompleted.toString() == "N") {
                                datatable += '<td><a href="ViewPrepnl.aspx?jid=' + jsonstr.Table[i].JobCardID.toString() + '"><span value="View" class="btn-small btn-success">View</span></a></td>'
                            }
                            else {
                                datatable += '<td><a href="ViewPrepnl.aspx?jid=' + jsonstr.Table[i].JobCardID.toString() + '"><span type="button" class="btn btn-danger btn-small" value="JobCompleted" >JobCompleted</span></a></td>'
                            }
                            //datatable += '<td><a href="ViewPrepnl.aspx?jid=' + jsonstr.Table[i].JobCardID.toString() + '">View</a></td>'
                            datatable += '<td><a href="ListOfTask.aspx?jid=' + jsonstr.Table[i].JobCardID.toString() + '">View</a></td>'
                            datatable += '</tr>'


                        }
                        datatable += '</tbody>'
                        //datatable += '</table><div class="pagination"></div>'

                        //$('#gridDiv').html = datatable;
                        document.getElementById('Docgrid').innerHTML = datatable;
                        $('#Docgrid').dataTable();

                    }
                    else {
                        document.getElementById('Docgrid').innerHTML = "Data Not Found";
                    }
                }
            });

        }

        function fillTabPM3Details(jc) {
            // alert('fillTabPM3Details : ' + jc);
            var datatable = "";
            $.ajax({
                url: "AjaxCalls/PMDetails.aspx?call=7&jid=" + jc, cache: false,
                context: document.body,

                success: function (Result) {

                    if (!Result == "") {
                        //alert(Result);
                        var jsonstr = JSON.parse(Result);

                        //datatable = '<table class="table table-bordered table-hover">'
                        datatable += '<thead>'
                        datatable += '<tr>'
                        datatable += '<th>' + '' + '</th>'
                        datatable += '<th>' + 'ID' + '</th>'
                        datatable += '<th>' + 'Job Card' + '</th>'
                        datatable += '<th>' + 'Task' + '</th>'
                        datatable += '<th>' + 'Sub Task' + '</th>'
                        datatable += '<th>' + 'Category' + '</th>'
                        datatable += '<th>' + 'Budget' + '</th>'
                        datatable += '<th>' + 'Claimed' + '</th>'
                        datatable += '<th>' + 'Employee Advance' + '</th>'
                        datatable += '<th>' + 'Vendor Advance' + '</th>'

                        datatable += '<th width="200px" style="text-align:center;">' + '' + '</th>'
                        //datatable += '<th width="80px" style="text-align:center;">' + '<input type="checkbox" class="header"/>' + '</th>'
                        datatable += '</tr>'
                        datatable += '</thead>'

                        datatable += '<tbody id="tblbody">'

                        for (var i = 0; i < jsonstr.Table.length; i++) {
                            var j = i + 1;
                            datatable += '<tr>'

                            if (jsonstr.Table[i].Isclaimedactive.toString() != "N") {
                                datatable += '<td></td>'
                            }
                            else {
                                if (jsonstr.Table[i].ISclaim.toString() != "N") {
                                    if (jsonstr.Table[i].Advanceamtrequest > 0 || jsonstr.Table[i].VAdvanceamtrequest > 0) {
                                        datatable += '<td></td>'

                                    }
                                    else { datatable += '<td><input type="checkbox" ID="chekclaimID" name="chekclaimID" onclick="getclaimcheckboxvalue()" value=' + jsonstr.Table[i].AccountID.toString() + ' /></td>' }
                                }
                                else {
                                    datatable += '<td></td>'
                                }

                                //datatable += '<td style="text-align:center;"><input type="checkbox" class="items"/></td>'
                            }

                            datatable += '<td>' + j + '</td>'

                            datatable += '<td>' + jsonstr.Table[i].jobcardNo.toString() + '</td>'

                            datatable += '<td>' + jsonstr.Table[i].Taskname.toString() + '</td>'

                            datatable += '<td>' + jsonstr.Table[i].Particulars.toString() + '</td>'

                            datatable += '<td>' + jsonstr.Table[i].category.toString() + '</td>'

                            datatable += '<td>' + jsonstr.Table[i].Total.toString() + '</td>'

                            datatable += '<td>' + jsonstr.Table[i].Claimed.toString() + '</td>'
                            if (jsonstr.Table[i].BranchHeadApproval == "Y") {

                                if (jsonstr.Table[i].FinaceApproval == "Y") {

                                    datatable += '<td><span class="btn btn-success btn-sm" data-toggle="tooltip" title="Approved"><a href="RembursementClaim.aspx?jc=' + jc + '&taid=' + jsonstr.Table[i].AccountID.toString() + '&Type=E">' + jsonstr.Table[i].Advanceamtrequest + '</a></span></td>'
                                }
                                else {
                                    datatable += '<td><span class="btn btn-warning btn-sm" data-toggle="tooltip" title="Wait Approval from FM">' + jsonstr.Table[i].Advanceamtrequest + '</span></td>'
                                }


                            }

                            else {
                                datatable += '<td><span data-toggle="tooltip" title="Wait Approval from BH">' + jsonstr.Table[i].Advanceamtrequest + '</span></td>'
                            }


                            if (jsonstr.Table[i].VBranchHeadApproval == "Y") {

                                if (jsonstr.Table[i].VFinaceApproval == "Y") {

                                    datatable += '<td><span class="btn btn-success btn-sm" data-toggle="tooltip" title="Approved"><a href="RembursementClaim.aspx?jc=' + jc + '&taid=' + jsonstr.Table[i].AccountID.toString() + '&Type=V">' + jsonstr.Table[i].VAdvanceamtrequest + '</a></span></td>'
                                }
                                else {
                                    datatable += '<td><span class="btn btn-warning btn-sm" data-toggle="tooltip" title="Wait Approval from FM">' + jsonstr.Table[i].VAdvanceamtrequest + '</span></td>'
                                }


                            }

                            else {
                                datatable += '<td><span data-toggle="tooltip" title="Wait Approval from BH">' + jsonstr.Table[i].VAdvanceamtrequest + '</span></td>'
                            }

                            //IsPostPnL
                            if (jsonstr.Table[i].Isclaimedactive.toString() != "N") {
                                datatable += '<td>Job Closed</td>'
                            }
                            else {
                                if (jsonstr.Table[i].ISclaim.toString() != "N") {
                                    //datatable += '<td><a href="RembursementClaim.aspx?taid=' + jsonstr.Table[i].AccountID.toString() + '">For Claim</a> | <a href="Employeeadvance.aspx?taid=' + jsonstr.Table[i].AccountID.toString() + '">Employee Advance</a> | <a href="Vendoradvance.aspx?taid=' + jsonstr.Table[i].AccountID.toString() + '">Vendor Advance</a></td>'
                                    datatable += '<td><a href="RembursementClaim.aspx?jc=' + jc + '&taid=' + jsonstr.Table[i].AccountID.toString() + '&Type=C">For Claim</a></td>'
                                }
                                else {
                                    datatable += '<td>' + jsonstr.Table[i].Name.toString() + '</td>'
                                }

                                //datatable += '<td style="text-align:center;"><input type="checkbox" class="items"/></td>'
                            }
                            datatable += '</tr>'

                        }
                        //datatable += '</tr><td colspan="6" style="text-align:right;"> <a href="#" class="btn btn-primary">Submit Claim</a></td>'
                        //datatable += '</tr>'
                        datatable += '</tbody>'
                        //datatable += '</table><div class="pagination"></div>'

                        //$('#gridDiv').html = datatable;
                        document.getElementById('Docgrid').innerHTML = datatable;
                        $('#Docgrid').dataTable({
                            "order": [[1, "asc"]],
                            stateSave: true
                        });

                    }
                    else {
                        document.getElementById('Docgrid').innerHTML = "Data Not Found";
                    }
                }
            });
        }

        function fillTabPM4Details(jc) {
            var datatable = "";
            $.ajax({
                url: "AjaxCalls/PMDetails.aspx?call=8&jid=" + jc, cache: false,
                context: document.body,

                success: function (Result) {
                    if (!Result == "") {
                        var jsonstr = JSON.parse(Result);
                        datatable = '<table class="table table-bordered table-hover">'
                        datatable += '<thead>'
                        datatable += '<tr>'
                        datatable += '<th>' + 'ID' + '</th>'
                        datatable += '<th>' + 'Job Card No' + '</th>'
                        datatable += '<th>' + 'Task Title' + '</th>'
                        datatable += '<th>' + 'Claimed Amount' + '</th>'
                        datatable += '<th>' + 'Status' + '</th>'
                        datatable += '<th>' + 'Claim Details' + '</th>'
                        datatable += '</tr>'
                        datatable += '</thead>'

                        datatable += '<tbody id="tblbody">'
                        for (var i = 0; i < jsonstr.Table.length; i++) {
                            var j = i + 1;
                            datatable += '<tr>'
                            datatable += '<td>' + j + '</td>'
                            datatable += '<td><a href="JobCardManager.aspx?jid=' + jsonstr.Table[i].JobCardID.toString() + '"><span class="btn-small btn-success" >"' + jsonstr.Table[i].JobCardNo.toString() + '" </span></a></td>'
                            datatable += '<td>' + jsonstr.Table[i].title.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].Amount.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].ClaimStatus.toString() + '</td>'
                            datatable += '<td>' + '<a href="RembursementClaimApprovalView.aspx?type=Task&nid=0&aid=' + jsonstr.Table[i].ClaimMasterID.toString() + '">View</a>' + '</td>'
                            datatable += '</tr>'
                        }
                        datatable += '</tbody>'
                        datatable += '</table><div class="pagination"></div>'

                        //$('#gridDiv').html = datatable;
                        document.getElementById('Docgrid').innerHTML = datatable;

                        $(".pagination").jPages({
                            containerID: "tblbody",
                            perPage: 10,
                            startPage: 1,
                            startRange: 1,
                            midRange: 10,
                            endRange: 1
                        });
                    }
                    else {
                        document.getElementById('Docgrid').innerHTML = "Data Not Found";
                    }
                }
            });
        }


        function fillTabPM4Details(jc) {
            var datatable = "";

            $.ajax({
                url: "AjaxCalls/PMDetails.aspx?call=8&jid=" + jc, cache: false,
                context: document.body,

                success: function (Result) {
                    if (!Result == "") {
                        //alert(Result);
                        var jsonstr = JSON.parse(Result);
                        //datatable = '<table class="table table-bordered table-hover">'
                        datatable += '<thead>'
                        datatable += '<tr>'
                        datatable += '<th>' + '' + '</th>'
                        datatable += '<th>' + 'ID' + '</th>'
                        //datatable += '<th>' + 'Task' + '</th>'
                        datatable += '<th>' + 'Task Details' + '</th>'
                        datatable += '<th>' + 'Start date' + '</th>'
                        datatable += '<th>' + 'End date' + '</th>'
                        datatable += '<th>' + 'Cost Center' + '</th>'
                        datatable += '<th>' + 'Assign From' + '</th>'
                        datatable += '<th>' + 'Assign To' + '</th>'
                        datatable += '<th>' + 'Assign Budget' + '</th>'
                        datatable += '<th>' + 'Requested Amt' + '</th>'
                        datatable += '<th>' + 'Claimed Amount' + '</th>'
                        datatable += '<th>' + 'Status' + '</th>'
                        datatable += '<th>' + 'Claim Details' + '</th>'
                        datatable += '</tr>'
                        datatable += '</thead>'
                        datatable += '<tbody id="tblbody">'
                        //alert(datatable);
                        for (var i = 0; i < jsonstr.Table.length; i++) {
                            var j = i + 1;
                            datatable += '<tr>'
                            if (jsonstr.Table[i].Amount.toString() != "0.00") {
                                if (jsonstr.Table[i].TL.toString() == 'Y' && jsonstr.Table[i].ClaimStatus.toString() == "Approved") {
                                    datatable += '<td><input type="checkbox" ID="chkChild" name="chekID" onclick="getcheckboxvalue()" value=' + jsonstr.Table[i].ClaimMasterID.toString() + ' /></td>'
                                }
                                else {
                                    datatable += '<td></td>'
                                }
                            }
                            else {
                                datatable += '<td></td>'
                            }
                            datatable += '<td>' + j + '</td>'

                            //datatable += '<td>' + jsonstr.Table[i].Title.toString() + '</td>'

                            datatable += '<td>' + jsonstr.Table[i].particulars.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].startdate.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].enddate.toString() + '</td>'

                            datatable += '<td>' + jsonstr.Table[i].categoryTask.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].AssignFrom.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].Name.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].Expence.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].RequestedAmt.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].Amount.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].ClaimStatus.toString() + '</td>'
                            if (jsonstr.Table[i].Amount.toString() != "0.00") {
                                if (jsonstr.Table[i].TL.toString() == 'Y') {
                                    datatable += '<td>' + '<a href="RembursementClaimApprovalView.aspx?jc=' + jc + '&type=Task&nid=0&aid=' + jsonstr.Table[i].ClaimMasterID.toString() + '">View</a> '

                                    if (jsonstr.Table[i].ClaimStatus.toString() == "Pending") {
                                        datatable += '|<a href="RembursementClaimPMReject.aspx?jc=' + jc + '&type=Task&nid=0&aid=' + jsonstr.Table[i].ClaimMasterID.toString() + '">Cancel</a>' + '</td>'
                                    }

                                }
                                else {
                                    datatable += '<td></td>'
                                }
                            }
                            else {
                                datatable += '<td>N/A</td>'
                            }
                            datatable += '</tr>'
                        }
                        datatable += '</tbody>'
                        //datatable += '</table><div class="pagination"></div>'

                        //$('#gridDiv').html = datatable;
                        document.getElementById('Docgrid').innerHTML = datatable;
                        $('#Docgrid').dataTable({
                            "order": [[1, "asc"]],
                            stateSave: true
                        });

                    }
                    else {
                        document.getElementById('Docgrid').innerHTML = "Data Not Found";
                    }
                }
            });
        }

        function fillTabKAM1Details() {
            var datatable = "";
            $.ajax({
                url: "AjaxCalls/PMDetails.aspx?call=13",
                context: document.body,

                success: function (Result) {
                    if (!Result == "") {
                        var jsonstr = JSON.parse(Result);
                        //datatable = '<table class="table table-bordered table-hover">'
                        datatable += '<thead>'
                        datatable += '<tr>'
                        datatable += '<th>' + 'ID' + '</th>'
                        datatable += '<th>' + 'Title' + '</th>'
                        datatable += '<th>' + 'Message' + '</th>'
                        datatable += '<th>' + 'Dated' + '</th>'
                        datatable += '<th>' + 'Status' + '</th>'
                        datatable += '</tr>'
                        datatable += '</thead>'

                        datatable += '<tbody id="tblbody">'
                        for (var i = 0; i < jsonstr.Table.length; i++) {
                            var j = i + 1;
                            datatable += '<tr>'
                            datatable += '<td>' + j + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].NotificationTitle.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].NotificationMessage.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].NotificationDate.toString() + '</td>'
                            if (jsonstr.Table[i].Status.toString() == "Open") {
                                datatable += '<td>' + '<a href="' + jsonstr.Table[i].Link.toString() + '"><span class="btn btn-sm btn-warning" >Open</span></a>' + '</td>'
                            }
                            else {
                                datatable += '<td>' + '<a href="' + jsonstr.Table[i].Link.toString() + '"><span class="btn btn-sm btn-danger" >Close</span></a>' + '</td>'
                                //datatable += '<td>' + '<input type="button" class="btn-small btn-danger" value="Close" />' + '</td>'
                            }
                            datatable += '</tr>'


                        }
                        datatable += '</tbody>'
                        //datatable += '</table><div class="pagination"></div>'

                        //$('#gridDiv').html = datatable;
                        document.getElementById('Docgrid').innerHTML = datatable;
                        $('#Docgrid').dataTable({
                            stateSave: true
                        });

                    }
                    else {
                        document.getElementById('Docgrid').innerHTML = "Data Not Found";
                    }
                }
            });
        }

        //for get claim page 

        function getclaimcheckboxvalue() {

            var valuesArray = $('input[name="chekclaimID"]:checked').map(function () {
                return this.value;
            }).get().join(",");

            if (valuesArray != "") {
                $("#btnvendoradvance").show()
                $("#btnvendoradvance").attr("href", "VendorAdvance.aspx?jc=" + GetParameterValues('jc') + "&type=Task&taid=" + valuesArray + "");

                $("#btnemployeeadvance").show()
                $("#btnemployeeadvance").attr("href", "EmployeeAdvance.aspx?jc=" + GetParameterValues('jc') + "&type=Task&taid=" + valuesArray + "");

            }
            else {
                $("#btnvendoradvance").hide()
                $("#btnemployeeadvance").hide()
            }
            ;

            //    alert(valuesArray);
        }

    </script>
    <script type="text/javascript">
        $(document).ready(function () {

            $(".header").click(function () {
                //alert(1);
                if ($(this).attr("checked") == true) {
                    $("input.items").attr('checked', true);
                }
                else {
                    $("input.items").attr('checked', false);
                }

            });
        });

        function Exporttoexcel() {
            window.open('data:application/vnd.ms-excel,' + $('#Docgrid').html());
            e.preventDefault();
        };

    </script>
</asp:Content>

