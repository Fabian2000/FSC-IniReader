using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using static System.Collections.Specialized.BitVector32;

namespace FSC_IniReader.Future
{
    public sealed partial class FSCIni
    {
        /// <summary>
        /// Creates a new instance of FSCIni
        /// </summary>
        public FSCIni()
        {
            // Do nothing ...
        }

        /// <summary>
        /// Creates a new instance of FSCIni
        /// </summary>
        /// <param name="iniData">Only the data, no file path. You can use File.ReadAllText for example to get the data</param>
        public FSCIni(string iniData)
        {
            IniData = iniData;
        }

        /// <summary>
        /// Creates a new instance of FSCIni
        /// </summary>
        /// <param name="iniData">Only the data, no file path. You can use File.ReadAllText for example to get the data</param>
        /// <param name="options">Options to manipulate the behavior of the ini reader</param>
        public FSCIni(string iniData, Action<FSCIniOptions>? options)
        {
            IniData = iniData;
            SetOptions(options);
        }

        /// <summary>
        /// Only the data, no file path. You can use File.ReadAllText for example to get the data
        /// </summary>
        public string IniData { set => IniBuilder(value); }

        /// <summary>
        /// Gets the section with all key value pairs
        /// </summary>
        /// <param name="section">The name of the section</param>
        /// <returns>Returns a section object</returns>
        public FSCIniSection this[string section]
        {
            get
            {
                return HasSection(section) ? _sections[_sections.FindIndex(param => param.Name == section)] : new FSCIniSection();
            }
        }

        /// <summary>
        /// Sets the options of the ini reader
        /// </summary>
        /// <param name="options">Options to manipulate the behavior of the ini reader</param>
        public void SetOptions(Action<FSCIniOptions>? options)
        {
            options?.Invoke(_options);
        }

        /// <summary>
        /// Adds a new section to the ini class
        /// </summary>
        /// <param name="section">The name of the section</param>
        public void Add(string section)
        {
            if (HasSection(section))
            {
                return;
            }

            if (!VerifySection(section))
            {
                return;
            }

            section = section.Replace("[", "").Replace("]", "");

            var newSection = new FSCIniSection();
            newSection.Name = section;

            _sections.Add(newSection);
        }

        /// <summary>
        /// Merges two ini classes to one
        /// </summary>
        /// <param name="ini">A second ini class for the merge process</param>
        /// <param name="overwrite">If a key in a section exists for both, the second ini key will overwrite the first one (if true)</param>
        public void Merge(FSCIni ini, bool overwrite)
        {
            foreach (var section in ini.GetAllSections())
            {
                Add(section);

                foreach (var key in ini[section].GetAllKeys())
                {
                    if (this[section].Add(key, ini[section][key]))
                    {
                        continue;
                    }

                    if (overwrite)
                    {
                        this[section][key] = ini[section][key];
                    }
                }
            }
        }

        /// <summary>
        /// Deletes a section
        /// </summary>
        /// <param name="section">The name of a section</param>
        public void Delete(string section)
        {
            if (!HasSection(section) && _sections is null)
            {
                return;
            }

            _sections.Remove(this[section]);
        }

        /// <summary>
        /// Checks if a section exists
        /// </summary>
        /// <param name="section">The name of a section</param>
        /// <returns>Returns true, if the section was found</returns>
        public bool HasSection(string section)
        {
            return _sections?.Find(prop => prop.Name == section) is not null;
        }

        /// <summary>
        /// Changes the name of a section to a new name
        /// </summary>
        /// <param name="oldSection">Old name</param>
        /// <param name="newSection">New name</param>
        public void Rename(string oldSection, string newSection)
        {
            if (!HasSection(oldSection))
            {
                return;
            }

            if (!VerifySection(oldSection))
            {
                return;
            }

            oldSection = oldSection.Replace("[", "").Replace("]", "");

            this[oldSection].Name = newSection;
        }

        /// <summary>
        /// Create a copy of the current instance
        /// </summary>
        /// <returns>Returns a new instance of FSCIni</returns>
        public FSCIni Copy()
        {
            var ini = new FSCIni();
            ini.SetOptions(options => options = _options);
            ini.Merge(this, false);
            return ini;
        }

        /// <summary>
        /// Get all sections
        /// </summary>
        /// <returns>Returns all names of the sections</returns>
        public List<string> GetAllSections()
        {
            if (IsEmpty())
            {
                return new List<string>();
            }

            return new List<string>(from section in _sections select section.Name);
        }

        /// <summary>
        /// Gets a new ini class with all given sections
        /// </summary>
        /// <param name="sections">An array of all sections for the extraction</param>
        /// <returns>Returns a new ini class</returns>
        public FSCIni Extract(params string[] sections)
        {
            var ini = Copy();

            foreach (var section in ini.GetAllSections())
            {
                if (sections.Contains(section))
                {
                    continue;
                }

                ini.Delete(section);
            }

            return new();
        }

        /// <summary>
        /// This removes all sections including key value pair from the ini class
        /// </summary>
        public void Clear()
        {
            _sections.Clear();
        }

        /// <summary>
        /// Checks if the ini class contains any section
        /// </summary>
        /// <returns>Returns true, if empty</returns>
        public bool IsEmpty()
        {
            return !_sections.Any();
        }

        /// <summary>
        /// Use FSCIni like String and add your ini data like this FSCIni ini = "...";
        /// </summary>
        /// <param name="iniData"></param>
        public static implicit operator FSCIni(string iniData)
        {
            return new FSCIni(iniData);
        }

        /// <summary>
        /// This merges two FSCIni classes together with overwrite false
        /// </summary>
        /// <param name="ini1"></param>
        /// <param name="ini2"></param>
        /// <returns>Returns a FSCIni class</returns>
        public static FSCIni operator +(FSCIni ini1, FSCIni ini2)
        {
            var ini = new FSCIni();
            ini.Merge(ini1, true);
            ini.Merge(ini2, false);
            return ini;
        }

        /// <summary>
        /// Returns the FSCIni class as Ini data for saving it for example as ini file
        /// </summary>
        /// <returns>Returns an ini data string</returns>
        public override string ToString()
        {
            _sections.Sort();

            if (_options.SortDirection == ListSortDirection.Descending)
            {
                _sections.Reverse();
            }

            foreach (var section in _sections)
            {
                section.Sort(_options.SortDirection);
            }

            var stringBuilder = new StringBuilder();

            foreach (var section in _sections)
            {
                if (section.IsEmpty() && !_options.AllowSavingEmptySections)
                {
                    continue;
                }

                if (_options.UseNewLineBeforeSection)
                {
                    stringBuilder.AppendLine();
                }

                stringBuilder.AppendLine($"[{section.Name}]");

                foreach (var key in section.GetAllKeys())
                {
                    if (_options.BeautifyWithSpaces)
                    {
                        stringBuilder.AppendLine($"{key} = {section[key]}");
                    }
                    else
                    {
                        stringBuilder.AppendLine($"{key}={section[key]}");
                    }
                }
            }

            return stringBuilder.ToString().Trim();
        }
    }
}
