// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function () {
    $("#loaderbody").addClass('hide');

    $(document).bind('ajaxStart', function () {
        $("#loaderbody").removeClass('hide');
    }).bind('ajaxStop', function () {
        $("#loaderbody").addClass('hide');
    });
});

showInPopup = (url, title) => {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            $('#coinModal .modal-body').html(res);
            $('#coinModal .modal-title').html(title);
            $('#coinModal').modal('show');
            // to make popup draggable
            $('.modal-dialog').draggable({
                handle: ".modal-header"
            });
        }
    })
}

jQueryAjaxPost = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) {
                    $('#viewAllCoin').html(res.html)
                    $('#coinModal .modal-body').html('');
                    $('#coinModal .modal-title').html('');
                    $('#coinModal').modal('hide');
                }
                else
                    $('#coinModal .modal-body').html(res.html);
            },
            error: function (err) {
                console.log(err)
            }
        })
        //to prevent default form submit event
        return false;
    } catch (ex) {
        console.log(ex)
    }
}

jQueryAjaxDelete = form => {
    if (confirm('Вы уверены, что хотите удалить эту запись?')) {
        try {
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    $('#viewAllCoin').html(res.html);
                },
                error: function (err) {
                    console.log(err)
                }
            })
        } catch (ex) {
            console.log(ex)
        }
    }

    //prevent default form submit event
    return false;
}


showInPopupDrink = (url, title) => {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            $('#drinkModal .modal-body').html(res);
            $('#drinkModal .modal-title').html(title);
            $('#drinkModal').modal('show');
            // to make popup draggable
            $('.modal-dialog').draggable({
                handle: ".modal-header"
            });
        }
    })
}

jQueryAjaxPostDrink = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) {
                    $('#viewAllDrink').html(res.html)
                    $('#drinkModal .modal-body').html('');
                    $('#drinkModal .modal-title').html('');
                    $('#drinkModal').modal('hide');
                }
                else
                    $('#drinkModal .modal-body').html(res.html);
            },
            error: function (err) {
                console.log(err)
            }
        })
        //to prevent default form submit event
        return false;
    } catch (ex) {
        console.log(ex)
    }
}

jQueryAjaxDeleteDrink = form => {
    if (confirm('Вы уверены, что хотите удалить эту запись?')) {
        try {
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    $('#viewAllDrink').html(res.html);
                },
                error: function (err) {
                    console.log(err)
                }
            })
        } catch (ex) {
            console.log(ex)
        }
    }

    //prevent default form submit event
    return false;
}

DeleteImageCoin = (url, value) => {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            if (confirm('Вы уверены, что хотите удалить выбранный предварительный просмотр?')) {
                $.post(url, { value: value }, function (result) { 
                    var divCoinImage = $('#coin-image-administration');
                    divCoinImage.remove();

                    var ExistsThumbnailFile = $('#ExistsThumbnailFile');
                    ExistsThumbnailFile.val(false);
                });
            }
        }
    })
}

DeleteImageDrink = (url, id) => {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            if (confirm('Вы уверены, что хотите удалить выбранный предварительный просмотр?')) {
                $.post(url, { id: id }, function (result) {
                    var divDrinkImage = $('#drink-image-administration');
                    divDrinkImage.remove();

                    var ExistsThumbnailFile = $('#ExistsThumbnailFile');
                    ExistsThumbnailFile.val(false);
                });
            }
        }
    })
}