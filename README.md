# Elsa Onboarding Playground

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
````

### 2. Run the AppHost via Aspire

This launches the workflow server and onboarding app.

```bash
dotnet run --project src/Infrastructure/Elsa.Onboarding.AppHost
```

> Note: Aspire cannot currently host Elsa Studio (WASM). You’ll need to run it separately.

### 3. Run Elsa Studio (Dashboard)

Open a new terminal:

```bash
dotnet run --project src/elsa-studio
```

Once running, visit:
[https://localhost:5003](https://localhost:5003)

---

## 📥 Import a Workflow

1. Open Elsa Studio (see above).

2. Go to **Workflow Definitions**.

3. Click **Import** and upload the following file:

   ```
   workflows/employee-onboarding.json
   ```

4. Publish the workflow after import.

---

## ▶️ Run a Workflow

Copy the `DefinitionId` of the imported workflow (e.g., `3e970eee281541c9`).

Use the following cURL command (or Postman):

```bash
curl --location 'https://localhost:5001/elsa/api/workflow-definitions/3e970eee281541c9/execute' \
--header 'Content-Type: application/json' \
--header 'Authorization: ApiKey 00000000-0000-0000-0000-000000000000' \
--data-raw '{
    "input": {
        "Employee": {
            "Name": "Alice Smith",
            "Email": "alice.smith@acme.com"
        }
    }
}'
```

---

## 📤 Workflow Interaction Model

Once the workflow is started:

* Elsa emits a `RunTask` webhook to the onboarding app at:

  ```
  https://localhost:5002/api/webhooks/run-task
  ```
* The onboarding app stores the task and displays it in the UI (`/`)
* The user clicks “Complete” in the UI, which notifies the Elsa server via API

---

## 🧠 References

* [External App Interaction Guide](https://docs.elsaworkflows.io/guides/external-application-interaction)
* [Elsa Studio GitHub](https://github.com/elsa-workflows/elsa-studio)
* [Aspire Framework Docs](https://learn.microsoft.com/en-us/dotnet/aspire/overview)
