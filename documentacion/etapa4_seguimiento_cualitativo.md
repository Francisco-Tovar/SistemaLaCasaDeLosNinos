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

**`FrmObservaciones.cs`** — Implementación basada en el **Diseñador de WinForms** (`.Designer.cs`):
- **Estructura**: Separación clara entre código lógico (`.cs`) y código de diseño (`.Designer.cs`). Permite el uso del *Visual Studio Toolbox*.
- **InitializeComponent()**: Obligatorio en el constructor para la instanciación de controles.
- **Historial**: Panel con scroll automático, tarjetas visuales dinámicas gestionadas por el código lógico mientras que el contenedor base se definió visualmente.
- **Encabezado de tarjeta**: Muestra `NombreAutor • FechaHora` — campos inmutables y visibles.
- **Captura**: `TextBox` multilinea con contador de caracteres en tiempo real.
- **Feedback**: El botón cambia a "✔ Guardado" con color verde durante 1.5 segundos tras un guardado exitoso.

---

## Refactorización UI: Transición a WinForms Designer

Tras completar la funcionalidad base de la Etapa 4, se realizó una refactorización transversal de la interfaz de usuario para permitir el mantenimiento visual desde Visual Studio.

### Cambios Realizados:
1.  **Migración Completa**: Todos los formularios del sistema (`FrmLogin`, `FormPrincipal`, `FrmGestionNinos`, `FrmEdicionNino`, `FrmTomaAsistencia` y `FrmObservaciones`) fueron convertidos al patrón de archivos parciales (`.Designer.cs`).
2.  **Estandarización**: Se eliminó la construcción manual de interfaces en los constructores, delegando la responsabilidad de instanciación y posicionamiento a `InitializeComponent()`.
3.  **Regla de Arquitectura (tcuskill)**: Se añadió la **Regla 8** al skill del proyecto, prohibiendo estrictamente el diseño programático de formularios de ahora en adelante para garantizar que el usuario pueda utilizar el *Toolbox* de Visual Studio.

### Estado Actual de los Forms:
- ✅ **FrmObservaciones.cs** + `FrmObservaciones.Designer.cs`
- ✅ **FrmLogin.cs** + `FrmLogin.Designer.cs`
- ✅ **FormPrincipal.cs** + `FormPrincipal.Designer.cs`
- ✅ **FrmGestionNinos.cs** + `FrmGestionNinos.Designer.cs`
- ✅ **FrmEdicionNino.cs** + `FrmEdicionNino.Designer.cs`
- ✅ **FrmTomaAsistencia.cs** + `FrmTomaAsistencia.Designer.cs`

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
