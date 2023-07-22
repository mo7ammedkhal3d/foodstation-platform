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


var DismisNearbyMap = function () {
    document.getElementById('_PartialGetNearbyresaurants').style.display = 'none';
    document.getElementById('btnDismisNearbyMap').style.display = 'none';
};


var GetNearbyRestaurants = function () {
    if (navigator.geolocation) {
        // Get the user's current position
        navigator.geolocation.getCurrentPosition(function (position) {
            // Display the latitude and longitude
            var longitude = position.coords.longitude;
            var latitude = position.coords.latitude;
            var UserLocationData = {
                lon: longitude,
                lat: latitude,
            };
            $.ajax({
                type: "POST",
                url: "/Home/GetNearbyRestaurants",
                data: UserLocationData,
                success: function (result) {
                    if (result) {
                        document.getElementById('_PartialGetNearbyresaurants').style.display = 'block';
                        $('#_PartialGetNearbyresaurants').html(result);
                        document.getElementById('btnDismisNearbyMap').style.display = 'block';
                        initMap();
                    } else {
                        TestSweetAlert("حدث خطأ ما أثناء عملية تحديد المنطقة الرجاء التأكد من أنك متصل بالأنترنت");
                    }
                }
            });
        }, function (error) {
            // Handle errors
            switch (error.code) {
                case error.PERMISSION_DENIED:
                    TestSweetAlert("User denied the request for Geolocation.");
                    break;
                case error.POSITION_UNAVAILABLE:
                    TestSweetAlert("Location information is unavailable.");
                    break;
                case error.TIMEOUT:
                    TestSweetAlert("The request to get user location timed out.");
                    break;
                case error.UNKNOWN_ERROR:
                    TestSweetAlert("An unknown error occurred.");
                    break;
            }
        });
    } else {
        // Geolocation is not supported by this browser
        TestSweetAlert("Geolocation is not supported by this browser.");
    }
};