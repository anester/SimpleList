$(function () {
    $.get('/List/Create/anester', null, function (data) {
        $('#CreateBtn').html(data);
        $('input[type="submit"]', $('#CreateBtn')).click(function () {
            var name = $('[name="UserListName"]', $('#CreateBtn')).val(),
                desc = $('[name="UserListName"]', $('#CreateBtn')).val();

            $.post('/List/Create/anester', { UserListName: name, Description: desc }, function (data) {

            });
        });
    }).error(function (e) {
        alert(e);
    });
});