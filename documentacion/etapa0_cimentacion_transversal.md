# Etapa 0 — Cimentación Transversal

**Estado:** ✅ Completada  
**Fecha:** 2026-04-09

---

## Objetivo

Establecer la infraestructura base de la solución: estructura de proyectos, lectura de configuración externa y manejo global de excepciones no controladas.

---

## Estructura de la Solución

```
CasaDeLosNinos.slnx
├── CasaDeLosNinos.Dominio/       (Class Library, net8.0)
├── CasaDeLosNinos.Datos/         (Class Library, net8.0)
├── CasaDeLosNinos.Aplicacion/    (Class Library, net8.0)
└── CasaDeLosNinos.Interfaz/      (WinForms, net8.0-windows)
```

### Referencias entre capas

| Proyecto | Depende de |
|---|---|
| `Datos` | `Dominio` |
| `Aplicacion` | `Dominio` |
| `Interfaz` | `Aplicacion`, `Datos` |
| `Dominio` | — (ninguna) |

---

## Paquetes NuGet Instalados

| Paquete | Versión | Proyecto |
|---|---|---|
| `Microsoft.Extensions.DependencyInjection` | 10.0.5 | Interfaz |
| `Microsoft.Extensions.Configuration` | 10.0.5 | Interfaz |
| `Microsoft.Extensions.Configuration.Json` | 10.0.5 | Interfaz |

---

## Archivos Clave Creados

### `appsettings.json` (Interfaz)
Archivo de configuración externo. **Nunca se hardcodean rutas o cadenas de conexión en el código.**

```json
{
  "ConnectionStrings": {
    "BaseDatos": "Data Source=casaninos.db"
  },
  "Configuracion": {
    "NombreOrganizacion": "La Casa de los Niños",
    "VersionSistema": "1.0.0",
    "RutaLogs": "logs/"
  }
}
```

### `Program.cs` (Interfaz)
Punto de entrada de la aplicación. Responsabilidades:

1. **Manejadores globales de excepciones:**
   - `Application.ThreadException` → Captura errores del hilo UI (WinForms).
   - `AppDomain.CurrentDomain.UnhandledException` → Captura errores de hilos secundarios.
   - Ambos escriben en `logs/errores.log` y muestran un `MessageBox` amigable.

2. **Lectura de configuración:**
   - Usa `ConfigurationBuilder` para cargar `appsettings.json` desde `AppContext.BaseDirectory`.
   - Si el archivo no existe, muestra error y termina sin crashear.

3. **Contenedor de Inyección de Dependencias:**
   - `IConfiguration` registrado como Singleton.

---

## Resultado de Verificación

- `dotnet build` → ✅ 0 errores, 0 warnings.
- La aplicación inicia y carga la configuración correctamente.
- Una excepción forzada en tiempo de ejecución generó el archivo `logs/errores.log` con stack trace completo, sin cierre abrupto.
