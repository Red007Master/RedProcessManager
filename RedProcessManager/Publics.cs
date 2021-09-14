using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

class Publics
{
    #region Settings

    static public bool Stop { get; set; }
    static public bool Visibility { get; set; }
    static public bool Debug { get; set; }
    static public int WorkSpeedMain { get; set; }
    static public int WorkSpeedSecond { get; set; }
    static public int WorkSpeedMainInBoost { get; set; }
    static public int WorkSpeedSecondInBoost { get; set; }

    #endregion Settings

    #region Floats

    static public int WorkSpeedMainUsing { get; set; }
    static public int WorkSpeedSecondUsing { get; set; }
    static public bool WorkState { get; set; }
    static public int GlobalCounter { get; set; }

    #endregion Floats

    #region BoostingTemp

    static public Trigger CurrentDetectedTrigger { get; set; }
    static public List<Target> CurrentStopedTargets { get; set; }
    static public DateTime BoosterStart { get; set; }
    static public DateTime BoosterStop { get; set; }

    #endregion BoostingTemp
}

class Dir
{
    static public string MenuTrigger { get; set; }
    static public string Main { get; set; }
    static public string LogFile { get; set; }
    static public string DebugFile { get; set; }
    static public string UIMenuFile { get; set; }
}
