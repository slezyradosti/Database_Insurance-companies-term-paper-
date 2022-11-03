using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB_Kursach
{
	public partial class SearchForm : Form
	{
		private readonly string _connectionString;
		public string _stringOfSelect = null; // строк запроса поиска
		private DataForAddOrEdit DFAOE;
		public SearchForm(int index, string connString)
		{
			InitializeComponent();
			_connectionString = connString;
			DFAOE = new DataForAddOrEdit(index);
		}

		private void SearchForm_Load(object sender, EventArgs e)
		{
			searchTab.SelectedIndex = DFAOE.tabIndex;
			Task t = LoadTableForSearchAsync(DFAOE.tabIndex);

			if (searchTab.SelectedIndex == 1 || searchTab.SelectedIndex == 5 || searchTab.SelectedIndex == 7 || searchTab.SelectedIndex == 8)
			{
				this.Size = new Size(600, 300);
				panel1.Size = new Size(this.Width, panel1.Size.Height);
				headerTextLabel.Size = new Size(this.Size.Width, headerTextLabel.Size.Height);
			}
			headerTextLabel.Text = $"Поиск элемента в таблице '{searchTab.SelectedTab.Text}'";
		}

		private async Task LoadTableForSearchAsync(int index)
		{
			switch (index)
			{
				case 0: //Компании
					{
						companyCNamesComboBox.Items.AddRange(await DFAOE.SelectCompnaiesNamesAndIDsAsync(_connectionString));
						companyTypeBox.Items.AddRange(await DFAOE.SelectPropertyTypesNamesAndIDsAsync(_connectionString));
						companyCitiesBox.Items.AddRange(await DFAOE.SelectCitiesNamesAndIDsAsync(_connectionString));
					}
					break;
				case 1: //Типы собственности
					{
						pTypeComboBox.Items.AddRange(await DFAOE.SelectPropertyTypesNamesAndIDsAsync(_connectionString));
					}
					break;
				case 2: //Филиалы
					{// 
						ownedByCompanyBox.Items.AddRange(await DFAOE.SelectCompnaiesNamesAndIDsAsync(_connectionString));
						branchCityBox.Items.AddRange(await DFAOE.SelectCitiesNamesAndIDsAsync(_connectionString));
						branchesComboBox.Items.AddRange(await DFAOE.SelectBranchesNamesAndIDsAsync(_connectionString));

						noMatterRadioButton.Checked = true;
					}
					break;
				case 3: //Сотрудники
					{
						employeeBranchBox.Items.AddRange(await DFAOE.SelectBranchesNamesAndIDsAsync(_connectionString));
					}
					break;
				case 4: //Договоры
					{
						contractInsuranceTypeComboBox.Items.AddRange(await DFAOE.SelectTypesOfInsuranceNamesAndIDsAsync(_connectionString));
						contractClientComboBox.Items.AddRange(await DFAOE.SelectClientsNamesAndIDsAsync(_connectionString));
						contractEmployeeComboBox.Items.AddRange(await DFAOE.SelectEmployeesNamesAndIDsAsync(_connectionString));
					}
					break;
				case 5: //Виды страхования
					{
						typeOfInsuranceComboBox.Items.AddRange(await DFAOE.SelectTypesOfInsuranceNamesAndIDsAsync(_connectionString));
					}
					break;
				case 6: //Клиенты
					{
						clientCityBox.Items.AddRange(await DFAOE.SelectCitiesNamesAndIDsAsync(_connectionString));
						clientSocailStatusBox.Items.AddRange(await DFAOE.SelectSocialStatusesNamesAndIDsAsync(_connectionString));
					}
					break;
				case 7: //Соц. положения
					{
						socailStatusComboBox.Items.AddRange(await DFAOE.SelectSocialStatusesNamesAndIDsAsync(_connectionString));
					}
					break;
				case 8: //Города 
					{
						cityComboBox.Items.AddRange(await DFAOE.SelectCitiesNamesAndIDsAsync(_connectionString));
					}
					break;
				default:
					break;
			}
		}

		private void searchCompanyButton_Click(object sender, EventArgs e)
		{
			string searchCommand = "SELECT * FROM all_companies_view ";

			if (companyCNamesComboBox.SelectedItem != null)
			{
				searchCommand += $"WHERE company_name = '{companyCNamesComboBox.Text}' ";
			}
			if (companyTypeBox.SelectedItem != null)
			{
				searchCommand += (searchCommand.Contains("WHERE")) ? "AND " : "WHERE ";
				searchCommand += $"property_type = '{companyTypeBox.Text}' ";
			}
			if (licenseNumberCheckBox.Checked)
			{
				searchCommand += (searchCommand.Contains("WHERE")) ? "AND " : "WHERE ";

				if (Int32.TryParse(licenseNumberBox1.Text.Trim('_'), out int licenseNumber1) && Int32.TryParse(licenseNumberBox2.Text.Trim('_'), out int licenseNumber2))
				{
					searchCommand += $"license_number >= {licenseNumber1} AND license_number <= {licenseNumber2} ";
				}
				else if (Int32.TryParse(licenseNumberBox1.Text.Trim('_'), out licenseNumber1))
				{
					searchCommand += $"license_number >= {licenseNumber1} ";
				}
				else if (Int32.TryParse(licenseNumberBox2.Text.Trim('_'), out licenseNumber2))
				{
					searchCommand += $"license_number <= {licenseNumber2} ";
				}

			}
			if (licenseDateCheckBox.Checked)
			{
				searchCommand += (searchCommand.Contains("WHERE")) ? "AND " : "WHERE ";
				searchCommand += $"license_expiration_date >= '{endOfLicensePicker1.Value.Year}-{endOfLicensePicker1.Value.Month}-{endOfLicensePicker1.Value.Day}' " +
					$"AND license_expiration_date <= '{endOfLicensePicker2.Value.Year}-{endOfLicensePicker2.Value.Month}-{endOfLicensePicker2.Value.Day}' ";
			}
			if (companyCitiesBox.SelectedItem != null)
			{
				searchCommand += (searchCommand.Contains("WHERE")) ? "AND " : "WHERE ";
				searchCommand += $"city = '{companyCitiesBox.Text}' ";
			}
			if (companyPhoneNumberBox.MaskFull)
			{
				searchCommand += (searchCommand.Contains("WHERE")) ? "AND " : "WHERE ";
				searchCommand += $"phone_number = {companyCitiesBox.Text} ";
			}
			if (openingYearCheckBox.Checked)
			{
				searchCommand += (searchCommand.Contains("WHERE")) ? "AND " : "WHERE ";

				if (openingYearBox1.MaskFull && openingYearBox2.MaskFull)
				{
					searchCommand += $"opening_year >= {openingYearBox1.Text} AND opening_year <= {openingYearBox2.Text} ";
				}
				else if (openingYearBox1.MaskFull)
				{
					searchCommand += $"opening_year >= {openingYearBox1.Text} ";
				}
				else if (openingYearBox2.MaskFull)
				{
					searchCommand += $"opening_year <= {openingYearBox2.Text} ";
				}
			}

			_stringOfSelect = searchCommand;
			this.Close();
		}

		private void panel1_Paint(object sender, PaintEventArgs e)
		{

		}

		private void SearchForm_FormClosed(object sender, FormClosedEventArgs e)
		{

		}

		private void searchPropertyTypeButton_Click(object sender, EventArgs e)
		{
			string searchCommand = "SELECT * FROM all_property_types_view ";

			if (pTypeComboBox.SelectedItem != null)
			{
				searchCommand += $"WHERE property_type = '{pTypeComboBox.Text}' ";
			}

			_stringOfSelect = searchCommand;
			this.Close();
		}

		private void searchBranchButton_Click(object sender, EventArgs e)
		{
			string searchCommand = "SELECT * FROM all_branches_view ";

			if (ownedByCompanyBox.SelectedItem != null)
			{
				searchCommand += $"WHERE company_name = '{ownedByCompanyBox.Text}' ";
			}
			if (branchesComboBox.SelectedItem != null)
			{
				searchCommand += (searchCommand.Contains("WHERE")) ? "AND " : "WHERE ";
				searchCommand += $"branch_name = '{branchesComboBox.Text}' ";
			}
			if (branchCityBox.SelectedItem != null)
			{
				searchCommand += (searchCommand.Contains("WHERE")) ? "AND " : "WHERE ";
				searchCommand += $"city = '{branchCityBox.Text}' ";
			}
			if (branchPhoneMaskedBox.MaskFull)
			{
				searchCommand += (searchCommand.Contains("WHERE")) ? "AND " : "WHERE ";
				searchCommand += $"phone_number = {branchPhoneMaskedBox.Text} ";
			}
			if (branchOpeningYearCheckBox.Checked)
			{
				searchCommand += (searchCommand.Contains("WHERE")) ? "AND " : "WHERE ";

				if (Int32.TryParse(branchOpeningYearMaskedBox1.Text.Trim('_'), out int branchOpYear1) && Int32.TryParse(branchOpeningYearMaskedBox2.Text.Trim('_'), out int branchOpYear2))
				{
					searchCommand += $"opening_year >= {branchOpYear1} AND opening_year <= {branchOpYear2} ";
				}
				else if (Int32.TryParse(branchOpeningYearMaskedBox1.Text.Trim('_'), out branchOpYear1))
				{
					searchCommand += $"opening_year >= {branchOpYear1} ";
				}
				else if (Int32.TryParse(branchOpeningYearMaskedBox2.Text.Trim('_'), out branchOpYear2))
				{
					searchCommand += $"opening_year <= {branchOpYear2} ";
				}
			}
			if (numberOfEmployeesCheckBox.Checked)
			{
				searchCommand += (searchCommand.Contains("WHERE")) ? "AND " : "WHERE ";

				if (Int32.TryParse(empNubmerMaskedText1.Text.Trim('_'), out int employeeNumber1) && Int32.TryParse(empNubmerMaskedText2.Text.Trim('_'), out int employeeNumber2))
				{
					searchCommand += $"number_of_employees >= {employeeNumber1} AND number_of_employees <= {employeeNumber2} ";
				}
				else if (Int32.TryParse(empNubmerMaskedText1.Text.Trim('_'), out employeeNumber1))
				{
					searchCommand += $"number_of_employees >= {employeeNumber1} ";
				}
				else if (Int32.TryParse(empNubmerMaskedText2.Text.Trim('_'), out employeeNumber2))
				{
					searchCommand += $"number_of_employees <= {employeeNumber2} ";
				}
			}
			if (mainOfficeRadioButton.Checked)
			{
				searchCommand += (searchCommand.Contains("WHERE")) ? "AND " : "WHERE ";
				searchCommand += $"is_main = 'true' ";
			}
			if (notMainOfficeRadioButton.Checked)
			{
				searchCommand += (searchCommand.Contains("WHERE")) ? "AND " : "WHERE ";
				searchCommand += $"is_main = 'false' ";
			}

			_stringOfSelect = searchCommand;
			this.Close();
		}

		private void searchEmployeeButton_Click(object sender, EventArgs e)
		{
			string searchCommand = "SELECT * FROM all_employees_view ";

			if (employeeBranchBox.SelectedItem != null)
			{
				searchCommand += $"WHERE branchAndCity = '{employeeBranchBox.Text}' ";
			}
			if (!String.IsNullOrEmpty(employeeSurnameBox.Text.Trim()))
			{
				searchCommand += (searchCommand.Contains("WHERE")) ? "AND " : "WHERE ";
				searchCommand += $"LOWER(surname) LIKE LOWER('{employeeSurnameBox.Text.Trim()}%') ";
			}
			if (!String.IsNullOrEmpty(employeeFirstnameBox.Text.Trim()))
			{
				searchCommand += (searchCommand.Contains("WHERE")) ? "AND " : "WHERE ";
				searchCommand += $"LOWER(firstname) LIKE LOWER('{employeeFirstnameBox.Text.Trim()}%') ";
			}
			if (!String.IsNullOrEmpty(employeeSecondnameBox.Text.Trim()))
			{
				searchCommand += (searchCommand.Contains("WHERE")) ? "AND " : "WHERE ";
				searchCommand += $"LOWER(lastname) LIKE LOWER('{employeeSecondnameBox.Text.Trim()}%') ";
			}


			_stringOfSelect = searchCommand;
			this.Close();
		}

		private void searchContractButton_Click(object sender, EventArgs e)
		{
			string searchCommand = "SELECT * FROM all_contracts_view ";

			if (contractInsuranceTypeComboBox.SelectedItem != null)
			{
				searchCommand += $"WHERE type_of_insurance = '{contractInsuranceTypeComboBox.Text}' ";
			}
			if (contractClientComboBox.SelectedItem != null)
			{
				searchCommand += (searchCommand.Contains("WHERE")) ? "AND " : "WHERE ";
				searchCommand += $"clientFullName = '{contractClientComboBox.Text}' ";
			}
			if (contractEmployeeComboBox.SelectedItem != null)
			{
				searchCommand += (searchCommand.Contains("WHERE")) ? "AND " : "WHERE ";
				searchCommand += $"employeeFullName = '{contractEmployeeComboBox.Text}' ";
			}
			if (contactSumCheckBox.Checked)
			{
				searchCommand += (searchCommand.Contains("WHERE")) ? "AND " : "WHERE ";

				if (Int32.TryParse(contractSumBox1.Text.Trim('_'), out int contactSum1) && Int32.TryParse(contractSumBox2.Text.Trim('_'), out int contactSum2))
				{
					searchCommand += $"sum_of_contract >= {contactSum1} AND sum_of_contract <= {contactSum2} ";
				}
				else if (Int32.TryParse(contractSumBox1.Text.Trim('_'), out contactSum1))
				{
					searchCommand += $"sum_of_contract >= {contactSum1} ";
				}
				else if (Int32.TryParse(contractSumBox2.Text.Trim('_'), out contactSum2))
				{
					searchCommand += $"sum_of_contract <= {contactSum2} ";
				}
			}
			if (contractDateCheckBox.Checked)
			{
				searchCommand += (searchCommand.Contains("WHERE")) ? "AND " : "WHERE ";
				searchCommand += $"license_expiration_date >= '{contractDateTime1.Value.Year}-{contractDateTime1.Value.Month}-{contractDateTime1.Value.Day}' " +
					$"AND license_expiration_date <= '{contractDateTime2.Value.Year}-{contractDateTime2.Value.Month}-{contractDateTime2.Value.Day}' ";
			}

			_stringOfSelect = searchCommand;
			this.Close();
		}

		private void searchTopeOfInsuranceButton_Click(object sender, EventArgs e)
		{
			string searchCommand = "SELECT * FROM all_types_of_insurance_view ";

			if (typeOfInsuranceComboBox.SelectedItem != null)
			{
				searchCommand += $"WHERE type_of_insurance = '{typeOfInsuranceComboBox.Text}' ";
			}

			_stringOfSelect = searchCommand;
			this.Close();
		}

		private void searchClientButton_Click(object sender, EventArgs e)
		{
			string searchCommand = "SELECT * FROM all_clients_view ";

			if (!String.IsNullOrEmpty(clientSurnameBox.Text.Trim()))
			{
				searchCommand += $"WHERE LOWER(surname) LIKE LOWER('{clientSurnameBox.Text.Trim()}%') ";
			}
			if (!String.IsNullOrEmpty(clientFirstnameBox.Text.Trim()))
			{
				searchCommand += (searchCommand.Contains("WHERE")) ? "AND " : "WHERE ";
				searchCommand += $"LOWER(firstname) LIKE LOWER('{clientFirstnameBox.Text.Trim()}%') ";
			}
			if (!String.IsNullOrEmpty(clientSecondnameBox.Text.Trim()))
			{
				searchCommand += (searchCommand.Contains("WHERE")) ? "AND " : "WHERE ";
				searchCommand += $"LOWER(lastname) LIKE LOWER('{clientSecondnameBox.Text.Trim()}%') ";
			}
			if (dateOfBirthCheckBox.Checked)
			{
				searchCommand += (searchCommand.Contains("WHERE")) ? "AND " : "WHERE ";
				searchCommand += $"date_of_birth >= '{clientDateOfBirthTimePicker1.Value.Year}-{clientDateOfBirthTimePicker1.Value.Month}-{clientDateOfBirthTimePicker1.Value.Day}' " +
					$"AND date_of_birth <= '{clientDateOfBirthTimePicker2.Value.Year}-{clientDateOfBirthTimePicker2.Value.Month}-{clientDateOfBirthTimePicker2.Value.Day}' ";
			}
			if (clientCityBox.SelectedItem != null)
			{
				searchCommand += (searchCommand.Contains("WHERE")) ? "AND " : "WHERE ";
				searchCommand += $"city = '{clientCityBox.Text}' ";
			}
			if (clientSocailStatusBox.SelectedItem != null)
			{
				searchCommand += (searchCommand.Contains("WHERE")) ? "AND " : "WHERE ";
				searchCommand += $"social_status = '{clientSocailStatusBox.Text}' ";
			}
			if (clientPhoneMaskedBox.MaskFull)
			{
				searchCommand += (searchCommand.Contains("WHERE")) ? "AND " : "WHERE ";
				searchCommand += $"phone_number = {clientPhoneMaskedBox.Text} ";
			}

			_stringOfSelect = searchCommand;
			this.Close();
		}

		private void searchSocailStatucButton_Click(object sender, EventArgs e)
		{
			string searchCommand = "SELECT * FROM all_social_status_of_clients_view ";

			if (socailStatusComboBox.SelectedItem != null)
			{
				searchCommand += $"WHERE social_status = '{socailStatusComboBox.Text}' ";
			}

			_stringOfSelect = searchCommand;
			this.Close();
		}

        private void searchCityButton_Click(object sender, EventArgs e)
        {
			string searchCommand = "SELECT * FROM all_cities_view ";

			if (cityComboBox.SelectedItem != null)
			{
				searchCommand += $"WHERE city = '{cityComboBox.Text}' ";
			}

			_stringOfSelect = searchCommand;
			this.Close();
		}

        private void searchTab_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void SearchForm_Resize(object sender, EventArgs e)
        {
			panel1.Size = new Size(this.Width, panel1.Size.Height);
		}
    }
}

