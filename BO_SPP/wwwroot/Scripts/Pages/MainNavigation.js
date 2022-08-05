$(document).ready(function () {
    var url = window.location.pathname;
    var myPageName = url.substring(url.lastIndexOf('/') + 1).replace('.aspx', '').replace('_Form', '').replace('_Preview', '');
    var el = document.getElementById(myPageName);
    if (el != null)
        el.className += " active";   
});