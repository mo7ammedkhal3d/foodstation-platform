 //https://www.youtube.com/watch?v=pWotdyUYQxw
// Account 

//#region User-Dashboard

var DeleteUserConfirm = function (str) {
    $("#Modal-DeleteUser").modal('show');
    $("#UserId").val(str);
    
};

var UserDeleteConfirm = function () {
    var userId = $("#UserId").val();
    $.ajax({
        type: "Post",
        url: "/Account/DeleteConfirmed",
        data: { id: userId },
        success: function (result) {
            if (result) {
                $("#Modal-DeleteUser").modal('hide');

            } else {
                TestSweetAlert("حذث خطا ما أثناء عملية الحدف ");
            }
        }
    });

};

//#endregion User-Dashboard

//#region Preloader

(function ($) {
    "use strict";
    /* Preloader */
    $(window).on('load', function () {
        var preloaderFadeOutTime = 500;
        function hidePreloader() {
            var preloader = $('.spinner-wrapper');
            setTimeout(function () {
                preloader.fadeOut(preloaderFadeOutTime);
            }, 500);
        }
        hidePreloader();
    });

})(jQuery);

//#endregion Preloader

//#region Hide Landing

$(document).ready(function () {
    if (window.location.pathname.includes('/GetRestaurantCategories/')) {
        document.getElementById('landing').style.display = "none";
    }
    else if (window.location.pathname.includes('Dashboard')) {
        document.getElementById('landing').style.display = "none";
    }
});


//#endregion Hide Landing

//#region Addtion 

var gitbillConfirm = function () {
    $.ajax({
        type: "Get",
        url: "/Home/GetBill",
        success: function (items) {
            $('#_billItems').html(items);
            $("#Modal-billItems").modal('show');
        }
    });
};

// Upload picture to show   

var loadFile = function (str, event) {
    var image = document.getElementById(str)
    image.src = URL.createObjectURL(event.target.files[0]);
};

// Reset filds whene cancel add    

var resetFilds = function (form,img, event) {
    const ResetForm = document.getElementById(form);
    ResetForm.reset();
    if (img != "") {
        var ImgUrl = document.getElementById(img);
        ImgUrl.src = "";
    }
};

//#endregion  Addtion

//#region Searsh Function

function Searsh(tableName) {
    // Declare variables
    var input, filter, table, tr, td, i, txtValue;
    input = document.getElementById("myInput");
    filter = input.value.toUpperCase();
    table = document.getElementById(tableName);
    tr = table.getElementsByTagName("tr");

    // Loop through all table rows, and hide those who don't match the search query
    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[0];
        if (td) {
            txtValue = td.textContent || td.innerText;
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
}

//#endregion Searsh Function

//#region Login

//$(function () {

//    var userLoginButton = $("#UserLoginModal button[name='login']").click(onUserLoginClick);

//    function onUserLoginClick() {

//        var url = "/Account/Login";

//        var antiForgeryToken = $("#UserLoginModal input[name='__RequestVerificationToken']").val();

//        var userName = $("#UserLoginModal input[name = 'UserName']").val();
//        var password = $("#UserLoginModal input[name = 'Password']").val();
//        var rememberMe = $("#UserLoginModal input[name = 'RememberMe']").prop('checked');

//        var userInput = {
//            __RequestVerificationToken: antiForgeryToken,
//            UserName: userName,
//            Password: password,
//            RememberMe: rememberMe
//        };

//        $.ajax({
//            type: "POST",
//            url: url,
//            data: userInput,
//            success: function (data) {

//                var parsed = $.parseHTML(data);

//                var hasErrors = $(parsed).find("input[name='LoginInValid']").val() == "true";

//                if (hasErrors == true) {
//                    $("#UserLoginModal").html(data);

//                    userLoginButton = $("#UserLoginModal button[name='login']").click(onUserLoginClick);

//                    var form = $("#UserLoginForm");

//                    $(form).removeData("validator");
//                    $(form).removeData("unobtrusiveValidation");
//                    $.validator.unobtrusive.parse(form);

//                }
//                else {
//                    location.href = '/Home/Index';

//                }
//            },
//            error: function (xhr, ajaxOptions, thrownError) {
//                var errorText = "Status: " + xhr.status + " - " + xhr.statusText;

//                PresentClosableBootstrapAlert("#alert_placeholder_login", "danger", "Error!", errorText);

//                console.error(thrownError + "\r\n" + xhr.statusText + "\r\n" + xhr.responseText);
//            }
//        });
//    }
//});

//#endregion Login

//#region _BillPartial

var DeleteItem = function (_id) {
    $.ajax({
        type: "Post",
        url: "/Home/DeleteItemFromBill",
        data: { id: _id },
        success: function (result) {
            document.getElementById("BasketId").click();
            return false;
        }
    });
};

var IncreasQuantity = function (_id) {
    $.ajax({
        type: "Post",
        url: "/Home/IncreasQuantity",
        data: { id: _id },
        success: function (result) {
            $.ajax({
                url: "/Home/Refreash",
                contentType: 'application/html; charset=utf-8',
                type: 'GET',
                dataType: 'html',
                success: (function (result) {
                    $('#_BillPartial').html(result);
                })
            });
           // document.getElementById("BasketId").click();
            return false;
        }
    });
};
var decreasQuantity = function (_id) {
    $.ajax({
        type: "Post",
        url: "/Home/decreasQuantity",
        data: { id: _id },
        success: function (result) {
            $.ajax({
                url: "/Home/Refreash",
                contentType: 'application/html; charset=utf-8',
                type: 'GET',
                dataType: 'html',
                success: (function (result) {
                    $('#_BillPartial').html(result);
                })
            });
            return false;
        }
    });
};



//#endregion _BillPartial

//#region custom function for Sweet massage
function TestSweetAlert(massage) {
    swal({
        //  title: title,
        text: massage,
        content: true,
        // icon: "info", /*"success"  "erroe"*/
        className: 'swal-IW',
        //timer: 1300,
        buttons: false,
    });
};

//#endregion custom function for Sweet massage

//#region Country-Dashboard

var EditCountryConfirm = function (_id) {
    $("#Modal-countryEdit").modal('show');
    $("#ECname").val(document.getElementById('CountryName+' + _id).value)
    $("#CountryId").val(_id)
    $("#hiddenId").val(_id)
};


var DeleteCountryConfirm = function (_id) {
    $.ajax({
        type: "Post",
        url: "/Countries/DeleteConfirmed",
        data: { id: _id },
        success: function (message) {
            if (message == "") {
                $.ajax({
                    url: '/Countries/Refreash',
                    contentType: 'application/html; charset=utf-8',
                    type: 'GET',
                    dataType: 'html',
                    success: (function (result) {
                        $('#_CountryPartial').html(result);
                    })
                });
            } else if (message == "haveItem") {
                swal({
                    // title: "Are you sure?",
                    text: "هده الدولة تضم عدد من المناطق بحدفك لها ستفقد بينات المناطق والطاعم المرتبطة بها والعناصر المرتبطة بالطاعم هل تريد بالتأكيد حذف هذه الدولة",
                    className: 'swal-IW',
                    icon: "warning",
                    buttons: true,
                    dangerMode: true,
                }).then((result) => {
                    if (result == true) {
                        $.ajax({
                            type: 'GET',
                            url: '/Countries/DeleteCountryAndRegions',
                            data: { id: _id },
                            contentType: 'application/html; charset=utf-8',
                            dataType: 'html',
                            success: (function (result) {
                                $.ajax({
                                    url: '/Countries/Refreash',
                                    contentType: 'application/html; charset=utf-8',
                                    type: 'GET',
                                    dataType: 'html',
                                    success: (function (result) {
                                        $('#_CountryPartial').html(result);
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

$(document).ready(function () {

    $("#btnCreateCountry").click(function () {
    if ($("#CCname").val() == "") {
        TestSweetAlert("قم بأدخال الأسم");
    } else {
        var formData = new FormData($("#CreateCountryForm")[0]);
        $.ajax({
            async: true,
            type: "POST",
            dataType: "JSON",
            url: "/Countries/Create",
            data: formData,
            processData: false,
            contentType: false,
            success: function (result) {
                if (result) {
                    $.ajax({
                        url: '/Countries/Refreash',
                        contentType: 'application/html; charset=utf-8',
                        type: 'GET',
                        dataType: 'html',
                        success: (function (result) {
                            $('#_CountryPartial').html(result);
                            var close = document.getElementById('btnCreateColse');
                            close.click();
                        })
                    });
                } else {
                    TestSweetAlert("حدث خطأما أثناء عملية الأضافة تاكد من أدخال الحقول بالشكل الصحيح وحاول مرة أخرى");
                }
            }
        });
    }
    
    return false;
});

    $("#btnEditCountry").click(function () {
        if ($("#ECname").val() == "") {
            TestSweetAlert("قم بأدخال الأسم");
        } else {
            var formData = new FormData($("#EditCountryForm")[0]);
            $.ajax({
                async: true,
                type: "POST",
                dataType: "JSON",
                url: "/Countries/Edit",
                data: formData,
                processData: false,
                contentType: false,
                success: function (result) {
                    if (result) {
                        $.ajax({
                            url: '/Countries/Refreash',
                            contentType: 'application/html; charset=utf-8',
                            type: 'GET',
                            dataType: 'html',
                            success: (function (result) {
                                $('#_CountryPartial').html(result);
                                var close = document.getElementById('btnEditColse');
                                close.click();
                            })
                        });
                    } else {
                        TestSweetAlert("حدث خطأما أثناء عملية الأضافة تاكد من أدخال الحقول بالشكل الصحيح وحاول مرة أخرى");
                    }
                }
            });
        }
        return false;
    });
        
});
    

//#endregion Country-Dashboard

//#region  Region-Dashboard


var EditRegionConfirm = function (_id) {
    $("#RegionId").val(_id)
    $.ajax({
        type: "Post",
        url: "/Regions/GetRegion",
        data: { id: _id },
        success: function (region) {
            $("#Modal-regionEdit").modal('show');
            $("#ERname").val(region.Name)
            $("#ERlongitude").val(region.Longitude)
            $("#ERlatitude").val(region.Latitude)
            $("#ERCountryId").val(region.CountryId)
            $("#hiddenId").val(_id)
        }
    });
};

//#Delete Region 

var DeleteRegionConfirm = function (_id) {
    $("#RegionId").val(_id)
    $("#Modal-RegionDelete").modal('show');
};

$(document).ready(function () {

  $("#btnRegionCreate").click(function () {
        if ($("#CRname").val() == "") {
            TestSweetAlert("قم بأدخال اسم المنطقة");
        } if ($("#CRlongitude").val() == "") {
          TestSweetAlert("قم بأدخال خط العرض للمنطقة");
        } if ($("#CRlatitude").val() == "") {
          TestSweetAlert("قم بأدخال خط الطول للمنطقة");
        } else {
            var formData = new FormData($("#CreateRegionForm")[0]);
            $.ajax({
                async: true,
                type: "POST",
                dataType: "JSON",
                url: "/Regions/Create",
                data: formData,
                processData: false,
                contentType: false,
                success: function (result) {
                    if (result) {
                        $.ajax({
                            url: '/Regions/Refreash',
                            contentType: 'application/html; charset=utf-8',
                            type: 'GET',
                            dataType: 'html',
                            success: (function (result) {
                                $('#_RegionPartial').html(result);
                                var close = document.getElementById('btnCreateColse');
                                close.click();
                            })
                        });
                    } else {
                        TestSweetAlert("حدث خطأما أثناء عملية الأضافة تاكد من أدخال الحقول بالشكل الصحيح وحاول مرة أخرى");
                    }
                }
            });
        }
        return false;
    });

  $("#btnRegionEdit").click(function () {
        if ($("#ERname").val() == "") {
            TestSweetAlert("قم بأدخال اسم المنطقة");
        } else if ($("#ERlongitude").val() == "") {
            TestSweetAlert("قم بأدخال خط العرض للمنطقة");
        } else if ($("#ERlatitude").val() == "") {
            TestSweetAlert("قم بأدخال خط الطول للمنطقة");
        } else {
                var formData = new FormData($("#EditRegionForm")[0]);
                $.ajax({
                    async: true,
                    type: "POST",
                    dataType: "JSON",
                    url: "/Regions/Edit",
                    data: formData,
                    processData: false,
                    contentType: false,
                    success: function (result) {
                        if (result) {
                            $.ajax({
                                url: '/Regions/Refreash',
                                contentType: 'application/html; charset=utf-8',
                                type: 'GET',
                                dataType: 'html',
                                success: (function (result) {
                                    $('#_RegionPartial').html(result);
                                    var close = document.getElementById('btnEditColse');
                                    close.click();
                                })
                            });
                        } else {
                            TestSweetAlert("حدث خطأما أثناء عملية التعديل تاكد من أدخال الحقول بالشكل الصحيح وحاول مرة أخرى");
                        }
                    }
                });

          }
 });

  $("#btnRegionDelete").click(function () { 
        var RegionId = $("#RegionId").val();
        $.ajax({
            type: "Post",
            url: "/Regions/DeleteConfirmed",
            data: { id: RegionId },
            success: function (message) {
                if (message == "") {
                    $("#Modal-RegionDelete").modal('hide');
                    $("#RegionId").val(null);
                    $.ajax({
                        url: '/Regions/Refreash',
                        contentType: 'application/html; charset=utf-8',
                        type: 'GET',
                        dataType: 'html',
                        success: (function (result) {
                            $('#_RegionPartial').html(result);
                        })
                    })
                } else if (message == "haveRestaurant") {
                    swal({
                        // title: "Are you sure?",
                        text: "هده المنطقة تضم عدد من المطاعم بحدفك لها ستفقد كل بينات المطاعم التابعة لهده المنطقة  والعناصر لهده المطاعم هل تريد بالتأكيد حدفها",
                        className: 'swal-IW',
                        icon: "warning",
                        buttons: true,
                        dangerMode: true,
                    }).then((result) => {
                        if (result == true) {
                            $("#Modal-RegionDelete").modal('hide');
                            $("#RegionId").val(null);
                            $.ajax({
                                type: 'GET',
                                url: '/Regions/DeleteRegionAndRestaurants',
                                data: { id: RegionId },
                                contentType: 'application/html; charset=utf-8',
                                dataType: 'html',
                                success: (function (result) {
                                    $.ajax({
                                        url: '/Regions/Refreash',
                                        contentType: 'application/html; charset=utf-8',
                                        type: 'GET',
                                        dataType: 'html',
                                        success: (function (result) {
                                            $('#_RegionPartial').html(result);
                                        })
                                    })
                                })
                            })
                        } else {
                            $("#Modal-RegionDelete").modal('hide');
                            $("#RegionId").val(null);
                        }
                    });
                }
            },
        });
     return false;
 });

});

//#endregion region-Dashboard

//#region Restaurant-Dashboard

// Edit Action


var EditRestaurantConfirm = function (_id) {

    $("#RestaurantId").val(_id)
    const form = document.getElementById('EditRestaurantForm');
    form.reset();
    $.ajax({
        type: "Post",
        url: "/Restaurants/GetRestaurant",
        data: { id: _id },
        success: function (restaurant) {
            $("#Modal-restaurantEdit").modal('show');
            $("#ERname").val(restaurant.Name)
            $("#ERdescription").val(restaurant.Description)
            $("#ERregion").val(restaurant.RegionId)
            $("#ERlongitude").val(restaurant.Longitude)
            $("#ERlatitude").val(restaurant.Latitude)
            $("#ERowner").val(restaurant.UserId)
            $("#ERparticipation").val(restaurant.Participation)
            for (var i = 0; i < restaurant.diningTypeIds.length; i++) {
                // find the corresponding checkbox with the matching value
                var checkbox = $('input.diningTypescheckbox[value="' + restaurant.diningTypeIds[i] + '"]');
                // set the 'checked' property of the checkbox to 'true'
                checkbox.prop('checked', true);
            }
            document.getElementById('ERimgUrl').src = "/Uploads/Restaurants/" + restaurant.ImgUrl;
            $("#hiddenId").val(_id)
            $("#hiddenImgUrl").val(restaurant.ImgUrl)
        }
    });
};


// Delete Action

var DeleteRestaurantConfirm = function (_id) {
    $("#RestaurantId").val(_id)
    $.ajax({
        type: "Post",
        url: "/Restaurants/GetRestaurant",
        data: { id: _id },
        success: function (Restaurant) {
            $("#Modal-restaurantDelete").modal('show');
            $("#DRname").val(Restaurant.Name)
            $("#DRdescription").val(Restaurant.Description)
            $("#DRregion").val(Restaurant.Region)
            document.getElementById('DRimgUrl').src = "/Uploads/Restaurants/" + Restaurant.ImgUrl;
        }
    });
};


$(document).ready(function () {

    $("#btnRestaurantCreate").click(function () {
        if ($("#CRname").val() == "") {
            TestSweetAlert("قم بأدخال الأسم");
        } else if ($("#CRdescription").val() == "") {
            TestSweetAlert("قم بأدخال الوصف");
        } else if ($("#CRpicture").val() == "") {
            TestSweetAlert("قم بأضافة صورة لمطعم");
        } else if ($("#CRlongitude").val() == "") {
            TestSweetAlert("قم بأدخال خط الطول للمطعم");
        } else if ($("#CRlatitude").val() == "") {
            TestSweetAlert("قم بأدخال خط العرض للمطعم");
        } else {
            var fileInput = document.getElementById('CRpicture');
            $.ajax({
                type: "Get",
                url: "/Restaurants/IsImageExist",
                data: { upload: fileInput.files[0].name },
                success: function (Message) {
                    if (Message == "") {
                        var image = $("#CRpicture").get(0).files;
                        var formData = new FormData($("#CreateRestaurantForm")[0]);
                        $.ajax({
                            async: true,
                            type: "POST",
                            dataType: "JSON",
                            url: "/Restaurants/Create",
                            data: formData,
                            processData: false,
                            contentType: false,
                            success: function (result) {
                                if (result) {
                                    $.ajax({
                                        url: '/Restaurants/Refreash',
                                        contentType: 'application/html; charset=utf-8',
                                        type: 'GET',
                                        dataType: 'html',
                                        success: (function (result) {
                                            $('#_RestaurantPartial').html(result);
                                            var close = document.getElementById('btnCreateColse');
                                            close.click();
                                        })
                                    });
                                } else {
                                    TestSweetAlert("حدث خطأما أثناء عملية الأضافة تاكد من أدخال الحقول بالشكل الصحيح وحاول مرة أخرى");
                                }
                            }
                        });
                    } else {
                        TestSweetAlert(Message);
                    }
                }
            });
        }
        return false;
    });

    $("#btnRestaurantEdit").click(function () {
        if ($("#ERname").val() == "") {
            TestSweetAlert("قم بأدخال الأسم");
        } else if ($("#ERdescription").val() == "") {
            TestSweetAlert("قم بأدخال الوصف");
        } else if ($("#ERlongitude").val() == "") {
            TestSweetAlert("قم بأدخال خط الطول للمطعم");
        } else if ($("#ERlatitude").val() == "") {
            TestSweetAlert("قم بأدخال خط العرض للمطعم");
        } else if ($("#ERpicture").val() != "") {
            var fileInput = document.getElementById('ERpicture');
            $.ajax({
                type: "Get",
                url: "/Restaurants/IsImageExist",
                data: { upload: fileInput.files[0].name },
                success: function (Message) {
                    if (Message == "") {
                        var formData = new FormData($("#EditRestaurantForm")[0]);
                        $.ajax({
                            async: true,
                            type: "POST",
                            dataType: "JSON",
                            url: "/Restaurants/Edit",
                            data: formData,
                            processData: false,
                            contentType: false,
                            success: function (result) {
                                if (result) {
                                    $.ajax({
                                        url: '/Restaurants/Refreash',
                                        contentType: 'application/html; charset=utf-8',
                                        type: 'GET',
                                        dataType: 'html',
                                        success: (function (result) {
                                            $('#_RestaurantPartial').html(result);
                                            var close = document.getElementById('btnEditColse');
                                            close.click();
                                        })
                                    });
                                } else {
                                    TestSweetAlert("حدث خطأما أثناء عملية التعديل تاكد من تعديل الحقول بالشكل الصحيح وحاول مرة أخرى");
                                }
                            }
                        });
                    } else {
                        TestSweetAlert(Message);
                    }
                }
            });
        } else {
            var formData = new FormData($("#EditRestaurantForm")[0]);
            $.ajax({
                async: true,
                type: "POST",
                dataType: "JSON",
                url: "/Restaurants/Edit",
                data: formData,
                processData: false,
                contentType: false,
                success: function (result) {
                    if (result) {
                        $.ajax({
                            url: '/Restaurants/Refreash',
                            contentType: 'application/html; charset=utf-8',
                            type: 'GET',
                            dataType: 'html',
                            success: (function (result) {
                                $('#_RestaurantPartial').html(result);
                                var close = document.getElementById('btnEditColse');
                                close.click();
                            })
                        });
                    } else {
                        TestSweetAlert("حدث خطأما أثناء عملية الأضافة تاكد من أدخال الحقول بالشكل الصحيح وحاول مرة أخرى");
                    }
                }
            });

        }
        return false;
    });

    $("#btnRestaurantDelete").click(function () {
        var RestaurantId = $("#RestaurantId").val();
        $.ajax({
            type: "Post",
            url: "/Restaurants/DeleteConfirmed",
            data: { id: RestaurantId },
            success: function (message) {
                if (message == "") {
                    $("#Modal-restaurantDelete").modal('hide');
                    $("#RestaurantId").val(null);
                    $.ajax({
                        url: '/Restaurants/Refreash',
                        contentType: 'application/html; charset=utf-8',
                        type: 'GET',
                        dataType: 'html',
                        success: (function (result) {
                            $('#_RestaurantPartial').html(result);
                        })
                    })
                } else if (message == "haveItem") {
                    swal({
                        // title: "Are you sure?",
                        text: "هذا المطعم مرتبط بعدد من العناصر هل تريد بالفعل حذفه",
                        className: 'swal-IW',
                        icon: "warning",
                        buttons: true,
                        dangerMode: true,
                    }).then((result) => {
                        if (result == true) {
                            $("#Modal-restaurantDelete").modal('hide');
                            $("#RestaurantId").val(null);
                            debugger;
                            $.ajax({
                                type: 'GET',
                                url: '/Restaurants/DeleteRestaurantandItems',
                                data: { id: RestaurantId },
                                contentType: 'application/html; charset=utf-8',
                                dataType: 'html',
                                success: (function (result) {
                                    $.ajax({
                                        url: '/Restaurants/Refreash',
                                        contentType: 'application/html; charset=utf-8',
                                        type: 'GET',
                                        dataType: 'html',
                                        success: (function (result) {
                                            $('#_RestaurantPartial').html(result);
                                        })
                                    })
                                })
                            })
                        } else {
                            $("#Modal-restaurantDelete").modal('hide');
                            $("#RestaurantId").val(null);
                        }
                    });
                }
            },
        });
        return false;
    });
});

//#endregion Restaurant-Dashboard

//#region Category-Dashboard

 //Edit Action

var EditCategoryConfirm = function (_id) {
    $("#CategoryId").val(_id)
    $.ajax({
        type: "Post",
        url: "/Categories/GetCategory",
        data: { id: _id },
        success: function (category) {
            $("#Modal-categoryEdit").modal('show');
            $("#ECatname").val(category.Name)
            document.getElementById('ECatimgUrl').src = "/Uploads/Categories/" + category.ImgUrl;
            $("#hiddenId").val(_id)
            $("#hiddenImgUrl").val(category.ImgUrl)
        }
    });
};

 //Delete Action

var DeleteCategoryConfirm = function (_id) {
    $("#CategoryId").val(_id)
    $.ajax({
        type: "Post",
        url: "/Categories/GetCategory",
        data: { id: _id },
        success: function (Category) {
            $("#Modal-categoryDelete").modal('show');
            $("#DCatname").val(Category.Name)
            document.getElementById('DCatimgUrl').src = "/Uploads/Categories/" + Category.ImgUrl;
        }
    });
};


$(document).ready(function () {

    $("#btnCategoryCreate").click(function () {
        if ($("#CCatname").val() == "") {
            TestSweetAlert("قم بأدخال الأسم");
        } else if ($("#CCatpicture").val() == "") {
            TestSweetAlert("قم بأضافة صورة لمطعم");
        } else {
            var fileInput = document.getElementById('CCatpicture');
            $.ajax({
                type: "Get",
                url: "/Categories/IsImageExist",
                data: { upload: fileInput.files[0].name },
                success: function (Message) {
                    if (Message == "") {
                        var formData = new FormData($("#createCategoryForm")[0]);
                        $.ajax({
                            async: true,
                            type: "POST",
                            dataType: "JSON",
                            url: "/Categories/Create",
                            data: formData,
                            processData: false,
                            contentType: false,
                            success: function (result) {
                                if (result) {
                                    $.ajax({
                                        url: '/Categories/Refreash',
                                        contentType: 'application/html; charset=utf-8',
                                        type: 'GET',
                                        dataType: 'html',
                                        success: (function (result) {
                                            $('#_CategoryPartial').html(result);
                                            var close = document.getElementById('btnCreateColse');
                                            close.click();
                                        })
                                    });
                                } else {
                                    TestSweetAlert("حدث خطأما أثناء عملية الأضافة تاكد من أدخال الحقول بالشكل الصحيح وحاول مرة أخرى");
                                }
                            }
                        });
                    } else {
                        TestSweetAlert(Message);
                    }
                }
            });
        }
        return false;
    });

    $("#btnCategoryEdit").click(function () {
        if ($("#ECatname").val() == "") {
            TestSweetAlert("قم بأدخال الأسم");
        } else if ($("#ECatpicture").val() != "") {
            var fileInput = document.getElementById('ECatpicture');
            $.ajax({
                type: "Get",
                url: "/Categories/IsImageExist",
                data: { upload: fileInput.files[0].name },
                success: function (Message) {
                    if (Message == "") {
                        var formData = new FormData($("#EditCategoryForm")[0]);
                        $.ajax({
                            async: true,
                            type: "POST",
                            dataType: "JSON",
                            url: "/Categories/Edit",
                            data: formData,
                            processData: false,
                            contentType: false,
                            success: function (result) {
                                debugger;
                                if (result) {
                                    $.ajax({
                                        url: '/Categories/Refreash',
                                        contentType: 'application/html; charset=utf-8',
                                        type: 'GET',
                                        dataType: 'html',
                                        success: (function (result) {
                                            $('#_CategoryPartial').html(result);
                                            var close = document.getElementById('btnEditColse');
                                            close.click();
                                        })
                                    });
                                } else {
                                    TestSweetAlert("حدث خطأما أثناء عملية التعديل تاكد من تعديل الحقول بالشكل الصحيح وحاول مرة أخرى");
                                }
                            }
                        });
                    } else {
                        TestSweetAlert(Message);
                    }
                }
            });
        } else {
            var formData = new FormData($("#EditCategoryForm")[0]);
            $.ajax({
                async: true,
                type: "POST",
                dataType: "JSON",
                url: "/Categories/Edit",
                data: formData,
                processData: false,
                contentType: false,
                success: function (result) {
                    if (result) {
                        $.ajax({
                            url: '/Categories/Refreash',
                            contentType: 'application/html; charset=utf-8',
                            type: 'GET',
                            dataType: 'html',
                            success: (function (result) {
                                $('#_CategoryPartial').html(result);
                                var close = document.getElementById('btnEditColse');
                                close.click();
                            })
                        });
                    } else {
                        TestSweetAlert("حدث خطأما أثناء عملية الأضافة تاكد من أدخال الحقول بالشكل الصحيح وحاول مرة أخرى");
                    }
                }
            });

        }
        return false;
    });

    $("#btnCategoryDelete").click(function () {
        var CategoryId = $("#CategoryId").val();
        $.ajax({
            type: "Post",
            url: "/Categories/DeleteConfirmed",
            data: { id: CategoryId },
            success: function (message) {
                debugger;
                if (message == "") {
                    $("#Modal-categoryDelete").modal('hide');
                    $("#CategoryId").val(null);
                    $.ajax({
                        url: '/Categories/Refreash',
                        contentType: 'application/html; charset=utf-8',
                        type: 'GET',
                        dataType: 'html',
                        success: (function (result) {
                            $('#_CategoryPartial').html(result);
                        })
                    })
                } else if (message == "haveItem") {
                    swal({
                        // title: "Are you sure?",
                        text: "هناك عدد من العناصر مندرجة تحت هذا الصنف بحدفك لهذا الصنف سيتم حدف جميع العناصر المندرجة له",
                        className: 'swal-IW',
                        icon: "warning",
                        buttons: true,
                        dangerMode: true,
                    }).then((result) => {
                        if (result == true) {
                            $("#Modal-categoryDelete").modal('hide');
                            $("#CategoryId").val(null);
                            $.ajax({
                                type: 'GET',
                                url: '/Categories/DeleteCategoryandItems',
                                data: { id: CategoryId },
                                contentType: 'application/html; charset=utf-8',
                                dataType: 'html',
                                success: (function (result) {
                                    $.ajax({
                                        url: '/Categories/Refreash',
                                        contentType: 'application/html; charset=utf-8',
                                        type: 'GET',
                                        dataType: 'html',
                                        success: (function (result) {
                                            $('#_CategoryPartial').html(result);
                                        })
                                    })
                                })
                            })
                        } else {
                            $("#Modal-categoryDelete").modal('hide');
                            $("#CategoryId").val(null);
                        }
                    });
                }
            },
        });
        return false;
    });
});

//#endregion Restaurant-Dashboard

//#region Item-Dashboard

// Edit Action

var EditItemConfirm = function (_id) {
    $("#itemId").val(_id)
    $.ajax({
        type: "Post",
        url: "/Items/GetItem",
        data: { id: _id },
        success: function (item) {
            $("#Modal-itemEdit").modal('show');
            $("#EIname").val(item.Name)
            $("#EIprice").val(item.Price)
            $("#EItimeOfDone").val(item.TimeOfDone)
            $("#EIcategory").val(item.CategoryId)
            $("#EIrestaurant").val(item.RestaurantId)
            document.getElementById('EIimgUrl').src = "/Uploads/Items/" + item.ImgUrl;
            $("#hiddenId").val(_id)
            $("#hiddenImgUrl").val(item.ImgUrl)
        }
    });
};

// Delete Action

var DeleteItemConfirm = function (_id) {
    $("#itemId").val(_id)
    $.ajax({
        type: "Post",
        url: "/Items/GetItem",
        data: { id: _id },
        success: function (item) {
            $("#Modal-itemDelete").modal('show');
            $("#DIname").val(item.Name)
            $("#DIprice").val(item.Price)
            $("#DItimeOfDone").val(item.TimeOfDone)
            $("#DIcategory").val(item.Category)
            $("#DIrestaurant").val(item.Restaurant)
            document.getElementById('DIimgUrl').src = "/Uploads/Items/" + item.ImgUrl;
        }
    });
};


$(document).ready(function () {
    $("#btnItemCreate").click(function () {
        if ($("#CIname").val() == "") {
            TestSweetAlert("قم بأدخال الأسم");
        } else if ($("#CIprice").val() == "") {
            TestSweetAlert("قم بأدخال السعر");
        } else if ($("#CItimeOfDone").val() == "") {
            TestSweetAlert("قم بأدخال وقت التحظير");
        } else if ($("#CIpicture").val() == "") {
            TestSweetAlert("قم بأضافة صورة للعنصر");
        } else {
            var fileInput = document.getElementById('CIpicture');
            $.ajax({
                type: "Get",
                url: "/Items/IsImageExist",
                data: { upload: fileInput.files[0].name },
                success: function (Message) {
                    if (Message == "") {
                        var formData = new FormData($("#CreateItemForm")[0]);
                        $.ajax({
                            async: true,
                            type: "POST",
                            dataType: "JSON",
                            url: "/Items/Create",
                            data: formData,
                            processData: false,
                            contentType: false,
                            success: function (result) {
                                if (result) {
                                    $.ajax({
                                        url: '/Items/Refreash',
                                        contentType: 'application/html; charset=utf-8',
                                        type: 'GET',
                                        dataType: 'html',
                                        success: (function (result) {
                                            $('#_ItemParial').html(result);
                                            var close = document.getElementById('btnCreateColse');
                                            close.click();
                                        })
                                    });
                                } else {
                                    TestSweetAlert("حدث خطأما أثناء عملية الأضافة تاكد من أدخال الحقول بالشكل الصحيح وحاول مرة أخرى");
                                }
                            }
                        });
                    } else {
                        TestSweetAlert(Message);
                    }
                }
            });
        }
        return false;
    });

    $("#btnItemEdit").click(function () {

        if ($("#EIname").val() == "") {
            TestSweetAlert("قم بأدخال الأسم");
        } else if ($("#EIprice").val() == "") {
            TestSweetAlert("قم بأدخال السعر");
        } else if ($("#EItimeOfDone").val() == "") {
            TestSweetAlert("قم بأدخال وقت التحظير");
        } else if ($("#EIpicture").val() != "") {
            var fileInput = document.getElementById('EIpicture');
            $.ajax({
                type: "Get",
                url: "/Items/IsImageExist",
                data: { upload: fileInput.files[0].name },
                success: function (Message) {
                    if (Message == "") {
                        var formData = new FormData($("#EditItemForm")[0]);
                        $.ajax({
                            async: true,
                            type: "POST",
                            dataType: "JSON",
                            url: "/Items/Edit",
                            data: formData,
                            processData: false,
                            contentType: false,
                            success: function (result) {
                                if (result) {
                                    $.ajax({
                                        url: '/Items/Refreash',
                                        contentType: 'application/html; charset=utf-8',
                                        type: 'GET',
                                        dataType: 'html',
                                        success: (function (result) {
                                            $('#_ItemParial').html(result);
                                            var close = document.getElementById('btnEditColse');
                                            close.click();
                                        })
                                    });
                                } else {
                                    TestSweetAlert("حدث خطأما أثناء عملية الأضافة تاكد من أدخال الحقول بالشكل الصحيح وحاول مرة أخرى");
                                }
                            }
                        });
                    } else {
                        TestSweetAlert(Message);
                    }
                }
            });
        } else {
            var formData = new FormData($("#EditItemForm")[0]);
            $.ajax({
                async: true,
                type: "POST",
                dataType: "JSON",
                url: "/Items/Edit",
                data: formData,
                processData: false,
                contentType: false,
                success: function (result) {
                    if (result) {
                        $.ajax({
                            url: '/Items/Refreash',
                            contentType: 'application/html; charset=utf-8',
                            type: 'GET',
                            dataType: 'html',
                            success: (function (result) {
                                $('#_ItemParial').html(result);
                                var close = document.getElementById('btnEditColse');
                                close.click();
                            })
                        });
                    } else {
                        TestSweetAlert("حدث خطأما أثناء عملية الأضافة تاكد من أدخال الحقول بالشكل الصحيح وحاول مرة أخرى");
                    }
                }
            });

        }
        return false;
    });
  
    $("#btnItemDelete").click(function () {
        var itemId = $("#itemId").val();
        $.ajax({
            type: "Post",
            url: "/Items/DeleteConfirmed",
            data: { id: itemId },
            success: function (result) {
                if (result) {
                    $("#Modal-itemDelete").modal('hide');
                    $("#itemId").val(null);
                    $.ajax({
                        url: '/Items/Refreash',
                        contentType: 'application/html; charset=utf-8',
                        type: 'GET',
                        dataType: 'html',
                        success: (function (result) {
                            $('#_ItemParial').html(result);
                        })
                    })
                } else {
                    TestSweetAlert("حذث خطا ما أثناء عملية الحدف ");
                }
            }
        });
    })
});

//#endregion Item-Dashboard

//#region Adv-Dashboars

var EditAdvConfirm = function (_id) {
    $("#AdvId").val(_id)
    $.ajax({
        type: "Post",
        url: "/Advertisements/GetAdvertisement",
        data: { id: _id },
        success: function (Adv) {
            $("#Modal-AdvEdit").modal('show');
            $("#EAdvDescription").val(Adv.Description)
            $("#EAdvRestaurant").val(Adv.RestaurantId)
            document.getElementById('EAdvimgUrl').src = "/Uploads/Advertisements/" + Adv.ImgUrl;
            $("#hiddenId").val(_id)
            $("#hiddenImgUrl").val(Adv.ImgUrl)
        }
    });
};

var DeleteAdvConfirm = function (_id) {

    swal({
        // title: "Are you sure?",
        text: "هل تريد بالتأكيد حذف هذا الاعلان",
        className: 'swal-IW',
        icon: "warning",
        buttons: true,
        dangerMode: true,
    }).then((result) => {
        if (result == true) {
            $.ajax({
                type: 'GET',
                url: '/Advertisements/DeleteConfirmed',
                data: { id: _id },
                contentType: 'application/html; charset=utf-8',
                dataType: 'html',
                success: (function (result) {
                    swal({
                        //  title: title,
                        text: result.Message,
                        content: true,
                        icon: "success",
                        className: 'swal-IW',
                        timer: 1700,
                        buttons: false,
                    });
                    $.ajax({
                        url: '/Advertisements/Refreash',
                        contentType: 'application/html; charset=utf-8',
                        type: 'GET',
                        dataType: 'html',
                        success: (function (result) {
                            $('#_AdvertisementPartial').html(result);
                        })
                    });
                })
            })
        }
    });
            

    return false;
};


$(document).ready(function () {

    $("#btnAdvCreate").click(function () {
        if ($("#CAdvpicture").val() == "") {
            TestSweetAlert("قم بأضافة صورة الأعلان");
        } else if ($("#CAdvDescription").val() == "") {
           TestSweetAlert("قم بادخال الوصف");
        } else {
            var img = document.getElementById('CAdvimgUrl');
            var fileInput = document.getElementById('CAdvpicture');
            if (img.naturalWidth != 1000 && img.naturalHeight != 350) {
                TestSweetAlert(" (1000px*350px) الصورة التي أدخلتها لاتتوافق مع أبعاد صورة الأعلان قم بتعديل أبعادها لتكون");
            }
            else {
                $.ajax({
                    type: "Get",
                    url: "/Advertisements/IsImageExist",
                    data: { upload: fileInput.files[0].name },
                    success: function (Message) {
                        if (Message == "") {
                            var formData = new FormData($("#CreateAdvForm")[0]);
                            $.ajax({
                                async: true,
                                type: "POST",
                                dataType: "JSON",
                                url: "/Advertisements/Create",
                                data: formData,
                                processData: false,
                                contentType: false,
                                success: function (result) {
                                    if (result.Seccess) {
                                        swal({
                                            //  title: title,
                                            text: result.Message,
                                            content: true,
                                            icon: "success",
                                            className: 'swal-IW',
                                            timer: 1700,
                                            buttons: false,
                                        });
                                        $.ajax({
                                            url: '/Advertisements/Refreash',
                                            contentType: 'application/html; charset=utf-8',
                                            type: 'GET',
                                            dataType: 'html',
                                            success: (function (result) {
                                                $('#_AdvertisementPartial').html(result);
                                                var close = document.getElementById('btnCreateColse');
                                                close.click();
                                            })
                                        });
                                    } else {
                                        TestSweetAlert(result.Message);
                                    }
                                }
                            });
                        } else {
                            TestSweetAlert(Message);
                        }
                    }
            });
            }   
        }
        return false;
    });

    $("#btnAdvEdit").click(function () {

        if ($("#EAdvDescription").val() == "") {
            TestSweetAlert("قم بادخال الوصف");
        } else if ($("#EAdvpicture").val() != ""){          
                    var img = document.getElementById('EAdvimgUrl');
                    var fileInput = document.getElementById('EAdvpicture');
                    if (img.naturalWidth != 1000 && img.naturalHeight != 350) {
                        TestSweetAlert(" (1000px*350px) الصورة التي أدخلتها لاتتوافق مع أبعاد صورة الأعلان قم بتعديل أبعادها لتكون");
                    }
                    else {
                        $.ajax({
                            type: "Get",
                            url: "/Advertisements/IsImageExist",
                            data: { upload: fileInput.files[0].name },
                            success: function (Message) {
                                if (Message == "") {
                                    var formData = new FormData($("#EditAdvForm")[0]);
                                    $.ajax({
                                        async: true,
                                        type: "POST",
                                        dataType: "JSON",
                                        url: "/Advertisements/Edit",
                                        data: formData,
                                        processData: false,
                                        contentType: false,
                                        success: function (result) {
                                            if (result) {
                                                $.ajax({
                                                    url: '/Advertisements/Refreash',
                                                    contentType: 'application/html; charset=utf-8',
                                                    type: 'GET',
                                                    dataType: 'html',
                                                    success: (function (result) {
                                                        $('#_AdvertisementPartial').html(result);
                                                        var close = document.getElementById('btnEditColse');
                                                        close.click();
                                                    })
                                                });
                                            } else {
                                                TestSweetAlert("حدث خطأما أثناء عملية الأضافة تاكد من أدخال الحقول بالشكل الصحيح وحاول مرة أخرى");
                                            }
                                        }
                                    });
                                } else {
                                    TestSweetAlert(Message);
                                }
                            }
                        });
                    }

               }else {
            var formData = new FormData($("#EditAdvForm")[0]);
            $.ajax({
                async: true,
                type: "POST",
                dataType: "JSON",
                url: "/Advertisements/Edit",
                data: formData,
                processData: false,
                contentType: false,
                success: function (result) {
                    if (result) {
                        $.ajax({
                            url: '/Advertisements/Refreash',
                            contentType: 'application/html; charset=utf-8',
                            type: 'GET',
                            dataType: 'html',
                            success: (function (result) {
                                $('#_AdvertisementPartial').html(result);
                                var close = document.getElementById('btnEditColse');
                                close.click();
                            })
                        });
                    } else {
                        TestSweetAlert("حدث خطأما أثناء عملية التعديل تاكد من أدخال الحقول بالشكل الصحيح وحاول مرة أخرى");
                    }
                }
            });

        }

        return false;
    });

});


//#endregion Adv-Dashboars

//#region GetRestaurants
var GetRestaurants = function (event) {
    $.ajax({
        type: "Post",
        url: "/Home/GetRegionRestaurants",
        success: function (result) {
            if (result) {
                window.location.href = "/Home/GetRegionRestaurants"
            }
            //else {
            //    swal({
            //        // title: "Are you sure?",
            //        text: "سيتم عرض كل المطاعم لأنه لم يتم تحديد المنطقة أدا كنت ترغب بعرض جميع المطاعم  أدغط على موافق",
            //        className: 'swal-IW',
            //        icon: "warning",
            //        buttons: true,
            //        dangerMode: true,
            //    }).then((result) => {
            //        if (result == true) {
            //            $.ajax({
            //                type: 'GET',
            //                url: '/Home/GetAllRestaurants',
            //                success: function (result) {
            //                    window.location.href = "/Home/GetRestaurants"
            //                }
            //            })
            //        }
            //    })
            //}

        }
    });
};

//#endregion GetRestaurants

//#region GetrestaurantCategories 

var _resturantId = $("#restaurantId").val();

var getItems = function (_id) {
    $.ajax({
        type: "Get",
        url: "/Home/GetCategoryItems",
        data: { categoryId: _id, restaurantId: _resturantId },
        success: function (items) {
            $('#_SelectedCategoryItems').html(items);
            return false;
        }
    });
};

var getallItems = function () {
    $.ajax({
        type: "Get",
        url: "/Home/GetAllRestaurantItems",
        data: { restaurantId: _resturantId },
        success: function (items) {
            $('#_SelectedCategoryItems').html(items);
            return false;
        }
    });
};

//#endregion GetrestaurantCategories


