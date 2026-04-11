# Structure - Sistema La Casa de los Niños

## File Hierarchy

```text
SistemaLaCasaDeLosNinos/
├── .planning/                  # Project planning and codebase mapping
├── CasaDeLosNinos.Dominio/      # Domain Layer
│   ├── Entidades/              # Domain models (e.g., Niño, Asistencia)
│   ├── Interfaces/             # Contracts for Regositories/Services
│   └── Dtos/                   # Shared Data Transfer Objects
├── CasaDeLosNinos.Aplicacion/   # Application Layer
│   ├── Servicios/              # Business logic implementations
│   └── Dtos/                   # Application-specific DTOs
├── CasaDeLosNinos.Datos/        # Data Layer
│   └── Repositorios/           # Dapper implementations
├── CasaDeLosNinos.Interfaz/     # UI Layer (WinForms)
│   ├── Formularios/            # WinForms (.cs, .Designer.cs, .resx)
│   ├── Estilos/                # ThemeEngine and UI logic
│   ├── Assets/                 # Fonts, Images, Icons
│   └── Properties/             # App resources
├── casaninos.db                # SQLite database
├── design.md                   # Documented UI/UX guidelines
└── README.md                   # Project intro
```

## Project Naming
Standardized format: `CasaDeLosNinos.[LayerName]`

## UI Organization
Forms are categorized into:
- **Base Forms**: Common logic for custom title bars.
- **Management Forms (Gestión)**: CRUD lists.
- **Edition Forms (Edición)**: Modal or detailed views.
- **Main Shell**: `FormPrincipal.cs`.
