import { Server } from "socket.io"
import got from 'got';
import mongoose from 'mongoose';

const server = new Server(80);

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
setInterval(async () => {
    for (const [client, sequenceNumber] of sequenceNumberByClient.entries()) {
        client.emit("seq-num", sequenceNumber);
        sequenceNumberByClient.set(client, sequenceNumber + 1);
    }

    for (const [client, ip] of ipByClient.entries()) {
        client.emit("request-ip");
        console.log(` ${client} - ip: ${ip}`)
    }

    
    await got.get('http://api/test/test').json().then((test) => console.log(test));  
}, 1000);

var Person = null;
setInterval(async () => {
    if(!Person)
    {
        mongoose.connect('mongodb://root:lunsztra@mongo-db:27017/admin');
        var db = mongoose.connection;
        db.on('error', console.error.bind(console, 'connection error:'));
        db.once('open', async () => {
            const personSchema = new mongoose.Schema({
                name: String
            });
            Person = mongoose.model('Person', personSchema);
            console.log("---CONNECTED---")
        });
    }

    if(!!Person)
    {
        await Person.create({ name: 'Axl Rose' });
        console.log("---ADDED---")
    }
}, 3000);