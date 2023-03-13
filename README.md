# Database_Insurance-companies-term-paper-
Database (PSQL) and Windows Forms application to work with it.

# Database Diagram
<details>
<summary>
	
## English
	
</summary>

![](https://github.com/slezyradosti/Database_Insurance-companies-term-paper-/blob/main/DB_Kursach/Pictures/for%20github/db_diagram(white)%20eng.jpg)

</details>

<details>
<summary>	
	
## Russian
	
</summary>

![](https://github.com/slezyradosti/Database_Insurance-companies-term-paper-/blob/main/DB_Kursach/Pictures/for%20github/db_diagram(white).jpg)

</details>

# About Database
<details>
<summary>
	
## Domain
	
</summary>

` CREATE DOMAIN op_year AS integer
	CHECK(VALUE BETWEEN 1850 AND CAST(date_part('year', current_date) AS integer)) NOT NULL; `
	
</details>
	
<details>
<summary>
	
## Tables
	
</summary>

- Property types

```
CREATE TABLE property_types
(	type_id serial NOT NULL,
	property_type text COLLATE pg_catalog."default" NOT NULL,
 	CONSTRAINT typeid_pkey PRIMARY KEY (type_id),
 	CONSTRAINT property_uniq UNIQUE (property_type)
);
```

- Cities

```
CREATE TABLE cities
(
	city_id serial NOT NULL,
	city text COLLATE pg_catalog."default" NOT NULL,
	CONSTRAINT cityid_pkey PRIMARY KEY (city_id),
 	CONSTRAINT city_uniq UNIQUE (city)
); 
```

- Companies

```
CREATE TABLE companies
(
	company_id serial NOT NULL,
	company_name text COLLATE pg_catalog."default" NOT NULL,
	type_id integer NOT NULL,
	license_number integer NOT NULL,
	license_expiration_date date NOT NULL,
	city_id integer NOT NULL,
	address text COLLATE pg_catalog."default" NOT NULL,
	phone_number varchar(10) NOT NULL,
	opening_year op_year,
	license_photo bytea,
	CHECK (CAST(date_part('year', current_date) AS integer) > opening_year),
	CONSTRAINT company_pkey PRIMARY KEY (company_id),
	CONSTRAINT name_uniq UNIQUE (company_name),
	CONSTRAINT typeid_fkey FOREIGN KEY (type_id)
		REFERENCES property_types (type_id)
		ON DELETE CASCADE,
	CONSTRAINT lnumber_uniq UNIQUE (license_number),
	CONSTRAINT cityid_fkey FOREIGN KEY (city_id)
		REFERENCES cities (city_id)
		ON DELETE CASCADE,
	CONSTRAINT phnumber_uniq UNIQUE (phone_number)
);
```

- Types of insurance

```
CREATE TABLE types_of_insurance
(
	type_of_insurance_id serial NOT NULL,
	type_of_insurance text COLLATE pg_catalog."default" NOT NULL,
	CONSTRAINT instypeid_pkey PRIMARY KEY (type_of_insurance_id),
 	CONSTRAINT instype_uniq UNIQUE (type_of_insurance)
);
```

- Social status of clients

```
CREATE TABLE social_status_of_clients
(
	social_status_id serial NOT NULL,
	social_status text COLLATE pg_catalog."default" NOT NULL,
	CONSTRAINT socstatus_pkey PRIMARY KEY (social_status_id),
 	CONSTRAINT socstatus_uniq UNIQUE (social_status)
);
```

- Branches

```
CREATE TABLE branches
(
	branch_id serial NOT NULL,
	branch_name text COLLATE pg_catalog."default" NOT NULL,
	company_id integer NOT NULL, --ref
	city_id integer NOT NULL, --ref
	is_main boolean NOT NULL, -- only one 'true' 
	address text COLLATE pg_catalog."default" NOT NULL,
	phone_number varchar(10) NOT NULL,
	opening_year op_year,
	number_of_employees integer NOT NULL DEFAULT 0, --не даю это поле на ввод, добавил триггер
	CHECK(opening_year > 0),
	CONSTRAINT branchid_pkey PRIMARY KEY (branch_id),
 	--CONSTRAINT branchname_uniq UNIQUE (branch_name),
	CONSTRAINT companyid_fkey FOREIGN KEY (company_id)
		REFERENCES companies (company_id)
		ON DELETE CASCADE,
	CONSTRAINT cityid_fkey FOREIGN KEY (city_id)
		REFERENCES cities (city_id)
		ON DELETE CASCADE,
	CONSTRAINT branchphnumber_uniq UNIQUE (phone_number),
	CONSTRAINT branchNameAndCity_uniq UNIQUE (branch_name, city_id)
); 
```

- Employees

```
CREATE TABLE employees
(
	employee_id serial NOT NULL,
	branch_id integer NOT NULL,
	surname text COLLATE pg_catalog."default" NOT NULL,
	firstname text COLLATE pg_catalog."default" NOT NULL,
	lastname text COLLATE pg_catalog."default" NOT NULL,
	CONSTRAINT employee_pkey PRIMARY KEY (employee_id),
	CONSTRAINT branchid_fkey FOREIGN KEY (branch_id)
		REFERENCES branches (branch_id)
		ON DELETE CASCADE,
 	CONSTRAINT fullname_uniq UNIQUE (surname, firstname, lastname)
);
```

- Clients

```
CREATE TABLE clients
(
	client_id serial NOT NULL,
	surname text COLLATE pg_catalog."default" NOT NULL,
	firstname text COLLATE pg_catalog."default" NOT NULL,
	lastname text COLLATE pg_catalog."default" NOT NULL,
	date_of_birth date NOT NULL,
	social_status_id integer NOT NULL, --ref
	city_id integer NOT NULL, --ref
	address text NOT NULL,
	phone_number varchar(10) NOT NULL,
	CHECK (date_of_birth BETWEEN (current_date - (365*120))::date AND (current_date - (365*18))), -- 18-120 лет 
	CONSTRAINT clientid_pkey PRIMARY KEY (client_id),
	CONSTRAINT social_statusid_fkey FOREIGN KEY (social_status_id)
		REFERENCES social_status_of_clients (social_status_id)
		ON DELETE CASCADE,
	CONSTRAINT clientcityid_fkey FOREIGN KEY (city_id)
		REFERENCES cities (city_id)
		ON DELETE CASCADE,
	CONSTRAINT clientchphnumber_uniq UNIQUE (phone_number),
 	CONSTRAINT clientfullname_uniq UNIQUE (surname, firstname, lastname)
);
```

- Contracts

```
CREATE TABLE contracts
(
	contract_id serial NOT NULL,
	type_of_insurance_id integer NOT NULL, --ref
	employee_id integer NOT NULL, --ref
	client_id integer NOT NULL, --ref
	text_of_contract text NOT NULL,
	sum_of_contract integer NOT NULL,
	date_of_onclusion_contract date DEFAULT(current_date) NOT NULL,
	CHECK (sum_of_contract > 0),
	CHECK (date_of_onclusion_contract BETWEEN (current_date - 365) AND current_date), --365 дней
	CONSTRAINT contract_id PRIMARY KEY (contract_id),
	CONSTRAINT instype_fkey FOREIGN KEY (type_of_insurance_id)
		REFERENCES types_of_insurance (type_of_insurance_id)
		ON DELETE CASCADE,
	CONSTRAINT employeeid_fkey FOREIGN KEY (employee_id)
		REFERENCES employees (employee_id)
		ON DELETE CASCADE,
	CONSTRAINT contract_clientid_fkey FOREIGN KEY (client_id)
		REFERENCES clients (client_id)
		ON DELETE CASCADE
	
);
```
</details>

<details>
<summary>
	
## Indexes
	
</summary>

```
CREATE INDEX idx_opening_year ON companies(opening_year);
CREATE INDEX idx_branch_opening_year ON branches(opening_year);
CREATE INDEX idx_client_date_of_birth ON clients(date_of_birth);
CREATE INDEX idx_date_of_onclusion_contract ON contracts(date_of_onclusion_contract);
CREATE INDEX idx_sum_of_contract ON contracts(sum_of_contract);
CREATE INDEX idx_client_fio ON clients(surname, firstname, lastname);
```
	
</details>

<details>
<summary>
	
## Views
	
</summary>

```
CREATE VIEW all_companies_view AS
SELECT company_id, company_name, property_types.property_type, license_number, license_expiration_date, cities.city, address, phone_number, opening_year, license_photo FROM companies
INNER JOIN property_types ON companies.type_id=property_types.type_id
INNER JOIN cities ON companies.city_id=cities.city_id
ORDER BY company_id ASC;

CREATE VIEW all_property_types_view AS
SELECT type_id, property_type FROM property_types;

CREATE VIEW all_branches_view AS
SELECT branch_id, branch_name, companies.company_name, cities.city, is_main, branches.address, branches.phone_number, branches.opening_year, number_of_employees FROM branches
INNER JOIN companies ON branches.company_id=companies.company_id
INNER JOIN cities ON branches.city_id=cities.city_id
ORDER BY branch_id ASC;

CREATE VIEW all_employees_view AS
SELECT employee_id, employees.surname, employees.firstname, employees.lastname, concat (branches.branch_name, ', ', cities.city) branchAndCity 
FROM employees
INNER JOIN branches ON employees.branch_id=branches.branch_id
INNER JOIN cities ON branches.city_id=cities.city_id
ORDER BY employee_id ASC;


CREATE VIEW all_contracts_view AS
SELECT contract_id, types_of_insurance.type_of_insurance, concat (employees.surname, ' ', employees.firstname, ' ', employees.lastname) employeeFullName, concat (clients.surname, ' ', clients.firstname, ' ', clients.lastname) clientFullName, text_of_contract, sum_of_contract, date_of_onclusion_contract
FROM contracts
INNER JOIN types_of_insurance ON contracts.type_of_insurance_id =types_of_insurance.type_of_insurance_id
INNER JOIN employees ON contracts.employee_id=employees.employee_id
INNER JOIN clients ON contracts.client_id=clients.client_id
ORDER BY contract_id ASC;


CREATE VIEW all_types_of_insurance_view AS
SELECT type_of_insurance_id, type_of_insurance FROM types_of_insurance;

CREATE VIEW all_clients_view AS
SELECT client_id,  clients.surname, clients.firstname, clients.lastname, date_of_birth, social_status_of_clients.social_status, cities.city, address, phone_number FROM clients
INNER JOIN social_status_of_clients ON clients.social_status_id=social_status_of_clients.social_status_id
INNER JOIN cities ON clients.city_id=cities.city_id
ORDER BY client_id ASC;

CREATE VIEW all_social_status_of_clients_view AS
SELECT social_status_id, social_status FROM social_status_of_clients;

CREATE VIEW all_cities_view AS
SELECT city_id, city FROM cities;
```
	
</details>


<details>
<summary>
	
## Triggers
	
</summary>

- Employees - branches (insert)
	
```
CREATE  OR REPLACE FUNCTION add_number_employee_to_branch()
RETURNS TRIGGER
AS $$
BEGIN

	UPDATE branches
        SET number_of_employees = number_of_employees + 1
       WHERE branch_id = NEW.branch_id;

	RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER add_number_emplyees AFTER INSERT ON employees
FOR EACH ROW EXECUTE PROCEDURE add_number_employee_to_branch();
```
	
- Employees - branches (delete)
	
```
CREATE OR REPLACE FUNCTION remove_number_employee_to_branch()
RETURNS TRIGGER
AS $$
BEGIN
--ПРОВЕРКА НА ОТРИЦАТЕЛЬНОЕ ЧИСЛО?
	--IF number_of_employees > 0 THEN
	UPDATE branches
        SET number_of_employees = number_of_employees - 1
       	WHERE branch_id = OLD.branch_id;
		RETURN OLD;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER remove_number_emplyees AFTER DELETE ON employees
FOR EACH ROW EXECUTE PROCEDURE remove_number_employee_to_branch();
```
	
- Employees - branches (update)
	
```
CREATE OR REPLACE FUNCTION update_number_employee_to_branch()
RETURNS TRIGGER
AS $$
BEGIN
	UPDATE branches
        SET number_of_employees = number_of_employees - 1
       	WHERE branch_id = OLD.branch_id;
	UPDATE branches
		SET number_of_employees = number_of_employees + 1
       WHERE branch_id = NEW.branch_id;
		RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER update_number_emplyees AFTER UPDATE ON employees
FOR EACH ROW EXECUTE PROCEDURE update_number_employee_to_branch();
```
	
- Only one main branch in company (insert)
	
```
CREATE OR REPLACE FUNCTION insert_ismain_branch()
RETURNS TRIGGER
AS $$
BEGIN
	IF ( SELECT COUNT(*) FROM branches
  	WHERE (branches.company_id = new.company_id)) >= 1 THEN
		new.is_main = false;
		RETURN NEW;
	ELSE
		new.is_main = true;
		RETURN NEW;
	END IF;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER insert_ismain_branch BEFORE INSERT ON branches
FOR EACH ROW EXECUTE PROCEDURE insert_ismain_branch();
```
	

- Only one main branch in company (delete)
	
```
CREATE OR REPLACE FUNCTION remove_ismain_branch()
RETURNS TRIGGER
AS $$
BEGIN
	IF (OLD.is_main = true) THEN
		UPDATE branches
		SET is_main = true
		WHERE branches.opening_year = (SELECT MIN(branches.opening_year) FROM branches WHERE old.company_id = branches.company_id); --AND old.company_id = branches.company_id; 
	--RETURN NEW;
	END IF;
	RETURN OLD;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER delete_ismain_branch AFTER DELETE ON branches
FOR EACH ROW EXECUTE PROCEDURE remove_ismain_branch();
```
	
- Only one main branch in company (update)
	
```
CREATE OR REPLACE FUNCTION update_ismain_branch()
RETURNS TRIGGER
AS $$
BEGIN --если изменяемый филиал был главным -> главным станет другой
	IF (OLD.is_main = true) THEN
		UPDATE branches
		SET is_main = true
		WHERE branches.opening_year = (SELECT MIN(branches.opening_year) FROM branches WHERE old.company_id = branches.company_id); --AND old.company_id = branches.company_id; 
	
	END IF;

	IF ( SELECT COUNT(*) FROM branches -- если у другой компании уже больше 1 филиала (первый всегда главный)
  		WHERE (branches.company_id = new.company_id)) > 1 THEN -- то обновленный будет обычным
			UPDATE branches
			SET is_main = false
			WHERE branch_id = new.branch_id;
	ELSE -- иначе обновленный станет главным
		UPDATE branches
		SET is_main = true
		WHERE branch_id = new.branch_id;
	END IF;
	
	RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER update_ismain_branch AFTER UPDATE ON branches
FOR EACH ROW 
WHEN (OLD.company_id is DISTINCT FROM NEW.company_id) 
EXECUTE PROCEDURE update_ismain_branch();
```
	
</details>

<details>
<summary>
	
## Queries
	
</summary>

- Inner Join With Foreign Key1

```
SELECT branch_id, branch_name, cities.city, is_main,
			branches.address, branches.phone_number, branches.opening_year, number_of_employees
			FROM branches
			INNER JOIN cities ON branches.city_id=cities.city_id
			WHERE branches.city_id = {0}
			GROUP BY branch_id, cities.city	
```
	
- Inner Join With Foreign Key2

```
SELECT employee_id, employees.surname, employees.firstname,
			employees.lastname, concat (branches.branch_name, ', ', cities.city) branchAndCity
			FROM employees
			INNER JOIN branches ON employees.branch_id=branches.branch_id
			INNER JOIN cities ON branches.city_id=cities.city_id
			WHERE employees.branch_id = {0}
			GROUP BY employee_id, branchAndCity;
```
	
- left Outer Join

```
SELECT branch_id, branch_name, companies.company_name, city, is_main, number_of_employees
			FROM branches
			LEFT OUTER JOIN companies ON branches.company_id = companies.company_id AND companies.license_photo IS NOT NULL
			LEFT OUTER JOIN cities ON branches.city_id= cities.city_id
			ORDER BY branch_id ASC
```
	
- right Outer Join

```
SELECT property_types.type_id , property_type , companies.company_name FROM property_types
			RIGHT OUTER JOIN companies ON companies.type_id = property_types.type_id AND companies.license_photo IS NOT NULL
			ORDER BY property_types.type_id ASC
```
	
- query On Query By Left Join Principle

```
SELECT companies.company_id, 
			(SELECT company_name FROM companies WHERE cmp.company_id = companies.company_id)
			FROM companies
			LEFT OUTER JOIN(SELECT DISTINCT company_id FROM branches
			WHERE is_main = true) cmp ON cmp.company_id = companies.company_id
			GROUP BY companies.company_id, cmp.company_id
			ORDER BY companies.company_id
```
	
- total Including

```
SELECT COUNT(client_id),
			SUM(CASE WHEN current_date - date_of_birth >= 18*365  AND current_date - date_of_birth< 20*365 THEN 1 ELSE 0 
			END) AS about18yoLower20yo,
			SUM(CASE WHEN current_date - date_of_birth >= 20 * 365  AND current_date - date_of_birth < 30 * 365 THEN 1 ELSE 0 
			END) AS about20yoLower30yo,
			SUM(CASE WHEN current_date - date_of_birth >= 30 * 365  AND current_date - date_of_birth < 45 * 365 THEN 1 ELSE 0
			END) AS about30yoLower45yo,
			SUM(CASE WHEN current_date - date_of_birth >= 45 * 365 THEN 1 ELSE 0
			END) AS about45yo
			FROM clients
```
	
- summary Queries With Condition On Data By Value

```
SELECT clients.client_id,
			concat (clients.surname, ' ', clients.firstname, ' ', clients.lastname) clientFullName, SUM(sum_of_contract)
			FROM contracts
			INNER JOIN clients ON contracts.client_id=clients.client_id
			WHERE(SELECT SUM(sum_of_contract) FROM contracts WHERE contracts.client_id= clients.client_id) >= {0}
			GROUP BY clients.client_id, clientFullName
			ORDER BY clientFullName ASC	
```
	
- summary Queries With Condition On Data By Mask

```
SELECT branch_id,
			concat (branch_name, ', ', city) branchNameAndCity, phone_number FROM all_branches_view
			WHERE phone_number LIKE '{0}%'
			GROUP BY branch_id, branchNameAndCity, phone_number ORDER BY branch_id ASC
```
	
- final Query With Condition On Data And Groups

```
SELECT company_name, COUNT(branches.branch_id)
			FROM companies 
			INNER JOIN branches ON branches.company_id=companies.company_id
			WHERE branches.opening_year BETWEEN {1} AND {2}
			GROUP BY company_name, companies.company_id
			HAVING companies.company_id = {0}
			ORDER BY company_name
```
	
- request On Request Based On Principle Of Final Request (Display information about contracts, the amount of which exceeds the average amount)

```
SELECT contract_id, types_of_insurance.type_of_insurance, 
			concat (employees.surname, ' ', employees.firstname, ' ', employees.lastname) employeeFullName,
			concat(clients.surname, ' ', clients.firstname, ' ', clients.lastname) clientFullName,
			text_of_contract, sum_of_contract, date_of_onclusion_contract
			FROM contracts
			INNER JOIN types_of_insurance ON contracts.type_of_insurance_id =types_of_insurance.type_of_insurance_id 
			INNER JOIN employees ON contracts.employee_id= employees.employee_id 
			INNER JOIN clients ON contracts.client_id= clients.client_id
			WHERE sum_of_contract > (SELECT avg(contracts.sum_of_contract) FROM contracts)
			GROUP BY contract_id, type_of_insurance, employeeFullName, clientFullName, sum_of_contract
			ORDER BY sum_of_contract
```
	
- queries With Subqueries Using With (display top employees by the number of signed contracts)

```
WITH employeeContracts AS 
			(SELECT employee_id, concat (employees.surname, ' ', employees.firstname, ' ', employees.lastname) employeeFullName, 
			(SELECT COUNT(contract_id) FROM contracts WHERE contracts.employee_id = employees.employee_id) as totalConracts 
			FROM employees 
			GROUP BY employees.employee_id), 
			topEmployees AS( 
			SELECT employee_id FROM employeeContracts 
			WHERE totalConracts > (SELECT MAX(totalConracts) * 0.85 FROM employeeContracts)) 
			SELECT employee_id, employeeFullName, totalConracts
			FROM employeeContracts 
			WHERE employee_id IN(SELECT employee_id FROM topEmployees) 
			GROUP BY employee_id, employeeFullName, totalConracts
			ORDER BY totalConracts DESC
```
	
- special Query2 (Determine the average number of customers for each branch and for each company)

```
WITH companiesAndClients AS ( 
			SELECT companies.company_id, company_name, AVG(DISTINCT clients.client_id ) as countOfCimpanyClietns FROM companies 
			LEFT JOIN branches ON branches.company_id=companies.company_id 
			LEFT JOIN employees ON employees.branch_id= branches.branch_id 
			LEFT JOIN contracts ON contracts.employee_id = employees.employee_id 
			LEFT JOIN clients ON contracts.client_id= clients.client_id 
			GROUP BY  companies.company_id, company_name), 
			branchesAndClients AS( 
			SELECT branches.company_id, branches.branch_id, concat (branch_name, ', ', cities.city) branchAndCity, 
			AVG(DISTINCT clients.client_id ) as countOfBranchClietns, is_main 
			FROM branches 
			INNER JOIN cities ON branches.city_id=cities.city_id 
			LEFT JOIN employees ON employees.branch_id= branches.branch_id 
			LEFT JOIN contracts ON contracts.employee_id = employees.employee_id 
			LEFT JOIN clients ON contracts.client_id= clients.client_id 
			GROUP BY branches.branch_id, branchAndCity) 
			SELECT DISTINCT company_name, countOfCimpanyClietns, branchesAndClients.branchAndCity, branchesAndClients.countOfBranchClietns 
			FROM companiesAndClients 
			RIGHT OUTER JOIN branchesAndClients ON companiesAndClients.company_id = branchesAndClients.company_id AND branchesAndClients.is_main = true
			ORDER BY company_name, branchAndCity 
```
	
- special Query3 (Determine the number of clients attracted by insurance companies for a specified period (several years) and income from these transactions)

```
SELECT company_name, count( DISTINCT clients.client_id ) as countOfCompanyClietns,
			SUM(sum_of_contract) as sumOfCompcontracts 
			FROM companies 
			LEFT JOIN branches ON branches.company_id=companies.company_id 
			LEFT JOIN employees ON employees.branch_id=branches.branch_id 
			LEFT JOIN contracts ON contracts.employee_id = employees.employee_id 
			LEFT JOIN clients ON contracts.client_id= clients.client_id 
			WHERE date_of_onclusion_contract BETWEEN {0} AND {1} 
			GROUP BY company_name 
			UNION ALL 
			SELECT 'ВСЕГО', count(DISTINCT clients.client_id ) as countOfCompanyClietns, SUM(sum_of_contract) as sumOfCompcontracts 
			FROM companies 
			LEFT JOIN branches ON branches.company_id=companies.company_id 
			LEFT JOIN employees ON employees.branch_id= branches.branch_id 
			LEFT JOIN contracts ON contracts.employee_id = employees.employee_id 
			LEFT JOIN clients ON contracts.client_id= clients.client_id 
			WHERE date_of_onclusion_contract BETWEEN {0} AND {1} 
			ORDER BY countOfCompanyClietns DESC, sumOfCompcontracts DESC, company_name DESC 
```
	
</details>

# About application

<details>
<summary>
	
## USER'S MANUAL
	
</summary>

For the correct operation of the program, a computer with Windows 7 or higher is required.

To work with the program, you need to run the DB_Kursach.exe executable file.

Authorization is required to continue working with the program. If the correct data is entered, the authorization window will close and the main menu will start. Otherwise, the program will give an authorization error.
To switch between tables, directories, queries and other elements, there is a top panel of the main menu. All the elements of the main menu panel have self-explanatory names and are intuitive.
To the right of each table there are buttons: add an entry, delete an entry, edit an entry, select all elements, search, update. These buttons are visualized as icons.

Clicking on the buttons for adding a record, editing a record, search, will open a new form of the corresponding action and the table on which they were clicked. In this form, you must fill in the proposed fields and click the summary button.

The search form provides for empty fields, the add and edit form reports an error with empty fields or incorrect data. The search result is displayed in the corresponding table. You can return to the original table by selecting it in the main menu bar.

By clicking on the delete button, only the selected rows of the corresponding table are deleted. Delete can be used after applying the search.

The Select All Items button will select all the items in the table on which the button was pressed. If all elements have already been selected, the selection will be deselected.
You can select one or many elements directly on the table using LMB.

The update button updates the corresponding table but does not send any query.

There is a compound form for items from the Customers table. To open a composite form, it is enough to double-click on the required element from the "Clients" table. The compound form has three sorting buttons: by date, by amount, and by type. The compound form has buttons similar to the main menu for adding, deleting, editing, selecting all elements, and updating. By clicking on the button for adding an entry, a form for adding a contract will open, similar to the form for adding any other entry, except that you cannot change the client's data.

Through the main menu bar you can get to the "Requests" form. When working with the Requests form, the buttons for adding, deleting, editing, and searching for records are not available. This form has: a drop-down list with implemented queries, various data entry fields, an “Apply” button and an “Export to Excel” button. Data entry fields become available and vice versa depending on the selected query. The "Export to Excel" button becomes available after the request is successfully completed.
 You can export to an Excel document the result of each of the submitted queries. When exporting, the user chooses the place and names of the Excel document. After saving the document, the user is prompted to open it immediately.
The main menu panel in the "Queries" section contains the following types: one-dimensional (in the form of a pyramid), columnar, three-dimensional columnar. Diagrams are opened in a separate window and are intended to visualize some queries.
The other list contains buttons for generating elements, clearing elements, and help.

Generation fills all tables and directories. In this case, the data of directories is filled with data for directories from files, and is not generated. Therefore, there is no need to fill in reference books before generation. You can generate data multiple times. At the same time, if the directory files are not updated, the contents of the directories will remain unchanged. In the tables whose data is generated, the old values ​​will remain and new ones will be added.
Clearing all items deletes the data of all tables and updates the values ​​of all IDs.

The help button opens a file containing the user manual.
	
</details>
	
<details>
<summary>
	
## Screens of views
	
</summary>

- Table clietns (or any table)

![](https://github.com/slezyradosti/Database_Insurance-companies-term-paper-/blob/main/DB_Kursach/Pictures/for%20github/table_clients.png)
	
- View clients-contracts
	
![](https://github.com/slezyradosti/Database_Insurance-companies-term-paper-/blob/main/DB_Kursach/Pictures/for%20github/client_contracts.png)
	
- Add company (any other - same)
	
![](https://github.com/slezyradosti/Database_Insurance-companies-term-paper-/blob/main/DB_Kursach/Pictures/for%20github/client_contracts.png)
	
- Example of query
	
![](https://github.com/slezyradosti/Database_Insurance-companies-term-paper-/blob/main/DB_Kursach/Pictures/for%20github/queries.png)
	
- Example of import query result to Excele
	
![](https://github.com/slezyradosti/Database_Insurance-companies-term-paper-/blob/main/DB_Kursach/Pictures/for%20github/export_to_exel.png)
	
</details>
