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
        private const string connectionString = @"Data Source=databaze.fai.utb.cz;
                                                        Initial Catalog=A20500_AP1DS;User ID=A20500;Password=ahoj1234";
        //TODO:predelat
        public void Connection()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                MessageBox.Show("Připojení otevřeno");
            }
        }

        public void Insert(string zadani, string reseni, DateTime datumHry, double stopky)
        {
            
            //SqlConnection connection = new SqlConnection();


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                //MessageBox.Show("Připojení otevřeno");

                string dotaz = "INSERT INTO Sudoku (Zadani,Reseni,DatumHry,Stopky) VALUES (@Zadani, @Reseni, @DatumHry, @Stopky)"; //TODO: dodelat dotaz a vytvorit databazi
                SqlCommand insert = new SqlCommand(dotaz, connection);
                insert.Parameters.AddWithValue("@Zadani", zadani);
                insert.Parameters.AddWithValue("@Reseni", reseni);
                insert.Parameters.AddWithValue("@DatumHry", datumHry);
                insert.Parameters.AddWithValue("@Stopky", stopky);

                int affectedRows = insert.ExecuteNonQuery();
                if (affectedRows < 1)
                {
                    MessageBox.Show("nepovedlo se uložit záznamy do databáze");
                    return;
                }
            }
        }
    }
}
