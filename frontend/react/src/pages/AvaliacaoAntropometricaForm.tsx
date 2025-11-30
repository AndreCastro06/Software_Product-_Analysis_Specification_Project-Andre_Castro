import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";

import { obterDadosParaAvaliacao } from "../services/obterDadosParaAvaliacao";
import { salvarAvaliacao } from "../services/avaliacaoService";

import type { AvaliacaoRequest } from "../types/avaliacao";
import type { DadosAvaliacao } from "../types/dadosAvaliacao";
import type { AvaliacaoResultadoDTO } from "../types/avaliacaoResultado";

import { Input } from "../components/ui/input";
import { Button } from "../components/ui/button";


// ======================================================
//  MODAL SIMPLES PARA O RESULTADO
// ======================================================
function ModalResultado({
  aberto,
  onClose,
  resultado,
}: {
  aberto: boolean;
  onClose: () => void;
  resultado: AvaliacaoResultadoDTO | null;
}) {
  if (!aberto || !resultado) return null;

  return (
    <div className="fixed inset-0 bg-black/50 flex items-center justify-center z-50">
      <div className="bg-white p-8 rounded-2xl shadow-xl w-full max-w-md text-center">

        <h2 className="text-2xl font-bold mb-4">Resultado da Avaliação</h2>

        <p className="text-lg mb-2">
          <b>Percentual de Gordura:</b> {resultado.percentualGordura}%
        </p>

        <p className="text-lg mb-2">
          <b>Massa Gorda:</b> {resultado.massaGorda} kg
        </p>

        <p className="text-lg mb-2">
          <b>Massa Magra:</b> {resultado.massaMagra} kg
        </p>

        <p className="text-lg mb-2">
          <b>Peso usado:</b> {resultado.peso} kg
        </p>

        <p className="text-lg mb-6">
          <b>Idade:</b> {resultado.idade} anos
        </p>

        <button
          onClick={onClose}
          className="w-full bg-blue-600 text-white py-3 rounded-xl hover:bg-blue-700 transition"
        >
          OK
        </button>
      </div>
    </div>
  );
}



// ======================================================
//                 COMPONENTE PRINCIPAL
// ======================================================
export default function AvaliacaoAntropometricaForm() {
  const { pacienteId } = useParams();
  const navigate = useNavigate();

  const [resultado, setResultado] = useState<AvaliacaoResultadoDTO | null>(null);
  const [modalAberto, setModalAberto] = useState(false);

  const [nomePaciente, setNomePaciente] = useState("");

  const [form, setForm] = useState<AvaliacaoRequest>({
    pacienteId: Number(pacienteId),
    dataAvaliacao: new Date().toISOString(),
    circunferenciaCintura: 0,
    circunferenciaQuadril: 0,

    pcb: 0,
    pcp: 0,
    pct: 0,
    pcsa: 0,
    pcse: 0,
    pcsi: 0,
    pcab: 0,
    pccx: 0,

    metodo: 0,
  });

  useEffect(() => {
    async function load() {
      if (!pacienteId) return;

      const dados: DadosAvaliacao = await obterDadosParaAvaliacao(Number(pacienteId));
      setNomePaciente(dados.nomeCompleto);
    }

    load();
  }, [pacienteId]);

  function update<K extends keyof AvaliacaoRequest>(key: K, value: AvaliacaoRequest[K]) {
    setForm((prev) => ({ ...prev, [key]: value }));
  }


  // ================================================
  //                 ENVIAR FORMULÁRIO
  // ================================================
  async function enviar(e: React.FormEvent) {
    e.preventDefault();

    try {
      const resposta = await salvarAvaliacao(form);

      // guarda resultado no estado
      setResultado(resposta.data);

      // abre modal
      setModalAberto(true);

    } catch (err: any) {
      console.error("ERRO BACKEND:", err.response?.data);
      alert("Erro ao salvar avaliação!");
    }
  }



  // ======================================================
  //                        UI
  // ======================================================
  return (
    <div className="p-8 max-w-3xl mx-auto">

      {/* MODAL */}
      <ModalResultado
        aberto={modalAberto}
        resultado={resultado}
        onClose={() => {
          setModalAberto(false);
          navigate("/painel-nutricionista");
        }}
      />


      <div className="flex justify-between items-center mb-8">
        <h1 className="text-4xl font-bold">Avaliação Antropométrica</h1>

        <Button
          onClick={() => navigate(-1)}
          className="px-5 py-2 bg-gray-300 text-gray-800 rounded-xl font-medium hover:bg-gray-400 transition"
        >
          ← Voltar
        </Button>
      </div>

      <p className="text-gray-700 mb-8 text-lg">
        Avaliando paciente <b>{nomePaciente}</b>
      </p>

   


      {/* FORMULÁRIO */}
      <form onSubmit={enviar} className="space-y-10">

                       <div className="bg-white border rounded-2xl p-6 shadow-sm">
              <h2 className="text-2xl font-semibold mb-6">Data da Avaliação</h2>

              <Input
                type="date"
                value={form.dataAvaliacao.split("T")[0]}
                onChange={(e) => update("dataAvaliacao", e.target.value)}
              />
            </div>

        {/* CIRCUNFERÊNCIAS */}
        <div className="bg-white border rounded-2xl p-6 shadow-sm">
          <h2 className="text-2xl font-semibold mb-6">Circunferências</h2>

          <div className="grid grid-cols-2 gap-8">

            <div>
              <h4 className="font-semibold text-gray-900 mb-2">Cintura (cm)</h4>
              <Input
                type="number"
                value={form.circunferenciaCintura}
                onChange={(e) => update("circunferenciaCintura", Number(e.target.value))}
              />
            </div>

            <div>
              <h4 className="font-semibold text-gray-900 mb-2">Quadril (cm)</h4>
              <Input
                type="number"
                value={form.circunferenciaQuadril}
                onChange={(e) => update("circunferenciaQuadril", Number(e.target.value))}
              />
            </div>

          </div>
        </div>


        {/* PROTOCOLO */}
        <div className="bg-white border rounded-2xl p-6 shadow-sm">
          <h2 className="text-2xl font-semibold mb-6">Protocolo</h2>

          <select
            className="border p-3 rounded-lg w-full"
            value={form.metodo}
            onChange={(e) => update("metodo", Number(e.target.value))}
          >
            <option value={0}>Selecione o método</option>
            <option value={1}>Jackson & Pollock – 3 Dobras</option>
            <option value={2}>Jackson & Pollock – 7 Dobras</option>
            <option value={3}>Durnin & Womersley</option>
            <option value={4}>Faulkner</option>
            <option value={5}>Guedes</option>
          </select>
        </div>


        {/* PREGAS CUTÂNEAS */}
        <div className="bg-white border rounded-2xl p-6 shadow-sm">
          <h2 className="text-2xl font-semibold mb-6">Pregas Cutâneas</h2>

          <div className="grid grid-cols-3 gap-8">

            {/** PCT */}
            <div>
              <h4 className="font-semibold text-gray-900 mb-2">Tríceps (PCT)</h4>
              <Input
                type="number"
                value={form.pct}
                onChange={(e) => update("pct", Number(e.target.value))}
              />
            </div>

            {/** PCSE */}
            <div>
              <h4 className="font-semibold text-gray-900 mb-2">Subescapular (PCSE)</h4>
              <Input
                type="number"
                value={form.pcse}
                onChange={(e) => update("pcse", Number(e.target.value))}
              />
            </div>

            {/** PCSI */}
            <div>
              <h4 className="font-semibold text-gray-900 mb-2">Supra-ilíaca (PCSI)</h4>
              <Input
                type="number"
                value={form.pcsi}
                onChange={(e) => update("pcsi", Number(e.target.value))}
              />
            </div>

            {/** PCAB */}
            <div>
              <h4 className="font-semibold text-gray-900 mb-2">Abdominal (PCAB)</h4>
              <Input
                type="number"
                value={form.pcab}
                onChange={(e) => update("pcab", Number(e.target.value))}
              />
            </div>

            {/** PCCX */}
            <div>
              <h4 className="font-semibold text-gray-900 mb-2">Coxa (PCCX)</h4>
              <Input
                type="number"
                value={form.pccx}
                onChange={(e) => update("pccx", Number(e.target.value))}
              />
            </div>

            {/** PCB */}
            <div>
              <h4 className="font-semibold text-gray-900 mb-2">Bíceps (PCB)</h4>
              <Input
                type="number"
                value={form.pcb}
                onChange={(e) => update("pcb", Number(e.target.value))}
              />
            </div>

            {/** PCP */}
            <div>
              <h4 className="font-semibold text-gray-900 mb-2">Peitoral (PCP)</h4>
              <Input
                type="number"
                value={form.pcp}
                onChange={(e) => update("pcp", Number(e.target.value))}
              />
            </div>

            {/** PCSA */}
            <div>
              <h4 className="font-semibold text-gray-900 mb-2">Supra-axilar (PCSA)</h4>
              <Input
                type="number"
                value={form.pcsa}
                onChange={(e) => update("pcsa", Number(e.target.value))}
              />
            </div>

          </div>
        </div>

        <Button
          type="submit"
          className="
            w-full py-4 text-white text-lg font-semibold
            bg-blue-600 rounded-xl 
            transition hover:bg-green-600 hover:scale-[1.02]
          "
        >
          Salvar Avaliação
        </Button>

      </form>
    </div>
  );
}
