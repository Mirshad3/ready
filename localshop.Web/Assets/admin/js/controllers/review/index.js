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
        "timeOut": "2000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }

    var saveSuccess = $('#saveSuccess').val();
    if (saveSuccess == "Success") {
        toastr['success']("Saved!");
    }

    var table = $('#datatable').DataTable({
        "order": []
    });


    function toJavaScriptDate(value) {
        var pattern = /Date\(([^)]+)\)/;
        var results = pattern.exec(value);
        var dt = new Date(parseFloat(results[1]));
        return `${dt.getDate()}/${dt.getMonth() + 1}/${dt.getFullYear()} ${dt.getHours()}:${dt.getMinutes()}:${dt.getSeconds()}`;
    }

    // Approve in waiting
    table.on('click', '.ls-review-approve', function () {
        var $tr = $(this).closest('tr');
        var userId = $(this).data('userid');
        var productId = $(this).data('productid');

        $.ajax({
            type: "POST",
            dataType: "json",
            url: "/admin/review/approve",
            data: { userId, productId },
            success: function (response) {
                if (response.success) {
                    toastr["success"]("Review approved!");
                    table.row($tr).remove().draw();
                } else {
                    toastr["error"]("Something went wrong!");
                }
            },
            error: function () {
                toastr["error"]("Something went wrong!");
            }
        });
    });

    // View
    table.on('click', '.ls-review-detail', function () {
        var userId = $(this).data('userid');
        var productId = $(this).data('productid');
        $.ajax({
            type: "GET",
            dataType: "json",
            url: "/admin/review/getReview",
            data: { userId, productId },
            success: function (response) {
                if (response.success) {
                    $('#detailModalTitle').text(response.model.Name);

                    var $ul = $('#detailModalRating').find('ul');
                    $ul.empty();

                    var rating = response.model.Review.Rating;
                    for (var i = 0; i < rating; i++) {
                        $ul.append('<li class="mdi mdi-star"></li>')
                    }
                    for (var i = rating; i < 5; i++) {
                        $ul.append('<li class="mdi mdi-star-outline"></li>')
                    }

                    $('#detailModal').find('.modal-body').text(response.model.Review.Body);
                    $('#detailModal').find('.modal-footer span').text(toJavaScriptDate(response.model.Review.DateAdded));
                    $('#detailModal').find('.modal-footer a').attr("href", `/product/detail/${response.model.Product.MetaTitle}`);
                } else {
                    toastr["error"]("Something went wrong!");
                }
            },
            error: function () {
                toastr["error"]("Something went wrong!");
            }
        });
    });

    // Delete
    table.on('click', '.ls-review-delete', function () {
        var $tr = $(this).closest('tr');
        var userId = $(this).data('userid');
        var productId = $(this).data('productid');

        Swal({
            title: 'Are you sure?',
            text: "You won't be able to revert this!",
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, delete it!'
        }).then((result) => {
            if (result.value) {
                $.ajax({
                    type: "POST",
                    dataType: "json",
                    url: "/admin/review/delete",
                    data: { userId, productId },
                    success: function (response) {
                        if (response.success) {
                            toastr["success"]("Review deleted!");
                            table.row($tr).remove().draw();
                        } else {
                            toastr["error"]("Something went wrong!");
                        }
                    },
                    error: function () {
                        toastr["error"]("Something went wrong!");
                    }
                });
            }
        });
    });
});