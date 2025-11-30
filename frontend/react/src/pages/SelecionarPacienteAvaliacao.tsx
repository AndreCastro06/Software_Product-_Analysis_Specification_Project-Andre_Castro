import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { listarMeusPacientes } from "../services/pacienteService";
import { Button } from "../components/ui/button";

interface PacienteResumo {
  id: number;
  nomeCompleto: string;
}

export default function SelecionarPacienteAvaliacao() {
  const navigate = useNavigate();
  const [pacientes, setPacientes] = useState<PacienteResumo[]>([]);

  useEffect(() => {
    async function load() {
      const lista = await listarMeusPacientes();
      setPacientes(lista);  
    }
    load();
  }, []);

  return (
    <div className="p-6 max-w-xl mx-auto">
      <div className="flex justify-between items-center mb-8">
      <h1 className="text-3xl font-bold mb-6">Selecionar Paciente</h1>
        <Button onClick={() => navigate(-1)}
          className="px-5 py-2 bg-gray-300 text-gray-800 rounded-xl font-medium hover:bg-gray-400 transition">
          ← Voltar
        </Button>
      </div>
      <div className="space-y-3">
        {pacientes.map((p) => (
          <button
            key={p.id}
            onClick={() => navigate(`/antropometrica/${p.id}`)}
            className="
              w-full text-left p-4 bg-orange-200 
              rounded-lg shadow transition-all
              hover:bg-green-300 hover:scale-105
            "
          >
            <b>{p.nomeCompleto}</b>
            <br />
            <span className="text-sm text-gray-700">ID: {p.id}</span>
          </button>
        ))}
      </div>
    </div>
  );
}

//  <div className="p-8 max-w-3xl mx-auto">

//       <div className="flex justify-between items-center mb-8">
//         <h1 className="text-4xl font-bold">Avaliação Antropométrica</h1>

//         <Button
//           onClick={() => navigate(-1)}
//           className="px-5 py-2 bg-gray-300 text-gray-800 rounded-xl font-medium hover:bg-gray-400 transition"
//         >
//           ← Voltar
//         </Button>
//       </div>