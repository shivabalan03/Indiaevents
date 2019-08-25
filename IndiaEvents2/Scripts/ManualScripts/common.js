// Auther   : Sivabalan S
// Date     : 25May2019
// Purpose  : Common functionality as alert and common validations
// !---------------------------------------------------------------------!

toastr.options = {
    "debug": false,
    "positionClass": "toast-top-right",
    "onclick": null,
    "fadeIn": 300,
    "fadeOut": 100,
    "timeOut": 5000,
    "extendedTimeOut": 1000
}

var showToastrs = false;
function customAlert(message, type) {
    if (!showToastrs) {
        if (type == "success")
            toastr.success(message);
        else if (type == "danger")
            toastr.error(message);
    }
}

function dateValidate(id) {
    var dateString = $(id)[0].value;
    var myDate = new Date(dateString);
    var today = new Date();
    if (myDate < today) {
        return customAlert("Please select upcoming date", "danger");
    } else {
        return "";
    }
}

//$(window).click('.eventDetails', function () {
//    $('#eventDetails').modal({ backdrop: 'static', keyboard: false });
//});

//$("#closemodal").on("click", function (event) {
//    $("#eventDetails").modal('hide');
//    event.stopPropagation();
//});

//$(".showEvents").click(function () {
//    alert('here');
//});

$('#eventDetails').on('shown.bs.modal', function (e) {
    var eventID = $()
});

function validateUser(username) {
    $.ajax({
        url: '/Home/validateUser',
        type: "GET",
        dataType:"json",
        headers: {
            "Authorization" : "Basic "+ btoa(username)
        },
        success: function (result) {
            if (result) {
                $("#modalEventDetails").modal("show");
            } else {
                $("#alert").html(customAlert("Please login", "danger"));
                $('#LoginModal').modal('show');
                $("#signInReset").trigger("click");
                $("#RegisterReset").trigger("click");
            }
        },
        error: function (err) {
            alert(err.statusText);
        }
    });
}

$('#EventModal').click(function () {
    var userName = window.localStorage.getItem("userName");
    if (userName != null) {
        validateUser(userName);
    } else {

    }
});



function bindEvent(imgAttr, eventName, link1, link2, link3, eventID) {
    var image = "<img class='card-img-top eventDetails' src='" + imgAttr + "'  height='150px' width='235px' />"
    if (imgAttr == null) {
        image = "<img class='card-img-top eventDetails' src='/Content/img/defaultEvent.png' height='150px' width='235px'/>"
    }
    //<img class="card-img-top" src="' + imgAttr + '" alt="Card image cap" />
    return '<div class="col-md-3"><div class="card card-cascade narrower mb-4"><div class="view view-cascade overlay">' + image +
        '<a><div class="mask rgba-white-slight waves-effect waves-light"></div></a></div><div class="card-body card-body-cascade"><h5 class="pink-text pb-2 pt-1"> "'
        + eventName + '"</h5><button type="button" class="btn btn-sm btn-info showEvents" data-eventID="' + eventID + '"  data-toggle="modal" data-target="#eventDetails" value="test">Info</button></div></div></div>';
}

function storeImage(eventID) {
    var fileUpload = $("#poster").get(0);
    var files = fileUpload.files;

    // Create FormData object  
    var fileData = new FormData();

    // Looping over all files and add it to FormData object  
    for (var i = 0; i < files.length; i++) {
        fileData.append(files[i].name, files[i]);
    }
    //fileData.append("eventID", eventID);
    $.ajax({
        url: '/Home/StoreFiles?eventID=' + eventID,
        type: "POST",
        contentType: false, // Not to set any content header  
        processData: false, // Not to process data  
        data: fileData,
        success: function (result) {
            alert(result);
        },
        error: function (err) {
            alert(err.statusText);
        }
    });
}



function serverCall(url, type, obj, bindType, id) {
    $.ajax({
        url: url,
        type: type,
        data: JSON.stringify(obj),
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (result) {
            return result;
        },
        complete: function (result) {
            if (bindType == "select") {
                if (id == "#eventtype") {
                    var dropDown = "<option value='Select'>Select</option>";
                    $.each(result.responseJSON, function (a, b) {
                        dropDown += "<option value='" + b.lookups + "'>" + b.lookups + "</option>"
                    });
                    $(id).html(dropDown);
                }
            } else if (bindType == "alert") {
                if (result.responseJSON.message == "Logged In successfully..!") {
                    $("#alert").html(customAlert(result.responseJSON.message, "success"));
                } else if (result.responseJSON.message == "Logout successfully..!") {
                    $("#alert").html(customAlert(result.responseJSON.message, "success"));
                } else if (result.responseJSON.message == "Event Create Successfully..!") {
                    $("#alert").html(customAlert(result.responseJSON.message, "success"));
                    storeImage(result.responseJSON.eventID);
                } else if (result.responseJSON.message == "Please check your credentials..!") {
                    $("#alert").html(customAlert(result.responseJSON.message, "danger"));
                }
                $("#loginInfo").html(result.responseJSON.htmlContent);
            } else if (bindType == "showEvents") {
                var eventHTML = "";
                //if (result.length > 0) {
                $.each(result.responseJSON, function (a, b) {
                    //var imag = b.Poster;
                    //if (b.Poster != "") {
                    //    $("#divImageHolder").html(imag)
                    //}
                    eventHTML += bindEvent(b.Posters, b.EventName, b.Website, "", "", b.EventID)
                    });
                    $("#eventsDetails").html(eventHTML);
                //}
            } 
            return result;
        },
        error: function (xhr) {
            $("#alert").html(customAlert(xhr.statusText, "danger"));
        }
    });
}