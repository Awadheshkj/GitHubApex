<%@ Page Title="" Language="VB" MasterPageFile="~/BranchHead/Apex.master" AutoEventWireup="false" CodeFile="News.aspx.vb" Inherits="News" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <!-- DATA TABLES -->
    <link href="plugins/datatables/dataTables.bootstrap.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>News
        </h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">News</li>
        </ol>
    </section>

    <!-- Main content -->
    <section class="content">
        <div class="row">
            <div class="col-xs-12">
                <!-- .box -->
                <div class="box">
                    <div class="box-header">
                        <h3 class="box-title">List Of News</h3>
                    
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                        <table id="example1" class="table table-bordered table-striped">

                        </table>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpFooter" runat="Server">
      <!-- DATA TABES SCRIPT -->
    <script src="../plugins/datatables/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="../plugins/datatables/dataTables.bootstrap.min.js" type="text/javascript"></script>
    <!-- page script -->

    <script type="text/javascript">
        $(document).ready(function () {
            getNews();
        });

        function getNews() {
            var datatable = "";
            $.ajax({
                url: "../AjaxCalls/PMDetails.aspx?call=21", cache: false,
                context: document.body,

                success: function (Result) {
                    if (!Result == "") {
                     


                        document.getElementById('example1').innerHTML = Result;

                        $('#example1').dataTable();
                    }
                    else {
                        document.getElementById('example1').innerHTML = "Data Not Found";
                    }
                }
            });
        }
    </script>
</asp:Content>

