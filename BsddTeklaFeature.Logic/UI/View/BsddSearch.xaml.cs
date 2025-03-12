using Bsdd.Core.Logic.UI.Services;
using BsddTeklaFeature.Logic.UI.BsddBridge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BsddTeklaFeature.Logic.UI.View
{
    /// <summary>
    /// Interaction logic for BsddSearch.xaml
    /// </summary>
    public partial class BsddSearch : Page
    {
        private readonly IBrowserService _browserService;

        public BsddSearch(BsddBridgeData bsddBridgeData)
        {
            _browserService = GlobalServiceFactory.Factory.CreateBrowserService();
            InitializeComponent();

            _browserService.Address = "https://buildingsmart-community.github.io/bSDD-filter-UI/v1.6.0/bsdd_search/";

            var bridgeSearch = new BsddSearchBridge(bsddBridgeData);
            bridgeSearch.SetParentWindow(this);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BrowserContainer.Children.Add((UIElement)_browserService.BrowserControl);
        }
    }
}
