using System.Collections.Generic;
using Tekla.Structures.Model;
using static Tekla.Structures.Plugins.PluginBase;

namespace BsddTeklaPlugin.Helpers
{
    public class GetInputs
    {

        public Part GetModelPart(List<InputDefinition> input)
        {
            return input.GetModelPart();
        }

        public List<Part> GetMultipleModelParts(List<InputDefinition> input, int numberOfParts)
        {
            return input.GetMultipleModelparts(numberOfParts);
        }

        public List<Part> GetAllModelParts(List<InputDefinition> input)
        {
            return input.GetAllModelParts();
        }



    }
}
