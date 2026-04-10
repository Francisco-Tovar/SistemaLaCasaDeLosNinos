# Etapa 1 — Persistencia y Versionamiento

**Estado:** ✅ Completada  
**Fecha:** 2026-04-09

---

## Objetivo

Generar físicamente la base de datos SQLite con el esquema relacional completo del dominio y establecer un sistema de versionamiento de esquema para soportar futuras migraciones.

---

## Paquetes NuGet Instalados

| Paquete | Versión | Proyecto |
|---|---|---|
| `Microsoft.Data.Sqlite` | 10.0.5 | Datos |
| `Dapper` | 2.1.72 | Datos |
| `Microsoft.Extensions.Configuration.Abstractions` | 10.0.5 | Datos |

---

## Entidades del Dominio (POCOs planos)

Todas las entidades están en `CasaDeLosNinos.Dominio/Entidades/`. Son clases planas sin atributos ORM ni objetos anidados, compatibles con el mapping automático de Dapper.

| Clase | Tabla SQLite | Notas |
|---|---|---|
| `Nino` | `Ninos` | Borrado lógico con campo `Activo` |
| `Rol` | `Roles` | Valores: Administrador, Funcionario |
| `Usuario` | `Usuarios` | FK plana `IdRol` (int) |
| `Asistencia` | `Asistencia` | Índice único `(IdNino, Fecha)` |
| `Observacion` | `Observaciones` | Timestamp inmutable |
| `Voluntario` | `Voluntarios` | Borrado lógico |
| `RegistroHoras` | `RegistroHoras` | `CHECK(HorasAportadas > 0)` |
| `CajaChica` | `CajaChica` | `CHECK(TipoMovimiento IN ('Ingreso','Egreso'))` |
| `VersionBD` | `VersionBD` | Control de migraciones |

---

## Interfaz de Contrato (Dominio)

```
CasaDeLosNinos.Dominio/Interfaces/IInicializadorBaseDatos.cs
```

```csharp
public interface IInicializadorBaseDatos
{
    Task InicializarAsync();
}
```

---

## Implementación — `InicializadorBaseDatos`

**Ruta:** `CasaDeLosNinos.Datos/InicializadorBaseDatos.cs`

Características:
- Recibe la cadena de conexión vía `IConfiguration.GetConnectionString("BaseDatos")`.
- Ejecuta `PRAGMA foreign_keys = ON` al abrir la conexión.
- Crea las 9 tablas con `CREATE TABLE IF NOT EXISTS` (idempotente).
- Inserta la versión `1.0.0` en `VersionBD` si la tabla está vacía.
- Inserta los roles base (`Administrador`, `Funcionario`) si la tabla está vacía.

---

## Restricciones de Integridad Verificadas

| Restricción | Tabla | Tipo | Estado |
|---|---|---|---|
| FK: `IdRol` inválido | `Usuarios` | FOREIGN KEY | ✅ Rechaza |
| Duplicado `(IdNino, Fecha)` | `Asistencia` | UNIQUE INDEX | ✅ Rechaza |
| `TipoMovimiento` inválido | `CajaChica` | CHECK | ✅ Rechaza |
| `HorasAportadas <= 0` | `RegistroHoras` | CHECK | ✅ Rechaza |
| JOIN 4 tablas: `Asistencia → Ninos → Usuarios → Roles` | — | Relacional | ✅ Consistente |

---

## Resultado de Verificación

- `dotnet build` → ✅ 0 errores, 0 warnings.
- Archivo `casaninos.db` generado físicamente (65,536 bytes).
- `VersionBD` contiene el registro `1.0.0`.
- Todas las pruebas de integridad referencial pasaron.

---

## Diagrama del Esquema

Ver: [`schema_base_datos.md`](schema_base_datos.md) y [`schema_relacional.png`](schema_relacional.png)
