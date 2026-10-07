# 🖥️ PcBuilder

A backend API for building and managing PC configurations. PcBuilder lets you browse a seeded component catalog, assemble builds with real compatibility validation, and get AI-assisted part recommendations from a natural-language prompt.

Built with **ASP.NET Core Minimal APIs**, **Entity Framework Core**, and **PostgreSQL**, with JWT authentication, in-memory catalog caching, live currency conversion, and OpenAPI docs via Scalar. Focused on solid domain logic and a clean API surface over a UI.

---

## 📦 Technologies

- `ASP.NET Core` (Minimal APIs, .NET 10)
- `C#`
- `Entity Framework Core`
- `PostgreSQL` (`Npgsql`)
- `ASP.NET Identity` + `JWT Bearer`
- `Scalar` / `OpenAPI`
- `Google Gemini` (AI build recommendations)
- `Frankfurter API` (currency exchange)
- `xUnit` + `NSubstitute`
- `FluentAssertions`
- `Testcontainers` (PostgreSQL integration tests)

---

## 🦄 Features

Here's what you can do with PcBuilder:

- **Browse the Component Catalog:** Full CRUD for CPUs, GPUs, motherboards, RAM, PSUs, cases, CPU coolers, hard drives, monitors, and brands.
- **Assemble PC Builds:** Create, update, and delete personal builds; add or remove individual components by type.
- **Compatibility Validation:** Socket, chipset, memory type/speed, cooler TDP, case form factor/clearance, and PSU wattage checks run before a build is saved or updated. Issues are returned with `Error` or `Warning` severity.
- **AI Build Recommendations:** Send a natural-language prompt to `/builds/ai/recommend`. Gemini extracts budget, purpose, resolution, brand preferences, and more; the service picks parts from the catalog using purpose-based budget allocation (`gaming`, `office`, or `default`) and runs compatibility checks on the result.
- **Multi-Currency Budgets:** AI recommendations can use non-USD budgets; rates are fetched from Frankfurter and cached.
- **Authentication & Roles:** Register and log in with Identity; protected build/AI endpoints require a JWT. Users can be promoted to Admin.
- **Seeded Catalog:** On startup (outside integration tests), brands and component data are seeded so the API is usable immediately.
- **Catalog Caching:** Component lists are cached in memory with sliding/absolute expiration; writes invalidate the relevant cache entries.
- **API Documentation:** In Development, OpenAPI is exposed and Scalar UI launches at `/scalar/v1`.

---

## 🗂️ Project Structure

```
PcBuilder/
├── PcBuilder/                    # Main API project
│   ├── Endpoints/                # Minimal API route groups
│   ├── Services/                 # Business logic & external providers
│   ├── Repositories/             # Data access (+ cached decorators)
│   ├── Entities/                 # EF Core entities
│   ├── Models/                   # Request/response DTOs
│   ├── Data/Seeding/             # Catalog & identity seeders
│   ├── Exceptions/               # Exception handlers
│   └── Extensions/               # DI, auth, cache, seeding setup
├── PcBuilder.Tests/              # Unit tests
└── PcBuilder.IntegrationTests/   # API tests with Testcontainers
```

---

## 🚀 Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [PostgreSQL](https://www.postgresql.org/download/) running locally
- A [Google AI / Gemini API key](https://ai.google.dev/) (required for AI recommendations)

### Configuration

Update `PcBuilder/appsettings.json` (or user secrets) with:

| Setting | Purpose |
|--------|---------|
| `ConnectionStrings:DefaultConnection` | PostgreSQL connection string |
| `Jwt:Key`, `Jwt:Issuer`, `Jwt:Audience` | JWT signing and validation |
| `Gemini:ApiKey` | Gemini API key for AI builds |
| `Gemini:Model` | Model name (e.g. `gemini-3.5-flash`) |
| `ExchangeRatesApi:*` | Frankfurter base URL, currency, timeout |
| `ComponentCatalogCache:*` / `ExchangeRatesCache:*` | Cache lifetimes |

> Prefer **user secrets** for `Jwt:Key` and `Gemini:ApiKey` instead of committing real values.

Example user-secrets setup:

```bash
cd PcBuilder
dotnet user-secrets set "Gemini:ApiKey" "YOUR_GEMINI_API_KEY"
dotnet user-secrets set "Jwt:Key" "YOUR_LONG_SECRET_KEY"
```

### Run the API

```bash
cd PcBuilder
dotnet restore
dotnet run
```

By default the HTTPS profile serves at `https://localhost:7088` and opens Scalar at `/scalar/v1`. HTTP-only: `http://localhost:5150`.

---

## 🔌 API Overview

### Auth

| Method | Route | Auth | Description |
|--------|-------|------|-------------|
| `POST` | `/auth/register` | No | Create an account |
| `POST` | `/auth/login` | No | Login; returns JWT |
| `POST` | `/admin/{userId}/make-admin` | Yes | Promote user to Admin |

### Component catalogs

Each resource supports list, get-by-id, create, update, and delete:

| Resource | Base route |
|----------|------------|
| CPUs | `/cpus` |
| GPUs | `/gpus` |
| Motherboards | `/motherboards` |
| RAM | `/rams` |
| PSUs | `/psus` |
| Cases | `/pc-cases` |
| CPU coolers | `/cpu-coolers` |
| Hard drives | `/hard-drives` |
| Monitors | `/pc-monitors` |
| Brands | `/brands` |

### Builds (JWT required)

| Method | Route | Description |
|--------|-------|-------------|
| `GET` | `/builds` | List current user's builds |
| `GET` | `/builds/{id}` | Get a build by id |
| `POST` | `/builds` | Create a build (compatibility checked) |
| `PUT` | `/builds/{id}` | Update a build (compatibility checked) |
| `DELETE` | `/builds/{id}` | Delete a build |
| `POST` | `/builds/{id}/components` | Set/replace one component |
| `DELETE` | `/builds/{id}/components/{componentType}` | Remove a component |
| `POST` | `/builds/ai/recommend` | AI-assisted build from a prompt |

#### AI recommend body

```json
{
  "prompt": "Gaming PC for 1440p, around 1500 EUR, prefer NVIDIA, no RGB case",
  "name": "My Rig",
  "acceptAiSuggestedName": false
}
```

The prompt is parsed into structured requirements (budget, currency, purpose, resolution, brand preferences, monitor need, etc.), then parts are selected and validated.

---

## 🧪 Testing

```bash
# Unit tests
dotnet test PcBuilder.Tests/PcBuilder.UnitTests.csproj

# Integration tests (spins up PostgreSQL via Testcontainers)
dotnet test PcBuilder.IntegrationTests/PcBuilder.IntegrationTests.csproj
```

Unit tests cover services such as compatibility checking (mocked dependencies). Integration tests hit the real API pipeline with authenticated/unauthorized handlers and a disposable Postgres container.

---

## 🧩 Compatibility Checks

When creating or updating a build, the API validates pairs including:

- **CPU ↔ Motherboard** — socket, chipset, memory type; memory speed as warning when mismatched
- **CPU ↔ Cooler** — supported sockets; TDP headroom as warning
- **CPU ↔ RAM** / **RAM ↔ Motherboard** — memory type and speed rules
- **Case ↔ Motherboard / Cooler / GPU / PSU** — form factor, clearance, length, PSU size
- **PSU ↔ GPU** — wattage adequacy

Failed checks return `400` with a message and an `Issues` list instead of saving an incompatible configuration.

---

## 📄 License

This project is for learning and portfolio use unless otherwise specified.
