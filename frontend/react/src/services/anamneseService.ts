// src/services/anamneseService.ts
import {api} from "./api";
import type {
  AnamneseRequest,
  AnamneseResponse,
} from "../types/anamnese";

export async function criarAnamnese(
  payload: AnamneseRequest
): Promise<AnamneseResponse> {
  const { data } = await api.post<AnamneseResponse>(
    "/api/Anamnese",
    payload
  );
  return data;
}

export async function obterAnamnesePorId(
  id: number
): Promise<AnamneseResponse> {
  const { data } = await api.get<AnamneseResponse>(`/Anamnese/${id}`);
  return data;
}

export async function obterAnamnesesPorPaciente(
  pacienteId: number
): Promise<AnamneseResponse[]> {
  const { data } = await api.get<AnamneseResponse[]>(
    `/Anamnese/paciente/${pacienteId}`
  );
  return data;
}