﻿using CoreWindowsWrapper;
using Diga.Core.Api.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;

using Point = Diga.Core.Api.Win32.Point;

namespace WebAssemblyViewer
{
    class EditWindow : NativeWindow
    {
        private NativeCheckBox _ChkMonitoring;
        private NativeCheckBox _ChkStatusBar;
        private NativeCheckBox _ChkAppStatusBar;
        private NativeCheckBox _ChkDevTools;
        private NativeCheckBox _ChkContextMenu;
        private NativeCheckBox _ChkTopMost;
        private NativeCheckBox _ChkMaximized;
        private NativeCheckBox _ChkDisableF4;
        private NativeCheckBox _ChkEnableF4WithPw;
        private NativeTextBox _TxtDisableF4Password;
        private NativeTextBox _TxtMonitoringUlr;
        private NativeTextBox _TxtUserDataFolder;
        private NativeButton _BnSelectUserDataFolder;
        private NativeTextBox _TxtBrowserExecutable;
        private NativeButton _BnSelectBrowserExecutable;
        private NativeTextBox _TxtTitle;

        private NativeTextBox _TxtMonitoringPath;
        private NativeTextBox _TxtUrl;

        private NativeButton _BnSelectMonitoringPath;
        private NativeCheckBox _ChkCgiMoitoring;
        private NativeTextBox _TxtCgiMonitoringPath;
        private NativeButton _BnSelectCgiMonitoringPath;
        private NativeTextBox _TxtCgiExeFile;
        private NativeButton _BnSelectCgiExeFile;
        private NativeTextBox _TxtCgiFileExtensions;
        private NativeTextBox _TxtCgiMonitoringUrl;
        private readonly BrowserOptions _Options;
        private NativeButton _BnOk;
        private NativeButton _BnCancel;
        private NativeLink _LinkButton;
        public bool Result { get; private set; }

        public EditWindow(BrowserOptions options) 
        {
            this._Options = options;
        }

        private void ViewToOptions()
        {
            this._Options.Monitoring = this._ChkMonitoring.Checked;
            this._Options.MonitoringUrl = this._TxtMonitoringUlr.Text;
            this._Options.MonitoringPath = this._TxtMonitoringPath.Text;
            this._Options.Title = this._TxtTitle.Text;
            this._Options.Url = this._TxtUrl.Text;
            this._Options.DevToolsEnable = this._ChkDevTools.Checked;
            this._Options.ContextMenuEnable = this._ChkContextMenu.Checked;
            this._Options.StatusBar = this._ChkStatusBar.Checked;
            this._Options.AppStatusBar = this._ChkAppStatusBar.Checked;
            this._Options.TopMost = this._ChkTopMost.Checked;
            this._Options.Maximized = this._ChkMaximized.Checked;
            this._Options.BrowserUserDataFolder = this._TxtUserDataFolder.Text;
            this._Options.BrowserExecutableFolder = this._TxtBrowserExecutable.Text;
            this._Options.DisableF4 = this._ChkDisableF4.Checked;
            this._Options.EnableF4Password = this._ChkEnableF4WithPw.Checked;
            this._Options.DisableF4Password = this._TxtDisableF4Password.Text;
            this._Options.EnableCgi = this._ChkCgiMoitoring.Checked;
            this._Options.CgiMonitoringFolder = this._TxtCgiMonitoringPath.Text;
            this._Options.CgiExeFile = this._TxtCgiExeFile.Text;
            this._Options.CgiFileExtensions = GetExtensionsFromExtensionText(this._TxtCgiFileExtensions.Text);
            this._Options.CgiMonitoringUrl = this._TxtCgiMonitoringUrl.Text;
        }

        private string GetTextFromMointoringExtensions(List<string> extensions)
        {
            if (extensions == null)
            {
                return string.Empty;
            }
            return string.Join(";", extensions);
        }

        private List<string> GetExtensionsFromExtensionText(string extension)
        {
            if (string.IsNullOrEmpty(extension))
                return null;
            return new List<string>(extension.Split(';'));
        }
        private void OptionsToView()
        {
            this._ChkMonitoring.Checked = this._Options.Monitoring;
            this._TxtMonitoringUlr.Text = this._Options.MonitoringUrl;
            this._TxtMonitoringPath.Text = this._Options.MonitoringPath;
            this._TxtUserDataFolder.Text = this._Options.BrowserUserDataFolder;
            this._TxtBrowserExecutable.Text = this._Options.BrowserExecutableFolder;
            this._TxtTitle.Text = this._Options.Title;
            this._TxtUrl.Text = this._Options.Url;
            this._ChkDevTools.Checked = this._Options.DevToolsEnable;
            this._ChkContextMenu.Checked = this._Options.ContextMenuEnable;
            this._ChkStatusBar.Checked = this._Options.StatusBar;
            this._ChkAppStatusBar.Checked = this._Options.AppStatusBar;
            this._ChkTopMost.Checked = this._Options.TopMost;
            this._ChkMaximized.Checked = this._Options.Maximized;
            this._ChkDisableF4.Checked = this._Options.DisableF4;
            this._ChkEnableF4WithPw.Checked = this._Options.EnableF4Password;
            this._TxtDisableF4Password.Text = this._Options.DisableF4Password;
            this._ChkCgiMoitoring.Checked = this._Options.EnableCgi;
            this._TxtCgiMonitoringPath.Text = this._Options.CgiMonitoringFolder;
            this._TxtCgiExeFile.Text = this._Options.CgiExeFile;
            this._TxtCgiFileExtensions.Text = GetTextFromMointoringExtensions(this._Options.CgiFileExtensions);
            this._TxtCgiMonitoringUrl.Text = this._Options.CgiMonitoringUrl;
        }

        protected override void OnBeforeCreate(BeforeWindowCreateEventArgs e)
        {
            e.Styles.Style = WindowStylesConst.WS_DLGFRAME | WindowStylesConst.WS_SYSMENU;
        }

        protected override void InitControls()
        {
            this.Text = "Configuration Editor";
            this.Width = 640;
            this.Height = 430;
            this.IconFile = "App.ico";
            this.StartUpPosition = WindowsStartupPosition.CenterScreen;

            int top = 10;
            int leftLeft = 10;
            int rightLeft = 120;
            int textHeight = 20;
            int lblWidth = 100;
            int chkWidth = 95;
            int inputWidth = this.Width - rightLeft - 50;
            int chkLeft = leftLeft;
            this._ChkContextMenu = new NativeCheckBox
            {
                Location = new Point(chkLeft, top),
                Width = chkWidth,
                Height = textHeight,
                Text = "Context-Menu",
            };
            this._ChkContextMenu.Style |= WindowStylesConst.WS_TABSTOP;
            chkLeft += this._ChkContextMenu.Width +10;
            
            this._ChkDevTools = new NativeCheckBox
            {
                Location = new Point(chkLeft, top),
                Width = chkWidth,
                Height = textHeight,
                Text = "Dev-Tools"
            };
            chkLeft += this._ChkDevTools.Width +10;
            this._ChkStatusBar = new NativeCheckBox
            {
                Location = new Point(chkLeft, top),
                Width = chkWidth,
                Height = textHeight,
                Text = "Statusbar"
            };

            chkLeft += this._ChkStatusBar.Width +10;
            this._ChkAppStatusBar = new NativeCheckBox
            {
                Location = new Point(chkLeft, top),
                Width = chkWidth,
                Height = textHeight,
                Text = "App-Statusbar"
            };
            chkLeft += this._ChkAppStatusBar.Width +10;
            this._ChkTopMost = new NativeCheckBox
            {
                Location = new Point(chkLeft, top),
                Width = chkWidth,
                Height = textHeight,
                Text = "Topmost"
            };
            chkLeft += this._ChkTopMost.Width +10;
            this._ChkMaximized = new NativeCheckBox
            {
                Location = new Point(chkLeft, top),
                Width = chkWidth,
                Height = textHeight,
                Text = "Maximized"
            };
            chkLeft = leftLeft;

            top += 30;
            this._ChkDisableF4 = new NativeCheckBox()
            {
                Location = new Point(chkLeft, top),
                Width = chkWidth ,
                Height = textHeight,
                Text = "Disable F4"
            };

            chkLeft += this._ChkDisableF4.Width + 10;

            this._ChkEnableF4WithPw = new NativeCheckBox()
            {
                Location = new Point(chkLeft, top),
                Width = chkWidth,
                Height = textHeight,
                Text = "PW Enable F4"
            };
            chkLeft += this._ChkEnableF4WithPw.Width + 10;
            this._TxtDisableF4Password = new NativeTextBox
            {
                Location = new Point(chkLeft, top),
                Width = chkWidth * 2,
                Height = textHeight,
                Text = "",
                
            };
            this._TxtDisableF4Password.Style |= EditBoxStyles.ES_PASSWORD | WindowStylesConst.WS_BORDER;
            

            top += 30;

            NativeLabel lblTitle = new NativeLabel
            {
                Location = new Point(leftLeft, top),
                Width = lblWidth,
                Height = textHeight,
                Text = "Title:",
                BackColor = this.BackColor
            };

            this._TxtTitle = new NativeTextBox
            {
                Location = new Point(rightLeft, top),
                Width = inputWidth,
                Height = textHeight,
            };
            this._TxtTitle.Style |= WindowStylesConst.WS_BORDER;


            top += 30;
            NativeLabel lblUrl = new NativeLabel
            {
                Location = new Point(leftLeft, top),
                Width = lblWidth,
                Height = textHeight,
                Text = "Url:",
                BackColor = this.BackColor
            };

            this._TxtUrl = new NativeTextBox
            {
                Location = new Point(rightLeft, top),
                Width = inputWidth,
                Height = textHeight
            };
            this._TxtUrl.Style |= WindowStylesConst.WS_BORDER;


            top += 30;
            NativeLabel lblUserFolder = new NativeLabel
            {
                Location = new Point(leftLeft, top),
                Width = lblWidth,
                Height = textHeight,
                Text = "Data-Folder:",
                BackColor = this.BackColor
            };

            this._TxtUserDataFolder = new NativeTextBox
            {
                Location = new Point(rightLeft, top),
                Width = inputWidth,
                Height = textHeight
            };
            this._TxtUserDataFolder.Style |= WindowStylesConst.WS_BORDER;

            this._BnSelectUserDataFolder = new NativeButton
            {
                Location = new Point(rightLeft + inputWidth + 1, top),
                Width = 25,
                Height = textHeight,
                Text = "…"
            };
            this._BnSelectUserDataFolder.Clicked += BnSelectUserDataFolder_Clicked;

            top += 30;
            NativeLabel lblBrowserExecutable = new NativeLabel
            {
                Location = new Point(leftLeft, top),
                Width = lblWidth,
                Height = textHeight,
                Text = "Browser-Folder:",
                BackColor = this.BackColor
            };

            this._TxtBrowserExecutable = new NativeTextBox
            {
                Location = new Point(rightLeft, top),
                Width = inputWidth,
                Height = textHeight
            };
            this._TxtBrowserExecutable.Style |= WindowStylesConst.WS_BORDER;

            this._BnSelectBrowserExecutable = new NativeButton
            {
                Location = new Point(rightLeft + inputWidth + 1, top),
                Width = 25,
                Height = textHeight,
                Text = "…"
            };
            this._BnSelectBrowserExecutable.Clicked += BnSelectBrowserExecutable_Clicked;

            top += 30;
            this._ChkMonitoring = new NativeCheckBox
            {
                Location = new Point(leftLeft, top),
                Width = lblWidth,
                Height = textHeight,
                Text = "Monitoring"
            };

            top += 30;
            NativeLabel lblMonitoriingUrl = new NativeLabel
            {
                Location = new Point(leftLeft, top),
                Width = lblWidth,
                Height = textHeight,
                Text = "Monitoring-Url:",
                BackColor = this.BackColor
            };
            this._TxtMonitoringUlr = new NativeTextBox
            {
                Location = new Point(rightLeft, top),
                Width = inputWidth,
                Height = textHeight
               
            };

            this._TxtMonitoringUlr.Style |= WindowStylesConst.WS_BORDER;

            top += 30;
            NativeLabel lblMonitoriingPath = new NativeLabel
            {
                Location = new Point(leftLeft, top),
                Width = lblWidth,
                Height = textHeight,
                Text = "Monitoring-Path:",
                BackColor = this.BackColor
            };

            this._TxtMonitoringPath = new NativeTextBox
            {
                Location = new Point(rightLeft, top),
                Width = inputWidth,
                Height = textHeight
            };
            this._TxtMonitoringPath.Style |= WindowStylesConst.WS_BORDER;

            this._BnSelectMonitoringPath = new NativeButton
            {
                Location = new Point(rightLeft + inputWidth + 1, top),
                Width = 25,
                Height = textHeight,
                Text = "…"
            };
            this._BnSelectMonitoringPath.Clicked += BnSelectMonitoringPath_Click;

            top += 30;
            this._ChkCgiMoitoring = new NativeCheckBox
            {
                Location = new Point(leftLeft, top),
                Width = lblWidth,
                Height = textHeight,
                Text = "CGI-Monitoring"

            };
            top += 30;
            NativeLabel lblCgiMonitoriingPath = new NativeLabel
            {
                Location = new Point(leftLeft, top),
                Width = lblWidth,
                Height = textHeight,
                Text = "CGI-Mon.-Path:",
                BackColor = this.BackColor
            };

            this._TxtCgiMonitoringPath = new NativeTextBox
            {
                Location = new Point(rightLeft, top),
                Width = inputWidth,
                Height = textHeight
            };
            this._TxtCgiMonitoringPath.Style |= WindowStylesConst.WS_BORDER;

            this._BnSelectCgiMonitoringPath = new NativeButton
            {
                Location = new Point(rightLeft + inputWidth + 1, top),
                Width = 25,
                Height = textHeight,
                Text = "…"
            };
            this._BnSelectCgiMonitoringPath.Clicked += BnSelectCgiMonitoringPath_Click;

            top += 30;

            NativeLabel lblCgiExeFile = new NativeLabel
            {
                Location = new Point(leftLeft, top),
                Width = lblWidth,
                Height = textHeight,
                Text = "CGI-EXE:",
                BackColor = this.BackColor
            };
            this._TxtCgiExeFile = new NativeTextBox
            {
                Location = new Point(rightLeft, top),
                Width = inputWidth,
                Height = textHeight
            };
            this._TxtCgiExeFile.Style |= WindowStylesConst.WS_BORDER;

            this._BnSelectCgiExeFile = new NativeButton
            {
                Location = new Point(rightLeft + inputWidth + 1, top),
                Width = 25,
                Height = textHeight,
                Text = "…"
            };
            this._BnSelectCgiExeFile.Clicked += BnSelectCgiExeFile_Clicked;
            top += 30;
            NativeLabel lblCgiFileExtension = new NativeLabel
            {
                Location = new Point(leftLeft, top),
                Width = lblWidth,
                Height = textHeight,
                Text = "CGI-Extensions:",
                BackColor = this.BackColor
            };
            this._TxtCgiFileExtensions = new NativeTextBox
            {
                Location = new Point(rightLeft, top),
                Width = inputWidth,
                Height = textHeight
            };
            this._TxtCgiFileExtensions.Style |= WindowStylesConst.WS_BORDER;
            top += 30;
            NativeLabel lblCgiMonitoringUrl= new NativeLabel
            {
                Location = new Point(leftLeft, top),
                Width = lblWidth,
                Height = textHeight,
                Text = "CGI-Mon. Url:",
                BackColor = this.BackColor
            };

            this._TxtCgiMonitoringUrl = new NativeTextBox
            {
                Location = new Point(rightLeft, top),
                Width = inputWidth,
                Height = textHeight
            };
            this._TxtCgiMonitoringUrl.Style |= WindowStylesConst.WS_BORDER;

            top += 30;
            this._LinkButton = new NativeLink
            {
                Location = new Point(leftLeft, top),
                Width = inputWidth -100,
                Height = textHeight,
                Text = "WebView2=><A ID=\"DLB\" HREF=\"https://go.microsoft.com/fwlink/p/?LinkId=2124703\">Download Bootstrap</A> or <A ID=\"DLL\" HREF=\"https://developer.microsoft.com/de-de/microsoft-edge/webview2/#download-section\">GoTo Webview2 Download</A>",
                ForeColor = CoreWindowsWrapper.Tools.ColorTool.Blue
            };

            this._LinkButton.LinkClicked += OnLinkClick;
            this._BnOk = new NativeButton
            {
                Width = 80,
                Height = 25,
                Location = new Point(this.Width - 33 - 81, top),
                Text = "&Ok"
            };
            this._BnOk.Clicked += BnOk_Clicked;
            this._BnCancel = new NativeButton
            {
                Width = 80,
                Height = 25,
                Location = new Point(this._BnOk.Left - 10 - 80, top),
                Text = "&Cancel"
            };
            this._BnCancel.Clicked += BnCancel_Clicked;
            top += 80;
            this.Height = top;
            this.Controls.Add(this._BnOk);
            this.Controls.Add(this._ChkContextMenu);
            this.Controls.Add(this._ChkDevTools);
            this.Controls.Add(this._ChkStatusBar);
            this.Controls.Add(this._ChkAppStatusBar);
            this.Controls.Add(this._ChkTopMost);
            this.Controls.Add(this._ChkMaximized);
            this.Controls.Add(this._ChkDisableF4);
            this.Controls.Add(this._ChkEnableF4WithPw);
            this.Controls.Add(this._TxtDisableF4Password);
            this.Controls.Add(lblTitle);
            this.Controls.Add(this._TxtTitle);
            this.Controls.Add(lblUrl);
            this.Controls.Add(this._TxtUrl);
            this.Controls.Add(lblUserFolder);
            this.Controls.Add(this._TxtUserDataFolder);
            this.Controls.Add(this._BnSelectUserDataFolder);
            this.Controls.Add(lblBrowserExecutable);
            this.Controls.Add(this._TxtBrowserExecutable);
            this.Controls.Add(this._BnSelectBrowserExecutable);
            this.Controls.Add(this._ChkMonitoring);
            this.Controls.Add(lblMonitoriingUrl);
            this.Controls.Add(this._TxtMonitoringUlr);
            this.Controls.Add(lblMonitoriingPath);
            this.Controls.Add(this._TxtMonitoringPath);
            this.Controls.Add(this._BnSelectMonitoringPath);
            this.Controls.Add(this._ChkCgiMoitoring);
            this.Controls.Add(lblCgiMonitoriingPath);
            this.Controls.Add(this._TxtCgiMonitoringPath);
            this.Controls.Add(this._BnSelectCgiMonitoringPath);
            this.Controls.Add(lblCgiExeFile);
            this.Controls.Add(this._TxtCgiExeFile);
            this.Controls.Add(this._BnSelectCgiExeFile);
            this.Controls.Add(lblCgiFileExtension);
            this.Controls.Add(_TxtCgiFileExtensions);
            this.Controls.Add(lblCgiMonitoringUrl);
            this.Controls.Add(_TxtCgiMonitoringUrl);
            
            this.Controls.Add(this._LinkButton);
            this.Controls.Add(this._BnCancel);
        }

        private void BnSelectCgiExeFile_Clicked(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "*.*\0*.*\0*.exe\0*.exe",
                DefaultFilterIndex = 2,
                Title = "Select CGI-EXE"
                
            };
            if (ofd.Show(this))
            {
                this._TxtCgiExeFile.Text = ofd.File;
            }
        }

        private void BnSelectCgiMonitoringPath_Click(object sender, EventArgs e)
        {
            OpenFolderDialog ofd = new OpenFolderDialog
            {
                Caption = "Select CGI-Monitoring Path"
            };
            if (ofd.Show(this))
            {
                this._TxtCgiMonitoringPath.Text = ofd.SelectedPath;
            }
        }

        private void OnLinkClick(object sender, NativeLinkClickEventArgs e)
        {
            var p = new ProcessStartInfo(e.Url)
            {
                UseShellExecute = true
            };
            Process.Start(p);
        }

        private void BnSelectBrowserExecutable_Clicked(object sender, EventArgs e)
        {
            OpenFolderDialog ofd = new OpenFolderDialog
            {
                Caption = "Select Browser-Executable-Folder Path"
            };
            if (ofd.Show(this))
            {
                this._TxtBrowserExecutable.Text = ofd.SelectedPath;
            }
        }

        private void BnSelectUserDataFolder_Clicked(object sender, EventArgs e)
        {
            OpenFolderDialog ofd = new OpenFolderDialog
            {
                Caption = "Select User-Data-Folder Path"
            };
            if (ofd.Show(this))
            {
                this._TxtUserDataFolder.Text = ofd.SelectedPath;
            }
        }

        private void BnOk_Clicked(object sender, EventArgs e)
        {
            ViewToOptions();
            this.Result = true;
            this.Close();
        }

        private void BnCancel_Clicked(object sender, EventArgs e)
        {
            this.Result = false;
            this.Close();
        }

        protected override void OnCreate(CreateEventArgs e)
        {
            base.OnCreate(e);
            OptionsToView();
            this.Result = false;
        }

        private void BnSelectMonitoringPath_Click(object sender, EventArgs e)
        {
            OpenFolderDialog ofd = new OpenFolderDialog
            {
                Caption = "Select Monitoring Path"
            };
            if (ofd.Show(this))
            {
                this._TxtMonitoringPath.Text = ofd.SelectedPath;
            }
        }
    }
}