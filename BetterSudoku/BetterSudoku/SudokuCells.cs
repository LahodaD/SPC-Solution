using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BetterSudoku
{
    class SudokuCells : Button
    {
        public bool IsLocked { get; set; }
        public int Value { get; set; }

        private List<int> possibilities = new List<int>();
        public void Clear()
        {
            Text = string.Empty;
            IsLocked = false;
            Value = 0;
        }

        public void SetText(int value)
        {
            if (value != 0)
            {
                Text = value.ToString();
            }
            Value = value;
        }
        public void DefoultPoss()
        {
            possibilities.Clear();
        }
        public void SetPossibilities(int value)
        {
            possibilities.Add(value);
        }

        public List<int> GetPossibilities()
        {
            return possibilities;
        }
    }
}
