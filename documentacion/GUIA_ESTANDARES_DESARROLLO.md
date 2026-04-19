# Guía de Diseño Premium y Estándares de Implementación

Esta guía consolida las reglas de oro para mantener la calidad visual y funcional del Sistema La Casa De Los Niños. Estas reglas deben ser seguidas estrictamente por cualquier asistente de IA para evitar regresiones de UI.

## 1. Implementación de Grillas (DataGridView)

Para mantener el estándar "Premium", las grillas no deben ser dejadas al azar del Diseñador de Visual Studio.

### Reglas Técnicas:
- **Inicialización Obligatoria**: Si el grid tiene `AutoGenerateColumns = false`, **DEBES** crear un método `ConfigurarGrilla()` en el code-behind y llamarlo en el constructor.
- **Bloqueo de Redimensionado**: En reportes de Auditoría o Fiscalización, el usuario **NO** debe poder alterar el layout.
  ```csharp
  grd.AllowUserToResizeColumns = false;
  grd.AllowUserToResizeRows = false;
  grd.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
  ```
- **Dimensiones Estándar**:
  - `ColumnHeadersHeight`: 45px.
  - `RowTemplate.Height`: 35px (estándar) o 45px (si hay mucho texto/multilínea).
- **Ajuste de Texto**: Para descripciones largas (como logs de auditoría), activar `DefaultCellStyle.WrapMode = DataGridViewTriState.True`.

## 2. Layout y Orden de Apilado (Z-Order)

El sistema utiliza un sistema de capas basado en `FormBase`. El orden de los controles en la colección `Controls` es crítico para el docking.

### Reglas de Estilizado Dinámico:
Para apilar componentes `Dock.Top` correctamente arriba del `Dock.Fill`:
1. `panelCabecera.SendToBack();` (Queda arriba del todo).
2. `panelInformacion.BringToFront();` (Queda debajo de la cabecera).
3. `grdDatos.BringToFront();` (Ocupa todo el resto del formulario).

## 3. Semántica Visual y Formatos

- **Moneda**: Utilizar siempre el símbolo de Colón Costarricense "**₡**" con dos decimales (`"N2"`).
- **Fechas**:
  - Grillas: `"dd/MMM/yyyy"` o `"dd/MM HH:mm"` para logs rápidos.
  - Etiquetas: `"FISCALIZACIÓN DEL PERÍODO: MM/yyyy"`.
- **Botones de Acción**:
  - **Ingreso**: Fondo `StatusSuccess` (Verde esmeralda).
  - **Egreso**: Fondo `Color.IndianRed` (Rojo suave), ignorando temas globales para máxima visibilidad de alerta.
- **Tipografía**: Segoe UI (estándar moderno de Windows).

## 4. Auditoría y Trazabilidad

Todo cambio financiero DEBE generar un registro en `AuditoriaCajaChica`.
- **Diferenciales (Deltas)**: Al editar un movimiento, se debe generar un string de diferencias legible: `Monto: ₡100 -> ₡150`.
- **DTOs**: Para reportes mensuales, utilizar `AuditoriaDetalleDTO` para vincular nombres de usuario y conceptos originales.

---
*Nota: Estas reglas se consideran "Leyes del Proyecto". Saltárselas se considera un fallo en la implementación.*
