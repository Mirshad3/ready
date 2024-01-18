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
                    //$("#partialDiv").html("Rs." + data);
                    var subtotal = $("#subtotal-cost").html().split(" ").pop();
                    var total = parseInt(subtotal) + /*parseInt(data)*/ 375;
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
        //var subTotal = parseFloat(total) + 375;
        var email = $("#Email").val();
        var phoneNumber = $("#PhoneNumber").val();
        if (phoneNumber.startsWith('0')) {
            // Remove the leading '0' and add '+94' to the beginning
            phoneNumber = '+94' + phoneNumber.substring(1);
        }
        var json = {
            merchant_id: "FM11605",
            amount: total,
            type: "ONE_TIME",
            order_id: $("#dateunique").val(),
            currency: "LKR",
            response_url: "https://test.com/response-endpoint",
            first_name: $("#FirstName").val(),
            last_name: $("#LastName").val(),
            email: email,
            phone: phoneNumber,
            logo: "",
        };
        console.log("apiValues", total, email, phoneNumber, $("#dateunique").val(), $("#Email").val(), $("#PhoneNumber").val());
        debugger;
        if (phoneNumber != null || email != null || total != null) {
            ////DirectPayCardPayment.init({
            ////    container: 'card_container', //<div id="card_container"></div>
            ////    merchantId: 'FM11605', //your merchant_id EM14096
            ////    amount: total,
            ////    refCode: $("#dateunique").val(), //unique referance code form merchant
            ////    currency: 'LKR',
            ////    type: 'ONE_TIME_PAYMENT',
            ////    customerEmail: $("#Email").val(),
            ////    customerMobile: phoneNumber,
            ////    description: 'Medco Products',  //product or service description
            ////    debug: true,
            ////    responseCallback: responseCallback,
            ////    errorCallback: errorCallback,
            ////    logo: 'https://test.com/directpay_logo.png',
            ////    apiKey: 'f5c6abd22db203d3b6cd8019369621605bd4d711cdc7a5c715b44b4ed73cca83' //028861b9e9f28b020471dd57dd71718b18118e090ae0d221d537b7b6f9c8d2e8
            ////});
            var dataString = CryptoJS.enc.Base64.stringify(
                CryptoJS.enc.Utf8.parse(JSON.stringify(json))
            );
            var hmacDigest = CryptoJS.HmacSHA256(
                dataString,
                "f5c6abd22db203d3b6cd8019369621605bd4d711cdc7a5c715b44b4ed73cca83"
            );

            var dp = new DirectPayIpg.Init({
                signature: hmacDigest,
                dataString: dataString,
                stage: "PROD", //DEV
                container: "card_container",
            });
            dp.doInContainerCheckout()
                .then((data) => {
                    console.log("client-res", JSON.stringify(data));                   
                    alert(JSON.stringify(data));
                    $("#myFormID").submit();
                })
                .catch((error) => {
                    console.log("client-error", JSON.stringify(error));
                    alert(JSON.stringify(error));
                });
            

        } else {
            alert("Phone Number or Email Empty");
        }
        //////response callback.
        ////function responseCallback(result) {
        ////    console.log("successCallback-Client", result);
        ////    $("#myFormID").submit();
        ////    //alert(JSON.stringify(result));
        ////}

        //////error callback
        ////function errorCallback(result) {
        ////    console.log("successCallback-Client", result);
        ////    alert(JSON.stringify(result));
        ////}

        //popup IPG
        
    }
    
    

    

    
});

 
 
 