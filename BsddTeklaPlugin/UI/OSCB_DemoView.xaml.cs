using BsddTeklaPlugin.Viewmodels;
using System;
using System.IO;
using System.Windows.Input;
//using System.Windows.Shapes;
using Tekla.Structures;
using Tekla.Structures.Datatype;
using Tekla.Structures.Dialog;

namespace BsddTeklaPlugin.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class OSCB_DemoView : PluginWindowBase
    {
        public DemoViewViewModel DemoViewViewModel;

        public OSCB_DemoView(DemoViewViewModel demoViewViewModel)
        {
            InitializeComponent();
            //UIControl.Content = new OSCB_DemoPage(demoViewViewModel);

            this.DemoViewViewModel = demoViewViewModel;


            try
            {
                string xsDir = string.Empty;
                TeklaStructuresSettings.GetAdvancedOption("XS_DIR", ref xsDir);
                Dialogs.SetSettings(string.Empty);
                this.Localization.Language = (string)Settings.GetValue("language");
                string fullPath = Path.Combine(xsDir, "messages", "DotAppsStrings", "WPFPluginTemplate.ail");
                if (File.Exists(fullPath))
                {
                    this.Localization.LoadAilFile(fullPath);
                }
            }
            catch (Exception)
            {
                //Log something...

            }



        }




        private void WpfOkApplyModifyGetOnOffCancel_ApplyClicked(object sender, System.EventArgs e)
        {
            this.Apply();
        }

        private void WpfOkApplyModifyGetOnOffCancel_CancelClicked(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void WpfOkApplyModifyGetOnOffCancel_GetClicked(object sender, System.EventArgs e)
        {
            this.Get();
        }

        private void WpfOkApplyModifyGetOnOffCancel_ModifyClicked(object sender, System.EventArgs e)
        {
            this.Modify();
        }

        private void WpfOkApplyModifyGetOnOffCancel_OkClicked(object sender, System.EventArgs e)
        {
            this.Apply();
            this.Close();
        }

        private void WpfOkApplyModifyGetOnOffCancel_OnOffClicked(object sender, System.EventArgs e)
        {
            this.ToggleSelection();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void WpfSaveLoad_AttributesSaveClicked(object sender, EventArgs e)
        {
            string name = saveload.SaveAsText;
            DemoViewViewModel.SaveJson(name);
        }

        private void WpfSaveLoad_HelpOpenClicked(object sender, EventArgs e)
        {
            DemoViewViewModel.OpenHelpFile.Execute(sender);
        }
    }
}
