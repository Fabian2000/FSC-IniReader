using System.ComponentModel;
using System.Text.RegularExpressions;

namespace FSC_IniReader.Future
{
    public sealed class FSCIniOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public FSCIniOptions()
        {
            // Do nothing ...
        }

        /// <summary>
        /// Detects and ignores lines that match this regex
        /// </summary>
        public Regex DetectComments { internal get; set; } = new Regex("""^#|^;|^\[;|^\[#""");

        /// <summary>
        /// If true, the ToString method will add empty lines before each section
        /// </summary>
        public bool UseNewLineBeforeSection { internal get; set; } = true;

        /// <summary>
        /// Sets the direction for sorting the data
        /// </summary>
        public ListSortDirection SortDirection { internal get; set; } = ListSortDirection.Ascending;

        /// <summary>
        /// If true, the sections may be empty
        /// </summary>
        public bool AllowSavingEmptySections { internal get; set; } = false;

        /// <summary>
        /// If true, ToString will return the ini data like key = val and not key=val
        /// </summary>
        public bool BeautifyWithSpaces { internal get; set; } = true;
    }
}
