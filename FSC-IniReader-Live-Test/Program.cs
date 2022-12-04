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
;