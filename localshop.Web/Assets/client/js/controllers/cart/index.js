$(document).ready(function () {
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
        "timeOut": "2000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    };

    var outOfStock = $('#outOfStock').val();
    var emptyMessage = $('#emptyMessage').val();
    var orderSuccess = $('#orderSuccess').val();

    if (outOfStock == "true") {
        toastr["warning"]("Some product is out of stock, so you can only set max quantity we have in stock!");
    }

    if (emptyMessage == "true") {
        toastr["info"]("Your cart is empty!");
    }

    if (orderSuccess == "false") {
        toastr["error"]("Something wrong! Your order isn't placed!");
    }

    $('#cart-btn-update').off('click').on('click', function () {
        var $trs = $('.cart-table-content tbody').children();
        var model = [];

        $.each($trs, function (index, value) {
            let $qty = $(value).find('.cart-plus-minus-box');

            let product = {
                id: $qty.data('productid'),
                quantity: $qty.val()
            }

            model.push(product);
        });

        $.ajax({
            type: "POST",
            dataType: "json",
            url: "/cart/updateCart",
            data: {
                model: JSON.stringify(model)
            },
            success: function (response) {
                if (response.success) {
                    window.location.href = "/cart";
                } else {
                    toastr["error"]("Something went wrong!");
                }
            },
            error: function () {
                toastr["error"]("Something went wrong!");
            }
        })
    });
});