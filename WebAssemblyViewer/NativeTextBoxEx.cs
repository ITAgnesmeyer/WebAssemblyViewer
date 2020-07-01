using CoreWindowsWrapper;
using Diga.Core.Api.Win32;

namespace WebAssemblyViewer
{
    class NativeTextBoxEx : NativeTextBox
    {
        protected override void Initialize()
        {
            base.Initialize();
            base.Style |= EditBoxStyles.ES_AUTOHSCROLL;
        }
    }
}