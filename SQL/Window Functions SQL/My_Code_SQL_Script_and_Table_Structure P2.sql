-- Script to create the Product table and load data into it.

--DROP TABLE product;
CREATE TABLE product
( 
    product_category varchar(255),
    brand varchar(255),
    product_name varchar(255),
    price int
);

INSERT INTO product VALUES
('Phone', 'Apple', 'iPhone 12 Pro Max', 1300),
('Phone', 'Apple', 'iPhone 12 Pro', 1100),
('Phone', 'Apple', 'iPhone 12', 1000),
('Phone', 'Samsung', 'Galaxy Z Fold 3', 1800),
('Phone', 'Samsung', 'Galaxy Z Flip 3', 1000),
('Phone', 'Samsung', 'Galaxy Note 20', 1200),
('Phone', 'Samsung', 'Galaxy S21', 1000),
('Phone', 'OnePlus', 'OnePlus Nord', 300),
('Phone', 'OnePlus', 'OnePlus 9', 800),
('Phone', 'Google', 'Pixel 5', 600),
('Laptop', 'Apple', 'MacBook Pro 13', 2000),
('Laptop', 'Apple', 'MacBook Air', 1200),
('Laptop', 'Microsoft', 'Surface Laptop 4', 2100),
('Laptop', 'Dell', 'XPS 13', 2000),
('Laptop', 'Dell', 'XPS 15', 2300),
('Laptop', 'Dell', 'XPS 17', 2500),
('Earphone', 'Apple', 'AirPods Pro', 280),
('Earphone', 'Samsung', 'Galaxy Buds Pro', 220),
('Earphone', 'Samsung', 'Galaxy Buds Live', 170),
('Earphone', 'Sony', 'WF-1000XM4', 250),
('Headphone', 'Sony', 'WH-1000XM4', 400),
('Headphone', 'Apple', 'AirPods Max', 550),
('Headphone', 'Microsoft', 'Surface Headphones 2', 250),
('Smartwatch', 'Apple', 'Apple Watch Series 6', 1000),
('Smartwatch', 'Apple', 'Apple Watch SE', 400),
('Smartwatch', 'Samsung', 'Galaxy Watch 4', 600),
('Smartwatch', 'OnePlus', 'OnePlus Watch', 220);

--
select * from product;

-- FIRST_VALUE
-- Write query to display the most expensive product under each category (corresponding to each record)
select *,
FIRST_VALUE(product_name) over(partition by product_category order by price desc) as most_exp_product
from product;

-- LAST_VALUE
-- Write query to display the least expensive product under each category (corresponding to each record)
--Frame Clauses
-- unbounded preceding and current row ==> it means that it specifies the range our window function is supposed to consider while applying that particular window function. And by default it considers all the rows preceding the current row and also the current row itself in a particular partition (mentioned in the OVER clause).
-- unbounded preceding and unbounded following  ==> We can apply the FRAME as the ‘range between unbounded preceding and unbounded following’. Therefore using this we consider the whole partition as the unbounded following will not be bounded by the current row but will extend to the whole window.
select *,
first_value(product_name) over(partition by product_category order by price desc) as most_exp_product,
-- Note: The default FRAME is a 'range between unbounded preceding and current row'.
-- Following will not return last value due to defualt Frame Clause
last_value(product_name)
	over(partition by product_category order by price desc
	range between unbounded preceding and current row) as least_exp_till_current_product,
-- extend frame to unbound following will return last in partition
last_value(product_name)
	over(partition by product_category order by price desc
	range between unbounded preceding and unbounded following) as least_exp_product
from product;
--ROWS vs Range
--The ROWS clause does that quite literally. It specifies a fixed number of rows that precede or follow the current row regardless of their value. These rows are used in the window function.
--On the other hand, the RANGE clause logically limits the rows. That means it considers the rows based on their value compared to the current row.
--range and rows behave different only for duplicate record 
--row stick to current row for duplication data but range stick to other duplicates on his level
select *,
last_value(product_name)
	over(partition by product_category order by price desc
	range between unbounded preceding and current row) as least_exp_till_current_product,
last_value(product_name)
	over(partition by product_category order by price desc
	rows between unbounded preceding and current row) as least_exp_till_current_product
from product where product_category='Phone';
--other use cases
--note: RANGE is only supported with UNBOUNDED and CURRENT ROW window frame delimiters only
--rows between 2 preceding and 2 following ==> this will have range of 5 rows ==> 2 preceding and 2 following adn current
select *,
last_value(product_name)
	over(partition by product_category order by price desc
	rows between 2 preceding and 2 following) as least_exp_in_range_of_5_product
from product where product_category='Phone';


-- Alternate way to write SQL query using Window functions


-- NTH_VALUE 
-- Write query to display the Second most expensive product under each category.


-- NTILE
-- Write a query to segregate all the expensive phones, mid range phones and the cheaper phones.



-- CUME_DIST (cumulative distribution) ; 
/*  Formula = Current Row no (or Row No with value same as current row) / Total no of rows */

-- Query to fetch all products which are constituting the first 30% 
-- of the data in products table based on price.




-- PERCENT_RANK (relative rank of the current row / Percentage Ranking)
/* Formula = Current Row No - 1 / Total no of rows - 1 */

-- Query to identify how much percentage more expensive is "Galaxy Z Fold 3" when compared to all products.



