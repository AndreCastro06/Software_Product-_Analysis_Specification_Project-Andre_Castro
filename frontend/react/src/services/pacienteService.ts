import { api } from "./api";

export interface PacienteResumo {
  id: number;
  nomeCompleto: string;
}

export async function listarMeusPacientes(): Promise<PacienteResumo[]> {
  const response = await api.get("/pacientes/meus-pacientes");
  return response.data as PacienteResumo[];
}