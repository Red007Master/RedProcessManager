using System;
using System.IO;

class Log
{
    static public void LogWrite(byte mode)
    {
        TimeSpan sesionTime = Publics.BoosterStop - Publics.BoosterStart;
        string output;
        if (mode == 1)
        {
            using (StreamWriter sw = new StreamWriter(Dir.LogFile, true, System.Text.Encoding.Default))
            {
                output = $"Process:[{Publics.CurrentDetectedTrigger.Name}] Start:[{Publics.BoosterStart}], Stop:[{Publics.BoosterStop}] Sesion time:[{sesionTime.Hours}:{sesionTime.Minutes}:{sesionTime.Seconds}]";
                sw.WriteLine(output);
                sw.Write("Stoped:");
                for (int i = 0; i < Publics.CurrentStopedTargets.Count; i++)
                {
                    if (i == Publics.CurrentStopedTargets.Count - 1)
                    {
                        sw.Write($"{Publics.CurrentStopedTargets[i].Name}.");
                    }
                    else
                    {
                        sw.Write($"{Publics.CurrentStopedTargets[i].Name},");
                    }
                }
                sw.WriteLine();
                sw.WriteLine();
            }
        }
        else if (mode == 2)
        {
            using (StreamWriter sw = new StreamWriter(Dir.LogFile, true, System.Text.Encoding.Default))
            {
                output = $"Booting up [{DateTime.Now}].";
                sw.WriteLine(output);
                sw.WriteLine();
                sw.WriteLine();
            }
        }
    }
}
