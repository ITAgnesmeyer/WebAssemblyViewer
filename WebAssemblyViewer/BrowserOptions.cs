namespace WebAssemblyViewer
{
    public class BrowserOptions
    {
        public bool Monitoring { get; set; }
        public string MonitoringUrl { get; set; }
        public string Title { get; set; }
        public bool StatusBar { get; set; }
        public bool AppStatusBar{get;set;}
        public string MonitoringPath { get; set; }
        public string Url { get; set; }
        public string BrowserUserDataFolder{get;set;}
        public string BrowserExecutableFolder { get; set; }
        public bool DevToolsEnable { get; set; }
        public bool ContextMenuEnable { get; set; }
        
    }
}
