const
    io = require("socket.io-client"),
    ioClient = io.connect("http://77.55.212.76:8000");

ioClient.on("seq-num", (msg) => console.info(msg));