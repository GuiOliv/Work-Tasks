using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Work_Tasks.Classes
{
    public static class DB
    {
        static SqlConnection sqlConnection = new SqlConnection("Server=DESKTOP-R029ABL\\SQLEXPRESS;Database=WorkTask;Trusted_Connection=True;TrustServerCertificate=True;");

        public static void SerialiseTask(Task task)
        {
            Dictionary<int, string> _departID= new Dictionary<int, string>();
            try
            {
                sqlConnection.Open();
                new SqlCommand($"INSERT INTO Task (Name, Status, Deadline, Author, Description) VALUES ('{task.name}', '{task.status.ToString()}', '{task.deadline}', '{task.author.fullName}','{task.description}')", sqlConnection).ExecuteNonQuery();

                SqlDataReader sqlData = new SqlCommand("SELECT Id, Department FROM Departments", sqlConnection).ExecuteReader();

                while (sqlData.Read())
                {
                    if (task.departments.Contains(sqlData[1]))
                    {
                        _departID.Add((int)sqlData[0], (string)sqlData[1]);
                    }
                }
                sqlData.Close();
                foreach (var id in _departID.Keys)
                {
                    new SqlCommand($"INSERT INTO Task_Department VALUES('{task.name}', {id})", sqlConnection).ExecuteNonQuery();
                }
            }
            catch (InvalidCastException)
            {
                throw new InvalidCastException("Invalid value");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally { sqlConnection.Close(); }
        }

        public static List<Task> DeserialiseTasks(List<Employee> _employees)
        {
            List<Task> _tasks = new List<Task>();
            SqlConnection _sqlConnectionDepart = new SqlConnection("Server=DESKTOP-R029ABL\\SQLEXPRESS;Database=WorkTask;Trusted_Connection=True;TrustServerCertificate=True;");
            _sqlConnectionDepart.Open();

            try
            {
                sqlConnection.Open();

                SqlDataReader sqlData = new SqlCommand("SELECT * FROM Task" ,sqlConnection).ExecuteReader();

                while (sqlData.Read())
                {
                    List<string> _departments = new List<string>();
                    SqlDataReader sqlDataDepart = new SqlCommand($"SELECT Departments.Department FROM Task_Department INNER JOIN Departments ON Departments.Id = Task_Department.DepartmentId INNER JOIN Task ON Task.Name = Task_Department.TaskName WHERE Task.Name = '{sqlData[0]}'", _sqlConnectionDepart).ExecuteReader();

                    while (sqlDataDepart.Read())
                    {
                        _departments.Add((string)sqlDataDepart[0]);
                    }
                    sqlDataDepart.Close();
    
                    _tasks.Add(new Task((string)sqlData[0], _departments, (Status)Enum.Parse(typeof(Status), (string)sqlData[1]), DateOnly.Parse((string)sqlData[2]), _employees.First(w => w.fullName == (string)sqlData[3]), (string)sqlData[4]));
                }
                sqlData.Close();
                return _tasks;
            }
            catch (InvalidCastException)
            {
                throw new InvalidCastException("Invalid value");
            }
            catch (InvalidOperationException)
            {
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally { sqlConnection.Close(); _sqlConnectionDepart.Close(); }
        }

        public static void DeleteTask(string name)
        {
            try
            {
                sqlConnection.Open();
                new SqlCommand($"DELETE FROM Task WHERE Name = '{name}'", sqlConnection).ExecuteNonQuery();
                new SqlCommand($"DELETE FROM Task_Department WHERE TaskName = '{name}'", sqlConnection).ExecuteNonQuery();

            }
            catch (InvalidCastException ex)
            {
                throw new Exception(ex.Message);
            }
            finally { sqlConnection.Close(); }
        }
    }
}
