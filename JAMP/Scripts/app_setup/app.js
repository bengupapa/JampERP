$(document).ready(function () {
    /**
    * Dynamically resize content are to display size
    */
    $(window).resize(function () {
        if ($(window).width() > 939) {
            $('.content-frame').css('width', '100%').css('width', '-=190px');
        } else {

            $('.content-frame').css('width', '100%');
        }
    });

    /**
    * Close sign out modal
    */
    $('#closeSignout').click(function () {
        $('#aboutModal').foundation('reveal', 'close');
    });

    /**
    * Close application and sign out
    */
    $('#signoutApp').click(function () {
        window.loggingOff = true;
        document.getElementById('logoutForm').submit()
    });
});
