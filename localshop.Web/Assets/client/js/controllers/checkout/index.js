$(function () {
    toastr.options = {
        "closeButton": false,
        "debug": false,
        "newestOnTop": false,
        "progressBar": false,
        "positionClass": "toast-bottom-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "3000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    };

    var outOfStock = $('#outOfStock').val();
    if (outOfStock == "true") {
        toastr["warning"]("Some product is out of stock, so you can only set max quantity we have in stock!");
    }
    $('.city-change').on('change', function () {
        $.ajax(
            {
                url: '/checkout/GetShippingCost?CityId=' + $('.city-change').val(),
                type: 'GET',
                data: "",
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    $("#partialDiv").html("Rs." + data);
                    var subtotal = $("#subtotal-cost").html().split(" ").pop();
                    var total = parseInt(subtotal) + parseInt(data);
                    $("#total-cost").html("Rs." + total);
                    
                },
                error: function () {
                    alert("error");
                }
            });
    });
    $('#paymentType').on('click', function () {
        debugger; 
        var checked = document.querySelector('[name="PaymentMethod"]:checked');
        if (checked.value == "Direct bank transfer") {
            $(".checkout-main-area").hide();
            popup();
        } else {
            $("#myFormID").submit();
        }
    });

   
     

    function popup() {
        var total = $("#totalSubTotal").val();
        var email = $("#Email").val();
        var phoneNumber = $("#PhoneNumber").val();
        console.log("apiValues", total, email, phoneNumber, $("#dateunique").val(), $("#Email").val(), $("#PhoneNumber").val() );
        debugger;
        if (phoneNumber != null || email != null || total != null) {
            DirectPayCardPayment.init({
                container: 'card_container', //<div id="card_container"></div>
                merchantId: 'EM14096', //your merchant_id EM14096
                amount: total,
                refCode: $("#dateunique").val(), //unique referance code form merchant
                currency: 'LKR',
                type: 'ONE_TIME_PAYMENT',
                customerEmail: $("#Email").val(),
                customerMobile: $("#PhoneNumber").val(),
                description: 'Medco Products',  //product or service description
                debug: true,
                responseCallback: responseCallback,
                errorCallback: errorCallback,
                logo: 'https://test.com/directpay_logo.png',
                apiKey: '028861b9e9f28b020471dd57dd71718b18118e090ae0d221d537b7b6f9c8d2e8' //028861b9e9f28b020471dd57dd71718b18118e090ae0d221d537b7b6f9c8d2e8
            });
        } else {
            alert("Phone Number or Email Empty");
        }
        //response callback.
        function responseCallback(result) {
            console.log("successCallback-Client", result);
            $("#myFormID").submit();
            //alert(JSON.stringify(result));
        }

        //error callback
        function errorCallback(result) {
            console.log("successCallback-Client", result);
            alert(JSON.stringify(result));
        }
    }
});