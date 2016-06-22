/// <reference path="jquery-1.10.2.min.js" />
$(document).ready(function () {
    $("#StudentDiv").hide();
    $("#btnFacultyDiv").hide();
    $("#btnStudentDivOpen").click(function () {
        $("#StudentDiv").show();
        $("#btnFacultyDiv").hide();
    });
    $("#btnFacultyDivOpen").click(function () {
        $("#StudentDiv").hide();
        $("#btnFacultyDiv").show();
    });
});