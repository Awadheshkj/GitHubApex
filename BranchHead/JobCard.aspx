<%@ Page Title="" Language="VB" MasterPageFile="~/BranchHead/Apex.master" AutoEventWireup="false" CodeFile="JobCard.aspx.vb" Inherits="JobCard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!-- DATA TABLES -->
    <link href="../plugins/datatables/dataTables.bootstrap.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .box1 {
            display: none;
            /*float: left;*/
            /*position: absolute;*/
            /*top: 50px;
            right: 30px;*/
            background: white;
            padding: 5px;
            border: 1px solid black;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Jobs
           
        </h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">Jobs</li>
        </ol>
    </section>

    <!-- Main content -->
    <section class="content">
        <div class="row">
            <div class="col-xs-12">
                <!-- .box -->
                <div class="box">
                    <div class="box-header">
                        <h3 class="box-title">List Of Jobs</h3>

                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">

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
                        <%--  <div id="companyDiv3" class="1123">chander</div>--%>
                        <div id="companyDiv2" style="min-height: 200px;" class="box1">
                            <div id="dvclose" onclick="closediv()" style="float: right; cursor: pointer;">Close X</div>
                            <div style="padding-top: 25px;">
                                <table id="Invoicedetail" class="table table-bordered table-striped">
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
<asp:Content ID="Content3" ContentPlaceHolderID="cpFooter" runat="Server">
    <!-- DATA TABES SCRIPT -->
    <script src="../plugins/datatables/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="../plugins/datatables/dataTables.bootstrap.min.js" type="text/javascript"></script>
    <!-- page script -->
    <script>
        $(document).ready(function () {
            $("#companyDiv2").hide()
            FillLeadDetails();


        });


        function FillLeadDetails() {
            var datatable = "";

            //var Searchtype = $("select[id$='ddlsearchtype'] option:selected").val();
            //var ddlorder = $("select[id$='ddlorder'] option:selected").val();
            //var ddlstatus = $("select[id$='ddlstatus'] option:selected").val();
            //var txtsearch = $('[id$=btnsearch]').val();

            $.ajax({
                url: "../AjaxCallsBH/AX_Jobs.aspx?call=1",
                context: document.body,
                success: function (Result) {
                    if (!Result == "") {
                        var jsonstr = JSON.parse(Result);
                        datatable = ''
                        datatable += '<thead>'
                        datatable += '<tr>'
                        datatable += '<th>' + 'ID' + '</th>'
                        datatable += '<th>' + 'JCID' + '</th>'
                        datatable += '<th>' + 'Job Code' + '</th>'
                        datatable += '<th>' + 'Project Name' + '</th>'
                        datatable += '<th>' + 'Client' + '</th>'
                        datatable += '<th>' + 'Project Activity' + '</th>'
                        datatable += '<th style="width:100px">Sub Activity</th>'
                        datatable += '<th>' + 'Project Manager' + '</th>'
                      
                        datatable += '<th>' + 'Post P&L' + '</th>'
                        //datatable += '<th>' + 'Job Detail' + '</th>'
                        datatable += '<th>' + 'Collateral' + '</th>'
                        datatable += '<th>' + 'Invoice Detail' + '</th>'
                        datatable += '<th>' + 'Job Code Request' + '</th>'
                        datatable += '<th>' + 'Check List' + '</th>'
                        datatable += '</tr>'
                        datatable += '</thead>'

                        datatable += '<tbody id="tblbody">'
                        for (var i = 0; i < jsonstr.Table.length; i++) {
                            var j = i + 1;
                            datatable += '<tr>'
                            datatable += '<td>' + j + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].JobCardID.toString() + '</td>'
                            if (jsonstr.Table[i].JobCardNo.toString() == "N/A") {
                                datatable += '<td><a id="btnBief" href="JobCardManager.aspx?jid=' + jsonstr.Table[i].JobCardID.toString() + '" class="btn btn-danger btn-sm">N/A</a></td>'

                            }
                            else {

                                if (jsonstr.Table[i].JobCardNo.toString().length > 7) {

                                    datatable += '<td><a id="btnBrief" href="JobCardManager.aspx?jid=' + jsonstr.Table[i].JobCardID.toString() + '" class="btn btn-warning btn-sm">' + jsonstr.Table[i].JobCardNo.toString() + '</a> </td>'
                                }
                                else {
                                    datatable += '<td><a id="btnBrief" href="JobCardManager.aspx?jid=' + jsonstr.Table[i].JobCardID.toString() + '" class="btn btn-success btn-sm">' + jsonstr.Table[i].JobCardNo.toString() + '</a> </td>'
                                }
                            }

                            datatable += '<td>' + jsonstr.Table[i].JobCardName.toString() + '</td>'

                            datatable += '<td>' + jsonstr.Table[i].Client.toString() + '</td>'

                            datatable += '<td>' + jsonstr.Table[i].ProjectType.toString() + '</td>'
   datatable += '<td>' + jsonstr.Table[i].RefActivityID.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].ProjectManager.toString() + '</td>'

                            //datatable += '<td>' + jsonstr.Table[i].JobStatus.toString() + '</td>'

                         

                            if (jsonstr.Table[i].PostPnLStatus.toString() == "N/A" || jsonstr.Table[i].JobCardNo.toString() == "N/A") {
                                datatable += '<td><a id="btnBief"><input type="button" class="btn btn-danger btn-sm" value="N/A" /></a></td>'
                            }
                            else if (jsonstr.Table[i].PostPnLStatus.toString() == "Pending") {
                                datatable += '<td><a href="#"><span class="btn btn-sm btn-warning">Pending </span></a></td>'
                            }
                            else {
                                datatable += '<td><a href="PostPnLManager.aspx?jid=' + jsonstr.Table[i].JobCardID.toString() + '&mode=view"><span class="btn btn-sm btn-success"> Generated </span></a></td>'
                            }

                            //datatable += '<td><a href="JobDetail.aspx?jid=' + jsonstr.Table[i].JobCardID.toString() + '">View</a></td>'
                            datatable += '<td><a href="Kcollaterals.aspx?jid=' + jsonstr.Table[i].JobCardID.toString() + '" class="btn btn-sm btn-default"><i class="fa fa-hand-o-right"></i>&nbsp;Show</a></td>'
                            if (jsonstr.Table[i].PostPnLStatus.toString() != "Generated") {
                                datatable += '<td><a id="btnBief"><input type="button" class="btn btn-danger btn-sm" value="N/A" /></a></td>'
                            }
                            else if (jsonstr.Table[i].isinvoiceing.toString() == "N") {
                                //Estimate_VS_Actuals.aspx?jid=
                                //viewJobInvoice.aspx?jid=
                                datatable += '<td><a href="Estimate_VS_Actuals.aspx?jid=' + jsonstr.Table[i].JobCardID.toString() + '"><span class="btn btn-sm btn-warning">Pending </span></a></td>'
                                //if (jsonstr.Table[i].Estimateforinvoicing.toString() == "N") {

                                //    datatable += '<td><a href="Estimate_VS_Actuals.aspx?jid=' + jsonstr.Table[i].JobCardID.toString() + '"><span class="btn btn-small btn-warning">Pending </span></a></td>'
                                //}
                                //else {
                                //    datatable += '<td><a href="viewJobInvoice.aspx?jid=' + jsonstr.Table[i].JobCardID.toString() + '"><span class="btn btn-small btn-warning">Pending </span></a></td>'
                                //}


                            }
                            else {
                                //datatable += '<td><a href="viewJobInvoice.aspx?jid=' + jsonstr.Table[i].JobCardID.toString() + '"><span class="btn btn-small btn-success"> Generated </span></a></td>'
                                datatable += '<td><span class="btn btn-sm btn-success" > Generated </span></td>'
                            }

                            if (jsonstr.Table[i].JobCardNo.toString() == "N/A") {
                                datatable += '<td><a id="btnBief" href="JobCardManager.aspx?jid=' + jsonstr.Table[i].JobCardID.toString() + '" class="btn btn-danger btn-sm">N/A</a></td>'

                            }
                            else if (jsonstr.Table[i].isinvoiceing.toString() == "N") {
                                datatable += '<td><a id="btnBrief" href="JobCardManager.aspx?jid=' + jsonstr.Table[i].JobCardID.toString() + '" class="btn btn-sm btn-warning">Open</a> </td>'

                            }
                            else {

                                //datatable += '<td><a id="btnBrief" href="JobCardManager.aspx?jid=' + jsonstr.Table[i].JobCardID.toString() + '" class="btn btn-success btn-sm">Close</a> </td>'
                                datatable += '<td><a id="btnBrief" href="JCSummary.aspx?jid=' + jsonstr.Table[i].JobCardID.toString() + '" class="btn btn-success btn-sm">Summary</a> </td>'
                            }

                            datatable += '<td><a href="Checklistview.aspx?jid=' + jsonstr.Table[i].JobCardID.toString() + '" class="btn btn-default btn-sm" ><i class="fa fa-eye"></i>&nbsp;View</a></td>'
                            datatable += '</tr>'
                        }


                        datatable += '</tbody>'
                        //$('#gridDiv').html = datatable;
                        document.getElementById('Docgrid').innerHTML = datatable;
                        jt = $('#Docgrid').DataTable({
                            "columnDefs": [{
                                "targets": [1],
                                "visible": false,
                                "searchable": false
                            }]
                        });


                        $('#Docgrid tbody .GenInvoice').on('click', 'span', function (e) {
                            alert(1);
                            var data = jt.row($(this).parents('tr')).data();
                            FillinvoiceDetails(data[1]);
                            //var of = $('.box1').offset();
                            //alert(of.left);
                            //alert(of.top);
                            //$('.box1').attr('left', of.left);
                            //$('.box1').attr('top', of.top);

                            //$('.box1').attr('position', "fixed");
                            //$('.box1').attr('left', "500");
                            //$('.box1').attr('top', "300");

                            $('.box1').css({ position: "absolute", left: e.pageX, top: e.pageY, display: "block" });



                        });




                        //$('#Docgrid tbody .GenInvoice').onclick(function () {
                        //    alert(1);
                        //    var data = jt.row($(this).parents('tr')).data();
                        //    FillinvoiceDetails(data[1]);
                        //});


                    }
                    else {
                        document.getElementById('Docgrid').innerHTML = "Data Not Found";
                    }
                }
            });
        }

        function FillinvoiceDetails(jcid) {

            var datatable1 = "";
            $.ajax({
                url: "../AjaxCallsBH/AX_Jobs.aspx?call=7&jc=" + jcid,
                context: document.body,
                success: function (Result) {

                    if (!Result == "") {
                        var jsonstr1 = JSON.parse(Result);
                        datatable1 += '<thead>'
                        datatable1 += '<tr>'
                        datatable1 += '<th>' + 'ID' + '</th>'
                        datatable1 += '<th>' + 'Grand Total' + '</th>'
                        // datatable1 += '<th><a href="Invoiceing.aspx?jid=' + jcid + '">Create New</a></th>'
                        datatable1 += '</tr>'
                        datatable1 += '</thead>'

                        datatable1 += '<tbody id="tblbody">'
                        for (var i = 0; i < jsonstr1.Table.length; i++) {
                            var j = i + 1;
                            datatable1 += '<tr>'
                            datatable1 += '<td>' + j + '</td>'
                            datatable1 += '<td><a id="btnBrief" target="_Blank" href="ViewJobInvoice.aspx?jid=' + jcid + '&min=' + jsonstr1.Table[i].RefMinTempEstimateID.toString() + '&max=' + jsonstr1.Table[i].RefMaxTempEstimateID.toString() + '">' + jsonstr1.Table[i].GrandTotal.toString() + '</a></td>'
                            datatable1 += '<td><a id="btnBrief1" href="InvoiceingDetails.aspx?jid=' + jcid + '&min=' + jsonstr1.Table[i].RefMinTempEstimateID.toString() + '&max=' + jsonstr1.Table[i].RefMaxTempEstimateID.toString() + '">View Details</a></td>'
                            datatable1 += '</tr>'
                            //' + jsonstr.Table[i].GrandTotal.toString() + '
                        }
                        datatable1 += '</tbody>'

                        //$('#gridDiv').html = datatable;
                        document.getElementById('Invoicedetail').innerHTML = datatable1;
                        //$('#Invoicedetail').dataTable();
                        $("#companyDiv2").show()
                    }
                    else {
                        document.getElementById('Invoicedetail').innerHTML = "Data Not Found";
                        $("#companyDiv2").show()
                    }
                }
            });
        }


        function closediv() {
            $("#companyDiv2").hide()
        }


    </script>

</asp:Content>

