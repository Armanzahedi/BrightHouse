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
function isEmail(email) {
    var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    return regex.test(email);
}
$("#newsLetterForm").submit(function (event) {
    var newsLetterCookie = getCookie("newLetterCookie");
    var overlay = $('<div id="overlay"></div>');
    var emailInput = $("#newsLetterEmail").val();
    if (emailInput == null || emailInput == "") {
        event.preventDefault();
        $("#newsLetterEmailValidation").show();
        $("#newsLetterEmailValidation").text('لطفا ایمیل خود را وارد کنید');
        console.log('لطفا ایمیل خود را وارد کنید');
    } else if (isEmail(emailInput) == false) {
        event.preventDefault();
        $("#newsLetterEmailValidation").show();
        $("#newsLetterEmailValidation").text('ایمیل وارد شده معتبر نیست');
        console.log('ایمیل وارد شده معتبر نیست');
    } else {
        $("#newsLetterEmailValidation").hide();
        if (newsLetterCookie === null || newsLetterCookie === "") {
            setCookie("newLetterCookie", "true")
        }
        $('.popup').hide();
        overlay.appendTo(document.body).remove();
    }
});