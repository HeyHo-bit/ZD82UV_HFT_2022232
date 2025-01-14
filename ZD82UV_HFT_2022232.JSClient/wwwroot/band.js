﻿let bands = [];
let stats = [];
let connection = null;
let bandIdToUpdate = -1;

getdata();
setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:4273/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("bandCreated", (user, message) => {
        getdata();
    });

    connection.on("bandDeleted", (user, message) => {
        getdata();
    });

    connection.on("bandUpdated", (user, message) => {
        getdata();
    });

    connection.onclose(async () => {
        await start();
    });

    start();
}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

async function getdata() {
    await fetch('http://localhost:4273/band')
        .then(x => x.json())
        .then(y => {
            bands = y;
            console.log(bands);
            display();
        });
}

function display() {
    document.getElementById('resultarea').innerHTML = "";
    bands.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>"
            + t.bandId + "</td><td>"
            + t.bandName + "</td><td>"
            + `<button type="button" onclick="showupdate(${t.bandId})">Update</button>`
            + `<button type="button" onclick="remove(${t.bandId})">Delete</button>` +
            "</td></tr>";
    });
}


function create() {
    let name = document.getElementById('titleToCreate').value;

    fetch('http://localhost:4273/band/', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(
            {
                bandName: name,
            })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}

function showupdate(id) {
    document.getElementById('titleToUpdate').value = bands.find(t => t['bandId'] == id)['bandName'];
    document.getElementById('updateformdiv').style.display = 'flex';
    bandIdToUpdate = id;
}

function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let name = document.getElementById('titleToUpdate').value;


    fetch('http://localhost:4273/band/', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(
            {
                bandId: bandIdToUpdate,
                bandName: name,
 
            })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}

function remove(id) {
    fetch('http://localhost:4273/band/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}