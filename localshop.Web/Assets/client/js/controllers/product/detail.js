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

    var saveSuccess = $('#saveSuccess').val();
    var activePanel = $('#activePanel').val();

    if (saveSuccess == 'false') {
        toastr["error"]("Something went wrong!");
    }
    if (activePanel == 'description') {
        $('#des1').tab('show');
    }
    if (activePanel == 'specification') {
        $('#des2').tab('show');
    }
    if (activePanel == 'review') {
        $('#des3').tab('show');
    }

    $('.ls-delete-review').on('click', function (e) {
        e.preventDefault();
        var $review = $(this).closest('.dec-review-wrap');
        $.ajax({
            type: "POST",
            dataType: "json",
            url: "/review/delete",
            data: {
                productId: $(this).data('productid'),
            },
            success: function (response) {
                if (response.success) {
                    $review.remove();
                    $('#review-box').removeClass('d-none');
                } else {
                    toastr["error"]("Something went wrong!");
                }
            },
            error: function () {
                toastr["error"]("Something went wrong!");
            }
        });
    });
});