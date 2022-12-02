using System.ComponentModel;
using System.Text.RegularExpressions;

namespace FSC_IniReader.Future
{
    public sealed class FSCIniOptions
    {
        public FSCIniOptions()
        {
            // Do nothing ...
        }

        public Regex DetectComments { internal get; set; } = new Regex("""^#|^;|^\[;|^\[#""");

        public bool UseNewLineBeforeSection { internal get; set; } = false;

        public ListSortDirection SortDirection { internal get; set; } = ListSortDirection.Ascending;

        public bool AllowSavingEmptySections { internal get; set; } = false;
    }
}
