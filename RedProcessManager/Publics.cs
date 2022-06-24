using System;
using System.Collections.Generic;

class Publics
{
    public class Settings
    {
        static public bool Stop { get; set; }
        static public bool Visibility { get; set; }
        static public bool Debug { get; set; }
        static public int WorkSpeedMain { get; set; }
        static public int WorkSpeedSecond { get; set; }
        static public int WorkSpeedMainInBoost { get; set; }
        static public int WorkSpeedSecondInBoost { get; set; }
    }

    public class Floats
    {
        static public int WorkSpeedMainUsing { get; set; }
        static public int WorkSpeedSecondUsing { get; set; }
        static public bool WorkState { get; set; }
        static public int GlobalCounter { get; set; }
        static public DateTime LastFileUpdate { get; set; }
    }

    public class BoostingTemp
    {
        static public Trigger CurrentDetectedTrigger { get; set; }
        static public List<Target> CurrentStopedTargets { get; set; }
        static public DateTime BoosterStart { get; set; }
        static public DateTime BoosterStop { get; set; }
    }

    public class Dir
    {
        static public string MenuTrigger { get; set; }
        static public string Main { get; set; }
        static public string LogFile { get; set; }
        static public string DebugFile { get; set; }
        static public string UIMenuFile { get; set; }
    }
}
