# elsa-onboarding-playground

This repository demonstrates how to build an employee‑onboarding workflow using [Elsa Workflows 3](https://elsaworkflows.io) and .NET Aspire.  
It is based on the official [External Application Interaction](https://docs.elsaworkflows.io/guides/external-application-interaction) guide from Elsa.

In this pattern:

- A **workflow server** orchestrates workflows and emits tasks.
- A **separate application** receives and executes those tasks via webhooks.

Elsa Studio (the dashboard) is included, but due to [current limitations](https://github.com/elsa-workflows/elsa-core/issues/8287) in .NET Aspire's handling of WebAssembly apps, it must be run manually.

---

## 🚀 Features

- Elsa Workflows 3 with Webhook-based task coordination
- .NET Aspire support for service orchestration and monitoring
- Example onboarding workflow JSON
- Manual task completion interface
- Simple authentication via API key

---

## 🧾 Project Structure

| Project                             | Description                                      |
|-------------------------------------|--------------------------------------------------|
| `elsa-server`                       | Workflow server with Elsa, API, DB, webhook sink |
| `onboarding`                        | External app receiving tasks & reporting progress |
| `elsa-studio`                       | Elsa Dashboard (must be started manually)        |
| `Infrastructure/Elsa.Onboarding.*` | Shared hosting, Aspire configuration             |
| `workflows/`                        | Workflow definition files (e.g., onboarding)     |

---

## ⚙️ Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- Node.js (for running Elsa Studio, if building from source)
- Docker (for PostgreSQL if using containerized DB)
- Optional: [Aspire Dashboard](https://learn.microsoft.com/en-us/dotnet/aspire/overview)

---

## 🛠️ Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/arttonoyan/elsa-onboarding-playground.git
cd elsa-onboarding-playground
