import axios from "axios";



const BASE_URL = "https://localhost:7155";

export async function login(data: { email: string; password: string }) {
  return axios.post(`${BASE_URL}/api/Auth/login`, data);
}


export function registerNutricionista(dados: { nome: string; email: string; password: string }) {
  return axios.post("https://localhost:7155/api/NutricionistaAuth/register", dados);
}

export function registerPaciente(dados: { nome: string; email: string; password: string }) {
  return axios.post("https://localhost:7155/api/PacienteAuth/register", dados);
}


