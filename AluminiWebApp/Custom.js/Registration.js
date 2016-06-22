$(document).ready(function () {
    $(document).on('change', "#CourseCategoryId", function () {
        $('#ddlCourses').empty();
        var CategoryCourseId = $("#CourseCategoryId").val();
        $.ajax({
            type: "Get",
            dataType: "json",
            url: "../Account/GetCourses?CourseCategoryId=" + CategoryCourseId,
            contentType: "Application/json",
            data: {},
            success: function (data) {
                $("#ddlCourses").append($("<option></option>").val("").html("Selet Course"));
                $.each(data, function (key, value) {
                    $("#ddlCourses").append($("<option></option>").val(value.Id).html(value.CourseName));
                });
            }
        });
    });
    $(document).on('change', "#StateId", function () {

        var StateId = $(this).val();
        //$(document).find('#StateId').empty();
        $.ajax({
            type: "Get",
            dataType: "json",
            url: "../Account/GetDistricts?StateId=" + StateId,
            contentType: "Application/json",
            data: {},
            success: function (data) {
                $(document).find("#ddlCitys").append($("<option></option>").val("").html("Select City"));
                $.each(data, function (key, value) {
                    $(document).find("#ddlCitys").append($("<option></option>").val(value.Id).html(value.CityName));
                });
            }
        });
    });

    //$(document).on('change', "#PermanentStateId", function () {      
    //    var StateId = $(this).val();
    //    $(document).find('#ddlPermanentDistricts').empty();
    //    $.ajax({
    //        type: "Get",
    //        dataType: "json",
    //        url: "../Account/GetDistricts?StateId=" + StateId,
    //        contentType: "Application/json",
    //        data: {},
    //        success: function (data) {
    //            $(document).find("#ddlPermanentDistricts").append($("<option></option>").val("").html("Selet District"));
    //            $.each(data, function (key, value) {
    //                $(document).find("#ddlPermanentDistricts").append($("<option></option>").val(value.Id).html(value.DistrictName));
    //            });
    //            $("#ddlPermanentDistricts").val($("#ddlDistricts").val());
    //            $("#ddlPermanentDistricts").change();

    //        }
    //    });
    //});


    $(document).on('change', "#ddlPermanentDistricts", function () {
        var DistrictId = $("#ddlPermanentDistricts").val();
        $('#ddlPermanentCitys').empty();
        $.ajax({
            type: "Get",
            dataType: "json",
            url: "../Account/GetCities?DistrictId=" + DistrictId + "",
            contentType: "Application/json",
            data: {},
            success: function (data) {
                $("#ddlPermanentCitys").append($("<option></option>").val("").html("Selet City"));
                $.each(data, function (key, value) {
                    $("#ddlPermanentCitys").append($("<option></option>").val(value.Id).html(value.CityName));
                });
                $('#ddlPermanentCitys').val($("#ddlCitys").val());
            }
        });
    });


    $(document).on('change', "#ddlDistricts", function () {
        var DistrictId = $("#ddlDistricts").val();
        $('#ddlCitys').empty();
        $.ajax({
            type: "Get",
            dataType: "json",
            url: "../Account/GetCities?DistrictId=" + DistrictId + "",
            contentType: "Application/json",
            data: {},
            success: function (data) {
                $("#ddlCitys").append($("<option></option>").val("").html("Selet City"));
                $.each(data, function (key, value) {
                    $("#ddlCitys").append($("<option></option>").val(value.Id).html(value.CityName));
                });
            }
        });
    });

    $(document).on('change', "#ddlCourses", function () {
        var StateId = $("#ddlCourses").val();
        $('#ddlGraduationYear').empty();
        $.ajax({
            type: "Get",
            dataType: "json",
            url: "../Account/GetYears?CourseId=" + StateId,
            contentType: "Application/json",
            data: {},
            success: function (data) {
                $("#ddlGraduationYear").append($("<option></option>").val("").html("Select"));
                $.each(data, function (key, value) {
                    $("#ddlGraduationYear").append($("<option></option>").val(value.Year).html(value.Year));
                });
            }
        });
    });
    $(document).on('change', '#UserProfilePicyture', function (event) {
        var files = event.target.files;
        for (var i = 0; i < files.length; i++) {
            var file = files[i];
            var reader = new FileReader();
            reader.addEventListener("load", function (event) {
                var dataURL = event.target.result;
                $('#ProfilePicture').val(dataURL);
                document.getElementById('ImagePrivew').setAttribute('src',dataURL);
            });
            reader.readAsDataURL(file);

        }

    });
});
