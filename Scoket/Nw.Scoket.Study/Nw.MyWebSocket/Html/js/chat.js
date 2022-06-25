const loginToken =
  "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNyc2Etc2hhMjU2IiwidHlwIjoiSldUIn0.eyJJZCI6IjEwIiwiTmFtZSI6IuW8oOS4iSIsIkFnZSI6IjI2IiwiZXhwIjoxNjUyNTI4Njc0LCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo1MDAxIiwiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NTAwMSJ9.J9dpwQ-IzoqGiqcnmGpjGmLe-r-JniRAAoaq7ww3uF8ztyIMzvtRkTBLY-A6KGbW2i4aybY1zljpzWDxPCDjCPBemDFA0U2RJo1a85bfo3dsISRrt9mlJOarTdkHVzHoUAxTLOS-oIyLB6L0OBlghb_xUlxrdjbXksde-Aw1Gzo";
const connection = new signalR.HubConnectionBuilder()
  .withUrl("https://localhost:5001/NewChantHub", {
    headers: { Authorization: loginToken },
    accessTokenFactory: () => {
      return loginToken;
    },
  })
  .withAutomaticReconnect([1000, 2000, 5000]) //指定自动重试配置第一次1秒，失败后，2秒，在失败5秒，在就不试了
  .build();

document.getElementById("sendButton").disabled = true;

//尝试重连前，会触发这个回调
connection.onreconnecting((error) => {
  console.assert(connection.state === signalR.HubConnectionState.Reconnecting);
  document.getElementById("sendButton").disabled = true;
  const li = document.createElement("li");
  li.textContent = `由于错误“${error}”导致连接丢失。 重新连接。`;
  document.getElementById("messagesList").appendChild(li);
});

//在自动重试期间，连上了，就触发这个回调
connection.onreconnected((connectionId) => {
  console.assert(connection.state === signalR.HubConnectionState.Connected);

  document.getElementById("sendButton").disabled = false;

  const li = document.createElement("li");
  li.textContent = `重新建立连接。 与 connectionId 连接 "${connectionId}".`;
  document.getElementById("messagesList").appendChild(li);
});

//配置的自动重试都无法连上，触发这个回调
connection.onclose((error) => {
  console.assert(connection.state === signalR.HubConnectionState.Disconnected);

  document.getElementById("sendButton").disabled = true;

  const li = document.createElement("li");
  li.textContent = `连接因错误“${error}”而关闭。 尝试刷新此页面以重新启动连接。`;
  document.getElementById("messagesList").appendChild(li);
});

connection.on("ReceiveMessage", function (response) {
  var li = document.createElement("li");
  document.getElementById("messagesList").appendChild(li);
  // We can assign user-supplied strings to an element's textContent because it
  // is not interpreted as markup. If you're assigning in any other way, you
  // should be aware of possible script injection concerns.

  li.textContent = `${response.body.user} says ${response.body.message}`;
});

connection
  .start()
  .then(function () {
    document.getElementById("sendButton").disabled = false;
  })
  .catch(function (err) {
    return console.error(err.toString());
  });

document
  .getElementById("sendButton")
  .addEventListener("click", function (event) {
    let request = {
      User: document.getElementById("userInput").value,
      Message: document.getElementById("messageInput").value,
    };
    connection.invoke("SendMessage", request).catch(function (err) {
      return console.error(err.toString());
    });
    event.preventDefault();
  });
