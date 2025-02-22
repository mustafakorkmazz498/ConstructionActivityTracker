$(function () {
    $("#registerForm").on("submit", function (event) {
        event.preventDefault(); 

        $("#registerError").addClass("d-none").text("");

        var email = $("#email").val().trim();
        var firstName = $("#firstName").val().trim();
        var lastName = $("#lastName").val().trim();
        var password = $("#password").val().trim();

        if (email === "" || firstName === "" || lastName === "" || password === "") {
            $("#registerError").removeClass("d-none").text("All fields are required.");
            return;
        }

        var registerData = {
            Email: email,
            FirstName: firstName,
            LastName: lastName,
            Password: password
        };

        var apiUrl = "https://localhost:44309/api/Auth/Register";

        $.ajax({
            url: apiUrl,
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify(registerData),
            success: function (response) {
                console.log("Registration successful", response);
                if (response && response.accessToken && response.accessToken.token) {
                    localStorage.setItem("accessToken", response.accessToken.token);
                }
                window.location.href = "/Home/Index";
            },
            error: function (xhr, status, error) {
                console.error("Registration failed: ", error);
                var errMsg = "Registration failed. Please try again.";
                if (xhr.responseJSON && xhr.responseJSON.message) {
                    errMsg = xhr.responseJSON.message;
                }
                $("#registerError").removeClass("d-none").text(errMsg);
            }
        });
    });
});
