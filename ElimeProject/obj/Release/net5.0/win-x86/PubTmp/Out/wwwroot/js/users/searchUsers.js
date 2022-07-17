$(document).ready(function (e) {
    $("#search").keyup(function () {
        var data = { searchString: $(this).val() };

        elm = $(this);
        time = (new Date()).getTime();
        delay = 3000;      
        elm.attr({ 'keyup': time });
        elm.off('keydown');
        elm.off('keypress');
        elm.on('keydown', function (e) { $(this).attr({ 'keyup': time }); });
        elm.on('keypress', function (e) { $(this).attr({ 'keyup': time }); });

        setTimeout(function () {
            oldtime = parseFloat(elm.attr('keyup'));
            if (oldtime <= (new Date()).getTime() - delay & oldtime > 0 & elm.attr('keyup') != '' & typeof elm.attr('keyup') !== 'undefined') {
                $("#userList").load("/Account/UserProfiles #userList", data);
                elm.removeAttr('keyup');
            }
        }, delay);
    });
});

$('body').on('click', '.linkUserProfile .hrefUserProfile', function (e) {
    e.prevenDefault;
    document.location.href = $(this).data('href');
})

