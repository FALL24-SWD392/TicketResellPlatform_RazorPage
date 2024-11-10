"use strict";

$(async () => {
    const connection = new signalR.HubConnectionBuilder().withUrl("/signalrhub").build();
    await connection.start();

})