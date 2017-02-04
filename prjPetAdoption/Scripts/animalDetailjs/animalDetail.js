$(document).ready(function() {
  $('.button').click(function(event) {
    $('.text').fadeToggle(1000);
    // fadeIn 預設隱藏的東西給打開
    // fadeOut 預設開啟的東西給關閉
  });
  
  // lightbox 效果
  lightbox.option({
        'resizeDuration': 500,
        'wrapAround': true
      });
  $(window).load(function() {
      $('#slider').nivoSlider(); 
  });
});


$(document).ready(function() {

  $('.condition h3').click(function(event) {

    // 讓點擊到的 h3 亮起來，其他h3移除active樣式
    $(this).toggleClass('active');

    // 讓點擊到的 h3找到父元素 .question ，再找裡面的 table 判斷收闔
    $(this).parent().find('table').slideToggle();

    // 自己以外的 table 隱藏起來
    $(this).parent().siblings().find('table').slideUp();
    // 自己以外的 h3 移除u樣式
    $(this).parent().siblings().find('h3').removeClass('active');

  });
});