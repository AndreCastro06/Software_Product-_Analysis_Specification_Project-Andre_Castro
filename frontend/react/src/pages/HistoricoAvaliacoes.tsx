import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { api } from "../services/api";
import type { AvaliacaoHistoricoDTO } from "../types/AvaliacaoHistoricoDTO";

export default function HistoricoAvaliacoes() {
  const { pacienteId } = useParams();
  const [avaliacoes, setAvaliacoes] = useState<AvaliacaoHistoricoDTO[]>([]);

  const nomeMetodo: Record<number, string> = {
    1: "Jackson & Pollock – 3 dobras",
    2: "Jackson & Pollock – 7 dobras",
    3: "Durnin & Womersley",
    4: "Marinho – Circunferências",
    5: "Marinho – 4 dobras",
  };

  function metodoTexto(m: number) {
    return nomeMetodo[m] ?? `Método ${m}`;
  }

  function formatarData(iso: string) {
    return new Date(iso).toLocaleDateString("pt-BR");
  }

  useEffect(() => {
    async function load() {
      if (!pacienteId) return;

      const resp = await api.get<AvaliacaoHistoricoDTO[]>(
        `/api/AvaliacaoAntropometrica/paciente/${pacienteId}`
      );

      setAvaliacoes(resp.data);
    }

    load();
  }, [pacienteId]);

  return (
    <div className="p-6 max-w-3xl mx-auto">
      <h1 className="text-2xl font-bold mb-6">Histórico de Avaliações</h1>

      <ul className="space-y-3">
        {avaliacoes.map((a) => (
          <li 
            key={a.id}
            className="p-4 border rounded-lg bg-white shadow-sm hover:shadow-md transition"
          >
            <div className="font-semibold text-lg">
              {formatarData(a.dataAvaliacao)}
            </div>
            <div className="text-gray-700">
              {a.percentualGordura}% de gordura
            </div>
            <div className="text-gray-500">
              {metodoTexto(a.metodo)}
            </div>
          </li>
        ))}
      </ul>
    </div>
  );
}
