# Etapa 2 — Seguridad y Autenticación

**Estado:** ✅ Completada  
**Fecha:** 2026-04-09

---

## Objetivo

Implementar un sistema de inicio de sesión seguro que bloquee el acceso al sistema sin credenciales válidas, usando hashing de contraseñas con BCrypt y validación por roles.

---

## Paquetes NuGet Instalados

| Paquete | Versión | Proyecto |
|---|---|---|
| `BCrypt.Net-Next` | 4.1.0 | Aplicacion |

---

## Arquitectura de Seguridad

### Capa Dominio — Contratos

| Interfaz | Archivo | Métodos |
|---|---|---|
| `IRepositorioUsuario` | `Interfaces/IRepositorioUsuario.cs` | `ObtenerPorNombreUsuarioAsync`, `ExisteAdminAsync`, `InsertarAsync` |
| `IRepositorioRol` | `Interfaces/IRepositorioRol.cs` | `ObtenerTodosAsync`, `ObtenerPorIdAsync` |
| `IServicioAutenticacion` | `Interfaces/IServicioAutenticacion.cs` | `ValidarCredencialesAsync`, `AsegurarUsuarioAdminPorDefectoAsync` |

### Capa Datos — Repositorios (Dapper)

| Clase | Tabla | Operaciones |
|---|---|---|
| `RepositorioUsuario` | `Usuarios` | Consulta por nombre de usuario, Insert |
| `RepositorioRol` | `Roles` | Consulta todos, consulta por Id |

### Capa Aplicacion — Servicio de Autenticación

**Ruta:** `CasaDeLosNinos.Aplicacion/Servicios/ServicioAutenticacion.cs`

```
ValidarCredencialesAsync(usuario, contrasena)
  └─ ObtenerPorNombreUsuarioAsync(usuario)
       └─ BCrypt.Net.BCrypt.Verify(contrasena, hash) → bool
            └─ retorna Usuario o null
```

---

## Semilla del Administrador

Al arrancar la aplicación por primera vez (tabla `Usuarios` vacía), se crea automáticamente:

| Campo | Valor |
|---|---|
| `NombreCompleto` | `Administrador del Sistema` |
| `NombreUsuario` | `admin` |
| `ContrasenaHash` | Hash BCrypt de `admin123` (salt factor 11) |
| `IdRol` | `1` (Administrador) |

> ⚠️ **Se recomienda cambiar la contraseña tras el primer ingreso.**

---

## Formulario de Login — `FrmLogin.cs`

**Ruta:** `CasaDeLosNinos.Interfaz/Formularios/FrmLogin.cs`

- UI construida íntegramente por código (sin `.Designer.cs`).
- Recibe `IServicioAutenticacion` por inyección de dependencias.
- Expone propiedad `UsuarioAutenticado` (`Usuario?`) al `Program.cs`.
- El botón "Ingresar" está deshabilitado durante la validación para evitar doble submit.
- La tecla `Enter` activa el botón (AcceptButton).

---

## Flujo de Arranque Actualizado (`Program.cs`)

```
1. Registrar manejadores de excepciones
2. Cargar appsettings.json
3. Construir ServiceCollection (DI)
   ├── IConfiguration (Singleton)
   ├── IInicializadorBaseDatos → InicializadorBaseDatos (Singleton)
   ├── IRepositorioUsuario → RepositorioUsuario (Transient)
   ├── IRepositorioRol → RepositorioRol (Transient)
   └── IServicioAutenticacion → ServicioAutenticacion (Transient)
4. InicializarAsync() → Crea esquema si no existe
5. AsegurarUsuarioAdminPorDefectoAsync() → Semilla de admin
6. FrmLogin.ShowDialog()
   ├── DialogResult.OK → Leer UsuarioAutenticado → Application.Run(FormPrincipal)
   └── DialogResult.Cancel → Terminar aplicación
```

---

## Resultado de Verificación

- `dotnet build` → ✅ 0 errores, 0 warnings.
- Hash de BCrypt almacenado en BD: `$2a$11$ZJE0fEcnrybCB7...` (texto plano nunca visible).
- Intento con credenciales inválidas → Mensaje de error visual, sin crash.
- Intento con `admin`/`admin123` → Acceso al `FormPrincipal` exitoso.
- Cierre del login sin credenciales → Aplicación termina limpiamente.
