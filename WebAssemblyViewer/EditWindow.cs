using CoreWindowsWrapper;
using Diga.Core.Api.Win32;
using System;

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
        private NativeTextBox _TxtMonitoringUlr;
        private NativeTextBox _TxtUserDataFolder;
        private NativeButton _BnSelectUserDataFolder;
        private NativeTextBox _TxtBrowserExecutable;
        private NativeButton _BnSelectBrowserExecutable;
        private NativeTextBox _TxtTitle;

        private NativeTextBox _TxtMonitoringPath;
        private NativeTextBox _TxtUrl;

        private NativeButton _BnSelectMonitoringPath;
        private BrowserOptions _Options;
        private NativeButton _BnOk;
        private NativeButton _BnCancel;

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

            this.Controls.Add(this._BnCancel);
        }

        private void BnSelectBrowserExecutable_Clicked(object sender, EventArgs e)
        {
            OpenFolderDialog ofd = new OpenFolderDialog();
            ofd.Caption = "Select Browser-Executable-Folder Path";
            if (ofd.Show(this))
            {
                this._TxtBrowserExecutable.Text = ofd.SelectedPath;
            }
        }

        private void BnSelectUserDataFolder_Clicked(object sender, EventArgs e)
        {
            OpenFolderDialog ofd = new OpenFolderDialog();
            ofd.Caption = "Select User-Data-Folder Path";
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
            OpenFolderDialog ofd = new OpenFolderDialog();
            ofd.Caption = "Select Monitoring Path";
            if (ofd.Show(this))
            {
                this._TxtMonitoringPath.Text = ofd.SelectedPath;
            }
        }
    }
}