using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darkmoor
{
    /// <summary>
    /// Things in the world carry around a history of what's happened to them, 
    /// that can be inspected.
    /// </summary>
    class HistoryLog
    {
        public List<string> _historyList = new List<string>();
        GameTime _timeObj;

        public HistoryLog()
        {
            _timeObj = GameTime.Instance;
        }

        public void addRecord(string record, bool isLogged = true)
        {
            record = _timeObj.GetDateString() + ": " + record;
            _historyList.Add(record);
            if (isLogged)
            {
                Console.WriteLine(record);
            }
        }
    }
}
