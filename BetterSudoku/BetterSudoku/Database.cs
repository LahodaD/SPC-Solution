using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BetterSudoku
{
    class Database
    {
        private const string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;
                                                        Initial Catalog=Sudoku;Integrated Security=True";
        //TODO:predelat
        //public void Connection()
        //{
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();
        //        MessageBox.Show("Připojení otevřeno");
        //    }
        //}

        public void Insert(string zadani, string reseni, DateTime datumHry, double stopky)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    connection.Open();
                    //MessageBox.Show("Připojení otevřeno");

                    string dotaz = "INSERT INTO Sudokutable (Zadani,Reseni,DatumHry,Stopky) VALUES (@Zadani, @Reseni, @DatumHry, @Stopky)"; //TODO: dodelat dotaz a vytvorit databazi
                    SqlCommand insert = new SqlCommand(dotaz, connection);
                    insert.Parameters.AddWithValue("@Zadani", zadani);
                    insert.Parameters.AddWithValue("@Reseni", reseni);
                    insert.Parameters.AddWithValue("@DatumHry", datumHry);
                    insert.Parameters.AddWithValue("@Stopky", stopky);

                    int affectedRows = insert.ExecuteNonQuery();
                    if (affectedRows < 1)
                    {
                        MessageBox.Show("nepovedlo se uložit záznamy do databáze");
                        connection.Close();
                        return;
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " nepodařilo se uložit do databáze");
                
                return;
            }

        }


        public string Select(string hledaneSlovo, string sloupec)
        {
            string found = "";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    
                    connection.Open();
                    
                    string dotaz = "SELECT Zadani, Reseni, DatumHry, Stopky FROM Sudokutable WHERE Id=@hledaneSlovo";
                    SqlCommand select = new SqlCommand(dotaz, connection);
                    select.Parameters.AddWithValue("@hledaneSlovo", hledaneSlovo);
                    

                    SqlDataReader dataReader = select.ExecuteReader();
                    while (dataReader.Read())
                    {
                        if (sloupec == "DatumHry")
                        {
                            found = dataReader[2].ToString();
                        }
                        else if (sloupec == "Stopky")
                        {
                            found = dataReader[3].ToString();
                        }
                        else if (sloupec == "Zadani")
                        {
                            found = dataReader[0].ToString();
                        }
                        else if (sloupec == "Reseni")
                        {
                            found = dataReader[1].ToString();
                        }
                        
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " nepodařilo se nacist z databáze");
                return "";
            }


            return found;
        }

    }
}
