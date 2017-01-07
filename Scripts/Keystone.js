function charLimit(el, lbl, maxLength) {

    var warning = document.getElementById(lbl);
    if (el.value.length > maxLength - 1) {
               warning.innerHTML="Out of Limit";
    }
    else {
        warning.innerHTML="";
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
function RejectConfirmationBox() {

    var result = confirm('Are you sure you want to Reject ?');
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
    $('#dialog-overlay').css({ height: maskHeight, width: maskWidth }).show();
    $('#' + doalogdivId).css({ top: dialogTop, left: dialogLeft, width: 'auto' }).show();

    // display the message
    //  $('#dialog-message').html(message);

}


