
drop table employee;
create table employee
( emp_ID int
, emp_NAME varchar(50)
, DEPT_NAME varchar(50)
, SALARY int);

insert into employee values(101, 'Mohan', 'Admin', 4000);
insert into employee values(102, 'Rajkumar', 'HR', 3000);
insert into employee values(103, 'Akbar', 'IT', 4000);
insert into employee values(104, 'Dorvin', 'Finance', 6500);
insert into employee values(105, 'Rohit', 'HR', 3000);
insert into employee values(106, 'Rajesh',  'Finance', 5000);
insert into employee values(107, 'Preet', 'HR', 7000);
insert into employee values(108, 'Maryam', 'Admin', 4000);
insert into employee values(109, 'Sanjay', 'IT', 6500);
insert into employee values(110, 'Vasudha', 'IT', 7000);
insert into employee values(111, 'Melinda', 'IT', 8000);
insert into employee values(112, 'Komal', 'IT', 10000);
insert into employee values(113, 'Gautham', 'Admin', 2000);
insert into employee values(114, 'Manisha', 'HR', 3000);
insert into employee values(115, 'Chandni', 'IT', 4500);
insert into employee values(116, 'Satya', 'Finance', 6500);
insert into employee values(117, 'Adarsh', 'HR', 3500);
insert into employee values(118, 'Tejaswi', 'Finance', 5500);
insert into employee values(119, 'Cory', 'HR', 8000);
insert into employee values(120, 'Monica', 'Admin', 5000);
insert into employee values(121, 'Rosalin', 'IT', 6000);
insert into employee values(122, 'Ibrahim', 'IT', 8000);
insert into employee values(123, 'Vikram', 'IT', 8000);
insert into employee values(124, 'Dheeraj', 'IT', 11000);
COMMIT;


/* **************
   Video Summary
 ************** */

select * from employee;

-- Using Aggregate function as Window Function
-- Without window function, SQL will reduce the no of records.
-- max salary from emplyees
select Max(SALARY) as max_salary from employee;

--max salary group by dept
select DEPT_NAME as deptName, MAX(SALARY) as max_salary from employee
group by DEPT_NAME;

--over function is used to make paritions of windows
select e.* , Max(SALARY) over() as max_salary
from employee as e;

select e.* , Max(SALARY) over(partition by dept_name) as max_salary
from employee as e;

--start Imp concept
--Row_Number() will generate a unique number for every row, even if one or more rows has the same value.
--RANK() will assign the same number for the row which contains the same value and skips the next number.
--DENSE_RANK () will assign the same number for the row which contains the same value without skipping the next number.
--end
--note*: order by is must in above mentioned fucntions over clause
select e.* ,
row_number() over(order by emp_ID) ROWNUMBER
from employee e;

select e.*,
ROW_NUMBER() over(partition by dept_name order by e.emp_id) as RowNumber
from employee e;

--fetch first two employees to join company by each dept
select x.* from (select e.*,
row_number() over(partition by dept_name order by emp_ID) as rowNumber
from employee e) as x
where rowNumber < 3;

--fetch  top 3 employee from each dept earning max salary
--order by column you want to assign rank
select x.* from (select e.*,
	rank() over(partition by dept_name order by salary desc) rnkNumber
	from employee e)
as x
where x.rnkNumber<4;
--fetch all employee from each dept earning max 3 salaries
select e.*,
dense_rank() over(partition by dept_name order by salary desc) dense_rnkNumber
from employee e;

--difference b/w rank vs dense_rank vs row_numer
select e.*,
rank() over(partition by dept_name order by salary desc) RankNumber,
dense_rank() over(partition by dept_name order by salary desc) DENSERANKNumber,
row_number() over(partition by dept_name order by salary desc) ROWNUMBER
from employee e;

--The LAG() function allows access to a value stored in a different row above the current row. 
--The Lead() function allows access to a value stored in a different row below the current row. 

--lag
select e.*,
lag(SALARY) over(partition by dept_name order by salary desc) as prev_emp_salary
from employee e;

select e.*,
lag(SALARY, 2, 0) over(partition by dept_name order by salary desc) as prev_second_last_emp_salary
from employee e;
--Note: Just like LAG(), the LEAD() function takes three arguments: the name of a column or an expression, the offset to be skipped below, and the default value to be returned if the stored value obtained from the row below is empty. Only the first argument is required. The third argument, the default value, can be specified only if you specify the second argument, the offset.
--lead
select e.*,
lag(SALARY) over(partition by dept_name order by salary desc) as next_emp_salary,
lead(SALARY) over(partition by dept_name order by salary desc) as next_emp_salary
from employee e;


-- fetch a query to display if the salary of an employee is higher, lower or equal to the previous employee
select e.*,
	lag(SALARY) over(partition by dept_name order by salary desc) as next_emp_salary,
	case when SALARY > lag(SALARY) over(partition by dept_name order by salary desc) then 'Higher than pervious employee'
	when SALARY < lag(SALARY) over(partition by dept_name order by salary desc) then 'Lower than pervious employee'
from employee e;
