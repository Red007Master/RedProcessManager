using System;
using System.Collections.Generic;
using System.IO;

class Write
{
    static public void WriteInTxt(string targetFile, string line, bool rewriteOff)
    {
        using (StreamWriter sw = new StreamWriter(targetFile, rewriteOff, System.Text.Encoding.Default))
        {
            sw.WriteLine($"{line}");
        }
    }
}
class Read
{
    static public List<string> GetTXTFileContent(string filePath)
    {
        List<string> result = new List<string>();
        string line;

        using (StreamReader sr = new StreamReader(filePath, System.Text.Encoding.Default))
        {
            while ((line = sr.ReadLine()) != null)
            {
                line = line.Replace(" ", "");
                result.Add(line);
            }
        }

        return result;
    }

    static public void ReadTriggersAndTargets()
    {
        Other.DebugLog("Try ReadTriggersAndTargets");

        try
        {
            Other.DebugLog("Try read Triggers");
            Triggers.Load();
            Other.DebugLog("Triggers readed success");
        }
        catch (Exception ex)
        {
            Other.DebugLog($"Triggers read fail ex=[{ex}]");
        }

        try
        {
            Other.DebugLog("Try read Targets");
            Targets.Load();
            Other.DebugLog("Targets readed success");
        }
        catch (Exception ex)
        {
            Other.DebugLog($"Targets read fail ex=[{ex}]");
        }
    }
}
