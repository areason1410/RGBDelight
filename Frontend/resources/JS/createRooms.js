
  const roomNameInput = document.getElementById('createRoomInput')
  document.getElementById("newRoom").addEventListener("click", () => {
      fetch("http://localhost:3000/database/createRoom", {
              method: "POST",
              headers: { 'Content-Type': 'application/json' },
              body: JSON.stringify({
                    "roomname": roomNameInput.value
              })
        }).then(res => {
              
              console.log("Request complete! response:", res);
        });
  })