using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FSC_IniReader
{
    public partial class FSCIniStream
    {
        // Removes the [] from a section to get the value
        private string _GetSectionValue(string value)
        {
            return value.Substring(1, value.Length - 2);
        }

        private bool _IsEmptyOrSpace(string text)
        {
            if (text == String.Empty || text == null)
            {
                return true;
            }

            foreach (char c in text)
            {
                if (c != ' ')
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Returns the value of the given key and / or section as string, bool, short, int, long, float or double
        /// </summary>
        /// <param name="key"></param>
        /// <param name="section"></param>
        /// <returns></returns>
        public (string AsString, bool AsBool, short AsShort, int AsInt, long AsLong, float AsFloat, double AsDouble) Read(string key, string section = null)
        {
            if (_IsEmptyOrSpace(section))
            {
                section = null;
            }

            List<string> sections = null;
            if (section == null)
            {
                sections = _iniFileNoSections;
            }
            else
            {
                if (SectionExists(section))
                {
                    sections = _iniFileSections[section];
                }
            }

            string str = String.Empty;

            if (KeyExists(key, section))
            {
                str = sections.Where(item => item.StartsWith(key + "=")).First();
                str = str.Substring(str.IndexOf('=') + 1, str.Length - str.IndexOf('=') - 1);
            }

            bool b = str == "1" || str.ToLower() == "true" ? true : false;

            long l;
            int i;
            short s;

            string tempString = str.Replace(".", ",");

            if (long.TryParse(tempString, out l))
            {
                i = (int)l;
                s = (short)i;
            }
            else
            {
                l = 0;
                i = 0;
                s = 0;
            }

            float f;
            double d;

            if (double.TryParse(tempString, out d))
            {
                f = (float)d;
            }
            else
            {
                d = 0;
                f = 0;
            }

            return (str, b, s, i, l, f, d);
        }

        /// <summary>
        /// Checks, if a key is available
        /// </summary>
        /// <param name="key"></param>
        /// <param name="section"></param>
        /// <returns></returns>
        public bool KeyExists(string key, string section = null)
        {
            if (_IsEmptyOrSpace(section))
            {
                section = null;
            }

            List<string> sections = null;
            if (section == null)
            {
                sections = _iniFileNoSections;
            }
            else
            {
                if (SectionExists(section))
                {
                    sections = _iniFileSections[section];
                }
                else
                {
                    return false;
                }
            }

            foreach (string str in sections)
            {
                if (str.StartsWith(key + "="))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks, if a section is available
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
        public bool SectionExists(string section)
        {
            if (_IsEmptyOrSpace(section))
            {
                section = null;
            }

            return _iniFileSections.ContainsKey(section);
        }

        /// <summary>
        /// Writes a new key with data into the given section
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content"></param>
        /// <param name="key"></param>
        /// <param name="section"></param>
        public void Write<T>(T content, string key, string section = null)
        {
            if (_IsEmptyOrSpace(section))
            {
                section = null;
            }

            string contentString = content.ToString();

            if (typeof(T) == typeof(bool))
            {
                if (contentString == "1" || contentString.ToLower() == "true")
                {
                    contentString = "true";
                }
                else
                {
                    contentString = "false";
                }
            }
            else if (typeof(T) == typeof(float) || typeof(T) == typeof(double))
            {
                contentString = contentString.Replace(".", ",");
            }

            if (section == null)
            {
                if (KeyExists(key))
                {
                    _iniFileNoSections[_GetIndexOfKey(key, _iniFileNoSections)] = key + "=" + contentString;
                }
                else
                {
                    _iniFileNoSections.Add(key + "=" + contentString);
                }
            }
            else
            {
                if (!SectionExists(section))
                {
                    _iniFileSections.Add(section, new List<string>());
                }

                if (KeyExists(key, section))
                {
                    _iniFileSections[section][_GetIndexOfKey(key, _iniFileSections[section])] = key + "=" + contentString;
                }
                else
                {
                    _iniFileSections[section].Add(key + "=" + contentString);
                }
            }
        }

        // Returns the index of a key in the given list
        private int _GetIndexOfKey(string key, List<string> list)
        {
            int count = 0;
            
            foreach (string str in list)
            {
                if (str.StartsWith(key + "="))
                {
                    return count;
                }

                count++;
            }

            return -1;
        }

        /// <summary>
        /// Deletes a key from the ini file
        /// </summary>
        /// <param name="key"></param>
        /// <param name="section"></param>
        /// <returns></returns>
        public bool DeleteKey(string key, string section = null)
        {
            if (_IsEmptyOrSpace(section))
            {
                section = null;
            }

            if (KeyExists(key, section))
            {
                if (section == null)
                {
                    _iniFileNoSections.Remove(_iniFileNoSections.Where(item => item.StartsWith(key + "=")).First());

                    return true;
                }
                else
                {
                    _iniFileSections[section].Remove(_iniFileSections[section].Where(item => item.StartsWith(key + "=")).First());

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Deletes a section from the ini file
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
        public bool DeleteSection(string section)
        {
            if (_IsEmptyOrSpace(section))
            {
                section = null;
            }

            if (SectionExists(section))
            {
                _iniFileSections.Remove(section);

                return !_iniFileSections.ContainsKey(section);
            }

            return false;
        }
    }
}
