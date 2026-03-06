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



1.2 UX UI analysis
Core business process

Below are concise, step-by-step descriptions of what happens on each screen. Each step is written as a user action followed by the immediate result — no UI components are mentioned.

Login

User enters their username, password and one-time token. → System verifies credentials and token integrity.

If verification fails. → System returns an invalid credentials message and prompts the user to retry or recover credentials.

If verification succeeds. → System establishes an authenticated session and transitions the user into the application workspace.

If the session cannot be created (server error or network issue). → System surfaces an error explaining the failure and suggests retrying later.

Note: authentication uses the Microsoft authentication flow (external identity provider).

Configure the generator

User chooses the target folder where generated outputs will be stored. → System checks access rights and confirms write permissions.

User selects the generation template (DUA template) and supplies any required parameters or metadata. → System validates the template selection and parameter values against template rules.

User reviews validation feedback. → If validation fails, system returns specific errors and guidance for correction; if validation succeeds, system saves the configuration.

User initiates the generation process. → System enqueues the job and returns a job identifier and initial status.

Monitoring progress

User opens the job details for the active generation. → System displays the current job status, completed stages and timestamps.

User requests more detailed logs or execution trace for the job. → System streams or returns the latest log entries and any warnings or errors produced so far.

User issues a control action on the job (pause, resume, cancel). → System applies the requested control, updates job status, and confirms the new state or returns an error if the control cannot be applied.

System detects an error during processing. → System records the error, marks the job as failed (or paused for manual intervention) and surfaces actionable diagnostic information to the user.

System completes a stage of multi-stage generation. → System updates progress indicators and, if configured, notifies the user of milestone completion.

Obtain result / export

When generation finishes, user opens the result summary. → System verifies result integrity and presents a preview or summary of the outputs and their metadata.

User selects an export format or delivery option (file download, archive, or external storage). → System packages the result in the chosen format and validates the package.

User initiates export. → System prepares the file(s), creates a downloadable artifact or delivers to the selected destination, and provides a confirmation (with file size, checksum and timestamp).

If export fails (format error, destination unreachable). → System returns a clear error and suggestions (retry, change format, check destination access).

User optionally requests a shareable link or audit report. → System generates the link/report, applies access controls, and presents delivery options.

Logout

User ends the session. → System invalidates session tokens, clears session state server-side, and confirms logout.

After logout completes. → System prevents access to authenticated resources until re-authentication and may present a final message confirming successful sign-out.

Wireframes

Below are the recommended wireframe entries to paste into the README. For each screen provide a short title, a one-sentence description of the screen’s purpose (what the user does and what the system returns), then embed the generated image under the description.

Login screen

Title: Login
Description: The user authenticates with username, password and one-time token; on success the system establishes an authenticated session and moves the user into the workspace; on failure the system returns a clear error and recovery options.
Image (embed here): ![Login screen](./images/login.png)

Generator configuration

Title: Configure Generator
Description: The user selects the target folder and the DUA template, supplies parameters and validates them; the system confirms validity or returns specific corrections and saves the configuration when valid.
Image (embed here): ![Configure generator](./images/configure_generator.png)

Monitoring / Progress

Title: Monitoring Progress
Description: The user opens job details to view real-time status, logs and stage completion; the system streams progress updates, exposes diagnostics, and accepts control commands (pause/resume/cancel).
Image (embed here): ![Monitoring progress](./images/monitoring_progress.png)

Result / Export

Title: Result & Export
Description: The user previews final outputs, selects an export format or destination and initiates export; the system packages the results, validates the artifact and delivers a downloadable file or transfer confirmation.
Image (embed here): ![Result export](./images/result_export.png)

Logout / Session end

Title: Logout
Description: The user ends the session; the system invalidates authentication, clears session state and confirms sign-out.
Image (embed here): ![Logout](./images/logout.png)
    
    -   **OpenTelemetry (JS)** `2.6.0` for traces/metrics + exporter to chosen backend (e.g., Grafana Cloud / Cloud Trace).
        
    -   **Sentry** (error + performance monitoring) for exception capture & session replay.
