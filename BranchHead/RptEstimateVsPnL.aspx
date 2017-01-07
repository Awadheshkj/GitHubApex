<%@ Page Title="" Language="VB" MasterPageFile="~/BranchHead/Apex.master" AutoEventWireup="false" CodeFile="RptEstimateVsPnL.aspx.vb" Inherits="RptEstimateVsPnL" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1> PrePnl And PostPnl Report</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">PrePnl And PostPnl Report</li>
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
                        <div id="divError" runat="server" class="ui-state-error ui-corner-all divError" visible="false">
                            <p>
                                <span class="ui-accordion" style="float: left; margin-right: .3em;"></span><strong>Warning: </strong>
                                <asp:Label ID="lblError" runat="server"></asp:Label>
                            </p>
                        </div>
                        <div id="divContent" runat="server" class="divContent">
                           
                            <div id="Docgrid" class="col-lg-12">
                                <div>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="ddlsearchcat" CssClass="form-control input-sm" runat="server">
                                                    <asp:ListItem Value="0">Select Job Status</asp:ListItem>
                                                    <asp:ListItem Value="0"> All</asp:ListItem>
                                                    <asp:ListItem Value="Open"> Open</asp:ListItem>
                                                    <asp:ListItem Value="Closed"> Closed</asp:ListItem>
                                                </asp:DropDownList>

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtfromdate" runat="server" Width="180px" CssClass="form-control input-sm" placeholder="Activity StartDate From"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txttodate" runat="server" Width="180px" CssClass="form-control input-sm" placeholder="Activity StartDate To"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Button runat="server" ID="btnSearch" Text="Search" CssClass="btn btn-info" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <asp:Label ID="Label1" runat="server" Text="" ForeColor="red" Font-Size="12px"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="form-horizontal" role="form">
                                    <div class="row">
                                        <div class="col-xs-12 col-sm-2">
                                        </div>
                                        <div class="col-xs-12 col-sm-2 pull-right text-right">
                                            <asp:Button ID="btnDownload" runat="server" Text="Download to excel" CssClass="btn btn-sm btn-success" Style="position: relative; top: 0px;" />
                                            <%--<asp:Button ID="btnExcel" runat="server" Text="Button" />--%>
                                        </div>

                                    </div>

                                    <br />
                                    <asp:GridView ID="grdDeal" runat="server" AllowPaging="true" PageSize="20" AutoGenerateColumns="false"
                                        ShowHeaderWhenEmpty="true" CssClass="panel-body table table-condensed table-bordered table-hover text-center"
                                        PagerSettings-Mode="NumericFirstLast" PagerSettings-PageButtonCount="10" Width="100%">
                                        <%-- <asp:GridView ID="grdDeal" runat="server" AutoGenerateColumns="false" CssClass="panel-body table table-condensed table-bordered table-hover text-center"
                    PagerSettings-Mode="NumericFirstLast"  Width="100%">--%>
                                        <Columns>
                                            <asp:TemplateField HeaderText="#" ItemStyle-Wrap="true" ItemStyle-Width="1%" HeaderStyle-Width="1%">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1%>
                                                </ItemTemplate>

                                                <HeaderStyle Width="1%"></HeaderStyle>

                                                <ItemStyle Wrap="True" Width="1%"></ItemStyle>
                                            </asp:TemplateField>
                                            <%--<asp:BoundField HeaderText="BPID" DataField="BPID" />--%>
                                            <asp:BoundField HeaderText="JobcardNo" DataField="JobcardNo" HeaderStyle-CssClass="text-center" ItemStyle-Wrap="true" />
                                            <asp:BoundField HeaderText="Client" DataField="Client" HeaderStyle-CssClass="text-center" ItemStyle-Wrap="true" />
                                            <asp:BoundField HeaderText="KAM" DataField="Kam" HeaderStyle-CssClass="text-center" ItemStyle-Wrap="true" />
                                            <asp:BoundField HeaderText="Project Name" DataField="JobcardName" HeaderStyle-CssClass="text-center" ItemStyle-Wrap="true" />
                                            <asp:BoundField HeaderText="Primary Catigory" DataField="ProjectType" HeaderStyle-CssClass="text-center" ItemStyle-Wrap="true" />
                                            <asp:BoundField HeaderText="Sub-Category" DataField="Category" HeaderStyle-CssClass="text-center" ItemStyle-Wrap="true" />
                                            <asp:BoundField HeaderText="Start Date" DataField="StartDate" HeaderStyle-CssClass="text-center" ItemStyle-Wrap="true" />
                                            <asp:BoundField HeaderText="End Date" DataField="EndDate" HeaderStyle-CssClass="text-center" ItemStyle-Wrap="true" />
                                            <asp:BoundField HeaderText="Job Status" DataField="JobStatus" HeaderStyle-CssClass="text-center" ItemStyle-Wrap="true" />
                                            <asp:BoundField HeaderText="Actual/Final Estimate" DataField="NetRevenueactual" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-right" ItemStyle-Wrap="true" />
                                            <asp:BoundField HeaderText="Pre/post PNL" DataField="posteventTotal" HeaderStyle-CssClass="text-center" ItemStyle-Wrap="true" ItemStyle-CssClass="text-right" />

                                        </Columns>
                                        <EmptyDataTemplate>
                                            No record found
                                        </EmptyDataTemplate>

                                        <PagerSettings Mode="NumericFirstLast"></PagerSettings>

                                        <PagerStyle CssClass="gridview panel-footer" />


                                    </asp:GridView>
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

    <script type="text/javascript">
        $(function DatePicker(obj1) {

            $('input[id$=txtfromdate]').datepicker({ format: 'dd-mm-yyyy' });
            $('input[id$=txttodate]').datepicker({ format: 'dd-mm-yyyy' });
        });
    </script>
</asp:Content>
