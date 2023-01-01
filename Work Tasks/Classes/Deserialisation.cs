using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.ObjectModel;
using System.Reflection;
using Azure.Core.Pipeline;
using System.Diagnostics;

namespace Work_Tasks.Classes
{
    public class Deserialisation
    {
        Stream Stream { get; set; }
        public List<Employee> DeserialiseEmployees()
        {
            List<Employee> employees = new List<Employee>();

            Stream = FileSystem.Current.OpenAppPackageFileAsync("MOCK_EMPLOYEE_DATA.csv").Result;

            using (StreamReader csv =  new StreamReader(Stream))
            {
                while (!csv.EndOfStream)
                {
                    var line = csv.ReadLine();
                    var values = line.Split(',');
                    if (Company.PossibleDepartments.Contains(values[10]))
                    {
                        employees.Add(new Employee(int.Parse(values[0]), values[1], values[2], values[3], values[4], values[5], int.Parse(values[6]), values[7], values[8], values[9], values[10]));
                    }
                }
            }

            return employees;
        }

    }
}
