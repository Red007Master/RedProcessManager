using System;

namespace RedProcessManager
{
    public class Program
    {
        static void Main(string[] args)
        {
            Other.Initialization();

            while (true)
            {
                Other.OneTickLogic();

                if (!Publics.Settings.Stop)
                    Proceses.ProcesesDetection();
            }

            Console.ReadLine();
        }
    }
}
