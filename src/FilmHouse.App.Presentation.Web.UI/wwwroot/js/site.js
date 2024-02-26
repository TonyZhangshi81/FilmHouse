$(function () {

  $(".tipup").tooltip();

  const $button = $("#cookieConsent button[data-cookie-string]");
  $button.on("click", function () { dataCookieString(this); });

  const isShow = $("#hidIsShow").val();
  if (isShow) {
    $('#cookieConsent').show(1000);
    /*
    $('#cookieConsent').animate({
      scrollTop: $(".site-info").offset().top
    }, 2500, "easeOutQuint");
    */
  }

  //Barba.Pjax.start();
});

function dataCookieString(event) {
  //document.cookie = event.dataset.cookieString;

  const pUrl = "/Account/grantcookie";

  $.ajax({
    type: "GET",
    url: pUrl,
    cache: false,
    dataType: "json",
    data: null,
    async: false,
    success: function (data) {
      console.log("grant consent!");

      setTimeout(function () {
        $('#cookieConsent').hide(400);
      }, 200);
    }
  });

}