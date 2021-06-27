$(function () {
    // Notification for added
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
    }

    var saveSuccess = $('#saveSuccess').val();
    var errorMessge = $('#errorMessage').val();
    if (saveSuccess == "true") {
        toastr['success']("Success!");
    }
    if (saveSuccess == "false") {
        if (errorMessge != '') {
            toastr['error'](errorMessge);
        }
        else {
            toastr['error']('Something went wrong!');
        }
    }

    // Add validator parsley
    window.Parsley.addValidator('decimal', {
        validateString: function (value) {
            return new RegExp('^[0-9]+(.[0-9]{2})?$', 'g').test(value);
        },
        messages: {
            en: 'This value should be an integer or decimal (.00)',
        }
    });

    // Load image
    var backgroundImage = $('#BackgroundImage'), productImage = $('#ProductImage');
    if (backgroundImage.val()) {
        $('#background-image').empty().append(`
            <div class="position-relative m-2">
                <img src="${backgroundImage.val()}" style="max-width:100%"/>
                <a href="javascript:void(0)" class="position-absolute px-1 bg-light clear-image" style="top:0;left:0;"><span class="fas fa-times"></span></a>
            </div>`);
    }
    if (productImage.val()) {
        $('#product-image').empty().append(`
            <div class="position-relative m-2">
                <img src="${productImage.val()}" style="max-width:100%"/>
                <a href="javascript:void(0)" class="position-absolute px-1 bg-light clear-image" style="top:0;left:0;"><span class="fas fa-times"></span></a>
            </div>`);
    }

    // Set up choose backgound image
    $('#background-image').on('click', '.clear-image', function (e) {
        e.preventDefault();
        backgroundImage.val('');
        $(this).parent().remove();
    });
    $('.btn-choose-background-image').on('click', function (e) {
        e.preventDefault();
        CKFinder.modal({
            resourceType: 'Images',
            chooseFiles: true,
            onInit: function (finder) {
                finder.on('files:choose', function (evt) {
                    var file = evt.data.files.first();

                    var newUrl = '/ckfinder' + file.get('url').split('ckfinder')[1];

                    backgroundImage.val(newUrl);

                    $('#background-image').empty().append(`
                        <div class="position-relative m-2">
                            <img src="${newUrl}" style="max-width:100%"/>
                            <a href="javascript:void(0)" class="position-absolute px-1 bg-light clear-image" style="top:0;left:0;"><span class="fas fa-times"></span></a>
                        </div>`);
                });
                finder.on('file:choose:resizedImage', function (evt) {
                    var newUrl = '/ckfinder' + evt.data.resizedUrl.split('ckfinder')[1];

                    backgroundImage.val(newUrl);

                    $('#background-image').empty().append(`
                        <div class="position-relative m-2">
                            <img src="${newUrl}" style="max-width:100%"/>
                            <a href="javascript:void(0)" class="position-absolute px-1 bg-light clear-image" style="top:0;left:0;"><span class="fas fa-times"></span></a>
                        </div>`);
                });
            }
        });
    });

    // Set up choose product image
    $('#product-image').on('click', '.clear-image', function (e) {
        e.preventDefault();
        productImage.val('');
        $(this).parent().remove();
    });
    $('.btn-choose-product-image').on('click', function (e) {
        e.preventDefault();
        CKFinder.modal({
            resourceType: 'Images',
            chooseFiles: true,
            onInit: function (finder) {
                finder.on('files:choose', function (evt) {
                    var file = evt.data.files.first();

                    var newUrl = '/ckfinder' + file.get('url').split('ckfinder')[1];

                    productImage.val(newUrl);

                    $('#product-image').empty().append(`
                        <div class="position-relative m-2">
                            <img src="${newUrl}" style="max-width:100%"/>
                            <a href="javascript:void(0)" class="position-absolute px-1 bg-light clear-image" style="top:0;left:0;"><span class="fas fa-times"></span></a>
                        </div>`);
                });
                finder.on('file:choose:resizedImage', function (evt) {
                    var newUrl = '/ckfinder' + evt.data.resizedUrl.split('ckfinder')[1];

                    productImage.val(newUrl);

                    $('#product-image').empty().append(`
                        <div class="position-relative m-2">
                            <img src="${newUrl}" style="max-width:100%"/>
                            <a href="javascript:void(0)" class="position-absolute px-1 bg-light clear-image" style="top:0;left:0;"><span class="fas fa-times"></span></a>
                        </div>`);
                });
            }
        });
    });
})