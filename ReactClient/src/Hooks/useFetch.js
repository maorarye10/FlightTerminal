import { useEffect, useState } from "react";

export const useFetch = (url) => {
  console.log("now in fetch hook...");
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState("");
  const [data, setData] = useState([]);

  useEffect(() => {
    console.log("fetching...");
    fetch(url, {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
      },
    })
      .then((response) => response.json())
      .then((json) => {
        console.log("Got the data!: ", json);
        setData(json);
        console.log("Now setting 'isLoading' to false");
        setIsLoading(false);
      })
      .catch((error) => {
        console.log(error.message);
        setError(error.message);
      });
  }, []);

  return [isLoading, error, data];
};
