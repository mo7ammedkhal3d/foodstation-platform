$(function () {

    var registerUserButton = $("#Modal-AddUser button[name = 'AddAccount']").click(onUserRegisterClick);

    function onUserRegisterClick() {
        var registerLoading = document.querySelector(".loading ");
        registerLoading.classList.add('registerloader');

        var baseUrl = window.location.protocol + "//" + window.location.host;
        var loginUrl = "/ManageAccount/AddNewUser";
        var url = baseUrl + loginUrl;

        var antiForgeryToken = $("#Modal-AddUser input[name='__RequestVerificationToken']").val();
        var userName = $("#Modal-AddUser input[name = 'UserName']").val();
        var phoneNumber = $("#Modal-AddUser input[name = 'PhoneNumber']").val();
        var email = $("#Modal-AddUser input[name='Email']").val();
        var  role = $("#Modal-AddUser select[name='Roles']").val();
        var password = $("#Modal-AddUser input[name='Password']").val();
        var confirmPassword = $("#Modal-AddUser input[name='ConfirmPassword']").val();

        var user = {
            __RequestVerificationToken: antiForgeryToken,
            UserName: userName,
            PhoneNumber: phoneNumber,
            Email: email,
            Role:role,
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
                    $('#Modal-AddUser .modal-body').height('auto');
                    $('#Modal-AddUser .modalContent').height('auto');
                    $("#Modal-AddUser").html(data);
                    var registerUserButton = $("#Modal-AddUser button[name = 'AddAccount']").click(onUserRegisterClick);

                    $("#AddUserForm").removeData("validator");
                    $("#AddUserForm").removeData("unobtrusiveValidation");
                    $.validator.unobtrusive.parse("#AddUserForm");
                }
                else {
                    $.ajax({
                        url: '/ManageAccount/Refreash',
                        contentType: 'application/html; charset=utf-8',
                        type: 'GET',
                        dataType: 'html',
                        success: (function (result) {
                            $('#_UserPartial').html(result);
                        })
                    });
                    var close = document.getElementById('btnCreateColse');
                    close.click();
                    $("#Modal-AddUser").modal('hide');
                }

            },
            error: function (xhr, ajaxOptions, thrownError) {
                var errorText = "Status: " + xhr.status + " - " + xhr.statusText;

                PresentClosableBootstrapAlert("#alert_placeholder_addUser", "danger", "Error!", errorText);

                console.error(thrownError + '\r\n' + xhr.statusText + '\r\n' + xhr.responseText);
            }

        });

    }

});