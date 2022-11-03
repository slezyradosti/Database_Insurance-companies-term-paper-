namespace DB_Kursach
{
	static class Queries
	{
		static public string[] queryNames =
		{
			"Вывести филиалы в определенном городе", "Вывести сотрудников определенного филиала", "Вывести информацию о филиалах и названия компаний у тех филиалов, компании чьих имеют фото лицензии",
			"Вывести типы собственности и, у компаний которых имеются фото лицензии", "Вывести компании, у которых есть главный офис", "Вывести количество договоров у клиентов",
			"Вывести число всего клиентов, в том числе по возрастам", "Вывести клиентов, у которых общая сумма на которую заключены договоры больше чем определенное число",
			"Вывести филиалы, у который номер телефона начинается с определенного значения", "Вывести клиентов, у которых сумма договора меньше чем 10000",
			"Вывести договоры определенного сотрудника", "Вывести компании и общее число сотрудников в их филиалах", "Вывести число филиалов у опред. компании, кот. были открыты в определенный период времени (годы)",
			"Вывести информацию о договорах, сумма которых превышает среднюю сумму", "Объединить сумму договоров каждого сотрудника филиала и общую сумму договоров по филиалу",
			"Вывести договоры всех филиалов определенной компании", "Вывести договоры филиалов из всех городов, кроме определенного города", "Вывести компании, которые заключили сделки на самую максимальную и минимальную сумму",
			"Вывести топовых сотрудников по количеству заключенных договоров"
 		};

		static public string[][] queryColumnsNames =
		{
			new string[] {"ID", "Филиал", "Город", "Главный офис", "Адрес", "Телефон", "Год открытия", "Число сотрудников"},
			new string[] {"ID", "Фамилия", "Имя", "Отчество", "Филиал"},
			new string[] {"ID", "Филиал", "Компания", "Город", "Главный офис", "Число сотрудников"},
			new string[] { "ID", "Тип собственности", "Компания"},
			new string[] {"ID", "Компания"},
			new string[] {"ID", "ФИО клиента", "Число договоров"},
			new string[] {"Всего клиентов", "Старше 18 и младше 20", "Старше 20 и младше 30", "Старше 30 и младше 45", "Старше 45" },
			new string[] {"ID", "ФИО клиента", "Общая сумма договоров" },
			new string[] {"ID", "Филиал", "Телефон" },
			new string[] {"ФИО клиента", "Сумма договора" },
			new string[] {"ID", "Вид страхования", "ФИО клиента", "Текст", "Сумма", "Дата заключения" },
			new string[] {"ID", "Компания", "Число сотрудников" },
			new string[] {"Компания", "Число филиалов"},
			new string[] {"ID", "Вид страхования", "ФИО сотрудника", "ФИО клиента", "Текст", "Сумма", "Дата заключения" },
			new string[] {"Филиал", "ФИО сотрудника", "Сумма договоров" },
			new string[] {"ID", "Вид страхования", "Филиал", "ФИО сотрудника", "ФИО клиента", "Текст", "Сумма", "Дата заключения" },
			new string[] {"ID", "Вид страхования", "Филиал", "ФИО сотрудника", "ФИО клиента", "Текст", "Сумма", "Дата заключения" },
			new string[] {"Компания", "Сумма договора"},
			new string[] {"ID", "ФИО сотрудника", "Количество заключенных договоров" }
 		};

		static public string[] specialQueryNames =
		{
			"•Определить три филиала в каждом городе, пользующиеся наибольшим спросом и всем городам в целом",
			"•Определить среднее количество клиентов по каждому филиалу и по каждой компании",
			"•Определить кол-во клиентов привлеченных стр. компаниями за указ. период и доход по этим сделкам",
		};

		static public string[][] specialQueryColumnsNames =
		{
			new string[] {"ID", "Филиал", "Город", "Количество договоров" },
			new string[] {"Компания", "Среднее количество клиентов", "Филиал", "Среднее количество клиентов"},
			new string[] {"Компания", "Количество клиентов", "Общий доход по сделкам"}
 		};

		static public string innerJoinWithForeignKey1 = "SELECT branch_id, branch_name, cities.city, is_main, " +
			"branches.address, branches.phone_number, branches.opening_year, number_of_employees " +
			"FROM branches " +
			"INNER JOIN cities ON branches.city_id=cities.city_id " +
			"WHERE branches.city_id = {0} " +
			"GROUP BY branch_id, cities.city ";
		static public string innerJoinWithForeignKey2 = "SELECT employee_id, employees.surname, employees.firstname, " +
			"employees.lastname, concat (branches.branch_name, ', ', cities.city) branchAndCity " +
			"FROM employees " +
			"INNER JOIN branches ON employees.branch_id=branches.branch_id " +
			"INNER JOIN cities ON branches.city_id=cities.city_id " +
			"WHERE employees.branch_id = {0} " +
			"GROUP BY employee_id, branchAndCity; ";
		//innerJoinWithDate1 - clientContracts
		//innerJoinWithDate2 - clients поиск
		static public string leftOuterJoin = "SELECT branch_id, branch_name, companies.company_name, city, is_main, number_of_employees " +
			"FROM branches " +
			"LEFT OUTER JOIN companies ON branches.company_id = companies.company_id AND companies.license_photo IS NOT NULL " +
			"LEFT OUTER JOIN cities ON branches.city_id= cities.city_id " +
			"ORDER BY branch_id ASC ";
		static public string rightOuterJoin = "SELECT property_types.type_id , property_type , companies.company_name FROM property_types " +
			"RIGHT OUTER JOIN companies ON companies.type_id = property_types.type_id AND companies.license_photo IS NOT NULL " +
			"ORDER BY property_types.type_id ASC ";
		static public string queryOnQueryByLeftJoinPrinciple = "SELECT companies.company_id, " +
			"(SELECT company_name FROM companies WHERE cmp.company_id = companies.company_id) " +
			"FROM companies " +
			"LEFT OUTER JOIN(SELECT DISTINCT company_id FROM branches " +
			"WHERE is_main = true) cmp ON cmp.company_id = companies.company_id " +
			"GROUP BY companies.company_id, cmp.company_id " +
			"ORDER BY companies.company_id ";
		static public string finalQueryWithoutCondition = "SELECT clients.client_id, " +
			"concat (clients.surname, ' ', clients.firstname, ' ', clients.lastname) clientFullName, COUNT(*) countClientContracts " +
			"FROM contracts " +
			"INNER JOIN clients ON contracts.client_id=clients.client_id " +
			"GROUP BY clients.client_id, clientFullName " +
			"ORDER BY clientFullName ASC ";
		//всего в том числе
		static public string totalIncluding = "SELECT COUNT(client_id), " +
			"SUM(CASE WHEN current_date - date_of_birth >= 18*365  AND current_date - date_of_birth< 20*365 THEN 1 ELSE 0 " +
			"END) AS about18yoLower20yo, " +
			"SUM(CASE WHEN current_date - date_of_birth >= 20 * 365  AND current_date - date_of_birth < 30 * 365 THEN 1 ELSE 0 " +
			"END) AS about20yoLower30yo, " +
			"SUM(CASE WHEN current_date - date_of_birth >= 30 * 365  AND current_date - date_of_birth < 45 * 365 THEN 1 ELSE 0 " +
			"END) AS about30yoLower45yo, " +
			"SUM(CASE WHEN current_date - date_of_birth >= 45 * 365 THEN 1 ELSE 0 " +
			"END) AS about45yo " +
			"FROM clients ";
		static public string summaryQueriesWithConditionOnDataByValue = "SELECT clients.client_id, " +
			"concat (clients.surname, ' ', clients.firstname, ' ', clients.lastname) clientFullName, SUM(sum_of_contract) " +
			"FROM contracts " +
			"INNER JOIN clients ON contracts.client_id=clients.client_id " +
			"WHERE(SELECT SUM(sum_of_contract) FROM contracts WHERE contracts.client_id= clients.client_id) >= {0} " +
			"GROUP BY clients.client_id, clientFullName " +
			"ORDER BY clientFullName ASC ";
		static public string summaryQueriesWithConditionOnDataByMask = "SELECT branch_id, " +
			"concat (branch_name, ', ', city) branchNameAndCity, phone_number FROM all_branches_view " +
			"WHERE phone_number LIKE '{0}%' " +
			"GROUP BY branch_id, branchNameAndCity, phone_number ORDER BY branch_id ASC ";
		static public string summaryQueriesWithConditionOnDataByIndex = "SELECT (SELECT concat(clients.surname, ' ', clients.firstname, ' ', clients.lastname) clientFullName " +
			"FROM clients WHERE contracts.client_id = clients.client_id), " +
			"sum_of_contract FROM contracts " +
			"WHERE sum_of_contract < 10000 " +
			"GROUP BY clientFullName, sum_of_contract ORDER BY clientFullName ASC ";
		static public string summaryQueriesWithConditionOnDataWithoutIndex = "SELECT contract_id, types_of_insurance.type_of_insurance, " +
			"concat (clients.surname, ' ', clients.firstname, ' ', clients.lastname) clientFullName, " +
			"text_of_contract, sum_of_contract, date_of_onclusion_contract " +
			"FROM contracts " +
			"INNER JOIN types_of_insurance ON contracts.type_of_insurance_id =types_of_insurance.type_of_insurance_id " +
			"INNER JOIN clients ON contracts.client_id=clients.client_id " +
			"WHERE contracts.employee_id = {0}" +
			"ORDER BY contract_id ASC ";
		static public string finalQueryWithConditionOnGroups = "SELECT companies.company_id, companies.company_name, " +
			"SUM(number_of_employees) sumOfEmployes " +
			"FROM branches " +
			"INNER JOIN companies ON branches.company_id=companies.company_id " +
			"GROUP BY companies.company_id, company_name " +
			"ORDER BY companies.company_id ASC ";
		static public string finalQueryWithConditionOnDataAndGroups = "SELECT company_name, COUNT(branches.branch_id) " +
			"FROM companies " +
			"INNER JOIN branches ON branches.company_id=companies.company_id " +
			"WHERE branches.opening_year BETWEEN {1} AND {2} " +
			"GROUP BY company_name, companies.company_id " +
			"HAVING companies.company_id = {0} " +
			"ORDER BY company_name ";
		static public string requestOnRequestBasedOnPrincipleOfFinalRequest = "SELECT contract_id, types_of_insurance.type_of_insurance, " +
			"concat (employees.surname, ' ', employees.firstname, ' ', employees.lastname) employeeFullName, " +
			"concat(clients.surname, ' ', clients.firstname, ' ', clients.lastname) clientFullName, " +
			"text_of_contract, sum_of_contract, date_of_onclusion_contract " +
			"FROM contracts " +
			"INNER JOIN types_of_insurance ON contracts.type_of_insurance_id =types_of_insurance.type_of_insurance_id " +
			"INNER JOIN employees ON contracts.employee_id= employees.employee_id " +
			"INNER JOIN clients ON contracts.client_id= clients.client_id " +
			"WHERE sum_of_contract > (SELECT avg(contracts.sum_of_contract) FROM contracts) " +
			"GROUP BY contract_id, type_of_insurance, employeeFullName, clientFullName, sum_of_contract " +
			"ORDER BY sum_of_contract ";
		static public string queryUsingUnion = "SELECT concat (branches.branch_name, ', ', cities.city) branchAndCity, " +
			"concat(employees.surname, ' ', employees.firstname, ' ', employees.lastname) employeeFullName, SUM(sum_of_contract) sumOfContract " +
			"FROM contracts " +
			"INNER JOIN employees ON contracts.employee_id=employees.employee_id " +
			"INNER JOIN branches ON employees.branch_id= branches.branch_id " +
			"INNER JOIN cities ON branches.city_id= cities.city_id " +
			"GROUP BY employees.employee_id, branchAndCity, employeeFullName " +
			"UNION " +
			"SELECT concat (branches.branch_name, ', ', cities.city) branchAndCity, 'ВСЕГО', SUM(sum_of_contract) " +
			"FROM contracts " +
			"INNER JOIN employees ON contracts.employee_id=employees.employee_id " +
			"INNER JOIN branches ON employees.branch_id=branches.branch_id " +
			"INNER JOIN cities ON branches.city_id= cities.city_id " +
			"GROUP BY branches.branch_id, branchAndCity " +
			"ORDER BY branchAndCity, sumOfContract ASC ";
		static public string queriesWithSubqueriesUsingIn = "SELECT contract_id, types_of_insurance.type_of_insurance, " +
			"concat (branches.branch_name, ', ', cities.city) branchNameAndCity, " +
			"concat (employees.surname, ' ', employees.firstname, ' ', employees.lastname) employeeFullName, " +
			"concat(clients.surname, ' ', clients.firstname, ' ', clients.lastname) clientFullName, " +
			"text_of_contract, sum_of_contract, date_of_onclusion_contract " +
			"FROM contracts " +
			"INNER JOIN types_of_insurance ON contracts.type_of_insurance_id =types_of_insurance.type_of_insurance_id " +
			"INNER JOIN employees ON contracts.employee_id= employees.employee_id " +
			"INNER JOIN clients ON contracts.client_id= clients.client_id " +
			"INNER JOIN branches ON employees.branch_id= branches.branch_id " +
			"INNER JOIN cities ON branches.city_id= cities.city_id " +
			"WHERE employees.branch_id IN (SELECT branch_id FROM branches WHERE company_id = (SELECT company_id FROM companies WHERE company_id = {0})) " +
			"ORDER BY branchNameAndCity, contract_id ASC ";
		static public string queriesWithSubqueriesUsingNotIn = "SELECT contract_id, types_of_insurance.type_of_insurance, " +
			"concat (branches.branch_name, ', ', cities.city) branchNameAndCity, " +
			"concat (employees.surname, ' ', employees.firstname, ' ', employees.lastname) employeeFullName, " +
			"concat(clients.surname, ' ', clients.firstname, ' ', clients.lastname) clientFullName, " +
			"text_of_contract, sum_of_contract, date_of_onclusion_contract " +
			"FROM contracts " +
			"INNER JOIN types_of_insurance ON contracts.type_of_insurance_id =types_of_insurance.type_of_insurance_id " +
			"INNER JOIN employees ON contracts.employee_id= employees.employee_id " +
			"INNER JOIN clients ON contracts.client_id= clients.client_id " +
			"INNER JOIN branches ON employees.branch_id= branches.branch_id " +
			"INNER JOIN cities ON branches.city_id= cities.city_id " +
			"WHERE employees.branch_id NOT IN (SELECT branch_id FROM branches WHERE branches.city_id = (SELECT city_id FROM cities WHERE city_id = {0})) " +
			"ORDER BY branchNameAndCity, contract_id ASC ";
		static public string queriesWithSubqueriesUsingCase = "SELECT DISTINCT " +
			"CASE " +
			"WHEN sum_of_contract = (SELECT MAX(sum_of_contract) FROM contracts) " +
			"THEN(SELECT company_name FROM companies WHERE company_id = " +
			"(SELECT branches.company_id FROM branches WHERE branches.branch_id = (SELECT employees.branch_id FROM employees WHERE employee_id = " +
			"(SELECT employee_id FROM contracts WHERE contracts.contract_id = c1.contract_id))))	 " +
			"WHEN sum_of_contract = (SELECT MIN(sum_of_contract) FROM contracts) " +
			"THEN(SELECT company_name FROM companies WHERE company_id = " +
			"(SELECT branches.company_id FROM branches WHERE branches.branch_id = (SELECT employees.branch_id FROM employees WHERE employee_id = " +
			"(SELECT employee_id FROM contracts WHERE contracts.contract_id = c1.contract_id))))	 " +
			"END companyName, sum_of_contract " +
			"FROM contracts c1 " +
			"ORDER BY companyName " +
			"LIMIT 2 ";
		//Вывести топовых сотрудников по количеству заключенных договоров
		static public string queriesWithSubqueriesUsingWith = "WITH employeeContracts AS " +
			"(SELECT employee_id, concat (employees.surname, ' ', employees.firstname, ' ', employees.lastname) employeeFullName, " +
			"(SELECT COUNT(contract_id) FROM contracts WHERE contracts.employee_id = employees.employee_id) as totalConracts " +
			"FROM employees " +
			"GROUP BY employees.employee_id), " +
			"topEmployees AS( " +
			"SELECT employee_id FROM employeeContracts " +
			"WHERE totalConracts > (SELECT MAX(totalConracts) * 0.85 FROM employeeContracts)) " +
			"SELECT employee_id, employeeFullName, totalConracts " +
			"FROM employeeContracts " +
			"WHERE employee_id IN(SELECT employee_id FROM topEmployees) " +
			"GROUP BY employee_id, employeeFullName, totalConracts " +
			"ORDER BY totalConracts DESC ";
		// СПЕЦИАЛЬНЫЙ ЗАПРОСЫ
		// неточно
		static public string specialQuery1 = "WITH topBranchesinCity AS ( " +
			"SELECT branches.branch_id, branch_name, city, COUNT(contracts.sum_of_contract) as countOfContracts " +
			"FROM employees " +
			"INNER JOIN branches ON employees.branch_id= branches.branch_id " +
			"INNER JOIN contracts ON contracts.employee_id = employees.employee_id " +
			"INNER JOIN cities ON branches.city_id= cities.city_id " +
			//"WHERE branches.city_id = 3 " +  добавляется при выборе пользователем города
			"{0} " +
			"GROUP BY branches.branch_id, cities.city_id " +
			"ORDER BY countOfContracts DESC " +
			"LIMIT 3) " +
			"SELECT branch_id, branch_name, city, countOfContracts " +
			"FROM topBranchesinCity ";
		static public string specialQuery2 = "WITH companiesAndClients AS ( " +
			"SELECT companies.company_id, company_name, AVG(DISTINCT clients.client_id ) as countOfCimpanyClietns FROM companies " +
			"LEFT JOIN branches ON branches.company_id=companies.company_id " +
			"LEFT JOIN employees ON employees.branch_id= branches.branch_id " +
			"LEFT JOIN contracts ON contracts.employee_id = employees.employee_id " +
			"LEFT JOIN clients ON contracts.client_id= clients.client_id " +
			"GROUP BY  companies.company_id, company_name), " +
			"branchesAndClients AS( " +
			"SELECT branches.company_id, branches.branch_id, concat (branch_name, ', ', cities.city) branchAndCity, " +
			"AVG(DISTINCT clients.client_id ) as countOfBranchClietns, is_main " +
			"FROM branches " +
			"INNER JOIN cities ON branches.city_id=cities.city_id " +
			"LEFT JOIN employees ON employees.branch_id= branches.branch_id " +
			"LEFT JOIN contracts ON contracts.employee_id = employees.employee_id " +
			"LEFT JOIN clients ON contracts.client_id= clients.client_id " +
			"GROUP BY branches.branch_id, branchAndCity) " +
			"SELECT DISTINCT company_name, countOfCimpanyClietns, branchesAndClients.branchAndCity, branchesAndClients.countOfBranchClietns " +
			"FROM companiesAndClients " +
			"RIGHT OUTER JOIN branchesAndClients ON companiesAndClients.company_id = branchesAndClients.company_id AND branchesAndClients.is_main = true " +
			"ORDER BY company_name, branchAndCity ";
		static public string specialQuery3 = "SELECT company_name, count( DISTINCT clients.client_id ) as countOfCompanyClietns, " +
			"SUM(sum_of_contract) as sumOfCompcontracts " +
			"FROM companies " +
			"LEFT JOIN branches ON branches.company_id=companies.company_id " +
			"LEFT JOIN employees ON employees.branch_id=branches.branch_id " +
			"LEFT JOIN contracts ON contracts.employee_id = employees.employee_id " +
			"LEFT JOIN clients ON contracts.client_id= clients.client_id " +
			"WHERE date_of_onclusion_contract BETWEEN {0} AND {1} " +
			"GROUP BY company_name " +
			"UNION ALL " +
			"SELECT 'ВСЕГО', count(DISTINCT clients.client_id ) as countOfCompanyClietns, SUM(sum_of_contract) as sumOfCompcontracts " +
			"FROM companies " +
			"LEFT JOIN branches ON branches.company_id=companies.company_id " +
			"LEFT JOIN employees ON employees.branch_id= branches.branch_id " +
			"LEFT JOIN contracts ON contracts.employee_id = employees.employee_id " +
			"LEFT JOIN clients ON contracts.client_id= clients.client_id " +
			"WHERE date_of_onclusion_contract BETWEEN {0} AND {1} " +
			"ORDER BY countOfCompanyClietns DESC, sumOfCompcontracts DESC, company_name DESC ";
	}
}
