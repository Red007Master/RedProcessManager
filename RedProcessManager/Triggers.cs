using System;
using System.Collections.Generic;

class Triggers
{
    public static List<Trigger> List = new List<Trigger>();
    public static string Path { get; set; }

    public static void Load()
    {
        Other.DebugLog("Try LoadTriggers");

        List = new List<Trigger>();
        List<string> fileContent = Read.GetTXTFileContent(Path);
        string[] buffer;

        for (int i = 0; i < fileContent.Count; i++)
        {
            if (!fileContent[i].Contains("//"))
            {
                buffer = fileContent[i].Split('|');
                List.Add(new Trigger(Convert.ToInt32(buffer[0]), buffer[1], buffer[2]));
            }
        }

        Other.DebugLog("LoadTriggers success");
    }
}

class Trigger
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int[] Targets { get; set; }

    public Trigger(int id, string name, string targets)
    {
        Id = id;
        Name = name;

        string[] buffer = targets.Split(',');
        Targets = Array.ConvertAll(buffer, int.Parse);
    }
    public Trigger() { }
}
