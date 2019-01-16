"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/reminderHub").build();

connection.on("ReceiveReminder", function(reminder) {
    console.log("Received Reminder: " + reminder);
    var reminder = reminder.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var li = document.createElement("li");
    li.innerHTML = reminder;
    document.getElementById("remindersList").appendChild(li);
});

start();

async function start() {
    try {
        await connection.start();
        console.log('connected');
        connection.invoke("SendReminder", "Connection started!");
    } catch (err) {
        console.log(err);
        connection.invoke("SendReminder", "Connection failed!");
        setTimeout(() => start(), 5000);
    }
};

connection.onclose(async () => {
    connection.invoke("SendReminder", "Connection closed!");
    await start();
});



