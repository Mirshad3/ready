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

    var errorMessage = $('#errorMessage').val();
    if (errorMessage == "image") {
        toastr['error']("Something went wrong, you need to add banner image!");
    }
    if (errorMessage == "true") {
        toastr['error']("Something went wrong!");
    }

    // Load image
    var bannerImage = $('#Image');
    if (bannerImage.val()) {
        $('#banner-image').empty().append(`
            <div class="position-relative m-2">
                <img src="${bannerImage.val()}" style="max-width:100%"/>
                <a href="javascript:void(0)" class="position-absolute px-1 bg-light clear-image" style="top:0;left:0;"><span class="fas fa-times"></span></a>
            </div>`);
    }

    // Set up choose banner image
    $('#banner-image').on('click', '.clear-image', function (e) {
        e.preventDefault();
        bannerImage.val('');
        $(this).parent().remove();
    });
    $('.btn-choose-banner-image').on('click', function (e) {
        e.preventDefault();
        CKFinder.modal({
            resourceType: 'Images',
            chooseFiles: true,
            onInit: function (finder) {
                finder.on('files:choose', function (evt) {
                    var file = evt.data.files.first();

                    var newUrl = '/ckfinder' + file.get('url').split('ckfinder')[1];

                    bannerImage.val(newUrl);

                    $('#banner-image').empty().append(`
                        <div class="position-relative m-2">
                            <img src="${newUrl}" style="max-width:100%"/>
                            <a href="javascript:void(0)" class="position-absolute px-1 bg-light clear-image" style="top:0;left:0;"><span class="fas fa-times"></span></a>
                        </div>`);
                });
                finder.on('file:choose:resizedImage', function (evt) {
                    var newUrl = '/ckfinder' + evt.data.resizedUrl.split('ckfinder')[1];

                    bannerImage.val(newUrl);

                    $('#banner-image').empty().append(`
                        <div class="position-relative m-2">
                            <img src="${newUrl}" style="max-width:100%"/>
                            <a href="javascript:void(0)" class="position-absolute px-1 bg-light clear-image" style="top:0;left:0;"><span class="fas fa-times"></span></a>
                        </div>`);
                });
            }
        });
    });
});