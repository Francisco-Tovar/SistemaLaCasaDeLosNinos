# Documentación Técnica — Sistema La Casa de los Niños

Este directorio contiene la documentación técnica de cada etapa de desarrollo del sistema.

---

## Índice de Etapas

| Etapa | Nombre | Estado | Documento |
|---|---|---|---|
| 0 | Cimentación Transversal | ✅ Completada | [etapa0_cimentacion_transversal.md](etapa0_cimentacion_transversal.md) |
| 1 | Persistencia y Versionamiento | ✅ Completada | [etapa1_persistencia_versionamiento.md](etapa1_persistencia_versionamiento.md) |
| 2 | Seguridad y Autenticación | ✅ Completada | [etapa2_seguridad_autenticacion.md](etapa2_seguridad_autenticacion.md) |
| 3 | Beneficiarios y Asistencia | ✅ Completada | [etapa3_gestion_beneficiarios_asistencia.md](etapa3_gestion_beneficiarios_asistencia.md) |
| 4 | Seguimiento Cualitativo | ✅ Completada | [etapa4_seguimiento_cualitativo.md](etapa4_seguimiento_cualitativo.md) |
| 5 | Gestión de Voluntarios | ⏳ Pendiente | — |
| 6 | Control Financiero (Caja Chica) | ⏳ Pendiente | — |

---

## Esquema de Base de Datos

- **Diagrama:** [schema_relacional.png](schema_relacional.png)
- **Descripción:** [schema_base_datos.md](schema_base_datos.md)

---

## Stack Tecnológico

| Componente | Tecnología |
|---|---|
| Framework | .NET 8, C# 12 |
| UI | WinForms |
| Base de Datos | SQLite |
| ORM | Dapper |
| Seguridad | BCrypt.Net-Next |
| DI / Config | Microsoft.Extensions |

---

## Reglas Arquitectónicas Clave

1. **4 Capas estrictas:** `Dominio → Datos → Aplicacion → Interfaz`
2. **Idioma:** Todos los identificadores en español.
3. **Asincronía total:** Toda E/S usa `async/await`. Prohibido `.Result`, `.Wait()`.
4. **Configuración externa:** Sin cadenas de conexión hardcodeadas. Todo en `appsettings.json`.
