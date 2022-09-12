using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqAndLamdaExpessionJoins
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CNIC { get; set; }
        public int Salary { get; set; }
        public int DeptId { get; set; }
        public Employee(int id, string name, string CNIC, int salary, int deptId)
        {
            this.Id = id;
            this.Name = name;
            this.CNIC = CNIC;
            this.Salary = salary;
            DeptId = deptId;
        }
    }
}
