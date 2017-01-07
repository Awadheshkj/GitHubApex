<%@ Page Title="" Language="VB" MasterPageFile="~/Apex.master" AutoEventWireup="false" CodeFile="TravelPlanUpload.aspx.vb" Inherits="TravelPlanUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Travel Plan</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="leads.aspx"><i class="fa fa-th"></i>&nbsp;Settings</a></li>
            <li class="active">Travel Plan</li>
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

                        <div style="padding-left: 25px; padding-top: 25px;">
                            <div class="panel panel-info">
                                <div class="panel-heading clearfix">
                                    <div style="width: 250px; float: right;"><a href="Files/Travel%20Plan%20as%20on%20July%2027.xlsx" target="_blank"><b><u>Click here </u></b></a>to Download Excel format.</div>
                                </div>
                                <div class="panel-body">


                                    <asp:FileUpload ID="fileuploadExcel" runat="server" />&nbsp;&nbsp;
           

                            <asp:Label ID="lblFileName" runat="server" Text="" />
                                    <asp:DropDownList ID="ddlSheets" runat="server" Visible="false"
                                        AppendDataBoundItems="true">
                                    </asp:DropDownList>

                                    <br />
                                    <br />
                                    <br />

                                    <div id="" >
                                        <asp:GridView ID="grvExcelData" runat="server" AutoGenerateColumns="true">
                                            <Columns>
                                                <%--<asp:BoundField DataField="TrainingDate" HeaderText="TrainingDate"
                            DataFormatString="{0:dd-MMM-yyyy}"></asp:BoundField>--%>

                                                <asp:BoundField DataField="TicketNo" HeaderText="TicketNo"></asp:BoundField>
                                                <asp:BoundField DataField="Remarks" HeaderText="Remarks"></asp:BoundField>
                                                <asp:BoundField DataField="Reason" HeaderText="Reason"></asp:BoundField>

                                            </Columns>
                                            <HeaderStyle BackColor="#df5015" Font-Bold="true" ForeColor="White" HorizontalAlign="Center" />

                                        </asp:GridView>
                                    </div>

                                </div>
                                <div class="panel-footer">
                                    <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" />
                                    <asp:Button ID="btnImport" runat="server" Visible="false" Text="Import Data" OnClick="Import_Click" />
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
</asp:Content>

