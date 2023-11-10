let genres = [];
let stats = [];
let connection = null;
let genreIdToUpdate = -1;

getdata();
setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:4273/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("genreCreated", (user, message) => {
        getdata();
    });

    connection.on("genreDeleted", (user, message) => {
        getdata();
    });

    connection.on("genreUpdated", (user, message) => {
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
    await fetch('http://localhost:4273/genre')
        .then(x => x.json())
        .then(y => {
            genres = y;
            console.log(genres);
            display();
        });
}

function display() {
    document.getElementById('resultarea').innerHTML = "";
    genres.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>"
            + t.genreId + "</td><td>"
            + t.genreKind + "</td><td>"
            + `<button type="button" onclick="showupdate(${t.genreId})">Update</button>`
            + `<button type="button" onclick="remove(${t.genreId})">Delete</button>` +
            "</td></tr>";
    });
}


function create() {
    let name = document.getElementById('titleToCreate').value;

    fetch('http://localhost:4273/genre/', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(
            {
                genreKind: name,

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
    document.getElementById('titleToUpdate').value = genres.find(t => t['genreId'] == id)['genreKind'];
    document.getElementById('updateformdiv').style.display = 'flex';
    genreIdToUpdate = id;
}

function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let name = document.getElementById('titleToUpdate').value;


    fetch('http://localhost:4273/genre/', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(
            {
                genreId: genreIdToUpdate,
                genreKind: name,
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
    fetch('http://localhost:4273/genre/' + id, {
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