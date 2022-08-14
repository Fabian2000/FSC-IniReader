using FSC_IniReader.Exceptions;

namespace FSC_IniReader
{
    public class FSCIniSection : IComparable<FSCIniSection>
    {
        private List<FSCIniKey> _iniKeys = new List<FSCIniKey>();
        private string _sectionName = string.Empty;

        public string? this[string key]
        {
            get => GetIniKey(key)?.Value;
            set
            {
                var keyValue = GetIniKey(key);

                if (keyValue == null)
                {
                    throw new FSCIniException("Key not found");
                }
                else if (string.IsNullOrWhiteSpace(value))
                {
                    throw new FSCIniException("Key may not be null, empty or whitespace");
                }
                else
                {
                    keyValue.Value = value;
                }
            }
        }

        /// <summary>
        /// Contains the name of the current section
        /// </summary>
        public string Name { get => _sectionName; internal set { _sectionName = value; } }

        private FSCIniKey? GetIniKey(string key)
        {
            return _iniKeys.Find(curKey => curKey?.Key?.Equals(key, StringComparison.OrdinalIgnoreCase) ?? false);
        }

        /// <summary>
        /// Adds a new key value pair to a section
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="FSCIniException"></exception>
        public FSCIniKey Add(string key, string value)
        {
            if (string.IsNullOrWhiteSpace(key) && key != string.Empty)
            {
                throw new FSCIniException("Key may not be null or  whitespace");
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new FSCIniException("Value may not be null, empty or whitespace");
            }

            if (!HasKey(key))
            {
                var keyValue = new FSCIniKey()
                {
                    Key = key.Trim(),
                    Value = value.Trim()
                };

                _iniKeys.Add(keyValue);

                _iniKeys.Sort();
            }

            return GetIniKey(key) ?? new FSCIniKey();
        }

        /// <summary>
        /// Deletes a key value pair from a section
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Delete(string key)
        {
            return _iniKeys.Remove(GetIniKey(key) ?? new FSCIniKey());
        }

        /// <summary>
        /// Asks, if a key value pair exists in a section
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool HasKey(string key)
        {
            return GetIniKey(key) != null;
        }

        /// <summary>
        /// Returns all key value pairs from a section
        /// </summary>
        /// <returns></returns>
        public List<FSCIniKey> GetAllKeys()
        {
            return _iniKeys;
        }

        public int CompareTo(FSCIniSection? other)
        {
            return Name.CompareTo(other?.Name);
        }
    }
}
