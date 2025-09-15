import axios from "axios";



const BASE_URL = "http://localhost:5238";

export async function login(data: { email: string; password: string }) {
  return axios.post(`${BASE_URL}/api/Auth/login`, data);
}


export function registerNutricionista(dados: { nomecompleto: string; email: string; password: string; }) {
  return axios.post("http://localhost:5238/api/NutricionistaAuth/register", dados);
}

export function registerPaciente(dados: { nomecompleto: string; email: string; password: string }) {
  return axios.post("http://localhost:5238/api/PacienteAuth/register", dados);
}



