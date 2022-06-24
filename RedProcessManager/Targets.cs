using System;
using System.Collections.Generic;

class Targets
{
    public static List<Target> List = new List<Target>();
    public static string Path { get; set; }

    public static void Load()
    {
        Other.DebugLog("Try LoadTargets");

        List = new List<Target>();
        List<string> fileContent = Read.GetTXTFileContent(Path);
        string[] buffer;

        for (int i = 0; i < fileContent.Count; i++)
        {
            if (!fileContent[i].Contains("//"))
            {
                buffer = fileContent[i].Split('|');
                List.Add(new Target(Convert.ToInt32(buffer[0]), buffer[1], buffer[2]));
            }
        }

        Other.DebugLog("LoadTargets success");
    }

    public static Target GetTargetById(int id)
    {
        Target result = new Target();

        for (int i = 0; i < List.Count; i++)
        {
            if (List[i].Id == id)
            {
                result = List[i];
                break;
            }
        }

        return result;
    }
}

class Target
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Path { get; set; }

    public Target(int id, string name, string path) { Id = id; Name = name; Path = path; }
    public Target() { }
}
