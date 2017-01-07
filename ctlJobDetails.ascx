<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlJobDetails.ascx.vb" Inherits="ctlJobDetails" %>
<table width="100%" border="0" align="center" cellpadding="0">
    <tr>
        <td colspan="2">

            <hr />
        </td>
    </tr>
    <tr>
        <td width="100%" valign="top">
            <div style="margin: 0px auto; text-align: center;">

                <a href="#" id="CreateTask" target="_parent">
                    <div style="width: 140px; float: left;">
                        <img src="icons/Claim_reports-128.png" width="100" height="80" style="padding: 10px" />
                        <br />
                        Create Task
                    </div>
                </a>
                <a href="#" id="task" target="_parent">
                    <div style="width: 140px; float: left; text-align: center;">

                        <img src="icons/Claim_reports-128.png" width="100" height="80" style="padding: 10px" />
                        <br />
                        Task List
                    </div>
                </a>
                <!--  <a href="#" id="manageTeam" target="_parent">
                            <div style="width: 140px; float: left; text-align: center;">
                                <img src="icons/Claim_reports-128.png" width="100" height="80" style="padding: 10px" />
                                <br />
                                Manage Team
                            </div>
                        </a>-->
                <a href="#" id="Prepnl">
                    <div style="width: 140px; float: left; text-align: center;">
                        <img src="icons/Claim_reports-128.png" width="100" height="80" style="padding: 10px" />
                        <br />
                        <span id="pre">Pre P&L</span>
                    </div>
                </a>
                <a href="#" id="postpnl" target="_parent">
                    <div style="width: 140px; float: left; text-align: center;">
                        <img src="icons/Claim_reports-128.png" width="100" height="80" style="padding: 10px" />
                        <br />
                        Post P&L
                    </div>
                </a>
                <a href="#" id="Notification" target="_parent">
                    <div style="width: 140px; float: left;">
                        <img src="icons/Claim_reports-128.png" width="100" height="80" style="padding: 10px" />
                        <br />
                        Notification <span id="notcnt"></span>
                    </div>
                </a>
                <a href="#" id="collatral">
                    <div style="width: 140px; float: left;">
                        <img src="icons/Claim_reports-128.png" width="100" height="80" style="padding: 10px" />
                        <br />
                        Collaterals
                    </div>
                </a>
                <!--  <div style="width: 140px; float: left;">
                            <img src="icons/Claim_reports-128.png" width="100" height="80" style="padding: 10px" />
                            <br />
                            Test
                        </div>
                        <div style="width: 140px; float: left;">
                            <img src="icons/Claim_reports-128.png" width="100" height="80" style="padding: 10px" />
                            <br />
                            Test
                        </div>
                        <div style="width: 140px; float: left;">
                            <img src="icons/Claim_reports-128.png" width="100" height="80" style="padding: 10px" />
                            <br />
                            Test
                        </div>-->
            </div>
            <div style="height: 110px;">&nbsp;</div>
            <h4>Task Details</h4>
            <!--<table class="table table-bordered table-hover" style="background-color: white; width: 100%; text-align: center;">
                        <thead>
                            <tr>
                                <td>Total Task</td>
                                <td>Closed</td>
                                <td>Running</td>
                                <td>Action</td>
                            </tr>
                        </thead>
                        <tbody id="tblbody">
                            <tr>
                                <td>5</td>
                                <td>5</td>
                                <td>5</td>
                                <td><a href="#">Create Task</a>| <a href="#">Task List</a></td>
                            </tr>
                        </tbody>
                    </table>-->
            <div id="Taskdetail">
                Loading...
            </div>
            <h4>Pre Pnl Summary</h4>
            <div id="predetail">
                Loading...
            </div>


            <!--<h4>Collaterals</h4>-->
            <table class="table table-bordered table-hover" style="background-color: white; display: none; width: 100%; text-align: center;">
                <thead>
                    <tr>
                        <td>Brief Collaterals</td>
                        <td>Task Collaterals</td>
                        <!-- <td>Running</td>-->
                        <td>Action</td>
                    </tr>
                </thead>
                <tbody id="Tbody2">
                    <tr>
                        <td>10</td>
                        <td>5</td>
                        <!--<td>5</td>-->
                        <td><a href="#">View more...</a></td>
                    </tr>
                </tbody>
            </table>

        </td>
    </tr>
</table>

<script type="text/javascript">
    $(function () {
        fillNotificationcount()
        var jc = GetParameterValues('jc');
        //var id = GetParameterValues('userid');
        //$('#task').hre('<strong>' + name + '</strong>');
        //$("#task").attr("href", "../PMTasklist.aspx?jid=" + jc + "&mode=Task_List");
        $("#task").attr("href", "ListOfTask.aspx?jid=" + jc);
        //$("#CreateTask").attr("href", "../Task.aspx?jid=" + jc + "&mode=add")
        $("#CreateTask").attr("href", "TaskAccount.aspx?tid=" + jc + "&mode=add")
        $("#manageTeam").attr("href", "PMTasklist.aspx?jid=" + jc + "&mode=Manage_Team");
        $("#Notification").attr("href", "PMTasklist.aspx?jid=" + jc + "&mode=Notification");
        $("#postpnl").attr("href", "PostPnLManager.aspx?mode=view&jid=" + jc);
        $("#collatral").attr("href", "Kcollaterals.aspx?jid=" + jc);
        $("#collatral").attr("target", "_parent");
        fillprepnlsummary(jc)
        fillTabPM1Details(jc)
        filltaskdetails(jc)

        //$('#spn_UserId').html('<strong>' + id + '</strong>');
    });
    function GetParameterValues(param) {
        var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
        for (var i = 0; i < url.length; i++) {
            var urlparam = url[i].split('=');
            if (urlparam[0] == param) {
                return urlparam[1];
            }
        }
    }

    function fillTabPM1Details(jc) {
        var datatable = "";
        $.ajax({
            url: "AjaxCalls/PMDetails.aspx?call=18&jid=" + jc, cache: false,
            context: document.body,

            success: function (Result) {
                if (!Result == "") {
                    var jsonstr = JSON.parse(Result);
                    //alert(jsonstr.Table[0].link);
                    $("#Prepnl").attr("href", "../" + jsonstr.Table[0].link.toString());
                    $("#Prepnl").attr("target", "_parent");
                    var start = jsonstr.Table[0].link.toString().charAt(0);
                    //alert(jsonstr.Table[0].link.toString().charAt(0));
                    if (start != "B") {
                        document.getElementById('pre').innerHTML = "Pre P&L"
                        //$("#postpnl").show();
                        if (jsonstr.Table[0].JobStatus.toString() != "Close") {
                            $("#postpnl").hide();
                        }
                        else {
                            $("#postpnl").show();
                        }
                    }
                    else {
                        document.getElementById('pre').innerHTML = "Brief"
                        $('#CreateTask').hide();
                        $("#postpnl").hide();
                    }
                    //alert(jsonstr.Table[0].JobStatus.toString());

                    //if (jsonstr.Table[0].link.toString() != "#") {
                    //    $("#Prepnl").attr("href", "../" + jsonstr.Table[0].link.toString());
                    //    $("#Prepnl").attr("target", "_parent");
                    //    $("#Prepnllink").attr("href", "../" + jsonstr.Table[0].link.toString());
                    //    $("#Prepnllink").attr("target", "_parent");
                    //}
                    //else {
                    //    $("#Prepnl").attr("href", jsonstr.Table[0].link.toString());
                    //    $("#Prepnllink").attr("href", jsonstr.Table[0].link.toString());
                    //}

                    //datatable += '<td><a href="' + jsonstr.Table[i].link.toString() + '")><span class="btn-small btn-success" value="Generated">Generated </span></a></td>'
                }
            }
        });
    }

    function fillNotificationcount() {
        var datatable = "";
        $.ajax({
            url: "../AjaxCalls/PMDetails.aspx?call=14",
            context: document.body,

            success: function (Result) {
                if (!Result == "") {
                    var jsonstr = JSON.parse(Result);
                    for (var i = 0; i < jsonstr.Table.length; i++) {
                        var j = i + 1;
                        datatable += jsonstr.Table[i].cnt.toString()
                    }
                    //$('#gridDiv').html = datatable;
                    if (datatable != "0") {
                        document.getElementById('notcnt').innerHTML = "(" + datatable + ")";
                    }
                    else {
                        document.getElementById('notcnt').innerHTML = " ";
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
                    datatable += '<td>' + 'Total Task' + '</td>'
                    datatable += '<td>' + 'Closed' + '</td>'
                    datatable += '<td>' + 'Running' + '</td>'
                    datatable += '<td>' + 'Action' + '</td>'
                    datatable += '</tr>'
                    datatable += '</thead>'

                    datatable += '<tbody id="tblbody">'
                    for (var i = 0; i < jsonstr.Table.length; i++) {
                        var j = i + 1;
                        datatable += '<tr>'
                        datatable += '<td>' + jsonstr.Table[i].total.toString() + '</td>'
                        datatable += '<td>' + jsonstr.Table[i].Complete.toString() + '</td>'
                        datatable += '<td>' + jsonstr.Table[i].Running.toString() + '</td>'


                        datatable += '<td><a href="../Task.aspx?jid=' + jc + '&mode=add" target="_parent">Create Task</a> | <a href="../ListOfTask.aspx?jid=' + jc + '" target="_parent">Task List</a></td>'
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
                    datatable += '<td>' + 'Budget' + '</td>'
                    //datatable += '<td>' + 'Action' + '</td>'
                    datatable += '</tr>'
                    datatable += '</thead>'

                    datatable += '<tbody id="tblbody">'
                    for (var i = 0; i < jsonstr.Table.length; i++) {
                        var j = i + 1;
                        datatable += '<tr>'
                        datatable += '<td>' + jsonstr.Table[i].category.toString() + '</td>'
                        datatable += '<td>' + jsonstr.Table[i].Budget.toString() + '</td>'

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
</script>
