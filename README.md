# 🧠 PEACE - Plataforma de Engajamento e Acompanhamento Clínico e Evolutivo

## 🎯 Visão Geral do Projeto

O PEACE é uma plataforma web voltada para nutricionistas e pacientes, com foco em avaliação clínica, acompanhamento evolutivo, e prescrição alimentar personalizada.

Nesta Sprint 1 (AC1), foi entregue a base funcional do sistema, com autenticação, estrutura de banco, registro de anamnese e geração de cálculo nutricional.

---

## 🧑‍💻 Equipe

- André Castro – RM: 123456
- [Adicione outros integrantes do grupo, se houver]

---

## 📌 Entregas desta Sprint (AC1)

- ✅ Estrutura de projeto ASP.NET Core Web API (.NET 8)
- ✅ Banco de dados PostgreSQL com Docker Compose
- ✅ Autenticação JWT com roles (Nutricionista e Paciente)
- ✅ Cadastro e login de usuários
- ✅ CRUD de **Anamnese clínica**
- ✅ Cálculo automático de GET com base em GEB
- ✅ Exportação de plano alimentar em PDF
- ✅ Integração com front-end em React via Axios
- ✅ Documentação com Swagger

---

## 🚀 Tecnologias Utilizadas

- ASP.NET Core Web API (.NET 8)
- PostgreSQL (Docker)
- Entity Framework Core
- JWT Bearer Authentication
- React + Vite (Front-end)
- Axios (requisições)
- PDF generation com iTextSharp ou ReportLab (dependendo do backend)
- Swagger / OpenAPI

---

## 📁 Organização do Projeto

**Backend (PeaceApi)**
PeaceApi/
├── Controllers/
├── DTOs/
├── Models/
├── Services/
├── Data/
├── Helpers/
└── appsettings.json

markdown
Copiar código

**Frontend (PeaceFront)**
PeaceFront/
├── src/
│ ├── pages/
│ ├── services/
│ ├── components/
│ └── App.tsx

yaml
Copiar código

**Banco de Dados**
- Docker Compose configurando PostgreSQL
- Migrations geradas com EF Core

---

## 📽️ Vídeo da Sprint

🔗 [Clique aqui para assistir à apresentação da AC1](https://link-do-video.com)

---

## 🗂️ Quadro de Tarefas (Board)

🔗 [Trello / GitHub Projects - Sprint 1](https://link-do-board.com)

---

## 📌 Funcionalidade Demonstrada em Vídeo

1. Cadastro de nutricionista e paciente
2. Login com JWT


---

## 📚 Próximas Sprints

3. Preenchimento da Anamnese Completa
4. Cálculo de GET com base em Harris-Benedict
5. Exportação de PDF com nome, CRN e plano gerado
6. Consumo da API via front-end (React)
7.  Calculo de Composição Corporal.

---

> **Data da entrega:** 14/09/2025  
> **Disciplina:** Projeto de Software  
> **Professor:** [Nome do Professor]
