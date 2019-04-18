; DefenceSpace.nsi
;
; This script is perhaps one of the simplest NSIs you can make. All of the
; optional settings are left to their default settings. The installer simply 
; prompts the user asking them where to install, and drops a copy of DefenceSpace.nsi
; there. 

;--------------------------------
  !include "MUI2.nsh"

; The name of the installer
Name "DefenceSpace"

; The file to write
OutFile "DefenceSpaceInstaller.exe"

; The default installation directory
InstallDir "$PROGRAMFILES\DefenceSpace"

; Request application privileges for Windows Vista
RequestExecutionLevel admin

;--------------------------------

  !define MUI_ABORTWARNING


; Pages

  !insertmacro MUI_PAGE_LICENSE "${NSISDIR}\Docs\Modern UI\License.txt"
  !insertmacro MUI_PAGE_COMPONENTS
  !insertmacro MUI_PAGE_DIRECTORY
  !insertmacro MUI_PAGE_INSTFILES
  
  !insertmacro MUI_UNPAGE_CONFIRM
  !insertmacro MUI_UNPAGE_INSTFILES

  !insertmacro MUI_LANGUAGE "English"
;--------------------------------



; The stuff to install
Section "GameFiles" ;No components page, name is not important
  SectionIn RO
  
  ; Set output path to the installation directory.
  SetOutPath "$INSTDIR"
  
  ; Put file there
  File "DefenceSpace.exe"  
  File "UnityPlayer.dll"
  ;File "dotNetFx40_Client_x86_x64.exe /q"

  
  SetOutPath "$INSTDIR\DefenceSpace_Data"
  File /r "DefenceSpace_Data\*.*"
  
  WriteUninstaller "$INSTDIR\uninstall.exe"
SectionEnd ; end the section

Section "Start Menu Shortcuts"

  CreateDirectory "$SMPROGRAMS\DefenceSpace"
  CreateShortcut "$SMPROGRAMS\DefenceSpace\Uninstall.lnk" "$INSTDIR\uninstall.exe" "" "$INSTDIR\uninstall.exe" 0
  CreateShortcut "$SMPROGRAMS\DefenceSpace\DefenceSpace.lnk" "$INSTDIR\DefenceSpace.exe" "" "$INSTDIR\DefenceSpace.exe" 0
  
SectionEnd

Section "Desktop Shortcuts"

  CreateShortcut "$DESKTOP\DefenceSpace.lnk" "$INSTDIR\DefenceSpace.exe" "" "$INSTDIR\DefenceSpace.exe" 0
  
SectionEnd


Section "Uninstall"
  

  ; Remove files and uninstaller

  Delete "$INSTDIR\*.*"

  RMDir /r "$INSTDIR\DefenceSpace_Data\*.*"

  Delete "$SMPROGRAMS\DefenceSpace\*.*"
  RMDir "$SMPROGRAMS\DefenceSpace"

  Delete "$DESKTOP\DefenceSpace.lnk"

 RMDir "$INSTDIR"

SectionEnd