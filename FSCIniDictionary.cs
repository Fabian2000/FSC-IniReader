using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSC_IniReader
{
    public class FSCIniDictionary
    {
        private string _filename = String.Empty;
        
        /// <summary>
        /// The FSCIniDictionary class let you use the ini file as dictionary. !Important! Comments are not included
        /// </summary>
        /// <param name="filename"></param>
        public FSCIniDictionary(string filename)
        {
            _filename = filename;

            using (FSCIniStream fscIniStream = new FSCIniStream(filename))
            {
                IniFile.Add("", new Dictionary<string, string>());

                foreach (string key in fscIniStream.GetAllKeys("", false))
                {
                    IniFile[""].Add(key, fscIniStream.Read(key).AsString);
                }

                foreach (string section in fscIniStream.GetAllSections(false))
                {
                    IniFile.Add(section, new Dictionary<string, string>());

                    foreach (string key in fscIniStream.GetAllKeys(section, false))
                    {
                        IniFile[section].Add(key, fscIniStream.Read(key, section).AsString);
                    }
                }
            }
        }

        /// <summary>
        /// Returns the IniFile as Dictionary
        /// </summary>
        public Dictionary<string, Dictionary<string, string>> IniFile { get; set; } = new Dictionary<string, Dictionary<string, string>>();

        /// <summary>
        /// Saves the changes from the IniFile property
        /// </summary>
        /// <param name="cleanFormat"></param>
        public void Save(bool cleanFormat = true)
        {
            StringBuilder sb = new StringBuilder();

            foreach (string section in IniFile.Keys)
            {
                if (section != "")
                {
                    sb.AppendLine($"[{section}]");
                }

                foreach (string key in IniFile[section].Keys)
                {
                    sb.AppendLine(key + "=" + IniFile[section][key]);
                }

                if (cleanFormat)
                {
                    sb.AppendLine();
                }
            }

            File.WriteAllText(_filename, sb.ToString());
        }
    }
}
