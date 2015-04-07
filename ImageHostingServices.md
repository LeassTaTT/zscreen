Save the following as Default.zihs and use Import function in ZScreen > Image Uploaders to access the services.

```
<?xml version="1.0"?>
<ImageHostingServiceManager xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <ImageHostingServices>
    <ImageHostingService>
      <Name>img1-0.1.1</Name>
      <Arguments />
      <UploadURL>http://img1.us/?app</UploadURL>
       <FileForm>fileup</FileForm>
      <RegexpList>
        <string>(https?://)?(i.\.)?([a-zA-Z0-9_%]*)\b\.[a-z]{2,4}(\.[a-z]{2})?((/[a-zA-Z0-9_%]*)+)?(\.[a-z]*)?$</string>
      </RegexpList>
       <Fullimage>$1</Fullimage>
      <Thumbnail />
      <Regexps>
        <string />
      </Regexps>
    </ImageHostingService>
    <ImageHostingService>
      <Name>TinyPic</Name>
      <Arguments>
        <ArrayOfString>
          <string>domain_lang</string>
          <string>en</string>
        </ArrayOfString>
        <ArrayOfString>
          <string>action</string>
          <string>upload</string>
        </ArrayOfString>
        <ArrayOfString>
          <string>MAX_FILE_SIZE</string>
          <string>200000000</string>
        </ArrayOfString>
        <ArrayOfString>
          <string>file_type</string>
          <string>image</string>
        </ArrayOfString>
        <ArrayOfString>
          <string>dimension</string>
          <string>1600</string>
        </ArrayOfString>
      </Arguments>
      <UploadURL>http://s5.tinypic.com/plugin/upload.php</UploadURL>
      <FileForm>the_file</FileForm>
      <RegexpList>
        <string>(?&lt;=ival" value=").+(?=" /&gt;)</string>
        <string>(?&lt;=pic" value=").+(?=" /&gt;)</string>
        <string>(?&lt;=ext" value=").+(?=" /&gt;)</string>
      </RegexpList>
      <Fullimage>"http://i" $1 ".tinypic.com/" $2 ($3 = "" ? ".jpg" : $3)</Fullimage>
      <Thumbnail>"http://i" $1 ".tinypic.com/" $2 "_th" ($3 = "" ? ".jpg" : $3)</Thumbnail>
    </ImageHostingService>
    <ImageHostingService>
      <Name>ImageShack</Name>
      <Arguments>
        <ArrayOfString>
          <string>MAX_FILE_SIZE</string>
          <string>13145728</string>
        </ArrayOfString>
        <ArrayOfString>
          <string>refer</string>
          <string />
        </ArrayOfString>
        <ArrayOfString>
          <string>brand</string>
          <string />
        </ArrayOfString>
        <ArrayOfString>
          <string>optimage</string>
          <string>1</string>
        </ArrayOfString>
        <ArrayOfString>
          <string>rembar</string>
          <string>1</string>
        </ArrayOfString>
        <ArrayOfString>
          <string>submit</string>
          <string>host it!</string>
        </ArrayOfString>
        <ArrayOfString>
          <string>optsize</string>
          <string>resample</string>
        </ArrayOfString>
        <ArrayOfString>
          <string>xml</string>
          <string>yes</string>
        </ArrayOfString>
      </Arguments>
      <UploadURL>http://imageshack.us/index.php</UploadURL>
      <FileForm>fileupload</FileForm>
      <RegexpList>
        <string>(?&lt;=image_link&gt;).+(?=&lt;/image_link)</string>
        <string>(?&lt;=thumb_link&gt;).+(?=&lt;/thumb_link)</string>
      </RegexpList>
      <Fullimage>$1</Fullimage>
      <Thumbnail>$2</Thumbnail>
    </ImageHostingService>
    <ImageHostingService>
      <Name>xs.to</Name>
      <Arguments>
        <ArrayOfString>
          <string>action</string>
          <string>doupload</string>
        </ArrayOfString>
        <ArrayOfString>
          <string>acceptTOS</string>
          <string>Yes</string>
        </ArrayOfString>
        <ArrayOfString>
          <string>prtype</string>
          <string>0</string>
        </ArrayOfString>
        <ArrayOfString>
          <string>submit</string>
          <string>Upload!</string>
        </ArrayOfString>
      </Arguments>
      <UploadURL>http://xs.to/directupload.php</UploadURL>
      <FileForm>userfile</FileForm>
      <RegexpList>
        <string>(?&lt;=value=").+(?="&gt;&lt;)</string>
      </RegexpList>
      <Fullimage>$1</Fullimage>
      <Thumbnail>$1 "xs.jpg"</Thumbnail>
    </ImageHostingService>
  </ImageHostingServices>
</ImageHostingServiceManager>
```