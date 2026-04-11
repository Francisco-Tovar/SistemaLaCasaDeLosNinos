# Proyecto: Sistema La Casa de los Niños

## Visión General
Sistema de gestión integral para la organización "La Casa de los Niños", diseñado para automatizar y centralizar el control de beneficiarios (niños), asistencia, voluntarios, finanzas (caja chica) y seguridad.

## Objetivos del Proyecto
- Digitalizar la gestión de beneficiarios con validaciones robustas.
- Facilitar el control de asistencia diario.
- Centralizar la gestión de voluntarios y personal.
- Proporcionar herramientas de auditoría y reportes para personal administrativo.
- Asegurar la integridad de los datos mediante copias de seguridad y exportación.

## Stack Tecnológico
- **Frontend**: WinForms (.NET 8.0) con motor de temas personalizado.
- **Backend**: C# (Clean Architecture: Dominio, Aplicación, Datos).
- **Persistencia**: SQLite con Dapper ORM.
- **UI/UX**: FontAwesome.Sharp, Grandstander & Nunito Sans.

## Usuarios Principal
- **Personal Administrativo**: Usuarios que requieren precisión en los datos, reportes detallados y gestión de procesos sensibles (caja chica, permisos).

## Hitos Clave
1. **Módulo de Beneficiarios (Actual)**: CRUD completo con validaciones avanzadas.
2. **Seguridad y Accesos**: Gestión de usuarios y sistema de permisos.
3. **Módulo de Voluntarios**: Gestión y seguimiento de colaboradores.
4. **Módulo Financiero**: Control de caja chica.
5. **Inteligencia de Negocio**: Reportes avanzados.
6. **Mantenimiento**: Herramientas de exportación y respaldo de DB.
