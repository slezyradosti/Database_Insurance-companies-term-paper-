# Database_Insurance-companies-term-paper-
Database (PSQL) and Windowf Forms application to work with it.

# Database Diagram

![](https://github.com/slezyradosti/Database_Insurance-companies-term-paper-/blob/main/DB_Kursach/Pictures/for%20github/db_diagram(white).jpg)

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

# About application
