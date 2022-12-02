using System;
using System.Collections.Generic;
using System.Linq;

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
                return new FSCIniSection();
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

        }

        /// <summary>
        /// Merges two ini classes to one
        /// </summary>
        /// <param name="ini">A second ini class for the merge process</param>
        /// <param name="overwrite">If a key in a section exists for both, the second ini key will overwrite the first one (if true)</param>
        public void Merge(FSCIni ini, bool overwrite)
        {

        }

        /// <summary>
        /// Splits the ini class into a list of ini classes
        /// </summary>
        /// <param name="afterXSections">The section index after that the split should happen (After a split, the counter jumps to 0 and counts again)</param>
        /// <returns>A list of multiple ini classes</returns>
        public List<FSCIni> Split(int afterXSections)
        {
            return new();
        }

        /// <summary>
        /// Splits the ini class into a list of ini classes
        /// </summary>
        /// <param name="afterSection">The section name after that the split should happen</param>
        /// <returns>A list of multiple ini classes</returns>
        public List<FSCIni> Split(string afterSection)
        {
            return new();
        }

        /// <summary>
        /// Splits the ini class into a list of ini classes
        /// </summary>
        /// <param name="afterSections">The section names after that the split should happen</param>
        /// <returns>A list of multiple ini classes</returns>
        public List<FSCIni> Split(params string[] afterSections)
        {
            return new();
        }

        /// <summary>
        /// Deletes a section
        /// </summary>
        /// <param name="section">The name of a section</param>
        public void Delete(string section)
        {

        }

        /// <summary>
        /// Checks if a section exists
        /// </summary>
        /// <param name="key">The name of a section</param>
        /// <returns>Returns true, if the section was found</returns>
        public bool HasSection(string section)
        {
            return false;
        }

        /// <summary>
        /// Changes the name of a section to a new name
        /// </summary>
        /// <param name="oldSection">Old name</param>
        /// <param name="newSection">New name</param>
        public void Rename(string oldSection, string newSection)
        {

        }

        /// <summary>
        /// Get all sections
        /// </summary>
        /// <returns>Returns all names of the sections</returns>
        public List<string> GetAllSections()
        {
            return new();
        }

        /// <summary>
        /// Gets a new ini class with all given sections
        /// </summary>
        /// <param name="sections">An array of all sections for the extraction</param>
        /// <returns>Returns a new ini class</returns>
        public FSCIni Extract(params string[] sections)
        {
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

        public static FSCIni operator -(FSCIni ini1, FSCIni ini2)
        {
            return new();
        }

        /// <summary>
        /// Returns the FSCIni class as Ini data for saving it for example as ini file
        /// </summary>
        /// <returns>Returns an ini data string</returns>
        public override string ToString()
        {
            return string.Empty;
        }
    }
}
