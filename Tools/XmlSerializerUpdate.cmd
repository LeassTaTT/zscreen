Sgen.exe /a:"..\ZScreen\bin\x86\Debug\HelpersLib.dll" /f /t:"HelpersLib.AppSettings" /v
Sgen.exe /a:"..\ZScreen\bin\x86\Debug\ZScreenLib.dll" /f /t:"ZScreenLib.XMLSettings" /t:"ZScreenLib.Workflow" /t:"ZScreenLib.ZScreenOptions" /v
Sgen.exe /a:"..\ZScreen\bin\x86\Debug\ZScreenCoreLib.dll" /f /t:"ZScreenCoreLib.ActionsConfig" /t:"ZScreenCoreLib.Software" /t:"ZScreenCoreLib.FileNamingConfig" /t:"ZScreenCoreLib.ImageEffectsConfig" /v
Sgen.exe /a:"..\ZScreen\bin\x86\Debug\UploadersLib.dll" /f /t:"UploadersLib.UploadersConfig" /t:"UploadersLib.GoogleTranslatorConfig" /t:"UploadersLib.ProxyConfig" /v

Sgen.exe /a:"..\ZScreen\bin\x86\Release\HelpersLib.dll" /f /t:"HelpersLib.AppSettings" /v
Sgen.exe /a:"..\ZScreen\bin\x86\Release\ZScreenLib.dll" /f /t:"ZScreenLib.XMLSettings" /t:"ZScreenLib.Workflow" /v
Sgen.exe /a:"..\ZScreen\bin\x86\Release\ZScreenCoreLib.dll" /f /t:"ZScreenCoreLib.ActionsConfig" /t:"ZScreenCoreLib.Software" /t:"ZScreenCoreLib.FileNamingConfig" /t:"ZScreenCoreLib.ImageEffectsConfig" /v
Sgen.exe /a:"..\ZScreen\bin\x86\Release\UploadersLib.dll" /f /t:"UploadersLib.UploadersConfig" /t:"UploadersLib.GoogleTranslatorConfig" /t:"UploadersLib.ProxyConfig" /v



Sgen.exe /a:"..\ZUploader\bin\Debug\ZUploader.exe" /f /t:"ZUploader.Settings" /v
Sgen.exe /a:"..\ZUploader\bin\Debug\HelpersLib.dll" /f /t:"HelpersLib.AppSettings" /v
Sgen.exe /a:"..\ZUploader\bin\Debug\UploadersLib.dll" /f /t:"UploadersLib.UploadersConfig" /t:"UploadersLib.GoogleTranslatorConfig" /v

Sgen.exe /a:"..\ZUploader\bin\Release\ZUploader.exe" /f /t:"ZUploader.Settings" /v
Sgen.exe /a:"..\ZUploader\bin\Release\HelpersLib.dll" /f /t:"HelpersLib.AppSettings" /v
Sgen.exe /a:"..\ZUploader\bin\Release\UploadersLib.dll" /f /t:"UploadersLib.UploadersConfig" /t:"UploadersLib.GoogleTranslatorConfig" /v

pause