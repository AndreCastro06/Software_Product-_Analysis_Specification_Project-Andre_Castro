// src/services/authService.ts
import { api } from "./api";

export interface LoginRequest {
  email: string;
  password: string;
}

export interface LoginResponse {
  token: string;
  role: string;
}

export interface RegisterNutricionistaRequest {
  nome: string;
  email: string;
  password: string;
}

export interface RegisterPacienteRequest {
  nome: string;
  email: string;
  password: string;
}

// 🔹 Login (usado em LoginPage)
export async function login(data: LoginRequest) {
  const response = await api.post<LoginResponse>("/api/Auth/login", data);
  return response;
}

// 🔹 Cadastro de Nutricionista (usado em RegisterNutricionista.tsx)
export async function registerNutricionista(data: {
  nomeCompleto: string;
  email: string;
  password: string;
  crn: string;
}) {
  return api.post("/api/NutricionistaAuth/register", data);
}

// 🔹 Cadastro de Paciente (se/ quando você usar)
export async function registerPaciente(data: RegisterPacienteRequest) {
  const response = await api.post("/api/PacienteAuth/register", data);
  return response;
}
