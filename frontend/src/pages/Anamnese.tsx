import { useState } from "react";
import axios from "axios";
import "../index.css"; // garante que o CSS global será carregado

const API_URL = "https://localhost:7155/api/Anamnese";

export default function RegistrarAnamnesePage() {
  const [formData, setFormData] = useState({
    nomeCompleto: "",
    dataNascimento: "",
    ocupacao: "",
    praticaAtividadeFisica: false,
    atividadeFisicaTipo: "",
    atividadeFisicaHorario: "",
    atividadeFisicaFrequencia: "",
    historicoPessoal_HAS: false,
    historicoPessoal_DM: false,
    historicoPessoal_Hipercolesterolemia: false,
    historicoPessoal_DoencaCardiaca: false,
    historicoFamiliar_HAS: false,
    historicoFamiliar_DM: false,
    historicoFamiliar_Hipercolesterolemia: false,
    historicoFamiliar_DoencaCardiaca: false,
    usaMedicamento: false,
    medicamentos: "",
    usaSuplemento: false,
    suplementos: "",
    temAlergiaAlimentar: false,
    alergias: "",
    intoleranciaLactose: false,
    aversoesAlimentares: "",
    consumoAguaDiario: "",
    frequenciaIntestinal: "Desconhecida",
    sexo: "Masculino",
    peso: "",
    altura: "",
    fatorAtividade: "Sedentario",
  });

  const [mensagem, setMensagem] = useState("");

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
    const { name, value, type } = e.target;
    if (e.target instanceof HTMLInputElement && type === "checkbox") {
      setFormData({ ...formData, [name]: e.target.checked });
    } else {
      setFormData({ ...formData, [name]: value });
    }
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setMensagem("Enviando...");

    try {
      const token = localStorage.getItem("token");

      // Monta o payload conforme o backend espera
      const payload = {
        pacienteId: 1, // TODO: pegar dinamicamente do token no futuro
        nomeCompleto: formData.nomeCompleto,
        dataNascimento: new Date(formData.dataNascimento).toISOString(),
        ocupacao: formData.ocupacao,
        praticaAtividadeFisica: formData.praticaAtividadeFisica,
        atividadeFisicaTipo: formData.atividadeFisicaTipo,
        atividadeFisicaHorario: formData.atividadeFisicaHorario,
        atividadeFisicaFrequencia: formData.atividadeFisicaFrequencia,

        historicoFamiliar_HAS: formData.historicoFamiliar_HAS,
        historicoFamiliar_DM: formData.historicoFamiliar_DM,
        historicoFamiliar_Hipercolesterolemia: formData.historicoFamiliar_Hipercolesterolemia,
        historicoFamiliar_DoencaCardiaca: formData.historicoFamiliar_DoencaCardiaca,

        historicoPessoal_HAS: formData.historicoPessoal_HAS,
        historicoPessoal_DM: formData.historicoPessoal_DM,
        historicoPessoal_Hipercolesterolemia: formData.historicoPessoal_Hipercolesterolemia,
        historicoPessoal_DoencaCardiaca: formData.historicoPessoal_DoencaCardiaca,

        usaMedicamento: formData.usaMedicamento,
        medicamentos: formData.medicamentos,
        usaSuplemento: formData.usaSuplemento,
        suplementos: formData.suplementos,

        temAlergiaAlimentar: formData.temAlergiaAlimentar,
        alergias: formData.alergias,
        intoleranciaLactose: formData.intoleranciaLactose,
        aversoesAlimentares: formData.aversoesAlimentares,

        consumoAguaDiario: Number(formData.consumoAguaDiario) || 0,

        frequenciaIntestinal:
          formData.frequenciaIntestinal === "Diaria"
            ? 0
            : formData.frequenciaIntestinal === "EmDiasAlternados"
            ? 1
            : formData.frequenciaIntestinal === "Semanal"
            ? 2
            : 3,

        sexo: formData.sexo === "Masculino" ? 0 : 1,
        peso: Number(formData.peso) || 0,
        altura: Number(formData.altura) || 0,

        fatorAtividade:
          formData.fatorAtividade === "Sedentario"
            ? 0
            : formData.fatorAtividade === "Leve"
            ? 1
            : formData.fatorAtividade === "Moderado"
            ? 2
            : 3,
      };

      console.log("JSON enviado:", payload);

      const response = await axios.post(API_URL, payload, {
        headers: {
          Authorization: `Bearer ${token}`,
          "Content-Type": "application/json",
        },
      });

      console.log("Anamnese salva:", response.data);
      setMensagem("✅ Anamnese cadastrada com sucesso!");
    } catch (error: any) {
      console.error("Erro ao salvar anamnese:", error);
      if (error.response?.data) {
        console.error("Detalhe do erro:", error.response.data);
      }
      setMensagem("❌ Erro ao salvar anamnese.");
    }
  };

  return (
    <div className="anamnese-wrapper">
      <div className="anamnese-container">
        <h1>Registrar Anamnese do Paciente</h1>
        <form onSubmit={handleSubmit} className="anamnese-form">
          <fieldset>
            <legend>Identificação</legend>
            <input name="nomeCompleto" type="text" placeholder="Nome Completo" onChange={handleChange} />
            <input name="dataNascimento" type="date" onChange={handleChange} />
            <input name="ocupacao" type="text" placeholder="Ocupação" onChange={handleChange} />
          </fieldset>

          <fieldset>
            <legend>Dados corporais</legend>
            <select name="sexo" onChange={handleChange}>
              <option value="Masculino">Masculino</option>
              <option value="Feminino">Feminino</option>
            </select>
            <input name="peso" type="number" placeholder="Peso (kg)" onChange={handleChange} />
            <input name="altura" type="number" placeholder="Altura (cm)" onChange={handleChange} />
          </fieldset>

          <fieldset>
            <legend>Atividade Física</legend>
            <label>
              <input type="checkbox" name="praticaAtividadeFisica" onChange={handleChange} /> Pratica atividade física
            </label>
            <input name="atividadeFisicaTipo" placeholder="Tipo" onChange={handleChange} />
            <input name="atividadeFisicaHorario" placeholder="Horário" onChange={handleChange} />
            <input name="atividadeFisicaFrequencia" placeholder="Frequência" onChange={handleChange} />
          </fieldset>

          <fieldset>

          
          <fieldset>
            <legend>Histórico Familiar</legend>
                <div className="field-group">
            {["HAS", "DM", "Hipercolesterolemia", "DoencaCardiaca"].map((item) => (
              <label key={item}>
                <input type="checkbox" name={`historicoFamiliar_${item}`} onChange={handleChange} /> {item}
              </label>
            ))}
            </div>
          </fieldset>

            
            <legend>Histórico Pessoal</legend>
              <div className="field-group">
            {["HAS", "DM", "Hipercolesterolemia", "DoencaCardiaca"].map((item) => (
              <label key={item}>
                <input type="checkbox" name={`historicoPessoal_${item}`} onChange={handleChange} /> {item}
              </label>
            ))}
            </div>
          </fieldset>

          <fieldset>
            <legend>Medicações e Suplementos</legend>
            <label>
              <input type="checkbox" name="usaMedicamento" onChange={handleChange} /> Usa Medicamentos
            </label>
            <input name="medicamentos" placeholder="Quais medicamentos?" onChange={handleChange} />

            <label>
              <input type="checkbox" name="usaSuplemento" onChange={handleChange} /> Usa Suplementos
            </label>
            <input name="suplementos" placeholder="Quais suplementos?" onChange={handleChange} />
          </fieldset>

          <fieldset>
            <legend>Alergias e Restrições</legend>
            <label>
              <input type="checkbox" name="temAlergiaAlimentar" onChange={handleChange} /> Tem alergia alimentar
            </label>
            <input name="alergias" placeholder="Quais alergias?" onChange={handleChange} />

            <label>
              <input type="checkbox" name="intoleranciaLactose" onChange={handleChange} /> Intolerância à lactose
            </label>
            <input name="aversoesAlimentares" placeholder="Aversões alimentares" onChange={handleChange} />
          </fieldset>

          <fieldset>
            <legend>Rotina</legend>
            <input
              name="consumoAguaDiario"
              type="number"
              placeholder="Consumo diário de água (ml)"
              onChange={handleChange}
            />
          </fieldset>

          <fieldset>
            <legend>Frequência Intestinal</legend>
            <select name="frequenciaIntestinal" onChange={handleChange}>
              <option value="Diaria">Diária</option>
              <option value="EmDiasAlternados">Em dias alternados</option>
              <option value="Semanal">Semanal</option>
              <option value="Desconhecida">Desconhecida</option>
            </select>
          </fieldset>

          <fieldset>
            <legend>Fator de Atividade</legend>
            <select name="fatorAtividade" onChange={handleChange}>
              <option value="Sedentario">Sedentário</option>
              <option value="Leve">Leve</option>
              <option value="Moderado">Moderado</option>
              <option value="Intenso">Intenso</option>
            </select>
          </fieldset>

          <button type="submit" className="btn-submit">Registrar Anamnese</button>
          {mensagem && <p className="mensagem">{mensagem}</p>}
        </form>
      </div>
    </div>
  );
}