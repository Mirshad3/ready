$(document).ready(function () {
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

    // gt, gte, lt, lte, notequalto extra validators
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
                uploadUrl: '/ckfinder/connector?command=QuickUpload&type=Files&responseType=json'
            },
            toolbar: ['ckfinder', 'imageUpload', '|', 'heading', '|', 'bold', 'italic', 'link', 'bulletedList', 'numberedList', 'blockQuote', '|', 'undo', 'redo', '|', 'insertTable', 'mediaEmbed']
        })
        .catch(function (error) {
            console.error(error);
        });

    // Setup choose images
    var listImages = $('#Images').val() === "" ? [] : $('#Images').val().split('@');
    $('#productImages').on('click', '.clear-image', function (e) {
        e.preventDefault();

        let idx = listImages.indexOf($(this).siblings('img').attr('src'));
        if (idx >= 0) {
            listImages.splice(idx, 1);
        }

        $('#Images').val(listImages.join('@'));

        $(this).parent().remove();
    });
    $('.btn-choose-images').on('click', function (e) {
        e.preventDefault();
        CKFinder.modal({
            resourceType: 'Images',
            chooseFiles: true,
            onInit: function (finder) {
                finder.on('files:choose', function (evt) {
                    var files = evt.data.files;
                    var chosenFiles = '';
                    files.forEach(function (file, i) {
                        var newUrl = '/ckfinder' + file.get('url').split('ckfinder')[1];

                        console.log(listImages);
                        console.log(listImages.indexOf(newUrl));

                        if (listImages.indexOf(newUrl) < 0) {
                            listImages.push(newUrl);

                            $('#Images').val(listImages.join('@'));

                            $('#productImages').append(`
                                <div class="position-relative m-2">
                                    <img src="${newUrl}" />
                                    <a href="javascript:void(0)" class="position-absolute px-1 bg-light clear-image" style="top:0;left:0;"><span class="fas fa-times"></span></a>
                                </div>`);
                        }
                    });
                });
                finder.on('file:choose:resizedImage', function (evt) {
                    document.getElementById('url').value = evt.data.resizedUrl;
                });
            }
        });
    });
});