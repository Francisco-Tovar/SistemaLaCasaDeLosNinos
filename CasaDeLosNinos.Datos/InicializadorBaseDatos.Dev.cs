#if DEBUG
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.Sqlite;

namespace CasaDeLosNinos.Datos
{
    /// <summary>
    /// Extensión de desarrollo para el inicializador de la base de datos.
    /// Datos realistas para el mes de Abril 2026, útiles para probar reportes.
    /// Solo se compila en modo DEBUG.
    /// </summary>
    public partial class InicializadorBaseDatos
    {
        private static async Task PopularDatosDePruebaMasivosAsync(SqliteConnection conexion)
        {
            await conexion.ExecuteAsync("PRAGMA foreign_keys = OFF;");

            using var transaction = conexion.BeginTransaction();
            try
            {
                var rng = new Random(42); // Semilla fija para resultados reproducibles
                var hoy = new DateTime(2026, 4, 21); // Fecha de referencia
                var inicioMes = new DateTime(2026, 4, 1);
                const int adminId = 1;

                // ─── 1. NIÑOS (18 beneficiarios) ───────────────────────────────────────
                // Dato: FechaIngreso variada. 14 activos desde el inicio, 2 que se dan de baja
                // en el mes, 2 que ingresan nuevos en abril.
                var ninos = new[]
                {
                    // Nombre,                           FechaNac,     Genero,       Encargado,                    Tel,         FechaIngreso,  Activo, FechaBaja
                    ("Andres Solano Vega",               "2016-03-12", "Masculino",  "María Vega",                 "88234512",  "2024-01-08",  1,      (string?)null),
                    ("Valentina Mora Jiménez",           "2015-07-20", "Femenino",   "Luisa Jiménez",              "83451267",  "2024-01-08",  1,      null),
                    ("Diego Ramírez Brenes",             "2017-02-05", "Masculino",  "Carlos Ramírez",             "87654321",  "2024-02-15",  1,      null),
                    ("Sofía Castro Alvarado",            "2018-11-30", "Femenino",   "Ana Alvarado",               "84123987",  "2024-02-15",  1,      null),
                    ("Mateo Fernández Rojas",            "2016-09-14", "Masculino",  "Pedro Fernández",            "86789012",  "2024-03-01",  1,      null),
                    ("Isabella Vargas Chávez",           "2017-05-22", "Femenino",   "Rosa Chávez",                "85432109",  "2024-03-01",  1,      null),
                    ("Sebastián Ulate Herrera",          "2015-12-08", "Masculino",  "Jorge Ulate",                "89012345",  "2024-04-10",  1,      null),
                    ("Camila Arce Montoya",              "2018-08-17", "Femenino",   "Sonia Montoya",              "82109876",  "2024-04-10",  1,      null),
                    ("Nicolás Quirós Badilla",           "2019-01-23", "Masculino",  "Fernando Quirós",            "87890123",  "2024-06-05",  1,      null),
                    ("Lucía Méndez Salazar",             "2016-06-11", "Femenino",   "Elena Salazar",              "83210987",  "2024-06-05",  1,      null),
                    ("Santiago Obando Picado",           "2017-10-29", "Masculino",  "Manuel Obando",              "86543210",  "2024-08-20",  1,      null),
                    ("Mariana Pérez Murillo",            "2018-04-03", "Femenino",   "Carmen Murillo",             "89876543",  "2024-08-20",  1,      null),
                    // Se dan de baja en abril 2026
                    ("Kevin Soto Espinoza",              "2015-08-14", "Masculino",  "Rodrigo Espinoza",           "85567890",  "2024-09-10",  0,      "2026-04-07"),
                    ("Daniela Acuña Segura",             "2016-12-19", "Femenino",   "Patricia Segura",            "82678901",  "2024-09-10",  0,      "2026-04-14"),
                    // Ingresan en abril 2026 (Altas del mes)
                    ("Emiliano Flores Ugalde",           "2019-07-05", "Masculino",  "Rebeca Ugalde",              "86901234",  "2026-04-02",  1,      null),
                    ("Valeria Chavarría Núñez",          "2020-02-14", "Femenino",   "Alejandro Núñez",            "83012345",  "2026-04-09",  1,      null),
                    // Fue dado de baja antes y reactivado en abril
                    ("Juan Carlos Mora Ríos",            "2016-05-30", "Masculino",  "Sandra Ríos",                "87123456",  "2025-01-15",  1,      null),
                    ("Paola Herrera Zamora",             "2017-09-08", "Femenino",   "Mario Zamora",               "84234567",  "2025-03-01",  1,      null),
                };

                var ninoIds = new List<int>();
                foreach (var (nombre, fechaNac, genero, enc, tel, fechaIngreso, activo, fechaBaja) in ninos)
                {
                    var id = await conexion.ExecuteScalarAsync<int>(@"
                        INSERT INTO Ninos (NombreCompleto, FechaNacimiento, Genero, Direccion, NombreEncargado, TelefonoEncargado, Activo, FechaIngreso, FechaCreacion, FechaBaja)
                        VALUES (@Nombre, @FechaNac, @Genero, @Dir, @Enc, @Tel, @Activo, @FechaIngreso, @FechaCreacion, @FechaBaja);
                        SELECT last_insert_rowid();",
                        new {
                            Nombre       = nombre,
                            FechaNac     = fechaNac,
                            Genero       = genero,
                            Dir          = "Barrio Los Ángeles, Desamparados",
                            Enc          = enc,
                            Tel          = tel,
                            Activo       = activo,
                            FechaIngreso = fechaIngreso,
                            FechaCreacion = "2024-01-01 08:00:00",
                            FechaBaja    = fechaBaja
                        }, transaction);
                    ninoIds.Add(id);
                }

                // ─── 2. VOLUNTARIOS (7 voluntarios, 1 inactivo) ────────────────────────
                var voluntarios = new[]
                {
                    // Nombre,                  Cédula,           Correo,                     Teléfono,   Especialidad,        Institución,                     Activo, FechaBaja
                    ("María Fernanda Solís",    "1-1523-0874",    "mfsolis@ucr.ac.cr",         "88001234", "Educación Primaria", "Universidad de Costa Rica",   1,      (string?)null),
                    ("Roberto Agüero Vega",     "2-1045-7623",    "raguero@itcr.ac.cr",        "83214567", "Psicología",         "Instituto Tecnológico CR",    1,      null),
                    ("Karina Ulate Bravo",      "1-1687-3412",    "kulate@gmail.com",          "86547890", "Nutrición",          "Independiente",               1,      null),
                    ("Alejandro Mora Soto",     "4-0987-6312",    "amora@ccss.sa.cr",          "89873456", "Medicina General",   "CCSS",                        1,      null),
                    ("Gabriela Pérez Lagos",    "1-1234-8765",    "gperez@ulatina.ac.cr",      "82345678", "Trabajo Social",     "Universidad Latina",          1,      null),
                    // Inactivo desde antes del mes (para probar que aún aparece si tuvo horas)
                    ("Carlos Vindas Núñez",     "6-0567-1234",    "cvindas@hotmail.com",       "85678901", "Educación Física",   "Independiente",               0,      "2026-02-15"),
                    // Activo que participó muy poco en el mes
                    ("Tatiana Campos Mora",     "1-2034-5678",    "tcampos@gmail.com",         "87890123", "Arte y Manualidades","Independiente",               1,      null),
                };

                var volIds = new List<int>();
                foreach (var (nombre, cedula, correo, tel, especialidad, institucion, activo, fechaBaja) in voluntarios)
                {
                    var id = await conexion.ExecuteScalarAsync<int>(@"
                        INSERT INTO Voluntarios (NombreCompleto, Cedula, Correo, Telefono, Especialidad, Institucion, ContactoSupervisor, Activo, FechaIngreso, FechaBaja)
                        VALUES (@Nombre, @Ced, @Correo, @Tel, @Esp, @Inst, @Sup, @Activo, @Ingreso, @FechaBaja);
                        SELECT last_insert_rowid();",
                        new {
                            Nombre    = nombre,
                            Ced       = cedula,
                            Correo    = correo,
                            Tel       = tel,
                            Esp       = especialidad,
                            Inst      = institucion,
                            Sup       = "Dirección CasaNiños",
                            Activo    = activo,
                            Ingreso   = "2024-01-15",
                            FechaBaja = fechaBaja
                        }, transaction);
                    volIds.Add(id);
                }

                // ─── 3. ASISTENCIA — Abril 2026 (Lun-Vie) ──────────────────────────────
                // Los niños activos ANTES del día tienen registro orgánico (85-95% de asistencia).
                // Los dados de baja en el mes asisten hasta su fecha de baja.
                // Los nuevos ingresos asisten desde su fecha de ingreso.

                // Niños activos durante todo el mes (ids 0-11, 16, 17 → ninoIds[0..11,16,17])
                var ninosConstantes = ninoIds.Take(12).Concat(ninoIds.Skip(16).Take(2)).ToList();
                // Niño Kevin Soto (id 12) baja el 07/abril
                var ninoKevinId = ninoIds[12];
                // Niña Daniela Acuña (id 13) baja el 14/abril
                var ninaDanielaId = ninoIds[13];
                // Emiliano ingresa el 02/abril
                var ninoEmilianoId = ninoIds[14]; var inicioEmiliano = new DateTime(2026, 4, 2);
                // Valeria ingresa el 09/abril
                var ninaValeriaId = ninoIds[15]; var inicioValeria = new DateTime(2026, 4, 9);

                // Probabilidades de asistencia por niño (algunos más irregulares)
                var probAsistencia = new Dictionary<int, int>();
                foreach (var id in ninosConstantes) probAsistencia[id] = rng.Next(80, 97);
                probAsistencia[ninoKevinId]    = 88;
                probAsistencia[ninaDanielaId]  = 75; // Daniela ya tenía problemas antes de la baja
                probAsistencia[ninoEmilianoId] = 92;
                probAsistencia[ninaValeriaId]  = 95;

                for (var d = inicioMes; d <= hoy; d = d.AddDays(1))
                {
                    if (d.DayOfWeek == DayOfWeek.Saturday || d.DayOfWeek == DayOfWeek.Sunday) continue;

                    // Niños constantes todo el mes
                    foreach (var ninoId in ninosConstantes)
                    {
                        bool presente = rng.Next(100) < probAsistencia[ninoId];
                        await conexion.ExecuteAsync(@"
                            INSERT OR IGNORE INTO Asistencia (IdNino, Fecha, Presente, IdUsuario)
                            VALUES (@Id, @Fecha, @Presente, @UserId);",
                            new { Id = ninoId, Fecha = d.ToString("yyyy-MM-dd"), Presente = presente ? 1 : 0, UserId = adminId }, transaction);
                    }

                    // Kevin asiste solo hasta el 07/abril
                    if (d <= new DateTime(2026, 4, 7))
                    {
                        bool presente = rng.Next(100) < probAsistencia[ninoKevinId];
                        await conexion.ExecuteAsync(@"
                            INSERT OR IGNORE INTO Asistencia (IdNino, Fecha, Presente, IdUsuario)
                            VALUES (@Id, @Fecha, @Presente, @UserId);",
                            new { Id = ninoKevinId, Fecha = d.ToString("yyyy-MM-dd"), Presente = presente ? 1 : 0, UserId = adminId }, transaction);
                    }

                    // Daniela asiste solo hasta el 14/abril
                    if (d <= new DateTime(2026, 4, 14))
                    {
                        bool presente = rng.Next(100) < probAsistencia[ninaDanielaId];
                        await conexion.ExecuteAsync(@"
                            INSERT OR IGNORE INTO Asistencia (IdNino, Fecha, Presente, IdUsuario)
                            VALUES (@Id, @Fecha, @Presente, @UserId);",
                            new { Id = ninaDanielaId, Fecha = d.ToString("yyyy-MM-dd"), Presente = presente ? 1 : 0, UserId = adminId }, transaction);
                    }

                    // Emiliano asiste desde el 02/abril
                    if (d >= inicioEmiliano)
                    {
                        bool presente = rng.Next(100) < probAsistencia[ninoEmilianoId];
                        await conexion.ExecuteAsync(@"
                            INSERT OR IGNORE INTO Asistencia (IdNino, Fecha, Presente, IdUsuario)
                            VALUES (@Id, @Fecha, @Presente, @UserId);",
                            new { Id = ninoEmilianoId, Fecha = d.ToString("yyyy-MM-dd"), Presente = presente ? 1 : 0, UserId = adminId }, transaction);
                    }

                    // Valeria asiste desde el 09/abril
                    if (d >= inicioValeria)
                    {
                        bool presente = rng.Next(100) < probAsistencia[ninaValeriaId];
                        await conexion.ExecuteAsync(@"
                            INSERT OR IGNORE INTO Asistencia (IdNino, Fecha, Presente, IdUsuario)
                            VALUES (@Id, @Fecha, @Presente, @UserId);",
                            new { Id = ninaValeriaId, Fecha = d.ToString("yyyy-MM-dd"), Presente = presente ? 1 : 0, UserId = adminId }, transaction);
                    }
                }

                // ─── 4. REGISTRO DE HORAS — Voluntarios ────────────────────────────────
                // Cada voluntario tiene días aleatorios (no todos los días), horas de 2-7

                var actividadesEducacion   = new[] { "Clases de lectura y escritura", "Taller de matemáticas básicas", "Apoyo en tareas escolares", "Lectura grupal de cuentos", "Juegos educativos y lúdicos" };
                var actividadesPsicologia  = new[] { "Sesiones de orientación grupal", "Talleres de inteligencia emocional", "Atención individual a niños referidos", "Actividad de resolución de conflictos" };
                var actividadesNutricion   = new[] { "Taller de alimentación saludable", "Supervisión de meriendas", "Charla a encargados sobre nutrición infantil", "Evaluación nutricional individual" };
                var actividadesMedicina    = new[] { "Revisión de peso y talla mensual", "Charla sobre higiene personal", "Primeros auxilios básicos para el personal", "Control de salud preventivo" };
                var actividadesTrabSocial  = new[] { "Visitas domiciliarias a familias", "Gestión de referencias a IMAS", "Taller de habilidades para padres", "Apoyo en proceso de matrícula EBAIS" };
                var actividadesEduFisica   = new[] { "Rutina de ejercicios matutinos", "Juegos recreativos al aire libre", "Actividad de coordinación y motricidad" };
                var actividadesArte        = new[] { "Taller de pintura con témperas", "Manualidades con material reciclado", "Dibujo libre y expresión artística", "Elaboración de tarjetas temáticas" };

                // Voluntario 0: María Fernanda (Educación) - muy activa, ~14 días en el mes
                await InsertarHorasAleatoriasAsync(conexion, transaction, rng, volIds[0], inicioMes, hoy, 14, 3, 6, actividadesEducacion, adminId);
                // Voluntario 1: Roberto (Psicología) - activo, ~10 días
                await InsertarHorasAleatoriasAsync(conexion, transaction, rng, volIds[1], inicioMes, hoy, 10, 2, 5, actividadesPsicologia, adminId);
                // Voluntario 2: Karina (Nutrición) - moderada, ~6 días
                await InsertarHorasAleatoriasAsync(conexion, transaction, rng, volIds[2], inicioMes, hoy, 6, 3, 5, actividadesNutricion, adminId);
                // Voluntario 3: Alejandro (Medicina) - poca frecuencia, ~4 días
                await InsertarHorasAleatoriasAsync(conexion, transaction, rng, volIds[3], inicioMes, hoy, 4, 3, 7, actividadesMedicina, adminId);
                // Voluntario 4: Gabriela (Trabajo Social) - activa, ~8 días
                await InsertarHorasAleatoriasAsync(conexion, transaction, rng, volIds[4], inicioMes, hoy, 8, 4, 7, actividadesTrabSocial, adminId);
                // Voluntario 5: Carlos Vindas (inactivo desde feb) — tuvo 2 días de actividad en abril antes de irse (para probar el reporte)
                await InsertarHorasAleatoriasAsync(conexion, transaction, rng, volIds[5], inicioMes, new DateTime(2026, 2, 14), 2, 3, 5, actividadesEduFisica, adminId);
                // Voluntario 6: Tatiana (Arte) - participación muy esporádica, ~3 días
                await InsertarHorasAleatoriasAsync(conexion, transaction, rng, volIds[6], inicioMes, hoy, 3, 2, 4, actividadesArte, adminId);

                // ─── 5. CAJA CHICA — Gastos e ingresos de abril 2026 ──────────────────
                // Ingresos: transferencias/subvenciones
                var ingresos = new[]
                {
                    (new DateTime(2026, 4, 1),  "Transferencia IMAS - Subvención mensual",          185000m),
                    (new DateTime(2026, 4, 7),  "Donación Iglesia Católica del barrio",              35000m),
                    (new DateTime(2026, 4, 15), "Transferencia IMAS - Subvención quincenal",         185000m),
                    (new DateTime(2026, 4, 18), "Donación Supermercado La Colonia - alimentos",      45000m),
                };

                foreach (var (fecha, concepto, monto) in ingresos)
                {
                    await conexion.ExecuteAsync(@"
                        INSERT INTO CajaChica (Fecha, Concepto, Monto, TipoMovimiento, IdUsuario)
                        VALUES (@Fecha, @Concepto, @Monto, 'Ingreso', @UserId);",
                        new { Fecha = fecha.ToString("yyyy-MM-dd"), Concepto = concepto, Monto = (double)monto, UserId = adminId }, transaction);
                }

                // Egresos: gastos variados realistas en colones
                var egresos = new[]
                {
                    // Semana 1
                    (new DateTime(2026, 4, 1),  "Almuerzo para niños - arroz, frijoles y pollo",    18500m),
                    (new DateTime(2026, 4, 2),  "Merienda: fruta picada y galletas",                 6200m),
                    (new DateTime(2026, 4, 2),  "Detergente líquido y productos de limpieza",       12400m),
                    (new DateTime(2026, 4, 3),  "Almuerzo para niños - sopa de res y pan",          16800m),
                    (new DateTime(2026, 4, 4),  "Merienda: leche y cereal",                          5900m),
                    (new DateTime(2026, 4, 4),  "Papelería: cuadernos, lápices y marcadores",       22300m),
                    (new DateTime(2026, 4, 7),  "Almuerzo para niños - arroz con atún y ensalada",  17200m),
                    (new DateTime(2026, 4, 7),  "Bolsas de basura y guantes desechables",            4800m),
                    // Semana 2
                    (new DateTime(2026, 4, 8),  "Merienda: yogurt y frutas",                         7400m),
                    (new DateTime(2026, 4, 9),  "Almuerzo para niños - picadillo y arroz",           17900m),
                    (new DateTime(2026, 4, 9),  "Recibo de electricidad CNFL - Abril",              38600m),
                    (new DateTime(2026, 4, 10), "Merienda: gelatina y galletas de soda",             5600m),
                    (new DateTime(2026, 4, 10), "Material didáctico: rompecabezas y juegos",         31500m),
                    (new DateTime(2026, 4, 11), "Almuerzo para niños - gallo pinto y huevo",         15200m),
                    (new DateTime(2026, 4, 14), "Almuerzo para niños - spaghetti con salsa",         18900m),
                    (new DateTime(2026, 4, 14), "Recibo de agua AyA - Abril",                        14200m),
                    // Semana 3
                    (new DateTime(2026, 4, 15), "Merienda: pan con natilla y jugo natural",           8100m),
                    (new DateTime(2026, 4, 15), "Gas para cocina (cilindro 25 kg)",                  24500m),
                    (new DateTime(2026, 4, 16), "Almuerzo para niños - pollo asado y puré",          21300m),
                    (new DateTime(2026, 4, 17), "Merienda: manzanas y peras",                         9600m),
                    (new DateTime(2026, 4, 17), "Escobas, lampazos y desinfectante",                 18700m),
                    (new DateTime(2026, 4, 21), "Almuerzo para niños - cazuela de verduras",         16400m),
                    (new DateTime(2026, 4, 21), "Jabón de manos y papel higiénico",                   8900m),
                };

                foreach (var (fecha, concepto, monto) in egresos)
                {
                    await conexion.ExecuteAsync(@"
                        INSERT INTO CajaChica (Fecha, Concepto, Monto, TipoMovimiento, IdUsuario)
                        VALUES (@Fecha, @Concepto, @Monto, 'Egreso', @UserId);",
                        new { Fecha = fecha.ToString("yyyy-MM-dd"), Concepto = concepto, Monto = (double)monto, UserId = adminId }, transaction);
                }

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                await conexion.ExecuteAsync("PRAGMA foreign_keys = ON;");
            }
        }

        /// <summary>
        /// Inserta registros de horas para un voluntario en días hábiles aleatorios del rango.
        /// </summary>
        private static async Task InsertarHorasAleatoriasAsync(
            SqliteConnection conexion,
            System.Data.IDbTransaction transaction,
            Random rng,
            int idVoluntario,
            DateTime inicio,
            DateTime fin,
            int cantidadDias,
            int horasMin,
            int horasMax,
            string[] actividades,
            int adminId)
        {
            // Generar lista de días hábiles disponibles en el rango
            var diasHabiles = new List<DateTime>();
            for (var d = inicio; d <= fin; d = d.AddDays(1))
            {
                if (d.DayOfWeek != DayOfWeek.Saturday && d.DayOfWeek != DayOfWeek.Sunday)
                    diasHabiles.Add(d);
            }

            // Seleccionar aleatoriamente `cantidadDias` días únicos
            var diasSeleccionados = diasHabiles
                .OrderBy(_ => rng.Next())
                .Take(Math.Min(cantidadDias, diasHabiles.Count))
                .OrderBy(d => d)
                .ToList();

            foreach (var dia in diasSeleccionados)
            {
                // Horas con decimales realistas: .0 o .5
                double horas = rng.Next(horasMin * 2, horasMax * 2 + 1) / 2.0;
                string actividad = actividades[rng.Next(actividades.Length)];

                await conexion.ExecuteAsync(@"
                    INSERT OR IGNORE INTO RegistroHoras (IdVoluntario, Fecha, HorasAportadas, Descripcion, IdUsuario)
                    VALUES (@VolId, @Fecha, @Horas, @Desc, @UserId);",
                    new {
                        VolId  = idVoluntario,
                        Fecha  = dia.ToString("yyyy-MM-dd"),
                        Horas  = horas,
                        Desc   = actividad,
                        UserId = adminId
                    }, (System.Data.IDbTransaction)transaction);
            }
        }
    }
}
#endif
