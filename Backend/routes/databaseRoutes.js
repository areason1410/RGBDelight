const db = require("../Database");
const express = require("express");
//const { get } = require("./port");
const router = express.Router();

router.get("/getUsers", async (req, res) => {
    db.getUsers(async (data) => {
        res.send(data)
    })
})

router.post("addUser", async (reg, res) => {
    db.addUser(reg.body.username, reg.body.username);
})

router.post("/addUser", async (req, res) => {
    db.addUser(req.body.username, req.body.password)
    res.send("Complete")
})


router.post("/verifyUser", async (req, res) => {
    console.log(req.body);
    db.checkDetails(req.body.username, async (data) => {
        if (data.password == req.body.password) {
            res.send("good Password");
        }
        else {
            res.send("Incorrect Username or Password")
        }
    })
})

router.post("/changePassword", async (req, res) => {
    db.changePassword(req.body.password, req.body.username);
    res.send("changed password")
})

module.exports = router;

