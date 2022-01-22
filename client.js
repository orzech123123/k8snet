const
    io = require("socket.io-client"),
    ioClient = io.connect("http://77.55.212.76:80");

ioClient.on("seq-num", (msg) => console.info(msg));