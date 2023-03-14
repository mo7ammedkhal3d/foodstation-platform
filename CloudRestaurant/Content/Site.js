
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
// start Bill javaScript

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

// end bill javaScript


// Item-Page

    //Create Action

    // custom function for Seet massage

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

    // Reset filds whene cancel add    
        
    var resetFilds = function (event) {
        document.getElementById('CreateItemForm').reset();
    var ImgUrl = document.getElementById('CIimgUrl');
    ImgUrl.src = "";
        };

   // Upload picture to show  for create Action 

    var CloadFile = function (event) {
            var image = document.getElementById('CIimgUrl');
    image.src = URL.createObjectURL(event.target.files[0]);
        };


    // Upload picture to show  for edit Action 

    var EloadFile = function (event) {
            var image = document.getElementById('EIimgUrl');
    image.src = URL.createObjectURL(event.target.files[0]);
        };

    // Edit Action

    var EditConfirm = function (_id) {
        $("#itemId").val(_id)
            $.ajax({
        type: "Post",
    url: "/Items/GetItem",
    data: {id: _id },
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

    var DeleteConfirm = function (_id) {
        $("#itemId").val(_id)
            $.ajax({
        type: "Post",
    url: "/Items/GetItem",
    data: {id: _id },
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

        $("#btnCreate").click(function () {

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

        $("#btnEdit").click(function () {

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
    data: {upload: fileInput.files[0].name },
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

        $("#btnDelete").click(function () {
                var itemId = $("#itemId").val();
    $.ajax({
        type: "Post",
    url: "/Items/DeleteConfirmed",
    data: {id: itemId },
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


//End Item-Page