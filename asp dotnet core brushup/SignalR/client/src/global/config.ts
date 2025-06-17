import axios from "axios";

export const port = 7246;
export const base_url = "https://localhost:7246/";

export const axios_auth = axios.create({
    baseURL: base_url,
    withCredentials : true,
    headers: {
        "Content-Type": "application/json"
    }
});

export const axios_no_auth = axios.create({
    baseURL: base_url,
    withCredentials: false,
    headers:{
        "Content-Type": "application/json",
        Accept: "application/json"
    }
})

// Axios instance for authenticated file uploads
export const axios_auth_form = axios.create({
  baseURL: base_url,
  withCredentials: true, // Ensures cookies are sent with requests
  headers: {
    "Content-Type": "multipart/form-data",
    Accept: "application/json",
  },
});