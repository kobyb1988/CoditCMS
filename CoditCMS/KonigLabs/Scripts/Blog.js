var replies = document.querySelectorAll('a.reply');

function insertAfter(newNode, referenceNode) {
    referenceNode.parentNode.insertBefore(newNode, referenceNode.nextSibling);
}
var validateEmail = function (email) {
    var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(email);
};


function bindCommentForm(form, commentId, postId) {
    //var form = document.querySelector('.comments .contact_form')    
    form.querySelector('.btn').onclick = function (e) {
        e.stopPropagation();
        e.preventDefault();
        var valid = true
        var name_input = form.querySelector('input[name="name"]')
        var name = name_input.value;
        if (!name) {
            name_input.style['border'] = '1px solid red';
            valid = false;
        }
        else {
            name_input.style['border'] = '';
        }
        var email_input = form.querySelector('input[name="email"]');
        var email = email_input.value;
        if (!email || !validateEmail(email)) {
            email_input.style['border'] = '1px solid red';
            valid = false;
        }
        else {
            email_input.style['border'] = '';
        }

        var text_input = form.querySelector('textarea')
        var text = text_input.value;
        if (!text) {
            text_input.style['border'] = '1px solid red';
            valid = false;
        }
        else {
            text_input.style['border'] = '';
        }



        if (!valid) {
            return;
        }

        var xmlhttp = new XMLHttpRequest();
        xmlhttp.onreadystatechange = function () {
            if (xmlhttp.readyState == 4) {
                if (xmlhttp.status == 200) {
                    var d = document.createElement('div')
                    d.innerHTML = 'Спасибо за ваш комментарий!';
                    d.id = 'thanks';
                    form.parentNode.insertBefore(d, form);
                    if (commentId) {
                        form.remove();
                    }
                    if (postId) {
                        name_input.value = '';
                        email_input.value = '';
                        text_input.value = '';
                    }
                    //console.log(form.parentNode, form);
                    setTimeout(function () {
                        var tha = document.querySelector("#thanks");
                        tha.remove();
                    }, 3000)
                }
            }
        }

        xmlhttp.open("POST", "/Comment", true);
        xmlhttp.setRequestHeader('Content-type', 'application/x-www-form-urlencoded');
        xmlhttp.send("name=" + name + "&email=" + email + "&text=" + text + "&commentId=" + commentId + "&postId=" + postId)

    }
}

function setUpReplyForm(link) {
    var forms = document.querySelectorAll('.comments .contact_form')
    for (var i = 0; i < forms.length; i++) {
        forms[i].parentNode.removeChild(forms[i]);
    }

    var block = link.parentNode.parentNode.parentNode;
    var form = document.querySelector('.contact_form')
    var clone = form.cloneNode('deep');
    clone.id = '';
    insertAfter(clone, block);
    bindCommentForm(clone, link.dataset.id, null);
}

for (var i = 0; i < replies.length; i++) {
    var link = replies[i];
    link.onclick = function (e) {
        console.log(e)
        e.stopPropagation();
        e.preventDefault();
        setUpReplyForm(e.target);
    }
}
var re = new RegExp('st\/([0-2]+)')
var match = re.exec(document.location.pathname)
if(match.length == 2){
    bindCommentForm(document.querySelector('form'), null, parseInt(match[1]));
}
