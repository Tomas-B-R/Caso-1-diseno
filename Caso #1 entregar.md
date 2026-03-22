# 1\. Frontend Design

## 1.1 Technology Stack

The application leverages a modern, stable stack optimized for Server-Side Rendering (SSR) and enterprise scalability:

| Category | Technology | Version |
| :--- | :--- | :--- |
| **Framework** | React (SSR web app) | 19.2.1 |
| **Language** | TypeScript | 6.0.0-beta |
| **Runtime** | Node.js | 21 |
| **Bundler / Dev Server** | Vite | 8.0.0 |
| **State Management** | Redux Toolkit | 5.0.1 / 2.11.2 |
| **Styling** | Tailwind CSS | 4.1.0 |
| **Validation** | Zod | 4.3.6 |
| **Unit Testing** | Vitest & Jest | 4.0.18 & 30.2.0 |
| **E2E Testing** | Playwright | 1.58.2 |
| **Formatting / Linting**| Prettier & ESLint | 3.8.1 & 10.0.2 |
| **Cloud Service** | Azure App Service | - |
| **CI / CD** | Azure DevOps Pipelines | - |
| **Observability** | Azure Application Insights SDK | - |

## 1.2 UX / UI Analysis

**Usability Attributes:**
The application targets high learnability, efficiency, memorability, low error rate, and high satisfaction.

**UX Test Evidence:**
Moderated usability tests measured task success, time-on-task, error rates, and System Usability Scale (SUS). Results indicated a 100% success rate with an average SUS score of 82, confirming the intuitiveness of the preliminary design.

**Core Business Workflows (Wireframes):**

  * **Login:** The user authenticates with a username, password, and a one-time token; the system establishes an authenticated session or returns recovery options on failure.
  * **Configure Generator:** The user selects the target folder and the DUA template; the system validates parameters and saves the configuration.
  * **Monitoring Progress:** The user views real-time status and logs; the system streams progress updates and accepts control commands (pause/resume/cancel).
  * **Result & Export:** The user previews final outputs and selects an export destination; the system packages and delivers the valid artifact.
  * **Logout:** The user ends the session; the system invalidates authentication and clears the session state.

## 1.3 Component Design Strategy

  * **Methodology:** UI architecture follows Atomic Design principles (atoms, molecules, organisms, templates, and pages) to ensure reusability.
  * **Centralized Styles & Branding:** Styling is managed via Tailwind CSS augmented by CSS variables (design tokens) configured in a central `src/styles/tokens.css` file. Class naming strictly follows the `ComponentName-StyleName` pattern.
  * **Responsiveness:** Layouts are mobile-first and fluid, relying exclusively on `em` positional units for responsive scalability.
  * **Internationalization (i18n):** Managed via `react-i18next` using JSON resource files within `src/i18n/` for dynamic localization.

## 1.4 Security

  * **Authentication:** Handled by Azure Entra ID via Single Sign-On (SSO).
  * **Multi-Factor Authentication (MFA):** Enforced using an authenticator application.
  * **Authorization & Roles:**
      * **Manager:** Granted `MANAGE_USERS` (CRUD operations), `VIEW_REPORTS` (performance analytics), and `EDIT_TEMPLATES` (DUA template management).
      * **Customs Agent:** Granted `LOAD_FILES` (upload documents), `GENERATE_DUA` (initiate AI processing), and `DOWNLOAD_DUA` (retrieve outputs).
  * **Secrets Management:** Environment variables, API keys, and sensitive configuration data are stored in Azure Key Vault.
  * **Module Locations:** Authentication logic resides in `src/services/AuthService.ts`, context in `src/context/AuthContext.tsx`, and route guards in `src/components/PrivateRoute.tsx`.

## 1.5 Layered Design

The architecture enforces strict separation of concerns to handle Server-Side Rendering (SSR) and client interactions securely:

  * **SSR & Authentication Layer:** Node.js server handles initial rendering; unauthenticated requests are intercepted by the Authentication Layer.
  * **Components Layer:** Houses the Atomic Design UI elements.
  * **Hooks Layer:** Connects visual components directly to business logic.
  * **Services Layer:** Contains core business logic and orchestrates operations.
  * **Infrastructure Layers (ApiClients & Settings):** `ApiClients` manages external API communication, while `Settings` reads configurations dynamically from Azure Key Vault.
  * **Shared Layers:** Cross-cutting concerns including Data Validation (Zod), State Management (Redux), Exception Handling, and Observability (Azure Application Insights).

## 1.6 Design Patterns

Key Object-Oriented patterns are explicitly implemented within the project structure:

  * **Builder & Strategy Patterns:** Applied to document processors (Word, Excel, PDF, images) to handle disparate file formats seamlessly.
  * **Adapter Pattern:** Used via `FormatAdapters` to structure concrete output types (Paragraph, Bullets, Table, Label, Amount) for the final DUA document.
  * **Observer Pattern:** Powers the `NotificationService` and `EventBus` (`src/utils/eventBus.ts`) for async API callbacks and cross-component UI refreshes without prop drilling.
  * **Singleton Pattern:** Utilized for shared, stateless services including `ExceptionHandling`, Document Parsers, `StateManagement` (Redux store), `ApiClients`, and `Settings` instances.
  * **Factory Pattern:** Abstract client creation in API modules to easily switch environments or implementations.

## 1.7 Project Scaffold

```plaintext
src/
├── components/       # Reusable React components (atoms→organisms)
│   ├── Button.tsx    # e.g. primary button (atom)
│   ├── Card.tsx      # composite card (molecule)
│   └── ...
├── context/          # React Context providers
│   └── AuthContext.tsx  # provides {user, setUser, login, logout}
├── hooks/            # Custom hooks
│   ├── useAuth.ts    # hook for auth state (wraps context)
│   ├── useNotification.ts  # hook to send UI toasts
│   └── ...
├── pages/            # Page-level components (route targets)
│   ├── LoginPage.tsx
│   ├── DashboardPage.tsx
│   └── ...
├── services/         # API and business services
│   ├── ApiService.ts  # HTTP client wrapper
│   ├── AuthService.ts # login, token refresh, logout
│   └── ...
├── stores/           # State management (Redux clients, global state)
│   └── store.ts      # instantiate Redux store (singleton)
├── i18n/             # Internationalization resources
│   ├── en.json       # English translations
│   └── es.json       # Spanish translations
├── styles/           # Global styles and design tokens
│   └── tokens.css    # CSS variables for colors, spacing, etc.
└── App.tsx           # Application entry (wraps routes with providers)
```

https://github.com/Tomas-B-R/Caso-1-diseno/tree/main/src