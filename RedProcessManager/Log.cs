using System;
using System.IO;

class Log
{
    static public void LogWrite(byte mode)
    {
        TimeSpan sesionTime = Publics.BoostingTemp.BoosterStop - Publics.BoostingTemp.BoosterStart;
        string output;
        if (mode == 1)
        {
            using (StreamWriter sw = new StreamWriter(Publics.Dir.LogFile, true, System.Text.Encoding.Default))
            {
                output = $"Process:[{Publics.BoostingTemp.CurrentDetectedTrigger.Name}] Start:[{Publics.BoostingTemp.BoosterStart}], Stop:[{Publics.BoostingTemp.BoosterStop}] Sesion time:[{sesionTime.Hours}:{sesionTime.Minutes}:{sesionTime.Seconds}]";
                sw.WriteLine(output);
                sw.Write("Stoped:");
                for (int i = 0; i < Publics.BoostingTemp.CurrentStopedTargets.Count; i++)
                {
                    if (i == Publics.BoostingTemp.CurrentStopedTargets.Count - 1)
                    {
                        sw.Write($"{Publics.BoostingTemp.CurrentStopedTargets[i].Name}.");
                    }
                    else
                    {
                        sw.Write($"{Publics.BoostingTemp.CurrentStopedTargets[i].Name},");
                    }
                }
                sw.WriteLine();
                sw.WriteLine();
            }
        }
        else if (mode == 2)
        {
            using (StreamWriter sw = new StreamWriter(Publics.Dir.LogFile, true, System.Text.Encoding.Default))
            {
                output = $"Booting up [{DateTime.Now}].";
                sw.WriteLine(output);
                sw.WriteLine();
                sw.WriteLine();
            }
        }
    }
}
