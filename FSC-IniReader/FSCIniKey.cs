namespace FSC_IniReader
{
    public class FSCIniKey : IComparable<FSCIniKey>
    {
        public string Key { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;

        public int CompareTo(FSCIniKey? other)
        {
            return Key.CompareTo(other?.Key);
        }
    }
}