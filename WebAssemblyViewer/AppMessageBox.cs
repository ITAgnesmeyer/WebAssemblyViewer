using CoreWindowsWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreWindowsWrapper.Tools;
using Diga.Core.Api.Win32;


namespace WebAssemblyViewer
{
    class AppMessageBox:NativeWindow
    {
        private NativeLabel _LblMessage;
        private NativeButton _BnOk;
        private NativeButton _BnCancel;
        private NativeLabel _LblIcon;
        public MessageBoxResult Result{get;private set;}
        protected override void OnBeforeCreate(BeforeWindowCreateEventArgs e)
        {
            e.Styles.Style = WindowStylesConst.WS_DLGFRAME | WindowStylesConst.WS_SYSMENU ;
            e.Styles.StyleEx |= WindowStylesConst.WS_EX_TOPMOST | WindowStylesConst.WS_EX_DLGMODALFRAME;
        }
        protected override void InitControls()
        {
            this.Width = 400;
            this.Height = 200;
            this.Text = "Message";
            this.StartUpPosition = WindowsStartupPosition.CenterScreen;
            NativeLabel frameLabel = new NativeLabel
            {
                Location = new Point(0,0),
                Width = this.Width,
                Height = 110,
                BackColor = ColorTool.White

            };
           
            this._LblMessage = new NativeLabel
            {
                Location = new Point(80, 20),
                Width = this.Width - 110,
                Height = this.Height - 110,
                Text = "Enter your text\nEnter your Text",
                //BackColor = ColorTool.Gray,
                //ForeColor = ColorTool.White,
            };
            this._LblIcon = new NativeLabel
            {
                Location = new Point(10,this._LblMessage.Height / 2 - 25),
                Width = 50,
                Height = 50,
                Text = "",
                Font =new Font{Name="Material Icons",Size = 40},
                ForeColor = ColorTool.Rgb(24, 52, 75)
            };
            this._BnOk = new NativeButton
            {
                Location = new Point(this.Width - 30-80,120),
                Width = 80,
                Height = 25,
                Text = "&Ok"
            };
            this._BnOk.Clicked += Ok_Clicked;
            this._BnCancel = new NativeButton
            {
                Location = new Point(this._BnOk.Left - 10 - 80, 120),
                Width = 80,
                Height = 25,
                Text = "&Cancel"
            };
           
            this._BnCancel.Clicked += Cancel_Clicked;
            this.Controls.Add(frameLabel);
            this.Controls.Add(this._LblIcon);
            this.Controls.Add(this._LblMessage);
            this.Controls.Add(this._BnOk);
            this.Controls.Add(this._BnCancel);
        }

        private void Cancel_Clicked(object sender, EventArgs e)
        {
            this.Result = MessageBoxResult.Cancel;
            this.Close();
        }

        private void Ok_Clicked(object sender, EventArgs e)
        {
            this.Result = MessageBoxResult.Ok;
            this.Close();
        }

        public string Message
        {
            get => this._LblMessage.Text; set=> this._LblMessage.Text = value;
        }

        public string Caption
        {
            get => this.Text;
            set => this.Text = value;
        }
    }
}
