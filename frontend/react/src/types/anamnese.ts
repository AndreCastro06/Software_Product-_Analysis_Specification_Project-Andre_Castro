// src/types/anamnese.ts

// REQUEST enviado ao backend
export interface AnamneseRequest {
  pacienteId?: number | null;

  nomeCompleto: string;
  dataNascimento: string;
  sexo: string;

  peso: number;
  altura: number;
  fatorAtividade: number;
  dataConsulta: string;
  ocupacao?: string;

  praticaAtividadeFisica: boolean;
  atividadeFisicaTipo?: string;
  atividadeFisicaHorario?: string;
  atividadeFisicaFrequencia?: string;

  historicoFamiliar_HAS: boolean;
  historicoFamiliar_DM: boolean;
  historicoFamiliar_Hipercolesterolemia: boolean;
  historicoFamiliar_DoencaCardiaca: boolean;

  historicoPessoal_HAS: boolean;
  historicoPessoal_DM: boolean;
  historicoPessoal_Hipercolesterolemia: boolean;
  historicoPessoal_DoencaCardiaca: boolean;

  usaMedicamento: boolean;
  medicamentos?: string;

  usaSuplemento: boolean;
  suplementos?: string;

  temAlergiaAlimentar: boolean;
  alergias?: string;

  intoleranciaLactose: boolean;
  aversoesAlimentares?: string;

  consumoAguaDiario?: number | null;
  frequenciaIntestinal: number;
}

// RESPONSE vindo do backend
export interface AnamneseResponse {
  id: number;
  pacienteId: number;

  nomeCompleto: string;
  dataNascimento: string;
  idade: number;

  sexo: string;
  peso: number;
  altura: number;
  fatorAtividade: number;
  

  ocupacao?: string | null;

  praticaAtividadeFisica: boolean;
  atividadeFisicaTipo?: string | null;
  atividadeFisicaHorario?: string | null;
  atividadeFisicaFrequencia?: string | null;

  historicoFamiliar_HAS: boolean;
  historicoFamiliar_DM: boolean;
  historicoFamiliar_Hipercolesterolemia: boolean;
  historicoFamiliar_DoencaCardiaca: boolean;

  historicoPessoal_HAS: boolean;
  historicoPessoal_DM: boolean;
  historicoPessoal_Hipercolesterolemia: boolean;
  historicoPessoal_DoencaCardiaca: boolean;

  usaMedicamento: boolean;
  medicamentos?: string | null;

  usaSuplemento: boolean;
  suplementos?: string | null;

  temAlergiaAlimentar: boolean;
  alergias?: string | null;

  intoleranciaLactose: boolean;
  aversoesAlimentares?: string | null;

  consumoAguaDiario?: number | null;
  frequenciaIntestinal: number;
}
