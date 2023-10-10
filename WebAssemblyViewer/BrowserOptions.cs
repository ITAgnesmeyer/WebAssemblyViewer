using System.Collections.Generic;
using System.Xml.Serialization;

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
        public bool Maximized { get; set; }
        public bool TopMost { get; set; }

        public bool DisableF4 { get; set; }
        public bool EnableF4Password { get; set; }
        public string DisableF4Password { get; set; }
        public bool EnableCgi { get; set; }
        public string CgiMonitoringFolder { get; set; }
        public string CgiExeFile { get; set; }
        public string CgiMonitoringUrl { get; set; }
        [XmlArray(IsNullable = true)]
        [XmlArrayItem(typeof(string),ElementName="extension")]
        public List<string> CgiFileExtensions { get; set; }
    }
}
