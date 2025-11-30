import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { api } from "../services/api";

interface PacienteResumo {
  id: number;
  nomeCompleto: string;
}

export default function HistoricoPacientes() {
  const navigate = useNavigate();

  const [pacientes, setPacientes] = useState<PacienteResumo[]>([]);

  useEffect(() => {
    async function carregar() {
      const resp = await api.get<PacienteResumo[]>("/pacientes/meus-pacientes");
      setPacientes(resp.data);
    }
    carregar();
  }, []);

  return (
    <div className="p-8 max-w-4xl mx-auto">
      <h1 className="text-3xl font-bold mb-8">Pacientes – Histórico</h1>

      <div className="space-y-4">
        {pacientes.map((p) => (
          <div
            key={p.id}
            onClick={() => navigate(`/historico/pacientes/${p.id}`)}
            className="
              p-4 rounded-xl bg-white border shadow-sm cursor-pointer
              hover:bg-blue-100 hover:scale-[1.01] transition
            "
          >
            <p className="text-lg font-semibold">{p.nomeCompleto}</p>
            <p className="text-gray-600">ID: {p.id}</p>
          </div>
        ))}
      </div>
    </div>
  );
}
