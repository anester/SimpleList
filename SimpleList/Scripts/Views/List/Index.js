﻿$(function () {
    var enterNewItem = function (e) {
        if (e.keyCode == 13) {
            var $input = $(this),
                $conlistdiv = $input.closest('.userlist'),
                userlistid = $conlistdiv.attr("data-userlistid"),
                today = new Date();

            $.post('/ListItem/Create', {
                UserListId: userlistid,
                ListItemName: $input.val(),
                DateEntered: (today.getMonth() + 1) + "/" + today.getDate() + "/" + today.getFullYear(),
                Description: ''
            }, function () {
                $.get('/List/ListPart/' + userlistid, function (html) {
                    var $newpanel = $(html);
                    $('.list-item-new', $newpanel).keyup(enterNewItem);
                    $conlistdiv.replaceWith($newpanel);
                    $('.list-item-new', $newpanel).focus();
                });
            });
        }
    };

    $('#newlistbtn').click(function () {
        $.get('/List/Create/anester', null, function (data) {
            $('#creatediv').html(data).show().dialog({

            });


            $('input[type="submit"]', $('#creatediv')).click(function () {
                var name = $('[name="UserListName"]', $('#creatediv')).val(),
                    desc = $('[name="UserListName"]', $('#creatediv')).val();

                $.post('/List/Create/anester', { UserListName: name, Description: desc }, function (data) {
                    location.reload();
                });
            });
        }).error(function (e) {
            alert(e);
        });
    });

    $('.remove-list-item').click(function () {
        var $this = $(this),
            $a = $this.closest('a');

        $.post('/ListItem/Delete/', { id: $this.attr('data-id') }, function () {
            $a.remove();
        });
    });

    $('.list-item-new').keyup(enterNewItem);
});