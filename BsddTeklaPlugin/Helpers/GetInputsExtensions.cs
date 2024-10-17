using System.Collections.Generic;
using Tekla.Structures;
using Tekla.Structures.Model;
using static Tekla.Structures.Plugins.PluginBase;

namespace BsddTeklaPlugin.Helpers
{
    public static class GetInputsExtensions
    {
        public static List<Part> GetMultipleModelparts(this List<InputDefinition> input, int numberOfParts)
        {
            List<Part> partList = new List<Part>();

            if (input.Count == numberOfParts)
            {

                for (int i = 0; i < numberOfParts; i++)
                {
                    object DataObject = input[i].GetInput();
                    if (input[i].GetInput().GetType() == typeof(Identifier))
                    {
                        Part part = new Model().SelectModelObject(input[i].GetInput() as Identifier) as Part;
                        partList.Add(part);
                    }

                }
            }

            return partList;
        }

        public static List<Part> GetAllModelParts(this List<InputDefinition> input)
        {
            List<Part> partList = new List<Part>();



            for (int i = 0; i < input.Count; i++)
            {
                object DataObject = input[i].GetInput();
                if (input[i].GetInput().GetType() == typeof(Identifier))
                {
                    Part part = new Model().SelectModelObject(input[i].GetInput() as Identifier) as Part;
                    partList.Add(part);
                }

            }


            return partList;
        }

        public static Part GetModelPart(this List<InputDefinition> input)
        {
            return new Model().SelectModelObject(input[0].GetInput() as Identifier) as Part;
        }

    }
}
