<%@ Page Title="" Language="VB" MasterPageFile="~/Apex.master" AutoEventWireup="false" CodeFile="Notifications.aspx.vb" Inherits="Notifications" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <!-- DATA TABLES -->
    <link href="plugins/datatables/dataTables.bootstrap.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <!-- Content Header (Page header) -->
                    <section class="content-header">
                        <h1>Notifications
                        </h1>
                        <ol class="breadcrumb">
                            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
                            <li class="active">Notifications</li>
                        </ol>
                    </section>

    <!-- Main content -->
    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <div class="box">
                    <div class="box-header">
                        <h3 class="box-title">List Of Notifications</h3>
                        
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                        <table id="example1" class="table table-bordered table-striped">
                            <thead>
                                <tr>
                                    <th>Title</th>
                                    <th>Project Name</th>
                                    <th>Project Manager</th>
                                    <th>Date</th>
                                    <th>Status</th>
                                    
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>Trident</td>
                                    <td>Internet
                          Explorer 4.0</td>
                                    <td>PM2</td>
                                    <td>2015-06-23</td>
                                    <td>OPEN</td>  
                                </tr>
                               <tr>
                                    <td>Trident</td>
                                    <td>Internet
                          Explorer 4.0</td>
                                    <td>PM2</td>
                                    <td>2015-06-23</td>
                                    <td><span class="btn btn-success btn-sm">OPEN</span></td>  
                                </tr>
                                <tr>
                                    <td>Trident</td>
                                    <td>Internet
                          Explorer 4.0</td>
                                    <td>PM2</td>
                                    <td>2015-06-23</td>
                                    <td><span class="btn btn-danger btn-sm">CLOSE</span></td>  
                                </tr>
                                <tr>
                                    <td>Trident</td>
                                    <td>Internet
                          Explorer 4.0</td>
                                    <td>PM2</td>
                                    <td>2015-06-23</td>
                                    <td>OPEN</td>  
                                </tr>
                                <tr>
                                    <td>Trident</td>
                                    <td>Internet
                          Explorer 4.0</td>
                                    <td>PM2</td>
                                    <td>2015-06-23</td>
                                    <td>OPEN</td>  
                                </tr>
                                <tr>
                                    <td>Trident</td>
                                    <td>Internet
                          Explorer 4.0</td>
                                    <td>PM2</td>
                                    <td>2015-06-23</td>
                                    <td>OPEN</td>  
                                </tr>
                                <tr>
                                    <td>Trident</td>
                                    <td>Internet
                          Explorer 4.0</td>
                                    <td>PM2</td>
                                    <td>2015-06-23</td>
                                    <td>OPEN</td>  
                                </tr>
                               <tr>
                                    <td>Trident</td>
                                    <td>Internet
                          Explorer 4.0</td>
                                    <td>PM2</td>
                                    <td>2015-06-23</td>
                                    <td>OPEN</td>  
                                </tr>
                                <tr>
                                    <td>Trident</td>
                                    <td>Internet
                          Explorer 4.0</td>
                                    <td>PM2</td>
                                    <td>2015-06-23</td>
                                    <td>OPEN</td>  
                                </tr>
                                <tr>
                                    <td>Trident</td>
                                    <td>Internet
                          Explorer 4.0</td>
                                    <td>PM2</td>
                                    <td>2015-06-23</td>
                                    <td>OPEN</td>  
                                </tr>
                                <tr>
                                    <td>Trident</td>
                                    <td>Internet
                          Explorer 4.0</td>
                                    <td>PM2</td>
                                    <td>2015-06-23</td>
                                    <td>OPEN</td>  
                                </tr>
                                <tr>
                                    <td>Trident</td>
                                    <td>Internet
                          Explorer 4.0</td>
                                    <td>PM2</td>
                                    <td>2015-06-23</td>
                                    <td>OPEN</td>  
                                </tr>
                            </tbody>
                           
                        </table>
                    </div>
                    <!-- /.box-body -->
                </div>
            </div>

        </div>
        </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpFooter" Runat="Server">
     <!-- DATA TABES SCRIPT -->
    <script src="plugins/datatables/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="plugins/datatables/dataTables.bootstrap.min.js" type="text/javascript"></script>
    <!-- page script -->
    <script type="text/javascript">
        $(function () {
            $("#example1").dataTable();
            $('#example2').dataTable({
                "bPaginate": true,
                "bLengthChange": false,
                "bFilter": false,
                "bSort": true,
                "bInfo": true,
                "bAutoWidth": false
            });
        });
    </script>

</asp:Content>

