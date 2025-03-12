using Bsdd.Core.Logic.UI.Services;
using CefSharp.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Bsdd.Core.Logic.Services
{
    public class BrowserControl : Control, ICustomBrowserControl
    {
        public ChromiumWebBrowser ChromiumWebBrowser { get; private set; }

        public BrowserControl()
        {
            this.ChromiumWebBrowser = new ChromiumWebBrowser();
        }

        public void Navigate(string url)
        {
            this.ChromiumWebBrowser.Address = url;
        }

    }
}
