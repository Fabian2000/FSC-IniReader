# FSC-IniReader
A crossplatform ini reader and writer written in .Net 5

Easy to use ini reader. Tested on Windows and Linux.

Static Methods:
```cs
public static (string AsString, bool AsBool, short AsShort, int AsInt, long AsLong, float AsFloat, double AsDouble) Read(string filename, string key, string section = null)
public static bool KeyExists(string filename, string key, string section = null)
public static bool SectionExists(string filename, string section)
public static void Write<T>(string filename, T content, string key, string section = null)
public static bool DeleteKey(string filename, string key, string section = null)
public static bool DeleteSection(string filename, string section)
public static List<string> GetAllSections(string filename)
public static List<string> GetAllKeys(string filename, string section = "")
```

Initial Methods
```cs
using (FSCIniStream ini = new FSCIniStream())
{
  ini.Read(...)
  ini.Write(...)
  ini.KeyExists(...)
  ini.SectionExists(...)
  ini.DeleteKey(...)
  ini.DeleteSection(...)
  ini.GetAllSections(...)
  ini.GetAllKeys(...)
  ini.Cancel // bool property
  ini.SetEmptyLineBeforeSection // bool property
  ini.GetStream()
  ini.Close()
  ini.Dispose()
}
```
It is important to use a using or dispose, otherwise the file will not save

Ini as Dictionary
```cs
FSCIniDictionary dictIni = new FSCIniDictionary();
dictIni.IniFile[""]["ini"] = "New Content";
dictIni.Save();

The first [ ] is the section and the second [ ] is the key. To get keys without section, leave the section empty [""]
```

---
Some features are missing or you found a bug? Message me on discord, every idea is welcome: Discord -> Fabian#3563 (Don't message me for fun!)
