import { HubConnectionBuilder } from "@microsoft/signalr";
import { useEffect, useState } from "react";

export const useSignalR = (url, method) => {
  const [connection, setConnection] = useState(null);
  const [isConnected, setIsConnected] = useState(false);
  const [data, setData] = useState(null);

  useEffect(() => {
    const newConnection = new HubConnectionBuilder()
      .withUrl(url)
      .withAutomaticReconnect()
      .build();
    setConnection(newConnection);
  }, []);

  useEffect(() => {
    if (connection) {
      connection.on(method, (json) => {
        console.log("Recived new data!: ", json);
        setData(json);
      });
      connection
        .start()
        .then((result) => {
          console.log("Connected using SignalR successfuly!");
          setIsConnected(true);
        })
        .catch((e) => console.log("Connection failed: ", e));
    }
  }, [connection]);

  return [isConnected, data];
};
