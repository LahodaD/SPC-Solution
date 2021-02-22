using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterSudoku
{
    class Solver
    {
        private SudokuCells[,] cells = new SudokuCells[9, 9];

        public Solver(SudokuCells[,] cells)
        {
            this.cells = cells;
        }

        public bool CheckValidValue(int xCoordinate, int yCoordinate, int value)
        {
            for (int i = 0; i < 9; i++)
            {
                if (cells[xCoordinate, i].Value == value && i != yCoordinate)
                {
                    return false;
                }
                if (cells[i, yCoordinate].Value == value && i != xCoordinate)
                {
                    return false;
                }
            }

            int tmpI = xCoordinate - (xCoordinate % 3);
            int tmpJ = yCoordinate - (yCoordinate % 3);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (cells[tmpI + i, tmpJ + j].Value == value && tmpI + i != xCoordinate && tmpJ + j != yCoordinate)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void OptionsAdd()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (cells[i, j].Value == 0)
                    {
                        cells[i, j].DefoultPoss();
                        for (int num = 1; num <= 9; num++)
                        {
                            if (CheckValidValue(i, j, num))
                            {
                                cells[i, j].SetPossibilities(num);
                            }
                        }
                    }
                }
            }
        }


        private int ValueAdd()
        {
            int isMoreCandidates = 0;
            foreach (SudokuCells cell in cells)
            {
                if (cell.GetPossibilities().Count == 1 && cell.Value == 0)
                {
                    cell.SetText(cell.GetPossibilities()[0]);
                }

                if (cell.GetPossibilities().Count != 1 && cell.Value == 0)
                {
                    isMoreCandidates++;
                }
            }
            return isMoreCandidates;
        }

        public bool LogicSolve()
        {
            int counter = 0;
            do
            {
                int pomCounter = counter;
                OptionsAdd();
                counter = ValueAdd();
                if (pomCounter == counter)
                {
                    return false;
                }
            } while (counter != 0);
            return true;
        }

        public bool Solve()
        {
            int row = 0;
            int col = 0;
            bool isEmpty = true;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (cells[i, j].Value == 0)
                    {
                        row = i;
                        col = j;
                        isEmpty = false;
                        break;
                    }
                }
                if (!isEmpty)
                {
                    break;
                }
            }

            if (isEmpty)
            {
                return true;
            }

            for (int num = 0; num < cells[row, col].GetPossibilities().Count; num++)
            {
                int pom = cells[row, col].GetPossibilities()[num];
                if (CheckValidValue(row, col, pom))
                {
                    cells[row, col].SetText(pom);
                    if (Solve())
                    {
                        return true;
                    }
                    else
                    {
                        cells[row, col].Value = 0;
                    }
                }
            }
            return false;
        }

    }
}
