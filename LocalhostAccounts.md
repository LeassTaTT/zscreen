# Introduction #

The Localhost tab provides a powerful interface to configure your locally hosted web servers using IIS or Apache etc.

![http://mcored.zscreen.net/2010-02/ZScreen_3.27.0.0_Beta-2010.02.16-21.08.26.png](http://mcored.zscreen.net/2010-02/ZScreen_3.27.0.0_Beta-2010.02.16-21.08.26.png)

# Details #

  * **LocalhostRoot** = root folder of your web server. In IIS this is usually Inetpub\wwwroot.
  * **SubFolderPath** = sub-folder path after your root folder. e.g. %y-%mo will dynamically create a new sub-folder path with current year and month.
  * **HttpHomePath** = typically your external IP address

If you set the Copy to Clipboard mode to "Local file as URI" in the main tab as follows:

![http://mcored.zscreen.net/2010-02/SS-2010.02.16-21.41.26.png](http://mcored.zscreen.net/2010-02/SS-2010.02.16-21.41.26.png)

you will be able to use this Localhost feature for sharing screenshots across a LAN:

```
file://mike-pc/transfer/Mike/2010-02/ZScreen_3.27.0.0_Beta-2010.02.16-21.43.44.png
```