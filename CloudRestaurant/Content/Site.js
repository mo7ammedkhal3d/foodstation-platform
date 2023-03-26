
// Home-Page

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
            document.getElementById("BasketId").click();
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

//#region Restaurant-Dashboard

//Create Action


// Reset filds whene cancel add    

var RresetFilds = function (event) {
    document.getElementById('CreateRestaurantForm').reset();
    var ImgUrl = document.getElementById('CRimgUrl');
    ImgUrl.src = "";
};

// Upload picture to show  for create Action 

var CRloadFile = function (event) {
    var image = document.getElementById('CRimgUrl');
    image.src = URL.createObjectURL(event.target.files[0]);
};


// Upload picture to show  for edit Action 

var ERloadFile = function (event) {
    var image = document.getElementById('ERimgUrl');
    image.src = URL.createObjectURL(event.target.files[0]);
};

// Edit Action

var EditRestaurantConfirm = function (_id) {
    $("#RestaurantId").val(_id)
    $.ajax({
        type: "Post",
        url: "/Restaurants/GetRestaurant",
        data: { id: _id },
        success: function (restaurant) {
            $("#Modal-restaurantEdit").modal('show');
            $("#ERname").val(restaurant.Name)
            $("#ERdescription").val(restaurant.Description)
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
            document.getElementById('DRimgUrl').src = "/Uploads/Restaurants/" + Restaurant.ImgUrl;
        }
    });
};


$(document).ready(function () {

    $("#btnRestaurantCreate").click(function () {
        debugger;
        if ($("#CRname").val() == "") {
            TestSweetAlert("قم بأدخال الأسم");
        } else if ($("#CRdescription").val() == "") {
            TestSweetAlert("قم بأدخال الوصف");
        } else if ($("#CRpicture").val() == "") {
            TestSweetAlert("قم بأضافة صورة لمطعم");
        } else {
            var fileInput = document.getElementById('CRpicture');
            $.ajax({
                type: "Get",
                url: "/Restaurants/IsImageExist",
                data: { upload: fileInput.files[0].name },
                success: function (Message) {
                    if (Message == "") {
                        var image = $("#CRpicture").get(0).files;
                        var formdata = new FormData;
                        formdata.append("Name", $("#CRname").val());
                        formdata.append("Description", $("#CRdescription").val());
                        formdata.append("upload", image[0]);
                        $.ajax({
                            async: true,
                            type: "POST",
                            dataType: "JSON",
                            url: "/Restaurants/Create",
                            data: formdata,
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
        } else if ($("#ERpicture").val() != "") {
            var fileInput = document.getElementById('ERpicture');
            $.ajax({
                type: "Get",
                url: "/Restaurants/IsImageExist",
                data: { upload: fileInput.files[0].name },
                success: function (Message) {
                    if (Message == "") {
                        var image = $("#ERpicture").get(0).files;
                        var formdata = new FormData;
                        formdata.append("Id", $("#hiddenId").val());
                        formdata.append("Name", $("#ERname").val());
                        formdata.append("Description", $("#ERdescription").val());
                        formdata.append("ImgUrl", $("#hiddenImgUrl").val());  ///
                        formdata.append("upload", image[0]);
                        $.ajax({
                            async: true,
                            type: "POST",
                            dataType: "JSON",
                            url: "/Restaurants/Edit",
                            data: formdata,
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
                    } else {
                        TestSweetAlert(Message);
                    }
                }
            });
        } else {
            var formdata = new FormData;
            formdata.append("Id", $("#hiddenId").val());
            formdata.append("Name", $("#ERname").val());
            formdata.append("Description", $("#ERdescription").val());
            formdata.append("ImgUrl", $("#hiddenImgUrl").val());
            $.ajax({
                async: true,
                type: "POST",
                dataType: "JSON",
                url: "/Restaurants/Edit",
                data: formdata,
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
                debugger;
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

//#region Item-Dashboard

//Create Action

// Reset filds whene cancel add    

var IresetFilds = function (event) {
    document.getElementById('CreateItemForm').reset();
    var ImgUrl = document.getElementById('CIimgUrl');
    ImgUrl.src = "";
};

// Upload picture to show  for create Action 

var CIloadFile = function (event) {
    var image = document.getElementById('CIimgUrl');
    image.src = URL.createObjectURL(event.target.files[0]);
};


// Upload picture to show  for edit Action 

var EIloadFile = function (event) {
    var image = document.getElementById('EIimgUrl');
    image.src = URL.createObjectURL(event.target.files[0]);
};

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
                        var image = $("#CIpicture").get(0).files;
                        var formdata = new FormData;
                        formdata.append("Name", $("#CIname").val());
                        formdata.append("Price", $("#CIprice").val());
                        formdata.append("TimeOfDone", $("#CItimeOfDone").val());
                        formdata.append("CategoryId", $("#CIcategory").val());
                        formdata.append("RestaurantId", $("#CIrestaurant").val());
                        formdata.append("upload", image[0]);
                        $.ajax({
                            async: true,
                            type: "POST",
                            dataType: "JSON",
                            url: "/Items/Create",
                            data: formdata,
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
                        var image = $("#EIpicture").get(0).files;
                        var formdata = new FormData;
                        formdata.append("Id", $("#hiddenId").val());
                        formdata.append("Name", $("#EIname").val());
                        formdata.append("Price", $("#EIprice").val());
                        formdata.append("TimeOfDone", $("#EItimeOfDone").val());
                        formdata.append("ImgUrl", $("#hiddenImgUrl").val());
                        formdata.append("CategoryId", $("#EIcategory").val());
                        formdata.append("RestaurantId", $("#EIrestaurant").val());
                        formdata.append("upload", image[0]);
                        $.ajax({
                            async: true,
                            type: "POST",
                            dataType: "JSON",
                            url: "/Items/Edit",
                            data: formdata,
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
            var formdata = new FormData;
            formdata.append("Id", $("#hiddenId").val());
            formdata.append("Name", $("#EIname").val());
            formdata.append("Price", $("#EIprice").val());
            formdata.append("TimeOfDone", $("#EItimeOfDone").val());
            formdata.append("ImgUrl", $("#hiddenImgUrl").val());
            formdata.append("CategoryId", $("#EIcategory").val());
            formdata.append("RestaurantId", $("#EIrestaurant").val());
            $.ajax({
                async: true,
                type: "POST",
                dataType: "JSON",
                url: "/Items/Edit",
                data: formdata,
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

//#region login and register window

const wrapper = document.querySelector(".wrapper ");
const loginLinke = document.querySelector(".login-like");
const registerLinke = document.querySelector(".register-like");
const ptnpopup = document.querySelector(".btnLogin-popup");
const iconClose = document.querySelector(".icon-close");

registerLinke.addEventListener('click', () => {
    wrapper.classList.add('active');
});
loginLinke.addEventListener('click', () => {
    wrapper.classList.remove('active');
});
ptnpopup.addEventListener('click', () => {
    wrapper.classList.add('active-popup');
});
iconClose.addEventListener('click', () => {
    wrapper.classList.remove('active-popup');
});

//#endregion login and register window

//#region GetrestaurantCategories 
debugger;
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

