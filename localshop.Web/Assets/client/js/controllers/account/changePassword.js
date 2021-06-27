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
    var errorMessage = $('#errorMessage').val();
    if (saveSuccess == "true") {
        toastr["success"]("Saved!");
    }

    if (saveSuccess == "false") {
        toastr["error"](errorMessage);
    }
});