<%@ Page Title="" Language="VB" MasterPageFile="~/Apex.master" AutoEventWireup="false" CodeFile="ClaimMaster.aspx.vb" Inherits="ClaimMaster" %>

<%-- Add content controls here --%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-lg-12">
        <div class="col-lg-6">
            <h4>Cost Center Wise Budget</h4>
            <div id="predetail">

            </div>
                            <h4>Project Team</h4>
             <div id="teamMembers">

            </div>
        </div>
        <div class="col-lg-6">
            <h4>Task Summary</h4>
            <div id="Taskdetail">
            </div>
            <h4>P&L Summary</h4>
            <div id="JCsummary">
                Loading...
            </div>
            <h4>Estimate Summary</h4>
            <div id="JCsummaryforEstimate">
                Loading...
            </div>
            <h4>
                Profit Percentege
            </h4>
            <div id="JCProfitPercentage">
                Loading...
            </div>
        </div>
    </div>



    <div class="col-lg-12">
        <div class="col-lg-6">
            &nbsp;
        </div>
        <div class="col-lg-6">
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cpFooter" runat="Server">
    <script type="text/javascript">
        $(function () {

            var jc = getenc('jc');
            //alert(jc);
            fillprepnlsummary(jc);
            //fillTabPM1Details(jc)
            filltaskdetails(jc);
            fillJCsummary(jc);
            fillJCsummaryforEstimate(jc);
            JCProfitPercentage(jc);
            fillAddignedSummary(jc);
            //$('#spn_UserId').html('<strong>' + id + '</strong>');
        });
        function getenc(param) {

            var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
            for (var i = 0; i < url.length; i++) {
                var urlparam = url[i].split('=');
                if (urlparam[0] == param) {
                    return urlparam[1];
                }
            }
        }

        function GetParameterValues1(param) {
            var encval = getenc();
            var url = "";
            var datatable = "";
            alert("AjaxCalls/DecriptQuery.aspx?enc=" + encodeURI(encval));
            $.ajax({
                url: "AjaxCalls/DecriptQuery.aspx?enc=" + encodeURI(encval), cache: false,
                context: document.body,

                success: function (Result) {
                    if (!Result == "") {

                        url = Result
                        for (var i = 0; i < url.length; i++) {
                            var urlparam = url[i].split('=');
                            if (urlparam[0] == param) {
                                return urlparam[1];
                            }
                        }
                    }
                }
            });
        }

        function filltaskdetails(jc) {
            var datatable = "";
            $.ajax({
                url: "AjaxCalls/PMDetails.aspx?call=15&jid=" + jc, cache: false,
                context: document.body,

                success: function (Result) {
                    if (!Result == "") {
                        var jsonstr = JSON.parse(Result);
                        // var jsonstr = JSON.parse(Result);
                        datatable = '<table class="table table-bordered table-hover" style="background-color: white; width: 100%; text-align: center;">'
                        datatable += '<thead>'
                        datatable += '<tr>'
                        datatable += '<td>' + 'Task(Your/Total)' + '</td>'
                        datatable += '<td>' + 'Closed Task(Your/Total)' + '</td>'
                        datatable += '<td>' + 'Running Task(Your/Total)' + '</td>'

                        datatable += '</tr>'
                        datatable += '</thead>'

                        datatable += '<tbody id="tblbody">'
                        for (var i = 0; i < jsonstr.Table.length; i++) {
                            var j = i + 1;
                            datatable += '<tr>'
                            datatable += '<td>' + jsonstr.Table[i].Ytotal.toString() + '/' + jsonstr.Table[i].total.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].YComplete.toString() + '/' + jsonstr.Table[i].Complete.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].YRunning.toString() + '/' + jsonstr.Table[i].Running.toString() + '</td>'

                            //alert(datatable);
                            //alert(jc);
                            datatable += '</tr>'


                        }
                        datatable += '</tbody>'
                        datatable += '</table>'

                        //$('#gridDiv').html = datatable;
                        document.getElementById('Taskdetail').innerHTML = datatable;
                        //$('#gridDiv').html = datatable;
                    }
                    else {
                        document.getElementById('Taskdetail').innerHTML = "Data Not Found";
                    }
                }
            });
        }

        function fillprepnlsummary(jc) {
            var datatable = "";
            $.ajax({
                url: "AjaxCalls/PMDetails.aspx?call=16&jid=" + jc, cache: false,
                context: document.body,

                success: function (Result) {
                    if (!Result == "") {
                        var jsonstr = JSON.parse(Result);
                        // var jsonstr = JSON.parse(Result);
                        datatable = '<table class="table table-bordered table-hover" style="background-color: white; width: 100%; text-align: center;">'
                        datatable += '<thead>'
                        datatable += '<tr>'
                        datatable += '<td>' + 'Cost Center' + '</td>'
                        datatable += '<td>' + 'Budget(Rs)' + '</td>'
                        datatable += '<td>' + 'Total Task(Rs)' + '</td>'
                        datatable += '<td>' + 'Total Approved Claims(Rs)' + '</td>'
                        //datatable += '<td>' + 'Action' + '</td>'
                        datatable += '</tr>'
                        datatable += '</thead>'

                        datatable += '<tbody id="tblbody">'
                        for (var i = 0; i < jsonstr.Table.length; i++) {
                            var j = i + 1;
                            datatable += '<tr>'
                            datatable += '<td>' + jsonstr.Table[i].Categoryb.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].Budget.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].task.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].Claimed.toString() + '</td>'


                            //datatable += '<td><a href="#" id="Prepnllink">View more...</a> </td>'
                            //alert(datatable);
                            //alert(jc);
                            datatable += '</tr>'


                        }
                        datatable += '</tbody>'
                        datatable += '</table>'

                        //$('#gridDiv').html = datatable;
                        document.getElementById('predetail').innerHTML = datatable;
                        //$('#gridDiv').html = datatable;
                    }
                    else {
                        document.getElementById('predetail').innerHTML = "Data Not Found";
                    }
                }
            });
        }

        function fillJCsummary(jc) {
            var datatable = "";
            $.ajax({
                url: "AjaxCalls/PMDetails.aspx?call=23&jid=" + jc, cache: false,
                context: document.body,

                success: function (Result) {
                    if (!Result == "") {
                        var jsonstr = JSON.parse(Result);
                        // var jsonstr = JSON.parse(Result);
                        datatable = '<table class="table table-bordered table-hover" style="background-color: white; width: 100%; text-align: center;">'
                        datatable += '<thead>'
                        datatable += '<tr>'
                        datatable += '<td>' + 'Pre P&L(Rs)' + '</td>'
                        datatable += '<td>' + 'Post P&L(Rs)' + '</td>'
                        datatable += '<td>' + 'Difference(Rs)' + '</td>'
                        //datatable += '<td>' + 'Action' + '</td>'
                        datatable += '</tr>'
                        datatable += '</thead>'

                        datatable += '<tbody id="tblbody">'
                        for (var i = 0; i < jsonstr.Table.length; i++) {
                            var j = i + 1;
                            datatable += '<tr>'
                            datatable += '<td>' + jsonstr.Table[i].Prepnl.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].PostPnl.toString() + '</td>'
                            datatable += '<td>' + (jsonstr.Table[i].Prepnl - jsonstr.Table[i].PostPnl).toFixed(2); + '</td>'
                            //datatable += '<td><a href="#" id="Prepnllink">View more...</a> </td>'
                            //alert(datatable);
                            //alert(jc);
                            datatable += '</tr>'


                        }
                        datatable += '</tbody>'
                        datatable += '</table>'

                        //$('#gridDiv').html = datatable;
                        document.getElementById('JCsummary').innerHTML = datatable;
                        //$('#gridDiv').html = datatable;
                    }
                    else {
                        document.getElementById('JCsummary').innerHTML = "Data Not Found";
                    }
                }
            });
        }

        function fillJCsummaryforEstimate(jc) {
            var datatable = "";
            $.ajax({
                url: "AjaxCalls/PMDetails.aspx?call=24&jid=" + jc, cache: false,
                context: document.body,

                success: function (Result) {
                    if (!Result == "") {
                        var jsonstr = JSON.parse(Result);
                        // var jsonstr = JSON.parse(Result);
                        datatable = '<table class="table table-bordered table-hover" style="background-color: white; width: 100%; text-align: center;">'
                        datatable += '<thead>'
                        datatable += '<tr>'
                        datatable += '<td>' + 'Estimate(Rs)' + '</td>'
                        datatable += '<td>' + 'Final Estimate(Rs)' + '</td>'
                        datatable += '<td>' + 'Difference(Rs)' + '</td>'
                        //datatable += '<td>' + 'Action' + '</td>'
                        datatable += '</tr>'
                        datatable += '</thead>'

                        datatable += '<tbody id="tblbody">'
                        for (var i = 0; i < jsonstr.Table.length; i++) {
                            var j = i + 1;
                            datatable += '<tr>'
                            datatable += '<td>' + jsonstr.Table[i].Estimate.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].FinalEstimate.toString() + '</td>'
                            datatable += '<td>' + (jsonstr.Table[i].Estimate - jsonstr.Table[i].FinalEstimate).toFixed(2); + '</td>'
                            //datatable += '<td><a href="#" id="Prepnllink">View more...</a> </td>'
                            //alert(datatable);
                            //alert(jc);
                            datatable += '</tr>'


                        }
                        datatable += '</tbody>'
                        datatable += '</table>'

                        //$('#gridDiv').html = datatable;
                        document.getElementById('JCsummaryforEstimate').innerHTML = datatable;
                        //$('#gridDiv').html = datatable;
                    }
                    else {
                        document.getElementById('JCsummaryforEstimate').innerHTML = "Data Not Found";
                    }
                }
            });
        }

        function JCProfitPercentage(jc) {
            var datatable = "";
            $.ajax({
                url: "AjaxCalls/PMDetails.aspx?call=24&jid=" + jc, cache: false,
                context: document.body,

                success: function (Result) {
                    if (!Result == "") {
                        var jsonstr = JSON.parse(Result);
                        // var jsonstr = JSON.parse(Result);
                        datatable = '<table class="table table-bordered table-hover" style="background-color: white; width: 100%; text-align: center;">'
                        datatable += '<thead>'
                        datatable += '<tr>'
                        datatable += '<td>' + 'Estimate VS Pre P&L' + '</td>'
                        datatable += '<td>' + 'Final Estimate VS Post P&L' + '</td>'
                        datatable += '<td>' + 'Difference' + '</td>'
                        //datatable += '<td>' + 'Action' + '</td>'
                        datatable += '</tr>'
                        datatable += '</thead>'

                        datatable += '<tbody id="tblbody">'
                        for (var i = 0; i < jsonstr.Table.length; i++) {
                            var estimatepr = 0
                            var finalestimateper = 0


                            var j = i + 1;
                            //alert(isNaN((jsonstr.Table[i].Postpnl)));
                            if (isNaN((jsonstr.Table[i].Estimate)) == true) {
                                estimatepr = 0
                            }
                            else {
                                estimatepr = (((jsonstr.Table[i].Estimate - jsonstr.Table[i].Prepnl) / jsonstr.Table[i].Estimate) * 100).toFixed(2)
                            }
                            if (isNaN(((jsonstr.Table[i].FinalEstimate - jsonstr.Table[i].PostPnl) / jsonstr.Table[i].FinalEstimate) * 100) == true) {
                                finalestimateper = 0
                            }
                            else {
                                finalestimateper = (((jsonstr.Table[i].FinalEstimate - jsonstr.Table[i].PostPnl) / jsonstr.Table[i].FinalEstimate) * 100).toFixed(2)
                            }
                            datatable += '<tr>'
                            datatable += '<td>' + estimatepr + '% </td>'
                            datatable += '<td>' + finalestimateper + '% </td>'
                            datatable += '<td>' + (estimatepr - finalestimateper).toFixed(2) + '% </td>'
                            //datatable += '<td><a href="#" id="Prepnllink">View more...</a> </td>'
                            //alert(datatable);
                            //alert(jc);
                            datatable += '</tr>'


                        }
                        datatable += '</tbody>'
                        datatable += '</table>'

                        //$('#gridDiv').html = datatable;
                        document.getElementById('JCProfitPercentage').innerHTML = datatable;
                        //$('#gridDiv').html = datatable;
                    }
                    else {
                        document.getElementById('JCProfitPercentage').innerHTML = "Data Not Found";
                    }
                }
            });
        }

        function fillAddignedSummary(jc) {
            var datatable = "";
            $.ajax({
                url: "AjaxCalls/AX_Dashboard.aspx?call=28&jc=" + jc, cache: false,
                context: document.body,

                success: function (Result) {
                    if (!Result == "") {
                        var jsonstr = JSON.parse(Result);
                        // var jsonstr = JSON.parse(Result);
                        datatable = '<table class="table table-bordered table-hover" style="background-color: white; width: 100%; text-align: center;">'
                        datatable += '<thead>'
                        datatable += '<tr>'
                        datatable += '<td>' + 'Role' + '</td>'
                        datatable += '<td>' + 'Cost Center' + '</td>'
                        datatable += '<td>' + 'Assigned To' + '</td>'
                        datatable += '<td>' + 'Budget(Rs)' + '</td>'
                        datatable += '<td>' + 'Task Count' + '</td>'

                        datatable += '</tr>'
                        datatable += '</thead>'

                        datatable += '<tbody id="tblbody">'
                        for (var i = 0; i < jsonstr.Table.length; i++) {
                            var j = i + 1;
                            datatable += '<tr>'
                            datatable += '<td>' + jsonstr.Table[i].Role.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].category.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].Name + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].Budget.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].cnt.toString() + '</td>'

                            datatable += '</tr>'

                        }
                        datatable += '</tbody>'
                        datatable += '</table>'

                        //$('#btnAssigned').popover({ html: true, content: datatable })
                        document.getElementById('teamMembers').innerHTML = datatable;
                    }
                    else {
                        //$('#btnAssigned').popover({ html: true, content: "Data Not Found" })
                        document.getElementById('teamMembers').innerHTML = "Data Not Found";
                    }
                }
            });
        }

    </script>
</asp:Content>
