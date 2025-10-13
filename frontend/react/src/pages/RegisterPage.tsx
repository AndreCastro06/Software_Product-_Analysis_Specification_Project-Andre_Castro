import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { registerNutricionista, registerPaciente } from "../services/authService";

export default function RegisterPage() {
  const navigate = useNavigate();
  const [nome, setNome] = useState("");
  const [email, setEmail] = useState("");
  const [senha, setSenha] = useState("");
  const [confirmarSenha, setConfirmarSenha] = useState("");
  const [tipoConta, setTipoConta] = useState<"Nutricionista" | "Paciente">("Paciente");
  const [crn, setCrn] = useState("");

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    
    if (senha !== confirmarSenha) {
      alert("As senhas não coincidem!");
      return;
    }

    const dadosCadastro = {
        nome,
        email,
        password: senha,
      ...(tipoConta === "Nutricionista" && { crn })
      };

    try {
      if (tipoConta === "Nutricionista") {
        await registerNutricionista(dadosCadastro);
      } else {
        await registerPaciente(dadosCadastro);
      }
      
      alert("Cadastro realizado com sucesso!");
      navigate("/login");
    } catch (error) {
      console.error("Erro ao cadastrar:", error);
      alert("Erro ao cadastrar. Tente novamente.");
    }

  };

  return (
    <div className="register-container">
      <h1 className="title">Cadastro</h1>
      <form onSubmit={handleSubmit} className="register-form">
        <input
          type="text"
          placeholder="Nome completo"
          value={nome}
          onChange={(e) => setNome(e.target.value)}
          className="register-input"
        />

        {tipoConta === "Nutricionista" && (
                    <input
                      type="text"
                      placeholder="CRN"
                      value={crn}
                      onChange={(e) => setCrn(e.target.value)}
                      className="register-input"
                    />
                  )}

        <input
          type="email"
          placeholder="Email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          className="register-input"
        />
        <input
          type="password"
          placeholder="Senha"
          value={senha}
          onChange={(e) => setSenha(e.target.value)}
          className="register-input"
        />
        <input
          type="password"
          placeholder="Confirmar Senha"
          value={confirmarSenha}
          onChange={(e) => setConfirmarSenha(e.target.value)}
          className="register-input"
        />
          

        <select
          value={tipoConta}
          onChange={(e) => setTipoConta(e.target.value as "Nutricionista" | "Paciente")}
          className="register-input"
        >
          <option value="Paciente">Paciente</option>
          <option value="Nutricionista">Nutricionista</option>
        </select>

        <button type="submit" className="register-button">Cadastrar</button>
      </form>
    </div>
  );
}