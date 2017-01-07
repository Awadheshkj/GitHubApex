<%@ Page Title="" Language="VB" MasterPageFile="~/Apex.master" AutoEventWireup="false" CodeFile="VendorContact.aspx.vb" Inherits="Masters_VendorContact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script lang="javascript">
        function call(id) {
            location.href = "VendorContactManager.aspx?mode=edit&lid=" + id;
        }
    </script>
    <!-- DATA TABLES -->
    <link href="plugins/datatables/dataTables.bootstrap.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Vendor Contact Details
        </h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">Vendor Contact Details</li>
        </ol>
    </section>

    <!-- Main content -->
    <section class="content">
        <div class="row">
            <div class="col-xs-12">
                <div class="box">
                    <div class="box-header">
                        <h3 class="box-title">Vendor Contact Details</h3>

                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                          <div class="alert alert-danger" id="divError" runat="server">
                            <strong>Warning: </strong>
                            <asp:Label ID="lblError" runat="server"></asp:Label>

                        </div>

                        <div class="span8" id="divContent" runat="server">
                            <div class="text-right">
                                <div class="control-group">
                                    <div class="btn-group">


                                        <asp:Button ID="btnAdd" runat="server" Text="Add New Vendor Contact" class="btn btn-primary pull-right" PostBackUrl="VendorContactManager.aspx?mode=add" />
                                    </div>
                                </div>
                            </div>
                            <div style="height:20px;">
                                &nbsp;
                            </div>
                            <div id="divgrid" runat="server">
                                <table class="table table-bordered table-striped" id="Docgrid">
                                </table>
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

            fillVendorDetails();
        });

        function fillVendorDetails() {

            var datatable = "";
            $.ajax({
                url: "AjaxCalls/PMDetails.aspx?call=22", cache: false,
                context: document.body,

                success: function (Result) {

                    if (!Result == "") {
                        var jsonstr = JSON.parse(Result);
                        //datatable = '<table class="table table-bordered table-hover">'
                        datatable += '<thead>'
                        datatable += '<tr>'
                        datatable += '<th>' + 'ID' + '</th>'
                        datatable += '<th>' + 'Vendor Number' + '</th>'
                        datatable += '<th>' + 'Category Name' + '</th>'
                        datatable += '<th> ' + 'Vendor Name' + '</th>'
                        datatable += '<th>' + 'Name 2' + '</th>'
                        datatable += '<th>' + 'Address' + '</th>'
                        datatable += '<th>' + 'Address2' + '</th>'
                        datatable += '<th>' + 'Phone No.' + '</th>'
                        datatable += '<th>' + 'Country' + '</th>'
                        datatable += '<th>' + 'State' + '</th>'
                        datatable += '<th>' + 'City' + '</th>'
                        datatable += '<th>' + 'PIN' + '</th>'
                        datatable += '<th>' + 'Credit Term' + '</th>'
                        datatable += '<th>' + 'Contact' + '</th>'
                        datatable += '<th>' + 'Contact2' + '</th>'
                        datatable += '<th>' + 'Email ID' + '</th>'
                        datatable += '<th>' + 'Website URL' + '</th>'
                        datatable += '<th>' + 'P.A.N. No.' + '</th>'
                        datatable += '<th>' + 'Service Tax' + '</th>'
                        datatable += '<th>' + 'TIN/VAT/LST' + '</th>'
                        datatable += '<th>' + 'CIN' + '</th>'
                        datatable += '<th>' + 'Key concern' + '</th>'
                        datatable += '<th>' + 'Remarks' + '</th>'
                        datatable += '<th>' + 'ISactive' + '</th>'
                        datatable += '<th>' + 'Action' + '</th>'
                        datatable += '</tr>'
                        datatable += '</thead>'

                        datatable += '<tbody id="tblbody">'
                        for (var i = 0; i < jsonstr.Table.length; i++) {
                            var j = i + 1;
                            datatable += '<tr>'
                            datatable += '<td>' + j + '</td>'
                            datatable += '<td><a href="VendorContactManager.aspx?mode=edit&lid=' + jsonstr.Table[i].VID.toString() + '">' + jsonstr.Table[i].VendorNumber.toString() + '</a></td>'
                            datatable += '<td>' + jsonstr.Table[i].Category.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].Name.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].Name2.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].Address.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].Address2.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].Phoneno.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].Country.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].State.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].City.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].PIN.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].CreditTeam.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].Contact.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].contact2.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].Email.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].website.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].PANNo.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].ServiceTax.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].TIN.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].CIN.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].KeyConcern.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].Remarks.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].Isactive.toString() + '</td>'

                            //datatable += '<td>' + '<a href="teamSelect.aspx?taid=' + jsonstr.Table[i].AccountID.toString() + '&TL=Y">Manage</a></td>'
                            //datatable += '<td>' + '<a href="Checklist.aspx?tid=' + jsonstr.Table[i].AccountID.toString() + '">Manage</a></td>'


                            //datatable += '<td>' + jsonstr.Table[i].TaskCompleted.toString() + '</td>'
                            datatable += '<td><a href="VendorContactManager.aspx?mode=edit&lid=' + jsonstr.Table[i].VID.toString() + '">Edit</a></td>'
                            datatable += '</tr>'


                        }
                        datatable += '</tbody>'
                        document.getElementById('Docgrid').innerHTML = datatable;
                        $('#Docgrid').dataTable({
                            "scrollY": 200,
                            "scrollX": true
                        });
                    }
                    else {
                        document.getElementById('Docgrid').innerHTML = "Data Not Found";
                    }
                }
            });
        }
    </script>
</asp:Content>
