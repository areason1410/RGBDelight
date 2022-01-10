
  document.getElementById("createRoom").addEventListener("click", () => {
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