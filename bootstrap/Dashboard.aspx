﻿<%@ Page Title="" Language="VB" MasterPageFile="~/Apex.master" AutoEventWireup="false" CodeFile="Dashboard.aspx.vb" Inherits="Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <!-- DATA TABLES -->
    <link href="plugins/datatables/dataTables.bootstrap.css" rel="stylesheet" type="text/css" />
    <style>
        .info-box-number {
            font-size: 18pt;
            text-align: center;
        }

        .info-box-text {
            font-weight: bold;
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Dashboard
                    <small>Version 2.0</small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">Dashboard</li>
        </ol>
    </section>

    <!-- Main content -->
    <section class="content">
        <!-- Info boxes -->
        <div class="row" id="CountsRow">
            <div class="col-md-3 col-sm-6 col-xs-12">
                <div class="info-box bg-aqua">
                    <span class="info-box-icon"><i class="fa fa-cart-arrow-down"></i></span>
                    <div class="info-box-content">
                        <span class="info-box-text">Total Jobs</span>
                        <div class="progress">
                            <div class="progress-bar" style="width: 100%"></div>
                        </div>
                        <span class="info-box-number" id="lblTotalJobs">0</span>
                        <%--<div class="progress">
                            <div class="progress-bar" style="width: 100%"></div>
                        </div>
                        <span class="progress-description">Current Month : 09
                  </span>--%>
                    </div>
                    <!-- /.info-box-content -->
                </div>
                <!-- /.info-box -->
            </div>
            <div class="col-md-3 col-sm-6 col-xs-12">
                <div class="info-box bg-yellow">
                    <span class="info-box-icon"><i class="fa fa-gears"></i></span>
                    <div class="info-box-content">
                        <span class="info-box-text">Running Jobs</span>
                        <div class="progress">
                            <div class="progress-bar" style="width: 100%"></div>
                        </div>
                        <span class="info-box-number" id="lblRunningJobs">0</span>
                        <%--<div class="progress">
                            <div class="progress-bar" style="width: 100%"></div>
                        </div>
                        <span class="progress-description">Current Month : 29
                  </span>--%>
                    </div>
                    <!-- /.info-box-content -->
                </div>
                <!-- /.info-box -->
            </div>
            <!-- /.col -->
            <div class="col-md-3 col-sm-6 col-xs-12">
                <div class="info-box bg-green">
                    <span class="info-box-icon"><i class="fa fa-beer"></i></span>
                    <div class="info-box-content">
                        <span class="info-box-text">Closed Jobs</span>
                        <div class="progress">
                            <div class="progress-bar" style="width: 100%"></div>
                        </div>
                        <span class="info-box-number" id="lblClosedJobs">0</span>
                        <%--<div class="progress">
                            <div class="progress-bar" style="width: 100%"></div>
                        </div>
                        <span class="progress-description">Current Month : 21
                  </span>--%>
                    </div>
                    <!-- /.info-box -->
                </div>
            </div>
            <!-- /.col -->
            <!-- fix for small devices only -->
            <div class="clearfix visible-sm-block"></div>

            <!-- /.col -->
            <div class="col-md-3 col-sm-6 col-xs-12">
                <div class="info-box bg-red">
                    <span class="info-box-icon"><i class="fa fa-recycle"></i></span>
                    <div class="info-box-content">
                        <span class="info-box-text">Pipleline Jobs</span>
                        <div class="progress">
                            <div class="progress-bar" style="width: 100%"></div>
                        </div>
                        <span class="info-box-number" id="lblPiplineJobs">0</span>

                    </div>
                    <!-- /.info-box-content -->
                </div>
                <!-- /.info-box -->
            </div>
            <!-- /.col -->
        </div>

        <!-- /.row -->

        <div class="row">
            <div class="col-md-8">
                <div class="box">
                    <div class="box-header with-border">
                        <h3 class="box-title">Report</h3>

                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-12" id="MonthlyRecapColumn">
                                <div class="chart">
                                    <!-- Sales Chart Canvas -->
                                    <!-- <canvas id="salesChart" height="180"></canvas>-->
                                    <div id="chart_div" style="width: 700px; height: 400px;">
                                        <i class="fa fa-spinner fa-pulse"></i>&nbsp;&nbsp;Loading...
                                    </div>
                                </div>
                                <!-- /.chart-responsive -->
                            </div>
                            <!-- /.col -->
                            <div class="col-md-12">
                                <div id="pnlFM">
                                    <ul class="nav nav-tabs">
                                        <li id="FMli1" class="active"><a id="tabFMPendingJob" href="#" onclick="activeTabFM1(this)">Pending For Job Code</a></li>
                                        <li id="FMli2"><a href="#" id="tabFMRunningJob" onclick="activeTabFM2(this)">Running Jobs</a></li>
                                        <li id="FMli3"><a href="#" id="tabFMCompletedJob" onclick="activeTabFM3(this)">Pending For Invoice</a></li>
                                        <li id="FMli5"><a href="#" id="tabFMClosed" onclick="activeTabFM4(this)">Completed Jobs</a></li>
                                        <li id="FMli4" style="display: none;"><a href="#" id="tabFMNotifications" onclick="activeTabFM4(this)">Notifications</a></li>
                                    </ul>
                                    <div class="col-lg-12">
                                        <div class="row">&nbsp;</div>
                                        <table id="DocgridFM1" class="table table-bordered table-striped">
                                            <%--<span style="font-size: 40px;">Loading...</span>--%>
                                        </table>
                                        <table id="DocgridFM2" class="table table-bordered table-striped">
                                            <%--<span style="font-size: 40px;">Loading...</span>--%>
                                        </table>
                                        <table id="DocgridFM3" class="table table-bordered table-striped">
                                            <%--<span style="font-size: 40px;">Loading...</span>--%>
                                        </table>
                                        <table id="DocgridFM4" class="table table-bordered table-striped">
                                            <%--<span style="font-size: 40px;">Loading...</span>--%>
                                        </table>
                                    </div>
                                </div>

                                <div id="pnlPM">
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


                                </div>
                            </div>

                        </div>
                        <!-- /.row -->
                    </div>
                    <!-- ./box-body -->
                    <div class="box-footer">

                        <!-- /.row -->
                    </div>
                    <!-- /.box-footer -->
                </div>
                <!-- /.box -->
            </div>
            <!-- /.col -->
            <div class="col-md-4">
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title">News Updates</h3>

                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                        <ul class="products-list product-list-in-box" id="lblNews">
                            <li class="item">
                                <i class="fa fa-spinner fa-pulse"></i>&nbsp;&nbsp;Loading...
                            </li>
                            <!-- /.item -->

                        </ul>
                    </div>
                    <!-- /.box-body -->
                    <div class="box-footer text-center">
                        <a href="News.aspx" class="uppercase">View All News</a>
                    </div>
                    <!-- /.box-footer -->
                </div>
            </div>
        </div>
        <!-- /.row -->

        <div class="row" id="KAMRow">
            <div id="pnlKAM" >
            </div>

            <div id="pnlkamadmin" class="col-lg-12">
                <div class="box">
                    <div class="box-header with-border">
                        <h3 class="box-title">Client Summary</h3>

                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                        <div class="col-lg-12">
                            <div id="divclient" class="col-lg-12">
                                <i class="fa fa-spinner fa-pulse"></i>&nbsp;&nbsp;Loading...
                            </div>

                        </div>
                    </div>
                </div>

                <div class="col-lg-6" style="display: block;">
                    <div class="box">
                        <div class="box-header with-border">
                            <h3 class="box-title">KAM Summary</h3>

                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <div class="col-lg-12">
                                <div id="divkam" class="col-lg-12">
                                    <i class="fa fa-spinner fa-pulse"></i>&nbsp;&nbsp;Loading...
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="box">
                        <div class="box-header with-border">
                            <h3 class="box-title">Cost centers Summary</h3>

                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <div class="col-lg-12">
                                <div id="divcategory" class="col-lg-12">
                                    <i class="fa fa-spinner fa-pulse"></i>&nbsp;&nbsp;Loading...
                                </div>
                            </div>
                        </div>
                    </div>
                </div>


            </div>
        </div>
        <!-- Main row -->
        <div class="row" id="MapsRow">
            <div class="col-md-6">
                <div id="piechart_3d" style="width: 550px; height: 450px;">
                    <i class="fa fa-spinner fa-pulse"></i>&nbsp;&nbsp;Loading...
                </div>
            </div>
            <div class="col-md-6">
                <div id="series_chart_div" style="width: 550px; height: 450px;">
                    <i class="fa fa-spinner fa-pulse"></i>&nbsp;&nbsp;Loading...
                </div>
            </div>
        </div>
        <!-- /.row -->
    </section>
    <!-- /.content -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpFooter" runat="server">
    <!-- DATA TABES SCRIPT -->
    <script src="plugins/datatables/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="plugins/datatables/dataTables.bootstrap.min.js" type="text/javascript"></script>
    <!-- page script -->
    <script type="text/javascript">

        $(document).ready(function () {
            fillNews();
            getRole();
            

            ClientWiseBarGraph();
            CategoryWisePie();
            ClientWiseBubble();


        });

        function fillNews() {
            var res = "";
            $.ajax({
                url: "AjaxCalls/AX_Dashboard.aspx?call=25",
                context: document.body,
                success: function (Result) {

                    if (!Result == "") {
                        //var jsonstr = JSON.parse(Result);
                        ////datatable = '<table class="table table-bordered table-hover">'

                        //for (var i = 0; i < jsonstr.Table.length; i++) {

                        //    datatable += '<li class="item"><div class="product-img">';
                        //    if (jsonstr.Table[i].Type.toString() == 'Internal') {
                        //        datatable += '<img src="dist/img/newslogo.png" alt="Product Image" />'
                        //    } else {
                        //        datatable += '<img src="dist/img/ExtNews.jpg" alt="Product Image" />'
                        //    }
                        //    datatable += '</div><div class="product-info"><a href="javascript::;" class="product-title">' + jsonstr.Table[i].Title.toString() + '</a>';
                        //    datatable += '<span class="product-description">' + jsonstr.Table[i].Description.toString() + '</span>';
                        //    datatable += '</div></li>';
                        //}

                        $('#lblNews').html(Result);
                    }

                }
            });
        }

        function getRole() {
            var res = "";
            $.ajax({
                url: "AjaxCalls/AX_Dashboard.aspx?call=4",
                context: document.body,
                success: function (Result) {
                    res = Result;

                    if (res == "K" || res == "H") {
                        $('#pnlPM').hide();
                        $('#pnlFM').hide();
                        $('#MapsRow').show();
                        $('#KAMRow').show();

                        fillJobCounts();
                        //fillTabKAM2Details();
                        fillTabClientwiseDetails();
                        fillTabKAMwiseDetails();
                        fillTabCategorywiseDetails();
                    } else if (res == "F") {
                        $('#ContentPlaceHolder1_pnlkamadmin').hide();
                        $('#MonthlyRecapColumn').hide();
                        $('#MapsRow').hide();
                        $('#pnlPM').hide();
                        $('#pnlFM').show();
                        $('#KAMRow').hide();

                        fillJobCounts();
                        fillTabFM1Details();

                    }
                    if (res == "O") {
                        $('#ContentPlaceHolder1_pnlkamadmin').hide();
                        $('#MonthlyRecapColumn').hide();
                        $('#pnlFM').hide();
                        $('#pnlPM').show();
                        $('#MapsRow').hide();
                        $('#KAMRow').hide();

                        fillTabPM1Detailssearch();
                        fillTabPM1Details();


                    } else {
                        $('#ContentPlaceHolder1_pnlkamadmin').hide();
                       
                    }
                }
            });
        }

        function fillJobCounts() {
            var datatable = "";
            $.ajax({
                url: "AjaxCalls/AX_Dashboard.aspx?call=22",
                context: document.body,

                success: function (Result) {
                    if (!Result == "") {
                        var jsonstr = JSON.parse(Result);
                        //datatable = '<table class="table table-bordered table-hover">'

                        for (var i = 0; i < jsonstr.Table.length; i++) {


                            if (i == 0) {
                                $('#lblTotalJobs').html(jsonstr.Table[i].Jobs.toString());
                            } else if (i == 1) {
                                $('#lblRunningJobs').html(jsonstr.Table[i].Jobs.toString());
                            } else if (i == 2) {
                                $('#lblClosedJobs').html(jsonstr.Table[i].Jobs.toString());
                            } else if (i == 3) {
                                $('#lblPiplineJobs').html(jsonstr.Table[i].Jobs.toString());
                            }



                        }


                    }
                    else {
                        //document.getElementById('Docgrid').innerHTML = "Data Not Found";
                    }
                }
            });
        }

        function ClientWiseBarGraph() {

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


                        var dataT = new google.visualization.DataTable();
                        dataT.addColumn('string', 'Category');
                        dataT.addColumn('number', month[new Date().getMonth() - 1]);
                        dataT.addColumn('number', month[new Date().getMonth()]);

                        for (var i = 0; i < jsonstr.Table.length; i++) {
                            dataT.addRow([jsonstr.Table[i].Category.toString(), parseFloat(jsonstr.Table[i].Revinew1.toString()), parseFloat(jsonstr.Table[i].Rev2.toString())]);
                        }

                        // Some raw data (not necessarily accurate)

                        var options = {
                            title: 'Monthly Business Development CostCenter Wise',
                            vAxis: { title: "Amount" },
                            hAxis: { title: "SBU" },
                            seriesType: "bars",
                            series: { 5: { type: "line" } }
                        };

                        var chart = new google.visualization.ComboChart(document.getElementById('chart_div'));
                        chart.draw(dataT, options);

                    }



                }
            });
        }

        function CategoryWisePie() {
            var datatable = "";
            $.ajax({
                url: "AjaxCalls/AX_Dashboard.aspx?call=23",
                context: document.body,

                success: function (Result) {
                    if (!Result == "") {
                        var jsonstr = JSON.parse(Result);


                        var dataP = new google.visualization.DataTable();
                        dataP.addColumn('string', 'Category');
                        dataP.addColumn('number', 'Percentage');

                        for (var i = 0; i < jsonstr.Table.length; i++) {
                            dataP.addRow([jsonstr.Table[i].Category.toString(), parseFloat(jsonstr.Table[i].Percent.toString())]);
                        }

                        // Some raw data (not necessarily accurate)

                        var options = {
                            title: 'Business CostCenter Wise',
                            pieHole: 0.4,
                        };

                        var chart = new google.visualization.PieChart(document.getElementById('piechart_3d'));
                        chart.draw(dataP, options);


                    }



                }
            });
        }

        function ClientWiseBubble() {
            var datatable = "";
            $.ajax({
                url: "AjaxCalls/AX_Dashboard.aspx?call=24",
                context: document.body,

                success: function (Result) {
                    if (!Result == "") {
                        var jsonstr = JSON.parse(Result);


                        var dataB = new google.visualization.DataTable();
                        dataB.addColumn('string', 'Client');
                        dataB.addColumn('number', 'Revenue');
                        dataB.addColumn('number', 'NoOfProjects');

                        for (var i = 0; i < jsonstr.Table.length; i++) {
                            dataB.addRow([jsonstr.Table[i].Client.toString(), parseFloat(jsonstr.Table[i].Revinew.toString()), parseInt(jsonstr.Table[i].projects.toString())]);
                        }

                        // Some raw data (not necessarily accurate)

                        var options = {
                            title: 'Client Wise ',
                            hAxis: { title: 'Revenue(in Cr.)' },
                            vAxis: { title: '# of Projects' },
                            bubble: { textStyle: { fontSize: 11 } }
                        };

                        var chart = new google.visualization.BubbleChart(document.getElementById('series_chart_div'));
                        chart.draw(dataB, options);


                    }



                }
            });
        }

        function numDifferentiationCr(val) {
            val = (val / 10000000).toFixed(2);
            return val;
        }

        function numDifferentiation(val) {
            if (val >= 10000000) val = (val / 10000000).toFixed(2) + ' Cr';
            else if (val >= 100000) val = (val / 100000).toFixed(2) + ' Lac';
            else if (val >= 1000) val = (val / 1000).toFixed(2) + ' K';
            return val;
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
                        datatable = '<table class="table table-bordered table-striped" >'
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
                        datatable = '<table class="table table-bordered table-striped">'
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

                        datatable = '<table class="table table-bordered table-striped">'
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

        function activeTabFM1(id) {
            $('#DocgridFM1_wrapper').show();
            $('#DocgridFM2_wrapper').hide();
            $('#DocgridFM3_wrapper').hide();
            $('#DocgridFM4_wrapper').hide();
            fillTabFM1Details();

            $('#FMli1').addClass('active');
            $('#FMli2').removeClass('active');
            $('#FMli3').removeClass('active');
            $('#FMli4').removeClass('active');
            

        }
        function activeTabFM2(id) {
            $('#DocgridFM1_wrapper').hide();
            $('#DocgridFM2_wrapper').show();
            $('#DocgridFM3_wrapper').hide();
            $('#DocgridFM4_wrapper').hide();
            fillTabFM2Details();
            $('#FMli1').removeClass('active');
            $('#FMli2').addClass('active');
            $('#FMli3').removeClass('active');
            $('#FMli4').removeClass('active');
            $('#FMli5').removeClass('active');
            

        }
        function activeTabFM3(id) {
            $('#DocgridFM1_wrapper').hide();
            $('#DocgridFM2_wrapper').hide();
            $('#DocgridFM3_wrapper').show();
            $('#DocgridFM4_wrapper').hide();
            fillTabFM3Details();
            $('#FMli1').removeClass('active');
            $('#FMli2').removeClass('active');
            $('#FMli3').addClass('active');
            $('#FMli4').removeClass('active');
            $('#FMli5').removeClass('active');
        }
        function activeTabFM4(id) {
            $('#DocgridFM1_wrapper').hide();
            $('#DocgridFM2_wrapper').hide();
            $('#DocgridFM3_wrapper').hide();
            $('#DocgridFM4_wrapper').show();
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
                    alert(1);
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

                        $('#DocgridFM1').show();
                        document.getElementById('DocgridFM1').innerHTML = datatable;
                        $('#DocgridFM1').dataTable();

                    }
                    else {
                        document.getElementById('DocgridFM1').innerHTML = "Data Not Found";
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

                        $('#DocgridFM2').show();
                        document.getElementById('DocgridFM2').innerHTML = datatable;

                        $('#DocgridFM2').dataTable();
                    }
                    else {
                        document.getElementById('DocgridFM2').innerHTML = "Data Not Found";
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

                        $('#DocgridFM3').show();
                        document.getElementById('DocgridFM3').innerHTML = datatable;

                        $('#DocgridFM3').dataTable();
                    }
                    else {
                        document.getElementById('DocgridFM3').innerHTML = "Data Not Found";
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

                        $('#DocgridFM4').show();
                        document.getElementById('DocgridFM4').innerHTML = datatable;
                        $('#DocgridFM4').dataTable();
                    }
                    else {
                        document.getElementById('DocgridFM4').innerHTML = "Data Not Found";
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

                                datatable += '<td ><a href="claimmaster.aspx?jc=' + jsonstr.Table[i].jobCardID.toString() + '&jno=' + jsonstr.Table[i].JobcardNo.toString() + '" Id="JCwise"  target="_parent">' + jsonstr.Table[i].JobcardNo.toString() + '</a></td>'
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

    </script>
</asp:Content>

