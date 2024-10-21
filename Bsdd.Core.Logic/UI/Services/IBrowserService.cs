using System.Windows;

namespace Bsdd.Core.Logic.UI.Services
{
    public interface IBrowserService
    {
        void LoadUrl(string url);
        void RegisterJsObject(string name, object objectToBind, bool isAsync = false);
        void ExecuteScriptAsync(string script);
        event DependencyPropertyChangedEventHandler IsBrowserInitializedChanged;
        bool IsBrowserInitialized { get; }
        void ShowDevTools();
        string Address { get; set; }
        ICustomBrowserControl BrowserInstance { get; }
        object BrowserControl { get; }
    }
}
