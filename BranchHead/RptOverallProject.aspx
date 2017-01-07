<%@ Page Title="" Language="VB" MasterPageFile="~/BranchHead/Apex.master" AutoEventWireup="false" CodeFile="RptOverallProject.aspx.vb" Inherits="RptOverallProject" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Overall Report</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">Overall Report</li>
        </ol>
    </section>

    <!-- Main content -->
    <section class="content">
        <div class="row">
            <div class="col-lg-12">
                <div class="box box-primary">
                    <div class="box-header">
                    </div>

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
                                        <li id="FMli3"><a href="#" id="tabFMCompletedJob" onclick="activeTabFM3(this)">Completed Jobs</a></li>
                                        <li id="FMli4" style="display: none;"><a href="#" id="tabFMNotifications" onclick="activeTabFM4(this)">Notifications</a></li>
                                    </ul>
                                </asp:Panel>

                                <asp:Panel runat="server" ID="PnlPM">
                                    <ul class="nav nav-tabs">
                                        <li id="PMli1" class="active"><a id="tabPendingPnL" href="#" onclick="activePM1(this)">Pre P&L</a></li>
                                        <li id="PMli2"><a href="#" id="tabManageTeam" onclick="activePM2(this)">Manage Team</a></li>
                                        <li id="PMli3"><a href="#" id="tabTask" onclick="activePM3(this)">Your Tasks</a></li>
                                        <li id="PMli4"><a href="#" id="tabClaim" onclick="activePM4(this)">Claims</a></li>
                                        <li id="PMli5"><a href="#" id="tabPostPnL" onclick="activePM5(this)">Post P&L</a></li>
                                        <%--<li id="PMli6" style="display: none;"><a href="#" >Notifications</a></li>--%>
                                        <li id="PMli6" style="display: block;"><a href="#" onclick="activeKAM1(this)">Notifications</a></li>
                                    </ul>
                                </asp:Panel>

                                <asp:Panel ID="PnlKAM" runat="server">
                                    <ul class="nav nav-tabs">
                                        <li id="KAMli1" style="display: none;"><a href="#" id="tabKAM1" onclick="activeKAM1(this)">Notifications</a></li>
                                        <li id="KAMli2" class="active" style="display: none;"><a href="#" id="tabStatus" onclick="activeKAM2(this)">Status Report</a></li>
                                        <li id="KAMli3"><a href="#" id="tabReport1" onclick="activeKAM3(this)">Overall Project Report</a></li>
                                        <li id="KAMli4" style="display: block;"><a href="#" id="tabReport2" onclick="activeKAM4(this)">Project Tasks</a></li>
                                    </ul>

                                </asp:Panel>


                            </div>
                            <div class="col-lg-12" id="divallreportsearch" runat="server" style="padding:5px 5px;">

                                <div class="col-lg-3">
                                    <asp:DropDownList ID="ddlsearchtype" CssClass="form-control" runat="server" onchange="searchchange()">
                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                        <asp:ListItem Value="JobCardNo">JC Number</asp:ListItem>
                                        <asp:ListItem Value="Client">Client</asp:ListItem>
                                        <asp:ListItem Value="FirstName">KAM</asp:ListItem>
                                        <asp:ListItem Value="JobCardName">Project Name</asp:ListItem>
                                        <asp:ListItem Value="ProjectType">Project Type</asp:ListItem>
                                        <asp:ListItem Value="Status">JC Status</asp:ListItem>
                                        <asp:ListItem Value="profitpercentage">Profit Percentage</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-2">
                                    <asp:DropDownList ID="ddlorder" runat="server" CssClass="form-control">
                                        <asp:ListItem Value=" ">Select </asp:ListItem>
                                        <asp:ListItem Value="asc">Ascending </asp:ListItem>
                                        <asp:ListItem Value="Desc">Descending </asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-2">
                                    <asp:DropDownList ID="ddlstatus" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="N">Open </asp:ListItem>
                                        <asp:ListItem Value="Y">Close </asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <%--<asp:TextBox ID="txtsearch" runat="server" Height="21px" placeholder="Search..."></asp:TextBox>--%>
                                <div class="col-lg-2">
                                    <input type="text" class="form-control " runat="server" id="btnsearch" placeholder="Search...">
                                </div>
                                <div class="col-lg-3">
                                    <%--<input type="text" class="search-wrapperinput" runat="server" id="btnsearch" placeholder="Search...">--%>
                                    <button type="submit" class="btn btn-sm btn-primary" onclick="fillTabKAM3Details()">Click</button>
                                </div>
                            </div>


                            <div id="Docgrid" class="col-lg-12">
                                <span style="font-size: 40px;">Loading...</span>
                            </div>
                        </div>
                        <asp:HiddenField ID="hdnRole" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cpFooter" runat="Server">
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
        }
        function activeTabFM3(id) {
            fillTabFM3Details();
            $('#FMli1').removeClass('active');
            $('#FMli2').removeClass('active');
            $('#FMli3').addClass('active');
            $('#FMli4').removeClass('active');
        }
        function activeTabFM4(id) {
            fillTabFM4Details();
            $('#FMli1').removeClass('active');
            $('#FMli2').removeClass('active');
            $('#FMli3').removeClass('active');
            $('#FMli4').addClass('active');
        }

        function fillTabFM1Details() {
            var datatable = "";
            $.ajax({
                url: "../AjaxCallsBH/AX_Dashboard.aspx?call=1",
                context: document.body,

                success: function (Result) {
                    if (!Result == "") {
                        var jsonstr = JSON.parse(Result);
                        datatable = '<table class="table table-bordered table-hover">'
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
                                datatable += '<td><a href="' + jsonstr.Table[i].link.toString() + '")><span class="btn-small btn-success" >Assign Job Code  </span></a></td>'
                            }
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
        function fillTabFM2Details() {
            var datatable = "";
            $.ajax({
                url: "../AjaxCallsBH/AX_Dashboard.aspx?call=2",
                context: document.body,

                success: function (Result) {
                    if (!Result == "") {
                        var jsonstr = JSON.parse(Result);
                        datatable = '<table class="table table-bordered table-hover">'
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

                            datatable += '<td><a href="JobCardManager.aspx?mode=edit&user=FM&jid=' + jsonstr.Table[i].JobCardID.toString() + '"><span class="btn-small btn-success" value="View">View</span></a></td>'
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
        function fillTabFM3Details() {
            var datatable = "";
            $.ajax({
                url: "../AjaxCallsBH/AX_Dashboard.aspx?call=3",
                context: document.body,

                success: function (Result) {
                    if (!Result == "") {
                        var jsonstr = JSON.parse(Result);
                        datatable = '<table class="table table-bordered table-hover">'
                        datatable += '<thead>'
                        datatable += '<tr>'
                        datatable += '<th>' + 'ID' + '</th>'
                        datatable += '<th>' + 'Job Code' + '</th>'
                        datatable += '<th>' + 'Title' + '</th>'
                        datatable += '<th>' + 'Project Manager' + '</th>'
                        datatable += '<th>' + 'Project Type' + '</th>'
                        datatable += '<th>' + 'Job Invoice' + '</th>'
                        datatable += '<th>' + 'Re-Open Job' + '</th>'
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
                            datatable += '<td><a href="viewJobInvoice.aspx?jid=' + jsonstr.Table[i].JobCardID.toString() + '"><span class="btn-small btn-success" value="View" >View </span></a></td>'
                            datatable += '<td><input type="button" class="btn-small btn-success" value="Re-Open" onclick="return opnejobcard();" /></a></td>'
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
        function fillTabFM4Details() {

        }
        function opnejobcard(jid) {
            var rsul = confirm('Are you sure you want to re-open job ?');
            if (rsul) {
                $.ajax({
                    url: "../AjaxCallsBH/AX_Jobs.aspx?call=3&jid=" + jid,
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
                url: "../AjaxCallsBH/AX_Dashboard.aspx?call=4",
                context: document.body,
                success: function (Result) {
                    res = Result;
                    //if (res == "K") {

                    //    fillTabKAM2Details();
                    //}
                    //if (res == "F") {
                    //    fillTabFM1Details();
                    //}
                    //alert(res);
                    if (res == "R") {
                        div = document.getElementById('<%=divallreportsearch.ClientID%>')
                        //To show
                        //div.style.display = "block";
                        //To hide
                            div.style.display = "none";
                            fillRM();

                        }
                        //if (res == "K") {
                        else {
                            activeKAM3('');

                        //fillTabKAM3Details();
                        //fillTabKAM4Details();
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

            function fillTabPM1Details() {
                var datatable = "";
                $.ajax({
                    url: "../AjaxCallsBH/AX_Dashboard.aspx?call=5", cache: false,
                    context: document.body,

                    success: function (Result) {
                        if (!Result == "") {
                            var jsonstr = JSON.parse(Result);
                            datatable = '<table class="table table-bordered table-hover">'
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
                                    datatable += '<td><a href="' + jsonstr.Table[i].link.toString() + '")><span class="btn-small btn-warning" value="Pending" >Pending </span></a></td>'

                                }
                                else if (jsonstr.Table[i].linkname.toString() == 'Generated') {
                                    datatable += '<td><a href="' + jsonstr.Table[i].link.toString() + '")><span class="btn-small btn-success" value="Generated">Generated </span></a></td>'

                                }

                                //datatable += '<td><a href="JobCardManager.aspx?mode=edit&jid=' + jsonstr.Table[i].JobcardID.toString() + '")>Manage Team</a></td>'
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
            function fillTabPM2Details() {
                var datatable = "";
                $.ajax({
                    url: "../AjaxCallsBH/AX_Dashboard.aspx?call=6",
                    context: document.body,

                    success: function (Result) {
                        if (!Result == "") {
                            var jsonstr = JSON.parse(Result);
                            datatable = '<table class="table table-bordered table-hover">'
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
            function fillTabPM3Details() {
                var datatable = "";
                $.ajax({
                    url: "../AjaxCallsBH/AX_Dashboard.aspx?call=7", cache: false,
                    context: document.body,

                    success: function (Result) {
                        if (!Result == "") {
                            var jsonstr = JSON.parse(Result);
                            datatable = '<table class="table table-bordered table-hover">'
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
            function fillTabPM4Details() {
                var datatable = "";
                $.ajax({
                    url: "../AjaxCallsBH/AX_Dashboard.aspx?call=8", cache: false,
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
            function fillTabPM5Details() {
                var datatable = "";
                $.ajax({
                    url: "../AjaxCallsBH/AX_Dashboard.aspx?call=9", cache: false,
                    context: document.body,

                    success: function (Result) {
                        if (!Result == "") {
                            var jsonstr = JSON.parse(Result);
                            datatable = '<table class="table table-bordered table-hover">'
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
                                    datatable += '<td> <a href="PostPnLManager.aspx?jid=' + jsonstr.Table[i].JobCardID + '"><span class="btn-small btn-success" >Generated</span></a></td>'

                                }
                                else {
                                    datatable += '<td> <a href="PostPnLManager.aspx?jid=' + jsonstr.Table[i].JobCardID + '"><span class="btn-small btn-warning"  >Pending</span></a></td>'
                                }
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
                //$('#divallreportsearch').show();
                div = document.getElementById('<%=divallreportsearch.ClientID%>')
                //To show
                div.style.display = "block";


                fillTabKAM3Details();

                $('#KAMli1').removeClass("active");
                $('#KAMli2').removeClass("active");
                $('#KAMli3').addClass("active");
                $('#KAMli4').removeClass("active");

            }

            function activeKAM4(id) {

                div = document.getElementById('<%=divallreportsearch.ClientID%>')
                //To hide
                div.style.display = "none";

                fillTabKAM4Details();
                $('#KAMli1').removeClass("active");
                $('#KAMli2').removeClass("active");
                $('#KAMli3').removeClass("active");
                $('#KAMli4').addClass("active");
            }

            function fillTabKAM1Details() {
                var datatable = "";
                $.ajax({
                    url: "../AjaxCallsBH/AX_Dashboard.aspx?call=13",
                    context: document.body,

                    success: function (Result) {
                        if (!Result == "") {
                            var jsonstr = JSON.parse(Result);
                            datatable = '<table class="table table-bordered table-hover">'
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
            function fillTabKAM2Details() {
                var datatable = "";
                $.ajax({
                    url: "../AjaxCallsBH/AX_Dashboard.aspx?call=10",
                    context: document.body,

                    success: function (Result) {
                        if (!Result == "") {
                            var jsonstr = JSON.parse(Result);
                            datatable = '<table class="table table-bordered table-hover">'
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
            function fillTabKAM3Details() {

                var datatable = "";
                var Searchtype = $("select[id$='ddlsearchtype'] option:selected").val();
                var ddlorder = $("select[id$='ddlorder'] option:selected").val();
                var ddlstatus = $("select[id$='ddlstatus'] option:selected").val();
                var txtsearch = $('[id$=btnsearch]').val()



                $.ajax({
                    url: "../AjaxCallsBH/AX_Dashboard.aspx?call=14&Searchtype=" + Searchtype + "&ddlorder=" + ddlorder + "&txtsearch=" + txtsearch + "&ddlstatus=" + ddlstatus,
                    context: document.body,

                    success: function (Result) {
                        if (!Result == "") {
                            var jsonstr = JSON.parse(Result);
                            var TotRevenue1 = "0.00";
                            var TotRevenue2 = "0.00";
                            var TotNetRevenue = "0.00";
                            var TotClaims = "0.00";
                            var TotProfit = "0.00";
                            var TotProfitperc = "0.00";

                            datatable = '<table class="table table-bordered table-hover" >'
                            datatable += '<thead >'
                            datatable += '<tr>'
                            datatable += '<th>' + 'ID' + '</th>'
                            datatable += '<th>' + 'JC No' + '</th>'
                            datatable += '<th>' + 'Client' + '</th>'
                            datatable += '<th>' + 'KAM' + '</th>'
                            datatable += '<th>' + 'Project Name' + '</th>'
                            datatable += '<th>' + 'Project Type' + '</th>'
                            datatable += '<th>' + 'Start Date' + '</th>'
                            datatable += '<th>' + 'End Date' + '</th>'
                            datatable += '<th>' + 'Status' + '</th>'
                            datatable += '<th>' + 'Expenses' + '</th>'
                            datatable += '<th>' + 'Agency Fee' + '</th>'
                            datatable += '<th>' + 'Net Revenue' + '</th>'
                            datatable += '<th>' + 'Actual Exp.' + '</th>'
                            datatable += '<th>' + 'Profit' + '</th>'
                            datatable += '<th>' + 'Profit %' + '</th>'
                            //datatable += '<th>' + 'Internal Budget' + '</th>'
                            //datatable += '<th>' + 'Pre P&L %' + '</th>'
                            //datatable += '<th>' + 'Post P&L %' + '</th>'
                            datatable += '</tr>'
                            datatable += '</thead>'

                            datatable += '<tbody id="tblbody" >'

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
                                datatable += '<td align="right">' + jsonstr.Table[i].Revenue1.toString() + '</td>'
                                datatable += '<td align="right">' + jsonstr.Table[i].Revenue2.toString() + '</td>'
                                datatable += '<td align="right">' + jsonstr.Table[i].NetRevenue.toString() + '</td>'
                                datatable += '<td align="right">' + jsonstr.Table[i].Claims.toString() + '</td>'
                                datatable += '<td align="right">' + jsonstr.Table[i].Profit.toString() + '</td>'
                                datatable += '<td align="right">' + jsonstr.Table[i].Profitperc.toString() + '</td>'
                                //datatable += '<td>' + jsonstr.Table[i].PostPnLPer.toString() + '</td>'
                                TotRevenue1 = Number(TotRevenue1) + Number(jsonstr.Table[i].Revenue1.toString())
                                TotRevenue2 = Number(TotRevenue2) + Number(jsonstr.Table[i].Revenue2.toString())
                                TotNetRevenue = Number(TotNetRevenue) + Number(jsonstr.Table[i].NetRevenue.toString())
                                TotClaims = Number(TotClaims) + Number(jsonstr.Table[i].Claims.toString())
                                TotProfit = Number(TotProfit) + Number(jsonstr.Table[i].Profit.toString())
                                TotProfitperc = Number(TotProfitperc) + Number(jsonstr.Table[i].Profitperc.toString())

                                datatable += '</tr>'
                            }
                            //datatable += '</tbody>'
                            //datatable += '</table>'

                            //datatable += '<table class="table table-bordered table-hover">'
                            //datatable += '<tr>'
                            //datatable += '<td>0</td>'
                            //datatable += '<td>' + jsonstr.Table[0].JobCardNo.toString() + '</td>'
                            //datatable += '<td>' + jsonstr.Table[0].Client.toString() + '</td>'
                            //datatable += '<td>' + jsonstr.Table[0].KAM.toString() + '</td>'
                            //datatable += '<td>' + jsonstr.Table[0].JobCardName.toString() + '</td>'
                            //datatable += '<td>' + jsonstr.Table[0].ProjectType.toString() + '</td>'
                            //datatable += '<td>' + jsonstr.Table[0].StartDate.toString() + '</td>'
                            //datatable += '<td>' + jsonstr.Table[0].EndDate.toString() + '</td>'
                            //datatable += '<td>' + jsonstr.Table[0].JobStatus.toString() + '</td>'
                            //datatable += '<td align="right">' + jsonstr.Table[0].Revenue1.toString() + '</td>'
                            //datatable += '<td align="right">' + jsonstr.Table[0].Revenue2.toString() + '</td>'
                            //datatable += '<td align="right">' + jsonstr.Table[0].NetRevenue.toString() + '</td>'
                            //datatable += '<td align="right">' + jsonstr.Table[0].Claims.toString() + '</td>'
                            //datatable += '<td align="right">' + jsonstr.Table[0].Profit.toString() + '</td>'
                            //datatable += '<td align="right">' + jsonstr.Table[0].Profitperc.toString() + '</td>'
                            //datatable += '</tr>'

                            datatable += '<tr style="">'
                            var profitavg = "0.00"
                            profitavg = (TotProfitperc / jsonstr.Table.length)

                            datatable += '<td colspan="9" align="right"><span style="font-weight:bold">Total :</span> </td>'
                            datatable += '<td align="right"><span style="font-weight:bold">' + numDifferentiation(TotRevenue1.toFixed(2)) + '</span></td>'
                            datatable += '<td align="right"><span style="font-weight:bold">' + numDifferentiation(TotRevenue2.toFixed(2)) + '</span></td>'
                            datatable += '<td align="right"><span style="font-weight:bold">' + numDifferentiation(TotNetRevenue.toFixed(2)) + '</span></td>'
                            datatable += '<td align="right"><span style="font-weight:bold">' + numDifferentiation(TotClaims.toFixed(2)) + '</span></td>'
                            datatable += '<td align="right"><span style="font-weight:bold">' + numDifferentiation(TotProfit.toFixed(2)) + '</span></td>'
                            datatable += '<td align="right"><span style="font-weight:bold">' + profitavg.toFixed(2) + '</span></td>'

                            datatable += '</tr>'

                            datatable += '</table><div class="pagination"></div>'
                            //datatable += '</tbody>'
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
            function fillTabKAM4Details() {
                var datatable = "";

                $.ajax({
                    url: "../AjaxCallsBH/AX_Dashboard.aspx?call=20",
                    context: document.body,

                    success: function (Result) {
                        if (!Result == "") {
                            var jsonstr = JSON.parse(Result);
                            datatable = '<table class="table table-bordered table-hover">'
                            datatable += '<thead>'
                            datatable += '<tr>'
                            datatable += '<th>' + 'ID' + '</th>'
                            datatable += '<th>' + 'JC No' + '</th>'
                            datatable += '<th>' + 'Task' + '</th>'
                            datatable += '<th>' + 'Sub Task' + '</th>'
                            datatable += '<th>' + 'Start Date' + '</th>'
                            datatable += '<th>' + 'End Date' + '</th>'
                            datatable += '<th>' + 'Description' + '</th>'
                            datatable += '<th>' + 'Assign From' + '</th>'
                            datatable += '<th>' + 'Assign To' + '</th>'
                            datatable += '<th>' + 'KAM' + '</th>'
                            datatable += '<th>' + 'Qty' + '</th>'
                            datatable += '<th>' + 'Assign Budget' + '</th>'
                            datatable += '<th>' + 'Status' + '</th>'
                            datatable += '<th>' + 'Remarks' + '</th>'
                            datatable += '<th>' + 'Task complete(%)' + '</th>'

                            datatable += '</tr>'
                            datatable += '</thead>'

                            datatable += '<tbody id="tblbody">'
                            for (var i = 0; i < jsonstr.Table.length; i++) {
                                var j = i + 1;
                                datatable += '<tr>'
                                datatable += '<td>' + j + '</td>'
                                datatable += '<td>' + jsonstr.Table[i].JobCardNo.toString() + '</td>'
                                datatable += '<td>' + jsonstr.Table[i].Title.toString() + '</td>'
                                datatable += '<td>' + jsonstr.Table[i].particulars.toString() + '</td>'
                                datatable += '<td>' + jsonstr.Table[i].startdate.toString() + '</td>'
                                datatable += '<td>' + jsonstr.Table[i].enddate.toString() + '</td>'
                                datatable += '<td>' + jsonstr.Table[i].Description.toString() + '</td>'
                                datatable += '<td>' + jsonstr.Table[i].AssignFrom.toString() + '</td>'
                                datatable += '<td>' + jsonstr.Table[i].Name.toString() + '</td>'
                                datatable += '<td>' + jsonstr.Table[i].KAM.toString() + '</td>'
                                datatable += '<td>' + jsonstr.Table[i].Quantity.toString() + '</td>'
                                datatable += '<td>' + jsonstr.Table[i].Expence.toString() + '</td>'
                                datatable += '<td>' + jsonstr.Table[i].status.toString() + '</td>'
                                datatable += '<td>' + jsonstr.Table[i].Remarks.toString() + '</td>'
                                datatable += '<td>' + jsonstr.Table[i].workstatus.toString() + '</td>'
                                datatable += '</tr>'


                            }
                            datatable += '</tbody>'
                            datatable += '</table><div class="pagination" style="border:1px;"></div>'

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
                            document.getElementById('divallreportsearch').innerHTML = "";
                        }
                    }
                });
            }

            //Relationship manager
            function fillRM() {
                var datatable = "";
                $.ajax({
                    url: "../AjaxCallsBH/AX_Dashboard.aspx?call=19",
                    context: document.body,

                    success: function (Result) {
                        if (!Result == "") {
                            var jsonstr = JSON.parse(Result);
                            //datatable='<h5>Client Summary</h5>'
                            datatable += '<div  style="max-height: 420px; overflow: auto; margin-top: 20px;">'

                            datatable += '<table class="table table-bordered table-hover" id="myTable" >'
                            datatable += '<thead>'
                            datatable += '<tr>'
                            datatable += '<th>' + 'JC' + '</th>'
                            datatable += '<th>' + 'Event Name' + '</th>'
                            datatable += '<th style="Width:80px">' + 'Date' + '</th>'
                            //datatable += '<th>' + 'Place' + '</th>'
                            datatable += '<th>' + 'Client' + '</th>'
                            datatable += '<th style="Width:120px">' + 'Concerned Person' + '</th>'
                            datatable += '<th>' + 'Employee' + '</th>'
                            datatable += '<th title="Pre-event /planning experience with Kestone&#39;s Team">' + 'Pre-event...' + '</th>'
                            datatable += '<th title="Experience with the PM, was he/she able to understand the requirement and advice you effectively">' + 'Experience with...' + '</th>'
                            datatable += '<th title="Was the PM able to add value to the project, with some new suggestions">' + 'Was the...' + '</th>'
                            datatable += '<th title="Was the team able to deliver everything as per timelines">' + 'Was the...' + '</th>'
                            datatable += '<th title="Overall delivery experience onsite">' + 'Overall...' + '</th>'
                            datatable += '<th title="Overall concept or approach of team">' + 'Overall con...' + '</th>'
                            datatable += '<th title="To what an extent the designs that came out were satisfactory">' + 'To what...' + '</th>'
                            datatable += '<th title="How was your experience with the CEP team">' + 'How was...' + '</th>'
                            datatable += '<th title="How much would you rate us for on time reporting">' + 'How much...' + '</th>'
                            datatable += '<th title="How much would you rate us on bringing quality attendees">' + 'How much w...' + '</th>'
                            datatable += '<th title="Any other feedback that you would like to give to us regarding this project or otherwise">' + 'Any other...' + '</th>'

                            datatable += '</tr>'
                            datatable += '</thead>'

                            datatable += '<tbody id="tblbody">'

                            for (var i = 0; i < jsonstr.Table.length; i++) {
                                var j = i + 1;
                                var jcrow = 0;
                                var jctable = "";
                                datatable += '<tr>'
                                //datatable += '<td>' + j + '</td>'

                                //datatable += '<td>' + jsonstr.Table[i].JobcardNo.toString() + '</td>'
                                datatable += '<td>' + jsonstr.Table[i].refjcID.toString() + '</td>'
                                datatable += '<td>' + jsonstr.Table[i].JobCardName.toString() + '</td>'
                                datatable += '<td>' + jsonstr.Table[i].ActivityDate.toString() + '</td>'

                                //datatable += '<td></td>'
                                datatable += '<td>' + jsonstr.Table[i].Client.toString() + '</td>'
                                datatable += '<td>' + jsonstr.Table[i].ContactName.toString() + '</td>'
                                datatable += '<td>' + jsonstr.Table[i].Employee.toString() + '</td>'

                                datatable += '<td>' + jsonstr.Table[i].a.toString() + '</td>'

                                datatable += '<td>' + jsonstr.Table[i].b.toString() + '</td>'
                                datatable += '<td>' + jsonstr.Table[i].c.toString() + '</td>'
                                datatable += '<td>' + jsonstr.Table[i].d.toString() + '</td>'
                                datatable += '<td>' + jsonstr.Table[i].e.toString() + '</td>'
                                datatable += '<td>' + jsonstr.Table[i].f.toString() + '</td>'
                                datatable += '<td>' + jsonstr.Table[i].g.toString() + '</td>'
                                datatable += '<td>' + jsonstr.Table[i].h.toString() + '</td>'

                                datatable += '<td>' + jsonstr.Table[i].i.toString() + '</td>'

                                datatable += '<td>' + jsonstr.Table[i].j.toString() + '</td>'
                                // alert(datatable);
                                datatable += '<td>' + jsonstr.Table[i].k.toString() + '</td>'

                                //datatable += '<td><a href="Feedback.aspx?id=' + jsonstr.Table[i].kamid.toString() + '&jc=' + jsonstr.Table[i].JobcardNo.toString() + '&type=K&cat=Events">' + jsonstr.Table[i].KAM.toString() + '</a></td>'
                                //datatable += '<td><a href="Feedback.aspx?id=' + jsonstr.Table[i].pmid.toString() + '&jc=' + jsonstr.Table[i].JobcardNo.toString() + '&type=PM&cat=Events">' + jsonstr.Table[i].ProjectManager.toString() + '</a></td>'
                                //datatable += '<td><a href="Feedback.aspx?id=' + jsonstr.Table[i].TL.toString() + '&jc=' + jsonstr.Table[i].JobcardNo.toString() + '&type=TM&cat=' + jsonstr.Table[i].category.toString() + '">' + jsonstr.Table[i].EventManager.toString() + '</a></td>'
                                //datatable += '<td>' + jsonstr.Table[i].category.toString() + '</td>'

                                datatable += '</tr>'


                            }
                            datatable += '</tbody>'
                            datatable += '</table></div><div class="pagination"></div>'
                            document.getElementById('Docgrid').innerHTML = datatable;
                        }
                        else {
                            document.getElementById('Docgrid').innerHTML = "Data Not Found";

                        }
                    }
                });
            }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("select[id$=ddlstatus]").hide();
            searchchange()
        });

        function searchchange() {
            var searchtype = $("select[id$='ddlsearchtype'] option:selected").val();
            if (searchtype == 'Status') {
                $("select[id$=ddlorder]").hide();
                $("select[id$=ddlstatus]").show();
                $('[id$=btnsearch]').hide();
            }
            else {
                $("select[id$=ddlorder]").show();
                $("select[id$=ddlstatus]").hide();
                $('[id$=btnsearch]').show();
            }

        }

        function numDifferentiation(val) {
            if (val >= 10000000) val = (val / 10000000).toFixed(2) + ' Cr';
            else if (val >= 100000) val = (val / 100000).toFixed(2) + ' Lac';
            else if (val >= 1000) val = (val / 1000).toFixed(2) + ' K';
            return val;
        }
    </script>
</asp:Content>

