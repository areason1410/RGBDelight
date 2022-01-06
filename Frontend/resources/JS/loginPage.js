// document.getElementById('createAccountButton').addEventListener("click", createAccount(){
// })

const emailInput = document.getElementById('email')
const passwordInput = document.getElementById('password')
const passwordConfirmInput = document.getElementById('confirmPassword')

document.getElementsByClassName('button').addEventListener("click", createAccount(), homePage())

function createAccount() {
    fetch("http://localhost:3000/database/addUser", {
        method: "POST",
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
            "email": emailInput.value,
            "password": password.value
        })
    }).then(res => {
        console.log("Request complete! response:", res);
    });
}

function homePage() {
    location.href = "homePage.html"
}
