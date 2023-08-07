
var EditUserConfirm = function (userId) {
        $("#userId").val(userId)
        $.ajax({
            type: "Post",
            url: "/ManageAccount/GetUser",
            data: { id: userId },
            success: function (user) {
            $("#Modal-EditUser").modal('show');
            $("#EUuserName").val(user.UserName)
            $("#EUemil").val(user.Email)
            $("#EUphoneNumber").val(user.PhoneNumber)
            $("#EUrole").val(user.Role)
            $("#hiddenId").val(userId)
            }
        });
    };

$(function () {

    var registerUserButton = $("#Modal-EditUser button[name = 'EditAccount']").click(onUserRegisterClick);

    function onUserRegisterClick() {
        var registerLoading = document.querySelector(".loading ");
        registerLoading.classList.add('registerloader');

        var baseUrl = window.location.protocol + "//" + window.location.host;
        var loginUrl = "/ManageAccount/EditUser";
        var url = baseUrl + loginUrl;

        var antiForgeryToken = $("#Modal-EditUser input[name='__RequestVerificationToken']").val();
        var id = $("#Modal-EditUser input[name = 'Id']").val();
        var userName = $("#Modal-EditUser input[name = 'UserName']").val();
        var phoneNumber = $("#Modal-EditUser input[name = 'PhoneNumber']").val();
        var email = $("#Modal-EditUser input[name='Email']").val();
        var role = $("#Modal-EditUser select[name='Roles']").val();
        var password = $("#Modal-EditUser input[name='Password']").val();
        var confirmPassword = $("#Modal-EditUser input[name='ConfirmPassword']").val();

        var user = {
            __RequestVerificationToken: antiForgeryToken,
            Id:id,
            UserName: userName,
            PhoneNumber: phoneNumber,
            Email: email,
            Role: role,
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
                    $('#Modal-EditUser .modal-body').height('auto');
                    $('#Modal-EditUser .modalContent').height('auto');
                    $("#Modal-EditUser").html(data);
                    var registerUserButton = $("#Modal-EditUser button[name = 'EditAccount']").click(onUserRegisterClick);

                    $("#EditUserForm").removeData("validator");
                    $("#EditUserForm").removeData("unobtrusiveValidation");
                    $.validator.unobtrusive.parse("#EditUserForm");
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
                    $("#Modal-EditUser").modal('hide');
                }

            },
            error: function (xhr, ajaxOptions, thrownError) {
                var errorText = "Status: " + xhr.status + " - " + xhr.statusText;

                PresentClosableBootstrapAlert("#alert_placeholder_editUser", "danger", "Error!", errorText);

                console.error(thrownError + '\r\n' + xhr.statusText + '\r\n' + xhr.responseText);
            }

        });

    }

});