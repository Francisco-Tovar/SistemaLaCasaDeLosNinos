# Requisitos: Módulo de Beneficiarios (Niños)

## 1. Alcance
Completar la funcionalidad de gestión de niños beneficiarios, asegurando que la entrada de datos sea íntegra, consistente y amigable para el personal administrativo.

## 2. Requisitos Funcionales
- **CRUD Completo**: Crear, leer, actualizar y "eliminar" (borrado lógico) registros de niños.
- **Validaciones Robustas**:
    - **Teléfono**: Formato costarricense (8 dígitos, e.g., 0000-0000) mediante `MaskedTextBox`.
    - **Cédula/Identificación**: Validación de formato y campos obligatorios.
    - **Fechas**: Validación de fecha de nacimiento (no puede ser futura, rangos de edad lógicos).
    - **Campos Obligatorios**: Nombre, primer apellido, contacto de emergencia.
- **Búsqueda y Filtrado**: Localización rápida de niños por nombre o identificación.
- **Persistencia Asíncrona**: Todas las operaciones de guardado deben ser no-bloqueantes.

## 3. Requisitos No Funcionales
- **Feedback Visual**: Notificaciones claras al usuario sobre éxito o error (MessageBox estilizados o etiquetas de estado).
- **Consistencia UI**: Aplicación estricta del tema actual (Oscuro/Claro) en todos los controles de edición.
- **Usabilidad**: Soporte total para navegación por teclado (Tab order correcto).

## 4. Criterios de Aceptación (UAT)
- [ ] Intentar guardar un teléfono inválido debe disparar una alerta visual.
- [ ] El `MaskedTextBox` de teléfono debe forzar el formato `####-####`.
- [ ] Al guardar un niño nuevo, este debe aparecer inmediatamente en la grilla de gestión.
- [ ] El borrado lógico debe marcar al niño como inactivo en la DB sin eliminar el registro físico.
