
function ResetColor() {
    $("a#home,a#books,a#contact").css("color", '#969547');
}
function SetColor(tagName) {
    $(tagName).css("color", '#fcf88e');
}

$(document).ready(function () {
    var thisLocation = $(location).attr('pathname').slice(1);
    ResetColor();
    switch (thisLocation) {
        case "Books":
            SetColor("a#books");
            break;
        case "Contact":
            SetColor("a#contact");
            break;
        case "":
            SetColor("a#home");
            break;
        case "Books/Search":
            ResetColor();
            break;
        default:
            break;
    }
});

