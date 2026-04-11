# Architecture - Sistema La Casa de los Niños

## Clean Architecture Layers
The project follows a standard Clean Architecture approach, split into four main projects:

### 1. CasaDeLosNinos.Dominio
- **Core Entities**: POCO classes representing the domain model.
- **Interfaces**: Service and repository definitions (contracts).
- **DTOs**: Data Transfer Objects used across layers.
- **No Dependencies**: This layer is independent of other project layers.

### 2. CasaDeLosNinos.Aplicacion
- **Business Logic**: Implementation of services and use cases.
- **Service Layer**: Coordinates domain logic and interacts with the Data layer via interfaces.
- **Dependency**: Depends on `.Dominio`.

### 3. CasaDeLosNinos.Datos
- **Persistence Logic**: Concrete implementations of repositories.
- **Technology**: Dapper and Microsoft.Data.Sqlite.
- **Dependency**: Depends on `.Dominio`.

### 4. CasaDeLosNinos.Interfaz
- **UI Presentation**: WinForms implementation.
- **Theme Engine**: Centralized styling and dynamic theme application.
- **Form Management**: Handles navigation, custom title bars, and modern UI behaviors.
- **Dependency**: Depends on `.Aplicacion` and `.Datos` (for DI configuration).

## Design Patterns
- **Repository Pattern**: Abstracts data access.
- **Dependency Injection**: Centralized service registration in the UI project.
- **Theme/Styling Engine**: Decouples UI logic from specific styling rules.
- **Composite UI**: A main shell form that hosts child forms dynamically.
