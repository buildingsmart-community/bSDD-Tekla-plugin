using Bsdd.Core.Logic.Services;
using Bsdd.Core.Logic.UI.Services;
using BsddTeklaFeature.Logic.UI.BsddBridge;
using BsddTeklaFeature.Logic.UI.View;
using System.Windows;
using System.Windows.Controls;
using static Tekla.Structures.Filtering.Categories.AssemblyFilterExpressions;

namespace BsddTeklaFeature
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : UserControl
    {

        private BsddBridgeData _bsddBridgeData;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void InitializeServices()
        {
            var serviceFactory = new ServiceFactory();
            GlobalServiceFactory.Factory = serviceFactory;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            BrowserContainer.Children.Add(new BsddSearch(_bsddBridgeData));
        }
    }
}
