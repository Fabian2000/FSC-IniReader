﻿using FSC_IniReader.Exceptions;

namespace FSC_IniReader
{
    public class FSCIni
    {
        private List<FSCIniSection> _iniSections = new List<FSCIniSection>();

        public FSCIni(string ini)
        {
            // TODO: Ini To Class
        }

        public FSCIniSection? this[string section]
        {
            get => GetIniSection(section);
            set
            {
                var iniSection = GetIniSection(section);

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

        public void Add(string section)
        {
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
        }

        public bool Delete(string section)
        {
            return _iniSections.Remove(GetIniSection(section) ?? new FSCIniSection());
        }

        public bool HasSection(string section)
        {
            return GetIniSection(section) != null;
        }

        public List<FSCIniSection> GetAllSections()
        {
            return _iniSections;
        }

        public override string ToString()
        {
            // TODO: Add Class To Ini
            return "";
        }
    }
}