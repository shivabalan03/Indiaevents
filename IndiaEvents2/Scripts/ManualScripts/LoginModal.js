// Auther   : Sivabalan S
// Date     : 25May2019
// Purpose  : Validate and handle Login modal fields
// !---------------------------------------------------------------------!

$(document).ready(function () {
    //Login
    function Login() {
        var userName = $("#username").val();
        var password = $("#password").val();
        if (userName == "") {
            $("#alert").html(customAlert("Please Enter User Name", "danger"));
        } else if (password == "") {
            $("#alert").html(customAlert("Please Enter Password", "danger"));
        } else {
            var obj = {
                userName : userName,
                password : password
            }
            serverCall('Home/Login', 'POST', obj, 'alert');
        }
    }

    function Logout() {
        serverCall('Home/Logout', 'POST', '{}', 'alert');
    }

    function CreateUser() {
        var userName = $("#usersname").val();
        var mailID = $("#mailid").val();
        var mobile = $("#mobile").val();
        var password = $("#txtpassword").val();
        var confirmPassword = $("#confirmpassword").val();

        if (userName == "") {
            $("#alert").html(customAlert("Please Enter User Name", "danger"));
        } else if (mailID == "") {
            $("#alert").html(customAlert("Please Enter Mail ID", "danger"));
        } else if(mobile == ""){
            $("#alert").html(customAlert("Please Enter Mobile", "danger"));
        } else if (password == "") {
            $("#alert").html(customAlert("Please Enter Password", "danger"));
        } else if (confirmPassword == "") {
            $("#alert").html(customAlert("Please Enter Confirm Password", "danger"));
        } else if (password != confirmPassword) {
            $("#alert").html(customAlert("Password Not Matched", "danger"));
        } else {
            var obj = {
                userName: userName,
                email: mailID,
                mobile: mobile,
                password: password,
            }
            serverCall('Home/CreateUser', 'POST', obj, 'alert');
        }
    }


    var selectors = "#loginSubmit, #createUser, #logout"
    $(selectors).click(function () {
        switch ($(this).attr('id')) {
            case "loginSubmit":
                Login();
                break;
            case "createUser":
                CreateUser();
                break;
            case "logout":
                Logout();
                break;
        }
    });

    

    
    
})