using Bsdd.Core.Logic.UI.Services;
using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Bsdd.Core.Logic.Services
{
    public class BrowserService : IBrowserService
    {
        private readonly ICustomBrowserControl customBrowserControl;
        private readonly CefSharp.Wpf.ChromiumWebBrowser chromiumWebBrowser;


        public BrowserService()
        {
            this.customBrowserControl = new BrowserControl();
            this.chromiumWebBrowser = (this.customBrowserControl as BrowserControl)?.ChromiumWebBrowser;
        }

        public ICustomBrowserControl BrowserInstance
        {
            get { return this.customBrowserControl; }
        }

        public object ChromiumWebBrowser
        {
            get { return this.chromiumWebBrowser; }
        }
        public object BrowserControl
        {
            get { return this.chromiumWebBrowser; }
        }

        public string Address
        {
            get { return this.chromiumWebBrowser.Address; }
            set { this.chromiumWebBrowser.Address = value; }
        }

        public void LoadUrl(string url)
        {
            this.chromiumWebBrowser.Address = url;
        }

        public void RegisterJsObject(string name, object objectToBind, bool isAsync = false)
        {
            this.chromiumWebBrowser.JavascriptObjectRepository.Register(name, objectToBind, isAsync);
        }

        public void ExecuteScriptAsync(string script)
        {
            this.chromiumWebBrowser.ExecuteScriptAsync(script);
        }
        public void ShowDevTools()
        {
            this.chromiumWebBrowser.ShowDevTools();
        }

        public event DependencyPropertyChangedEventHandler IsBrowserInitializedChanged
        {
            add { this.chromiumWebBrowser.IsBrowserInitializedChanged += value; }
            remove { this.chromiumWebBrowser.IsBrowserInitializedChanged -= value; }
        }

        public bool IsBrowserInitialized => this.chromiumWebBrowser.IsBrowserInitialized;
    }
}
