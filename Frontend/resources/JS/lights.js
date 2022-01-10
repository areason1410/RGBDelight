const url = "http://localhost:3000/database/getRoom";


var href = window.location.href;

var dsplt= href.split("?");

var urivars = dsplt[1].split("=");

for(var i = 0; i < urivars.length; i++)
{

    urivars[i] = urivars[i].replace("%20", " ")
}

console.log(urivars.indexOf("roomID")+1);
const index = urivars.indexOf("roomID")+1;

fetch(url, {
    method: "POST",
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({
        "roomid": urivars[index],
    })
}).then(d => d.json()).then(res => {
    res.forEach(light => {
        console.log(light)

    })
})