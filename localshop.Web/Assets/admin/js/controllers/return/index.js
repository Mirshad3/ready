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

    var orderStatusNames = ["Pending", "Approved", "Delivered",  "ReadyToShip", "Picked"];

    var table = $('#datatable').DataTable({
        "order": []
    });

    var currentStatus;
    var $tr;
    table.on('click', '.ls-btn-update-status', function (e) {
        e.preventDefault();

        $tr = $(this).closest('tr');
        currentOrderId = $tr.data('orderid');
        $.ajax({
            type: "GET",
            dataType: "json",
            url: "/admin/return/getReturnStatus",
            data: { orderId: $tr.data('orderid') },
            success: function (response) {
                if (response.success) {
                    currentStatus = orderStatusNames.find(os => os == response.orderStatus);

                    if (currentStatus) {
                        $('#select-order-status').val(currentStatus);
                    }
                    else {
                        toastr["error"]("Something went wrong!");
                    }

                } else {
                    toastr["error"]("Something went wrong!");
                }
            },
            error: function () {
                toastr["error"]("Something went wrong!");
            }
        });
    });
    $('#rt-btn-save').on('click', function () {
        var selected = $('#select-order-status').val();
      
        console.log("mmm", $tr.data('orderid'))
        $.ajax({
            type: "POST",
            dataType: "json",
            url: "/admin/return/updateStatus",
            data: { orderId: $tr.data('orderid'), statusName: selected },
            success: function (response) {
                if (response.success) {
                   
                    toastr["success"]("Updated!");
                } else {
                    toastr["error"]("Something went wrong!");
                }
            },
            error: function () {
                toastr["error"]("Something went wrong!");
            }
        });
        $('#editModal').modal('toggle');
    });
   
}); 

 
