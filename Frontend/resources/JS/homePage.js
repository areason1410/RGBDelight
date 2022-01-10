



class Light {
  constructor(id) {
    this.id = id;
    this.r = 255;
    this.g = 255;
    this.b = 255;
  }
}

class room {


  constructor(name) {
    this.Lights = [new Light(1), new Light(2), new Light(3)]
    this.name = name;
  }
}

let room1 = new room("Bedroom");
let room2 = new room("Kitchen");
let room3 = new room("Living Room");

let rooms = [room1, room2, room3];
//console.log(room1.Lights);


rooms.forEach(room => {
  const roomCard = document.createElement("div");
  roomCard.classList.add("roomCard");
  roomCard.addEventListener("click", () => {
    window.location.href = "lights.html?roomName=" + room.name;

  })

  const roomName = document.createElement("p");
  roomName.classList.add("roomName")
  roomName.innerText = room.name;
  roomCard.appendChild(roomName);



  document.body.appendChild(roomCard);


});

// var link = window.location.href;
// link = link.replace("http://example.com/PageTwo.html?","");
// document.write("The variable contained this content:" + link + "");



{/* <div class="roomCard">
            <div class="kitchen">
                <div class="imgKit">
                    <img class="imgKit" src="resources/images/restaurant_black_24dp.svg" alt="kitchen">
                </div>
                    <section class="kitLights">
                        <div class="kitLightsComp">
                            <div class="kitchenName">
                            <p class="kitchenName">Kitchen</p>
                        </div>
                            <div class="kitImgLights">
                                <img class="kitLight" src="resources/images/lightbulb_black_24dp.svg" alt="lightbulb">
                                <img class="kitLight" src="resources/images/lightbulb_black_24dp.svg" alt="lightbulb">
                                <img class="kitLight" src="resources/images/lightbulb_black_24dp.svg" alt="lightbulb">
                            </div>  
                            <p class="kitPresetText">Morning Preset</p>
                        </div>
                    </section>
            </div>
        </div>  */}