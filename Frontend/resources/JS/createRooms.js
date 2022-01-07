let buttonBedRoom = document.getElementById("bedRoom").addEventListener("click", () => {
        var room = document.createElement("section");
        card.classList.add("rooms");
        var name = document.createElement("p");
        name.classList.add("BedRoom");
        name.innerText = doc.data(bedRoom).name
        room.appendChild(name)
        room.appendChild(image)
        room.appendChild(edit);
        room.id = doc.id;
        document.getElementById("newroom").appendChild(room);

        window.location.replace("mainPage.html")
  })