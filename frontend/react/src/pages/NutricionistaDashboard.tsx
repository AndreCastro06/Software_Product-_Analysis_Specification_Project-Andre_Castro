// src/pages/NutricionistaDashboard.tsx

import DashboardCard from "../components/DashboardCard";
import { LogOut, UserPlus, Activity, Utensils } from "lucide-react";
import { Button } from "../components/ui/button";
import { useNavigate } from "react-router-dom";

export default function NutricionistaDashboard() {
  const navigate = useNavigate();

  function logout() {
    localStorage.removeItem("token");
    localStorage.removeItem("role");
    navigate("/");
  }

  return (
    <div className="min-h-screen bg-white p-8 flex flex-col items-center">

      {/* HEADER */}
      <div className="w-full max-w-5xl flex justify-between items-center mb-10">
        <h1 className="text-3xl font-bold text-gray-800">
          Painel do Nutricionista
        </h1>

        <Button
          variant="outline"
          onClick={logout}
          className="
            flex items-center gap-2
            bg-blue-600 text-white px-4 py-2 rounded-lg
            transition-all duration-300
            hover:bg-red-600 hover:scale-105
          "
        >
          <LogOut className="w-4 h-4" />
          Sair
        </Button>
      </div>

      {/* CARDS */}
      <div className="grid grid-cols-1 md:grid-cols-3 gap-8 justify-items-center w-full max-w-6xl">

        {/* CARD 1 */}
        <div
          className="
            w-full max-w-sm p-6 rounded-xl shadow-md cursor-pointer"
        >
          <DashboardCard
            title="Nova Anamnese"
            description="Cadastrar paciente + anamnese inicial"
            icon={<UserPlus />}
            to="/anamnese/nova"
          />
        </div>

        {/* CARD 2 */}
        <div
          className="
            w-full max-w-sm p-6 rounded-xl shadow-md 
          "
        >
          <DashboardCard
            title="Avaliação Antropométrica"
            description="Registrar medidas e composição corporal"
            icon={<Activity />}
            to="/antropometrica"
          />
        </div>

        {/* CARD 3 */}
        <div
          className="
            w-full max-w-sm p-6 rounded-xl shadow-md cursor-pointer
          "
        >
          <DashboardCard
            title="Plano Alimentar"
            description="Criar plano alimentar personalizado"
            icon={<Utensils />}
            to="/plano-alimentar"
          />
        </div>
 
        {/* CARD 4 - Histórico */}
        <div
          className="
            w-full max-w-sm p-6 rounded-xl shadow-md cursor-pointer
          "
        >
          <DashboardCard
            title="Histórico de Avaliações"
            description="Acompanhar evolução dos pacientes"
            icon={<Activity />}
            to="/historico/pacientes"
          />
        </div>


      </div>
    </div>
  );
}
