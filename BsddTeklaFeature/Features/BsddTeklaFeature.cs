using BsddTeklaFeature.Viewmodels;
using Fusion;
using System;
using System.ComponentModel;
using System.Windows;
using Tekla.Structures.Ui.WpfKit.Features;

namespace BsddTeklaFeature.Features
{
    [SharedResources("Resources/Icons.xaml")]
    [LocalizedResources("Locales/en-US.xaml")]
    [LocalizedResources("Locales/nl-NL.xaml", Culture = "nl-NL")]
    public class BsddTeklaFeature : App.Feature
    {
        private delegate void Panels_SetIsTabVisible(string viewName, bool tabVisible);

        [PublishedView("BsddTeklaFeature.MainToolPanel", ViewType = typeof(MainWindow))]
        [PublishedViewMetadata("Panel.Location", "Side")]
        [PublishedViewMetadata("Panel.HeaderTextKey", "Bsdd.Title")]
        [PublishedViewMetadata("Panel.HeaderGeometryKey", "Bsdd.Icon1")]
        [PublishedViewMetadata("Panel.IsVisible", true)]
        [PublishedViewMetadata("Panel.IsTabVisible", true)]
        public ViewModel CreateExamplePanel(object parameter)
        {
            return new BsddTeklaViewModel();
        }



        [PublishedMethod]
        public override void Starting(CancelEventArgs starting)
        {
 
            base.Starting(starting);
         
        }


        [PublishedMethod]
        public void Shell_OnModeChanged(TsMode mode)
        {
            try
            {
             this.Host.Invoke((Panels_SetIsTabVisible m) => m("BsddTeklaFeature.MainToolPanel", mode == TsMode.Modeling));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
