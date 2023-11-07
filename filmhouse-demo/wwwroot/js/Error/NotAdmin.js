
$(function () {

  delayURL();

});

function delayURL() {
  var delay = document.getElementById("count_down").innerHTML;
  var t = setTimeout("delayURL()", 1000);
  if (delay > 0) {
    delay--;
    document.getElementById("count_down").innerHTML = delay;
  } else {
    clearTimeout(t);
    window.location.href = "/Home";
  }
};
