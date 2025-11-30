import { useState } from "react";
import { login } from "../services/authService";
import { useNavigate } from "react-router-dom";
import { Button } from "../components/ui/button";
import { Input } from "../components/ui/input";
import logo from "../assets/logo.png";
import "../index.css";

export default function LoginPage() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [erro, setErro] = useState("");
  const navigate = useNavigate();

  async function handleLogin(e: React.FormEvent) {
    e.preventDefault();
    setErro("");

    try {
      const response = await login({ email, password });
      const data = response.data as { token: string; role: string };

      localStorage.setItem("token", data.token);
      localStorage.setItem("role", data.role);

      if (data.role === "Nutricionista") {
      navigate("/painel-nutricionista");
      } else {
        navigate("/paciente");
      }
    } catch {
      setErro("Credenciais inválidas");
    }
  }

  return (
    <div className="min-h-screen flex items-center justify-center bg-[#fafafa]">
      
      <form
        onSubmit={handleLogin}
        className="
          bg-white
          shadow-lg
          p-10
          rounded-2xl
          w-[380px]
          space-y-6
          border border-gray-200
        "
      >
                        <img 
          src={logo} 
          alt="Logo PEACE" 
          className="mx-auto w-48 mb-6" 
        />

        <h2 className="text-3xl font-semibold text-center text-gray-800">
          Login
        </h2>

        {erro && (
          <p className="text-red-500 text-sm text-center">{erro}</p>
        )}

        <div className="space-y-4">
          <Input
            type="email"
            placeholder="E-mail"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            className="w-full"
          />

          <Input
            type="password"
            placeholder="Senha"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            className="w-full"
          />
        </div>
        <Button
          type="submit"
          className="w-full bg-orange-600 hover:bg-orange-700 text-white py-2 rounded-lg font-medium"
        >
          Entrar
        </Button>

        <div className="text-center">
          <button
            type="button"
            onClick={() => navigate("/register")}
            className="text-blue-600 hover:underline text-sm font-medium"
          >
            Criar conta (Nutricionista)
          </button>
        </div>
      </form>
    </div>
  );
}
