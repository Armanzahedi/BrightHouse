function setCookie(cname, cvalue) {
    document.cookie = cname + "=" + cvalue + ";path=/";
}
function getCookie(cname) {
    var name = cname + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var ca = decodedCookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}
$(function () {
    var newsLetterCookie = getCookie("newLetterCookie");
    console.log(newsLetterCookie);
    if (newsLetterCookie === null || newsLetterCookie === "") {
        var overlay = $('<div id="overlay"></div>');
        overlay.show();
        overlay.appendTo(document.body);
        $('.popup-onload').show();
    }
    $('.close').click(function () {
        if (newsLetterCookie === null || newsLetterCookie === "") {
            setCookie("newLetterCookie", "true")
        }
        $('.popup-onload').hide();
        overlay.appendTo(document.body).remove();
        return false;
    });
    $('.x').click(function () {
        if (newsLetterCookie === null || newsLetterCookie === "") {
            setCookie("newLetterCookie", "true")
        }
        $('.popup').hide();
        overlay.appendTo(document.body).remove();
        return false;
    });
});
$("#newsLetterForm").submit(function (event) {
    event.preventDefault();
    console.log("triggered");
});