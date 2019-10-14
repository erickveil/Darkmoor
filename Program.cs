using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Darkmoor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Console.WriteLine("Starting...");

            var table = new RandomTable<int>();
            table.AddItem(12);
            table.AddItem(11);
            table.AddItem(83838);
            int result = table.GetResult();
            Console.WriteLine("Result: " + result);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
