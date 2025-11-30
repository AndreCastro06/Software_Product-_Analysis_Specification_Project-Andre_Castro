import { api } from "./api";

export async function getHistoricoAvaliacoes(id: number) {
  return api.get(`/avaliacao-antropometrica/paciente/${id}`);
}