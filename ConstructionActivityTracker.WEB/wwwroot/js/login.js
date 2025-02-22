$(function () {
    $("#loginForm").on("submit", function (event) {
        event.preventDefault();

        var email = $("#email").val().trim();
        var password = $("#password").val().trim();

        if (email === "" || password === "") {
            alert("Lütfen hem email hem şifre giriniz.");
            return;
        }

        var loginData = {
            Email: email,
            Password: password
        };

        var apiUrl = "https://localhost:44309/api/Auth/Login";

        $.ajax({
            url: apiUrl,
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify(loginData),
            success: function (response) {
                if (response && response.accessToken && response.accessToken.token) {
                    localStorage.setItem("accessToken", response.accessToken.token);
                }
                window.location.href = "/Home/Index";
            },
            error: function (xhr, status, error) {
                console.error("Login failed: ", error);
                alert("Giriş başarısız. Lütfen bilgilerinizi kontrol ediniz.");
            }
        });
    });
});
