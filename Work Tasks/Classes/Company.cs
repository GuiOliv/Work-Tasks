using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Work_Tasks.Classes
{
    public class Company
    {
        public static List<string> PossibleDepartments = new List<string>() {"Human Resources", "Research and Development", "Support", "Marketing", "Sales"};
        public Company()
        {
            Deserialisation deserialisation = new Deserialisation();

            employees = deserialisation.DeserialiseEmployees();

            tasks = DB.DeserialiseTasks(employees);
        }

        public List<Employee> employees { get; set; } = null;
        public List<Task> tasks { get; set; } = new List<Task>();

        public void BuildTask(string name, List<string> departments, string status, string deadline, string author, string description)
        {
            tasks.Add(new Task(name, departments, (Status)Enum.Parse(typeof(Status), status), DateOnly.Parse(deadline), employees.First(w => w.fullName == author), description));

            DB.SerialiseTask(tasks[tasks.Count - 1]);
        }
    }
}
