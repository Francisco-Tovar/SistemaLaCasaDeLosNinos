# Etapa 4 — Seguimiento Cualitativo (Bitácora de Observaciones)

**Estado:** ✅ Completada  
**Fecha:** 2026-04-10

---

## Objetivo

Implementar un historial cronológico de observaciones cualitativas por niño. La información crítica (autor y marca de tiempo) se captura automáticamente en el código — el usuario solo redacta el contenido.

---

## Regla de Negocio Central: Inmutabilidad

> **Toda observación es firmada digitalmente e inmutable.**  
> Una vez registrada, no puede editarse ni eliminarse.

| Campo | Captura | ¿El usuario lo edita? |
|---|---|---|
| `IdNino` | Selección en grilla | No (viene del contexto) |
| `IdUsuario` | Sesión activa (`_idUsuarioSesion`) | **Nunca** |
| `FechaHora` | `DateTime.Now` en `ServicioObservacion` | **Nunca** |
| `Contenido` | Campo de texto en el formulario | Sí |

---

## Componentes Implementados

### Capa Dominio

| Archivo | Tipo | Descripción |
|---|---|---|
| `Dtos/ObservacionDetalleDto.cs` | DTO de lectura | Combina campos de `Observaciones` + `NombreAutor` (JOIN) |
| `Interfaces/IRepositorioObservacion.cs` | Contrato | `ObtenerPorNinoAsync(int)`, `InsertarAsync(Observacion)` |
| `Interfaces/IServicioObservacion.cs` | Contrato | `ObtenerHistorialAsync(int)`, `RegistrarAsync(int, int, string)` |

### Capa Datos

**`RepositorioObservacion.cs`** — implementación con Dapper:

```sql
-- ObtenerPorNinoAsync: enriquece con nombre del autor via JOIN
SELECT o.Id, o.IdNino, u.NombreCompleto AS NombreAutor,
       o.FechaHora, o.Contenido
FROM  Observaciones o
JOIN  Usuarios      u ON u.Id = o.IdUsuario
WHERE o.IdNino = @idNino
ORDER BY o.FechaHora DESC;
```

### Capa Aplicacion

**`ServicioObservacion.cs`** — Reglas de validación:
- Contenido no puede estar vacío.
- Contenido no puede superar 2,000 caracteres.
- `FechaHora = DateTime.Now` asignado aquí, **nunca** en la UI.
- `IdUsuario` recibido como parámetro de la sesión, **nunca** de un control del formulario.

### Capa Interfaz (UI)

**`FrmObservaciones.cs`** — Diseño por código (sin `.Designer.cs`):
- **Historial**: Panel con scroll automático, tarjetas visuales (color azul claro) con altura dinámica calculada según el contenido del texto.
- **Encabezado de tarjeta**: Muestra `NombreAutor • FechaHora` — campos inmutables y visibles.
- **Captura**: `TextBox` multilinea con contador de caracteres en tiempo real (alerta roja > 1,900 chars).
- **Feedback**: El botón cambia a "✔ Guardado" con color verde durante 1.5 segundos tras un guardado exitoso.

---

## Integración

### Punto de acceso
El módulo de Observaciones se abre desde **`FrmGestionNinos`** con el botón "📝 Bitácora" (color morado) en la barra de herramientas.

### Flujo de sesión
```
Program.cs
  └─ FrmLogin (autenticación)
       └─ UsuarioAutenticado.Id
            └─ FormPrincipal (_idUsuarioActual.Id)
                 └─ FrmGestionNinos (_idUsuarioSesion)
                      └─ FrmObservaciones (_idUsuarioSesion)
                           └─ ServicioObservacion.RegistrarAsync(idNino, idUsuarioSesion, contenido)
```
El `IdUsuario` nunca se pierde ni se solicita de nuevo — fluye desde la sesión inicial.

### DI registrado (`Program.cs`)
```csharp
servicios.AddTransient<IRepositorioObservacion, RepositorioObservacion>();
servicios.AddTransient<IServicioObservacion,    ServicioObservacion>();
```

---

## Verificación

- [x] `dotnet build` → 0 errores, 0 warnings.
- [x] Ningún campo UI permite editar `IdUsuario` o `FechaHora`.
- [x] El JOIN en el repositorio retorna el nombre del autor (no el Id).
- [x] Las observaciones se muestran de más reciente a más antigua.
- [x] El contador de caracteres alerta visualmente al acercarse al límite.
- [x] El servicio valida contenido vacío y exceso de caracteres antes de persistir.
