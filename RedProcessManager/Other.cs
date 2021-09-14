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

        DebugLog("Start");

        Settings.GetDefaultSettingsList();

        InitializationInstalLogic();

        #region WorkSpeed

        Publics.WorkSpeedMainUsing = Publics.WorkSpeedMain;
        Publics.WorkSpeedSecondUsing = Publics.WorkSpeedSecond;

        #endregion WorkSpeed

        #region CoreSettingsApply

        ConsoleVisibility.Hide();

        Log.LogWrite(2);

        if (Publics.Visibility)
        {
            ConsoleVisibility.Show();
        }

        #endregion CoreSettingsApply
    }

    static private void InitializationGetConstantData()
    {
        #region FilesDir

        Dir.Main = @"C:\RedProcessManager";

        Targets.Path = $@"{Dir.Main}\target.txt";
        Triggers.Path = $@"{Dir.Main}\trigger.txt";
        Settings.SettingsFile = $@"{Dir.Main}\settings.txt";
        Dir.DebugFile = $@"{Dir.Main}\debugLog.txt";
        Dir.LogFile = $@"{Dir.Main}\log.txt";
        Dir.UIMenuFile = $@"{Dir.Main}\UI\RedUI.exe";

        #endregion FilesDir
        #region FloatFilesDir

        Dir.MenuTrigger = $@"{Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)}\menu";

        #endregion FloatFilesDir
        #region OtherData

        Publics.GlobalCounter = 0;

        #endregion OtherData
    }
    static private void InitializationInstalLogic()
    {
        if (!Directory.Exists(Dir.Main))
        {
            Directory.CreateDirectory(Dir.Main);
            File.Create(Targets.Path).Close();
            {
                Write.WriteInTxt(Targets.Path, @"// In this file write processes that must be turned off following the process rules writed in trigger.txt", true);
                Write.WriteInTxt(Targets.Path, @"// syntax example '1 | helloWorld | C:\Windows\Programs\helloWorld.exe': '1' is process ID increase it by 1 for every process, 'helloWorld' is name of process in task manager (in details tab), 'C:\Windows\Programs\helloWorld.exe' is path to process file", true);
            }
            File.Create(Triggers.Path).Close();
            {
                Write.WriteInTxt(Triggers.Path, @"// In this file write processes that must trigger process busting", true);
                Write.WriteInTxt(Triggers.Path, @"// syntax example '1 | game    | 0,1,2': '1' is process number increase it by 1 for every process, 'game' is name of process in task manager (in details tab), '0,1,2' is ID of processes in target.txt, they be closed after detecting 'game' process", true);
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
        Publics.GlobalCounter++;

        if (Publics.WorkState == false)
        {
            if (Publics.GlobalCounter > 50)
            {
                Other.DebugLog("GlobalCounter iteration 50, GlobalCounter set to 0");

                Read.ReadTriggersAndTargets();

                GC.Collect();
                Publics.GlobalCounter = 0;
            }

            if (Directory.Exists(Dir.MenuTrigger))
            {
                DebugLog("Menu call detected");

                ConsoleVisibility.Show();

                Directory.Delete(Dir.MenuTrigger);

                Process.Start(Dir.UIMenuFile);

                DebugLog("Menu call complete");
            }
        }
    }

    static public void DebugLog(string inputLogMassage)
    {
        if (Publics.Debug || inputLogMassage == "Start")
        {
            string result = $"[{DateTime.Now}] {inputLogMassage}.";

            if (inputLogMassage != "Start")
            {
                Console.WriteLine(result);
                Write.WriteInTxt(Dir.DebugFile, result, true);
            }
            else
            {
                Write.WriteInTxt(Dir.DebugFile, "", true);

                Console.WriteLine(result);
                Write.WriteInTxt(Dir.DebugFile, result, true);
            }
        }
    }
}
