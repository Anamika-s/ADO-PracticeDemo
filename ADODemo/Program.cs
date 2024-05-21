// Step 1 : 
using System.Data.SqlClient;
 
namespace ADODemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string choice = "y";
            while (choice.ToLower() == "y")
            {
                int ch = Menu();
                switch (ch)
                {
                    case 1: GetEmployees(); break;
                    case 2:
                        {
                            Console.WriteLine("Enter Name");
                            string name = Console.ReadLine();
                            Console.WriteLine("Enter Dept");
                            string dept = Console.ReadLine();
                            Console.WriteLine("Enter Salary");
                            int salary = Int32.Parse(Console.ReadLine());
                            Console.WriteLine("enter Doj");
                            DateTime doj = DateTime.Parse(Console.ReadLine());

                            InsertEmployeeRecord(name, dept, salary, doj); break;
                        }
                    case 3:
                        {
                            Console.WriteLine("Enter ID for which to Edit record");
                            int id = Byte.Parse(Console.ReadLine());
                            Console.WriteLine("Enter updated dept");
                            string dept = Console.ReadLine();
                            Console.WriteLine("enter updated salary");
                            int salary = Int32.Parse(Console.ReadLine());
                            UpdateEmployee(id, dept, salary); break;
                        }
                    case 4:
                        {
                            Console.WriteLine("enter id for which to search record");
                            int id = byte.Parse(Console.ReadLine());
                            SearchEmployeeByID(id); 
                            break;
                        }
                    case 5:
                        {
                            Console.WriteLine("enter id for which to delete record");
                            int id = byte.Parse(Console.ReadLine());
                            DeleteEmployee(id); break;
                        }
                        case 6:
                        {
                            Console.WriteLine($"No of Employees are {GetEmployeesCount()}"); break;
                            
                        }
                }
                Console.WriteLine("Shall we repeat");
                choice = Console.ReadLine();
            
            }
            
        }

        static void GetEmployees()
        {
            SqlConnection connection = new SqlConnection(@"data source=ANAMIKA\SQLSERVER;initial catalog=ProjectDb;integrated security=true");
            SqlCommand command = new SqlCommand("select * from Employee", connection);
            connection.Open();
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
        static void InsertEmployeeRecord(string name , string dept, int salary, DateTime doj)
        {
            SqlConnection connection = new SqlConnection(@"data source=ANAMIKA\SQLSERVER;initial catalog=ProjectDb;integrated security=true");
            SqlCommand command = new SqlCommand();
            command.CommandText = "insert into Employee(name, dept, salary, doj) values(@name, @dept,@salary,@doj)";
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@dept", dept);
            command.Parameters.AddWithValue("@salary", salary);
            command.Parameters.AddWithValue("@doj", doj);
            command.Connection = connection;
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

        }


        static void UpdateEmployee(int id, string dept, int salary)
        {
            SqlConnection connection = new SqlConnection(@"data source=ANAMIKA\SQLSERVER;initial catalog=ProjectDb;integrated security=true");
            SqlCommand command = new SqlCommand();
            command.CommandText = "update employee set dept = @dept, salary = @salary where id=@id";
            command.Connection = connection;
            command.Parameters.AddWithValue("@dept", dept);
            command.Parameters.AddWithValue("@salary", salary);
            command.Parameters.AddWithValue("@id", id);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        static void DeleteEmployee(int id)
        {
            SqlConnection connection = new SqlConnection(@"data source=ANAMIKA\SQLSERVER;initial catalog=ProjectDb;integrated security=true");
            SqlCommand command = new SqlCommand();
            command.CommandText = "delete employee where id=@id";
            command.Parameters.AddWithValue("@id", id);
            command.Connection = connection;
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
        static void SearchEmployeeByID(int id)
        {
            SqlConnection connection = new SqlConnection(@"data source=ANAMIKA\SQLSERVER;initial catalog=ProjectDb;integrated security=true");
            SqlCommand command = new SqlCommand("select * from Employee where id=@id", connection);
            command.Parameters.AddWithValue("@id", id);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                Console.WriteLine(reader[0] + " " + reader[1] + " " + reader[2] + reader[3]);
            }
            else
            {
                Console.WriteLine("There is no record with this ID");
            }
            connection.Close();
        }
        static int GetEmployeesCount()
        {
            SqlConnection connection = new SqlConnection(@"data source=ANAMIKA\SQLSERVER;initial catalog=ProjectDb;integrated security=true");
            SqlCommand command = new SqlCommand("select count(*) from Employee", connection);
            command.Connection = connection;
            connection.Open();
            int count = (int)command.ExecuteScalar();
            return count;

        }
        static int Menu()
        {
            Console.WriteLine("1. List of Records");
            Console.WriteLine("2. Insert Record");
            Console.WriteLine("3. Update Record");
            Console.WriteLine("4. Search Record");
            Console.WriteLine("5. Delete Record");
            Console.WriteLine("6. Get Employees Count");
            Console.WriteLine("Enter your choice");
            int ch = byte.Parse(Console.ReadLine());
            return ch;

        }
    }
}