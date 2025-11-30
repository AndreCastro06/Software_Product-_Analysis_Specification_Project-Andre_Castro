// src/types/avaliacao.ts

export interface AvaliacaoRequest {
  pacienteId: number;
  dataAvaliacao: string;
  circunferenciaCintura: number;
  circunferenciaQuadril: number;

  pcb?: number;
  pcp?: number;
  pct?: number;
  pcsa?: number;
  pcse?: number;
  pcsi?: number;
  pcab?: number;
  pccx?: number;

  metodo: number;   
}

export interface AvaliacaoResponse {
  id: number;
  pacienteId: number;

  // valores derivados da anamnese
  peso: number;
  altura: number;
  idade: number;
  sexo: string;

  circunferenciaCintura: number;
  circunferenciaQuadril: number;

  pregas: {
    pcb?: number | null;
    pcp?: number | null;
    pct?: number | null;
    pcsa?: number | null;
    pcse?: number | null;
    pcsi?: number | null;
    pcab?: number | null;
    pccx?: number | null;
  };

  dataAvaliacao: string;

  

  percentualGordura: number;
  massaMagra: number;
  massaGorda: number;
  tmb: number;
  imc: number;

  metodo: number;
  get: number;
  geb: number;
}
