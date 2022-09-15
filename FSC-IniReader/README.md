# FSC-IniReader
New, better, easier ...
The powerful crossplatform IniReader is back.

### What's new?
- Now ini files get delivered as a string. 
    - That makes it possible to files in your own choice of encoding and you can add ini content into your source code.
- The code is easier to use.
- The script is shorter.
- Upgrade to .Net6. + (Since 2.1.1 more targets supported)
- Key value pairs (without section) will now be added to section [NULL]

### How to use?
1. Create an ini string by reading a file or write it directly into the source code.
2. Create an instance of FSCIni with the ini parameter
```cs
using FSC_IniReader;

var content = "..."; // <- Ini file content

var ini = new FSCIni(content);
or
var ini = new FSCIni(content, new FSCIniOptions());
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
bool Delete(string key);
bool HasKey(string key);
List<FSCIniKey> GetAllKeys();
```

FSCIniOptions contains some extended customize possibilities.