# Testing - Sistema La Casa de los Niños

## Current State
- **Automated Tests**: No unit or integration test projects are currently present in the solution.
- **Manual Verification**: Features are verified manually using the WinForms UI during development.

## Testing Strategy (Proposed)
1. **Unit Tests**: Targets for `Dominio` and `Aplicacion` logic using xUnit or NUnit.
2. **Repository Integration Tests**: Verifying SQLite/Dapper operations.
3. **UI Functional Testing**: Manual UAT (User Acceptance Testing) cycles.

## Verification Checklist
- [ ] Database connectivity.
- [ ] Theme application correctness across all forms.
- [ ] Form resizing and dragging behavior.
- [ ] CRUD operations for primary entities (Niños, Asistencia).
