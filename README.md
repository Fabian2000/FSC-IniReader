# FSC-IniReader
New, better, easier ...
The powerful crossplatform IniReader is back.

## 2.2.0 Early Access

### What's new?
Big remake. Please use the namespace FSCIni.Future. The old namespace is automatically marked as obsolete.
If you find any issue, please message me. More details on github.
- Added custom datatypes like FSCIniTypes
- Added new methods like ini1.Merge(ini2);
- Upgrade to .Net 7

### Example Code
```cs
using FSC_IniReader.Future;

FSCIni ini = @"
[AccountInfo]
Username = Jack
Password = JacksPassword1234

[Settings]
EnableDarkmode = true
RememberLogin = false
ProgramWidth = 720
ProgramHeight = 480
";

if (ini["Settings"]["ProgramWidth"].GetInt == 720)
{
    ini["Settings"]["ProgramWidth"] = 1080;
}

Console.WriteLine(ini.ToString());
```

### Example Code 2

```cs
using FSC_IniReader.Future;

FSCIni ini = new FSCIni(@"
[AccountInfo]
Username = Jack
Password = JacksPassword1234

[Settings]
EnableDarkmode = true
RememberLogin = false
ProgramWidth = 720
ProgramHeight = 480
");

if (ini["Settings"]["ProgramWidth"].GetInt == 720)
{
    ini["Settings"]["ProgramWidth"] = 1080;
}

Console.WriteLine(ini.ToString());
```

### Options
FSCIni got new options. Check out the constructor or the SetOptions Method

## 2.1.0

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

---

Some features are missing or you found a bug? Message me on discord, every idea is welcome: Discord -> Fabian#3563 (Don't message me for fun!)