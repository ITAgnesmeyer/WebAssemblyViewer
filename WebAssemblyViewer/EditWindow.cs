using CoreWindowsWrapper;
using Diga.Core.Api.Win32;
using System;

namespace WebAssemblyViewer
{
    class EditWindow : NativeWindow
    {
        private NativeCheckBox _ChkMonitoring;
        private NativeTextBox _TxtMonitoriingUlr;
        private NativeTextBox _TxtUserDataFolder;
        private NativeButton _BnSelectUserDataFolder;
        private NativeTextBox _TxtBrowserExecutable;
        private NativeButton _BnSelectBrowserExecutable;
        private NativeTextBox _TxtTitle;
        private NativeCheckBox _ChkStatusBar;
        private NativeTextBox _TxtMonitoringPath;
        private NativeTextBox _TxtUrl;
        private NativeCheckBox _ChkDevTools;
        private NativeCheckBox _ChkContextMenu;
        private NativeButton _BnSelectMonitoringPath;
        private BrowserOpetions _Options;
        private NativeButton _BnOk;
        private NativeButton _BnCancel;

        public bool Result{get;private set;}
        public EditWindow(BrowserOpetions options) : base()
        {
            this._Options = options;

        }

        private void ViewToOptions()
        {
            this._Options.Monitoring = this._ChkMonitoring.Checked;
            this._Options.MointoringUrl = this._TxtMonitoriingUlr.Text;
            this._Options.MonitoringPath = this._TxtMonitoringPath.Text;
            this._Options.Title = this._TxtTitle.Text;
            this._Options.Url = this._TxtUrl.Text;
            this._Options.DevToolsEnable = this._ChkDevTools.Checked;
            this._Options.ContextMenuEnable = this._ChkContextMenu.Checked;
            this._Options.StatusBar = this._ChkStatusBar.Checked;
            this._Options.BrowserUserDataFolder = this._TxtUserDataFolder.Text;
            this._Options.BrowserExecutableFolder = this._TxtBrowserExecutable.Text;

        }
        private void OptionsToView()
        {
            this._ChkMonitoring.Checked = this._Options.Monitoring;
            this._TxtMonitoriingUlr.Text = this._Options.MointoringUrl;
            this._TxtMonitoringPath.Text = this._Options.MonitoringPath;
            this._TxtUserDataFolder.Text = this._Options.BrowserUserDataFolder;
            this._TxtBrowserExecutable.Text = this._Options.BrowserExecutableFolder;
            this._TxtTitle.Text = this._Options.Title;
            this._TxtUrl.Text = this._Options.Url;
            this._ChkDevTools.Checked = this._Options.DevToolsEnable;
            this._ChkContextMenu.Checked = this._Options.ContextMenuEnable;
            this._ChkStatusBar.Checked = this._Options.StatusBar;

        }
        protected override void OnBeforeCreate(BeforeWindowCreateEventArgs e)
        {
            e.Styles.Style = WindowStylesConst.WS_DLGFRAME | WindowStylesConst.WS_SYSMENU;
        }
        protected override void InitControls()
        {
            this.Text = "Configuration Editor";
            this.Width = 500;
            this.Height = 430;
            this.StartUpPosition = WindowsStartupPosition.CenterScreen;

            int top = 10;
            int leftLeft = 10;
            int rightLeft = 120;


            this._ChkContextMenu = new NativeCheckBox
            {
                Location = new Point(leftLeft, top),
                Width = 100,
                Height = 20,
                Text = "Context-Menu",
                
                
            };
            this._ChkContextMenu.Style |= WindowStylesConst.WS_TABSTOP;
            this._ChkDevTools = new NativeCheckBox
            {
                Location = new Point(rightLeft, top),
                Width = 100,
                Height = 20,
                Text = "Dev-Tools"
                
            };

            this._ChkStatusBar = new NativeCheckBox
            {
                Location = new Point(rightLeft + 110, top),
                Width = 100,
                Height = 20,
                Text = "Statusbar"
                
            };

            top += 30;
            
            NativeLabel lblTitle = new NativeLabel
            {
                Location = new Point(leftLeft, top), 
                Width = 100, 
                Height = 20, 
                Text ="Title:", 
                BackColor = this.BackColor
            };

            this._TxtTitle = new NativeTextBox
            {
                Location = new Point(rightLeft, top),
                Width = 300,
                Height = 20,
            };
            this._TxtTitle.Style |= WindowStylesConst.WS_BORDER;


            top += 30;
            
            
            NativeLabel lblUrl = new NativeLabel
            {
                Location = new Point(leftLeft, top),
                Width = 100,
                Height = 20,
                Text = "Url:",
                BackColor = this.BackColor
            };

            this._TxtUrl = new NativeTextBox
            {
                Location = new Point(rightLeft, top),
                Width = 300,
                Height = 20

            };
            this._TxtUrl.Style |= WindowStylesConst.WS_BORDER;


            top += 30;
            NativeLabel lblUserFolder = new NativeLabel
            {
                Location = new Point(leftLeft, top),
                Width = 100,
                Height = 20,
                Text = "Data-Folder:",
                BackColor = this.BackColor
            };

            this._TxtUserDataFolder = new NativeTextBox
            {
                Location = new Point(rightLeft, top),
                Width = 300,
                Height = 20
            };
            this._TxtUserDataFolder.Style |= WindowStylesConst.WS_BORDER;

            this._BnSelectUserDataFolder = new NativeButton
            {
                Location = new Point(rightLeft + 301, top),
                Width = 25,
                Height = 20,
                Text = "…"
            };
            this._BnSelectUserDataFolder.Clicked += BnSelectUserDataFolder_Clicked;

            top += 30;
            NativeLabel lblBrowserExecutable = new NativeLabel
            {
                Location = new Point(leftLeft, top),
                Width = 100,
                Height = 20,
                Text = "Browser-Folder:",
                BackColor = this.BackColor
            };

            this._TxtBrowserExecutable = new NativeTextBox
            {
                Location = new Point(rightLeft, top),
                Width = 300,
                Height = 20
            };
            this._TxtBrowserExecutable.Style |= WindowStylesConst.WS_BORDER;

            this._BnSelectBrowserExecutable = new NativeButton
            {
                Location = new Point(rightLeft + 301, top),
                Width = 25,
                Height = 20,
                Text = "…"
            };
            this._BnSelectBrowserExecutable.Clicked += BnSelectBrowserExecutable_Clicked;

            top += 30;
            this._ChkMonitoring =  new NativeCheckBox
            {
                Location = new Point(leftLeft, top),
                Width = 100,
                Height = 20,
                Text = "Monitoring"
            };

            top += 30;
            NativeLabel lblMonitoriingUrl = new NativeLabel
            {
                Location = new Point(leftLeft, top), 
                Width = 100, 
                Height = 20, 
                Text = "Monitoring-Url:", 
                BackColor= this.BackColor

            };
            this._TxtMonitoriingUlr = new NativeTextBox
            {
                Location=new Point(rightLeft,top),
                Width = 300,
                Height = 20
            };
            this._TxtMonitoriingUlr.Style |= WindowStylesConst.WS_BORDER;

            top += 30;
            
            
            NativeLabel lblMonitoriingPath = new NativeLabel
            {
                Location = new Point(leftLeft, top),
                Width = 100,
                Height = 20,
                Text = "Monitoring-Path:",
                BackColor = this.BackColor
            };

            this._TxtMonitoringPath = new NativeTextBox
            {
                Location = new Point(rightLeft, top),
                Width = 300,
                Height = 20
                
            };
            this._TxtMonitoringPath.Style |= WindowStylesConst.WS_BORDER;

            this._BnSelectMonitoringPath = new NativeButton
            {
                Location = new Point(rightLeft + 301, top),
                Width = 25,
                Height = 20,
                Text = "…"
            };
            this._BnSelectMonitoringPath.Clicked+= BnSelectMonitoringPath_Click;

            top += 30;
            this._BnOk = new NativeButton
            {
                Width = 100,
                Height = 30,
                Location = new Point(500 - 30 - 100,top ),
                Text = "&Ok"
                

            };
            this._BnOk.Clicked += BnOk_Clicked;
            this._BnCancel = new NativeButton
            {
                Width = 100,
                Height = 30,
                Location = new Point(this._BnOk.Left - 10 - 100, top),
                Text = "&Cancel"
            };
            this._BnCancel.Clicked += BnCancel_Clicked;
            top += 80;
            this.Height = top;
            this.Controls.Add(this._BnOk);
            this.Controls.Add(this._ChkContextMenu);
            this.Controls.Add(this._ChkDevTools);
            this.Controls.Add(this._ChkStatusBar);
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
            this.Controls.Add(this._TxtMonitoriingUlr);
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