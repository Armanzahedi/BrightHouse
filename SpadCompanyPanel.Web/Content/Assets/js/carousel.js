$('.slider').slick({
	slidesToShow: 1,
	slidesToScroll: 1,
	arrows: false,
	fade: false,
	asNavFor: '.slider-nav-thumbnails',
});

$('.slider-nav-thumbnails').slick({
	slidesToShow: 5,
	slidesToScroll: 1,
	asNavFor: '.slider',
	dots: true,
	focusOnSelect: true
});

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