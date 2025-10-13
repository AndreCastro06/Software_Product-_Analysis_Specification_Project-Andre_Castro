import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import LoginPage from "../pages/LoginPage";
import RegisterPage from "../pages/RegisterPage";
import PainelNutricionista from "../pages/painel-nutricionista"; // Painel do Nutricionista
import AnamnesePage from "../pages/Anamnese"; // Painel do Nutricionista

// (futuramente adicionamos PacientePage também)

export default function AppRoutes() {
  return (
    <BrowserRouter>
      <Routes>
        {/* Rota padrão → Login */}
        <Route path="/" element={<LoginPage />} />

        {/* Cadastro */}
        <Route path="/register" element={<RegisterPage />} />

        {/* Painel do Nutricionista */}
        <Route path="/painel-nutricionista" element={<PainelNutricionista />} />

           {/* Painel do Nutricionista */}
        <Route path="/RegistrarAnamnesePage" element={<AnamnesePage/>} />

        {/* (Opcional) Painel do Paciente */}
        {/* <Route path="/painel-paciente" element={<PacientePage />} /> */}

        {/* Qualquer outra rota redireciona pro login */}
        <Route path="*" element={<Navigate to="/" />} />
      </Routes>
    </BrowserRouter>
  );
}