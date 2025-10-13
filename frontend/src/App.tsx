import { BrowserRouter as Router, Routes, Route, Navigate } from "react-router-dom";
import LoginPage from "./pages/LoginPage";
import RegisterPage from "./pages/RegisterPage";
import PainelNutricionista from "./pages/painel-nutricionista";
import RegistrarAnamnesePage from "./pages/Anamnese";

export default function AppRoutes() {
  return (
    <Router>
      <Routes>
        {/* Página inicial redireciona para login */}
        <Route path="/" element={<Navigate to="/login" />} />

        {/* Autenticação */}
        <Route path="/login" element={<LoginPage />} />
        <Route path="/register" element={<RegisterPage />} />

        {/* Painel do Nutricionista */}
        <Route path="/painel-nutricionista" element={<PainelNutricionista />} />

        {/* Registrar Anamnese */}
        <Route path="/registrar-anamnese" element={<RegistrarAnamnesePage />} />

        {/* Rota fallback (404) */}
        <Route path="*" element={<h1 style={{ textAlign: "center", marginTop: "3rem" }}>404 - Página não encontrada</h1>} />
      </Routes>
    </Router>
  );
}