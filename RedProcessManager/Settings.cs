using System;
using System.Collections.Generic;
using System.IO;

class Settings
{
    static public void ReadSettings()
    {
        Other.DebugLog("ReadSettings Try");

        string line;
        List<string> fileContentList = new List<string>();
        string[] cuted1;
        string[] cuted2;

        using (StreamReader sr = new StreamReader(SettingsFile, System.Text.Encoding.Default))
        {
            while ((line = sr.ReadLine()) != null)
            {
                if (line != "")
                {
                    line = line.Replace(" ", "");

                    fileContentList.Add(line);
                }
            }
        }

        for (int i = 0; i < fileContentList.Count; i++)
        {
            if (fileContentList[i].Contains("|"))
            {
                fileContentList[i] = fileContentList[i].Replace("[", "");
                fileContentList[i] = fileContentList[i].Replace("]", "");
                cuted1 = fileContentList[i].Split('|');
                cuted2 = fileContentList[i + 1].Split('=');

                for (int j = 0; j < SettingsList.Count; j++)
                {
                    if (SettingsList[j].Name == cuted1[0])
                    {
                        SettingsList[j].Value = cuted2[1];
                        break;
                    }
                }
            }
        }

        Other.DebugLog("ReadSettings Success");
    }
    static public void WriteSettings()
    {
        Other.DebugLog("Try WriteSettings");

        try
        {
            File.Delete(SettingsFile);
        }
        catch (Exception)
        {}

        for (int i = 0; i < SettingsList.Count; i++)
        {
            Write.WriteInTxt(SettingsFile, $"[{SettingsList[i].Name}|{SettingsList[i].Type}]", true);
            Write.WriteInTxt(SettingsFile, $"{SettingsList[i].Name}={SettingsList[i].Value}\n", true);
        }

        Other.DebugLog("WriteSettings success");
    }

    static public void GetDefaultSettingsList()
    {
        Other.DebugLog("GetDefaultSettingsList Try");

        SettingsList.Add(new Setting("bool", "false", "Stop"));
        SettingsList.Add(new Setting("bool", "true", "Visibility"));
        SettingsList.Add(new Setting("bool", "true", "Debug"));
        SettingsList.Add(new Setting("int", "2000", "WorkSpeedMain"));
        SettingsList.Add(new Setting("int", "1000", "WorkSpeedSecond"));
        SettingsList.Add(new Setting("int", "10000", "WorkSpeedMainInBoost"));
        SettingsList.Add(new Setting("int", "5000", "WorkSpeedSecondInBoost"));

        Other.DebugLog("GetDefaultSettingsList Success");

        ApplySettingsList();
    }
    static public void ApplySettingsList()
    {
        Other.DebugLog("Try ApplySettingsList");

        foreach (Setting setting in SettingsList)
        {
            try
            {
                Other.DebugLog($"Try ApplySetting [{setting.Name}]");

                switch (setting.Name)
                {
                    case "Stop":
                        Publics.Settings.Stop = Convert.ToBoolean(setting.Value);
                        break;

                    case "Visibility":
                        Publics.Settings.Visibility = Convert.ToBoolean(setting.Value);
                        break;

                    case "Debug":
                        Publics.Settings.Debug = Convert.ToBoolean(setting.Value);
                        break;

                    case "WorkSpeedMain":
                        Publics.Settings.WorkSpeedMain = Convert.ToInt32(setting.Value);
                        break;

                    case "WorkSpeedSecond":
                        Publics.Settings.WorkSpeedSecond = Convert.ToInt32(setting.Value);
                        break;

                    case "WorkSpeedMainInBoost":
                        Publics.Settings.WorkSpeedMainInBoost = Convert.ToInt32(setting.Value);
                        break;

                    case "WorkSpeedSecondInBoost":
                        Publics.Settings.WorkSpeedSecondInBoost = Convert.ToInt32(setting.Value);
                        break;
                }

                Other.DebugLog($"ApplySetting success");
            }
            catch (Exception ex)
            {
                Other.DebugLog($"ApplySetting [{setting.Name}] fail ex=[{ex}]");
            }
        }

        Other.DebugLog("ApplySettingsList Success");
    }

    static public void ConsoleOutputSettings()
    {
        for (int i = 0; i < SettingsList.Count; i++)
        {
            Console.WriteLine($"Setting name = [{SettingsList[i].Name}], type = [{SettingsList[i].Type}], value = [{SettingsList[i].Value}]");
        }

        Console.WriteLine("\n");
    }

    static List<Setting> SettingsList = new List<Setting>();
    static public string SettingsFile { get; set; }
}

class Setting
{
    public string Type { get; set; }
    public string Value { get; set; }
    public string Name { get; set; }

    public Setting(string type, string value, string name) { Type = type; Value = value; Name = name; }
    public Setting() { Type = ""; Value = ""; Name = ""; }
}
