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
}

async function initialiseDatabase() {
    database.serialize(() => {
        database.run("CREATE TABLE IF NOT EXISTS accounts(username text, password text)", (err) => {
            if (err) {
                console.error(err)
            }
        })
        //add database.run for colour table
        database.run("CREATE TABLE IF NOT EXISTS colour(R text, G text, B text)", (err) => {
            if (err) {
                console.log(err)
            }
        })
    })
}

async function getUsers(callBack) {
    var accounts = db.get("SELECT * FROM accounts", (error, result) => {
        if (error) {
            return callBack(null)
        }
        console.log(accounts)
    })
}

async function addUser(username, password) {
    var result = new Promise((resolve, reject) => {
        db.run("INSERT INTO accounts(username, password) VALUES(?, ?)",[username, password], (err) => {
            if(err) {
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
    var colours = db.get("SELECT * FROM colours", (error, result) => {
        if (error) {
            return callBack(null)
        }
        console.log(colours)
    })
}

async function addColour(R, G, B) {
    var result = new Promise((resolve, reject) => {
        db.run("INSERT INTO colours(R, G, B) VALUES(?, ?, ?"),[R, G, B], (err) => {
            if(err) {
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

