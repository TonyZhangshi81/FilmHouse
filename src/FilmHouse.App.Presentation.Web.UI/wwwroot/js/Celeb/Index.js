$(document).ready(function () {
  $("#link-to-details").smoothScroll();
  $("#link-to-summary").smoothScroll();
  $("#link-to-works").smoothScroll();
});

$(document).ready(function () {
  $("#btnDisplay").click(function () {
    $("#summaryshort").slideUp(500);
    $("#summarylong").slideDown(500);
  });
  $("#btnHidden").click(function () {
    $("#summaryshort").slideDown(500);
    $("#summarylong").slideUp(500);
  });
});

$(function () {
  $("[rel=drevil]").popover({
    trigger: 'manual',
    //placement: 'bottom',
    //title: '<div style="text-align:center; color:red; text-decoration:underline; font-size:14px;"> Muah ha ha</div>',
    html: 'true',
    //content: '<div id="popOverBox"><img src="http://www.hd-report.com/wp-content/uploads/2008/08/mr-evil.jpg" width="251" height="201" /></div>',
    //animation: false
  }).on("mouseenter", function () {
    var _this = this;
    $(this).popover("show");
    $(this).siblings(".popover").on("mouseleave", function () {
      $(_this).popover('hide');
    });
  }).on("mouseleave", function () {
    var _this = this;
    setTimeout(function () {
      if (!$(".popover:hover").length) {
        $(_this).popover("hide")
      }
    }, 100);
  });
});

$(document).ready(function () {
  $.backstretch("../../Content/Account/CelebBack.jpg");
});
