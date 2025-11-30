import React from "react";
import { useNavigate } from "react-router-dom";
import { LogOut } from "lucide-react";

const AnamnesePage: React.FC = () => {
  const navigate = useNavigate();

  const handleLogout = () => {
    localStorage.removeItem("token");
    navigate("/login");
  };

  return (
    <div className="anamnese-container">
      {/* Topbar */}
      <header className="anamnese-header">
        <h1 className="anamnese-title">Painel do Nutricionista</h1>
        <button onClick={handleLogout} className="logout-button">
          <LogOut className="logout-icon" />
          Sair
        </button>
      </header>

      {/* Conteúdo principal */}
      <main className="anamnese-main">
        <div className="anamnese-grid">
          <button
            onClick={() => navigate("/registrar-anamnese")}
            className="anamnese-card card-green"
          >
            📋 Registrar Paciente
          </button>

          <button
            onClick={() => navigate("/pacientes-registrados")}
            className="anamnese-card card-blue"
          >
            👩‍⚕️ Pacientes Registrados
          </button>

          <button
            onClick={() => navigate("/calcular-dieta")}
            className="anamnese-card card-orange"
          >
            🔢 Calcular Necessidade Energética e Dieta
          </button>

          <button
            onClick={() => navigate("/avaliacao-fisica")}
            className="anamnese-card card-purple"
          >
            💪 Registrar Avaliação Física
          </button>
        </div>
      </main>
    </div>
  );
};

export default AnamnesePage;