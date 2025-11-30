// src/services/api.ts
import axios from "axios";

export const api = axios.create({
  baseURL: "https://localhost:7155",
  headers: {
    "Content-Type": "application/json",
  },
});

// Interceptor para enviar token automaticamente
api.interceptors.request.use((config) => {
  const token = localStorage.getItem("token");

  // Garante que headers sempre exista
  config.headers = config.headers ?? {};

  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }

  return config;
});
