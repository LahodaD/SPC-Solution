using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BetterSudoku
{
    public partial class History : Form
    {
        public string EnteringGame { get; set; }
        public History()
        {
            InitializeComponent();
            AddDataToListbox();
        }

        //TODO: udelat objekt
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

        private void History_Load(object sender, EventArgs e)
        {
            createGameFild();        
        }

        private void AddDataToListbox()
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;
                                                        Initial Catalog=Sudoku;Integrated Security=True";
            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    connection.Open();
                
                    string dotaz = "SELECT * FROM Sudokutable";
                    SqlCommand select = new SqlCommand(dotaz, connection);

                    SqlDataReader dataReader = select.ExecuteReader();
                    while (dataReader.Read())
                    {
                        listBox1.Items.Add(dataReader[0].ToString());
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (listBox1.SelectedIndex != -1)
            {
                Database database = new Database();
                string vybrana = (string)listBox1.SelectedItem;
                dateGame.Text = database.Select(vybrana, "DatumHry");
                timer.Text = database.Select(vybrana, "Stopky");

                int i = 0;
                foreach (SudokuCells cell in cells)
                {
                    cell.Clear();
                    cell.SetText(database.Select(vybrana, "Zadani")[i] - '0');
                    i++;
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
            {
                return;
            }
            string choose = (string)listBox1.SelectedItem;
            Database database = new Database();
            int i = 0;
            foreach (SudokuCells cell in cells)
            {
                cell.Clear();
                cell.SetText(database.Select(choose, "Reseni")[i] - '0');
                i++;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
            {
                return;
            }
            string choose = (string)listBox1.SelectedItem;
            Database database = new Database();
            int i = 0;
            foreach (SudokuCells cell in cells)
            {
                cell.Clear();
                cell.SetText(database.Select(choose, "Zadani")[i] - '0');
                i++;
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            EnteringGame = (string)listBox1.SelectedItem;
            Close();
        }
    }
}
