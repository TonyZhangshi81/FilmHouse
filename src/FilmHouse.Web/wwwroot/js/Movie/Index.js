$(function () {
  $("#link-to-details").smoothScroll();
  $("#link-to-summary").smoothScroll();
  $("#link-to-resources").smoothScroll();
  $("#link-to-comments").smoothScroll();

  $("#btnDisplay").click(function () {
    $("#summaryshort").slideUp(500);
    $("#summarylong").slideDown(500);
  });
  $("#btnHidden").click(function () {
    $("#summaryshort").slideDown(500);
    $("#summarylong").slideUp(500);
  });

});