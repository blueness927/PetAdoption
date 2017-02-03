//滑動離開頂部時就取消at_top的class
$(window).scroll(function(e){
  if ($(window).scrollTop()<=0)
    $(".navbar,.explore").addClass("at_top");
  else
    $(".navbar,.explore").removeClass("at_top");
});

//緩慢滑動
$(document).on('click', 'a', function(event){
    event.preventDefault();
    $('html, body').animate({
        scrollTop: $( $.attr(this, 'href') ).offset().top
    }, 500);
});

//偵測進入貓咪範圍就站起來
function detect_cat(cat_id,x){
  var catplace = $(cat_id).offset().left+$(cat_id).width()/2;
  if (Math.abs(x-catplace)<80)
    $(cat_id).css("bottom","0px");
  else
    $(cat_id).css("bottom","-50px");
}

//滑鼠移動時觸發的事件
$(window).mousemove(function(evt){
  var pagex = evt.pageX;
  var pagey = evt.pageY;
  
 

    
  //站起來的貓咪
  // console.log(x);
  detect_cat("#cat_yellow",pagex);
  detect_cat("#cat_blue",pagex);
  detect_cat("#cat_grey",pagex);
  


   //更新三角形
  $(".tri1").css("transform",
                 "translateX("+x/-5+"px) rotate(-15deg)");
  $(".tri2").css("transform",
                 "translateX("+x/-10+"px) rotate(-15deg)");
  $(".tri3").css("transform",
                 "translateX("+x/-12+"px) rotate(-15deg)");
  $(".tri4").css("transform",
                 "translateX("+x/-14+"px) rotate(-15deg)");
  $(".tri5").css("transform",
                 "translateX("+x/-16+"px) rotate(-15deg)");
});

