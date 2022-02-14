using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using CoreWindowsWrapper;
using Diga.Core.Api.Win32;
using Diga.NativeControls.WebBrowser;
using Diga.WebView2.Wrapper;
using Diga.WebView2.Wrapper.EventArguments;

namespace WebAssemblyViewer
{
    class BrowserWindow:NativeWindow
    {
        private NativeWebBrowser _Browser;
        private BrowserOptions _Options;
        private const string ParamTable = "";//"<table style=\"font-size:20; border-color:blue;background-color:ghostwhite;\"><thead style=\"text-align:left; background-color:gainsboro;\"><tr><th>Parameter</th><th>Description</th></tr></thead><tbody><tr><td>/me</td><td>Enables Monitoring</td></tr><tr><td>/mu:&lt;url&gt;</td><td>Monitoring-Url<br>The url for testing while monitoring!</td></tr><tr><td>/mf:&lt;Folder&gt;</td><td>Monitoring-Folder-Path<br>path to the Folder contining Html-Content</td></tr><tr><td>/t:&lt;title&gt</td><td>Tile shown in the Window - Head</td></tr><tr><td>/sb</td><td>enables the Statusbar option of the Browser</td></tr><tr><td>/dt</td><td>Enables Dev-Tools F12</td></tr><tr><td>/cm</td><td>endables Context-Menu</td></tr><tr><td>/nu:&lt;url&gt;</td><td>Url to navigate to</td></tr></tbody></table>";
        private const string ParamErrorHead = "<h1>Prameter Error</h1>";
        private const string ParamErrorMonitoringUrl = "<h3>The Monitorin-Url must be Set<br>and must start with<br>http://<br>or https://<br>or file://</h3>";
        private const string ParamErrorUrl = "<h3>The Url must be set<br>and must start with<br>http://<br>or https://<br>or file://</h3>";
        private const string ParamErrorMoitoringPath = "<h3>The Monitoring-Path must be valid and set!</h3>";
        public BrowserWindow(BrowserOptions options):base()
        {
            this._Options = options;
            this.Text = options.Title;
            this._Browser.BrowserUserDataFolder = options.BrowserUserDataFolder;
            this._Browser.BrowserExecutableFolder = options.BrowserExecutableFolder;
            this.StatusBar = options.AppStatusBar;
        }
       
        protected override void InitControls()
        {
            this.Text = "WebAssemblyRunner";
            this.Name = "WebAssemblyRunner";
            this.StatusBar = true;
            this.Width = 800;
            this.Height = 600;
            this.StartUpPosition = WindowsStartupPosition.CenterScreen;
            this.IconFile = "App.ico";
            

            this._Browser = new NativeWebBrowser
            {
                Width = this.Width,
                Height = this.Height,
                DefaultContextMenusEnabled = false,
                DevToolsEnabled = false,
                AutoDock = true
            };
            
            this._Browser.WebViewCreated += OnWebWindowCreated;
            this._Browser.ProcessFailed += OnProcessFailed;
            this._Browser.ContentLoading += OnContentLoading;
            this._Browser.NavigationStart += OnNavigationStart;
            this._Browser.NavigationCompleted += OnNavigationCompleted;
            this._Browser.PermissionRequested += OnPermissionRequested;
            this._Browser.AcceleratorKeyPressed += OnAcceleratorKeyPressed;
            this._Browser.WebMessageReceived+=OnWebMessageReceived;

            this.Controls.Add(this._Browser);
            
        }

        private void OnWebMessageReceived(object sender, WebMessageReceivedEventArgs e)
        {
            Debug.Print(e.Source);
            Debug.Print(e.WebMessageAsJson);
            //Debug.Print(e.WebMessageAsString);
        }

        private const uint NoneStyle = 385941504;

        private const uint NoneExStyle = 327680;

        private uint OldStyle;
        private uint OldExStyle;

        private void OnAcceleratorKeyPressed(object sender, AcceleratorKeyPressedEventArgs e)
        {
            uint currentStyle = GetWindowStyle();
            uint currentExStyle = GetWindowExStyle();
            if (e.KeyVentType == KeyEventType.KeyDown && e.VirtualKey == VirtualKeys.VK_F11)
            {
                if (currentStyle == NoneStyle && currentExStyle == NoneExStyle)
                {
                    SetWindowState(WindowState.Normal);
                    UpdateStyle(this.OldStyle);
                    UpdateExStyle(this.OldExStyle);
                    UpdateWidow();

                }
                else
                {
                    this.OldStyle = currentStyle;
                    this.OldExStyle = currentExStyle;
                    SetWindowState(WindowState.Maximized);
                    UpdateStyle(NoneStyle);
                    UpdateExStyle(NoneExStyle);
                    UpdateWidow();

                }

                
            }
        }

        private void OnPermissionRequested(object sender, PermissionRequestedEventArgs e)
        {
            Debug.Print("Url" + e.Uri);
            Debug.Print("PermissonType:" + e.PermissionType);
            Debug.Print("State:" + e.State);
        }

        private void OnNavigationCompleted(object sender, NavigationCompletedEventArgs e)
        {
            Debug.Print("IsSuccess:" + e.IsSuccess);
            Debug.Print("NavigationId" + e.NavigationId);
            Debug.Print("WebErrorStatus:" + e.WebErrorStatus);
        }

        private void OnNavigationStart(object sender, NavigationStartingEventArgs e)
        {
            Debug.Print("isRequested:" + e.IsRedirected);
            Debug.Print("IsuserIndended:" + e.IsUserInitiated);
            Debug.Print("Url:" + e.uri);
        }

        private void OnContentLoading(object sender, ContentLoadingEventArgs e)
        {
            Debug.Print("NavigationId:" + e.NavigationId);
            Debug.Print("IsError:" + e.IsErrorPage);
        }

        private void OnProcessFailed(object sender, ProcessFailedEventArgs e)
        {
            string message = "Process Error:";
            switch(e.ProcessFailedKind)
            {
                case ProcessFailedKind.BrowserProcessExited:
                    message += "Browser Process Exited!";
                    break;
                case ProcessFailedKind.RenderProcessExited:
                    message += "Render Process Exited!";
                    break;
                case ProcessFailedKind.RenderProcessUnresponsive:
                    message += "Render Process Unresponsive!";
                    break;
            }

            string messageAll = $"A critical Error occured!\n{message}\nThe application will be closed!";
            MessageBox.Show(this.Handle, messageAll, "Process Failed!", MessageBoxOptions.OkOnly | MessageBoxOptions.IconError);
            Close();

        }

        private void OnWebWindowCreated(object sender, EventArgs e)
        {
            
            if(this._Options.Monitoring)
            {
                if(!TestUrl(this._Options.MonitoringUrl))
                {
                    string message = ParamErrorHead + ParamErrorMonitoringUrl + ParamTable; 
                    this._Browser.NavigateToString(message);
                    return;
                }

                if(!Directory.Exists(this._Options.MonitoringPath))
                {
                    string message = ParamErrorHead + ParamErrorMoitoringPath + ParamTable;
                    this._Browser.NavigateToString(message);
                    return;
                }
            }

            //this._Browser.Navigate(this._Options.Url);
            this._Browser.IsStatusBarEnabled = this._Options.StatusBar;
            this._Browser.MonitoringFolder = this._Options.MonitoringPath;
            this._Browser.MonitoringUrl = this._Options.MonitoringUrl;
            

            this._Browser.EnableMonitoring = this._Options.Monitoring;
            if(!TestUrl(this._Options.Url))
            {
                string message = ParamErrorHead + ParamErrorUrl + ParamTable;
                this._Browser.NavigateToString(message);
                return;
            }
            this._Browser.DevToolsEnabled = this._Options.DevToolsEnable;
            this._Browser.DefaultContextMenusEnabled = this._Options.ContextMenuEnable;
            
            //this.Width += 1;
            this.Visible = false;
            if (this._Options.Maximized)
            {
                this.SetWindowState(WindowState.Maximized);
                this.UpdateStyle(NoneStyle);
                //+ unchecked((uint)0x00000008L)
                this.UpdateExStyle(NoneExStyle );
                this.UpdateWidow();
                this._Browser.AcceleratorKeyPressed -= OnAcceleratorKeyPressed;
                IntPtr hMon = User32.MonitorFromWindow(this.Handle,
                    MonitorDefaultFlags.MONITOR_DEFAULTTONEAREST);
                if (hMon != IntPtr.Zero)
                {
                    MonitorInfo info = new MonitorInfo();
                    info.cbSize = (uint)Marshal.SizeOf(info);
                    if (User32.GetMonitorInfo(hMon, ref info))
                    {
                        Rect r = info.rcMonitor;
                        this.Left = r.Left;
                        this.Top = r.Top;
                        this.Width = r.Width;
                        this.Height = r.Height;
                    }

                }

            }

            if (this._Options.TopMost)
            {
                uint style = GetWindowExStyle();
                style |= unchecked((uint)0x00000008L);
                
                this.UpdateExStyle(style );
                
            }

            this.Visible = true;
            
            this._Browser.Navigate(this._Options.Url);
            
            
            //this.Top = 0;
            //this.Left = 0;
        }

        private bool TestUrl(string url)
        {
            if (string.IsNullOrEmpty(url)) return false;
            if (url.StartsWith("http://")) return true;
            if (url.StartsWith("https://")) return true;
            if (url.StartsWith("file://")) return true;
            return false;
        }
       
    }
}
