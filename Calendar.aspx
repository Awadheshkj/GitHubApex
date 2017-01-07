<%@ Page Title="" Language="VB" MasterPageFile="~/Apex.master" AutoEventWireup="false" CodeFile="Calendar.aspx.vb" Inherits="Calendar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!-- fullCalendar 2.2.5-->
    <link href="plugins/fullcalendar/fullcalendar.min.css" rel="stylesheet" type="text/css" />
    <link href="plugins/fullcalendar/fullcalendar.print.css" rel="stylesheet" type="text/css" media='print' />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Schedule Calendar
        </h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">Leads</li>
        </ol>
    </section>

    <!-- Main content -->
    <section class="content">
        <div class="row">
            <div class="col-xs-12">
                <div class="box box-primary">
                    <div class="box-body no-padding">
                        <!-- THE CALENDAR -->
                        <div id="calendar"></div>
                    </div>
                    <!-- /.box-body -->
                </div>
                <!-- /. box -->

            </div>
        </div>
    </section>



</asp:Content>

<asp:Content ID="content3" runat="server" ContentPlaceHolderID="cpFooter">
    <!-- fullCalendar 2.2.5 -->
    <script src="dist/js/moment.min.js" type="text/javascript"></script>
    <script src="plugins/fullcalendar/fullcalendar.min.js" type="text/javascript"></script>
    <!-- Page specific script -->
    <script type="text/javascript">
        $(document).ready(function () {
            var _events = "[";

            $.ajax({
                url: "AjaxCalls/TravelPlanCalendar.aspx?call=1", cache: false,
                context: document.body,

                success: function (Result) {
                    if (!Result == "") {
                        var jsonstr = JSON.parse(Result);
                        for (var i = 0; i < jsonstr.Table.length; i++) {
                            
                            if (jsonstr.Table[i].EventDays.toString() == "1") {
                                if (_events != '[') {
                                    _events += ',';
                                }
                           
                                _events += '{';
                                _events += '"title": "' + jsonstr.Table[i].Title.toString().replace("/", "or") + '",';
                                _events += '"start": "' + new Date(parseInt(jsonstr.Table[i].Y1.toString()) + ', ' + parseInt(jsonstr.Table[i].M1.toString()) + ', ' + parseInt(jsonstr.Table[i].D1.toString()) + '') + '",';
                                
                                _events += '"backgroundColor": "#f56954",';
                                _events += '"borderColor": "#f56954"';
                                _events += '}';
                               
                            }
                            else if (jsonstr.Table[i].EventDays.toString() == "2") {
                                if (_events != '[') {
                                    _events += ',';
                                }
                                _events += '{';
                                _events += '"title": "' + jsonstr.Table[i].Title.toString().replace("/", "or") + '",';
                                _events += '"start": "' + new Date(parseInt(jsonstr.Table[i].Y1.toString()) + ', ' + parseInt(jsonstr.Table[i].M1.toString()) + ', ' + parseInt(jsonstr.Table[i].D1.toString())) + '",';
                                _events += '"end": "' + new Date(parseInt(jsonstr.Table[i].Y2.toString()) + ', ' + parseInt(jsonstr.Table[i].M2.toString()) + ', ' + parseInt(jsonstr.Table[i].D2.toString())) + '",';

                                _events += '"backgroundColor": "#00b200",';
                                _events += '"borderColor": "#00b200"';
                                _events += '}';
                            }
                        };
                        _events += ']';

                        //alert(_events);
                        /* initialize the external events
            -----------------------------------------------------------------*/
                        function ini_events(ele) {
                            ele.each(function () {

                                // create an Event Object (http://arshaw.com/fullcalendar/docs/event_data/Event_Object/)
                                // it doesn't need to have a start or end
                                var eventObject = {
                                    title: $.trim($(this).text()) // use the element's text as the event title
                                };

                                // store the Event Object in the DOM element so we can get to it later
                                $(this).data('eventObject', eventObject);

                                // make the event draggable using jQuery UI
                                $(this).draggable({
                                    zIndex: 1070,
                                    revert: true, // will cause the event to go back to its
                                    revertDuration: 0  //  original position after the drag
                                });

                            });
                        }
                        ini_events($('#external-events div.external-event'));

                        /* initialize the calendar
                         -----------------------------------------------------------------*/
                        //Date for the calendar events (dummy data)
                        var date = new Date();
                        var d = date.getDate(),
                                m = date.getMonth(),
                                y = date.getFullYear();
                        $('#calendar').fullCalendar({
                            header: {
                                left: 'prev,next today',
                                center: 'title',
                                right: 'month,agendaWeek,agendaDay'
                            },
                            buttonText: {
                                today: 'today',
                                month: 'month',
                                week: 'week',
                                day: 'day'
                            },
                            //Random default events
                            events:
                             JSON.parse(_events)
                            ,
                          
                            //JSON.parse('[' + _events + ']'),

                            editable: false,
                            droppable: true, // this allows things to be dropped onto the calendar !!!
                            drop: function (date, allDay) { // this function is called when something is dropped

                                // retrieve the dropped element's stored Event Object
                                var originalEventObject = $(this).data('eventObject');

                                // we need to copy it, so that multiple events don't have a reference to the same object
                                var copiedEventObject = $.extend({}, originalEventObject);

                                // assign it the date that was reported
                                copiedEventObject.start = date;
                                copiedEventObject.allDay = allDay;
                                copiedEventObject.backgroundColor = $(this).css("background-color");
                                copiedEventObject.borderColor = $(this).css("border-color");

                                // render the event on the calendar
                                // the last `true` argument determines if the event "sticks" (http://arshaw.com/fullcalendar/docs/event_rendering/renderEvent/)
                                $('#calendar').fullCalendar('renderEvent', copiedEventObject, true);

                                // is the "remove after drop" checkbox checked?
                                if ($('#drop-remove').is(':checked')) {
                                    // if so, remove the element from the "Draggable Events" list
                                    $(this).remove();
                                }

                            }
                        });

                        ///* ADDING EVENTS */
                        //var currColor = "#3c8dbc"; //Red by default
                        ////Color chooser button
                        //var colorChooser = $("#color-chooser-btn");
                        //$("#color-chooser > li > a").click(function (e) {
                        //    e.preventDefault();
                        //    //Save color
                        //    currColor = $(this).css("color");
                        //    //Add color effect to button
                        //    $('#add-new-event').css({ "background-color": currColor, "border-color": currColor });
                        //});
                        //$("#add-new-event").click(function (e) {
                        //    e.preventDefault();
                        //    //Get value and make sure it is not null
                        //    var val = $("#new-event").val();
                        //    if (val.length == 0) {
                        //        return;
                        //    }

                        //    //Create events
                        //    var event = $("<div />");
                        //    event.css({ "background-color": currColor, "border-color": currColor, "color": "#fff" }).addClass("external-event");
                        //    event.html(val);
                        //    $('#external-events').prepend(event);

                        //    //Add draggable funtionality
                        //    ini_events(event);

                        //    //Remove event from text input
                        //    $("#new-event").val("");
                        //});
                    }
                }
            });
        });



    </script>

</asp:Content>

