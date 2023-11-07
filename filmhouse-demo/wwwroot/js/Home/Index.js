$(function () {
  var url = new URL(window.location.href);
  var params = url.searchParams;
  var offset = params.get('offset');

  if (offset === '0' || offset === null) {
    $(".div-offset-0").show();
    $(".div-offset-1").hide();
    $(".div-offset-2").hide();
  } else if (offset === '1') {
    $(".div-offset-0").hide();
    $(".div-offset-1").show();
    $(".div-offset-2").hide();
  } else if (offset === '2') {
    $(".div-offset-0").hide();
    $(".div-offset-1").hide();
    $(".div-offset-2").show();
  }
});