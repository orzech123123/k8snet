// const
//     io = require("socket.io-client"),
//     { exec } = require("child_process"),
//     ioClient = io.connect("http://77.55.212.76:80");

import { io } from "socket.io-client";
import got from 'got';

const ioClient = io.connect("http://77.55.212.76/:80");

ioClient.on("seq-num", (msg) => console.info(msg));

ioClient.on("request-ip", async () => {
    await got.get('http://api.ipify.org').text().then((ip) => ioClient.emit("receive-ip", ip));
    
    // exec("curl api.ipify.org", (error, stdout, stderr) => {
    //     if (error) {
    //         console.log(`error: ${error.message}`);
    //         return;
    //     }
    //     if (stderr) {
    //         console.log(`stderr: ${stderr}`);
    //         return;
    //     }
        
    //     ioClient.emit("receive-ip", stdout)
    // });
});
