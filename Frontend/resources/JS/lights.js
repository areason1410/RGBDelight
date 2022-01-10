const url = "http://localhost:3000/database/getRoom";


var href = window.location.href;

var dsplt = href.split("?");

var urivars = dsplt[1].split("=");

for (var i = 0; i < urivars.length; i++) {

    urivars[i] = urivars[i].replace("%20", " ")
}

console.log(urivars.indexOf("roomID") + 1);
const index = urivars.indexOf("roomID") + 1;

fetch(url, {
    method: "POST",
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({
        "roomid": urivars[index],
    })
}).then(d => d.json()).then(res => {
    res.forEach(light => {
        console.log(light)

        const lightInfo = document.createElement("div")
        lightInfo.classList.add("lightInfo")

        const lightColourText = document.createElement("p")
        lightColourText.classList.add("lightColourText")
        lightColourText.innerText = ("Colour: ")
        lightInfo.appendChild(lightColourText)

        const lightColour = document.createElement("p")
        lightColour.classList.add("lightColour")
        lightColour.innerText = light.R, light.G, light.B
        lightInfo.appendChild(lightColour)

        const lightStateText = document.createElement("p")
        lightStateText.classList.add("lightStateText")
        lightStateText.innerText = ("State: ")
        lightInfo.appendChild(lightStateText)

        const lightState = document.createElement("p")
        lightState.classList.add("lightState")
        lightState.innerText = light.state
        lightInfo.appendChild(lightState)

        const slider = document.createElement("input")
        slider.type = "range"
        slider.min = 0
        slider.max = 100
        slider.value = light.Brightness
        lightInfo.appendChild(slider)


        const button = document.createElement("button")
        button.classList.add("applyButton")
        button.innerText = "Apply"
        button.addEventListener("click", () => {
            console.log(light.RoomID)
        })
        lightInfo.appendChild(button)

        document.body.appendChild(lightInfo)
    })
})
