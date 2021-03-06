; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{62139810-FC9A-4632-9280-876E8D676DAB}
AppName=Skelet
AppVersion=1.1
;AppVerName=Skelet 1.1
AppPublisher=Stefan Teunissen
AppPublisherURL=http://www.stefanteunissen.nl
AppSupportURL=http://www.stefanteunissen.nl
AppUpdatesURL=http://www.stefanteunissen.nl
DefaultDirName={pf}\Skelet
DisableProgramGroupPage=yes
OutputBaseFilename=setup
Compression=lzma
SolidCompression=yes

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked
Name: "quicklaunchicon"; Description: "{cm:CreateQuickLaunchIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked; OnlyBelowVersion: 0,6.1

[Files]
Source: "E:\Users\teunissenstefan\Documents\Visual Studio 2017\Projects\Skelet\Skelet\bin\Debug\Skelet.exe"; DestDir: "{app}"; Flags: ignoreversion
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{commonprograms}\Skelet"; Filename: "{app}\Skelet.exe"
Name: "{commondesktop}\Skelet"; Filename: "{app}\Skelet.exe"; Tasks: desktopicon
Name: "{userappdata}\Microsoft\Internet Explorer\Quick Launch\Skelet"; Filename: "{app}\Skelet.exe"; Tasks: quicklaunchicon

[Run]
Filename: "{app}\Skelet.exe"; Description: "{cm:LaunchProgram,Skelet}"; Flags: nowait postinstall skipifsilent

