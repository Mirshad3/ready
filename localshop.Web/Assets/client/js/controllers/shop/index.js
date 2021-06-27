$(document).ready(function () {
    var filterViewMode = $('#filterViewMode').val();

    // Sort right bar
    function redirect() {
        var view = $('#viewSort').children('option:selected').val();
        var sortBy = $('#sortBy').children('option:selected').val();

        if (view == 20 && sortBy == '@SortByEnums.Default') {
            window.location.href = `/shop`;
        } else if (view != 20 && sortBy == '@SortByEnums.Default') {
            window.location.href = `/shop?view=${view}`;
        } else if (view == 20 && sortBy != '@SortByEnums.Default') {
            window.location.href = `/shop?sortBy=${sortBy}`;
        } else {
            window.location.href = `/shop?view=${view}&sortBy=${sortBy}`;
        }
    }

    if (filterViewMode == 'List') {
        $('#shop2').tab('show');
    } else {
        $('#shop1').tab('show');
    }

    $('#viewSort').on('change', function () {
        redirect();
    });
    $('#sortBy').on('change', function () {
        redirect();
    });

    $('#shop1').on('click', function (e) {
        e.preventDefault();
        $(this).prev().val('Default');
        $(this).closest('form').submit();
    });
    $('#shop2').on('click', function (e) {
        e.preventDefault();
        $(this).prev().prev().val('List');
        $(this).closest('form').submit();
    });
});