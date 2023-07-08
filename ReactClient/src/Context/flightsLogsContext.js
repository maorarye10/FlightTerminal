import { useFetch } from "Hooks/useFetch";
import { createContext, useEffect, useState } from "react";

export const flightsLogsContext = createContext({});

const Provider = flightsLogsContext.Provider;

export const FlighsLogsProvider = ({ children }) => {
  console.log("In Context Provider...");
  const [logs, setLogs] = useState([]);
  const [isLoading, error, data] = useFetch(
    "https://localhost:7014/api/flights"
  );

  useEffect(() => {
    console.log("data has changed and is now: ", data);
  }, [data]);

  useEffect(() => {
    console.log("In context provider and 'isLoading' has changed: ", isLoading);
    if (!isLoading && data) {
      console.log(
        "'isLoading' is now false and data is not null and is: ",
        data
      );
      setLogs(data);
    }
  }, [isLoading]);

  const handleNewLog = (log) => {
    setLogs([...logs, log]);
  };

  const value = {
    logs,
    handleNewLog,
  };

  return <Provider value={value}>{children}</Provider>;
};
