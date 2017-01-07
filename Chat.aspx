<%@ Page Title="" Language="VB" MasterPageFile="~/Apex.master" AutoEventWireup="false" CodeFile="Chat.aspx.vb" Inherits="Chat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Chat</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>

            <li class="active">Chat</li>
        </ol>
    </section>

    <!-- Main content -->
    <section class="content">
        <div class="row">
            <div class="col-lg-xs-12">
                <div class="box box-primary">
                    <div class="box-header">
                    </div>

                    <div class="box-body">


                        <div class="col-12">

                            <div class="col-lg-12" id="divUploads">
                                <iframe src="https://web.flock.co/" class="form-control" width="100%" frameborder="0" style="min-height: 550px;"></iframe>
                            </div>



                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpFooter" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#divchat').hide();
        });
    </script>
</asp:Content>

