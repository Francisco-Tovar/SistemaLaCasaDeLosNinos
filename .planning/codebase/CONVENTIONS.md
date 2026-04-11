# Conventions - Sistema La Casa de los Niños

## Coding Standards
- **Naming**: 
  - Classes/Interfaces: PascalCase (`INiñoRepositorio`).
  - Private fields: camelCase with underscore prefix (`_niñoServicio`).
  - Methods: PascalCase (`ObtenerTodosAsync`).
- **Asynchrony**: Use `Task` and `async/await` for all IO-bound operations (database, files).

## WinForms Specifics
- **Form Design**: 
  - Standardized use of `Designer.cs` (no manual UI code in principal `.cs`).
  - Borderless forms with `WndProc` override for custom resizing/dragging.
- **Theme Hooks**:
  - `panelMenu` / `panelNavegacion` -> Sidebar styling.
  - `panelTitleBar` / `panelCabecera` -> Header styling.
  - `panelDesktop` / `panelContenido` -> Workspace styling.
- **Control Naming**:
  - `btn[Action]` (e.g., `btnGuardar`).
  - `txt[Field]` (e.g., `txtNombre`).
  - `lbl[Desc]` (e.g., `lblTitulo`).

## UI/UX Rules
- **Typography hierarchy**: 12px Bold in Sidebar, Grandstander for Titles.
- **Button Styling**: Flat buttons, no borders, specific highlight colors for success/error.
- **Logo Usage**: Standardized `logo.png` for headers and login.
