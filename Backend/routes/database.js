const database = require("../database");
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

module.exports = router;