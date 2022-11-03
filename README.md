# Database_Insurance-companies-term-paper-
Database (PSQL) and Windowf Forms application to work with it.

# Database Diagram

![](https://github.com/slezyradosti/Database_Insurance-companies-term-paper-/blob/main/DB_Kursach/Pictures/for%20github/db_diagram(white).jpg)

# About Database

## Domain

` CREATE DOMAIN op_year AS integer
	CHECK(VALUE BETWEEN 1850 AND CAST(date_part('year', current_date) AS integer)) NOT NULL; `

## Tables

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

# About application
