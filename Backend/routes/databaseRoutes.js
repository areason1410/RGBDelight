const db = require("../database");
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

router.post("/changeEmail", async (req, res) => {
    db.changeEmail(req.body.username, req.body.newEmail)
    res.send("changed Email")
})

router.post("/createRoom", async (req, res) => {
    db.createRoom(req.body.id, req.body.name, req.body.bulbs)
    res.send("Complete")
})

router.post("/addbulb", async (req, res) => {
    db.addBulb(req.body.id, req.body.colour, req.body.state)
    res.send("complete")
})


module.exports = router;

