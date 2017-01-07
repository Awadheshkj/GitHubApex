<%@ Page Title="" Language="VB" MasterPageFile="~/Apex.master" AutoEventWireup="false" CodeFile="ClaimMaster.aspx.vb" Inherits="ClaimMaster" %>

<%-- Add content controls here --%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    </asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-lg-12">
        <div class="col-lg-6">
            <div id="predetail">

            </div>
            </div>
        <div class="col-lg-6">
            <div id="Taskdetail">

            </div>
            </div>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cpFooter" runat="Server"> 
    <script type="text/javascript">
        $(function () {
          
            var jc = GetParameterValues('jc');
            
            fillprepnlsummary(jc)
            //fillTabPM1Details(jc)
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
                        datatable += '<td>' + 'Closed Task' + '</td>'
                        datatable += '<td>' + 'Running Task' + '</td>'
                       
                        datatable += '</tr>'
                        datatable += '</thead>'

                        datatable += '<tbody id="tblbody">'
                        for (var i = 0; i < jsonstr.Table.length; i++) {
                            var j = i + 1;
                            datatable += '<tr>'
                            datatable += '<td>' + jsonstr.Table[i].total.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].Complete.toString() + '</td>'
                            datatable += '<td>' + jsonstr.Table[i].Running.toString() + '</td>'

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
    </asp:Content>