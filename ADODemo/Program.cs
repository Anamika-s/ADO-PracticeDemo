// Step 1 : 
using System.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ADODemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Step 2: 
            // Create SqlConnection object with connectionString
            //SqlConnection connection = new SqlConnection();
            ////connection.ConnectionString = "it contains informtaion about database";
            //connection.ConnectionString = @"data source=ANAMIKA\SQLSERVER;initial catalog=ProjectDb;integrated security=true";
            ////connection.ConnectionString = @"data source=ANAMIKA\SQLSERVER;initial catalog=ProjectDb;user id=sa;password=pp";
            SqlConnection connection = new SqlConnection(@"data source=ANAMIKA\SQLSERVER;initial catalog=ProjectDb;integrated security=true");
            // Step 3: Declare Command Object
            // It tales 2 parameters
            // First :Command
            // Second : Connection Object
            //SqlCommand command = new SqlCommand();
            //command.CommandText = "Select * from Employee";
            //command.Connection= connection;

            SqlCommand command = new SqlCommand("select * from Employee", connection);

            // Step 4 : 
            // OPen Connection
            connection.Open();
            // Step 5 : Execute query on server
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Console.WriteLine(reader[0] + " " + reader[1] + " " + reader[2]);
                    //Console.WriteLine(reader["id"] + " " + reader["name"]);
                }
            }
            else
            {
                Console.WriteLine("There are no records");
            }

            connection.Close();
        }
    }
}