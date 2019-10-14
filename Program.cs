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

            var die = new Dice();
            var namer = new RandomName(die);

            for (int i = 0; i < 10; ++i)
            {
                Console.WriteLine(namer.CreateWord());
                //Console.WriteLine(die.Roll(1, 6));
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
