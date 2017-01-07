<%@ Page Title="" Language="VB" MasterPageFile="~/Apex.master" AutoEventWireup="false" CodeFile="Home.aspx.vb" Inherits="Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!-- DATA TABLES -->
    <link href="plugins/datatables/dataTables.bootstrap.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Dashboard
           
        </h1>
        <ol class="breadcrumb">
            <li><a class="active" href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>

        </ol>
    </section>

    <!-- Main content -->
    <section class="content">
        <div class="row">
            <div class="col-xs-12">
                <!-- .box -->
                <div class="box">
                    <div class="box-header">
                        <h3 class="box-title">Home</h3>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                        <div id="divError" runat="server" class="ui-state-error ui-corner-all divError" visible="false">
                            <p>
                                <span class="ui-accordion" style="float: left; margin-right: .3em;"></span><strong>Warning: </strong>
                                <asp:Label ID="lblError" runat="server"></asp:Label>
                            </p>
                        </div>
                        <div id="divContent" runat="server" class="divContent">
                            <div id="divMain" class="col-lg-12">
                                <asp:Panel runat="server" ID="pnlFM">
                                    <ul class="nav nav-tabs">
                                        <li id="FMli1" class="active"><a id="tabFMPendingJob" href="#" onclick="activeTabFM1(this)">Pending For Job Code</a></li>
                                        <li id="FMli2"><a href="#" id="tabFMRunningJob" onclick="activeTabFM2(this)">Running Jobs</a></li>
                                        <li id="FMli3"><a href="#" id="tabFMCompletedJob" onclick="activeTabFM3(this)">Pending For Invoice</a></li>
                                        <li id="FMli5"><a href="#" id="tabFMClosed" onclick="activeTabFM4(this)">Completed Jobs</a></li>
                                        <li id="FMli4" style="display: none;"><a href="#" id="tabFMNotifications" onclick="activeTabFM4(this)">Notifications</a></li>
                                    </ul>
                                </asp:Panel>

                                <asp:Panel runat="server" ID="PnlPM">
                                    <table id="DocgridPM" class="table table-bordered table-striped">
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

                                    <%--<ul class="nav nav-tabs">
                                        <li id="PMli1" class="active"><a id="tabPendingPnL" href="#" onclick="activePM1(this)">Pre P&L</a></li>
                                        <li id="PMli2"><a href="#" id="tabManageTeam" onclick="activePM2(this)">Manage Team</a></li>
                                        <li id="PMli3"><a href="#" id="tabTask" onclick="activePM3(this)">Your Tasks</a></li>
                                        <li id="PMli4"><a href="#" id="tabClaim" onclick="activePM4(this)">Claims</a></li>
                                        <li id="PMli5"><a href="#" id="tabPostPnL" onclick="activePM5(this)">Post P&L</a></li>
                                        <li id="PMli6" style="display: none;"><a href="#" >Notifications</a></li>
                                        <li id="PMli6" style="display: block;"><a href="#" onclick="activeKAM1(this)">Notifications</a></li>
                                    </ul>--%>
                                </asp:Panel>

                                <asp:Panel ID="PnlKAM" runat="server">
                                </asp:Panel>

                                <asp:Panel ID="pnlkamadmin" runat="server">
                                    <div class="col-121" style="display: block; margin-top: 20px;">
                                        <h5>Client Summary</h5>
                                        <div id="divclient" class="col-81" style="width: 67%; float: left; max-height: 180px; overflow: auto;">
                                            Loading...
                                        </div>

                                        <div id="divclientGraph" class="col-411" style="float: right; width: 33%; height: 250px;">
                                            <%--Loading...--%>
                                        </div>
                                    </div>
                                    <div class="col-121" style="display: block;">
                                        <p>&nbsp;</p>
                                        <h5>KAM Summary</h5>

                                        <div id="divkam" class="col-81" style="margin-top: 0px; width: 67%; float: left; max-height: 180px; min-height: 120px; overflow: auto;">
                                            Loading...
                                        </div>
                                        <div id="divkamGraph" class="col-411" style="float: right; width: 33%; margin-top: 0px; height: 250px;">
                                            <%--Grapth area--%>
                                        </div>
                                    </div>
                                    <div class="col-121" style="display: block; margin-top: 20px;">
                                        <h5>Cost centers Summary</h5>
                                        <div id="divcategory" class="col-81" style="margin-top: 0px; min-width: 67%; float: left; height: 250px; overflow: auto;">
                                            Loading...
                                        </div>
                                        <div id="divcategorygraph" class="col-411" style="float: right; width: 33%; margin-top: 0px">
                                            <%--Loading...--%>
                                        </div>

                                    </div>
                                </asp:Panel>
                            </div>

                            <div class="col-lg-12">
                                <div class="row">&nbsp;</div>
                                <table id="Docgrid" class="table table-bordered table-striped">
                                    <%--<span style="font-size: 40px;">Loading...</span>--%>
                                </table>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <asp:HiddenField ID="hdnRole" runat="server" />

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpFooter" runat="server">
    <!-- DATA TABES SCRIPT -->
    <script src="plugins/datatables/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="plugins/datatables/dataTables.bootstrap.min.js" type="text/javascript"></script>
    <!-- page script -->

    <script>

        $(document).ready(function () {
            //$('#hdnRole').val(getrole());

            getRole();

        });

        function activeTabFM1(id) {
            fillTabFM1Details();
            $('#FMli1').addClass('active');
            $('#FMli2').removeClass('active');
            $('#FMli3').removeClass('active');
            $('#FMli4').removeClass('active');
        }
        function activeTabFM2(id) {
            fillTabFM2Details();
            $('#FMli1').removeClass('active');
            $('#FMli2').addClass('active');
            $('#FMli3').removeClass('active');
            $('#FMli4').removeClass('active');
            $('#FMli5').removeClass('active');
        }
        function activeTabFM3(id) {
            fillTabFM3Details();
            $('#FMli1').removeClass('active');
            $('#FMli2').removeClass('active');
            $('#FMli3').addClass('active');
            $('#FMli4').removeClass('active');
            $('#FMli5').removeClass('active');
        }
        function activeTabFM4(id) {
            fillTabFM4Details();
            $('#FMli1').removeClass('active');
            $('#FMli2').removeClass('active');
            $('#FMli3').removeClass('active');
            $('#FMli4').removeClass('active');
            $('#FMli5').addClass('active');
        }

        function fillTabFM1Details() {
            var datatable = "";
            $.ajax({
                url: "AjaxCalls/AX_Dashboard.aspx?call=1",
                context: document.body,

                success: function (Result) {
                    if (!Result == "") {
                        var jsonstr = JSON.parse(Result);
                        //datatable = '<table class="table table-bordered table-hover">'
                        datatable += '<thead>'
                        datatable += '<tr>'
                        datatable += '<th>' + 'ID' + '</th>'
                        datatable += '<th>' + 'Job Code' + '</th>'
                        datatable += '<th>' + 'Title' + '</th>'
                        datatable += '<th>' + 'IsTask' + '</th>'
                        datatable += '<th>' + 'Project Manager' + '</th>'
                        datatable += '<th>' + 'Project Type' + '</th>'
                        datatable += '<th>' + 'Action' + '</th>'
                        datatable += '</tr>'
                        datatable += '</thead>'

                        datatable += '<tbody id="tblbody">'
                        for (var i = 0; i < jsonstr.Table.length; i++) {
                            var j = i + 1;
                            datatable += '<tr>'
                            datatable += '<td>' + j + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].JobCardNo.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].JobCardName.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].IsTask.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].ProjectManager.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].ProjectType.toString() + '</td>'

                            if (jsonstr.Table[i].link.toString() != 'N/A') {
                                datatable += '<td><a href="' + jsonstr.Table[i].link.toString() + '")><span class="btn btn-sm btn-success" >Assign Job Code  </span></a></td>'
                            }
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
        function fillTabFM2Details() {
            var datatable = "";
            $.ajax({
                url: "AjaxCalls/AX_Dashboard.aspx?call=2",
                context: document.body,

                success: function (Result) {
                    if (!Result == "") {
                        var jsonstr = JSON.parse(Result);
                        //datatable = '<table class="table table-bordered table-hover">'
                        datatable += '<thead>'
                        datatable += '<tr>'
                        datatable += '<th>' + 'ID' + '</th>'
                        datatable += '<th>' + 'Job Code' + '</th>'
                        datatable += '<th>' + 'Title' + '</th>'
                        datatable += '<th>' + 'Project Manager' + '</th>'
                        datatable += '<th>' + 'Project Type' + '</th>'
                        datatable += '<th>' + 'Action' + '</th>'
                        datatable += '</tr>'
                        datatable += '</thead>'

                        datatable += '<tbody id="tblbody">'
                        for (var i = 0; i < jsonstr.Table.length; i++) {
                            var j = i + 1;
                            datatable += '<tr>'
                            datatable += '<td>' + j + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].JobCardNo.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].JobCardName.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].ProjectManager.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].ProjectType.toString() + '</td>'

                            datatable += '<td><a href="JobCardManager.aspx?mode=edit&user=FM&jid=' + jsonstr.Table[i].JobCardID.toString() + '"><span class="btn btn-sm btn-success" value="View">View</span></a></td>'
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
        function fillTabFM3Details() {
            var datatable = "";
            $.ajax({
                url: "AjaxCalls/AX_Dashboard.aspx?call=3",
                context: document.body,

                success: function (Result) {
                    if (!Result == "") {
                        var jsonstr = JSON.parse(Result);
                        //datatable = '<table class="table table-bordered table-hover">'
                        datatable += '<thead>'
                        datatable += '<tr>'
                        datatable += '<th>' + 'ID' + '</th>'
                        datatable += '<th>' + 'Job Code' + '</th>'
                        datatable += '<th>' + 'Title' + '</th>'
                        datatable += '<th>' + 'Project Manager' + '</th>'
                        datatable += '<th>' + 'Project Type' + '</th>'
                        datatable += '<th>' + 'Job Details' + '</th>'
                        datatable += '<th>' + 'Job Invoice' + '</th>'
                        //datatable += '<th>' + 'Re-Open Job' + '</th>'
                        datatable += '</tr>'
                        datatable += '</thead>'

                        datatable += '<tbody id="tblbody">'
                        for (var i = 0; i < jsonstr.Table.length; i++) {
                            var j = i + 1;
                            datatable += '<tr>'
                            datatable += '<td>' + j + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].JobCardNo.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].JobCardName.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].ProjectManager.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].ProjectType.toString() + '</td>'
                            datatable += '<td><a href="Estimate_VS_Actuals.aspx?jid=' + jsonstr.Table[i].JobCardID.toString() + '&type=fm"><span class="btn btn-sm  btn-success" value="View" >View </span></a></td>'
                            datatable += '<td><a href="viewJobInvoice.aspx?jid=' + jsonstr.Table[i].JobCardID.toString() + '&min=' + jsonstr.Table[i].RefMinTempEstimateID.toString() + '&max=' + jsonstr.Table[i].RefMaxTempEstimateID.toString() + '"><span class="btn btn-sm  btn-success" value="View" >View </span></a></td>'
                            //datatable += '<td><input type="button" class="btn-small btn-success" value="Re-Open" onclick="return opnejobcard();" /></a></td>'
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
        function fillTabFM4Details() {
            var datatable = "";
            $.ajax({
                url: "AjaxCalls/AX_Dashboard.aspx?call=21",
                context: document.body,

                success: function (Result) {
                    if (!Result == "") {
                        var jsonstr = JSON.parse(Result);
                        //datatable = '<table class="table table-bordered table-hover">'
                        datatable += '<thead>'
                        datatable += '<tr>'
                        datatable += '<th>' + 'ID' + '</th>'
                        datatable += '<th>' + 'Job Code' + '</th>'
                        datatable += '<th>' + 'Title' + '</th>'
                        datatable += '<th>' + 'Project Manager' + '</th>'
                        datatable += '<th>' + 'Project Type' + '</th>'
                        datatable += '<th>' + 'Job Details' + '</th>'
                        datatable += '<th>' + 'Job Invoice' + '</th>'
                        //datatable += '<th>' + 'Re-Open Job' + '</th>'
                        datatable += '</tr>'
                        datatable += '</thead>'

                        datatable += '<tbody id="tblbody">'
                        for (var i = 0; i < jsonstr.Table.length; i++) {
                            var j = i + 1;
                            datatable += '<tr>'
                            datatable += '<td>' + j + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].JobCardNo.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].JobCardName.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].ProjectManager.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].ProjectType.toString() + '</td>'
                            datatable += '<td><a href="Estimate_VS_Actuals.aspx?jid=' + jsonstr.Table[i].JobCardID.toString() + '&type=fm"><span class="btn btn-sm  btn-success" value="View" >View </span></a></td>'
                            datatable += '<td><a href="viewJobInvoice.aspx?jid=' + jsonstr.Table[i].JobCardID.toString() + '&min=' + jsonstr.Table[i].RefMinTempEstimateID.toString() + '&max=' + jsonstr.Table[i].RefMaxTempEstimateID.toString() + '"><span class="btn btn-sm  btn-success" value="View" >View </span></a></td>'
                            //datatable += '<td><input type="button" class="btn-small btn-success" value="Re-Open" onclick="return opnejobcard();" /></a></td>'
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
        function opnejobcard(jid) {
            var rsul = confirm('Are you sure you want to re-open job ?');
            if (rsul) {
                $.ajax({
                    url: "AjaxCalls/AX_Jobs.aspx?call=3&jid=" + jid,
                    context: document.body,
                    success: function (Result) {
                        fillTabFM3Details();
                    }
                });
            }
            else {
                return false;
            }
        }
        function getRole() {
            var res = "";
            $.ajax({
                url: "AjaxCalls/AX_Dashboard.aspx?call=4",
                context: document.body,
                success: function (Result) {
                    res = Result;

                    if (res == "K") {

                        //fillTabKAM2Details();
                        fillTabClientwiseDetails();
                        fillTabKAMwiseDetails();
                        fillTabCategorywiseDetails();
                    }
                    if (res == "H") {

                        fillTabClientwiseDetails();
                        fillTabKAMwiseDetails();
                        fillTabCategorywiseDetails();
                    }
                    if (res == "F") {
                        $('#ContentPlaceHolder1_pnlkamadmin').hide();
                        fillTabFM1Details();
                    }

                    if (res == "O") {
                        $('#ContentPlaceHolder1_pnlkamadmin').hide();
                        fillTabPM1Detailssearch();
                        fillTabPM1Details();
                    }
                    if (res == "R") {
                        //fillTabPM1Details();
                        //alert('Test role');
                        $('#ContentPlaceHolder1_pnlkamadmin').hide();
                        fillRM();
                    }

                }
            });
            //alert(res);
            return res;
        }

        function activePM1(id) {
            fillTabPM1Details();
            $('#PMli1').addClass("active");
            $('#PMli2').removeClass("active");
            $('#PMli3').removeClass("active");
            $('#PMli4').removeClass("active");
            $('#PMli5').removeClass("active");
            $('#PMli6').removeClass("active");
        }
        function activePM2(id) {
            fillTabPM2Details();
            $('#PMli1').removeClass("active");
            $('#PMli2').addClass("active");
            $('#PMli3').removeClass("active");
            $('#PMli4').removeClass("active");
            $('#PMli5').removeClass("active");
            $('#PMli6').removeClass("active");
        }
        function activePM3(id) {
            fillTabPM3Details()
            $('#PMli1').removeClass("active");
            $('#PMli2').removeClass("active");
            $('#PMli3').addClass("active");
            $('#PMli4').removeClass("active");
            $('#PMli5').removeClass("active");
            $('#PMli6').removeClass("active");
        }
        function activePM4(id) {
            fillTabPM4Details()
            $('#PMli1').removeClass("active");
            $('#PMli2').removeClass("active");
            $('#PMli3').removeClass("active");
            $('#PMli4').addClass("active");
            $('#PMli5').removeClass("active");
            $('#PMli6').removeClass("active");
        }
        function activePM5(id) {
            fillTabPM5Details()
            $('#PMli1').removeClass("active");
            $('#PMli2').removeClass("active");
            $('#PMli3').removeClass("active");
            $('#PMli4').removeClass("active");
            $('#PMli5').addClass("active");
            $('#PMli6').removeClass("active");
        }
        function activePM6(id) {
            fillTabPM6Details()
            $('#PMli1').removeClass("active");
            $('#PMli2').removeClass("active");
            $('#PMli3').removeClass("active");
            $('#PMli4').removeClass("active");
            $('#PMli5').removeClass("active");
            $('#PMli6').addClass("active");
        }

        function fillTabClientwiseDetails() {
            var month = new Array();
            month[0] = "January";
            month[1] = "February";
            month[2] = "March";
            month[3] = "April";
            month[4] = "May";
            month[5] = "June";
            month[6] = "July";
            month[7] = "August";
            month[8] = "September";
            month[9] = "October";
            month[10] = "November";
            month[11] = "December";

            //Chart

            //chart start
            var data1 = new Array();
            var data2 = new Array();
            var data = new Array();
            var arrdata = ""
            var strXMLdata = ""
            //Data for each product





            var datatable = "";
            $.ajax({
                url: "AjaxCalls/AX_Dashboard.aspx?call=15",
                context: document.body,

                success: function (Result) {
                    if (!Result == "") {
                        var jsonstr = JSON.parse(Result);
                        datatable = '<table class="table table-bordered table-hover" >'
                        datatable += '<thead>'
                        datatable += '<tr>'

                        datatable += '<th>' + 'Client' + '</th>'
                        if (new Date().getMonth() == 0) {
                            //alert(month[11]);
                            datatable += '<th>' + 'Sum Of Net Rev. ' + month[11] + '</th>'
                        }
                        else {

                            //alert(month[new Date().getMonth() - 1]);
                            datatable += '<th>' + 'Sum Of Net Rev. ' + month[new Date().getMonth() - 1] + '</th>'
                        }

                        //datatable += '<th>' + 'Sum Of Net Rev. ' + month[new Date().getMonth() - 1] + '</th>'
                        datatable += '<th>' + 'Sum Of Net Rev. ' + month[new Date().getMonth()] + '</th>'
                        //$.datepicker.formatDate("MM yy", new Date())
                        datatable += '<th>' + 'Difference' + '</th>'
                        datatable += '<th>' + 'Change %' + '</th>'
                        datatable += '</tr>'
                        datatable += '</thead>'

                        datatable += '<tbody id="tblbody" >'
                        strXMLdata = strXMLdata + "<categories>";
                        for (var i = 0; i < jsonstr.Table.length; i++) {
                            var j = i + 1;
                            datatable += '<tr>'
                            //datatable += '<td>' + j + '</td>'

                            datatable += '<td>' + jsonstr.Table[i].client.toString() + '</td>'
                            datatable += '<td>' + numDifferentiation(jsonstr.Table[i].Revinew1.toString()) + '</td>'
                            datatable += '<td>' + numDifferentiation(jsonstr.Table[i].Rev2.toString()) + '</td>'
                            datatable += '<td>' + numDifferentiation(jsonstr.Table[i].Differ.toString()) + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].change.toString() + '</td>'
                            datatable += '</tr>'
                           
                        }
                       

                        datatable += '</tbody>'
                        datatable += '</table>'

                        //$('#gridDiv').html = datatable;
                        document.getElementById('divclient').innerHTML = datatable;

                    }
                    else {
                        document.getElementById('divclient').innerHTML = "Data Not Found";
                    }
                }

            });



        }
        function fillTabKAMwiseDetails() {
            var month = new Array();
            month[0] = "January";
            month[1] = "February";
            month[2] = "March";
            month[3] = "April";
            month[4] = "May";
            month[5] = "June";
            month[6] = "July";
            month[7] = "August";
            month[8] = "September";
            month[9] = "October";
            month[10] = "November";
            month[11] = "December";

            var datatable = "";
            $.ajax({
                url: "AjaxCalls/AX_Dashboard.aspx?call=16",
                context: document.body,

                success: function (Result) {
                    if (!Result == "") {
                        var jsonstr = JSON.parse(Result);
                        datatable = '<table class="table table-bordered table-hover">'
                        datatable += '<thead>'
                        datatable += '<tr>'

                        datatable += '<th>' + 'KAM' + '</th>'
                        if (new Date().getMonth() == 0) {
                            //alert(month[11]);
                            datatable += '<th>' + 'Sum Of Net Rev. ' + month[11] + '</th>'
                        }
                        else {

                            //alert(month[new Date().getMonth() - 1]);
                            datatable += '<th>' + 'Sum Of Net Rev. ' + month[new Date().getMonth() - 1] + '</th>'
                        }

                        //datatable += '<th>' + 'Sum Of Net Rev. ' + month[new Date().getMonth() - 1] + '</th>'
                        datatable += '<th>' + 'Sum Of Net Rev. ' + month[new Date().getMonth()] + '</th>'
                        //$.datepicker.formatDate("MM yy", new Date())
                        datatable += '<th>' + 'Difference' + '</th>'
                        datatable += '<th>' + 'Change %' + '</th>'
                        datatable += '</tr>'
                        datatable += '</thead>'

                        datatable += '<tbody id="tblbody">'
                        for (var i = 0; i < jsonstr.Table.length; i++) {
                            var j = i + 1;
                            datatable += '<tr>'
                            //datatable += '<td>' + j + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].KAM.toString() + '</td>'

                            datatable += '<td>' + numDifferentiation(jsonstr.Table[i].Revinew1.toString()) + '</td>'
                            datatable += '<td>' + numDifferentiation(jsonstr.Table[i].Rev2.toString()) + '</td>'
                            datatable += '<td>' + numDifferentiation(jsonstr.Table[i].Differ.toString()) + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].change.toString() + '</td>'
                            datatable += '</tr>'


                        }
                        datatable += '</tbody>'
                        datatable += '</table>'

                        //$('#gridDiv').html = datatable;
                        document.getElementById('divkam').innerHTML = datatable;

                    }
                    else {
                        document.getElementById('divkam').innerHTML = "Data Not Found";
                    }
                }
            });
        }
        function fillTabCategorywiseDetails() {
            var month = new Array();
            month[0] = "January";
            month[1] = "February";
            month[2] = "March";
            month[3] = "April";
            month[4] = "May";
            month[5] = "June";
            month[6] = "July";
            month[7] = "August";
            month[8] = "September";
            month[9] = "October";
            month[10] = "November";
            month[11] = "December";

            //chart start
            var data1 = new Array();
            var data2 = new Array();
            var data = new Array();
            var arrdata = ""
            var strXMLdata = ""
            //Data for each product

            var datatable = "";
            $.ajax({
                url: "AjaxCalls/AX_Dashboard.aspx?call=17",
                context: document.body,

                success: function (Result) {
                    if (!Result == "") {
                        var jsonstr = JSON.parse(Result);

                        ClientWiseBarGraph(jsonstr);

                        datatable = '<table class="table table-bordered table-hover">'
                        datatable += '<thead>'
                        datatable += '<tr>'

                        datatable += '<th>' + 'Category' + '</th>'
                        if (new Date().getMonth() == 0) {
                            //alert(month[11]);
                            datatable += '<th>' + 'Sum Of Net Rev. ' + month[11] + '</th>'
                        }
                        else {

                            //alert(month[new Date().getMonth() - 1]);
                            datatable += '<th>' + 'Sum Of Net Rev. ' + month[new Date().getMonth() - 1] + '</th>'
                        }

                        //datatable += '<th>' + 'Sum Of Net Rev. ' + month[new Date().getMonth() - 1] + '</th>'
                        datatable += '<th>' + 'Sum Of Net Rev. ' + month[new Date().getMonth()] + '</th>'
                        //$.datepicker.formatDate("MM yy", new Date())
                        datatable += '<th>' + 'Difference' + '</th>'
                        datatable += '<th>' + 'Change %' + '</th>'
                        datatable += '</tr>'
                        datatable += '</thead>'

                        datatable += '<tbody id="tblbody">'
                        strXMLdata = strXMLdata + "<categories>";
                        for (var i = 0; i < jsonstr.Table.length; i++) {
                            var j = i + 1;
                            datatable += '<tr>'
                            //datatable += '<td>' + j + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].Category.toString() + '</td>'
                            datatable += '<td>' + numDifferentiation(jsonstr.Table[i].Revinew1.toString()) + '</td>'
                            datatable += '<td>' + numDifferentiation(jsonstr.Table[i].Rev2.toString()) + '</td>'
                            datatable += '<td>' + numDifferentiation(jsonstr.Table[i].Differ.toString()) + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].change.toString() + '</td>'
                            datatable += '</tr>'

                            }
                        
                        
                        datatable += '</tbody>'
                        datatable += '</table>'

                        //$('#gridDiv').html = datatable;
                        document.getElementById('divcategory').innerHTML = datatable;

                        ClientWiseBarGraph(jsonstr);

                    }
                    else {
                        document.getElementById('divcategory').innerHTML = "Data Not Found";
                    }
                }
            });
        }

        function fillTabPM1Details() {
            var datatable = "";
            $.ajax({
                url: "AjaxCalls/AX_Dashboard.aspx?call=5", cache: false,
                context: document.body,

                success: function (Result) {
                    if (!Result == "") {
                        var jsonstr = JSON.parse(Result);
                        //datatable = '<table class="table table-bordered table-hover">'
                        datatable += '<thead>'
                        datatable += '<tr>'
                        datatable += '<th>' + 'ID' + '</th>'
                        datatable += '<th>' + 'Job Code' + '</th>'
                        datatable += '<th>' + 'KAM Name' + '</th>'
                        datatable += '<th>' + 'Activity Name' + '</th>'
                        datatable += '<th>' + 'Project Type' + '</th>'
                        datatable += '<th>' + 'Activity Date' + '</th>'

                        datatable += '<th>' + 'Pre P&L' + '</th>'
                        //datatable += '<th>' + 'Manage Team' + '</th>'
                        datatable += '</tr>'
                        datatable += '</thead>'

                        datatable += '<tbody id="tblbody">'
                        for (var i = 0; i < jsonstr.Table.length; i++) {
                            var j = i + 1;
                            datatable += '<tr>'
                            datatable += '<td>' + j + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].JobcardNo.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].KAMName.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].BriefName.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].ProjectType.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].Activitydate.toString() + '</td>'


                            if (jsonstr.Table[i].linkname.toString() == 'Pending') {
                                datatable += '<td><a href="' + jsonstr.Table[i].link.toString() + '")><span class="btn btn-sm  btn-warning" value="Pending" >Pending </span></a></td>'

                            }
                            else if (jsonstr.Table[i].linkname.toString() == 'Generated') {
                                datatable += '<td><a href="' + jsonstr.Table[i].link.toString() + '")><span class="btn btn-sm  btn-success" value="Generated">Generated </span></a></td>'

                            }

                            //datatable += '<td><a href="JobCardManager.aspx?mode=edit&jid=' + jsonstr.Table[i].JobcardID.toString() + '")>Manage Team</a></td>'
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
        function fillTabPM2Details() {
            var datatable = "";
            $.ajax({
                url: "AjaxCalls/AX_Dashboard.aspx?call=6",
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
                                datatable += '<td><a href="ViewPrepnl.aspx?jid=' + jsonstr.Table[i].JobCardID.toString() + '"><span value="View" class="btn btn-sm  btn-success">View</span></a></td>'
                            }
                            else {
                                datatable += '<td><a href="ViewPrepnl.aspx?jid=' + jsonstr.Table[i].JobCardID.toString() + '"><span type="button" class="btn btn-danger btn-sm" value="JobCompleted" >JobCompleted</span></a></td>'
                            }
                            //datatable += '<td><a href="ViewPrepnl.aspx?jid=' + jsonstr.Table[i].JobCardID.toString() + '">View</a></td>'
                            datatable += '<td><a href="ListOfTask.aspx?jid=' + jsonstr.Table[i].JobCardID.toString() + '" class="btn btn-sm btn-default">View</a></td>'
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
        function fillTabPM3Details() {
            var datatable = "";
            $.ajax({
                url: "AjaxCalls/AX_Dashboard.aspx?call=7", cache: false,
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
                            datatable += '<td>' + '<a href="teamSelect.aspx?taid=' + jsonstr.Table[i].AccountID.toString() + '&TL=Y" class="btn btn-sm btn-default">Manage</a></td>'
                            datatable += '<td>' + '<a href="Checklist.aspx?tid=' + jsonstr.Table[i].AccountID.toString() + '" class="btn btn-sm btn-default">Manage</a></td>'


                            datatable += '<td>' + jsonstr.Table[i].TaskCompleted.toString() + '</td>'
                            datatable += '<td><a href="RembursementClaim.aspx?taid=' + jsonstr.Table[i].AccountID.toString() + '" class="btn btn-sm btn-default">For Claim</a></td>'
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
        function fillTabPM4Details() {
            var datatable = "";
            $.ajax({
                url: "AjaxCalls/AX_Dashboard.aspx?call=8", cache: false,
                context: document.body,

                success: function (Result) {
                    if (!Result == "") {
                        var jsonstr = JSON.parse(Result);
                        //datatable = '<table class="table table-bordered table-hover">'
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
                            datatable += '<td><a href="JobCardManager.aspx?jid=' + jsonstr.Table[i].JobCardID.toString() + '"><span class="btn btn-sm btn-success" >"' + jsonstr.Table[i].JobCardNo.toString() + '" </span></a></td>'
                            datatable += '<td>' + jsonstr.Table[i].title.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].Amount.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].ClaimStatus.toString() + '</td>'
                            datatable += '<td>' + '<a href="RembursementClaimApprovalView.aspx?type=Task&nid=0&aid=' + jsonstr.Table[i].ClaimMasterID.toString() + '" class="btn btn-sm btn-default">View</a>' + '</td>'
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
        function fillTabPM5Details() {
            var datatable = "";
            $.ajax({
                url: "AjaxCalls/AX_Dashboard.aspx?call=9", cache: false,
                context: document.body,

                success: function (Result) {
                    if (!Result == "") {
                        var jsonstr = JSON.parse(Result);
                        //datatable = '<table class="table table-bordered table-hover">'
                        datatable += '<thead>'
                        datatable += '<tr>'
                        datatable += '<th>' + 'ID' + '</th>'
                        datatable += '<th>' + 'Title' + '</th>'
                        datatable += '<th>' + 'Primary Activity' + '</th>'
                        datatable += '<th>' + 'Activity End Date' + '</th>'
                        datatable += '<th>' + 'Post P&L' + '</th>'
                        datatable += '</tr>'
                        datatable += '</thead>'

                        datatable += '<tbody id="tblbody">'
                        for (var i = 0; i < jsonstr.Table.length; i++) {
                            var j = i + 1;
                            datatable += '<tr>'
                            datatable += '<td>' + j + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].JobCardName.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].ProjectType.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].Activitydate.toString() + '</td>'
                            if (jsonstr.Table[i].PostPnl.toString() == "Y") {
                                datatable += '<td> <a href="PostPnLManager.aspx?jid=' + jsonstr.Table[i].JobCardID + '"><span class="btn btn-sm btn-success" >Generated</span></a></td>'

                            }
                            else {
                                datatable += '<td> <a href="PostPnLManager.aspx?jid=' + jsonstr.Table[i].JobCardID + '"><span class="btn btn-sm btn-warning"  >Pending</span></a></td>'
                            }
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
        function fillTabPM6Details() { }

        function activeKAM1(id) {
            fillTabKAM1Details();
            $('#KAMli1').addClass("active");
            $('#KAMli2').removeClass("active");
            $('#KAMli3').removeClass("active");
            $('#KAMli4').removeClass("active");
        }
        function activeKAM2(id) {
            fillTabKAM2Details();
            $('#KAMli1').removeClass("active");
            $('#KAMli2').addClass("active");
            $('#KAMli3').removeClass("active");
            $('#KAMli4').removeClass("active");
        }
        function activeKAM3(id) {
            fillTabKAM3Details();
            $('#KAMli1').removeClass("active");
            $('#KAMli2').removeClass("active");
            $('#KAMli3').addClass("active");
            $('#KAMli4').removeClass("active");
        }
        function activeKAM4(id) {
            fillTabKAM4Details();
            $('#KAMli1').removeClass("active");
            $('#KAMli2').removeClass("active");
            $('#KAMli3').removeClass("active");
            $('#KAMli4').addClass("active");
        }

        function fillTabKAM1Details() {
            var datatable = "";
            $.ajax({
                url: "AjaxCalls/AX_Dashboard.aspx?call=13",
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
                                datatable += '<td>' + '<a href="' + jsonstr.Table[i].Link.toString() + '"><span class="btn-small btn-warning" >Open</span></a>' + '</td>'
                            }
                            else {
                                datatable += '<td>' + '<input type="button" class="btn-small btn-danger" value="Close" />' + '</td>'
                            }
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
        function fillTabKAM2Details() {
            var datatable = "";
            $.ajax({
                url: "AjaxCalls/AX_Dashboard.aspx?call=10",
                context: document.body,

                success: function (Result) {
                    if (!Result == "") {
                        var jsonstr = JSON.parse(Result);
                        //datatable = '<table class="table table-bordered table-hover">'
                        datatable += '<thead>'
                        datatable += '<tr>'
                        datatable += '<th>' + 'ID' + '</th>'
                        datatable += '<th>' + 'JC No' + '</th>'
                        datatable += '<th>' + 'Category' + '</th>'
                        datatable += '<th>' + 'Task' + '</th>'
                        datatable += '<th>' + 'Item' + '</th>'
                        datatable += '<th>' + 'Specs.' + '</th>'
                        datatable += '<th>' + 'Qty.' + '</th>'
                        datatable += '<th>' + 'Inch.' + '</th>'
                        datatable += '<th>' + 'Mem.' + '</th>'
                        datatable += '<th>' + 'ETD' + '</th>'
                        datatable += '<th>' + 'Status' + '</th>'
                        datatable += '<th>' + 'Vendor' + '</th>'
                        datatable += '<th>' + 'Contact' + '</th>'
                        datatable += '<th>' + 'Remarks' + '</th>'
                        datatable += '</tr>'
                        datatable += '</thead>'

                        datatable += '<tbody id="tblbody">'
                        for (var i = 0; i < jsonstr.Table.length; i++) {
                            var j = i + 1;
                            datatable += '<tr>'
                            datatable += '<td>' + j + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].JobCardNo.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].Category.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].Task.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].Description.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].Specifications.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].Quantity.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].Incharge.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].Member.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].ETD.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].Status.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].Vendor.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].ContactName.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].Remarks.toString() + '</td>'
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
        function fillTabKAM3Details() {
            var datatable = "";
            $.ajax({
                url: "AjaxCalls/AX_Dashboard.aspx?call=11",
                context: document.body,

                success: function (Result) {
                    if (!Result == "") {
                        var jsonstr = JSON.parse(Result);
                        //datatable = '<table class="table table-bordered table-hover">'
                        datatable += '<thead>'
                        datatable += '<tr>'
                        datatable += '<th>' + 'ID' + '</th>'
                        datatable += '<th>' + 'JC No' + '</th>'
                        datatable += '<th>' + 'Client' + '</th>'
                        datatable += '<th>' + 'KAM' + '</th>'
                        datatable += '<th>' + 'Title' + '</th>'
                        datatable += '<th>' + 'Project Type' + '</th>'
                        datatable += '<th>' + 'Start Date' + '</th>'
                        datatable += '<th>' + 'End Date' + '</th>'
                        datatable += '<th>' + 'Status' + '</th>'
                        datatable += '<th>' + 'Net Revnue' + '</th>'
                        datatable += '<th>' + 'Total Cost' + '</th>'
                        datatable += '<th>' + 'Profit' + '</th>'
                        datatable += '<th>' + 'Profit %' + '</th>'
                        datatable += '<th>' + 'Internal Budget' + '</th>'
                        datatable += '<th>' + 'Pre P&L %' + '</th>'
                        datatable += '<th>' + 'Post P&L %' + '</th>'
                        datatable += '</tr>'
                        datatable += '</thead>'

                        datatable += '<tbody id="tblbody">'
                        for (var i = 0; i < jsonstr.Table.length; i++) {
                            var j = i + 1;
                            datatable += '<tr>'
                            datatable += '<td>' + j + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].JobCardNo.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].Client.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].KAM.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].JobCardName.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].ProjectType.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].StartDate.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].EndDate.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].JobStatus.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].NetRevenue.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].TotalCost.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].Profit.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].ProfitPercent.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].InternalBudget.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].PrePnLPer.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].PostPnLPer.toString() + '</td>'
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
        function fillTabKAM4Details() {
            var datatable = "";
            $.ajax({
                url: "AjaxCalls/AX_Dashboard.aspx?call=12",
                context: document.body,

                success: function (Result) {
                    if (!Result == "") {
                        var jsonstr = JSON.parse(Result);
                        //datatable = '<table class="table table-bordered table-hover">'
                        datatable += '<thead>'
                        datatable += '<tr>'
                        datatable += '<th>' + 'ID' + '</th>'
                        datatable += '<th>' + 'JC No' + '</th>'
                        datatable += '<th>' + 'Client' + '</th>'
                        datatable += '<th>' + 'KAM' + '</th>'
                        datatable += '<th>' + 'Title' + '</th>'
                        datatable += '<th>' + 'Project Type' + '</th>'
                        datatable += '<th>' + 'Status' + '</th>'
                        datatable += '<th>' + 'PM' + '</th>'
                        datatable += '<th>' + 'Budget Alloc.' + '</th>'
                        datatable += '<th>' + 'Budget Util.' + '</th>'
                        datatable += '<th>' + 'Savngs' + '</th>'
                        datatable += '<th>' + 'Profitability' + '</th>'
                        datatable += '</tr>'
                        datatable += '</thead>'

                        datatable += '<tbody id="tblbody">'
                        for (var i = 0; i < jsonstr.Table.length; i++) {
                            var j = i + 1;
                            datatable += '<tr>'
                            datatable += '<td>' + j + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].JobCardNo.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].Client.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].KAM.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].JobCardName.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].ProjectType.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].JobCompleted.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].ProjectManager.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].EstimatedSubTotal.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].postpnltotal.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].remaining.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].porofit.toString() + '</td>'
                            datatable += '</tr>'


                        }
                        datatable += '</tbody>'
                        //datatable += '</table><div class="pagination" style="border:1px;"></div>'

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

       

    </script>

    <!-- PM GRID -->

    <script type="text/javascript">


        function fillTabPM1Detailssearch() {

            var urls = "AjaxCalls/PMDetails.aspx?call=5";

            $.ajax({

                //url: "../AjaxCalls/PMDetails.aspx?call=5", cache: false,
                url: urls, cache: false,
                context: document.body,

                success: function (Result) {
                    if (!Result == "") {
                        var jsonstr = JSON.parse(Result);

                        var datatable = "";

                        datatable += '<thead>'
                        datatable += '<tr>'
                        datatable += '<th>' + 'Project Name' + '</th>'
                        datatable += '<th>' + 'Client Name' + '</th>'
                        datatable += '<th>' + 'Job Code' + '</th>'
                        datatable += '<th>' + 'Estimate' + '</th>'
                        //Budget
                        datatable += '<th>' + 'Claims' + '</th>'
                        datatable += '<th>' + 'Final Cost' + '</th>'
                        //datatable += '<th>' + 'Savings' + '</th>'
                        datatable += '<th>' + 'JC Status' + '</th>'
                        datatable += '</tr>'
                        datatable += '</thead>'

                        datatable += '<tbody id="tblbody">'
                        for (var i = 0; i < jsonstr.Table.length; i++) {
                            var j = i + 1;
                            datatable += '<tr>'
                            datatable += '<td>' + jsonstr.Table[i].Briefname.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].Client.toString() + '</td>'

                            if (jsonstr.Table[i].JobcardNo.toString() != "N/A") {

                                datatable += '<td ><a href="claimmaster.aspx?jc=' + jsonstr.Table[i].jobCardID.toString() + '" Id="JCwise"  target="_parent">' + jsonstr.Table[i].JobcardNo.toString() + '</a></td>'
                            }
                            else {
                                datatable += '<td><a href="PrePnlManager.aspx?mode=edit&bid=' + jsonstr.Table[i].BriefID.toString() + '&kid=y" Id="JCwise"  target="_parent">' + jsonstr.Table[i].JobcardNo.toString() + '</a></td>'
                            }

                            datatable += '<td align="Right">' + jsonstr.Table[i].Budget.toString() + '</td>'

                            if (jsonstr.Table[i].Claims.toString() != '') {
                                datatable += '<td ><a href="claimmaster.aspx?jc=' + jsonstr.Table[i].jobCardID.toString() + '" Id="JCwise" target="main" target="_parent">' + jsonstr.Table[i].Claims.toString() + '</a></td>'
                            }
                            else {
                                datatable += '<td align="Right">' + jsonstr.Table[i].Claims.toString() + '</td>'
                            }
                            datatable += '<td align="Right">' + jsonstr.Table[i].Final_Cost.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].JobStatus.toString() + '</td>'

                            datatable += '</tr>'


                        }
                        datatable += '</tbody>'
                        //datatable += '</table><div class="pagination"></div>'

                        //$('#gridDiv').html = datatable;
                        document.getElementById('DocgridPM').innerHTML = datatable;
                        $('#DocgridPM').dataTable();


                    }
                    else {
                        document.getElementById('DocgridPM').innerHTML = "Data Not Found";
                    }
                }
            });
        }

        function numDifferentiation(val) {
            if (val >= 10000000) val = (val / 10000000).toFixed(2) + ' Cr';
            else if (val >= 100000) val = (val / 100000).toFixed(2) + ' Lac';
            else if (val >= 1000) val = (val / 1000).toFixed(2) + ' K';
            return val;
        }

    </script>
</asp:Content>

