import { useState } from "react";
import logo from "../assets/logo.png";
import { login } from "../services/authService";
import { useNavigate } from "react-router-dom";

export default function LoginPage() {
  const [email, setEmail] = useState("");
  const [senha, setSenha] = useState("");
  const [erro, setErro] = useState("");
  const navigate = useNavigate();


  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setErro("");

    try {
      const response = await login({ email, password: senha });
      const { token, nome, role } = response.data;

      console.log("Login bem-sucedido:", response.data);

      localStorage.setItem("token", token);
      localStorage.setItem("nome", nome);
      localStorage.setItem("role", role);


      if (role === "Nutricionista") {
        console.log("Login como Nutricionista");
        navigate("/painel-nutricionista");
      } else if (role === "Paciente") {
        console.log("Login como Paciente");
        // não redireciona
      } else {
        console.warn("Tipo de usuário não reconhecido:", role);
      }
    } catch (error: any) {
      console.error("Erro no login:", error);
       setErro("Usuário ou senha inválidos.");
       }
    }
   

  return (
    <div className="login-container">
      <div className="logo-container">
        <img src={logo} alt="Logo PEACE" className="logo" />
        <h1 className="login-title">Login</h1>
      </div>

      <form onSubmit={handleSubmit} className="login-form">
        <input
          type="email"
          placeholder="Email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          className="login-input"
        />
        <input
          type="password"
          placeholder="Senha"
          value={senha}
          onChange={(e) => setSenha(e.target.value)}
          className="login-input"
        />
        <button type="submit" className="login-button">Entrar</button>
        {erro && <p style={{ color: "red" }}>{erro}</p>}
      </form>

      <div style={{ marginTop: "1rem" }}>
        <span>Novo por aqui? </span>
        <a href="/register" style={{ color: "#007bff", textDecoration: "none", fontWeight: "bold" }}>
          crie sua conta
        </a>
      </div>
    </div>
    
  );
}