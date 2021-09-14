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

                if (!Publics.Stop)
                    Proceses.ProcesesDetection();
            }

            Console.ReadLine();
        }
    }
}
