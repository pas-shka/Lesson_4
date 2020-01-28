using System;

using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_4
{
    class Program
    {

        static void Main(string[] args)
        {
            ConnectDB connectionBD = new ConnectDB();
            if (connectionBD.SelectNumbers().Count != 0)
            {
                GetPatientData a = new GetPatientData();
                a.SendNumberCart();
                a.CloseBrowser();
            }
            else
            {
                Console.WriteLine("List Empty");
            }

            Console.WriteLine("Validation completed. Press ENTER");
            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
        }
    }
}
