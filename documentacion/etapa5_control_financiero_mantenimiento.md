# Etapa 5 — Control Financiero, Auditoría y Mantenimiento

**Estado:** ✅ Completada  
**Fecha:** 2026-04-20

---

## Objetivo

Implementar el motor de trazabilidad del sistema (Auditoría), la gestión financiera básica (Caja Chica) y las herramientas de administración de base de datos (Mantenimiento).

---

## 1. Módulo de Auditoría (Bitácora Universal)

### Arquitectura de Trazabilidad
Se implementó `IServicioAuditoria` para centralizar el registro de eventos en la tabla `AuditoriaSistema`.

| Operación | Captura Automática |
|---|---|
| **Login/Logout** | Usuario, IP (opcional), Marca de tiempo |
| **CRUD** | Identificador del registro, valores modificados, módulo origen |
| **Errores** | Stack trace y detalle del error capturado globalmente |

### Interfaz `FrmBitacoraSistema`
- **Diseño Solid**: Sin líneas verticales, cabeceras de 45px, filas de 35px.
- **Filtros Avanzados**: Búsqueda por módulo, rango de fechas y texto libre (usuario/detalle).
- **Limpieza Automática**: Función para rotar registros antiguos (90+ días) para mantener el rendimiento.

---

## 2. Caja Chica (Finanzas)

### Reglas de Negocio
- Toda transacción debe estar ligada a un `IdMovimiento` único para auditoría.
- Soporte para **Ingresos** y **Egresos**.
- Cálculo dinámico de saldo mensual basado en el período seleccionado.

### Interfaz `FrmCajaChica`
- Grid optimizado con símbolos de moneda (₡).
- Botones de acción rápida con iconos sincronizados dinámicamente.
- Acceso directo a la auditoría específica del movimiento seleccionado.

---

## 3. Herramientas de Mantenimiento

### `FrmMantenimiento`
Módulo crítico reservado para administradores para la salud de los datos.

- **Backup/Restore**: Implementado mediante comandos nativos de SQLite para generar archivos `.bak`.
- **Cultura y Localización**: Se forzó la cultura `es-CR` para asegurar que los diálogos del sistema (MessageBox) y formatos de fecha sean consistentes.
- **Limpieza de Operaciones**: Scripts para truncar tablas de asistencia y auditoría sin afectar la configuración de maestros (Niños, Voluntarios, Usuarios).

---

## Componentes Técnicos

### Capa Dominio
- `Entidades/AuditoriaSistema.cs`
- `Entidades/CajaChica.cs`
- `Interfaces/IServicioAuditoria.cs`

### Capa Datos (Dapper)
- `RepositorioAuditoria.cs`
- `RepositorioCajaChica.cs`

---

## Verificación de Integridad
- [x] Los backups generan archivos válidos que pueden ser restaurados.
- [x] Cada acción de Caja Chica genera una entrada en `AuditoriaSistema`.
- [x] El motor de temas aplica correctamente el modo oscuro a los grids financieros.
