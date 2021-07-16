"use strict";

function getCookie(name) {
    const value = `; ${document.cookie}`;
    const parts = value.split(`; ${name}=`);
    if (parts.length === 2) return parts.pop().split(';').shift();
}

function checkIfDarkmodeEnabled() {
    var cookie = getCookie("darkmodeEnaled");
    if (cookie == 'true')
        return true;
    return false;
}

var connection = new signalR.HubConnectionBuilder().withUrl("/chathub").build();

document.getElementById("sendButton").disabled = true;

if (document.location.search === "") {
    document.getElementById("messagesList").scrollIntoView({ block: "end", inline: "nearest" });
}
connection.on("ReceiveMessage", function (user, message, time)
{
    var div = document.createElement("div");
    document.getElementById("messagesList").appendChild(div);
    var darkmode = checkIfDarkmodeEnabled() ? 'dark' : '';
    div.innerHTML = `<div class="row"><div class="alert alert-info col-8 fadein-long ${darkmode}" style="padding: 7px">
<p class="text-secondary" style="font-size: 11px; margin: 0px">${user}</p>
<p class="text-secondary float-end" style="font-size: 10px; margin: 0px">${time.toString()}</p >
${message}
</div></div>`;
    document.getElementById("messagesList").scrollIntoView({ behavior: "smooth", block: "end", inline: "nearest" });

});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;

    connection.invoke("JoinGroup").catch(function (err) {
        return console.error(err.toString());
    })
}).catch(function (err) {
    return console.error(err.toString());
})

document.getElementById("sendButton").addEventListener("click", function (event) {
    sendMessage();
    event.preventDefault();
});

document.addEventListener('keyup', (e) => {
    if (e.code === 'Enter')
        sendMessage();
});


function sendMessage() {
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", message).catch(function (err) {
        return console.error(err.toString());
    });
    var div = document.createElement("div");
    document.getElementById("messagesList").appendChild(div);
    var darkmode = checkIfDarkmodeEnabled() ? 'dark' : '';
    if (document.getElementById("messageInput").value != "") {
        div.innerHTML = `<div class="row"><div class="col-4"></div>
<div class="alert alert-primary fadein-long col-8 ${darkmode}" style="padding: 7px">
<p class="text-secondary" style="font-size: 11px; margin: 0px">You</p>
<p class="text-secondary float-end" style="font-size: 10px; margin: 0px">${new Date().toLocaleString().replace(/(.*)\D\d+/, '$1')}</p >
${message}
</div></div>`;
    }
    document.getElementById("messageInput").value = "";
    document.getElementById("messagesList").scrollIntoView({ behavior: "smooth", block: "end", inline: "nearest" });
}