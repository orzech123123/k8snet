const
    io = require("socket.io-client"),
    socket = io.connect("wss://environment-events.katacoda.com/?v=20201231&client=katacoda&socketid=JQBUTpcpGjGlUrJCAEG4");

//ioClient.on("seq-num", (msg) => console.info(msg));

socket.onAny((eventName, ...args) => {
    console.log(eventName);
    console.log(args);
});

console.log('check 1', socket.connected);
socket.on('connect', function() {
  console.log('check 2', socket.connected);
});