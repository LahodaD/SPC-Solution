using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BetterSudoku
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SudokuCells[,] cells = new SudokuCells[9, 9];
        private void createGameFild()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    cells[i, j] = new SudokuCells();
                    cells[i, j].Size = new Size(40, 40);
                    cells[i, j].Location = new Point(i * 40, j * 40);
                    cells[i, j].FlatStyle = FlatStyle.Flat;
                    cells[i, j].FlatAppearance.BorderColor = Color.Black;

                    //cells[i, j].KeyPress += pressCell;

                    panel1.Controls.Add(cells[i, j]);
                }
            }
        }

        private void pressCell(Object sender, KeyPressEventArgs e)
        {
            SudokuCells cell = (SudokuCells)sender;
            int cellValue;

            if (cell.IsLocked)
            {
                return;
            }

            if (int.TryParse(e.KeyChar.ToString(), out cellValue))
            {
                if (cellValue == 0)
                {
                    cell.clear();
                }
                else
                {
                    cell.setText(cellValue);
                }
                cell.ForeColor = SystemColors.ControlDark;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            createGameFild();
        }

        private bool checkValidValue(int xCoordinate, int yCoordinate, int value)
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

        private bool solve()//TODO:vylepsit
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

            for (int num = 1; num <= 9; num++)
            {
                if (checkValidValue(row, col, num))
                {
                    cells[row, col].setText(num);
                    if (solve())
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

        private void manualEntry_Click(object sender, EventArgs e)
        {
            clearBoard();

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    cells[i, j].KeyPress += pressCell;
                }
            }
        }


        private void sloveBtn_Click(object sender, EventArgs e)
        {
            bool wrongValue = false;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (!checkValidValue(i, j, cells[i, j].Value) && cells[i, j].Value != 0)
                    {
                        cells[i, j].BackColor = Color.Red;
                        wrongValue = true;
                    }
                    else //nastavi backcolor zase na bilou pokud uz je to opravene
                    {
                        cells[i, j].BackColor = Color.White;
                    }
                }
            }
            if (wrongValue)
            {
                MessageBox.Show("chyba v zadani, prosim upravte");
                return;
            }

            if (!solve())//TODO: nefunguje tak jak ma
            {
                MessageBox.Show("nelze vyřešit");
            }
        }

        private void loadGame_Click(object sender, EventArgs e)
        {
            clearBoard();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "(*.txt)|*.txt";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileName = openFileDialog1.FileName;
                string s;
                int row = 0;
                using (StreamReader sr = new StreamReader(fileName))
                {
                    while ((s = sr.ReadLine()) != null)
                    {
                        //kontorola radku a sloupcu
                        if (s.Length > 9 || row > 9)
                        {
                            MessageBox.Show("soubor byl poškozen špatný počet řádků nebo sloupců");
                            return;
                        }

                        for (int i = 0; i < 9; i++)
                        {
                            if ((s[i]-'0') < 0 || (s[i] - '0') > 9) //kontrola pozadovanych hodnot
                            {
                                MessageBox.Show("soubor byl poškozen špatná hodnota");
                                return;
                            }

                            cells[i, row].setText(s[i] - '0');
                            if (cells[i,row].Value != 0)
                            {
                                cells[i, row].IsLocked = true;
                            }
                        }
                        row++;
                    }
                }
            }
        }

        private void saveGame_Click(object sender, EventArgs e)
        {
            string fileName = string.Empty;
            saveFileDialog1.InitialDirectory = "c:\\";
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt";
            
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileName = saveFileDialog1.FileName;
                using (StreamWriter sw = new StreamWriter(fileName))
                {
                    for (int i = 0; i < 9; i++)
                    {
                        for (int j = 0; j < 9; j++)
                        {
                            sw.Write(cells[j, i].Value);
                        }
                        sw.WriteLine();
                    }
                    sw.Flush();
                }
            }
        }

        private void clearBoard()
        {
            foreach (SudokuCells cell in cells)
            {
                cell.clear();
            }
        }
    }
}
