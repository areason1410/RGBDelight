var sliderR = document.getElementById("Red");
var sliderG = document.getElementById("Green");
var sliderB = document.getElementById("Blue");

var outputR = document.getElementById("outputR");
var outputG = document.getElementById("outputG");
var outputB = document.getElementById("outputB");

var apply = document.getElementById("apply");

outputR.innerHTML = sliderR.value; // Display the default slider value
outputG.innerHTML = sliderG.value; // Display the default slider value
outputB.innerHTML = sliderB.value; // Display the default slider value

// Update the current slider value (each time you drag the slider handle)
sliderR.oninput = function() {
  outputR.innerHTML = this.value;
}

sliderG.oninput = function() {
    outputG.innerHTML = this.value;
}
sliderB.oninput = function() {
    outputB.innerHTML = this.value;
}

apply.addEventListener("click", async () => {
    url = "http://localhost:3000/PortAPI/changeColour" //this will be the pis ip
    data = {
        "red": sliderR.value,
        "green": sliderG.value,
        "blue": sliderB.value
    }
    const response = await fetch(url, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(data)
    });
    //return response.json();
})