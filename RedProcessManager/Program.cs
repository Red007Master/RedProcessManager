using System;
using System.Threading;

namespace RedProcessManager
{
    public class Program
    {
        static void Main(string[] args)
        {
            Other.Initialization();

            try
            {
                while (true)
                {
                    Other.OneTickLogic();

                    if (!Publics.Settings.Stop)
                        Proceses.ProcesesDetection();
                }
            }
            catch (Exception ex)
            {
                Other.DebugLog($"!!!MAIN_LOOP_EXCEPTION!!!\nEx=[{ex}]");

                for (int i = 0; i < 5; i++)
                {
                    Thread.Sleep(1000);
                    Console.Beep();
                }
            }
        }
    }
}
