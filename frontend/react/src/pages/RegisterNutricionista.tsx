// src/pages/RegisterNutricionista.tsx

import { useState } from "react";
import { registerNutricionista } from "../services/authService";
import { Input } from "../components/ui/input";
import { Button } from "../components/ui/button";
import { useNavigate } from "react-router-dom";

export default function RegisterNutricionista() {
  const [nomeCompleto, setNomeCompleto] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [crn, setCrn] = useState("");

  const navigate = useNavigate();

  async function handleRegister(e: React.FormEvent) {
    e.preventDefault();

    await registerNutricionista({
      nomeCompleto,
      email,
      password,
      crn,
    });

    alert("Nutricionista cadastrado!");
    navigate("/");
  }

  return (
    <div className="flex h-screen items-center justify-center bg-gray-50">
      <form
        onSubmit={handleRegister}
        className="bg-white shadow p-8 rounded-xl w-96 space-y-4"
      >
        <h2 className="text-2xl font-bold text-center">Cadastrar Nutricionista</h2>

        <Input
          placeholder="Nome completo"
          value={nomeCompleto}
          onChange={(e) => setNomeCompleto(e.target.value)}
        />

        <Input
          placeholder="CRN"
          value={crn}
          onChange={(e) => setCrn(e.target.value)}
        />

        <Input
          placeholder="E-mail"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
        />

        <Input
          placeholder="Senha"
          type="password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />

             <Button
          type="submit"
          className="w-full bg-orange-600 hover:bg-orange-700 text-white py-2 rounded-lg font-medium"
        >
          Cadastar
        </Button>

        <Button
          type="button"
          className="w-full bg-gray-200 text-gray-800"
          onClick={() => navigate("/")}
        >
          Voltar
        </Button>
      </form>
    </div>
  );
}

