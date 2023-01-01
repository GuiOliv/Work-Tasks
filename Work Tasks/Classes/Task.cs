using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Work_Tasks.Classes
{
    public class Task
    {
        public Task(string name, List<string> departments, Status status, DateOnly deadline, Employee author, string description)
        {
            this.name = name;
            this.departments = departments;
            this.status = status;
            this.deadline = deadline;
            this.author = author;
            this.description = description;

            AddEmployee(author);
        }

        public string name { get; private set; }
        public List<string> departments { get; private set; } = new List<string>();
        public string departmentsString
        {
            get
            {
                string _ = "";
                foreach (var department in departments)
                {
                    _ += department + (department == departments.Last() ? "": ", ");
                }
                return _;
            }
        }
        public Status status { get;  set; }
        public DateOnly deadline { get; private set; }
        public Employee author { get; set; }
        public string description { get; private set; }
        public List<Employee> employees { get; private set; } = new List<Employee>();
        public string employeesString
        {
            get
            {
                string _ = "";
                foreach (var employee in employees)
                {
                    _ += employee + (employee == employees.Last() ? "" : Environment.NewLine);
                }
                return _;
            }
        }
        public Array statusEnum
        {
            get
            {
                return Enum.GetValues(typeof(Status));
            }
        }

        public void AddEmployee(Employee employee)
        {
            if (departments.Contains(employee.department))
            {
                employees.Add(employee);
            }
        }

        public override string ToString()
        {
            return name;
        }
    }

    public enum Status
    {
        Open,
        InProgress,
        Completed,
        Blocked,
        Cancelled
    }
}
