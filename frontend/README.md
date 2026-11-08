# PcBuilder Frontend

React SPA for browsing the component catalog, assembling builds with compatibility feedback, and saving AI recommendations.

## Stack

- React 19 + Vite + TypeScript
- Tailwind CSS 4
- React Router 7
- TanStack Query 5

## Prerequisites

- Node.js 20+ (npm)
- The **PcBuilder API** running locally (default `https://localhost:7088`)

For HTTPS against the local API, trust the ASP.NET Core dev certificate once:

```bash
dotnet dev-certs https --trust
```

Alternatively, point the SPA at the HTTP URL (e.g. `http://localhost:5150`) via `VITE_API_BASE_URL` — see `.env.example`.

## Setup

```bash
cd frontend
npm install
```

Copy env example if you need a non-default API URL:

```bash
cp .env.example .env
```

`VITE_API_BASE_URL` defaults to `https://localhost:7088` when unset.

## Run

```bash
npm run dev
```

App: [http://localhost:5173](http://localhost:5173)  
API must already be up (CORS allows `5173` / `5174`).

## Build

```bash
npm run build
```

## Notes

- JWT session is stored in `localStorage` (`pcbuilder.auth`). Expired `expiresAt` values are treated as logged out.
- Builds and AI routes require sign-in.
- AI recommend does **not** persist; use **Save as my build** after reviewing the result.
