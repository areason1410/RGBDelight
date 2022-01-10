const serialPort = require("serialport");
const port = new serialPort("/dev/ttyACM0",{
    baudRate: 9600
});
const express = require("express");
const router = express.Router();


router.post("/changeColour", async(req, res) => {
    port.write(`${req.body.red}, ${req.body.green}, ${req.body.blue}`);
    console.log(`${req.body.red}, ${req.body.green}, ${req.body.blue}`);
})

module.exports = router;