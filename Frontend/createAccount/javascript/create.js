const emailInput = document.getElementById("emailInput");
const password = document.getElementById("password")

document.getElementById("createAccount").addEventListener("click", () => {
    fetch("http://localhost:3000/database/addUser", {
        method: "POST",
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
            "username": emailInput.value,
            "password": password.value
        })
    }).then(res => {
        console.log("Request complete! response:", res);
    });

})


function loginPage() {
    location.href = "loginPage.html"
}