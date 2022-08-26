using FSC_IniReader.Exceptions;
using System.Text;

namespace FSC_IniReader
{
    public class FSCIni
    {
        private List<FSCIniSection> _iniSections = new List<FSCIniSection>();

        /// <summary>
        /// Creates a class from an ini file
        /// </summary>
        /// <param name="iniContent">A string that contains the data from the ini file [Section]\r\nkeyValue=something\r\n...</param>
        public FSCIni(string iniContent) : this(iniContent, new FSCIniOptions())
        {
        }

        /// <summary>
        /// Creates a class from an ini file
        /// </summary>
        /// <param name="iniContent">A string that contains the data from the ini file [Section]\r\nkeyValue=something\r\n...</param>
        /// <param name="options">Defines some little extra possibilities to setup the ini reader</param>
        public FSCIni(string iniContent, FSCIniOptions options)
        {
            var lines = iniContent.Split("\n").ToList();

            for (var i = 0; i < lines.Count; i++)
            {
                lines[i] = lines[i].Trim();

                if (options.IgnoreLines.IsMatch(lines[i]))
                {
                    continue;
                }

                if (!lines[i].Contains("=") && !(lines[i].StartsWith("[") && lines[i].EndsWith("]")))
                {
                    lines.Remove(lines[i]);

                    if (lines.Count > 0)
                    {
                        i--;
                    }

                    continue;
                }
            }

            var lastSection = "NULL";

            foreach (var line in lines)
            {
                if (options.IgnoreLines.IsMatch(line))
                {
                    continue;
                }

                if (line.Contains("="))
                {
                    var iniKeyValue = new FSCIniKey()
                    {
                        Key = line.Substring(0, line.IndexOf('=')),
                        Value = line.Substring(line.IndexOf('=') + 1, line.Length - line.IndexOf('=') - 1)
                    };

                    if (!HasSection(lastSection))
                    {
                        Add(lastSection);
                    }

                    var section = GetIniSection(lastSection);
                    section?.Add(iniKeyValue.Key, iniKeyValue.Value);
                }
                else if (line.Contains("[") && line.Contains("]"))
                {
                    var section = line.Replace("[", "");
                    section = section.Replace("]", "");

                    Add(section);

                    lastSection = section;
                }
            }
        }

        /// <summary>
        /// Returns a section
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
        /// <exception cref="FSCIniException"></exception>
        public FSCIniSection? this[string section]
        {
            get => GetIniSection(section == string.Empty ? "NULL" : section);
            set
            {
                var iniSection = GetIniSection(section == string.Empty ? "NULL" : section);

                if (iniSection == null)
                {
                    throw new FSCIniException("Section not found");
                }
                else
                {
                    iniSection = value;
                }
            }
        }

        private FSCIniSection? GetIniSection(string section)
        {
            return _iniSections.Find(curSection => curSection?.Name?.Equals(section, StringComparison.OrdinalIgnoreCase) ?? false);
        }

        /// <summary>
        /// Adds a new section
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
        /// <exception cref="FSCIniException"></exception>
        public FSCIniSection Add(string section)
        {
            section = section == string.Empty ? "NULL" : section;

            if (string.IsNullOrWhiteSpace(section) && section != string.Empty)
            {
                throw new FSCIniException("Section may not be null or whitespace");
            }

            if (!HasSection(section))
            {
                var iniSection = new FSCIniSection()
                {
                    Name = section.Trim(),
                };

                _iniSections.Add(iniSection);

                _iniSections.Sort();
            }

            return this[section] ?? new FSCIniSection();
        }

        /// <summary>
        /// Deletes a section
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
        public bool Delete(string section)
        {
            section = section == string.Empty ? "NULL" : section;
            return _iniSections.Remove(GetIniSection(section) ?? new FSCIniSection());
        }

        /// <summary>
        /// Asks, if a section exists
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
        public bool HasSection(string section)
        {
            section = section == string.Empty ? "NULL" : section;
            return GetIniSection(section) != null;
        }

        /// <summary>
        /// Returns all sections
        /// </summary>
        /// <returns></returns>
        public List<FSCIniSection> GetAllSections()
        {
            return _iniSections;
        }

        /// <summary>
        /// Creates a string from the ini class (Export method)
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ToString(false);
        }

        /// <summary>
        /// Creates a string from the ini class (Export method)
        /// </summary>
        /// <param name="minify">If true, there will be no extra lines when a new section starts</param>
        /// <returns></returns>
        public string ToString(bool minify)
        {
            var stringBuilder = new StringBuilder();

            foreach (var iniSection in GetAllSections())
            {
                stringBuilder.AppendLine($"[{iniSection.Name}]");

                foreach (var iniKeyValue in iniSection.GetAllKeys())
                {
                    stringBuilder.AppendLine($"{iniKeyValue.Key}={iniKeyValue.Value}");
                }

                if (!minify)
                {
                    stringBuilder.AppendLine();
                }
            }

            var result = stringBuilder.ToString();

            if (result.EndsWith(Environment.NewLine))
            {
                while (result.Contains(Environment.NewLine + Environment.NewLine))
                {
                    result = result.Replace(Environment.NewLine + Environment.NewLine, Environment.NewLine);
                }

                result = result.Substring(0, result.Length - Environment.NewLine.Length);
            }

            return result;
        }
    }
}