# Manual de Usuario - Sistema La Casa de los Niños

## 1. Descripción General y Objetivos
El **Sistema La Casa de los Niños** es una herramienta administrativa integral diseñada para modernizar, asegurar y facilitar la gestión diaria de la organización. Su principal meta es reemplazar el papeleo tradicional por un entorno digital rápido y confiable.

El sistema le permitirá, desde un solo programa:
- Mantener expedientes detallados de cada niño (incluyendo fotografías tomadas en el momento y un historial de observaciones).
- Llevar un control preciso y diario de la asistencia.
- Administrar el catálogo de voluntarios que apoyan la institución, junto con un control estricto de a quién responden (sus supervisores y entidades).
- Controlar el acceso al propio sistema mediante cuentas de usuario personalizadas y protegidas.

---

## 2. Requisitos Técnicos e Instalación

### 2.1 Requisitos Mínimos del Equipo
Para garantizar un funcionamiento fluido, la computadora donde se ejecute el sistema debe cumplir con:
- **Sistema Operativo:** Windows 10 (versión 1809 o más reciente) o Windows 11. (No es compatible con sistemas Apple o Linux).
- **Componente Base de Microsoft:** El programa requiere un archivo gratuito llamado **.NET Desktop Runtime 8.0**. Si la computadora no lo tiene, Windows le solicitará descargarlo desde la página oficial de Microsoft la primera vez que intente abrir el sistema.
- **Hardware Integrado:** Una cámara web funcional (integrada en su laptop o conectada por cable USB). Esto es indispensable para registrar fotografías en los expedientes.
- **Almacenamiento:** El sistema guarda todos sus datos (incluyendo fotos y la base de datos de manera interna) en la misma carpeta donde está instalado. No se requiere internet para guardar información.

### 2.2 Instalación del Sistema
1. El equipo técnico le entregará un dispositivo de almacenamiento (USB / llave maya) con el instalador oficial del programa.
2. Conecte el dispositivo a su computadora, abra el archivo instalador y siga los pasos del asistente en pantalla (el proceso estándar de presionar "Siguiente" y luego "Instalar").
3. Una vez finalizado el proceso, el asistente creará automáticamente un acceso directo en su escritorio.
4. Para abrir el programa en su día a día, simplemente haga doble clic en el ícono de **Sistema La Casa de los Niños** que ahora se encuentra en su pantalla principal.
*(Si Windows muestra una pantalla azul advirtiendo que "Windows protegió su PC" durante la instalación, se debe a que es un programa interno y privado de uso institucional. Haga clic en "Más información" y luego en "Ejecutar de todas formas").*

---

## 3. Acceso y Entorno del Sistema

### 3.1 Inicio de Sesión
Al ejecutarse, el sistema le pedirá sus credenciales para proteger la privacidad de los datos infantiles:
- **Usuario y Contraseña:** Ingrese la cuenta que le haya proporcionado la administración.
- **Validaciones:** Si se equivoca en la contraseña, el sistema le notificará el error. La pantalla no le dejará avanzar hasta que ingrese credenciales válidas.
- **Dato Importante:** Existe un usuario "Admin" de emergencia preconfigurado en la instalación que solo debe ser usado por la persona encargada de TI para configurar los primeros empleados.

### 3.2 Interfaz de Usuario e Interacción General
Al entrar, verá una pantalla dividida en dos secciones clave:
- **Menú Lateral Oscuro:** Es su panel de navegación principal. En la parte inferior, este menú incluye un botón con forma de "luna" o "sol" que le permitirá **cambiar los colores** de toda la aplicación entre un Modo Claro (ideal para el día o mucha iluminación) y un Modo Oscuro (ideal para descansar la vista). El sistema recordará su preferencia automáticamente.
- **Área de Trabajo Central:** La zona blanca (o gris oscuro) donde cargan las listas y formularios.
- **Reglas de las Tablas (Grillas):** Todas las listas tienen un formato estándar "Solid" (sin líneas verticales para mejor lectura). **Doble clic** sobre el nombre de una persona en cualquier lista siempre le abrirá la ventana para editar su información. Haga un **solo clic** si desea seleccionarlo para apretar otros botones como "Desactivar".
- **Diseño Premium:** Las ventanas no son redimensionables para garantizar que la interfaz siempre se vea perfecta.

---

## 4. Guía Detallada por Módulo Operativo

### 4.1 Módulo: Gestión de Niños (Expedientes)
Este es el corazón del programa. Le muestra un registro de todos los niños matriculados.

**¿Cómo funciona la pantalla principal de Niños?**
- En la parte superior hay una caja de **Búsqueda**. Al escribir el nombre de un niño, la lista se reducirá de inmediato mostrando solo coincidencias.
- Hay una casilla que dice **"Mostrar Inactivos"**. Si la marca, podrá ver a los niños que ya no asisten al programa (sus nombres aparecen en texto inclinado/cursivo y de color gris).
- Encontrará el botón **"Refrescar"** para actualizar la lista en cualquier momento.

**Agregar o Editar un Expediente:**
1. Al presionar **+ Nuevo** o hacer **doble clic** sobre un niño, entra al formulario de expediente.
2. Se le pedirán datos exigidos: Nombre completo, Fecha de nacimiento, Cédula del menor, Alergias y un nombre y teléfono del Encargado responsable. Si falta alguno de estos datos y presiona "Guardar", el programa le indicará qué casilla está vacía en color rojo.
3. **Manejo de Fotografías:** 
   - A la izquierda de este formulario hay un recuadro. Al hacer clic en el **ícono de la cámara**, se abrirá el asistente fotográfico.
   - Si tiene varias cámaras instaladas, puede abrir la lista desplegable y seleccionar la correcta.
   - Pulse "Iniciar Cámara", posicione al niño, y presione el botón inferior para **"Tomar Foto"**.
   - Presione **"Aceptar y Usar Foto"** y verá cómo la imagen queda guardada en el expediente.
4. **Estado:** En la esquina superior derecha, notará botones para **Activar** o **Desactivar** al niño según su matricula vigente.

**Observaciones Históricas:**
Para no saturar la pantalla principal, cada niño tiene un botón independiente de "Observaciones". Ahí puede escribir notas sobre incidentes, reuniones de padres o comportamiento. Cada nota guardará automáticamente su nombre (como el usuario que la escribió) y la fecha, creando un historial impecable **imposible de borrar o editar**.

### 4.2 Módulo: Control de Asistencia
Diseñado para la eficiencia. Su propósito es marcar quiénes llegaron hoy en segundos.

**Procedimiento de pase de lista:**
1. Arriba a la derecha verá un selector de calendario. Escoja la fecha. **Nota:** El sistema tiene una regla estricta: *no le permitirá seleccionar días en el futuro*, solo el día de hoy o días pasados que quiera corregir.
2. El sistema cargará automáticamente a todos los niños que tengan el estatus **Activo**. Use el **Buscador** si la lista es muy larga.
3. Puede hacer clic en el cuadrito de **"Presente"** uno por uno, o puede usar el botón verde gigante de **"Marcar Todos"** para ahorrar tiempo y luego solo despintar a los que faltaron.
4. Hay una columna llamada **Observación**. Si un niño se enfermó, haga doble clic en ese espacio, escriba "enfermo" o "tarde", y presione Enter.
5. **Paso Final Vital:** Nada de esto se guarda hasta que presione el botón azul de **"Guardar Asistencia"**.

### 4.3 Módulo: Gestión de Voluntarios
Registra a toda persona externa, practicante o pasante que preste sus servicios. 

**Datos Requeridos del Voluntariado:**
- **Información de Control:** El formulario le exigirá escribir la "Especialidad", la "Institución" y el nombre de su "Supervisor".
- **Verificación de Cédula (CR/DIMEX):** El programa tiene validaciones de seguridad. Si elige Documento Nacional (9 dígitos) o DIMEX (12 dígitos), el sistema le obligará a ingresar el formato correcto.
- **Registro de Horas:** Use el botón de "Horas" para contabilizar el tiempo que el voluntario ofrece a la institución.
- **Estatus Histórico:** El sistema guarda automáticamente la fecha de baja cuando se desactiva un voluntario para reportes de fin de año.

### 4.4 Módulo: Gestión de Usuarios y Permisos
Pantalla restringida a la dirección. Permite manejar quién tiene acceso al sistema.

- **Permisos Granulares:** Al agregar o editar un usuario, usted puede marcar individualmente a qué módulos tiene acceso (Niños, Asistencia, Caja Chica, etc.).
- **Control de Acceso:** Use el botón "Desactivar" sobre un usuario que ha dejado la institución.
- **Protección del Sistema:** El Administrador principal (Id: 1) no puede ser desactivado para evitar bloqueos accidentales.

---

## 5. Módulos de Gestión Avanzada

### 5.1 Control Financiero (Caja Chica)
Este módulo habilita una interfaz de contabilidad para la dirección, eliminando los apuntes en cuaderno:
- **Ingresos y Egresos:** Registre donaciones o gastos con un par de clics.
- **Balances:** Calcula saldos en tiempo real según el mes seleccionado.
- **Auditoría de Movimientos:** Cada registro permite ver quién lo creó y qué cambios ha sufrido, garantizando transparencia total.

### 5.2 Central de Reportes y Vista Previa
Permite generar documentos administrativos oficiales:
- **Vista Previa:** Antes de exportar, puede ver los datos en pantalla para asegurarse de que la información es correcta.
- **Exportación:** Genere archivos PDF o Excel de Asistencias, Voluntarios o resúmenes de Altas y Bajas.

### 5.3 Mantenimiento y Auditoría de Sistema
- **Respaldos:** Permite crear copias de seguridad de toda la base de datos para prevenir pérdida de información.
- **Bitácora Universal:** Un registro de CADA acción realizada en el sistema (quién entró, qué borró, qué editó) visible solo para la dirección en el menú de Seguridad.

---
*Fin del Documento - Sistema La Casa de los Niños - Versión 2.0 (Abril 2026)*
