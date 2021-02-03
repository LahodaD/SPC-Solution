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
        public void clear()
        {
            Text = string.Empty;
            IsLocked = false;
            Value = 0;
        }

        public void setText(int value)
        {
            if (value != 0)
            {
                Text = value.ToString();
            }
            Value = value;
        }
        // pripadne dodelat metodu setText()
    }
}
