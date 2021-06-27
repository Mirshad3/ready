$('document').ready(function () {
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

    var table = $('#datatable').DataTable({
        "order": []    
    });

    // Delete user
    table.on('click', '.ls-user-delete', function (e) {
        var $tr = $(this).closest('tr');

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
                    url: "/admin/user/delete",
                    data: { userId: $tr.data("user-id") },
                    success: function (response) {
                        if (response.success) {
                            toastr["success"]("User deleted!");
                            table.row($tr).remove().draw();
                        } else {
                            toastr["error"]("Something went wrong!");
                        }
                    },
                    error: function () {
                        toastr["error"]("Something went wrong!");
                    },
                });
            }
        })
    });

    // Change role
    $('.user-role').on('change', function () {
        var $tr = $(this).closest('tr');

        $.ajax({
            type: "POST",
            dataType: "json",
            url: "/admin/user/changerole",
            data: { userId: $tr.data("user-id"), roleName: $(this).val() },
            success: function (response) {
                if (response.success) {
                    toastr["success"]("Changed role!");
                } else {
                    toastr["error"]("Something went wrong!");
                }
            },
            error: function () {
                toastr["error"]("Something went wrong!");
            },
        })
    });
});