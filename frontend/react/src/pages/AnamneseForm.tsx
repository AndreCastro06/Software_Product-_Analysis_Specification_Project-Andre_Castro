import { useState } from "react";
import { useNavigate } from "react-router-dom";

import { criarAnamnese } from "../services/anamneseService";
import type { AnamneseRequest } from "../types/anamnese";

import { Input } from "../components/ui/input";
import { Button } from "../components/ui/button";
import { Textarea } from "../components/ui/textarea";

export default function AnamneseForm() {
  const navigate = useNavigate();
  const [activeTab, setActiveTab] = useState("dados");

  const [form, setForm] = useState<AnamneseRequest>({
    nomeCompleto: "",
    dataNascimento: "",
    sexo: "",
    dataConsulta: new Date().toISOString().split("T")[0],
    peso: 0,
    altura: 0,
    fatorAtividade: 0,
    ocupacao: "",
    praticaAtividadeFisica: false,
    atividadeFisicaTipo: "",
    atividadeFisicaHorario: "",
    atividadeFisicaFrequencia: "",
    historicoFamiliar_HAS: false,
    historicoFamiliar_DM: false,
    historicoFamiliar_Hipercolesterolemia: false,
    historicoFamiliar_DoencaCardiaca: false,
    historicoPessoal_HAS: false,
    historicoPessoal_DM: false,
    historicoPessoal_Hipercolesterolemia: false,
    historicoPessoal_DoencaCardiaca: false,
    usaMedicamento: false,
    medicamentos: "",
    usaSuplemento: false,
    suplementos: "",
    temAlergiaAlimentar: false,
    alergias: "",
    intoleranciaLactose: false,
    aversoesAlimentares: "",
    consumoAguaDiario: 0,
    frequenciaIntestinal: 0,
  });

  function update<K extends keyof AnamneseRequest>(key: K, value: AnamneseRequest[K]) {
    setForm((prev) => ({ ...prev, [key]: value }));
  }

  async function enviar(e: React.FormEvent<HTMLFormElement>) {
    e.preventDefault();
    await criarAnamnese(form);
    alert("Anamnese cadastrada com sucesso!");
    navigate("/painel-nutricionista");
  }

  return (
    <div className="p-6 max-w-4xl mx-auto">

      <div className="flex items-center justify-between mb-6">
        <h1 className="text-3xl font-bold">Cadastrar Anamnese</h1>

        <Button
          type="button"
          onClick={() => navigate(-1)}
          className="
            px-4 py-2 
            bg-gray-600 text-gray-700 
            rounded-lg 
            font-medium shadow-sm
            transition-all duration-300
            hover:border-blue-600  hover:scale-105
          "
        >
          ← Voltar
        </Button>
      </div>

      <form onSubmit={enviar}>

        {/* ------- TABS ------- */}
        <div className="flex gap-2 border-b mb-6">
          {[
            ["dados", "Dados Básicos"],
            ["atividade", "Atividade Física"],
            ["historico", "Histórico Clínico"],
            ["alergias", "Alergias / Medicações"],
            ["habitos", "Hábitos"],
          ].map(([value, label]) => (
            <button
              key={value}
              type="button"
              onClick={() => setActiveTab(value)}
              className={`px-4 py-2 border-b-2 transition
                ${activeTab === value ? "border-blue-600 font-semibold" : "border-transparent text-gray-500"}`}
            >
              {label}
            </button>
          ))}
        </div>

        {/* ------- ABA 1: DADOS BÁSICOS ------- */}
        {activeTab === "dados" && (
          <div className="grid grid-cols-2 gap-4">

            <div>
              <label className="font-medium">Nome completo</label>
              <Input
                placeholder="Ex: Ana Oliveira"
                value={form.nomeCompleto}
                onChange={(e) => update("nomeCompleto", e.target.value)}
              />
            </div>

            <div>
              <label className="font-medium">Data da consulta</label>
              <Input
                type="date"
                value={form.dataConsulta}
                onChange={(e) => update("dataConsulta", e.target.value)}
              />
            </div>

            <div>
              <label className="font-medium">Data de nascimento</label>
              <Input
                type="date"
                value={form.dataNascimento}
                onChange={(e) => update("dataNascimento", e.target.value)}
              />
            </div>

            <div>
              <label className="font-medium">Sexo</label>
              <select
                value={form.sexo}
                onChange={(e) => update("sexo", e.target.value)}
                className="border rounded-md px-3 py-2 w-full bg-white"
                required
              >
                <option value="">Selecione o sexo</option>
                <option value="Masculino">Masculino</option>
                <option value="Feminino">Feminino</option>
              </select>
            </div>

            <div>
              <label className="font-medium">Peso (kg)</label>
              <Input
                type="number"
                value={form.peso}
                onChange={(e) => update("peso", Number(e.target.value))}
              />
            </div>

            <div>
              <label className="font-medium">Altura (cm)</label>
              <Input
                type="number"
                value={form.altura}
                onChange={(e) => update("altura", Number(e.target.value))}
              />
            </div>

            <div>
              <label className="font-medium">Fator de atividade</label>
              <select
                value={form.fatorAtividade}
                onChange={(e) => update("fatorAtividade", Number(e.target.value))}
                className="border rounded-md px-3 py-2 w-full bg-white"
                required
              >
                <option value="">Fator de atividade</option>
                <option value={0}>Sedentário (1.2)</option>
                <option value={1}>Levemente ativo (1.375)</option>
                <option value={2}>Moderadamente ativo (1.55)</option>
                <option value={3}>Muito ativo (1.725)</option>
                <option value={4}>Extremamente ativo (1.9)</option>
              </select>
            </div>

            <div className="col-span-2">
              <label className="font-medium">Ocupação</label>
              <Input
                placeholder="Ex: Analista de sistemas"
                value={form.ocupacao || ""}
                onChange={(e) => update("ocupacao", e.target.value)}
              />
            </div>
          </div>
        )}

        {/* ------- ABA 2: ATIVIDADE FÍSICA ------- */}
        {activeTab === "atividade" && (
          <div className="grid grid-cols-2 gap-4">

            <label className="flex items-center gap-2 col-span-2">
              <input
                type="checkbox"
                checked={form.praticaAtividadeFisica}
                onChange={(e) => update("praticaAtividadeFisica", e.target.checked)}
              />
              Pratica atividade física regularmente?
            </label>

            {form.praticaAtividadeFisica && (
              <>
                <div>
                  <label className="font-medium">Tipo de atividade</label>
                  <Input
                    placeholder="Ex: musculação, corrida"
                    value={form.atividadeFisicaTipo || ""}
                    onChange={(e) => update("atividadeFisicaTipo", e.target.value)}
                  />
                </div>

                <div>
                  <label className="font-medium">Horário</label>
                  <Input
                    placeholder="Ex: 07:00"
                    value={form.atividadeFisicaHorario || ""}
                    onChange={(e) => update("atividadeFisicaHorario", e.target.value)}
                  />
                </div>

                <div>
                  <label className="font-medium">Frequência semanal</label>
                  <Input
                    placeholder="Ex: 5x na semana"
                    value={form.atividadeFisicaFrequencia || ""}
                    onChange={(e) => update("atividadeFisicaFrequencia", e.target.value)}
                  />
                </div>
              </>
            )}
          </div>
        )}

        {/* ------- ABA 3: HISTÓRICO CLÍNICO ------- */}
        {activeTab === "historico" && (
          <div className="grid grid-cols-1 md:grid-cols-2 gap-6">

            {/* Histórico Familiar */}
            <div>
              <h2 className="text-lg font-semibold mb-2">Histórico Familiar</h2>
              <div className="space-y-2 flex flex-col">
                {[
                  ["historicoFamiliar_HAS", "Hipertensão (HAS)"],
                  ["historicoFamiliar_DM", "Diabetes"],
                  ["historicoFamiliar_Hipercolesterolemia", "Hipercolesterolemia"],
                  ["historicoFamiliar_DoencaCardiaca", "Doença cardíaca"],
                ].map(([key, label]) => (
                  <label key={key} className="flex items-center gap-2">
                    <input
                      type="checkbox"
                      checked={form[key as keyof AnamneseRequest] as boolean}
                      onChange={(e) => update(key as any, e.target.checked)}
                    />
                    {label}
                  </label>
                ))}
              </div>
            </div>

            {/* Histórico Pessoal */}
            <div>
              <h2 className="text-lg font-semibold mb-2">Histórico Pessoal</h2>
              <div className="space-y-2 flex flex-col">
                {[
                  ["historicoPessoal_HAS", "Hipertensão"],
                  ["historicoPessoal_DM", "Diabetes"],
                  ["historicoPessoal_Hipercolesterolemia", "Hipercolesterolemia"],
                  ["historicoPessoal_DoencaCardiaca", "Doença cardíaca"],
                ].map(([key, label]) => (
                  <label key={key} className="flex items-center gap-2">
                    <input
                      type="checkbox"
                      checked={form[key as keyof AnamneseRequest] as boolean}
                      onChange={(e) => update(key as any, e.target.checked)}
                    />
                    {label}
                  </label>
                ))}
              </div>
            </div>

          </div>
        )}

        {/* ------- ABA 4: ALERGIAS / MEDICAÇÕES ------- */}
        {activeTab === "alergias" && (
          <div className="grid gap-4">

            <label className="flex items-center gap-2">
              <input
                type="checkbox"
                checked={form.temAlergiaAlimentar}
                onChange={(e) => update("temAlergiaAlimentar", e.target.checked)}
              />
              Possui alergias alimentares?
            </label>

            {form.temAlergiaAlimentar && (
              <Textarea
                placeholder="Liste as alergias"
                value={form.alergias || ""}
                onChange={(e) => update("alergias", e.target.value)}
              />
            )}

            <label className="flex items-center gap-2 mt-4">
              <input
                type="checkbox"
                checked={form.usaMedicamento}
                onChange={(e) => update("usaMedicamento", e.target.checked)}
              />
              Usa algum medicamento?
            </label>

            {form.usaMedicamento && (
              <Textarea
                placeholder="Quais medicamentos?"
                value={form.medicamentos || ""}
                onChange={(e) => update("medicamentos", e.target.value)}
              />
            )}

            <label className="flex items-center gap-2 mt-4">
              <input
                type="checkbox"
                checked={form.usaSuplemento}
                onChange={(e) => update("usaSuplemento", e.target.checked)}
              />
              Usa suplementação?
            </label>

            {form.usaSuplemento && (
              <Textarea
                placeholder="Quais suplementos?"
                value={form.suplementos || ""}
                onChange={(e) => update("suplementos", e.target.value)}
              />
            )}
          </div>
        )}

        {/* ------- ABA 5: HÁBITOS ------- */}
        {activeTab === "habitos" && (
          <div className="grid grid-cols-2 gap-4">
            
            <div>
              <label className="font-medium">Consumo de água (ml/dia)</label>
              <Input
                type="number"
                value={form.consumoAguaDiario || 0}
                onChange={(e) => update("consumoAguaDiario", Number(e.target.value))}
              />
            </div>

            <div>
              <label className="font-medium">Frequência intestinal</label>
              <select
                value={form.frequenciaIntestinal}
                onChange={(e) => update("frequenciaIntestinal", Number(e.target.value))}
                className="border rounded-md px-3 py-2 w-full bg-white"
                required
              >
                <option value={0}>Desconhecida</option>
                <option value={1}>Diária</option>
                <option value={2}>A cada 2 a 3 dias</option>
                <option value={3}>Mais de 4 dias</option>
              </select>
            </div>
          </div>
        )}

        <Button
          type="submit"
          className="
            mt-6 w-full py-3 text-white font-semibold 
            bg-blue-600 
            rounded-lg 
            transition-all duration-300
            hover:bg-green-500 hover:scale-105
          "
        >
          Salvar Anamnese
        </Button>

      </form>
    </div>
  );
}
