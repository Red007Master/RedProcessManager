using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

class Proceses
{
    #region BustStartStop

    static public void SystemBoostStart(Trigger inputTrigger)
    {
        Other.DebugLog($"Detected process [{inputTrigger.Name}] initiating boosting");

        Publics.CurrentDetectedTrigger = inputTrigger;
        TargetsDown(GetOnlineTargetsListFromTrigger(inputTrigger));

        Publics.BoosterStart = DateTime.Now;
        Publics.WorkState = true;
        Publics.WorkSpeedMainUsing = Publics.WorkSpeedMainInBoost;
        Publics.WorkSpeedSecondUsing = Publics.WorkSpeedSecondInBoost;

        OtherLogicStart();

        GC.Collect();

        Other.DebugLog($"Boosting Start, success");
    }
    static public void SystemBoostStop()
    {
        Other.DebugLog($"Process [{Publics.CurrentDetectedTrigger.Name}] don't detected, trying to restore system");

        ProcessesUp(Publics.CurrentStopedTargets);

        Publics.BoosterStop = DateTime.Now;
        Log.LogWrite(1);
        Publics.WorkState = false;
        Publics.WorkSpeedMainUsing = Publics.WorkSpeedMain;
        Publics.WorkSpeedSecondUsing = Publics.WorkSpeedSecond;
        OtherLogicStop();
        Publics.CurrentStopedTargets = null;
        Publics.CurrentDetectedTrigger = null;

        GC.Collect();

        Other.DebugLog($"Boosting Stop, success");
    }

    #endregion BustStartStop

    #region ProcessesDownUp

    static void TargetsDown(List<Target> inputTarget)
    {
        Process[] targetProcess;
        Publics.CurrentStopedTargets = inputTarget;

        for (int i = 0; i < inputTarget.Count; i++)
        {
            targetProcess = Process.GetProcessesByName(inputTarget[i].Name);

            foreach (var process in targetProcess)
            {
                Other.DebugLog($"Trying to kill process [{process.ProcessName}]");

                try
                {
                    process.Kill();
                    Other.DebugLog($"Process [{process.ProcessName}] secsesfull killed");
                }
                catch (Exception ex)
                {
                    Other.DebugLog($"Process [{process.ProcessName}] don't killed, error acured. Exception masage [{ex}]");
                }
            }
        }


    }
    static void ProcessesUp(List<Target> inputProcesses)
    {
        for (int i = 0; i < inputProcesses.Count; i++)
        {
            Other.DebugLog($"Trying to restore process [{inputProcesses[i].Name}]");

            try
            {
                Process.Start(inputProcesses[i].Path);
                Other.DebugLog($"Process [{inputProcesses[i].Name}] secsesfull restored");
            }
            catch (Exception ex)
            {
                Other.DebugLog($"Process [{inputProcesses[i].Name}] don't restored, error acured. Exception masage [{ex}]");
            }
        }
    }

    #endregion ProcessesDownUp

    #region ProcessesDetection

    static public void ProcesesDetection()
    {
        if (Publics.WorkState)
        {
            ProcesesDetectionAtOn();
        }
        else if (!Publics.WorkState)
        {
            ProcesesDetectionAtOff();
        }

        Thread.Sleep(Publics.WorkSpeedMainUsing);
    }
    static private void ProcesesDetectionAtOn()
    {
        Process currentRequestAsProcess = Process.GetProcessesByName(Publics.CurrentDetectedTrigger.Name).FirstOrDefault();
        Thread.Sleep(Publics.WorkSpeedSecondUsing);
        if (currentRequestAsProcess == null)
        {
            SystemBoostStop();
        }
    }
    static private void ProcesesDetectionAtOff()
    {
        for (int i = 0; i < Triggers.List.Count; i++)
        {
            Process currentRequestAsProcess = Process.GetProcessesByName(Triggers.List[i].Name).FirstOrDefault();

            Thread.Sleep(Publics.WorkSpeedSecondUsing);

            if (currentRequestAsProcess != null)
            {
                SystemBoostStart(Triggers.List[i]);
                break;
            }

            Thread.Sleep(Publics.WorkSpeedSecondUsing);
        }
    }

    static public List<Target> GetOnlineTargetsListFromTrigger(Trigger inputTrigger)
    {
        List<Target> result = new List<Target>();
        Process[] processes = Process.GetProcesses();

        for (int i = 0; i < inputTrigger.Targets.Length; i++)
        {
            Target target = Targets.GetTargetById(inputTrigger.Targets[i]);

            for (int j = 0; j < processes.Length; j++)
            {
                if (processes[j].ProcessName == target.Name || processes[j].ProcessName.Contains(target.Name))
                {
                    result.Add(target);
                    break;
                }
            }
        }

        return result;
    }

    #endregion ProcessesDetection

    #region OtherLogic

    static void OtherLogicStop()
    {
        //Console.Beep(500, 200);

        if (Publics.CurrentDetectedTrigger.Name == "RainbowSix")
        {
            DiscordPrioritizerThread.Abort();
        }
    }
    static void OtherLogicStart()
    {
        //Console.Beep(500, 200);

        Process currentDetectedTrigger = Process.GetProcessesByName(Publics.CurrentDetectedTrigger.Name).First();

        currentDetectedTrigger.PriorityClass = ProcessPriorityClass.AboveNormal;

        if (Publics.CurrentDetectedTrigger.Name == "RainbowSix")
        {
            DiscordPrioritizerThread = new Thread(DiscordPrioritizer);
            DiscordPrioritizerThread.Start();
        }
    }

    #endregion OtherLogic

    #region Discord

    static void DiscordPrioritizer()
    {
        Process[] DiscordProcessesArray;
        while (true)
        {
            try
            {
                DiscordProcessesArray = Process.GetProcessesByName("Discord");

                foreach (var DiscordProcess in DiscordProcessesArray)
                {
                    DiscordProcess.PriorityClass = ProcessPriorityClass.High;
                    Thread.Sleep(100);
                }
                Thread.Sleep(1000);
            }
            catch (Exception ex)
            {
                Other.DebugLog($"DiscordPrioritizer exeption: [{ex}]"); //TEST
            }
        }
    }

    static Thread DiscordPrioritizerThread { get; set; }

    #endregion
}
