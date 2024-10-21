using BsddTeklaFeature.Logic.UI.BsddBridge;
using System;
using System.Collections.Generic;
using NLog;
using Bsdd.Core.Log;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Bsdd.Core.Logic.UI.Services;
using Tekla.Structures.Model;

namespace BsddTeklaFeature.Logic.UI.Wrappers
{
    public class UpdateUIonSave
    {
        Logger logger = LogManager.GetCurrentClassLogger();

        private IBrowserService browser;

        public UpdateUIonSave(BsddSettings bsddSettings)
        {
            //UIDocument uidoc = uiapp.ActiveUIDocument;
            //Document doc = uidoc.Document;

            ////UpdateSettings(bsddSettings);
            //SettingsManager.SaveSettingsToGlobalParametersAndDataStorage(doc, bsddSettings);
            UpdateBsddLastSelection();


        }

        public void SetBrowser(IBrowserService browserObject)
        {
            browser = browserObject;
        }

        public void UpdateBsddLastSelection()
        {
            List<ModelObject> lastSelection = new List<ModelObject>();
            try
            {
                lastSelection = GlobalSelection.LastSelectedElementsWithDocs[GlobalModel.currentModelPath];

            }
            catch (Exception)
            {

            }
            //var jsonString = JsonConvert.SerializeObject(ElementsManager.SelectionToIfcJson(GlobalModel.currentModel, lastSelection));
            //var jsFunctionCall = $"updateSelection({jsonString});";

            //if (browser.IsBrowserInitialized)
            //{
            //    browser.ExecuteScriptAsync(jsFunctionCall);
            //}
        }

    }

}
