using FSC_IniReader.Exceptions;
using System.Text;

namespace FSC_IniReader
{
    public class FSCIni
    {
        private List<FSCIniSection> _iniSections = new List<FSCIniSection>();

        public FSCIni(string iniContent)
        {
            var lines = iniContent.Split("\n").ToList();

            for (var i = 0; i < lines.Count; i++)
            {
                lines[i] = lines[i].Trim();

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

        public bool Delete(string section)
        {
            section = section == string.Empty ? "NULL" : section;
            return _iniSections.Remove(GetIniSection(section) ?? new FSCIniSection());
        }

        public bool HasSection(string section)
        {
            section = section == string.Empty ? "NULL" : section;
            return GetIniSection(section) != null;
        }

        public List<FSCIniSection> GetAllSections()
        {
            return _iniSections;
        }

        public override string ToString()
        {
            return ToString(false);
        }

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