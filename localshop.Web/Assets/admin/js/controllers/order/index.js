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

    var orderStatusNames = ["Pending", "Approved", "Delivered", "Cancelled", "ReadyToShip", "Picked"];

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
            url: "/admin/order/getOrderStatus",
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

    $('#ls-btn-save').on('click', function () {
        var selected = $('#select-order-status').val();
        $.ajax({
            type: "POST",
            dataType: "json",
            url: "/admin/order/updateStatus",
            data: { orderId: $tr.data('orderid'), statusName: selected },
            success: function (response) {
                if (response.success) {
                    var $td = $tr.find('.order-status');
                    if (selected == orderStatusNames[0]) {
                        $td.html(`<span class="text-warning">${selected}</span>`);
                    }
                    if (selected == orderStatusNames[1]) {
                        $td.html(`<span class="text-primary">${selected}</span>`);
                    }
                    if (selected == orderStatusNames[2]) {
                        $td.html(`<span class="text-success">${selected}</span>`);
                    }
                    if (selected == orderStatusNames[3]) {
                        $td.html(`<span class="text-danger">${selected}</span>`);
                    }
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
JsBarcode(".barcode").init();

$('.PrintDiv').on('click', function () {
    var divContents = document.getElementById("printdivcontent").innerHTML;
    var printWindow = window.open('', '', '');
    printWindow.document.write('<html><head><title>Print DIV Content</title>');
    printWindow.document.write('</head><body >');
    printWindow.document.write(divContents);
    printWindow.document.write('</body></html>');
    printWindow.document.close();
    printWindow.print();
});
