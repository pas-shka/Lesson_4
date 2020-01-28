using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Lesson_4
{
    class ConnectDB
    {
        private static int count = 0;
        public int CountList { get; set; }
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        //public MySqlConnection ConnectionBD()
        //{
        //    MySqlConnection sqlConnection = new MySqlConnection(connectionString);
        //    try
        //    {
        //        // Открываем подключение
        //        sqlConnection.Open();
        //        Console.WriteLine("connection OK");
        //    }
        //    catch (SqlException ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //    return sqlConnection;
        //}

        public List<Patient> SelectNumbers() //Выбока списка карт и занесение их в коллекцию
        {
            string sqlSelect = "SELECT p.OHIP_Number, p.OHIP_Code FROM patients_ohip_to_check p WHERE p.Done=0;";
            
            List<Patient> ListPat = new List<Patient>();

            using (MySqlConnection connect= new MySqlConnection(connectionString))
            {
                connect.Open();
                MySqlCommand comand = new MySqlCommand(sqlSelect, connect);
                
                MySqlDataReader reader = comand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ListPat.Add(new Patient(reader.GetValue(0),reader.GetValue(1)));                   
                    }
                    Console.WriteLine("GET LIST Patient - OK");
                }
                
                reader.Close();
            }
            
            return ListPat;
        }

        public void SqlUpdate(Patient PatientData)
        {
            using (MySqlConnection connect = new MySqlConnection(connectionString))
            {
                connect.Open();

                string sqlUpdate = $"UPDATE patients_ohip_to_check SET Valid_LastName = \"{PatientData.LName}\", Valid_FirstName = \"{PatientData.FName}\", Valid_DOB='{PatientData.DOB}',  Error='{PatientData.Error}', Done=1  WHERE OHIP_Number='{PatientData.OHIP_Number}'";

                MySqlCommand command = new MySqlCommand(sqlUpdate, connect);
                    command.ExecuteNonQuery();               
            }

            count++;
            Console.WriteLine(count+" - Update LIST Patient - OK - " + PatientData.OHIP_Number);
        }
    }
}
