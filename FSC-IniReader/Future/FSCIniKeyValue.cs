using System;

namespace FSC_IniReader.Future
{
    internal sealed class FSCIniKeyValue : IComparable<FSCIniKeyValue>
    {
        internal string Key { get; set; } = string.Empty;
        internal FSCIniTypes Value { get; set; } = string.Empty;

        public int CompareTo(FSCIniKeyValue? other)
        {
            return Key.CompareTo(other?.Key);
        }
    }
}
