using BsddTeklaFeature.Logic.UI.BsddBridge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BsddTeklaFeature.Logic.UI.Wrappers
{
    public class UpdateUIonSave
    {
        Logger logger = LogManager.GetCurrentClassLogger();

        private IBrowserService browser;

        public override void Execute(UIApplication uiapp, BsddSettings bsddSettings)
        {
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            //UpdateSettings(bsddSettings);
            SettingsManager.SaveSettingsToGlobalParametersAndDataStorage(doc, bsddSettings);
            UpdateBsddLastSelection();


        }

        public void SetBrowser(IBrowserService browserObject)
        {
            browser = browserObject;
        }

        public void UpdateBsddLastSelection()
        {
            List<ElementType> lastSelection = new List<ElementType>();
            try
            {
                lastSelection = GlobalSelection.LastSelectedElementsWithDocs[GlobalDocument.currentDocument.PathName];

            }
            catch (Exception)
            {

            }
            var jsonString = JsonConvert.SerializeObject(ElementsManager.SelectionToIfcJson(GlobalDocument.currentDocument, lastSelection));
            var jsFunctionCall = $"updateSelection({jsonString});";

            if (browser.IsBrowserInitialized)
            {
                browser.ExecuteScriptAsync(jsFunctionCall);
            }
        }

    }

}
