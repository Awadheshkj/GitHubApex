﻿<%@ Page Title="" Language="VB" MasterPageFile="~/BranchHead/Apex.master" AutoEventWireup="false" CodeFile="Leads.aspx.vb" Inherits="Leads" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!-- DATA TABLES -->
    <link href="../plugins/datatables/dataTables.bootstrap.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Leads
           
        </h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">Leads</li>
        </ol>
    </section>

    <!-- Main content -->
    <section class="content">
        <div class="row">
            <div class="col-xs-12">
                <!-- .box -->
                <div class="box">
                    <div class="box-header">
                        <h3 class="box-title">List Of Leads</h3>
                        <%--<a class="pull-right btn btn-primary btn-sm" href="LeadManager.aspx?mode=add"><i class="fa fa-th"></i>&nbsp;Add Lead</a>--%>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">

                        <table id="example1" class="table table-bordered table-striped">
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
                    <!-- /.box-body -->
                </div>

                <!-- /.box -->
            </div>
            <!-- /.col -->
        </div>
        <!-- /.row -->
    </section>
    <!-- /.content -->
</asp:Content>

<asp:Content ID="content3" runat="server" ContentPlaceHolderID="cpFooter">
    <!-- DATA TABES SCRIPT -->
    <script src="../plugins/datatables/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="../plugins/datatables/dataTables.bootstrap.min.js" type="text/javascript"></script>
    <!-- page script -->


    <script>
        $(document).ready(function () {
            FillLeadDetails();

        });

        function FillLeadDetails(leadconvert) {
            var datatable = "";
            $.ajax({
                url: "../AjaxCallsBH/AX_Leads.aspx?call=1", cache: false,
                context: document.body,

                success: function (Result) {
                    if (!Result == "") {
                        var jsonstr = JSON.parse(Result);
                        datatable = ''
                        datatable += '<thead>'
                        datatable += '<tr>'
                        datatable += '<th>' + 'ID' + '</th>'
                        datatable += '<th>' + 'Job Code' + '</th>'
                        datatable += '<th>' + 'Project Name' + '</th>'
                        datatable += '<th>' + 'KAM' + '</th>'
                        datatable += '<th>' + 'Client' + '</th>'
                        datatable += '<th>' + 'Primary Activity' + '</th>'
                        datatable += '<th>' + 'Sub Activity' + '</th>'
                        //RefActivityTypeID
                        datatable += '<th>' + 'Contact Person' + '</th>'
                        datatable += '<th>' + 'Budget' + '</th>'
                        datatable += '<th>' + 'Brief' + '</th>'
                        datatable += '<th>' + 'Pre P&L' + '</th>'
                        datatable += '<th>' + 'Estimate' + '</th>'
                        //datatable += '<th>' + 'Action' + '</th>'
                        //datatable += '<th>' + 'Transfer PM' + '</th>'
                        //datatable += '<th>' + 'Edit' + '</th>'
                        datatable += '</tr>'
                        datatable += '</thead>'

                        datatable += '<tbody id="tblbody">'
                        for (var i = 0; i < jsonstr.Table.length; i++) {
                            var j = i + 1;
                            datatable += '<tr>'
                            datatable += '<td>' + j + '</td>'

                            datatable += '<td>' + jsonstr.Table[i].JobCardNo.toString() + '</td>'

                            datatable += '<td>' + jsonstr.Table[i].LeadName.toString() + '</td>'

                            datatable += '<td>' + jsonstr.Table[i].KAMName.toString() + '</td>'

                            datatable += '<td>' + jsonstr.Table[i].Client.toString() + '</td>'

                            datatable += '<td>' + jsonstr.Table[i].ActivityType.toString() + '</td>'

                            datatable += '<td>' + jsonstr.Table[i].RefActivityTypeID.toString() + '</td>'

                            datatable += '<td>' + jsonstr.Table[i].ContactPerson.toString() + '</td>'

                            datatable += '<td>' + jsonstr.Table[i].Budget.toString() + '</td>'

                            if (jsonstr.Table[i].Briefed.toString() == "N/A") {
                                datatable += '<td><a id="btnBief" class="btn btn-danger btn-small">N/A</a></td>'
                            }
                            else if (jsonstr.Table[i].Briefed.toString() == "Pending") {
                                if (leadconvert == 1) {
                                    window.location.href = 'BriefManager.aspx?mode=edit&bid=' + jsonstr.Table[i].BriefID.toString();
                                }
                                datatable += '<td><a href=' + jsonstr.Table[i].link_brief.toString() + ' class="btn btn-warning btn-small">P</a></td>'
                            }
                            else {
                                datatable += '<td><a href=' + jsonstr.Table[i].link_brief.toString() + ' class="btn btn-success btn-small">G</a></td>'
                            }

                            if (jsonstr.Table[i].linkname.toString() == "N/A") {
                                datatable += '<td><a class="btn btn-danger btn-small">N/A</a></td>'
                            }
                            else if (jsonstr.Table[i].linkname.toString() == "Pending") {
                                // + jsonstr.Table[i].link.toString() + 
                                datatable += '<td><a href="#" class="btn btn-warning btn-small">P</a></td>'
                            }
                            else {
                                datatable += '<td><a href=' + jsonstr.Table[i].link.toString() + ' class="btn btn-success btn-small">G</a></td>'
                            }


                            if (jsonstr.Table[i].linkname.toString() == "N/A") {
                                datatable += '<td><a id="btnBief" class="btn btn-danger btn-small">N/A</a></td>'
                            }
                            else if (jsonstr.Table[i].linkname.toString() == "Pending") {

                                // datatable += '<td><a href=' + jsonstr.Table[i].linkestimate.toString() + '&mode=Pending id="btnBief" class="btn btn-warning btn-small">Pending</a></td>'
                                //datatable += '<td><a href="estimate.aspx?bid=' + jsonstr.Table[i].briefid.toString() + '&mode=Pending class="btn btn-warning btn-small">Pending</a></td>'
                                datatable += '<td><a id="btnBief" class="btn btn-danger btn-small">N/A</a></td>'
                            }
                            else if (jsonstr.Table[i].linkname2.toString() == "Pending") {

                                datatable += '<td><a href="#" id="btnBief" class="btn btn-warning btn-small">P</a></td>'
                                //' + jsonstr.Table[i].linkestimate.toString() + '&mode=Pending
                                //datatable += '<td><a href="estimate.aspx?bid=' + jsonstr.Table[i].briefid.toString() + '&mode=Pending class="btn btn-warning btn-small">Pending</a></td>'
                                //datatable += '<td><a id="btnBief" class="btn btn-danger btn-small">N/A</a></td>'
                            }
                            else {
                                datatable += '<td><a href=' + jsonstr.Table[i].linkestimate.toString() + '&mode=Genereted id="btnBief" class="btn btn-success btn-small">G</a></td>'
                                //datatable += '<td><a href="openestimate.aspx?bid=' + jsonstr.Table[i].briefid.toString() + '&mode=Genereted id="btnBief" class="btn btn-success btn-small">Generated</a></td>'
                            }

                            //if (jsonstr.Table[i].BriefID.toString() != "") {
                            //    datatable += "<td><span class='badge'>Lead<br/>Converted</label>"


                            //}
                            //else {
                            //    datatable += "<td><input type='button' class='btn btn-primary' id='btnCloseLead' onclick='ConvertLead(" + jsonstr.Table[i].leadID.toString() + ")' value='Convert' />"


                            //}
                            //if (jsonstr.Table[i].Briefed.toString() == "N/A") {
                            //    //   datatable += '<td><a class="btn btn-danger btn-small">N/A</a></td>'
                            //    datatable += '<td>&nbsp;</td>'
                            //}
                            //else if (jsonstr.Table[i].Briefed.toString() == "Pending") {
                            //    datatable += '<td>&nbsp;</td>'
                            //}
                            //else {

                            //    datatable += '<td><a class="btn btn-sm btn-primary" href=changepm.aspx?bid=' + jsonstr.Table[i].BriefID.toString() + ' ><i class="fa fa-suitcase"></a></td>'

                            //}

                            //datatable += '<td><a class="btn btn-sm btn-primary" href="LeadManager.aspx?mode=edit&lid=' + jsonstr.Table[i].leadID.toString() + '"><i class="fa fa-edit"></a></td>'


                            datatable += '</tr>'


                        }
                        datatable += '</tbody>'

                        document.getElementById('example1').innerHTML = datatable;

                        $('#example1').dataTable();
                    }
                    else {
                        document.getElementById('example1').innerHTML = "Data Not Found";
                    }
                }
            });


        }

        function ConvertLead(leadid) {
            if (confirm('Do you want to convert the Lead to Brief?') == true) {
                //alert(dataID);
                $.ajax({
                    url: "../AjaxCallsBH/AX_Leads.aspx?call=2&id=" + leadid,
                    context: document.body,
                    success: function (Result) {
                        //FillLeadDetails(1);

                        var jsonstr = JSON.parse(Result);
                        for (var i = 0; i < jsonstr.Table.length; i++) {
                            var j = i + 1;
                            // window.location.href = link;
                            //briefID
                            window.location.href = 'BriefManager.aspx?mode=edit&bid=' + jsonstr.Table[i].briefID.toString();
                        }
                    }

                });
            }
        }
    </script>


</asp:Content>

