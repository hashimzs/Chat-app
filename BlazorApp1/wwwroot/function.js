function getScreenWidth() {
    var screenWidth = window.innerWidth || document.documentElement.clientWidth || document.body.clientWidth;

    return screenWidth;
}


function scrollToBottom(elementId) {
    var element = document.getElementById(elementId);
    element.scrollTop = element.scrollHeight;
}