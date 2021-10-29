

const path = require("path");
const express = require("express");
const app = express();
const cors = require("cors")

//routers
//const portRouter = require("./routes/port");

//use
app.use(express.json());
app.use(cors());
app.use(express.static('../public'));
//app.use("/PortAPI", portRouter);


app.listen(3000, async () => {
    console.log("server started on port 3000");
});

