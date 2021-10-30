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
    
    var addSuccess = $('#addSuccess').val();
    if (addSuccess == "Success") {
        toastr['success']("Added!");
    } else if (addSuccess == "Failed") {
        toastr['error']("Something went wrong, check if category is existed!");
    }

    var table = $('#datatable').DataTable();
    $('#isActive').change(function () {
        cb = $(this);
        cb.val(cb.prop('checked'));
    });
    // Delete category
    table.on('click', '.ls-category-delete', function (e) {
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
                    url: "/admin/category/delete",
                    data: { categoryId: $tr.data("category-id") },
                    success: function (response) {
                        if (response.success) {
                            toastr["success"]("Category deleted!");
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

    // Edit category
    var $editTr;

    table.on('click', '.ls-category-edit', function (e) {
        var $tr = $(this).closest('tr');
        $editTr = $tr;

        $.ajax({
            type: "GET",
            dataType: "json",
            url: "/admin/category/edit",
            data: { categoryId: $tr.data("category-id") },
            success: function (response) {
                if (response.success) {
                    $('#categoryId').val(response.category.Id);
                    $('#categoryName').val(response.category.Name);
                    $('#isActive').val(response.category.IsActive);
                    
                } else {
                    toastr["error"]("Something went wrong!");
                }
            },
            error: function () {
                toastr["error"]("Something went wrong!");
            }
        });
    });

    $('#editCategory').on('submit', function (e) {
        e.preventDefault();
        $.ajax({
            type: "POST",
            dataType: "json",
            url: "/admin/category/edit",
            data: {
                Id: $('#categoryId').val(),
                Name: $('#categoryName').val(),
                IsActive: $('#isActive').val()
            },
            success: function (response) {
                if (response.success) {

                    var index = table.row($editTr).index();
                    table.cell({ row: index, column: 0 }).data(response.category.Name);

                    toastr["success"]("Saved!");
                } else {
                    toastr["error"]("Something went wrong!");
                }
            },
            error: function () {
                toastr["error"]("Something went wrong!");
            },
            complete: function () {
                $('#editModal').modal('toggle');
            }
        });
    });
});