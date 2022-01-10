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
    addUser,
    getRoom,
    addBulb,
    getRooms,
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
        database.run("CREATE TABLE IF NOT EXISTS rooms(id text, name text, bulbs text)", (err) => {
            if (err) {
                console.log(err)
            }
        })
        database.run("CREATE TABLE IF NOT EXISTS bulbs(id text, colour text, state text)", (err) => {
            if (err) {
                console.log(err)
            }
        })
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

async function createRoom(roomname) {

    var getID = new Promise((resolve, reject) => {
        let tempID = 0;

        database.all("SELECT RoomID FROM rooms", (error, result) => {
            result.forEach(id => {
                if(tempID <= id.RoomID) tempID = id.RoomID+1;
            })

            resolve(tempID);
        })
    })
    const newID = await getID;

    var result = new Promise((resolve, reject) => {
        database.run("INSERT INTO rooms(RoomID, RoomName) VALUES(?, ?)", [newID, roomname], (err) => {
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

async function addBulb() {
    var result = new Promise((resolve, reject) => {
        database.run("INSERT INTO bulbs(BulbID, colour, state, RoomID) VALUES(?, ?, ?, ?)", [id, colour, state, roomid], (err) => {
            if (err) {
                reject(err);
            }
            else {
                resolve("good")
            }
        })
    })
    var res = await result;
    return res;
}

async function getRooms(callback) {
    var room = database.all("SELECT * FROM rooms", (error, result) => {
        if (error) {
            //return callBack(null)
        }
        return callback(result);
    })
}

async function getRoom(roomid, callback) {

    var room = database.all("SELECT * FROM bulbs WHERE RoomID = ?", roomid, (error, result) => {
        if (error) {
        }
        //console.log(row);
        return callback(result);
        //console.log(results);
    })

    // console.log(results);
}



async function createScene() {

}
