using System.Collections.Generic;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;
using static Tekla.Structures.Plugins.PluginBase;

namespace BsddTeklaPlugin.Helpers
{
    public class DefineInputs
    {

        /// <summary>
        /// Lets the user select 1 modelpart    
        /// </summary>
        /// <returns>InputDefinition for the plug-in</returns>
        public List<InputDefinition> SelectModelPart()
        {
            return SelectMultipleModelParts(1);
        }

        /// <summary>
        /// Lets the user select n modelparts
        /// </summary>
        /// <param name="numberOfParts">Number of SINGLE modelparts you want the user to select</param>
        /// <returns>InputDefinition for the plug-in</returns>
        public List<InputDefinition> SelectMultipleModelParts(int numberOfParts)
        {
            List<InputDefinition> inputList = new List<InputDefinition>();
            try
            {
                for (int i = 0; i < numberOfParts; i++)
                {

                    ModelObject part = new Picker().PickObject(Picker.PickObjectEnum.PICK_ONE_PART);
                    inputList.Add(new InputDefinition(part.Identifier));


                }
            }
            catch
            {

            }
            return inputList;
        }


    }
}
