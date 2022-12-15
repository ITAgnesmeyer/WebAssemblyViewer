using CoreWindowsWrapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WebAssemblyViewer
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            string configFile = "WebAssemblyViewer.cfg";


            ArgOptions argOptions = GetOptions(args);

            if (argOptions.ContainsHelp)
            {
                MessageBox.Show(IntPtr.Zero,
                    "Parameter:\n\n/f:<Path>\tset the path to config-file\n/e \t\topens the Config Editor!\n/?\t\tHelp\n\nExample:\n\nWebAssemblyViewer.exe /f:c:\\tmp\\config.cfg\n\n",
                    "Parameters", MessageBoxOptions.OkOnly | MessageBoxOptions.IconInformation);
                return;
            }

            if (argOptions.ContainsConfigFilePath)
            {
                configFile = argOptions.ConfigFilePath;
                FileInfo fi = new FileInfo(configFile);
                if (fi.Directory == null || !fi.Directory.Exists)
                {
                    MessageBox.Show("The directory of config-File cannot be found!");
                    return;
                }
            }

            if (!LoadOptions(configFile, out BrowserOptions opetions))
            {
                opetions = GetDefaultOptions();
                if (WriteOptions(configFile, opetions))
                {
                    if (!argOptions.ContainsEdit)
                    {
                        MessageBoxResult result = MessageBox.Show(IntPtr.Zero,
                            "The Application created a configuration - file = (WebAssemblyViewer.cfg)\nDo you want to continue with emtyp configuration file?",
                            "Config file created!",
                            MessageBoxOptions.YesNo | MessageBoxOptions.IconQuestion | MessageBoxOptions.DefButton2);
                        if (result == MessageBoxResult.No)
                        {
                            result = MessageBox.Show(IntPtr.Zero, "Do you want to Edit the Cofig-File?",
                                "Want to edit Config-File?", MessageBoxOptions.OkOnly | MessageBoxOptions.YesNo);
                            if (result == MessageBoxResult.Yes)
                            {
                                argOptions.ContainsEdit = true;
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                }
            }

            if (argOptions.ContainsEdit)
            {
                EditWindow ew = new EditWindow(opetions);
                NativeApp.Run(ew);


                if (ew.Result)
                {
                    if (WriteOptions(configFile, opetions))
                    {
                        AppMessageBox mg = new AppMessageBox
                        {
                            Caption = "Continue?",
                            Message =
                            "The configuration has been saved.\nDo you want to start the App?\nClick Ok to start the Applicaiton."
                        };
                        NativeApp.Run(mg);
                        var result = mg.Result;
                        if (result == MessageBoxResult.Cancel)
                        {
                            return;
                        }
                    }
                }
                else
                {
                    return;
                }
            }

            BrowserWindow bw = new BrowserWindow(opetions);
            NativeApp.Run(bw);
        }
        
        public static BrowserOptions GetDefaultOptions()
        {
            return new BrowserOptions
            {
                Title = "My Application",
                Monitoring = false,
                MonitoringPath = "",
                MonitoringUrl = "",
                Url = "https://www.itagnesmeyer.de",
                StatusBar = true,
                DevToolsEnable = true,
                ContextMenuEnable = true,
                BrowserUserDataFolder = "",
                BrowserExecutableFolder = "",
                AppStatusBar=false,
                Maximized = false,
                TopMost = false
                
            };
        }

        private static bool LoadOptions(string fileName, out BrowserOptions options)
        {
            EasyXMLSerializer.SerializeTool ser = new EasyXMLSerializer.SerializeTool(fileName);
            options = ser.ReadXmlFile<BrowserOptions>(fileName);
            if (options == null)
            {
                if (!string.IsNullOrEmpty(ser.LastError))
                {
                    MessageBox.Show(IntPtr.Zero, "Cannot load Config File " + fileName + "\n" + ser.LastError,
                        "Error Loading Config!", MessageBoxOptions.OkOnly | MessageBoxOptions.IconExclamation);
                }

                return false;
            }

            return true;
        }

        private static bool WriteOptions(string fileName, BrowserOptions opetions)
        {
            bool retVal = true;
            EasyXMLSerializer.SerializeTool ser = new EasyXMLSerializer.SerializeTool(fileName);
            if (!ser.WriteXmlFile(opetions))
            {
                MessageBox.Show(IntPtr.Zero, "Cannot write Options to File:" + fileName + "\n" + ser.LastError,
                    "Options - Error!", MessageBoxOptions.OkOnly | MessageBoxOptions.IconExclamation);
                retVal = false;
            }

            return retVal;
        }
        
        static ArgOptions GetOptions(string[] args)
        {
            Dictionary<string, string> retval = args.ToDictionary(
                k => k.Split(new[] {':'}, 2)[0].ToLower(),
                v => v.Split(new[] { ':' }, 2).Length > 1
                    ? v.Split(new[] {':'}, 2)[1]
                    : null);

            ArgOptions ops = new ArgOptions(retval);
            return ops;
        }
    }
}
