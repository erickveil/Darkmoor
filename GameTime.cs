using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darkmoor
{
    /// <summary>
    /// Singleton for tracking the date. Often used by History objects.
    /// 
    /// Not thread safe, but safe enough.
    /// https://csharpindepth.com/Articles/Singleton
    /// </summary>
    class GameTime
    {
        private static GameTime _instance = null;
        private Dice _dice;
        private bool _isInit = false;

        public int Year = 1;
        public int Month = 1;
        public int Day = 1;

        /// <summary>
        /// Private constructor prevents external instantiation.
        /// Use Instance.
        /// </summary>
        private GameTime()
        {

        }

        /// <summary>
        /// Get the copy of this singleton
        /// </summary>
        public static GameTime Instance
        {
            get
            {
                if (_instance is null) { _instance = new GameTime(); }
                return _instance;
            }
        }

        public void Init(Dice dice)
        {
            _dice = dice;
            _isInit = true;
        }

        public string GetDateString()
        {
            return Month + "/" + Day + "/" + Year;
        }

    }
}
