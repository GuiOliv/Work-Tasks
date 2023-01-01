using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Work_Tasks.Classes
{
    public class Employee
    {

        #region Constructors
        public Employee(int id, string ssn, string firstName, string lastName, string streetName, int streetNumber, string zipCode, string city, string email, string department)
        {
            this.id = id;
            this.ssn = ssn;
            this.firstName = firstName;
            this.lastName = lastName;
            this.streetName = streetName;
            this.streetNumber = streetNumber;
            this.zipCode = zipCode;
            this.city = city;
            this.email = email;
            this.department = department;
        }

        public Employee(int id, string ssn, string firstName, string lastName, string gender, string streetName, int streetNumber, string zipCode, string city, string email, string department)
        {
            this.id = id;
            this.ssn = ssn;
            this.firstName = firstName;
            this.lastName = lastName;
            this.gender = gender;
            this.streetName = streetName;
            this.streetNumber = streetNumber;
            this.zipCode = zipCode;
            this.city = city;
            this.email = email;
            this.department = department;
        }
        #endregion

        #region Properties
        public int id { get; set; }
        public string ssn { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string fullName
        {
            get
            {
                return firstName+ " " + lastName;
            }
        }
        public string gender { get; set; }
        public string streetName { get; set; }
        public int streetNumber { get; set; }
        public string zipCode { get; set; }
        public string city { get; set; }
        public string email { get; set; }
        public string department { get; set; }
        #endregion

        public override string ToString()
        {
            return fullName;
        }

    }
}
