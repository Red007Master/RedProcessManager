using System;
using System.Diagnostics;
using System.IO;

class Other
{
    #region Initialization

    static public void Initialization()
    {
        #region Console

        Console.Title = "RedProcessManager";
        Console.ForegroundColor = ConsoleColor.Green;

        #endregion Console

        InitializationGetConstantData();

        Settings.GetDefaultSettingsList();

        InitializationInstalLogic();

        #region WorkSpeed

        Publics.Floats.WorkSpeedMainUsing = Publics.Settings.WorkSpeedMain;
        Publics.Floats.WorkSpeedSecondUsing = Publics.Settings.WorkSpeedSecond;

        #endregion WorkSpeed

        #region CoreSettingsApply

        ConsoleVisibility.Hide();

        Log.LogWrite(2);

        if (Publics.Settings.Visibility)
        {
            ConsoleVisibility.Show();
        }

        #endregion CoreSettingsApply

        #region FilesUpdateGetLogic

        Publics.Floats.LastFileUpdate = new FileInfo(Targets.Path).LastWriteTime;

        if (new FileInfo(Triggers.Path).LastWriteTime > Publics.Floats.LastFileUpdate)
        {
            Publics.Floats.LastFileUpdate = new FileInfo(Triggers.Path).LastWriteTime;
        }

        #endregion FilesUpdateGetLogic

        DebugLog("Start");
    }

    static private void InitializationGetConstantData()
    {
        #region FilesDir

        Publics.Dir.Main = @"D:\Development\RedsSoft\RedProcessManager";

        Targets.Path = $@"{Publics.Dir.Main}\target.txt";
        Triggers.Path = $@"{Publics.Dir.Main}\trigger.txt";
        Settings.SettingsFile = $@"{Publics.Dir.Main}\settings.txt";
        Publics.Dir.DebugFile = $@"{Publics.Dir.Main}\debugLog.txt";
        Publics.Dir.LogFile = $@"{Publics.Dir.Main}\log.txt";
        Publics.Dir.UIMenuFile = $@"{Publics.Dir.Main}\UI\RedUI.exe";

        #endregion FilesDir
        #region FloatFilesDir

        Publics.Dir.MenuTrigger = $@"{Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)}\menu";

        #endregion FloatFilesDir
        #region OtherData

        Publics.Floats.GlobalCounter = 0;

        #endregion OtherData
    }
    static private void InitializationInstalLogic()
    {
        if (!Directory.Exists(Publics.Dir.Main))
        {
            Directory.CreateDirectory(Publics.Dir.Main);
            File.Create(Targets.Path).Close();
            {
                Write.WriteInTxt(Targets.Path, @"// In this file write processes that must be turned off following the process rules writed in trigger.txt", true);
                Write.WriteInTxt(Targets.Path, @"// syntax example '1 | helloWorld | C:\Windows\Programs\helloWorld.exe': '1' it is process ID increase it by 1 for every process, 'helloWorld' is name of process in task manager (in details tab), 'C:\Windows\Programs\helloWorld.exe' is path to process file", true);
            }
            File.Create(Triggers.Path).Close();
            {
                Write.WriteInTxt(Triggers.Path, @"// In this file write processes that must trigger process busting", true);
                Write.WriteInTxt(Triggers.Path, @"// syntax example '1 | game | 0,1,2': '1' is it process number increase it by 1 for every process, 'game' is name of process in task manager (in details tab), '0,1,2' is ID of processes in target.txt, they be closed after detecting 'game' process", true);
            }
        }
        else
        {
            if (File.Exists(Settings.SettingsFile))
            {
                Settings.ReadSettings();
                Settings.ApplySettingsList();
                Read.ReadTriggersAndTargets();
            }
            else
            {
                Read.ReadTriggersAndTargets();
                Settings.WriteSettings();
            }
        }
    }

    #endregion Initialization

    static public void OneTickLogic()
    {
        Publics.Floats.GlobalCounter++;

        if (Publics.Floats.WorkState == false)
        {
            if (Publics.Floats.GlobalCounter > 50)
            {
                GC.Collect();
                Publics.Floats.GlobalCounter = 0;
            }

            if (new FileInfo(Triggers.Path).LastWriteTime > Publics.Floats.LastFileUpdate || new FileInfo(Targets.Path).LastWriteTime > Publics.Floats.LastFileUpdate)
            {
                Other.DebugLog("File update detected");
                Publics.Floats.LastFileUpdate = DateTime.Now + TimeSpan.FromSeconds(5);
                Read.ReadTriggersAndTargets();
            }

            if (Directory.Exists(Publics.Dir.MenuTrigger))
            {
                DebugLog("Menu call detected");

                ConsoleVisibility.Show();

                Directory.Delete(Publics.Dir.MenuTrigger);

                Process.Start(Publics.Dir.UIMenuFile);

                DebugLog("Menu call complete");
            }
        }
    }

    static public void DebugLog(string inputLogMassage)
    {
        if (Publics.Settings.Debug || inputLogMassage == "Start")
        {
            string result = $"[{DateTime.Now}] {inputLogMassage}.";

            if (inputLogMassage != "Start")
            {
                Console.WriteLine(result);
                Write.WriteInTxt(Publics.Dir.DebugFile, result, true);
            }
            else
            {
                Write.WriteInTxt(Publics.Dir.DebugFile, "", true);

                Console.WriteLine(result);
                Write.WriteInTxt(Publics.Dir.DebugFile, result, true);
            }
        }
    }
}
