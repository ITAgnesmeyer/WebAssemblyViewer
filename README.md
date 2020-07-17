# WebAssemblyViewer
Application to Show WebAssembly-Sites without a Web-Server.

## Why was this project created?
Microsoft provides an extremely cool and powerful developr platform with [Blazor](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor "Blazor").
This platform is based on [WebAssembies](https://webassembly.org/ "WebAssembly"). However, WebAssemblies can only be operated in conjunction with a Web server. This is actually a pity, as unimagined design possibilities are opening up with this technology based on WebAsseblies. Each application would have to start its own Web server. This leads to various problems. Apart from the eligibility questions, this can cause major problems. 
Personally, I think that is a great pity. Therefore, with this tool I have created a way to view Balzor pages on the desktop without starting a web server every time. The tool is based on [Edge Chromium](https://www.microsoft.com/edge "Edge Chromium"), Microsoft's cool newly developed web browser. Fortunately, this browser provides a [WebView2](https://docs.microsoft.com/microsoft-edge/hosting/webview2 "WebView2") programming interface. With this programming interface it is possible to intercept and redirect calls. With a bit of extra effort, web pages can be read from a directory and also "WebAssemblies" can be started. 

### So try it out.

## Call parameters
The tool has call parameters that allow you to set a path to a configuration file.
The configuration file is an XML file in which you can set various parameters.
If you then edit and save the parameters, you can call the tool with the switch "/e". Then you will see an editing window where you can easily edit the parameters.

![Configuration-Editor](https://github.com/ITAgnesmeyer/WebAssemblyViewer/blob/master/images/ConfigEditor.PNG "Config-Editor")

## Ship Edge with the Tool
You can also ship the Edge browser right away. There is a "BrowserExecutableFolder" entry in the configuration file. Here you can simply leave the path to your Edge installation. In this case, the Edge browser will start in this directory. This can become important if you don't have the ability to install the Edge browser on the target system. You can also specify where the browser should store the profile data "BrowserUserDataFolder". If you don't have anything here, the profile data will be stored in the directory of the application. Which, of course, is not possible with an installation.

## Start Blazor-Pages from a directory
If you want to start a web page from a directory, 3 parameters are responsible for this.
You need to turn this on first. To do this, set the "Monitoring" switch to true. Then you give the "Url" to which you want to intercept "MointoringUrl". And then you enter the directory where your Blazor page is located "MonitoringPath".
Attention the url must end with a "/" at the "MonitoringUrl" switch. E.g. "https://localhost:1/". It is important that only entries with "http://", "https://" or "file://" can be found there. In my tests I did not manage to deposit other values there. Not all possible combinations are possible either. The entries must follow the rules of URL's. WebAssenbly - Pages do not work for entries with "file://". This is because the Java scripts do not allow this. The tool generates response headers that allow crossloading.

## Application Icon.
The icon of the application is loaded from the directory of the application. There is a file "App.ico" here you can deposit your own icon if you want.


## The Application creates a config-File.

```xml
<BrowserOptions xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Monitoring>false</Monitoring>
  <MonitoringUrl>URL To MONITORING-Folder</MointoringUrl>
  <Title>My Application</Title>
  <StatusBar>true</StatusBar>
  <AppStatusBar>false</AppStatusBar>
  <MonitoringPath></MonitoringPath>
  <Url>https://www.itagnesmeyer.de</Url>
  <BrowserUserDataFolder>Folder for User-Data WebView2</BrowserUserDataFolder>
  <BrowserExecutableFolder>Path to WebView2-Edge executable</BrowserExecutableFolder>
  <DevToolsEnable>true</DevToolsEnable>
  <ContextMenuEnable>true</ContextMenuEnable>
</BrowserOpetions>
```
## Parameter
1. /? => Help
2. /f:PATH => Path to the Config-File
3. /e => opens the Config-Editor

### examples
In this example the configuration-file "c:\tmp\myConfig.cfg" is used.
```
WebAssemblyViewer.exe /f:c:\tmp\myConfig.cfg
```
In this example the configuration-file "c:\tmp\myConfig.cfg" will be shown in then Config-Editor:
```
WebAssemblyViewer.exe /e /f:c:\tmp\myConfig.cfg
```
---
##### 2020 Dipl.-Ing.(FH) Guido Agnesmeyer



