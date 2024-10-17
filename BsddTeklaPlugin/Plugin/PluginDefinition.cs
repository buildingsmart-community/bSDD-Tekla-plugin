using BsddTeklaPlugin.Helpers;
using BsddTeklaPlugin.Viewmodels;
using System.Collections.Generic;
using Tekla.Structures.Model;
using Tekla.Structures.Plugins;

namespace BsddTeklaPlugin.Plugin
{
    [Plugin(BaseData.PLUGIN_NAME)]
    [PluginUserInterface("BsddTeklaPlugin.UI." + BaseData.PLUGIN_WINDOW_NAME)]
    public class PluginDefinition : PluginBase
    {

        #region Private Helpers

        private DefineInputs DefineInputs { get; set; } = new DefineInputs();
        private GetInputs GetInputs { get; set; } = new GetInputs();

        private PluginInitiator PluginInitiator { get; set; } = new PluginInitiator();
        #endregion


        #region Private Fields

        private string _PresetName;

        #endregion

        #region Properties

        public Model Model { get; set; }
        public DemoViewViewModel Data { get; set; }

        #endregion

        public PluginDefinition(DemoViewViewModel data)
        {
            Model = new Model();
            Data = data;
        }

        #region Overrides
        public override List<InputDefinition> DefineInput()
        {
            return DefineInputs.SelectModelPart();
        }

        public override bool Run(List<InputDefinition> Input)
        {
            GetValuesFromDialog();
            return PluginInitiator.Run(Input);
        }
        #endregion

        #region Private methods
        private void GetValuesFromDialog()
        {
            _PresetName = Data.Presetname;
            if (IsDefaultValue(_PresetName)) _PresetName = "";
        }

        #endregion
    }
}
