
const baseUrl = "http://10.1.21.58:3000"
const url = baseUrl + "/database/getRoom";
const applyUrl = baseUrl + "/database/applyLightChange"
const portUrl = baseUrl + "/PortAPI/changeColour"



var urlParams = new URLSearchParams(window.location.search);
console.log(urlParams.get("roomID")); 

fetch(url, {
    method: "POST",
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({
        "roomid": urlParams.get("roomID"),
    })
}).then(d => d.json()).then(res => {
    res.forEach(light => {
        console.log(light)

        const lightInfo = document.createElement("div")
        //lightInfo.classList.add("lightInfo")

        const lightID = document.createElement("p");
        lightID.innerText = "Light ID: " + light.BulbID;
        lightInfo.appendChild(lightID);

        const r = document.createElement("input");
        r.classList.add("RInput")
        r.type = "text";
        r.placeholder = "Red";
        r.value = light.R;
        lightInfo.append(r);

        const g = document.createElement("input");
        g.classList.add("GInput")
        g.type = "text";
        g.placeholder = "Green";
        g.value = light.G
        lightInfo.append(g);

        const b = document.createElement("input");
        b.classList.add("BInput")
        b.type = "text";
        b.placeholder = "Blue";
        b.value = light.B;
        lightInfo.append(b);


        const sliderLabel = document.createElement("label");
        sliderLabel.htmlFor = "slider"
        sliderLabel.innerText = light.Brightness;
        lightInfo.appendChild(sliderLabel);

        const slider = document.createElement("input")
        slider.type = "range"
        slider.min = 0
        slider.max = 100
        slider.value = light.Brightness
        slider.id = "slider";
        slider.addEventListener("input", () => {
            sliderLabel.innerText = slider.value;
        })
        lightInfo.appendChild(slider)


        const button = document.createElement("button")
        button.classList.add("applyButton")
        button.innerText = "Apply"
        button.addEventListener("click", () => {
            fetch(applyUrl, {
                method: "POST",
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({
                    "R": r.value,
                    "G": g.value,
                    "B": b.value,
                    "state": 1,
                    "Brightness": slider.value,
                    "RoomID": light.RoomID,
                    "BulbID": light.BulbID
                })
            })
                .then(d => d.json()).then(res => {
                    console.log(res);
                })
                //uncomment in production
                fetch(portUrl, {
                    method: "POST",
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify({
                          "red": r.value,
                          "green": g.value,
                          "blue": b.value
                        })
                    })
                    .then(d => d.json()).then(res => {
                        console.log("applied on bulb");
                    })
        })
        lightInfo.appendChild(button)

        document.body.appendChild(lightInfo)
    })
})
