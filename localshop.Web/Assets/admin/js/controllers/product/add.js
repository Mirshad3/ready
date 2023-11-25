﻿$(document).ready(function () {
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

    // Set up end discount date
    if ($('#DiscountPrice').val() == '') {
        $('#end-discount-date').hide();
    }
    $('#DiscountPrice').on('change', function () {
        if ($(this).val() != '') {
            $('#end-discount-date').slideDown(500, function () {
                $(this).find('input').focus();
            });
        } else {
            $('#end-discount-date').slideUp(500, function () {
                $(this).find('input').val('');
            });
        }
    });

    // Add validator parsley
    var parseRequirement = function (requirement) {
        if (isNaN(+requirement))
            return parseFloat(jQuery(requirement).val());
        else
            return +requirement;
    };

    window.Parsley.addValidator('lt', {
        validateString: function (value, requirement) {
            return parseFloat(value) < parseRequirement(requirement);
        },
        messages: {
            en: 'This value should be a smaller than regular price',
        },
        priority: 32
    });

    window.Parsley.addValidator('decimal', {
        validateString: function (value) {
            return new RegExp('^[0-9]+(.[0-9]{2})?$', 'g').test(value);
        },
        messages: {
            en: 'This value should be an integer or decimal (.00)',
        }
    });

    // Create editor
    ClassicEditor
        .create(document.querySelector('#ShortDesciption'), {
            toolbar: ['heading', '|', 'bold', 'italic', 'link', 'bulletedList', 'numberedList', 'blockQuote', '|', 'undo', 'redo']
        })
        .catch(function (error) {
            console.error(error);
        });
    ClassicEditor
        .create(document.querySelector('#LongDescription'), {
            ckfinder: {
                uploadUrl: '/ckfinder/connector/azstore/connector?command=QuickUpload&type=Files&responseType=json'
            },
            toolbar: ['ckfinder', 'imageUpload', '|', 'heading', '|', 'bold', 'italic', 'link', 'bulletedList', 'numberedList', 'blockQuote', '|', 'undo', 'redo', '|', 'insertTable', 'mediaEmbed']
        })
        .catch(function (error) {
            console.error(error);
        });


    // Setup choose images
    var listImages = [];
    $('#productImages').on('click', '.clear-image', function (e) {
        e.preventDefault();

        let idx = listImages.indexOf($(this).siblings('img').attr('src'));
        if (idx >= 0) {
            listImages.splice(idx, 1);
        }

        $('#Images').val(listImages.join('@'));

        $(this).parent().remove();
    });
    ////$('.btn-choose-images').on('click', function (e) {
    ////    e.preventDefault();

    ////    CKFinder.modal({
    ////        resourceType: 'Image',
    ////        chooseFiles: true,
    ////        onInit: function (finder) {
    ////            finder.on('files:choose', function (evt) {
    ////                var files = evt.data.files;
    ////                files.forEach(function (file, i) {
    ////                    var newUrl = file.getUrl();

    ////                    if (listImages.indexOf(newUrl) < 0) {
    ////                        listImages.push(newUrl);

    ////                        $('#Images').val(listImages.join('@'));

    ////                        $('#productImages').append(`
    ////                    <div class="position-relative m-2">
    ////                        <img src="${newUrl}" />
    ////                        <a href="javascript:void(0)" class="position-absolute px-1 bg-light clear-image" style="top:0;left:0;"><span class="fas fa-times"></span></a>
    ////                    </div>`);
    ////                    }
    ////                });
    ////            });
    ////            finder.on('file:choose:resizedImage', function (evt) {
    ////                document.getElementById('url').value = evt.data.resizedUrl;
    ////            });
    ////        },
    ////        filebrowserBrowseUrl: '/ckfinder/connector/azstore/index.html?type=Images',
    ////        filebrowserUploadUrl: '/ckfinder/connector/azstore/connector?command=QuickUpload&type=Images',
    ////    });

    ////});

    $('.btn-choose-images').on('click', function (e) {
        e.preventDefault();

        // Trigger the click event on the hidden file input
        $('#fileInput').click();
    });

    // Handle file input change event
    $('#fileInput').on('change', function () {
        var files = this.files;

        // Iterate through the selected files
        for (var i = 0; i < files.length; i++) {
            var file = files[i];

            // Perform the upload using AJAX
            uploadFile(file);
        }
    });

    // Function to upload a file to Azure Blob Storage
    function uploadFile(file) {
        var formData = new FormData();
        formData.append('file', file);

        $.ajax({
            url: '/File/Upload', // Update with your controller action for file upload
            type: 'POST',
            data: formData,
            contentType: false,
            processData: false,
            success: function (response) {
                if (response.success) {
                    // Handle success: update UI with response.url
                    console.log('File uploaded successfully:', response.url);

                    // Add the uploaded image to the productImages div
                    var newUrl = response.url;

                    if (listImages.indexOf(newUrl) < 0) {
                        listImages.push(newUrl);

                        $('#Images').val(listImages.join('@'));

                        $('#productImages').append(`
                <div class="position-relative m-2">
                    <img src="${newUrl}" />
                    <a href="javascript:void(0)" class="position-absolute px-1 bg-light clear-image" style="top:0;left:0;"><span class="fas fa-times"></span></a>
                </div>`);
                    }
                } else {
                    // Handle failure: display an error message or take appropriate action
                    console.error('File upload failed:', response.message);
                }
            },

            error: function (error) {
                // Handle the error response if needed
                console.error('Error uploading file:', error);
            }
        });
    }

});