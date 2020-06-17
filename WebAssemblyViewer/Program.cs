using CoreWindowsWrapper;
using System;
using System.Diagnostics;

namespace WebAssemblyViewer
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            BrowserOpetions opetions;
            if(!LoadOptions("WebAssemblyViewer.cfg",out opetions))
            {
                opetions = GetDefaultOptions();
                if(WriteOptions("WebAssemblyViewer.cfg", opetions))
                {
                    MessageBoxResult result = MessageBox.Show(IntPtr.Zero, "The Application created a configuration - file = (WebAssemblyViewer.cfg)\nDo you want to continue with emtyp configuration file?", "Config file created!", MessageBoxOptions.YesNo | MessageBoxOptions.IconQuestion | MessageBoxOptions.DefButton2);
                    if (result == MessageBoxResult.No)
                        return;
                }
            }
            BrowserWindow bw = new BrowserWindow(opetions);
            NativeApp.Run(bw);
        }

        public static BrowserOpetions GetDefaultOptions()
        {
            return new BrowserOpetions
            {
                Title = "My Application",
                Monitoring = false,
                MonitoringPath = "",
                MointoringUrl = "",
                Url = "https://www.itagnesmeyer.de",
                StatusBar = true,
                DevToolsEnable = true,
                ContextMenuEnable = true,
            };
    }

        private static bool LoadOptions(string fileName, out BrowserOpetions options)
        {
            EasyXMLSerializer.SerializeTool ser = new EasyXMLSerializer.SerializeTool(fileName);
            options = ser.ReadXmlFile<BrowserOpetions>(fileName);
            if(options == null)
            {
                if(!string.IsNullOrEmpty(ser.LastError))
                {
                    MessageBox.Show(IntPtr.Zero, "Cannot load Config File " + fileName + "\n" + ser.LastError,"Error Loading Config!", MessageBoxOptions.OkOnly | MessageBoxOptions.IconExclamation);

                }
                return false;
            }
            return true;

        }
        private static bool WriteOptions(string fileName , BrowserOpetions opetions)
        {
            bool retVal = true;
            EasyXMLSerializer.SerializeTool ser = new EasyXMLSerializer.SerializeTool(fileName);
            if (!ser.WriteXmlFile(opetions))
            {
                MessageBox.Show(IntPtr.Zero, "Cannot write Options to File:" + fileName + "\n" + ser.LastError, "Options - Error!", MessageBoxOptions.OkOnly | MessageBoxOptions.IconExclamation);
                retVal = false;
            }
            return retVal;
        }
    }


}
