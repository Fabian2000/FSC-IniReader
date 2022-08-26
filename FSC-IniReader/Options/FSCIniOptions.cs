using System.Text.RegularExpressions;

namespace FSC_IniReader
{
    public class FSCIniOptions
    {
        /// <summary>
        /// Removes lines matching to the RegEx. Can be used for comments. Default RegEx: ^#|^;|^\[;|^\[#
        /// </summary>
        public Regex IgnoreLines { get; set; } = new Regex(@"^#|^;|^\[;|^\[#");
    }
}
