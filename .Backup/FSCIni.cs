using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSC_IniReader
{
    public class FSCIni
    {
        public static (string AsString, bool AsBool, short AsShort, int AsInt, long AsLong, float AsFloat, double AsDouble) Read(string filename, string key, string section = null)
        {
            using (FSCIniStream fscIniStream = new FSCIniStream(filename))
            {
                return fscIniStream.Read(key, section);
            }
        }

        public static bool KeyExists(string filename, string key, string section = null)
        {
            using (FSCIniStream fscIniStream = new FSCIniStream(filename))
            {
                return fscIniStream.KeyExists(key, section);
            }
        }

        public static bool SectionExists(string filename, string section)
        {
            using (FSCIniStream fscIniStream = new FSCIniStream(filename))
            {
                return fscIniStream.SectionExists(section);
            }
        }

        public static void Write<T>(string filename, T content, string key, string section = null)
        {
            using (FSCIniStream fscIniStream = new FSCIniStream(filename))
            {
                fscIniStream.Write(content, key, section);
            }
        }

        public static bool DeleteKey(string filename, string key, string section = null)
        {
            using (FSCIniStream fscIniStream = new FSCIniStream(filename))
            {
                return fscIniStream.DeleteKey(key, section);
            }
        }

        public static bool DeleteSection(string filename, string section)
        {
            using (FSCIniStream fscIniStream = new FSCIniStream(filename))
            {
                return fscIniStream.DeleteSection(section);
            }
        }

        public static List<string> GetAllSections(string filename)
        {
            using (FSCIniStream fscIniStream = new FSCIniStream(filename))
            {
                return fscIniStream.GetAllSections();
            }
        }

        public static List<string> GetAllKeys(string filename, string section = "")
        {
            using (FSCIniStream fscIniStream = new FSCIniStream(filename))
            {
                return fscIniStream.GetAllKeys(section);
            }
        }
    }
}
