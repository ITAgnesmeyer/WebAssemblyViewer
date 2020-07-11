# WebAssemblyViewer
Application to Show WebAssembly-Sites without a Web-Server.

The Application creates a config-File.

```xml
<BrowserOpetions xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Monitoring>false</Monitoring>
  <MointoringUrl>URL To MONITORING-Folder</MointoringUrl>
  <Title>My Application</Title>
  <StatusBar>true</StatusBar>
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

