using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace FSC_IniReader.Future
{
    public sealed partial class FSCIni
    {
        private List<FSCIniSection> _sections = new();
        private FSCIniOptions _options = new FSCIniOptions();

        private void IniBuilder(string iniData)
        {
            iniData = iniData.Trim();

            var data = Regex.Split(iniData, @"\r\n|\n|\r");

            var lastSection = "NONE";

            foreach ( var line in data)
            {
                if (_options.DetectComments.IsMatch(line))
                {
                    continue;
                }

                if (line.StartsWith("[") && line.EndsWith("]"))
                {
                    lastSection = line.Replace("[", "");
                    lastSection = lastSection.Replace("]", "");
                    lastSection = lastSection.Trim();

                    Add(lastSection);
                }
                else if (line.Contains("="))
                {
                    if (lastSection == "NONE")
                    {
                        Add(lastSection);
                    }

                    var keyValue = Regex.Match(line, "^(.+?)=(.+)$").Groups;
                    var key = keyValue[1].Value;
                    key = key.Trim();

                    var value = keyValue[2].Value;
                    value = value.Trim();

                    var section = this[lastSection];
                    section.Add(key, value);
                }
            }
        }

        private bool VerifySection(string section)
        {
            return !string.IsNullOrWhiteSpace(section) && !Regex.Unescape(section).Contains("""\""");
        }
    }
}
