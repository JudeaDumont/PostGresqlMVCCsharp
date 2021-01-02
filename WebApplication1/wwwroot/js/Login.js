const loginForm = document.getElementById("login-form");
const loginButton = document.getElementById("login-form-submit");
const loginErrorMsg = document.getElementById("login-error-msg");

loginButton.addEventListener("click", (e) => {
    e.preventDefault();
    const username = loginForm.username.value;
    const password = loginForm.password.value;
    var encrypted = CryptoJS.SHA256(password).toString(CryptoJS.enc.Base64);

    $.ajax({
        type: "POST",
        url: "/login",
        data: { username: username, password: encrypted },
        dataType: "json",
        success: function (response) {
            if (response != null) {
                alert("Success");
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