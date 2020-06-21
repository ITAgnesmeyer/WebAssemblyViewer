using CoreWindowsWrapper;
using CoreWindowsWrapper.Tools;
using Diga.Core.Api.Win32;
using System;

namespace WebAssemblyViewer
{
    class EditWindow : NativeWindow
    {
        private NativeCheckBox _ChkMonitoring;
        private NativeTextBox _TxtMonitoriingUlr;
        private NativeTextBox _TxtTitle;
        private NativeCheckBox _ChkStatusBar;
        private NativeTextBox _TxtMonitoringPath;
        private NativeTextBox _TxtUrl;
        private NativeCheckBox _ChkDevTools;
        private NativeCheckBox _ChkContextMenu;
        private NativeButton _BnSelectMonitoringPath;
        protected override void OnBeforeCreate(BeforeWindowCreateEventArgs e)
        {
            e.Styles.Style = WindowStylesConst.WS_DLGFRAME | WindowStylesConst.WS_SYSMENU;
        }
        protected override void InitControls()
        {
            this.Text = "Configuration Editor";
            this.Width = 500;
            this.Height = 400;
            this.StartUpPosition = WindowsStartupPosition.CenterScreen;

            int top = 10;
            int leftLeft = 10;
            int rightLeft = 120;


            this._ChkContextMenu = new NativeCheckBox
            {
                Location = new Point(leftLeft, top),
                Width = 100,
                Height = 20,
                Text = "Context-Menu"
            };

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

            top += 60;
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
            
           


            this.Controls.Add(lblMonitoriingUrl);
            this.Controls.Add(this._TxtMonitoriingUlr);
            this.Controls.Add(lblTitle);
            this.Controls.Add(this._TxtTitle);
            this.Controls.Add(lblUrl);
            this.Controls.Add(this._TxtUrl);
            this.Controls.Add(lblMonitoriingPath);
            this.Controls.Add(this._TxtMonitoringPath);
            this.Controls.Add(this._ChkContextMenu);
            this.Controls.Add(this._ChkDevTools);
            this.Controls.Add(this._ChkMonitoring);
            this.Controls.Add(this._ChkStatusBar);
            this.Controls.Add(this._BnSelectMonitoringPath);
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