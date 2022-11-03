using DataGenerator;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DB_Kursach
{
	public class InteractionsWithForms
	{
		private Random random = new Random();

		public bool sortSumASCorDESC = false;// виды сортировок для суммы: true - от меньшего, flase - от большего
		public DateTime Date1 { get; private set; }
		public DateTime Date2 { get; private set; }
		public int clientID { get; set; }
		public int indexOfQueriesComboBox { get; set;}
		public bool isSpecialQuery { get; set; } // false - default, true - special
		public InteractionsWithForms(int clID = -1)
		{
			clientID = clID;
			isSpecialQuery = false;
			indexOfQueriesComboBox = -1;
		}

		public void SetDate1(DateTime d1) => Date1 = d1;
		public void SetDate2(DateTime d2) => Date2 = d2;

		public async Task<string> GenerateAllTablesAsync(string connString)
		{
			return await Task.Run(() =>
			{
				//Листы хранящие Айди полей таблиц
				List<int> citiesIDs = new List<int>();
				List<int> typesOfInsuranceIDs = new List<int>();
				List<int> propertyTypesIDs = new List<int>();
				List<int> socialStatusOfClientsIDs = new List<int>();
				List<int> companiesIDs = new List<int>();
				List<int> branchesIDs = new List<int>();
				List<int> employeesIDs = new List<int>();
				List<int> clientsIDs = new List<int>();

				//индесы дли листов с айдишниками
				int propertyTypeIndex = 0;
				int companyIndex = 0;
				int branchIndex = 0;
				int cityIndex = 0;
				int socIndex = 0;
				int typeInsuranceIndex = 0;
				int employeeIndex = 0;
				int clientIndex = 0;

				using (var conn = new NpgsqlConnection(connString))
				{
					// генерация справочников
					string[] data = Generation.GenerateFullDirectory("cities");
					for (int i = 0; i < data.Count(); i++)
					{
						WorkWithDataBase.InsertValueIntoTable("cities", "city", data[i], connString);
					}

					DataTable dt = WorkWithDataBase.SelectIDsFromTable("city_id", "cities", connString);
					var firstArray = dt.Rows.Cast<DataRow>().Select(x => x.ItemArray[0]);
					foreach (int i in firstArray)
					{
						if (!citiesIDs.Contains(i))
						{
							citiesIDs.Add(i);
						}
					}


					data = Generation.GenerateFullDirectory("typesOfInsurance");
					for (int i = 0; i < data.Count(); i++)
					{
						WorkWithDataBase.InsertValueIntoTable("types_of_insurance", "type_of_insurance", data[i], connString);
					}

					dt = WorkWithDataBase.SelectIDsFromTable("type_of_insurance_id", "types_of_insurance", connString);
					firstArray = dt.Rows.Cast<DataRow>().Select(x => x.ItemArray[0]);
					foreach (int i in firstArray)
					{
						if (!typesOfInsuranceIDs.Contains(i))
						{
							typesOfInsuranceIDs.Add(i);
						}
					}


					data = Generation.GenerateFullDirectory("propertyTypes");
					for (int i = 0; i < data.Count(); i++)
					{
						WorkWithDataBase.InsertValueIntoTable("property_types", "property_type", data[i], connString);
					}

					dt = WorkWithDataBase.SelectIDsFromTable("type_id", "property_types", connString);
					firstArray = dt.Rows.Cast<DataRow>().Select(x => x.ItemArray[0]);
					foreach (int i in firstArray)
					{
						if (!propertyTypesIDs.Contains(i))
						{
							propertyTypesIDs.Add(i);
						}
					}


					data = Generation.GenerateFullDirectory("socialStatusOfClients");
					for (int i = 0; i < data.Count(); i++)
					{
						WorkWithDataBase.InsertValueIntoTable("social_status_of_clients", "social_status", data[i], connString);
					}

					dt = WorkWithDataBase.SelectIDsFromTable("social_status_id", "social_status_of_clients", connString);
					firstArray = dt.Rows.Cast<DataRow>().Select(x => x.ItemArray[0]);
					foreach (int i in firstArray)
					{
						if (!socialStatusOfClientsIDs.Contains(i))
						{
							socialStatusOfClientsIDs.Add(i);
						}
					}


					//генерация таблиц

					// данные в таблицу companies
					DateTime date = DateTime.Today;
					for (int i = 0; i < Generation._companyNames.Count(); i++)
					{
						propertyTypeIndex = random.Next(0, propertyTypesIDs.Count);
						cityIndex = random.Next(0, citiesIDs.Count - 1);

						date = date.AddDays(random.Next(0, 1001)); // 04-22-2022, а нужно 2022-04-22
						WorkWithDataBase.InsertValuesIntoTable("public.companies(company_name, type_id, license_number, license_expiration_date, city_id, address, phone_number, opening_year)",
							$"'{Generation._companyNames[i]}', {propertyTypesIDs[propertyTypeIndex]}, {random.Next(1000, 9999)}, '{date.Year}-{date.Month}-{date.Day}', {citiesIDs[cityIndex]}, '{Generation.GenerateLine("address")}', '{Generation.GeneratePhoneNumber()}', {random.Next(1850, DateTime.Now.Year)}", connString);
						date = DateTime.Today;
					}

					//получить лист ID из таблицы companies
					dt = WorkWithDataBase.SelectIDsFromTable("company_id", "companies", connString);
					firstArray = dt.Rows.Cast<DataRow>().Select(x => x.ItemArray[0]);
					foreach (int i in firstArray)
					{
						if (!companiesIDs.Contains(i))
						{
							companiesIDs.Add(i);
						}
					}

					//данные в таблицу branches
					for (int i = 0; i < random.Next(companiesIDs.Count()+25, companiesIDs.Count() + 150); i++)
					{
						companyIndex = random.Next(0, companiesIDs.Count); // индекс компании из списка айдишников
						cityIndex = random.Next(0, citiesIDs.Count - 1);

						WorkWithDataBase.InsertValuesIntoTable("public.branches(branch_name, company_id, city_id, is_main, address, phone_number, opening_year, number_of_employees)",
							$"'{WorkWithDataBase.SelectLineFromTable("company_name", "companies", connString, where: $"WHERE company_id={companiesIDs[companyIndex]}")}', {companyIndex + 1}, {citiesIDs[cityIndex]}, 'false', '{Generation.GenerateLine("address")}', '{Generation.GeneratePhoneNumber()}', {random.Next(1850, 2020)}, DEFAULT", connString);
					}
					//получить лист ID из таблицы companies
					dt = WorkWithDataBase.SelectIDsFromTable("branch_id", "branches", connString);
					firstArray = dt.Rows.Cast<DataRow>().Select(x => x.ItemArray[0]);
					foreach (int i in firstArray)
					{
						if (!branchesIDs.Contains(i))
						{
							branchesIDs.Add(i);
						}
					}


					//данные в таблицу employees
					bool sex = true; // пол для генерации. true - male, false - fimale
					for (int i = 0; i < random.Next(branchesIDs.Count * 5 + 200, branchesIDs.Count * 30 + 400); i++)
					{
						branchIndex = random.Next(0, branchesIDs.Count);
						if (random.Next(0, 2) == 0)
						{
							sex = true;
						}
						else
						{
							sex = false;
						}
						string[] fullName = Generation.GenerateFullName(sex);


						WorkWithDataBase.InsertValuesIntoTable("public.employees(branch_id, surname, firstname, lastname)",
							$"'{branchesIDs[branchIndex]}', '{fullName[0]}', '{fullName[1]}', '{fullName[2]}'", connString);
					}

					dt = WorkWithDataBase.SelectIDsFromTable("employee_id", "employees", connString);
					firstArray = dt.Rows.Cast<DataRow>().Select(x => x.ItemArray[0]);
					foreach (int i in firstArray)
					{
						if (!employeesIDs.Contains(i))
						{
							employeesIDs.Add(i);
						}
					}

					//данные в таблицу clients
					sex = true; // пол для генерации. true - male, false - fimale
					for (int i = 0; i < random.Next(450, 1201); i++)
					{
						date = DateTime.Today;
						date -= new TimeSpan(random.Next(7000, 32000), 0, 0, 0);

						cityIndex = random.Next(0, citiesIDs.Count - 1);
						socIndex = random.Next(0, socialStatusOfClientsIDs.Count - 1);
						if (random.Next(0, 2) == 0)
						{
							sex = true;
						}
						else
						{
							sex = false;
						}
						string[] fullName = Generation.GenerateFullName(sex);


						WorkWithDataBase.InsertValuesIntoTable("public.clients(surname, firstname, lastname, date_of_birth, social_status_id, city_id, address, phone_number)",
							$"'{fullName[0]}', '{fullName[1]}', '{fullName[2]}', '{date.Year}-{date.Month}-{date.Day}', {socialStatusOfClientsIDs[socIndex]}, {citiesIDs[cityIndex]}, '{Generation.GenerateLine("address")}', {Generation.GeneratePhoneNumber()}", connString);
					}

					dt = WorkWithDataBase.SelectIDsFromTable("client_id", "clients", connString);
					firstArray = dt.Rows.Cast<DataRow>().Select(x => x.ItemArray[0]);
					foreach (int i in firstArray)
					{
						if (!clientsIDs.Contains(i))
						{
							clientsIDs.Add(i);
						}
					}


					//генерация договоров
					for (int i = 0; i < 10000; i++)
					{
						date = DateTime.Today;
						date -= new TimeSpan(random.Next(0, 120), 0, 0, 0);

						typeInsuranceIndex = random.Next(0, typesOfInsuranceIDs.Count - 1);
						employeeIndex = random.Next(0, employeesIDs.Count - 1);
						clientIndex = random.Next(0, clientsIDs.Count - 1);

						WorkWithDataBase.InsertValuesIntoTable("public.contracts(type_of_insurance_id, employee_id, client_id, text_of_contract, sum_of_contract, date_of_onclusion_contract)",
							$"{typesOfInsuranceIDs[typeInsuranceIndex]}, {employeesIDs[employeeIndex]}, {clientsIDs[clientIndex]}, '{Generation.GenerateLine("contractText")}', {random.Next(1000, 350000)}, '{date.Year}-{date.Month}-{date.Day}'", connString);
					}

					branchesIDs.Clear();
					citiesIDs.Clear();
					clientsIDs.Clear();
					companiesIDs.Clear();
					employeesIDs.Clear();
					propertyTypesIDs.Clear();
					socialStatusOfClientsIDs.Clear();
					typesOfInsuranceIDs.Clear();
				}

				return "Генерация записей завершена";
			});
		}

		public async Task<string> ClearAllTablesAsync(string connString)
		{
			return await Task.Run(() =>
			{
				WorkWithDataBase.ClearTableCascade("property_types", connString);
				WorkWithDataBase.ClearTableCascade("cities", connString);
				WorkWithDataBase.ClearTableCascade("companies", connString);
				WorkWithDataBase.ClearTableCascade("types_of_insurance", connString);
				WorkWithDataBase.ClearTableCascade("social_status_of_clients", connString);
				WorkWithDataBase.ClearTableCascade("branches", connString);
				WorkWithDataBase.ClearTableCascade("employees", connString);
				WorkWithDataBase.ClearTableCascade("clients", connString);
				WorkWithDataBase.ClearTableCascade("contracts", connString);

				return "Очистка записей завершена";
			});
		}
	}
}
