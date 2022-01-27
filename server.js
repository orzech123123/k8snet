import { Server } from "socket.io"

const server = new Server(82);

let
    sequenceNumberByClient = new Map(),
    ipByClient = new Map();

// event fired every time a new client connects:
server.on("connection", (socket) => {
    console.info(`Client connected [id=${socket.id}]`);
    // initialize this client's sequence number
    sequenceNumberByClient.set(socket, 1);
    ipByClient.set(socket, null);

    // when socket disconnects, remove it from the list:
    socket.on("disconnect", () => {
        sequenceNumberByClient.delete(socket);
        console.info(`Client gone [id=${socket.id}]`);
    });
    
    socket.on("receive-ip", (ip) => {
        ipByClient.set(socket, ip);
    });
});

// sends each client its current sequence number
setInterval(() => {
    for (const [client, sequenceNumber] of sequenceNumberByClient.entries()) {
        client.emit("seq-num", sequenceNumber);
        sequenceNumberByClient.set(client, sequenceNumber + 1);
    }

    for (const [client, ip] of ipByClient.entries()) {
        client.emit("request-ip");
        console.log(` ${client} - ip: ${ip}`)
    }
}, 1000);