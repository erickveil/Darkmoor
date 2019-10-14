using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darkmoor
{
    public class Dice
    {
        readonly Random _rng = new Random();

        /// <summary>
        /// Rolls qty dice with faces and returns the total.
        /// Can obtain the individual die rolls in results.
        /// Provides expected variation from the mean.
        /// </summary>
        /// <param name="qty"></param>
        /// <param name="faces"></param>
        /// <param name="results"></param>
        /// <returns></returns>
        public int Roll(int qty, int faces, out List<int> results)
        {
            results = new List<int>();
            int total = 0;
            for (int i = 0; i < qty; ++i)
            {
                int roll = _rng.Next(faces) + 1;
                total += roll;
                results.Add(roll);
                //Console.WriteLine("Roll: " + roll);
            }
            //Console.WriteLine("Total: " + total);
            return total;
        }

        /// <summary>
        /// Overloaded function without the need for a results list.
        /// </summary>
        /// <param name="qty"></param>
        /// <param name="faces"></param>
        /// <returns></returns>
        public int Roll(int qty, int faces)
        {
            int total = 0;
            for (int i = 0; i < qty; ++i)
            {
                int roll = _rng.Next(faces) + 1;
                total += roll;
                //Console.WriteLine("Roll: " + roll);
            }
            //Console.WriteLine("Total: " + total);
            return total;
        }
    }
}
