
  document.getElementById("createRoom").addEventListener("click", () => {
      var roomInput = document.createElement("input");
      roomInput.setAttribute('type', 'text')
      
      
      fetch("http://localhost:3000/database/createRoom", {
              method: "POST",
              headers: { 'Content-Type': 'application/json' },
              body: JSON.stringify({
                    "roomid": 10,
                    "roomname": "test"
              })
        }).then(res => {
              
              console.log("Request complete! response:", res);
        });
  })