# FSC-IniReader
New, better, easier ...
The powerful crossplatform IniReader is back.

### What's new?
- Now ini files get delivered as a string. 
    - That makes it possible to files in your own choice of encoding and you can add ini content into your source code.
- The code is easier to use.
- The script is shorter.
- Upgrade to .Net6.
- Key value pairs (without section) will now be added to section [NULL]

### How to use?
1. Create an ini string by reading a file or write it directly into the source code.
2. Create an instance of FSCIni with the ini parameter
```cs
using FSC_IniReader;

var content = "..."; // <- Ini file content

var ini = new FSCIni(content);
or
var ini = new FSCIni(content, new FSCOptions());
```
3. Access the ini sections
```cs
ini["Section1"]
```
4. Access the keys to get the values
```cs
ini["Section1"]["Key1"]
```

Methods:
```cs
Section:
FSCIniSection Add(string section);
bool Delete(string section);
bool HasSection(string section);
List<FSCIniSection> GetAllSections();
string ToString()
string ToString(bool minify);

KeyValues:
FSCIniKey Add(string key, string value);
Delete(string key);
HasKey(string key);
List<FSCIniKey> GetAllKeys();
```

FSCOptions contains some extended customize possibilities.

# FSC-IniReader [OLD]
#### To get the old source, check out the .Backup folder
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
