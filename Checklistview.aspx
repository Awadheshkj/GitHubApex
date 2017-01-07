<%@ Page Title="" Language="VB" MasterPageFile="~/Apex.master" AutoEventWireup="false" CodeFile="Checklistview.aspx.vb" Inherits="Checklistview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link href="plugins/datatables/dataTables.bootstrap.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!-- Content Header (Page header) -->
    <section class="content-header">
        <%--  <h1>Leads
           
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
                        <h3 class="box-title">Check List</h3>

                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                        <div class="inner-Content-container">
                            <div class="InnerContentData">
                                <div class="modal fade" id="dialog-box-CheckList">
                                    <div class="modal-dialog">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                                <h4 class="modal-title">Check list History</h4>
                                            </div>
                                            <div class="modal-body">
                                                <div id="statuslist">Loading...</div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-12" style="overflow:auto;">
                                    <table id="Docgrid" class="table table-bordered table-striped">
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

        $(document).ready(function () {
            fillTabKAM4Details()
        });

        function fillTabKAM4Details() {
            var datatable = "";
            var Totassignbudget = "0.00";
            var TotTaskcomplete = "0.00";


            $.ajax({
                url: "AjaxCalls/AX_Dashboard.aspx?call=20&jid=" + GetParameterValues("jid"),
                context: document.body,

                success: function (Result) {
                    if (!Result == "") {

                        var jsonstr = JSON.parse(Result);

                        //datatable = '<table class="table table-bordered table-hover">'
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
                        datatable += '<th>' + 'Collatrals' + '</th>'
                        datatable += '<th>' + 'Claims' + '</th>'
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
                            datatable += '<td align="right">' + jsonstr.Table[i].Expence.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].status.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].Remarks.toString() + '</td>'
                            datatable += '<td align="right">' + jsonstr.Table[i].workstatus.toString() + ' %</td>'

                            datatable += '<td><a class="btn btn-sm btn-default" href="javascript:loadHistory(' + jsonstr.Table[i].AccountID.toString() + ')" >View</a></td>'
                            datatable += '<td><a class="btn btn-sm btn-default" href="#" >' + jsonstr.Table[i].Claims.toString() + '</a></td>'
                            //datatable += '<td><a class="btn btn-sm btn-default" href="PMTasklist.aspx?jid=' + jsonstr.Table[i].RefJobcardID.toString() + '&mode=Claims" >' + jsonstr.Table[i].Claims.toString() + '</a></td>'
                            

                            datatable += '</tr>'

                            Totassignbudget = Number(Totassignbudget) + Number(jsonstr.Table[i].Expence.toString())
                            TotTaskcomplete = Number(TotTaskcomplete) + Number(jsonstr.Table[i].workstatus.toString())
                        }

                        datatable += '<tr style="">'
                        var profitavg = "0.00"
                        profitavg = (TotTaskcomplete / jsonstr.Table.length)
                        datatable += '<td>&nbsp;</td>'
                        datatable += '<td>&nbsp;</td>'
                        datatable += '<td>&nbsp;</td>'
                        datatable += '<td>&nbsp;</td>'
                        datatable += '<td>&nbsp;</td>'
                        datatable += '<td>&nbsp;</td>'
                        datatable += '<td>&nbsp;</td>'
                        datatable += '<td>&nbsp;</td>'
                        datatable += '<td>&nbsp;</td>'
                        datatable += '<td>&nbsp;</td>'
                        datatable += '<td  align="right"><span style="font-weight:bold">Total :</span> </td>'
                        datatable += '<td align="right"><span style="font-weight:bold">' + Totassignbudget.toFixed(2) + '</span></td>'
                        datatable += '<td align="right"><span style="font-weight:bold"></span></td>'
                        datatable += '<td align="right"><span style="font-weight:bold"></span></td>'
                        datatable += '<td align="right"><span style="font-weight:bold">Average : ' + profitavg.toFixed(0) + ' %</span></td>'
                        datatable += '<td align="right"><span style="font-weight:bold"></span></td>'
                        datatable += '<td>&nbsp;</td>'
                        datatable += '</tr>'
                        datatable += '</tbody>'
                        //datatable += '</table><div class="pagination" style="border:1px;"></div>'

                        //$('#gridDiv').html = datatable;

                        document.getElementById('Docgrid').innerHTML = datatable;
                        $('#Docgrid').dataTable({
                            "responsive": true
                        }
                            );
                    }

                    else {
                        document.getElementById('Docgrid').innerHTML = "Data Not Found";

                    }
                }
            });
        }

        function loadHistory(lid) {

            var listid = lid;
            var datatable = "";
            $.ajax({
                url: "AjaxCall/Taskhistory.aspx?lid=" + listid,
                context: document.body,
                success: function (Result) {

                    document.getElementById('statuslist').innerHTML = Result;
                }

            });
            $('#dialog-box-CheckList').modal('show');
        }

    </script>
</asp:Content>

