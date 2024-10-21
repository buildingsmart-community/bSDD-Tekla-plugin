using BsddTeklaFeature.Logic.IfcJson;
using Newtonsoft.Json;
using System.Collections.Generic;
using Tekla.Structures.Model;

namespace BsddTeklaFeature.Logic.UI.BsddBridge
{
    public static class GlobalSelection
    {
        // This list will store the last selected elements
        public static Dictionary<string, List<ModelObject>> LastSelectedElementsWithDocs { get; private set; } = new Dictionary<string, List<ModelObject>>();

    }
    public static class GlobalModel
    {
        public static Model currentModel;
    }
    public static class GlobalBsddSettings
    {
        public static BsddSettings bsddsettings = new BsddSettings();
    }
    public class BsddDictionary
    {
        [JsonProperty("ifcClassification")]
        public IfcClassification IfcClassification { get; set; }

        [JsonProperty("parameterMapping")]
        public string ParameterMapping { get; set; }
    }

    public class BsddSettings
    {
        [JsonProperty("bsddApiEnvironment")]
        public string BsddApiEnvironment { get; set; } = "production";

        [JsonProperty("mainDictionary")]
        public BsddDictionary MainDictionary { get; set; }

        [JsonProperty("ifcDictionary")]
        public BsddDictionary IfcDictionary { get; set; }

        [JsonProperty("filterDictionaries")]
        public List<BsddDictionary> FilterDictionaries { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("includeTestDictionaries")]
        public bool IncludeTestDictionaries { get; set; }
    }

    public class BsddBridgeData
    {
        [JsonProperty("settings")]
        public BsddSettings Settings { get; set; }

        [JsonProperty("ifcData")]
        public List<IfcEntity> IfcData { get; set; }
    }
}
