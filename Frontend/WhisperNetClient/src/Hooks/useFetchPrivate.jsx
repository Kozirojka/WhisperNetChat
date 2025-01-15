import { useCallback } from "react";
import useAuth from "./useAuth";

const useFetchPrivate = () => {
    const { auth } = useAuth(); 

    const fetchPrivate = useCallback(
      async (url, options = {}) => {
        if (!options.headers) {
          options.headers = {};
        }

        if (!options.headers["Authorization"]) {
          options.headers["Authorization"] = `Bearer ${auth?.token}`;
        }

        if (!options.headers["Content-Type"]) {
          options.headers["Content-Type"] = "application/json";
        }

        const response = await fetch(url, options);
        return response;
      },
      [auth]
    );

    return fetchPrivate;
};

export default useFetchPrivate;
