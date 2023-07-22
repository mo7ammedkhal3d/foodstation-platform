$(function () {
    
    //#region Code For Future Devlobe

    //$("#UserRegistrationModal").on('hidden.bs.modal', function (e) {
    //    $("#UserRegistrationModal input[name='CategoryId']").val('0');
    //});

    //$('.RegisterLink').click(function () {

    //    $("#UserRegistrationModal input[name='CategoryId']").val($(this).attr('data-categoryId'));

    //    $("#UserRegistrationModal").modal("show");

    //});

    //$("#UserRegistrationModal input[name = 'AcceptUserAgreement']").click(onAcceptUserAgreementClick);

    //$("#UserRegistrationModal button[name = 'register']").prop("disabled", true);

    //function onAcceptUserAgreementClick() {
    //    if ($(this).is(":checked")) {
    //        $("#UserRegistrationModal button[name = 'register']").prop("disabled", false);
    //    }
    //    else {
    //        $("#UserRegistrationModal button[name = 'register']").prop("disabled", true);

    //    }

    //}

    //$("#UserRegistrationModal input[name = 'Email']").blur(function () {

    //    var email = $("#UserRegistrationModal input[name = 'Email']").val();

    //    var url = "UserAuth/UserNameExists?userName=" + email;

    //    $.ajax({
    //        type: "GET",
    //        url: url,
    //        success: function (data) {
    //            if (data == true) {

    //                PresentClosableBootstrapAlert("#alert_placeholder_register", "warning", "Invalid Email", "This email address has already been registered");

    //            }
    //            else {
    //                CloseAlert("#alert_placeholder_register");
    //            }
    //        },
    //        error: function (xhr, ajaxOptions, thrownError) {
    //            var errorText = "Status: " + xhr.status + " - " + xhr.statusText;

    //            PresentClosableBootstrapAlert("#alert_placeholder_register", "danger", "Error!", errorText);

    //            console.error(thrownError + '\r\n' + xhr.statusText + '\r\n' + xhr.responseText);

    //        }
    //    });
    //});

    //#endregion Code For Future Devlobe

    function onLoginGoClick() {
        var form = document.getElementById('registerForm');
        form.reset();
        var activePopup = document.querySelector(".active");
        activePopup.style.height = "520px";
        wrapper.classList.remove('active');
    }

    var registerUserButton = $("#Modal-Register button[name = 'register']").click(onUserRegisterClick);

    function onUserRegisterClick()
    {
        var registerLoading = document.querySelector(".loading ");
        registerLoading.classList.add('registerloader');

        var baseUrl = window.location.protocol + "//" + window.location.host;
        var loginUrl = "/Account/Register";
        var url = baseUrl + loginUrl;

        var antiForgeryToken = $("#Modal-Register input[name='__RequestVerificationToken']").val();
        var userName = $("#Modal-Register input[name = 'UserName']").val();
        var email = $("#Modal-Register input[name='Email']").val();
        var password = $("#Modal-Register input[name='Password']").val();
        var confirmPassword = $("#Modal-Register input[name='ConfirmPassword']").val();

        var user = {
            __RequestVerificationToken: antiForgeryToken,
            UserName: userName,
            Email: email,
            Password: password,
            ConfirmPassword: confirmPassword,
        };

        $.ajax({
            type: "POST",
            url: url,
            data: user,
            success: function (data) {
                registerLoading.classList.remove('registerloader');
                var parsed = $.parseHTML(data);

                var hasErrors = $(parsed).find("input[name='RegistrationInValid']").val() == 'true';

                if (hasErrors) {
                    var activePopup = document.querySelector(".active");
                    activePopup.style.height="590px";

                    $("#Modal-Register").html(data);
                    var registerUserButton = $("#Modal-Register button[name = 'register']").click(onUserRegisterClick);
                    $("#Modal-Register button[name = 'btnLoginGo']").click(onLoginGoClick);

                    $("#registerForm").removeData("validator");
                    $("#registerForm").removeData("unobtrusiveValidation");
                    $.validator.unobtrusive.parse("#registerForm");
                }
                else {
                    var currentPath = window.location.pathname;
                    location.href = currentPath;
                }

            },
            error: function (xhr,ajaxOptions,thrownError) {
                var errorText = "Status: " + xhr.status + " - " + xhr.statusText;

                PresentClosableBootstrapAlert("#alert_placeholder_register", "danger", "Error!", errorText);

                console.error(thrownError + '\r\n' + xhr.statusText + '\r\n' + xhr.responseText);
            }

        });

    }

});