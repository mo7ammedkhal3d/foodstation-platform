var DeleteUserConfirm = function (userId) {
    $.ajax({
        type: "Post",
        url: "/ManageAccount/DeleteUser",
        data: { id: userId },
        success: function (message) {
            if (message == "") {
                $.ajax({
                    url: '/ManageAccount/Refreash',
                    contentType: 'application/html; charset=utf-8',
                    type: 'GET',
                    dataType: 'html',
                    success: (function (result) {
                        $('#_UserPartial').html(result);
                    })
                });
            } else if (message == "haveBills") {
                swal({
                    // title: "Are you sure?",
                    text: "أن الزبون الذي تود حذفة له عدد من الفواتير المرتبطة بعدد من المطاعم الرجاء الارسال للمطاعم لطلب منه تصفيه حساب الفاتورة المرتبطة بهذا الزبون من تم حذف الفاتورة لتتمكن من حذفة من قائمة المستخدمين ",
                    className: 'swal-IW',
                    icon: "warning",
                })
            } else if (message == "haveRestaurant") {
                swal({
                    // title: "Are you sure?",
                    text: "هذا المستخدم مالك لعدد من المطاعم بحذفك له سيتم حذف جميع بيانات المطاعم المالك لها هل تريد تأكيد الحذف",
                    className: 'swal-IW',
                    icon: "warning",
                    buttons: true,
                    dangerMode: true,
                }).then((result) => {
                    if (result == true) {
                        $.ajax({
                            type: "Post",
                            url: "/ManageAccount/DeleteUserAndRestaurants",
                            data: { id: userId },
                            success: (function (result) {
                                $.ajax({
                                    url: '/ManageAccount/Refreash',
                                    contentType: 'application/html; charset=utf-8',
                                    type: 'GET',
                                    dataType: 'html',
                                    success: (function (result) {
                                        $('#_UserPartial').html(result);
                                    })
                                });
                            })
                        })
                    }
                });
            }
        },
    });
    return false;
};