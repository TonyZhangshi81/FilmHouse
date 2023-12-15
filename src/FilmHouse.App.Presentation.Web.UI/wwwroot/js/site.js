$(function () {

  $(".tipup").tooltip();

  var button = document.querySelector("#cookieConsent button[data-cookie-string]");

  var $button = $("#cookieConsent button[data-cookie-string]");
  $button.click(function () { dataCookieString(this); });


  //Barba.Pjax.start();
});


function dataCookieString(event) {
  //document.cookie = event.dataset.cookieString;

  var pUrl = "/Home/grantcookie";

  $.ajax({
    type: "GET",
    url: pUrl,
    cache: false,
    dataType: "json",
    data: null,
    async: false,
    success: function (data) {
      console.log("grant consent!");
      $("#cookieConsent").hide();
    }
  });

}