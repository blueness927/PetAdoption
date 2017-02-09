$(function () {
    $("#tabs").tabs();
});



//FAQ
$(function () {
    $(' .answer').hide();
    $(' h4').click(
           function () {
               if ($(this).hasClass('close')) {
                   $(this).next('.answer').fadeOut();
                   $(this).removeClass('close');
               }
               else {
                   $(this).next('.answer').slideDown();
                   $(this).addClass('close');
               }
           }
       );
})