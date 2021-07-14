var right = 0
var wrong = 0;

function ShowResult() {

    for (var i = 0; document.getElementById(i) != null; i++){
        var element = document.getElementById(i);

        if (element.checked && element.getAttribute('value') == 'true') {
            right++
        }
        else if (!element.checked && element.getAttribute('value') == 'true') {
            wrong++
        }
    }

    result.textContent = 'right: ' + right + '  wrong: ' + wrong;
    right = 0
    wrong = 0
}