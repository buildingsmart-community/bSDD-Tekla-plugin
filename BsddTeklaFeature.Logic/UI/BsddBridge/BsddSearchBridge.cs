using Bsdd.Core.Logic.IfcJson;
using Newtonsoft.Json;
using NLog;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using static BsddTeklaFeature.Logic.UI.View.BsddSearch;

namespace BsddTeklaFeature.Logic.UI.BsddBridge
{

    public class BsddSearchBridge
    {

 
        private static Page _bsddSearchParent;
        private static BsddBridgeData _bsddBridgeData;


        public BsddSearchBridge(BsddBridgeData bsddBridgeData) // Modify this line
        {
            _bsddBridgeData = bsddBridgeData;
            

           
        }
        public void SetParentWindow(Page bsddSearchParent)
        {
            _bsddSearchParent = bsddSearchParent;
        }
        /// <summary>
        /// Saves the data returned from the bSDD API.
        /// </summary>
        /// <param name="ifcJsonData">The form data to save.</param>
        /// <returns>The response from the bSDD API.</returns>
        public string save(string ifcJsonData)
        {

            Logger logger = LogManager.GetCurrentClassLogger();

            logger.Info($"SAVE: Trying to save ifcJsonData to Element: {ifcJsonData}");

            var converter = new IfcJsonConverter();

            // Deserialize the JSON data into an IfcData object using the IfcDataConverter
            var ifcEntity = JsonConvert.DeserializeObject<IfcEntity>(ifcJsonData, converter);

           

            //_bsddSearchParent.Dispatcher.Invoke(() => _bsddSearchParent.Close());

            

            // Return the serialized JSON data for the IfcData object
            return JsonConvert.SerializeObject(ifcEntity);

        }
        /// <summary>
        /// Closes UI 
        /// </summary>
        /// <returns>The response from the bSDD API.</returns>
        public void cancel()
        {
            //Close UI
            //_bsddSearchParent.Dispatcher.Invoke(() => _bsddSearchParent.Close());

        }
        ////public string loadSettings ()
        ////{
        ////    return JsonConvert.SerializeObject(_bsddBridgeData);
        ////}
        /// <summary>
        /// Loads BsddBridgeData.
        /// </summary>
        public string loadBridgeData()
        {
            return JsonConvert.SerializeObject(_bsddBridgeData);
        }
    }

}
