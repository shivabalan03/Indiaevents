$("#testing").click(function () {
    serverCall('Home/sendEmail', 'POST', '{}', 'alert');
});