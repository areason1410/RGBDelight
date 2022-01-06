const sqlite3 = require("sqlite3").verbose();

const database = new sqlite3.Database('Database/accounts.sqlite3', (err) => {
    if (err) {
        console.error(err);
    }

    console.log("connected to the database\n");
})

module.exports = {
    initialiseDatabase,
    addUser,
    getUsers,
    checkDetails,
    changePassword,
    changeEmail,
    createRoom,
    addUser
}

async function initialiseDatabase() {
    database.serialize(() => {
        database.run("CREATE TABLE IF NOT EXISTS accounts(username text, password text)", (err) => {
            if (err) {
                console.error(err);
            }
        })
        //add database.run for colour table
        database.run("CREATE TABLE IF NOT EXISTS colour(R text, G text, B text)", (err) => {
            if (err) {
                console.log(err)
            }
        })
        database.run("CREATE TABLE IF NOT EXISTS rooms(bulb text, effect text, colour text)")
    })
}

async function getUsers(callBack) {
    var accounts = database.get("SELECT * FROM accounts", (error, result) => {
        if (error) {
            return callBack(null)
        }
        callback(result);
    })
}

async function addUser(username, password) {
    var result = new Promise((resolve, reject) => {
        database.run("INSERT INTO accounts(username, password) VALUES(?, ?)", [username, password], (err) => {
            if (err) {
                reject(err);
            }
            else {
                resolve("good");
            }
        })
    })
    var res = await result;
    return res;
}

async function getColours(callBack) {
    var colours = database.get("SELECT * FROM colours", (error, result) => {
        if (error) {
            return callBack(null)
        }
        callback(result);
    })
}

async function addColour(R, G, B) {
    var result = new Promise((resolve, reject) => {
        database.run("INSERT INTO colours(R, G, B) VALUES(?, ?, ?"), [R, G, B], (err) => {
            if (err) {
                reject(err);
            }
            else {
                resolve("good")
            }
        }
    })
    var res = await result;
    return res;
}

async function checkDetails(username, callback) {
    var account = database.get("SELECT username, password FROM accounts WHERE username == ?", username, (err, result) => {
        if (err) {
            return callback(null);
        }
        else {
            callback(result);
        }

    })
}

async function changePassword(password, username) {
    var result = new Promise((resolve, reject) => {
        database.run("UPDATE accounts SET password = ? WHERE username = ?", [password, username], (err) => {
            if (err) {
                reject(err);
            }
            else {
                resolve("Password Changed")
            }
        })
    })
    var res = await result;
    return res;
}

async function changeEmail(username, newEmail) {
    var result = new Promise((resolve, reject) => {
        database.run("UPDATE accounts SET username = newEmail Where username = ?", (username, newEmail), (err) => {
            if (err) {
                reject(err)
            }
            else {
                resolve("email changed")
            }
        })
    })
    var res = await result
    return res
}

async function createRoom() {
    var result = new Promise((resolve, reject) => {
        database.run("INSERT INTO rooms(bulb, effect, colour) VALUES(?, ?)", [bulb, effect, colour], (err) => {
            if (err) {
                reject(err);
            }
            else {
                resolve("good");
            }
        })
    })
    var res = await result;
    return res;
}



async function createScene() {

}
