const db = require("../database");
const express = require("express");
//const { get } = require("./port");
const router = express.Router();

router.get("/getUsers", async (req, res) => {
    db.getUsers(async (data) => {
        res.send(data)
    })
})

<<<<<<< HEAD:Backend/routes/databaseRoutes.js
router.post("addUser", async (reg, res) => {
    db.addUser(reg.body.username, reg.body.username);
})
=======
router.post("/addUser", async (req, res) => {
    db.addUser(req.body.username, req.body.password)
    res.send("Complete")
})

module.exports = router;
>>>>>>> ebfed2862385cb277f32bf413d700f4a2aa767c0:Backend/routes/database.js

