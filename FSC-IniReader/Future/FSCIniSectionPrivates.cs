using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace FSC_IniReader.Future
{
    public sealed partial class FSCIniSection
    {
        private List<FSCIniKeyValue> _keyValues = new();

        private bool VerifyKey(string key)
        {
            return (
                !string.IsNullOrWhiteSpace(key) &&
                !Regex.Unescape(key).Contains("""\""") &&
                !key.Contains("=")
            );
        }
    }
}
