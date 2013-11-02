$(function () {
    var enterNewItem = function (e) {
        if (e.keyCode == 13) {
            var $input = $(this),
                $conlistdiv = $input.closest('.userlist'),
                userlistid = $conlistdiv.attr("data-userlistid"),
                today = new Date();

            $.post('/ListItem/Create', AddAntiForgeryToken({
                UserListId: userlistid,
                ListItemName: $input.val(),
                DateEntered: (today.getMonth() + 1) + "/" + today.getDate() + "/" + today.getFullYear(),
                Description: ''
            }), function () {
                $.get('/List/ListPart/' + userlistid, function (html) {
                    var $newpanel = $(html);
                    applyListEvents($newpanel);
                    $conlistdiv.replaceWith($newpanel);
                    $('.list-item-new', $newpanel).focus();
                });
            });
        }
    },
    checkIfAllItemsChecked = function ($container) {
        var $items = $('.list-item-isdone', $container),
            allChecked = true;
        $items.each(function (i, e) {
            var $e = $(e);

            if (!$e.is(':checked')) {
                allChecked = false;
            }
        });

        return allChecked;
    },
    idDoneClicked = function () {
        var $this = $(this),
            $listgroup = $this.closest('.user-list-group'),
            $userlist = $this.closest('.userlist'),
            value = $this.val(),
            listitemid = $this.closest('a').attr('data-id'),
            ischecked = $this.is(':checked');

        if (ischecked) {
            $.get('/ListItem/CompleteListItem/' + listitemid, function () {
                if (checkIfAllItemsChecked($listgroup)) {
                    $('<div>Do you want to close the list.</div>').dialog({
                        resizable: false,
                        height: 140,
                        modal: true,
                        buttons: {
                            "Close List": function () {
                                $.get('/List/CloseList/' + $userlist.attr('data-userlistid'), function () {
                                    $userlist.remove();
                                });
                                $(this).dialog("close");
                            },
                            Cancel: function () {
                                $(this).dialog("close");
                            }
                        }
                    });
                }
            });
        }
        else {
            $.get('/ListItem/UnCompleteListItem/' + listitemid, function () {

            });
        }
    },
    locklistClicked = function () {
        var $this = $(this),
            $userlist = $this.closest('.userlist'),
            $input = $('input.list-item-new', $userlist),
            ischecked = $this.is(':checked'),
            userlistid = $userlist.attr('data-userlistid');

        if (ischecked) {
            $.get('/List/LockList/' + userlistid, function () {
                $input.hide();
            });
        }
        else {
            $.get('/List/OpenList/' + userlistid, function () {
                $input.show();
            });
        }
    },
    removeListItem = function () {
        var $this = $(this),
            $a = $this.closest('a');

        $.post('/ListItem/Delete/', AddAntiForgeryToken({ id: $this.attr('data-id') }), function () {
            $a.remove();
        });
    },
    applyListEvents = function ($container) {
        $('.remove-list-item', $container).click(removeListItem);
        $('.list-item-isdone', $container).click(idDoneClicked);
        $('.list-item-new', $container).keyup(enterNewItem);
        $('.user-list-lock', $container).click(locklistClicked);
    };

    $('#newlistbtn').click(function () {
        $.get('/List/Create/' + UserId, null, function (data) {
            $('#creatediv').html(data).show().dialog({

            });


            $('input[type="submit"]', $('#creatediv')).click(function () {
                var name = $('[name="UserListName"]', $('#creatediv')).val(),
                    desc = $('[name="UserListName"]', $('#creatediv')).val();

                $.post('/List/Create/anester', AddAntiForgeryToken({ UserListName: name, Description: desc }), function (data) {
                    location.reload();
                });
            });
        }).error(function (e) {
            alert(e);
        });
    });

    $('#filterlistbtn').click(function () {
        var $name = $('#listnameipt'),
            $date = $('#listdateipt'),
            $container = $('.user-list-container');

        $.post('/List/Index', AddAntiForgeryToken({ loginname: UserId, daterange: $date.val(), name: $name.val() }), function (data) {
            $container.html(data);
            applyListEvents($container);
        });
    });

    applyListEvents($('body'));
});