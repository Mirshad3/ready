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
        toastr['error']("Something went wrong, check if city is existed!");
    }

    var table = $('#datatable').DataTable();

    // Delete city
    table.on('click', '.ls-city-delete', function (e) {
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
                    url: "/admin/city/delete",
                    data: { CityId: $tr.data("city-id") },
                    success: function (response) {
                        if (response.success) {
                            toastr["success"]("City deleted!");
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

    // Edit city
    var $editTr;

    table.on('click', '.ls-city-edit', function (e) {
        var $tr = $(this).closest('tr');
        $editTr = $tr;

        $.ajax({
            type: "GET",
            dataType: "json",
            url: "/admin/city/edit",
            data: { cityId: $tr.data("city-id") },
            success: function (response) {
                if (response.success) {
                    $('#cityId').val(response.city.Id);
                    $('#cityName').val(response.city.Name);
                } else {
                    toastr["error"]("Something went wrong!");
                }
            },
            error: function () {
                toastr["error"]("Something went wrong!");
            }
        });
    });

    $('#editcity').on('submit', function (e) {
        e.preventDefault();
        $.ajax({
            type: "POST",
            dataType: "json",
            url: "/admin/city/edit",
            data: {
                Id: $('#cityId').val(),
                Name: $('#cityName').val()
            },
            success: function (response) {
                if (response.success) {

                    var index = table.row($editTr).index();
                    table.cell({ row: index, column: 0 }).data(response.city.Name);

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