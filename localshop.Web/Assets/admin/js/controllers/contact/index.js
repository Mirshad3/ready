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

    var table = $('#datatable').DataTable({
        "order": []
    });

    // Read
    table.on('click', 'tbody tr', function () {
        var $tr = $(this);
        var contactId = $(this).data('contactid');
        $.ajax({
            type: "GET",
            dataType: "json",
            url: "/admin/contact/getContact",
            data: { contactId },
            success: function (response) {
                if (response.success) {
                    $tr.removeClass("bg-soft-blue");
                    $('#messageModalTitle').html(`${response.contact.Name} (${response.contact.Email}) <br /><span class="text-secondary">${response.contact.Subject}</span>`);
                    $('#messageModalBody').html('<p style="word-wrap: break-word;">' + response.contact.Message + '</p>');
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
    table.on('click', '.ls-contact-delete', function (e) {
        e.stopPropagation();
        var $tr = $(this).closest('tr');
        var contactId = $tr.data('contactid');

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
                    url: "/admin/contact/delete",
                    data: { contactId },
                    success: function (response) {
                        if (response.success) {
                            toastr["success"]("Message deleted!");
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
        });
    });
});