using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace pripojeni_k_databazi
{
    class Program
    {
        static void Main(string[] args)
        {


            string connectionString = @"Data Source=databaze.fai.utb.cz;Initial Catalog=A20500_AP1DS;User ID=A20500;Password=ahoj1234";

            using (SqlConnection pripojeni = new SqlConnection(connectionString))
            {
                pripojeni.Open();

                SqlCommand prikaz = new SqlCommand();
                prikaz.Connection = pripojeni;
                prikaz.CommandText = "SELECT * FROM Sudoku";

                SqlDataReader dataReader = prikaz.ExecuteReader();
                while (dataReader.Read()) // dokud neprojdeme vsechny zaznamy
                {
                    Console.WriteLine("{0}| {1}| {2}| {3}| {4}", dataReader[0], dataReader[1], dataReader[2], dataReader[3], dataReader[4]);
                }
            }
            Console.ReadKey();

            //string connectionString;
            //SqlConnection cnnn;

            //connectionString = @"Data Source=databaze.fai.utb.cz;Initial Catalog=A20500_AP1DS;User ID=A20500;Password=ahoj1234";
            //cnnn = new SqlConnection(connectionString);
            //cnnn.Open();
            //Console.WriteLine("aplikace se připojila");

            //SqlCommand prikaz = new SqlCommand();
            //prikaz.Connection = cnnn;
            //prikaz.CommandText = "SELECT * FROM Sudoku";
            //int pocetSlov = (int)prikaz.ExecuteScalar();
            //cnn.Close();



            //string Zadani = "ahoj";
            //string Reseni = "cus";
            //DateTime DatumHry = DateTime.Now;
            //Timer Stopky = null;

            //string dotaz = "INSERT INTO Sudoku (Zadani,Reseni,DatumHry) VALUES (@Zadani, @Reseni, @DatumHry)"; //TODO: dodelat dotaz a vytvorit databazi
            //using (SqlConnection cnn = new SqlConnection(connectionString))
            //{
            //    cnn.Open();
            //    SqlCommand insCmd = new SqlCommand(dotaz, cnn);
            //    // use sqlParameters to prevent sql injection!
            //    insCmd.Parameters.AddWithValue("@Zadani", Zadani);
            //    insCmd.Parameters.AddWithValue("@Reseni", Reseni);
            //    insCmd.Parameters.AddWithValue("@DatumHry", DatumHry);
            //    //insCmd.Parameters.AddWithValue("@Stopky", Stopky);
            //    int affectedRows = insCmd.ExecuteNonQuery();
            //    Console.WriteLine(affectedRows + " rows inserted!");
            //}

            //SqlDataReader dataReader = prikaz.ExecuteReader();
            //while (dataReader.Read())
            //{
            //    Console.WriteLine("co to je {0}", dataReader[0]);
            //}

            //cnn.Close();
            //Console.Write("počet slov je {0}", pocetSlov);


            //Stopwatch stopwatch = new Stopwatch();
            //stopwatch.Start();
            //Thread.Sleep(2000);
            //stopwatch.Stop();
            //TimeSpan stopwatchElapsed = stopwatch.Elapsed;
            //Console.WriteLine(Convert.ToDouble(stopwatchElapsed.TotalSeconds));


            //Console.ReadKey();
        }
    }
}
