
let button4 = document.getElementById("createRoom").addEventListener("click", () => {
     
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
    })

    async function createRoom() {
      var result = new Promise((resolve, reject) => {
          database.run("INSERT INTO rooms(RoomID, RoomName) VALUES(?, ?)", [roomid, roomname], (err) => {
              if (err) {
                  reject(err);
              }
              else {
                  resolve("good");
              }
          })
      })
      var res = await result;
      return res;
  }

  const roomID = document.getElementById(roomID);
  const roomName = document.getElementById(roomName)

  document.getElementById("createRoom").addEventListener("click", () => {
        fetch("http://localhost:3000/database/addRoom", {
              method: "POST",
              headers: { 'Content-Type': 'applictaion/json' },
              body: JSON.stringify({
                    "roomid": roomID.value,
                    "roomname": roomName.value
              })
        }).then(res => {
              console.log("Request complete! response:", res);
        });
  })