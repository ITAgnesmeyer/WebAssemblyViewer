using System.Collections.Generic;

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
            set
            {
                if (this._Args.ContainsKey("/e"))
                {
                    if (!value)
                    {
                        this._Args.Remove("/e");
                    }
                }
                else
                {
                    if (value)
                    {
                        this._Args.Add("/e", "");
                    }
                }
            }
        }
    }
}