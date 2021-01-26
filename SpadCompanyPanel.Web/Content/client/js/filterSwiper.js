var widthSize = window.innerWidth;
var width = 3;
if (widthSize < 992){
    width = 2;
    $(".categories span").addClass("btn swiper-btn");
}
if(widthSize < 650){
    width = 1;
    $(".categories span").addClass("btn swiper-btn");

}
var swiper = new Swiper('.swiper-container', {
    pagination: '.swiper-pagination',
    slidesPerView: width,
    slidesPerColumn: 1,
    paginationClickable: true,
    spaceBetween: 30
});
$(".categories span").on("click", function(){
    var filter = $(this).html().toLowerCase();
    var slidesxcol;
    $(".categories span")
    $(".categories span").removeClass("active");
    $(this).addClass("active");

    if(filter=="all"){
        $("[data-filter]").removeClass("non-swiper-slide").addClass("swiper-slide").show();
        if($(".swiper-slide").length > 16)
            slidesxcol = 2;
        else slidesxcol = 1;
        swiper.destroy();
        swiper = new Swiper('.swiper-container', {
            pagination: '.swiper-pagination',
            slidesPerView: width,
            slidesPerColumn: 1,
            paginationClickable: true,
            spaceBetween: 30
        });
    }
    else {
        $(".swiper-slide").not("[data-filter='"+filter+"']").addClass("non-swiper-slide").removeClass("swiper-slide").hide();
        $("[data-filter='"+filter+"']").removeClass("non-swiper-slide").addClass("swiper-slide").attr("style", null).show();
        console.log($(".swiper-slide").length)
        if($(".swiper-slide").length > 6)
            slidesxcol = 2;
        else slidesxcol = 1;
        swiper.destroy();
        swiper = new Swiper('.swiper-container', {
            pagination: '.swiper-pagination',
            slidesPerView: width,
            slidesPerColumn: 1,
            paginationClickable: true,
            spaceBetween: 30
        });
    }
})