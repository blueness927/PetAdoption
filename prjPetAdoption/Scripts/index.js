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



