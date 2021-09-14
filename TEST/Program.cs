using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEST
{
    class Program
    {
        static void Main(string[] args)
        {
            //ConsoleHelper.SetCurrentFont("Consolas", 20);

            int spacing = 5;

            if (spacing % 2 == 1)
                spacing++;

            Console.WriteLine(spacing % 2);

            Console.ReadLine();
        }
    }
}
