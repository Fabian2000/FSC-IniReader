using FSC_IniReader.Future;
using System.Text.RegularExpressions;

FSCIni ini = new FSCIni("", option =>
{
    option.DetectComments = new Regex("Hey");
});