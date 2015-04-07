# Requirements #

  * Inno Setup

# Getting started #

Download the current ZScreen setup and install it to a temporary folder e.g. ZScreen\bin\Release

Download the latest .iss file from http://code.google.com/p/zscreen/source/browse/#svn%2Ftrunk

Modify the .iss to until all the files are accessible with the correct paths. You will be modifying the "Files" section in the .iss file.

# Inno Setup file #

In the .iss file, just under "Files" section where it currently says:

```
[Files]

Source: ZScreen\bin\Release\*.exe; Excludes: *.vshost.exe; DestDir: {app}; Flags: ignoreversion
Source: ZScreen\bin\Release\*.dll; DestDir: {app}; Flags: ignoreversion
Source: ZScreen\bin\Release\*.xml; DestDir: {app}; Flags: ignoreversion recursesubdirs
Source: ZUploader\bin\Release\*.exe; Excludes: *.vshost.exe; DestDir: {app}; Flags: ignoreversion
Source: ZUploader\bin\Release\*.dll; DestDir: {app}; Flags: ignoreversion
```

You will need to add lines to include the setting files.

```
Source: AppSettings.xml; DestDir: {localappdata}\{#MyAppMyAppName}; Flags: ignoreversion
Source: ZScreen-4.2.4-Settings.xml; DestDir: {appdata}\{#MyAppMyAppName}\Settings; Flags: ignoreversion
Source: UploadersConfig.xml; DestDir: {appdata}\{#MyAppMyAppName}\Settings; Flags: ignoreversion
Source: GoogleTranslateConfig.xml; DestDir: {userappdata}\{#MyAppMyAppName}\Settings; Flags: ignoreversion
```