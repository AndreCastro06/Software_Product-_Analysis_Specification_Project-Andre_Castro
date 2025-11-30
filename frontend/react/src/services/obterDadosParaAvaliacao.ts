import { api } from "./api";
import type { DadosAvaliacao } from "../types/dadosAvaliacao";

export async function obterDadosParaAvaliacao(id: number): Promise<DadosAvaliacao> {
  const response = await api.get<DadosAvaliacao>(`/pacientes/${id}/dados-avaliacao`);
  return response.data;
}