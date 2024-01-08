
$(function () {

  delayURL();

});

function delayURL() {
  var delay = document.getElementById("countDown").innerHTML;
  var t = setTimeout(delayURL, 1000);
  if (delay > 0) {
    delay--;
    document.getElementById("countDown").innerHTML = delay;
  } else {
    clearTimeout(t);
    window.location.href = "/";
  }
};
