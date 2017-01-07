function charLimit(el, lbl, maxLength) {

    var warning = document.getElementById(lbl);
    if (el.value.length > maxLength - 1) {
        warning.innerHTML = "Out of Limit";
    }
    else {
        warning.innerHTML = "";
    }
    if (el.value.length > maxLength)

        return false;
    return true;
}
function characterCount(el, maxLength) {

    if (el.value.length > maxLength)
        el.value = el.value.substring(0, maxLength);




    return true;
}
function ConfirmationBox() {

    var result = confirm('Are you sure you want to delete ?');
    if (result) {

        return true;
    }
    else {
        return false;
    }
}

function ConfirmReOpen() {
    var result = confirm('Are you sure you want to re-open job ?');
    if (result) {

        return true;
    }
    else {
        return false;
    }
}
function RejectConfirmationBox() {

    var result = confirm('Are you sure you want to Reject ?');
    if (result) {

        return true;
    }
    else {
        return false;
    }
}
    function CancelConfirmationBox() {

        var result = confirm('Are you sure you want to cancel?');
        if (result) {

            return true;
        }
        else {
            return false;
        }
    }

    function popup(doalogdivId) {

        // get the screen height and width  
        var maskHeight = $(document).height();
        var maskWidth = $(window).width();
        // alert(doalogdivId);

        // calculate the values for center alignment
        var dialogTop = "100px"// (maskHeight / 3) - ($('#dialog-box').height());
        var dialogLeft = (maskWidth / 2) - ($('#' + doalogdivId).width() / 2);

        // assign values to the overlay and dialog box
        //$('#dialog-overlay').css({ height: maskHeight, width: maskWidth }).show();

        $('#' + doalogdivId).css({ top: dialogTop, left: dialogLeft, width: 'auto' }).show();
        $('#' + doalogdivId).draggable();
        // display the message
        //  $('#dialog-message').html(message);

    }


    function ShowMsg(msg) {
        //alert(msg);
        $("#lblMsg").text(msg);
        $("#message-drawer").removeClass("alert-messages hidden").addClass("alert-messages");
        setTimeout(function () { $("#message-drawer").removeClass("alert-messages").addClass("alert-messages hidden"); }, 1000)
    }

    function checknum(obj) {
        var objval = obj.value;

        if (!isNaN(objval)) {
            return true;
        }
        else {
            setTimeout("ShowMsg('Invalid data')", 100)
            $('input[id$=' + obj.id + ']').val("");
            return false;
        }

    }
    function CheckEmail(obj) {
        var emailid = obj.value;
        if (emailid != "") {
            var x = emailid;

            var atpos = x.indexOf("@");
            var dotpos = x.lastIndexOf(".");
            if (atpos < 1 || dotpos < atpos + 2 || dotpos + 2 >= x.length) {
                //  alert("Please enter valid email id");
                setTimeout("ShowMsg('Please enter valid email id')", 100)

                $('input[id$=' + obj.id + ']').val("");
                return false;
            }


        }

    }

    function GetParameterValues(param) {
        var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
        for (var i = 0; i < url.length; i++) {
            var urlparam = url[i].split('=');
            if (urlparam[0] == param) {
                return urlparam[1];
            }
        }
    }
