
# Introduction #

FTP Accounts tab provides a powerful interface to configure your FTP Account settings:

![http://wmwiki.com/mcored/zscreen/ZScreen_3.5.2.0_Beta-2009.09.12-18.44.54.png](http://wmwiki.com/mcored/zscreen/ZScreen_3.5.2.0_Beta-2009.09.12-18.44.54.png)

# Automatic folder creation #

ZScreen can automatically create sub-folders in your FTP Server based on the SubFolderPath settings. FTP Path of a file is shown below:
```
URL = ftp:// + Host + : + Port + SubFolderPath + FileName
```

# URL creation based on FTP Account settings #

The usual URL creation procedure is as follows:
```
URL = HttpHomePath + SubFolderPath + FileName
```

This default behavior of URL creation can be customized.

If HttpHomePath starts with the @ char then,
```
URL = HttpHomePath + FileName
```
If HttpHomePath is empty then,
```
URL = Host + SubFolderPath + FileName
```

## Notes ##

If URL does not start with **http://** then it will be added automatically. The **/** char will be handled automatically; you don't need to add **/** char to the beginning or end of paths.

If Host starts with **ftp.** then it will be automatically removed while making URL (when using empty HttpHomePath or when using % in HttpHomePath).

File Name: screenshot.jpg

## Standard usage ##
### Using IP address ###
```
Host: 80.123.456.78
SubFolderPath: ZScreen
HttpHomePath: zscreen.net/jaex
PreviewFtpPath: ftp://80.123.456.78:21/ZScreen/screenshot.jpg
PreviewHttpPath: http://zscreen.net/jaex/ZScreen/screenshot.jpg
```
### Using domain ###
```
Host: ftp.zscreen.net
SubFolderPath:
HttpHomePath:
PreviewFtpPath: ftp://ftp.zscreen.net:21/screenshot.jpg
PreviewHttpPath: http://zscreen.net/screenshot.jpg
```
### Using subdomain ###
```
Host: jaex.zscreen.net
SubFolderPath: ZScreen/Screenshots
HttpHomePath: www.zscreen.net/jaex
PreviewFtpPath: ftp://jaex.zscreen.net:21/ZScreen/Screenshots/screenshot.jpg
PreviewHttpPath: http://www.zscreen.net/jaex/ZScreen/Screenshots/screenshot.jpg
```
## Using empty HttpHomePath ##
### Using domain ###
```
Host: ftp.zscreen.net
SubFolderPath: ZScreen
HttpHomePath:
PreviewFtpPath: ftp://ftp.zscreen.net:21/ZScreen/screenshot.jpg
PreviewHttpPath: http://zscreen.net/ZScreen/screenshot.jpg
```
### Using subdomain ###
```
Host: jaex.zscreen.net
SubFolderPath: Screenshots/ZScreen
HttpHomePath:
PreviewFtpPath: ftp://jaex.zscreen.net:21/Screenshots/ZScreen/screenshot.jpg
PreviewHttpPath: http://jaex.zscreen.net/Screenshots/ZScreen/screenshot.jpg
```

## Using % in HttpHomePath ##

% char will be automatically replaced with Host.

### Using domain ###
```
Host: zscreen.net
SubFolderPath: ZScreen
HttpHomePath: %/jaex
PreviewFtpPath: ftp://ftp.zscreen.net:21/jaex/ZScreen/screenshot.jpg
PreviewHttpPath: http://zscreen.net/jaex/ZScreen/screenshot.jpg
```
### Using subdomain ###
```
Host: jaex.zscreen.net
SubFolderPath: ZScreen
HttpHomePath: %
PreviewFtpPath: ftp://jaex.zscreen.net:21/ZScreen/screenshot.jpg
PreviewHttpPath: http://jaex.zscreen.net/ZScreen/screenshot.jpg
```

## Using @ in HttpHomePath ##
### Using subdomain ###
```
Host: ftp.powweb.com
SubFolderPath: zscreen
HttpHomePath: @mcored.zscreen.net
PreviewFtpPath: ftp://ftp.powweb.com:21/zscreen/screenshot.jpg
PreviewHttpPath: http://mcored.zscreen.net/screenshot.jpg
```