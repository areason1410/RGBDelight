const sqlite3 = require("sqlite3").verbose();

const database = new sqlite3.Database('Database/accounts.sqlite3', (err) => {
    if (err) {
        console.error(err);
    }

    console.log("connected to the database\n");
})

module.exports = {
    initialiseDatabase,
    getUsers,
    addUser
}

async function initialiseDatabase() {
    database.serialize(() => {
        database.run("CREATE TABLE IF NOT EXISTS accounts(username text, password text)", (err) => {
            if (err) {
                console.error(err)
            }
        })
    })
}

async function getUsers(callBack) {
    var accounts = database.get("SELECT * FROM accounts", (error, result) => {
        if (err) {
            return callBack(null)
        }

    })
}

async function addUser(username, password) {
    var result = new Promise((resolve, reject) => {
        database.run("INSERT INTO accounts(username, password) VALUES(?, ?)", [username, password], (err) => {
            if (err) {
                reject(err)
            }
            else {
                resolve("Good")
            }
        })
    })

    var res = await result
    return res
}

function createAccount(username, password) {
    var newUser = document.getElementById(email)
    var newPass = document.getElementById(password)
    var confirm = document.getElementById(confirmPassword)

    if (newPass == confirm) {
        database.run("INSERT INTO accounts(username, password) VALUES(?, ?)", [newUser, newPass], (err) => {
            if (err) {
                reject(err)
            }
            else {
                resolve("Good")
            }
        }
    }
}


function login(username, password) {

}