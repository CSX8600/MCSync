using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using MesaSuite.Common.Extensions;
using Newtonsoft.Json;

namespace MesaSuite.Common
{
    public class UserPreferences
    {
        private static UserPreferences _instance = null;

        [JsonProperty("sections")]
        public Dictionary<string, Dictionary<string, object>> Sections { get; set; } = new Dictionary<string, Dictionary<string, object>>();
        public Dictionary<string, object> GetPreferencesForSection(string section)
        {
            return Sections.GetOrSetDefault(section, new Dictionary<string, object>());
        }

        public static UserPreferences Get(bool forceReload = false)
        {
            if (_instance == null || forceReload)
            {
                _instance = new UserPreferences();
                _instance.Load();
            }

            return _instance;
        }

        private UserPreferences() { }

        private void Load()
        {
            if (!File.Exists("userpreferences.json"))
            {
                Save();

                return;
            }

            JsonSerializer serializer = new JsonSerializer();
            using (StreamReader reader = new StreamReader("userpreferences.json"))
            using (JsonTextReader jsonReader = new JsonTextReader(reader))
            {
                try
                {
                    serializer.Populate(jsonReader, this);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("An error occurred trying to read your userpreferences.json: " + ex.Message + "\r\n\r\nIt will not be loaded.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                jsonReader.Close();
            }
        }

        public void Save()
        {
            JsonSerializer serializer = new JsonSerializer();
            using (StreamWriter writer = new StreamWriter("userpreferences.json"))
            using (JsonTextWriter jsonWriter = new JsonTextWriter(writer))
            {
                serializer.Serialize(jsonWriter, this);
                jsonWriter.Flush();
                jsonWriter.Close();
            }
        }
    }
}
