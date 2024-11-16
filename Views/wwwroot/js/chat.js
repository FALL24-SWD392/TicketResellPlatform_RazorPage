﻿"use strict"

const { signalR } = require("../lib/microsoft/signalr/dist/browser/signalr")

var connection = new signalR.HubConnectionBuilder().withUrl("/signalrhub").build();

document.getElementById("btnSendMessage").disable = true;

connection.on("ReceiveMessage", function (message) {
    
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    li.textContent = `${ message.Message }`;
});

connection.start().then(function () {
    document.getElementById("btnSendMessage").disable = false;
}).catch(function (err) {
    return console.error(err.toString());
})