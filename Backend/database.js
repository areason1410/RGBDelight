const sqlite3 = require("sqlite3").verbose();

const database = new sqlite3.Database('Database/accounts.sqlite3', (err) => {
    if (err) {
        console.error(err);
    }

    console.log("connected to the database\n");
})

module.exports = {
    initialiseDatabase,
    getUsers
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
    db.get("SELECT * FROM accounts", (error, result) => {
        if (error) {
            return callBack(null)
        }

    })
}