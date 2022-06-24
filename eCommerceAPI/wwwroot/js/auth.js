function redirectToSignup() {
  window.location.href = "signup.html";
}
function signIn() {
  var email = $("#email").val();
  var password = $("#password").val();

  if (email != "" && password != "") {
    let user = JSON.stringify({
      email: email,
      password: password,
    });
    $.ajax({
      type: "post",
      url: "https://localhost:7141/api/Users/login",
      contentType: "application/json",
      data: user,
      success: function (response) {
        localStorage.setItem("user", JSON.stringify(response.data));

        let urlPath = window.location.pathname.replace(
          "auth/login.html",
          "index.html"
        );
        window.location.href = urlPath;
      },
      error: function (error) {
        console.log("====================================");
        console.log(error);
        console.log("====================================");
      },
    });
  }
}

function signUp() {
  var email = $("#email").val();
  var password = $("#password").val();
  var fullname = $("#fullname").val();

  if (email != "" && password != "") {
    let user = JSON.stringify({
      email: email,
      fullname: fullname,
      password: password,
    });
    $.ajax({
      type: "post",
      url: "https://localhost:7141/api/Users/signup",
      contentType: "application/json",
      data: user,
      success: function (response) {
        localStorage.setItem("user", JSON.stringify(response.data));

        let urlPath = window.location.pathname.replace(
          "auth/signup.html",
          "index.html"
        );
        window.location.href = urlPath;
      },
      error: function (error) {
        console.log("====================================");
        console.log(error);
        console.log("====================================");
      },
    });
  }
}

function logout() {
  localStorage.removeItem("user");
  window.location.href = "auth/login.html";
}
function redirectToSignup() {
  window.location.href = "signup.html";
}
function redirectToLogin() {
  window.location.href = "login.html";
}
function redirectToIndex() {
  window.location.href = "index.html";
}
