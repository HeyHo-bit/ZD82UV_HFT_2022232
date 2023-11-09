let songs = [];
let connection = null;
let songIdToUpdate = -1;

getdata();
setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:4273/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("songCreated", (user, message) => {
        getdata();
    });

    connection.on("songDeleted", (user, message) => {
        getdata();
    });

    connection.on("SongUpdated", (user, message) => {
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
    await fetch('http://localhost:4273/Song')
        .then(x => x.json())
        .then(y => {
            songs = y;
            console.log(songs);
            display();
        });
}

function display() {


    document.getElementById('resultarea').innerHTML = "";
    songs.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>"
            + t.songId + "</td><td>"
            + t.songTitle + "</td><td>"
            + t.releaseDate + "</td><td>"
            + t.income/*+ "M"*/ + "</td><td>"
            + t.rating +/*"/5"+*/ "</td><td>"
        + `<button type="button" onclick="showupdate(${t.songId})">Update</button>`
        + `<button type="button" onclick="remove(${t.songId})">Delete</button>` +
            "</td></tr>";
    });
}


function create() {
    let name = document.getElementById('titleToCreate').value;
    let name2 = document.getElementById('releasedateToCreate').value;
    let name3 = document.getElementById('incomeToCreate').value;
    let name4 = document.getElementById('ratingToCreate').value;
    fetch('http://localhost:4273/Song/', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(
            {
                songTitle: name,
                releasedate: name2,
                income: name3,
                rating: name4
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
    document.getElementById('titleToUpdate').value = songs.find(t => t['songId'] == id)['songTitle'];
    //document.getElementById('releasedateToUpdate').value = songs.find(t => t['songId'] == id)['releaseDate'];
    //document.getElementById('incomeToUpdate').value = songs.find(t => t['songId'] == id)['income'];
    //document.getElementById('ratingToUpdate').value = songs.find(t => t['songId'] == id)['rating'];
    document.getElementById('updateformdiv').style.display = 'flex';
    songIdToUpdate = id;
}

function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let name = document.getElementById('titleToUpdate').value;
    //let name2 = document.getElementById('releasedateToUpdate').value;
    //let name3 = document.getElementById('incomeToUpdate').value;
    //let name4 = document.getElementById('ratingToUpdate').value;


    fetch('http://localhost:4273/Song/', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(
            {
                songId: songIdToUpdate,
                songTitle: name,
                //releasedate: name2,
                //income: name3,
                //rating: name4
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
    fetch('http://localhost:4273/Song/' + id, {
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