let labels = [];
let stats = [];
let connection = null;
let labelIdToUpdate = -1;

getdata();
setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:4273/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("labelCreated", (user, message) => {
        getdata();
    });

    connection.on("labelDeleted", (user, message) => {
        getdata();
    });

    connection.on("labelUpdated", (user, message) => {
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
    await fetch('http://localhost:4273/label')
        .then(x => x.json())
        .then(y => {
            labels = y;
            console.log(labels);
            display();
        });
}

function display() {
    document.getElementById('resultarea').innerHTML = "";
    labels.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>"
            + t.labelId + "</td><td>"
            + t.labelName + "</td><td>"
            + `<button type="button" onclick="showupdate(${t.labelId})">Update</button>`
            + `<button type="button" onclick="remove(${t.labelId})">Delete</button>` +
            "</td></tr>";
    });
}


function create() {
    let name = document.getElementById('titleToCreate').value;

    fetch('http://localhost:4273/label/', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(
            {
                labelName: name,
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
    document.getElementById('titleToUpdate').value = labels.find(t => t['labelId'] == id)['labelName'];
    document.getElementById('updateformdiv').style.display = 'flex';
    labelIdToUpdate = id;
}

function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let name = document.getElementById('titleToUpdate').value;


    fetch('http://localhost:4273/label/', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(
            {
                labelId: labelIdToUpdate,
                labelName: name,

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
    fetch('http://localhost:4273/label/' + id, {
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