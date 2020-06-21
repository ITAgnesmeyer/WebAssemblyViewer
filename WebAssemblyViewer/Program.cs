﻿using CoreWindowsWrapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WebAssemblyViewer
{
    class ArgOptions
    {
        private Dictionary<string,string> _Args;

        public ArgOptions(Dictionary<string,string> args)
        {
            this._Args = args;
            
        }

        public bool ContainsHelp
        {
            get => this._Args.ContainsKey("/?");
        }

        public bool ContainsConfigFilePath
        {
            get => this._Args.ContainsKey("/f");
        }

        public string ConfigFilePath
        {
            get => this._Args["/f"];
        }

        public bool ContainsEdit
        {
            get => this._Args.ContainsKey("/e");
        }
    }
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
                MessageBox.Show(IntPtr.Zero,"Parameter:\n\n/f:<Path>\tset the path to config-file\n/e \t\topens the Config Editor!\n/?\t\tHelp\n\nExample:\n\nWebAssemblyViewer.exe /f:c:\\tmp\\config.cfg\n\n","Parameters",MessageBoxOptions.OkOnly | MessageBoxOptions.IconInformation);
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


            BrowserOpetions opetions;
            if(!LoadOptions(configFile,out opetions))
            {
                opetions = GetDefaultOptions();
                if(WriteOptions(configFile, opetions))
                {
                    if (!argOptions.ContainsEdit)
                    {
                        MessageBoxResult result = MessageBox.Show(IntPtr.Zero, "The Application created a configuration - file = (WebAssemblyViewer.cfg)\nDo you want to continue with emtyp configuration file?", "Config file created!", MessageBoxOptions.YesNo | MessageBoxOptions.IconQuestion | MessageBoxOptions.DefButton2);
                        if (result == MessageBoxResult.No)
                            return;
                    }
                }
            }

            if (argOptions.ContainsEdit)
            {
                EditWindow ew = new EditWindow();
                NativeApp.Run(ew);
                return;
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


        static ArgOptions GetOptions(string[] args)
        {
            Dictionary<string, string> retval = args.ToDictionary(
                k => k.Split(new[] { ':' }, 2)[0].ToLower(),
                v => v.Split(new[] { ':' }, 2).Count() > 1 
                    ? v.Split(new[] { ':' }, 2)[1] 
                    : null);

            ArgOptions ops = new ArgOptions(retval);
            return ops;
        }
    }


}