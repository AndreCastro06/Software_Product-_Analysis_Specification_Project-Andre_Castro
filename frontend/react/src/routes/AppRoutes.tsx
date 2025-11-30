import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";

import LoginPage from "../pages/LoginPage";
import RegisterPage from "../pages/RegisterNutricionista";

import NutricionistaDashboard from "../pages/NutricionistaDashboard";
import AnamnesePage from "../pages/AnamneseForm";
import AvaliacaoAntrometricaPage from "../pages/AvaliacaoAntropometricaForm";
import SelecionarPacienteAvaliacao from "../pages/SelecionarPacienteAvaliacao";
import HistoricoPacientes from "../pages/HistoricoPacientes";
import HistoricoAvaliacoes from "../pages/HistoricoAvaliacoes";


export default function AppRoutes() {
  return (
    <BrowserRouter>
      <Routes>
        {/* Rota padrão → Login */}
        <Route path="/" element={<LoginPage />} />

        {/* Cadastro de nutricionista */}
        <Route path="/register" element={<RegisterPage />} />

        {/* Painel principal do nutricionista */}
        <Route path="/painel-nutricionista" element={<NutricionistaDashboard />} />

        {/* Cadastro de Anamnese */}
        <Route path="/anamnese/nova" element={<AnamnesePage />} />

        {/* Seleciona paciente para avaliacao Antropometrica */}
        <Route path="/antropometrica" element={<SelecionarPacienteAvaliacao />} />

        {/* Cadastro de Avaliacao Antropometrica */}
        <Route path="/antropometrica/:pacienteId" element={<AvaliacaoAntrometricaPage />} />
        
        {/* Lista todos os pacientes */}
        <Route path="/historico/pacientes" element={<HistoricoPacientes />} />

        {/* Histórico de avaliações de um paciente */}
        <Route path="/historico/pacientes/:pacienteId" element={<HistoricoAvaliacoes />} />

        {/* Qualquer outra rota redireciona para login */}
        <Route path="*" element={<Navigate to="/" />} />
        
      </Routes>
    </BrowserRouter>
  );
}
