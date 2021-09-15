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
});