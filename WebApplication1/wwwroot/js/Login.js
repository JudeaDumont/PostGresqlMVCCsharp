const loginForm = document.getElementById("login-form");
const loginButton = document.getElementById("login-form-submit");
const loginErrorMsg = document.getElementById("login-error-msg");

loginButton.addEventListener("click", (e) => {
    e.preventDefault();
    const username = loginForm.username.value;
    const password = loginForm.password.value;
    var encrypted = CryptoJS.SHA256(password).toString(CryptoJS.enc.Base64);
    console.log(encrypted); //todo: remove this (debug)

    $.ajax({
        type: "POST",
        url: "/login",
        data: { username: username, password: encrypted },
        dataType: "json",
        success: function (response) { //todo: name a function that should move client to new page etc.
            if (response != null) {
                alert(JSON.stringify(response));

                //todo: logout functionality needs to delete the cookies
                //todo: this should just set a user object in cookie

                Cookies.set('name', response.u.userColumn, { expires: 1 });
                Cookies.set('id', response.u.id, { expires: 1 });
                Cookies.set('password', response.u.password, { expires: 1 });
                Cookies.set('role', response.u.role, { expires: 1 });

                console.log(Cookies.get('name'));
                console.log(Cookies.get('id'));
                console.log(Cookies.get('password'));
                console.log(Cookies.get('role'));

            } else {
                alert("Something went wrong");
            }
        },
        failure: function (response) {
            alert("failure" + response.responseText);
        },
        error: function (response) {
            alert("error" + response.responseText);
        }
    });

    //if (username === "user" && encrypted === CryptoJS.SHA256("web_dev").toString(CryptoJS.enc.Base64)) {
    //    alert("You have successfully logged in.");
    //    location.reload();
    //} else {

    //    loginErrorMsg.style.opacity = 1;
    //}
})