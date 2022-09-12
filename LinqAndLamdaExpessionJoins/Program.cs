using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;
using System.Text.RegularExpressions;

namespace LinqAndLamdaExpessionJoins
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Mapping Class Objects
            List<Employee> employees = GetEmployees();
            List<Department> departments = GetDepartments();

            //////////------------------Inner Join-----------------------/////////////
            ///An inner join returns only those records that exist in the tables. Using the "join" keyword we can do an inner join using a LINQ query.
            var innerJoin = from emp in employees
                                join dept in departments
                                on emp.DeptId equals dept.Id
                                select new
                                {
                                    EmployeeId = emp.Id,
                                    EmployeeName = emp.Name,
                                    EmployeeCNIC = emp.CNIC,
                                    EmployeeSalary = emp.Salary,
                                    DepartmentName = dept.Name,
                                };

            Console.WriteLine("Employee Id\tEmployee Name\tDepartment Name");
            foreach (var data in innerJoin)
            {
                Console.WriteLine(data.EmployeeId + "\t\t" + data.EmployeeName + "\t" + data.DepartmentName);
            }

            //////////------------------Left Join-----------------------/////////////
            //////A Left Outer join returns all records from the left table and the matching record from the right table.If there are no matching records in the right table then it returns null.If we want to do a Left Outer join in LINQ then we must use the keyword "into" and method "DefaultIfEmpty".
            var leftOuterJoin = from e in employees
                                join d in departments on e.DeptId equals d.Id into dept
                                from department in dept.DefaultIfEmpty()
                                select new
                                {
                                    EmployeeId = e.Id,
                                    EmployeeName = e.Name,
                                    EmployeeCNIC = e.CNIC,
                                    EmployeeSalary = e.Salary,
                                    DepartmentName = department.Name,
                                };
            Console.WriteLine("Employee Id\tEmployee Name\tDepartment Name");
            foreach (var data in leftOuterJoin)
            {
                Console.WriteLine(data.EmployeeId + "\t\t" + data.EmployeeName + "\t" + data.DepartmentName);
            }


            //////////------------------Right Join-----------------------/////////////
            ///A right outer join is not possible with LINQ.LINQ only supports left outer joins. If we swap the tables and do a left outer join then we can get the behavior of a right outer join.
            var rightOuterJoin = from d in departments
                                 join e in employees on d.Id equals e.DeptId into emp
                                 from employee in emp.DefaultIfEmpty()
                                 select new
                                 {
                                     EmployeeId= employee.Id,
                                     EmployeeName = employee.Name,
                                     EmployeeCNIC = employee.CNIC,
                                     EmployeeSalary = employee.Salary,
                                     DepartmentName = d.Name
                                 };
            Console.WriteLine("Employee Id\tEmployee Name\tDepartment Name");
            foreach (var data in rightOuterJoin)
            {
                Console.WriteLine(data.EmployeeId + "\t\t" + data.EmployeeName + "\t" + data.DepartmentName);
            }
            //////////------------------Full Outer Join-----------------------/////////////
            ///A full outer join is a logical union of a left outer join and a right outer join. LINQ does not support full outer joins directly, the same as right outer joins.
            var leftOuterJoin1 = from e in employees
                                join d in departments on e.DeptId equals d.Id into dept
                                from department in dept.DefaultIfEmpty()
                                select new
                                {
                                    EmployeeId= e.Id,
                                    EmployeeName = e.Name,
                                    DepartmentName = department.Name
                                };
            var rightOuterJoin1 = from d in departments
                                  join e in employees on d.Id equals e.DeptId into emp
                                 from employee in emp.DefaultIfEmpty()
                                 select new
                                 {
                                     EmployeeId= employee.Id,
                                     EmployeeName = employee.Name,
                                     DepartmentName = d.Name
                                 };
            leftOuterJoin = leftOuterJoin.Union(rightOuterJoin);
            Console.WriteLine("Employee Id\tEmployee Name\tDepartment Name");
            foreach (var data in leftOuterJoin)
            {
                if (!string.IsNullOrEmpty(data.EmployeeName))
                    Console.WriteLine(data.EmployeeId + "\t\t" + data.EmployeeName + "\t" + data.DepartmentName);
                else
                    Console.WriteLine(data.EmployeeId + "\t\t" + data.EmployeeName + "\t\t" + data.DepartmentName);
            }

            //////////------------------Cross Join in LINQ-----------------------/////////////
            ///A cross join is also known as a Cartesian Join. This join does not require any condition in the join but LINQ does not allow using the "join" keyword without any condition. Using two from clauses we can do a cross join.
            var crossJoin = from e in employees
                            from d in departments
                            select new
                            {
                                EmployeeId= e.Id,
                                EmployeeName = e.Name,
                                DepartmentName = d.Name
                            };
            Console.WriteLine("Employee Id\tEmployee Name\tDepartment Name");
            foreach (var data in crossJoin)
            {
                Console.WriteLine(data.EmployeeId + "\t\t" + data.EmployeeName + "\t" + data.DepartmentName);
            }
            //////////------------------Group join in LINQ-----------------------/////////////
            ///Generally, in SQL, a group join can be done using a "Group by" clause. There are two ways to do a group join in LINQ.
            ///1.Using INTO keyword
            var groupJoin = from d in departments
                            join e in employees on d.Id equals e.DeptId into emp
                            select new
                            {
                                DeparmentId= d.Id,
                                DeparmentName = d.Name,
                                Employee = emp
                            };
            foreach (var data in groupJoin)
            {
                Console.WriteLine("Department:" + data.DeparmentId + " - " + data.DeparmentName);
                if (data.Employee != null && data.Employee.Count() > 0)
                {
                    Console.WriteLine("Employee Id\tEmployee Name");
                    foreach (var empData in data.Employee)
                    {
                        Console.WriteLine(empData.Id + "\t\t" + empData.Name);
                    }
                }
                else
                {
                    Console.WriteLine("Department has no employee.");
                }
                Console.WriteLine("");
                Console.WriteLine("");
            }
            ///2. Using sub query
            var groupJoin1 = from d in departments
                            select new
                            {
                                DeparmentId= d.Id,
                                DeparmentName = d.Name,
                                Employee = (from e in employees
                                            where e.DeptId == d.Id
                                            select e)
                            };
            foreach (var data in groupJoin)
            {
                Console.WriteLine("Department:" + data.DeparmentId + " - " + data.DeparmentName);

                var employees2 = data.Employee as IEnumerable<Employee>;
                if (employees2 != null && employees2.Count() > 0)
                {
                    Console.WriteLine("Employee Id\tEmployee Name");
                    foreach (var empData in employees2)
                    {
                        Console.WriteLine(empData.Id + "\t\t" + empData.Name);
                    }
                }
                else
                {
                    Console.WriteLine("Department has no employee.");
                }
                Console.WriteLine("");
                Console.WriteLine("");
            }
            ////////////////////////////////////////////Linq End Here /////////////////////////////////////////////////////////

            ////////////////////////////////////////////Lamda Expression //////////////////////////////////////////////////////
            
            //////////------------------Inner Join-----------------------/////////////
            var lamdaJoin = employees.Join(departments,
                              emp => emp.DeptId,
                              dept => dept.Id,
                              (emp, dept) => new
                              {
                                  EmployeeId = emp.Id,
                                  EmployeeName = emp.Name,
                                  EmployeeCNIC = emp.CNIC,
                                  EmployeeSalary = emp.Salary,
                                  DepartmentName = dept.Name,
                              });
            Console.WriteLine("Employee Id\tEmployee Name\tDepartment Name");
            foreach (var data in innerJoin)
            {
                Console.WriteLine(data.EmployeeId + "\t\t" + data.EmployeeName + "\t" + data.DepartmentName);
            }

            //////////------------------Left Join-----------------------/////////////

            // Option 1: Expecting either 0 or 1 matches from the "Right"
            // table (Bars in this case):
            var leftOuterOneToOne = employees.GroupJoin(
                      departments,
                      emp => emp.DeptId,
                      dept => dept.Id,
                      (x, y) => new { emp = x, dept = y.SingleOrDefault() });

            // Option 2: Expecting either 0 or more matches from the "Right" table
            // (courtesy of currently selected answer):
            var leftOuterOneToMany = employees.GroupJoin(
                      departments,
                      emp => emp.DeptId,
                      dept => dept.Id,
                      (x, y) => new { Emp = x, Dept = y })
                   .SelectMany(
                       x => x.Dept.DefaultIfEmpty(),
                       (x, y) => new { emp = x.Emp, dept = y });


            //////////------------------Right Join-----------------------/////////////
            var rightOuterOneToMany = employees.GroupJoin(departments, 
                left => left.DeptId,
                right => right.Id, 
                (left, right) => new {
                    emp = right,
                    dept = left
                }).SelectMany(p => p.emp.DefaultIfEmpty(), (x, y) => new {
                    Emp = y,
                    Dept = x.dept
                });

            Console.ReadKey();
        }
        public static List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();
            employees.Add(new Employee(4, "Zahir Fuentes", "commodo tincidunt", 1, 1));
            employees.Add(new Employee(5, "Byron Vang", "Aliquam", 4, 2));
            employees.Add(new Employee(6, "Winifred Ashley", "metus urna convallis", 0, 2));
            employees.Add(new Employee(7, "Elmo Mason", "dolor vitae dolor", 3, 3));
            employees.Add(new Employee(8, "Rahim Santiago", "Nunc lectus pede ultrices", 8, 1));
            employees.Add(new Employee(9, "John", "56465464", 1000, 4));
            return employees;
        }
        public static List<Department> GetDepartments()
        {
            List<Department> departments = new List<Department>();
            departments.Add(new Department(1, "Augue Porttitor LLC"));
            departments.Add(new Department(2, "Pede Et Limited"));
            departments.Add(new Department(3, "Ipsum Dolor Corporation"));
            departments.Add(new Department(4, "Mollis Lectus Company"));
            departments.Add(new Department(5, "Viverra LLP"));
            return departments;
        }
    }
}