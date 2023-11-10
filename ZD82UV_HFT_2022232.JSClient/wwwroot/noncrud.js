let stats = [];
let stats2 = [];
let stats3 = [];
let stats4 = [];
let yearstat = [];
let connection = null;
let statIdToUpdate = -1;

getdata();
getdataBest()
getdataTopLabel()
getdataMostSong()
getdataYear()
setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:4273/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    //connection.on("statCreated", (user, message) => {
    //    getdata();
    //});

    //connection.on("statDeleted", (user, message) => {
    //    getdata();
    //});

    //connection.on("statUpdated", (user, message) => {
    //    getdata();
    //});

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
    await fetch('http://localhost:4273/Stat/LabelRevenu')
        .then(x => x.json())
        .then(y => {
            stats = y;
            display();
        });
}
async function getdataTopLabel() {
    await fetch('http://localhost:4273/Stat/TopLabel')
        .then(x => x.json())
        .then(y => {
            stats3 = y;
            display();
        });

}
async function getdataBest() {
    await fetch('http://localhost:4273/Stat/BestSong')
        .then(x => x.json())
        .then(y => {
            stats2 = y;
            display();
        });
}
async function getdataMostSong() {
    await fetch('http://localhost:4273/Stat/MostSong')
        .then(x => x.json())
        .then(y => {
            stats4 = y;
            display();
        });
}
async function getdataYear() {
    await fetch('http://localhost:4273/Stat/YearStatistics')
        .then(x => x.json())
        .then(y => {
            yearstat = y;
            display();
        });
}

//function showupdate(id) {
//    document.getElementById('yearStat').value = yearstat.find(t => t['year'] == id)['year'];
//    document.getElementById('yearStat').value = yearstat.find(t => t['year'] == id)['avgRating'];
//    document.getElementById('yearStat').value = yearstat.find(t => t['year'] == id)['songNumber'];
//    document.getElementById('updateformdiv').style.display = 'flex';
//    songIdToUpdate = id;

//    document.getElementById('yearStat').innerHTML = "";
//    stats4.forEach(t => {
//        document.getElementById('yearStat').innerHTML =
//            "<tr><td>"
//            + t.year + "</td><td>"
//            + t.avgRating + "</td><td>"
//            + t.songNumber + "</td><td>"
//        "</td></tr>";
//    });
//}
//function update() {
//    document.getElementById('updateformdiv').style.display = 'none';
//    let name = document.getElementById('titleToUpdate').value;
//    let name2 = document.getElementById('releasedateToUpdate').value;
//    let name3 = document.getElementById('incomeToUpdate').value;
//    let name4 = document.getElementById('ratingToUpdate').value;


//    fetch('http://localhost:4273/Stat/YearStatistics', {
//        method: 'PUT',
//        headers: { 'Content-Type': 'application/json' },
//        body: JSON.stringify(
//            {
//                songId: songIdToUpdate,
//                songTitle: name,
//                releasedate: name2,
//                income: name3,
//                rating: name4
//            })
//    })
//        .then(response => response)
//        .then(data => {
//            console.log('Success:', data);
//            getdata();
//        })
//        .catch((error) => {
//            console.error('Error:', error);
//        });
//}
function displayMostyear() {
    document.getElementById('resultarea').innerHTML = "";
    yearstat.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>"
            + t.year + "</td><td>"
            + t.avgRating +"/5" + "</td><td>"
            + t.songNumber + " song(s)" + "</td><td>"
        "</td></tr>";
    });
}
function myFunction() {
    var input, filter, table, tr, td, i, txtValue;
    input = document.getElementById("myInput");
    filter = input.value.toUpperCase();
    table = document.getElementById("resultarea");
    tr = table.getElementsByTagName("tr");
    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[0];
        if (td) {
            txtValue = td.textContent || td.innerText;
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
}

function displayMostSong() {
    document.getElementById('resultarea').innerHTML = "";
    stats4.forEach(t => {
        document.getElementById('resultarea').innerHTML =
            "<tr><td>"
            + "Most Song:" + "</td><td>"
            + t.songName + "</td><td>"
        + t.songNumber + " song(s)" + "</td><td>";
    });
}

function displayToplabel() {
    document.getElementById('resultarea').innerHTML = "";
    stats3.forEach(t => {
        document.getElementById('resultarea').innerHTML =
            "<tr><td>"
            + "Top Label:" + "</td><td>"
            + t.labelName + "</td><td>"
        + t.songCount + " song(s)" + "</td><td>"
            + t.revenu + "M" + "</td><td>";
    });
}

function displayBest() {
    document.getElementById('resultarea').innerHTML = "";
    stats2.forEach(t => {
        document.getElementById('resultarea').innerHTML =
            "<tr><td>"
            + "Best Song:" + "</td><td>"
            + t.songName + "</td><td>"
        + t.songCount + " song(s)" + "</td><td>"
            + t.rate + "/5" + "</td><td>";
    });
}

function displayLabelRevenu() {
    document.getElementById('resultarea').innerHTML = "";
    stats.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>"
            + "Labels:" + "</td><td>"
            + t.labelName + "</td><td>"
            + t.songCount + " song(s)" + "</td><td>"
            + t.revenu + "M" + "</td><td>";
    });
}



