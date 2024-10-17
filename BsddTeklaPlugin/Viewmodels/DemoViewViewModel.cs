using BsddTeklaPlugin.Base;
using BsddTeklaPlugin.Plugin;
using Fusion;
using Newtonsoft.Json;
using System.IO;
using System.Reflection;
using System.Windows.Input;
using Tekla.Structures.Dialog;
using Tekla.Structures.Model;
using Tekla.Structures.Plugins;
using TSD = Tekla.Structures.Datatype;

namespace BsddTeklaPlugin.Viewmodels
{
    public class DemoViewViewModel : ViewModel
    {
        [StructuresField("Typeverbinding")]
        [JsonIgnore]
        public string typeverbinding;

        [StructuresDialog("Typeverbinding", typeof(TSD.String))]
        public string Presetname
        {
            get { return typeverbinding; }
            set
            {
                SetValue(ref this.typeverbinding, value);
            }
        }

        [JsonIgnore]
        private ICommand openHelpFile;

        [JsonIgnore]
        public ICommand OpenHelpFile
        {
            get { return openHelpFile; }
            set
            {
                SetValue(ref this.openHelpFile, value);
            }
        }

        public DemoViewViewModel()
        {
            this.OpenHelpFile = new RelayCommand(OnOpenHelpFileClicked);
        }

        private void OnOpenHelpFileClicked(object obj)
        {
            System.Windows.MessageBox.Show("Help file");
            //or Process.Start("http://helplink");
        }


        public void SaveJson(string name)
        {
            string saveName = name;
            if (name == "")
            {
                saveName = "NoName";
            }
            string filename = System.IO.Path.Combine(new Model().GetInfo().ModelPath, "attributes", saveName + $".{typeof(DemoViewViewModel).Namespace}" + $".{BaseData.PLUGIN_WINDOW_NAME}" + ".json");


            using (StreamWriter file = File.CreateText(filename))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, this);


            }
        }

        public DemoViewViewModel LoadJson(string name)
        {
            string saveName = name;
            if (name != "")
            {
                string filename = System.IO.Path.Combine(new Model().GetInfo().ModelPath, "attributes", saveName + $".{BaseData.PLUGIN_NAME}" + ".json");

                var jsonString = File.ReadAllText(filename);
                var jsonViewModel = JsonConvert.DeserializeObject<DemoViewViewModel>(jsonString);

                return jsonViewModel;
            }
            else
            {
                return new DemoViewViewModel();
            }

        }

        public void LoadJsonAndUpdateProperties(string name)
        {
            var jsonViewModel = LoadJson(name);

            foreach (PropertyInfo info in GetType().GetProperties())
            {
                if (info.CanWrite)
                {
                    object value = info.GetValue(jsonViewModel);

                    info.SetValue(this, value, null);
                }
            }
        }

    }
}
