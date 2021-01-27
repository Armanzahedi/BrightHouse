
$(function(){
    var overlay = $('<div id="overlay"></div>');
    overlay.show();
    overlay.appendTo(document.body);
    $('.popup-onload').show();
    $('.close').click(function(){
        $('.popup-onload').hide();
        overlay.appendTo(document.body).remove();
        return false;
    });




    $('.x').click(function(){
        $('.popup').hide();
        overlay.appendTo(document.body).remove();
        return false;
    });
});