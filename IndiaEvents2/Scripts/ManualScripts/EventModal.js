// Auther   : Sivabalan S
// Date     : 25May2019
// Purpose  : Validate and handle Event modal fields
// !---------------------------------------------------------------------!

$(document).ready(function () {
    function validate(eventDetails, events) {
        var fromDate = dateValidate("#fromdate")
        var toDate = dateValidate("#todate")
        if (eventDetails.EventName == "") {
            $("#eventAlert").html(customAlert("Please Enter Event Name", "danger"));
            return false;
        } else if (eventDetails.EventType == "Select") {
            $("#eventAlert").html(customAlert("Please Enter Event Type", "danger"));
            return false;
        } else if (eventDetails.EventFee == "") {
            $("#eventAlert").html(customAlert("Please Enter Event Fee", "danger"));
            return false;
        } else if (eventDetails.EventFromDate == "") {
            $("#eventAlert").html(customAlert("Please Enter Event From Date", "danger"));
            return false;
        } else if (eventDetails.EventToDate == "") {
            $("#eventAlert").html(customAlert("Please Enter Event To Date", "danger"));
            return false;
        }else if (fromDate != "") {
            $("#eventAlert").html(customAlert(fromDate, "danger"));
            return false;
        } else if (toDate != "") {
            $("#eventAlert").html(customAlert(toDate, "danger"));
            return false;
        } else if (eventDetails.CollegeName == "") {
            $("#eventAlert").html(customAlert("Please Enter College Name", "danger"));
            return false;
        } else if (eventDetails.Department == "") {
            $("#eventAlert").html(customAlert("Please Enter Department Name", "danger"));
            return false;
        } else if (eventDetails.City == "") {
            $("#eventAlert").html(customAlert("Please Enter City", "danger"));
            return false;
        } else if (eventDetails.State == "") {
            $("#eventAlert").html(customAlert("Please Enter State", "danger"));
            return false;
        } else if (eventDetails.Address == "") {
            $("#eventAlert").html(customAlert("Please Enter Route Information", "danger"));
            return false;
        } else {
            if (events.length <= 0) {
                $("#eventAlert").html(customAlert("Atleast Enter one event detail", "danger"));
            } else {
                var poster = $('#poster')[0].files[0];
                if (poster.size > 2000000) {
                    $("#eventAlert").html(customAlert("Max. File size 2Mb", "danger"));
                } else {
                    return true
                }
            }
        }
    }

    function createEvent() {
        //var events = $('#eventssection *').serializeArray();
        var events = [];
        $.each($("#rowSection tr"), function (a, b) {
            var eventName = $($(b).find("#event")[0]).val();
            var personName = $($(b).find("#organisername")[0]).val();
            var personMobile = $($(b).find("#organisermobile")[0]).val();

            if (eventName != "" && personName != "" && personMobile != "") {
                var details = {
                    eventName: eventName,
                    personName: personName,
                    personMobile: personMobile,
                }
                events.push(details);
            }
        });

        var eventDetails = {
            EventName       : $("#eventname").val(),
            EventType       : $("#eventtype :selected").val(),
            EventFee        : $("#eventfee").val(),
            EventFromDate   : $("#fromdate").val(),
            EventToDate     : $("#todate").val(),
            Website         : $("#websiteurl").val(),
            CollegeName     : $("#collegename").val(),
            Department      : $("#department").val(),
            City            : $("#city").val(),
            State           : $("#state").val(),
            Address         : $("#address").val(),
            Events          : events,
            poster          : $('#poster')[0].files[0]
        }

        if (validate(eventDetails, events)) {
            //serverCall('Home/PostEvent', 'POST', { lst: eventDetails, events: JSON.stringify(events) }); // eventDetails);
            serverCall('Home/PostEvent', 'POST', { eventDetails : eventDetails}, "alert");
        }
    }

    function clone() {
        var row = $("#rowSection").clone(true);
        $(row[0]).find("#event").val("");
        $(row[0]).find("#organisername").val("");
        $(row[0]).find("#organisermobile").val("");
        row.appendTo("#eventssection");
    }


    // APP MAIN START HERE
    var eventTypes = serverCall('Home/getEventsTypes', 'GET', '{}', "select", "#eventtype");
    var selector = $("#subEvents, #createEvent");
    $(selector).on("click", function () {
        switch ($(this).attr('id')) {
            case "subEvents":
                clone();
                break;
            case "createEvent":
                createEvent();
                break;
        }
    });

    $('#poster').bind('change', function () {
        var a = (this.files[0].size);
        if (a > 2000000) {
            $("#eventAlert").html(customAlert("Max. File size 2Mb", "danger"));
        };
    });
});