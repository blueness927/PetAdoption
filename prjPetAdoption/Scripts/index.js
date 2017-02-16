$(window).scroll(function(evt){
  if ($(window).scrollTop()>0)
    $(".navbar").removeClass("navbar-top");
  else
      $(".navbar").addClass("navbar-top");
});

var s = skrollr.init();


//滑動離開頂部時就取消at_top的class
$(window).scroll(function(e){
  if ($(window).scrollTop()<=0)
    $(".navbar,.explore").addClass("at_top");
  else
    $(".navbar,.explore").removeClass("at_top");
});

//Back-toTop
$(function () {

    $('.back-to-top').each(function () {
  
        var $el = $(scrollableElement('html', 'body'));

        $(this).on('click', function (event) {
            event.preventDefault();
            $el.animate({ scrollTop: 0 }, 500);
        });
    });

    function scrollableElement (elements) {
        var i, len, el, $el, scrollable;
        for (i = 0, len = arguments.length; i < len; i++) {
            el = arguments[i],
            $el = $(el);
            if ($el.scrollTop() > 0) {
                return el;
            } else {
                $el.scrollTop(1);
                scrollable = $el.scrollTop() > 0;
                $el.scrollTop(0);
                if (scrollable) {
                    return el;
                }
            }
        }
        return [];
    }
});

//讓 Bootstrap 輪播的內容占用一樣的高度
$(function() {
    carouselNormalization();
});
function carouselNormalization() {
    var items = $('.new .carousel .carousel-inner .item'), heights = [], tallest, bwidth, height, width;
    if( items.length ) {
        function normalizeHeights() {
            bwidth = $('.carousel').width();
            items.each(function() {
                height = $(this).height();
                width = $(this).width();
                if( width > bwidth ) {
                    height = height * ( bwidth / width );
                }
                heights.push(height);
            });
            tallest = Math.max.apply(null, heights);
            if( tallest > bwidth ) {
                tallest = bwidth;
            }
            items.each(function() {
                $(this).css('height', tallest + 'px');
            });
        };
        normalizeHeights();
        $(window).on('resize', function() {
            bwidth = $('.carousel').width();
            heights = [];
            items.each(function() {
                $(this).css('height', 'auto');
            });
            normalizeHeights();
        });
    }
}

//cat animate
$(window).mousemove(function(evt){  
  var x = evt.pageX; 
  var y = evt.pageY; 
  //console.log( x + "," + y);
  
  $("#cross").css("left",x+"px");
  $("#cross").css("top",y+"px");
  
  var catplace = $("#cat_blue").offset().left+$("#cat_blue").width()/2;
  if (Math.abs(x-catplace)<80){
    $("#cat_blue").css("bottom","0px");
  }else{
    $("#cat_blue").css("bottom","-50px");
  }
  
  var catplace = $("#cat_yellow").offset().left+$("#cat_yellow").width()/2;
  if (Math.abs(x-catplace)<80){
    $("#cat_yellow").css("bottom","0px");
  }else{
    $("#cat_yellow").css("bottom","-50px");
  }
  
  var catplace = $("#cat_grey").offset().left+$("#cat_grey").width()/2;
  if (Math.abs(x-catplace)<80){
    $("#cat_grey").css("bottom","0px");
  }else{
    $("#cat_grey").css("bottom","-50px");
  }
  
  
});

$(document).ready(function(){
  //Check to see if the window is top if not then display button
  $(window).scroll(function(){
    if ($(this).scrollTop() > 100) {
      $('.scrollToTop').fadeIn();
    } else {
      $('.scrollToTop').fadeOut();
    }
  });
 
  //Click event to scroll to top
  $('.scrollToTop').click(function(){
    $('html, body').animate({scrollTop : 0},500);
    return false;
  });
});



