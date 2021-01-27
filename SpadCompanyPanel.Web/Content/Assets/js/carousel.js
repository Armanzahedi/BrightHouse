$('.slider').slick({
	slidesToShow: 1,
	slidesToScroll: 1,
	arrows: true,
	fade: false,
	asNavFor: '.slider-nav-thumbnails',
});

$('.slider-nav-thumbnails').slick({
	slidesToShow: 5,
	slidesToScroll: 5,
	asNavFor: '.slider',
	dots: false,
	focusOnSelect: true
});
$(".slick-next").eq(1).hide();
$(".slick-prev").eq(1).hide();
var heightSize = $(".slider-nav-thumbnails div img[slide=slide_1]")[0].height;
$(".slick-next").css("height" , heightSize);
$(".slick-prev").css("height" , heightSize);

//remove active class from all thumbnail slides
$('.slider-nav-thumbnails .slick-slide').removeClass('slick-active');

//set active class to first thumbnail slides
$('.slider-nav-thumbnails .slick-slide').eq(0).addClass('slick-active');

// On before slide change match active thumbnail to current slide
$('.slider').on('beforeChange', function (event, slick, currentSlide, nextSlide) {
	var mySlideNumber = nextSlide;
	$('.slider-nav-thumbnails .slick-slide').removeClass('slick-active');
	$('.slider-nav-thumbnails .slick-slide').eq(mySlideNumber).addClass('slick-active');
});

//UPDATED 

$('.slider').on('afterChange', function(event, slick, currentSlide){
	$('.content').hide();
	$('.content[data-id=' + (currentSlide + 1) + ']').show();
});