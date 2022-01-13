

const path = require("path");
const express = require("express");
const app = express();
const cors = require("cors")
const database = require("./database")
//routers
const portRouter = require("./routes/port");
const accountRouter = require("./routes/databaseRoutes");

//use
app.use(express.json());
app.use(cors());
app.use(express.static("../Frontend"));
app.use("/PortAPI", portRouter);
app.use("/database", accountRouter)


app.listen(3000, async () => {
    console.log("server started on port 3000");
    await database.initialiseDatabase()
});

