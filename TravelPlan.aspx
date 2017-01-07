<%@ Page Title="" Language="VB" MasterPageFile="~/Apex.master" AutoEventWireup="false" CodeFile="TravelPlan.aspx.vb" Inherits="TravelPlan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!-- DATA TABLES -->
    <link href="plugins/datatables/dataTables.bootstrap.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Travel Plan
        </h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">Travel Plan</li>
        </ol>
    </section>

    <!-- Main content -->
    <section class="content">
        <div class="row">
            <div class="col-xs-12">
                <div class="box">
                    <div class="box-header">
                        <h3 class="box-title">Travel Plan List</h3>

                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                        <div class="table-responsive">
                            <table id="tblTravel" class="table table-bordered table-striped">
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
            </div>
        </div>
    </section>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpfooter" runat="Server">
    <!-- DATA TABES SCRIPT -->
    <script src="plugins/datatables/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="plugins/datatables/dataTables.bootstrap.min.js" type="text/javascript"></script>
    <!-- page script -->
    <script type="text/javascript">

        $(function () {

            $('[data-toggle="popover"]').popover({ trigger: 'hover', html: true })
        })



        $(document).ready(function () {
            FillTravelDetails();

        });

        function FillTravelDetails() {
            var datatable = "";
            $.ajax({
                url: "AjaxCalls/AX_TravelPlan.aspx?call=1", cache: false,
                context: document.body,

                success: function (Result) {
                    if (!Result == "") {
                        var jsonstr = JSON.parse(Result);
                        datatable = ''
                        datatable += '<thead>'
                        datatable += '<tr>'
                        datatable += '<th>Month</th>'
                        datatable += '<th>Client</th>'
                        datatable += '<th>Region</th>'
                        datatable += '<th>Event</th>'
                        datatable += '<th>KAM</th>'
                        datatable += '<th>City</th>'
                        datatable += '<th>Date</th>'
                        datatable += '<th>JC Number</th>'
                        datatable += '<th>Details</th>'
                        datatable += '</tr>'
                        datatable += '</thead>'

                        datatable += '<tbody id="tblbody">'
                        for (var i = 0; i < jsonstr.Table.length; i++) {
                            var j = i + 1;
                            datatable += '<tr>'
                            datatable += '<td>' + jsonstr.Table[i].Month.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].Client.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].Region.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].Event.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].KAM.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].City.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].Date.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].JCNumber.toString() + '</td>'
                            datatable += '<td>' + '<span data-content="Day II :' + jsonstr.Table[i].Day2.toString() + '<br />Day III :' + jsonstr.Table[i].Day3.toString() + '<br />'
                            datatable += '<br />PM :' + jsonstr.Table[i].Day3.toString() + '<br />EM :' + jsonstr.Table[i].Day3.toString() + '<br />Venue :' + jsonstr.Table[i].Day3.toString() + '<br />'
                            datatable += 'Activity Type :' + jsonstr.Table[i].ActivityType.toString() + '<br/>" data-placement="top" data-toggle="popover" data-container="body">'
                            datatable += 'Event Details</span></td>'
                            datatable += '</tr>'

                        }
                        datatable += '</tbody>'
                        //datatable += '</table><div class="pagination"></div>'

                        //$('#gridDiv').html = datatable;
                        document.getElementById('tblTravel').innerHTML = datatable;

                        $("#tblTravel").dataTable();

                    }
                    else {
                        document.getElementById('tblTravel').innerHTML = "Data Not Found";
                    }
                }
            });
        }



    </script>
</asp:Content>
