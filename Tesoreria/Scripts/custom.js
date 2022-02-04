$(document).ready(function(){

  var offset = 200;
  var duration = 1000;
  $(window).scroll(function () {
      if ($(this).scrollTop() > offset) {
          $('.back-to-top').fadeIn(duration);
      } else {
          $('.back-to-top').fadeOut(duration);
      }
  });
  
  $('.back-to-top').click(function (event) {
      event.preventDefault();
      $('html, body').animate({ scrollTop: 0 }, 600);
      return false;
  })

  $(".submenu > a").click(function(e) {
    e.preventDefault();
    var $li = $(this).parent("li");
    var $ul = $(this).next("ul");

    if($li.hasClass("open")) {
      $ul.slideUp(350);
      $li.removeClass("open");
    } else {
      $(".nav > li > ul").slideUp(350);
      $(".nav > li").removeClass("open");
      $ul.slideDown(350);
      $li.addClass("open");
    }
  });

  $(".dropdown > a").click(function (e) {
    e.preventDefault();
    var $li = $(this).parent("li");
    var $ul = $(this).next("ul");

    if ($li.hasClass("open")) {
        $ul.slideUp(350);
        $li.removeClass("open");
    } else {
        $(".nav > li > ul").slideUp(350);
        $(".nav > li").removeClass("open");
        $ul.slideDown(350);
        $li.addClass("open");
    }
   });
  
});