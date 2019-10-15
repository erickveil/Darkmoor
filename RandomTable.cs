using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darkmoor
{
    class RandomTable<T>
    {
        private List<T> _ItemList;
        private int _NumEntries;
        private Dice _dice;

        /// <summary>
        /// Initializes the entry list
        /// </summary>
        public RandomTable(Dice dice)
        {
            _dice = dice;

            _ItemList = new List<T>();
            _NumEntries = 0;
        }

        /// <summary>
        /// Adds and item to the table, with a heavier weight, the higher the
        /// weight value is.
        /// </summary>
        /// <param name="Item"></param>
        /// <param name="Weight"></param>
        public void AddItem(T Item, uint Weight = 1)
        {
            ++_NumEntries;
            for (uint i = 0; i < Weight; ++i)
            {
                _ItemList.Add(Item);
            }
        }

        /// <summary>
        /// Rolls on the table and returns the result
        /// </summary>
        /// <returns></returns>
        public T GetResult()
        {
            int roll = _dice.Roll(1, _ItemList.Count) - 1;
            return _ItemList[roll];
        }

        /// <summary>
        /// Retruns the result at the location requested
        /// If the index is out of bounds, will throw.
        /// </summary>
        /// <param name="Index"></param>
        /// <returns></returns>
        public T GetResult(int Index)
        {
            if (Index > _ItemList.Count)
            {
                throw new Exception("Error on RandomTable::GetResult(int) " +
                    "requesting index " + Index + " out of a possible " + 
                    _ItemList.Count + " entries.");
            }
            return _ItemList[Index];
        }

        /// <summary>
        /// Gets the number of individual items added to the table
        /// </summary>
        /// <returns></returns>
        public int CountUniqueEntries()
        {
            return _NumEntries;
        }

        /// <summary>
        /// Gets the size of the table itself.
        /// </summary>
        /// <returns></returns>
        public int CountPossibilites()
        {
            return _ItemList.Count;
        }
    }
}
