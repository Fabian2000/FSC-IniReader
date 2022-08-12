using FSC_IniReader.Exceptions;

namespace FSC_IniReader
{
    public class FSCIniSection
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

        public string Name { get => _sectionName; internal set { _sectionName = value; } }

        private FSCIniKey? GetIniKey(string key)
        {
            return _iniKeys.Find(curKey => curKey?.Value?.Equals(key, StringComparison.OrdinalIgnoreCase) ?? false);
        }

        public void Add(string key, string value)
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
        }

        public bool Delete(string key)
        {
            return _iniKeys.Remove(GetIniKey(key) ?? new FSCIniKey());
        }

        public bool HasKey(string key)
        {
            return GetIniKey(key) != null;
        }

        public List<FSCIniKey> GetAllKeys()
        {
            return _iniKeys;
        }
    }
}
