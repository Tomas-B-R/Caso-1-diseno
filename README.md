# Caso-1-diseno

# Frontend design



-   **Application type:**  
    Single Page Application (SPA) — React client with optional server-rendered entrypoints for specific pages (static + serverless mix).
    
-   **Web framework:**  
    **React** `19.2.1` — stable React 19 series with the latest security patch applied.
    
-   **Web server:**  
    Static assets served via CDN/edge (Vercel / Cloud CDN) + **Cloud Run** (or equivalent) for SSR or server components.
    
-   **Coding Language:**  
    **TypeScript** `6.0.0-beta` (use `6.0.0-beta` for cutting-edge repo; fallback: `^5.9.x` if you require GA stability).
    
-   **Build tool / Bundler / Dev server:**  
    **Vite** `8.0.0` (use the latest stable 8.x release / beta where needed for rollup/rolldown improvements).
    
-   **Unit testing framework:**  
    **Vitest** `4.0.18` — fast, Vite-native unit test runner compatible with Jest APIs.
    
-   **Integration / E2E testing tools:**  
    **Playwright** `1.x` (use latest stable, e.g., `>=1.58.x`) for cross-browser E2E, visual regression and CI replay.
    
-   **Data validation framework (client):**  
    **Zod** `^4.0.0` — TypeScript-first runtime schema validation and DTO parsing.
    
-   **HTTP client / API layer:**  
    **Axios** `1.13.6` — centralized axios instance with interceptors for auth/refresh/retry.
    
-   **State management:**  
    **Redux Toolkit** `^2.11.2` (RTK + RTK Query where required for caching & optimistic updates).
    
-   **Styling / Design tokens:**  
    **Tailwind CSS** `4.1.0` + CSS variables tokens file (`src/styles/tokens.ts`) and small token shim for non-Tailwind contexts.
    
-   **Component primitives / accessibility:**  
    **Radix UI** `1.x` or **Headless UI** `1.x` (pick one); use storybook-driven components.
    
-   **Internationalization (i18n):**  
    **i18next** `25.x` + `react-i18next` `16.x` — typed keys and runtime lazy-load locales.
    
-   **Formatting & linting:**
    
    -   **Prettier** `3.6.0` (code formatter).
        
    -   **ESLint** `10.0.1` (linting + `@typescript-eslint` parsers + `eslint-plugin-react`).
        
-   **Code style / conventions:**
    
    -   `eslint-config-airbnb` or internal `@org/eslint-config` wrapper pinned and enforced in CI.
        
    -   `prettier` + `eslint` integration (`eslint-plugin-prettier`) to keep a single source of truth.
        
-   **Code automation task tool (monorepo / task runner):**  
    **Turborepo** `2.8.x` (fast orchestration, remote caching) for multi-package monorepo tasks and caching.
    
-   **Package manager:**  
    **pnpm** `8.x` (strict isolation + performance) — pin lockfile to CI.
    
-   **Code repositories service:**  
    **GitHub** (main repo) — branch protection, CODEOWNERS, PR templates.
    
-   **CI / CD pipelines technology:**  
    **GitHub Actions** (primary), with optional **Cloud Build** / **Google Cloud Build** or **Vercel** previews for branch deployments.
    
-   **Environments:**
    
    -   `development` (local, feature branches)
        
    -   `staging` (integration, QA)
        
    -   `production` (live)  
        Each environment has separate API base URLs, feature-flag sets, and secrets.
        
-   **Environment deployments tools:**
    
    -   **Terraform** `1.14.6` for infra IaC (VPC, Cloud Run, IAM, buckets).
        
    -   **Turborepo / pnpm** for build orchestration and artifact production.
        
-   **Cloud service (primary):**  
    **Google Cloud Platform (GCP)** — using Cloud Run for server containers, Cloud Storage for assets, Secret Manager for secrets, Cloud SQL (or managed DB) for relational needs.
    
-   **Hosted services within the cloud service:**
    
    -   **Cloud Run** (containers)
        
    -   **Cloud Storage** (static assets)
        
    -   **Secret Manager** (secrets)
        
    -   **Cloud CDN / Cloud Load Balancing** (edge delivery)
        
    -   **Cloud Monitoring / Trace** for infra telemetry
        
-   **CI/CD deployments (hosted preview / static hosting):**
    
    -   **Vercel** for static and hybrid SSR preview environments (branch deploys).
        
    -   **Cloud Run + Terraform** for production container deployments.
        
-   **Observability (frontend):**
    
    -   **OpenTelemetry (JS)** `2.6.0` for traces/metrics + exporter to chosen backend (e.g., Grafana Cloud / Cloud Trace).
        
    -   **Sentry** (error + performance monitoring) for exception capture & session replay.
