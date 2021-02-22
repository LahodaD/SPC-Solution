using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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

                    if (((i / 3) + (j / 3)) % 2 == 0)
                    {
                        cells[i, j].BackColor = Color.LightGray;
                    }

                    panel1.Controls.Add(cells[i, j]);
                }
            }
        }

        private void PressCell(Object sender, KeyPressEventArgs e)
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
                    cell.Clear();
                }
                else
                {
                    cell.SetText(cellValue);
                }
                cell.ForeColor = SystemColors.ControlDark;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            createGameFild();
            
        }

        public void LoadFromHisotry(History history)
        {
            

            Database database = new Database();
            if (history.MyProperty == null)
            {
                return;
            }

            string pom = database.Select(history.MyProperty, "Zadani").ToString();
            
            int i = 0;
            foreach (SudokuCells cell in cells)
            {
                cell.SetText(pom[i] - '0');
                i++;
            }
        }



        private void ManualEntry_Click(object sender, EventArgs e)
        {
            ClearBoard();

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    cells[i, j].KeyPress += PressCell;
                }
            }
        }


        private void SolveBtn_Click(object sender, EventArgs e)
        {
            Solver solver = new Solver(cells);

            bool wrongValue = false;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (!solver.CheckValidValue(i, j, cells[i, j].Value) && cells[i, j].Value != 0)
                    {
                        cells[i, j].ForeColor = Color.Red;
                        wrongValue = true;
                    }
                    else if (cells[i,j].Value != 0)//nastavi backcolor zase na modrou pokud uz je to opravene
                    {
                        //nastavi se zadané hodnoty na modrou
                        cells[i, j].ForeColor = SystemColors.ControlDark;
                    }
                }
            }
            if (wrongValue)
            {
                MessageBox.Show("chyba v zadani, prosim upravte");
                return;
            }

            string[] zadani = new string[81];
            int a = 0;
            foreach (SudokuCells cell in cells)
            {
                zadani[a] = cell.Value.ToString();
                a++;
            }

            string zadaniPom = string.Join("", zadani);
            
            double stopky;
            
            Database databse = new Database();
            
            
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            if (!solver.LogicSolve())
            {
                solver.Solve();
            }
            stopwatch.Stop();

            string[] reseni = new string[81];
            int b = 0;
            foreach (SudokuCells cell in cells)
            {
                reseni[b] = cell.Value.ToString();
                b++;
            }

            string reseniPom = string.Join("", reseni);

            TimeSpan stopwatchElapsed = stopwatch.Elapsed;
            stopky = Convert.ToDouble(stopwatchElapsed.TotalSeconds);

            databse.Insert(zadaniPom, reseniPom, DateTime.Now, stopky);
        }

        private void LoadGame_Click(object sender, EventArgs e)
        {
            ClearBoard();

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

                            cells[i, row].SetText(s[i] - '0');
                            if (cells[i,row].Value != 0)
                            {
                                cells[i, row].IsLocked = true;
                                cells[i, row].ForeColor = SystemColors.ControlDark;
                            }
                        }
                        row++;
                    }
                }
            }
        }

        private void SaveGame_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt";
            
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileName = saveFileDialog1.FileName;
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
        
        private void ClearBoard()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    cells[i,j].Clear();
                    if (((i / 3) + (j / 3)) % 2 == 0)
                    {
                        cells[i, j].BackColor = Color.LightGray;
                    }
                    else
                    {
                        cells[i, j].BackColor = Color.White;
                    }
                }
            }
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            ClearBoard();
            History history = new History();
            history.ShowDialog();
            LoadFromHisotry(history);
        }


    }
}
