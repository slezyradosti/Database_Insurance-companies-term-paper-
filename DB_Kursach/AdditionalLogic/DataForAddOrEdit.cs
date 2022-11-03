using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_Kursach
{
    public enum HowAddOrEdit
    {
        DefaultAdd = 0,
        DefaultEdit = 1,
        CompoundFormAdd = 2,
        CompoundFormEdit = 3
    }

    class DataForAddOrEdit
    {
        public Dictionary<string, int> CompanyNamesAndIDs { get; private set; }
        public Dictionary<string, int> PTypesNamesAndIDs { get; private set; }
        public Dictionary<string, int> CitiesNamesAndIDs { get; private set; }
        public Dictionary<string, int> BranchesNamesAndIDs { get; private set; }
        public Dictionary<string, int> SocailStatusesNamesAndIDs { get; private set; }
        public Dictionary<string, int> TypesOfInsuranceNamesAndIDs { get; private set; }
        public Dictionary<string, int> ClientsNamesAndIDs { get; private set; }
        public Dictionary<string, int> EmployeesNamesAndIDs { get; private set; }
        public int tabIndex { get; set; } // индекс владки из которой пошел запрос
        public int ID { get; set; } // ID элемента из бд

        public DataForAddOrEdit(int index = 0, int id = -1)
        {
            CompanyNamesAndIDs = new Dictionary<string, int>();
            PTypesNamesAndIDs = new Dictionary<string, int>();
            CitiesNamesAndIDs = new Dictionary<string, int>();
            BranchesNamesAndIDs = new Dictionary<string, int>();
            SocailStatusesNamesAndIDs = new Dictionary<string, int>();
            TypesOfInsuranceNamesAndIDs = new Dictionary<string, int>();
            ClientsNamesAndIDs = new Dictionary<string, int>();
            EmployeesNamesAndIDs = new Dictionary<string, int>();
            tabIndex = index;
            ID = id;
        }

        public async Task<string[]> SelectCompnaiesNamesAndIDsAsync(string connString)
        {
            return await Task.Run(() =>
            {
                CompanyNamesAndIDs.Clear();

                DataTable dt = WorkWithDataBase.SelectSpecificFieldsFromTable("companies ORDER BY company_name ASC", "company_id, company_name", connString);
                var IDs = dt.Rows.Cast<DataRow>().Select(x => x.ItemArray[0]).ToArray();
                var names = dt.Rows.Cast<DataRow>().Select(x => x.ItemArray[1]).ToArray();

                for (int i = 0; i < IDs.Count(); i++)
                {
                    try
                    {
                        CompanyNamesAndIDs.Add(names[i].ToString(), (int)IDs[i]);
                    }
                    catch (Exception) { }
                }

                return CompanyNamesAndIDs.Keys.ToArray();
            });
        }

        public async Task<string[]> SelectPropertyTypesNamesAndIDsAsync(string connString)
        {
            return await Task.Run(() =>
            {
                PTypesNamesAndIDs.Clear();

                DataTable dt = WorkWithDataBase.SelectSpecificFieldsFromTable("property_types ORDER BY property_type ASC", "type_id, property_type", connString);
                var IDs = dt.Rows.Cast<DataRow>().Select(x => x.ItemArray[0]).ToArray();
                var names = dt.Rows.Cast<DataRow>().Select(x => x.ItemArray[1]).ToArray();

                for (int i = 0; i < IDs.Count(); i++)
                {
                    try
                    {
                        PTypesNamesAndIDs.Add(names[i].ToString(), (int)IDs[i]);
                    }
                    catch (Exception) { }
                }

                return PTypesNamesAndIDs.Keys.ToArray();
            });
        }

        public async Task<string[]> SelectCitiesNamesAndIDsAsync(string connString)
        {
            return await Task.Run(() =>
            {
                CitiesNamesAndIDs.Clear();

                DataTable dt = WorkWithDataBase.SelectSpecificFieldsFromTable("cities  ORDER BY city ASC", "city_id, city", connString);
                var IDs = dt.Rows.Cast<DataRow>().Select(x => x.ItemArray[0]).ToArray();
                var names = dt.Rows.Cast<DataRow>().Select(x => x.ItemArray[1]).ToArray();

                for (int i = 0; i < IDs.Count(); i++)
                {
                    try
                    {
                        CitiesNamesAndIDs.Add(names[i].ToString(), (int)IDs[i]);
                    }
                    catch (Exception) { }
                }

                return CitiesNamesAndIDs.Keys.ToArray();
            });
        }

        public async Task<string[]> SelectBranchesNamesAndIDsAsync(string connString)
        {
            return await Task.Run(() =>
            {
                BranchesNamesAndIDs.Clear();

                DataTable dt = WorkWithDataBase.SelectSpecificFieldsFromTable("all_branches_view ORDER BY branch_name, city ASC", "branch_id, concat (branch_name, ', ', city) nameAndCity", connString);
                var IDs = dt.Rows.Cast<DataRow>().Select(x => x.ItemArray[0]).ToArray();
                var names = dt.Rows.Cast<DataRow>().Select(x => x.ItemArray[1]).ToArray();

                for (int i = 0; i < IDs.Count(); i++)
                {
                    try
                    {
                        BranchesNamesAndIDs.Add(names[i].ToString(), (int)IDs[i]);
                    }
                    catch (Exception ex) { }
                }

                return BranchesNamesAndIDs.Keys.ToArray();
            });
        }

        public async Task<string[]> SelectSocialStatusesNamesAndIDsAsync(string connString)
        {
            return await Task.Run(() =>
            {
                SocailStatusesNamesAndIDs.Clear();

                DataTable dt = WorkWithDataBase.SelectSpecificFieldsFromTable("social_status_of_clients ORDER BY social_status ASC", "social_status_id, social_status", connString);
                var IDs = dt.Rows.Cast<DataRow>().Select(x => x.ItemArray[0]).ToArray();
                var names = dt.Rows.Cast<DataRow>().Select(x => x.ItemArray[1]).ToArray();

                for (int i = 0; i < IDs.Count(); i++)
                {
                    try
                    {
                        SocailStatusesNamesAndIDs.Add(names[i].ToString(), (int)IDs[i]);
                    }
                    catch (Exception) { }
                }

                return SocailStatusesNamesAndIDs.Keys.ToArray();
            });
        }

        public async Task<string[]> SelectTypesOfInsuranceNamesAndIDsAsync(string connString)
        {
            return await Task.Run(() =>
            {
                TypesOfInsuranceNamesAndIDs.Clear();

                DataTable dt = WorkWithDataBase.SelectSpecificFieldsFromTable("types_of_insurance ORDER BY type_of_insurance ASC", "type_of_insurance_id, type_of_insurance", connString);
                var IDs = dt.Rows.Cast<DataRow>().Select(x => x.ItemArray[0]).ToArray();
                var names = dt.Rows.Cast<DataRow>().Select(x => x.ItemArray[1]).ToArray();

                for (int i = 0; i < IDs.Count(); i++)
                {
                    try
                    {
                        TypesOfInsuranceNamesAndIDs.Add(names[i].ToString(), (int)IDs[i]);
                    }
                    catch (Exception) { }
                }

                return TypesOfInsuranceNamesAndIDs.Keys.ToArray();
            });
        }
        public async Task<string[]> SelectClientsNamesAndIDsAsync(string connString)
        {
            return await Task.Run(() =>
            {
                ClientsNamesAndIDs.Clear();

                DataTable dt = WorkWithDataBase.SelectSpecificFieldsFromTable("clients ORDER BY surname, firstname, lastname ASC", "client_id, concat (surname, ' ', firstname, ' ', lastname)", connString);
                var IDs = dt.Rows.Cast<DataRow>().Select(x => x.ItemArray[0]).ToArray();
                var fullnames = dt.Rows.Cast<DataRow>().Select(x => x.ItemArray[1]).ToArray();

                for (int i = 0; i < IDs.Count(); i++)
                {
                    try
                    {
                        ClientsNamesAndIDs.Add(fullnames[i].ToString(), (int)IDs[i]);
                    }
                    catch (Exception) { }
                }

                return ClientsNamesAndIDs.Keys.ToArray();
            });
        }

        public async Task<string[]> SelectEmployeesNamesAndIDsAsync(string connString)
        {
            return await Task.Run(() =>
            {
                EmployeesNamesAndIDs.Clear();

                DataTable dt = WorkWithDataBase.SelectSpecificFieldsFromTable("employees ORDER BY surname, firstname, lastname ASC", "employee_id, concat (surname, ' ', firstname, ' ', lastname)", connString);
                var IDs = dt.Rows.Cast<DataRow>().Select(x => x.ItemArray[0]).ToArray();
                var fullnames = dt.Rows.Cast<DataRow>().Select(x => x.ItemArray[1]).ToArray();

                for (int i = 0; i < IDs.Count(); i++)
                {
                    try
                    {
                        EmployeesNamesAndIDs.Add(fullnames[i].ToString(), (int)IDs[i]);
                    }
                    catch (Exception) { }
                }

                return EmployeesNamesAndIDs.Keys.ToArray();
            });
        }
    }
}
