// src/services/avaliacaoService.ts

import { api } from "./api";
import type { AvaliacaoRequest, AvaliacaoResponse } from "../types/avaliacao";

export async function salvarAvaliacao(dados: AvaliacaoRequest) {
  return api.post<AvaliacaoResponse>("/api/AvaliacaoAntropometrica", dados);
}

export async function listarAvaliacoesDoPaciente(pacienteId: number) {
  const response = await api.get<AvaliacaoResponse[]>(
    `/api/AvaliacaoAntropometrica/paciente/${pacienteId}`
  );

  return response.data;
}

export async function simularAvaliacao(dados: AvaliacaoRequest) {
  const response = await api.post("/AvaliacaoAntropometrica/simular", dados);
  return response.data;
}
