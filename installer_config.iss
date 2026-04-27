; ----------------------------------------------------------------------------------
; SCRIPT DE INSTALACIÓN - SISTEMA LA CASA DE LOS NIÑOS
; ----------------------------------------------------------------------------------

#define MyAppName "Sistema La Casa de los Niños"
#define MyAppVersion "1.2.1"
#define MyAppPublisher "TCU - CENFOTEC"
#define MyAppExeName "CasaDeLosNinos.Interfaz.exe"

[Setup]
AppId={{C62B0E1F-7A9B-4D1D-A1C8-E9B0F3A2D1C1}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
DefaultDirName={autopf}\{#MyAppName}
DisableProgramGroupPage=yes
; El archivo comprimido final se guardará en la carpeta 'Output'
OutputDir=Output
OutputBaseFilename=Instalador_CasaDeLosNinos_v1.2.1
Compression=lzma
SolidCompression=yes
WizardStyle=modern
PrivilegesRequired=admin
SetupIconFile=app_icon.ico
UninstallDisplayIcon={app}\{#MyAppExeName}

[Languages]
Name: "spanish"; MessagesFile: "compiler:Languages\Spanish.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
; Copiar TODO el contenido de la carpeta publish (esencial para aplicaciones self-contained)
; Esto incluye el .exe, DLLs del sistema, archivos .json de runtime y subcarpetas de idiomas
Source: "publish\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs; Excludes: "*.pdb"

; Manual de usuario (fuera de la carpeta publish)
Source: "documentacion\Manuales\ManualUsuario_CasaDeLosNinos.pdf"; DestDir: "{app}"; Flags: ignoreversion

; Fuentes Críticas (Grandstander)
Source: "CasaDeLosNinos.Interfaz\Assets\Fonts\Grandstander\Grandstander-VariableFont_wght.ttf"; \
    DestDir: "{fonts}"; FontInstall: "Grandstander"; Flags: onlyifdoesntexist uninsneveruninstall

; Carpeta de Base de Datos (Se crea vacía para recibir la BD limpia)
Source: "database\*"; DestDir: "{app}\database"; Flags: ignoreversion recursesubdirs createallsubdirs

[Icons]
Name: "{autoprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

[Dirs]
Name: "{app}\logs"; Permissions: everyone-modify
Name: "{app}\database"; Permissions: everyone-modify

[Code]
// Lógica adicional si se requiere verificar pre-requisitos en el futuro
