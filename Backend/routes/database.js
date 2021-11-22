const db = require("../Database");
const express = require("express");
//const { get } = require("./port");
const router = express.Router();

router.get("/getUsers", async (req, res) => {
    db.getUsers(async (data) => {
        res.send(data)
    })
})

router.post("/addUser", async (req, res) => {
    db.addUser(req.body.username, req.body.password)
    res.send("Complete")
})

module.exports = router;

