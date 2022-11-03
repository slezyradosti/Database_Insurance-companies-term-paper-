using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DB_Kursach
{
	public partial class ClientContractsForm : Form
	{
		private readonly string _connectionString;

		InteractionsWithForms interactions = new InteractionsWithForms();
		public ClientContractsForm(int clientID, string connString)
		{
			InitializeComponent();
			interactions = new InteractionsWithForms(clientID);
			_connectionString = connString;
		}

		private void ClientContracts_Load(object sender, EventArgs e)
		{
			resetButton.Enabled = false;

			DataTable dt = WorkWithDataBase.SpecifitSelectFieldsFromTable("SELECT surname, firstname, lastname, date_of_birth, social_status_of_clients.social_status, cities.city, address, phone_number " +
			"FROM clients " +
			"INNER JOIN social_status_of_clients ON social_status_of_clients.social_status_id = clients.social_status_id " +
			"INNER JOIN cities ON cities.city_id = clients.city_id " +
			$"WHERE clients.client_id = {interactions.clientID}", _connectionString);

			var stringArr = dt.Rows[0].ItemArray.Select(x => x.ToString()).ToArray();
			
			stringArr[3] = stringArr[3].Substring(0, 10); //достаетм дату без времени 1/1/1111 вместо 1/1/1111 AM: 12:12:12
			cNameLabel.Text = "ФИО: " +  stringArr[0] + " " + stringArr[1] + " " + stringArr[2];
			dateOfBirthLabel.Text = "Дата рождения: " + stringArr[3];
			socialStatusLabel.Text = "Социальное положение: " + stringArr[4];
			cityLabel.Text = "Город: " + stringArr[5];
			addressLabel.Text = "Адрес: " + stringArr[6];
			phoneLabel.Text = "Телефон: " + stringArr[7];

			// Итоговый запрос с условием
			dt = WorkWithDataBase.SpecifitSelectFieldsFromTable("SELECT contract_id, types_of_insurance.type_of_insurance, employees.surname, employees.firstname, employees.lastname, text_of_contract, sum_of_contract, date_of_onclusion_contract, branches.branch_name " +
				"FROM contracts " +
				"INNER JOIN types_of_insurance ON contracts.type_of_insurance_id =types_of_insurance.type_of_insurance_id " +
				"INNER JOIN employees ON contracts.employee_id=employees.employee_id " +
				"INNER JOIN branches ON branches.branch_id = employees.branch_id " +
				$"WHERE contracts.client_id = {interactions.clientID} " +
				"ORDER BY contract_id ASC;", _connectionString);
			
			if (dt != null)
			{
				clientContractsGrid.DataSource = dt;

				clientContractsGrid.BackgroundColor = Color.White;
				SetColumnsNames();
			}
			
			countOfClientContractsLabel.Text = "Количество полей: " + clientContractsGrid.Rows.Count;

			//узнаем стоблец в суммой договора, и считаем общую сумму
			sumLabel.Text = "Общая стоимость договоров: " + WorkWithDataBase.SelectLineFromTable("sum_of_contract", "contracts", _connectionString, "SUM", $"WHERE client_id={interactions.clientID}");
		
		}

		private void sortBySumutton_Click(object sender, EventArgs e)
		{
			string howSorting = "ASC;";

			// изменения вида сортировки для следующего щелчка
			if (interactions.sortSumASCorDESC)
			{
				interactions.sortSumASCorDESC = false;
				howSorting = "DESC;";
			}
			else
			{
				interactions.sortSumASCorDESC = true;
				howSorting = "ASC;";
			}

			if (interactions.Date2.Year == 1)
			{
				interactions.SetDate2(DateTime.Now);
			}
			
			DataTable dt = WorkWithDataBase.SpecifitSelectFieldsFromTable("SELECT contract_id, types_of_insurance.type_of_insurance, employees.surname, employees.firstname, employees.lastname, text_of_contract, sum_of_contract, date_of_onclusion_contract, branches.branch_name " +
				"FROM contracts " +
				"INNER JOIN types_of_insurance ON contracts.type_of_insurance_id =types_of_insurance.type_of_insurance_id " +
				"INNER JOIN employees ON contracts.employee_id=employees.employee_id " +
				"INNER JOIN branches ON branches.branch_id = employees.branch_id " +
				$"WHERE contracts.client_id = {interactions.clientID} " +
				$"AND date_of_onclusion_contract BETWEEN '{interactions.Date1.Year}-{interactions.Date1.Month}-{interactions.Date1.Day}' AND '{interactions.Date2.Year}-{interactions.Date2.Month}-{interactions.Date2.Day}' " +
				"ORDER BY sum_of_contract " + howSorting, _connectionString);
			
			if (dt != null)
			{
				clientContractsGrid.DataSource = dt;
				SetColumnsNames();
			}

			resetButton.Enabled = true;
		}

		private void SetColumnsNames()
        {
			if (clientContractsGrid.Columns.Count == 9)
			{
				clientContractsGrid.ClearSelection();
				clientContractsGrid.Columns[0].HeaderText = "ID";
				clientContractsGrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
				clientContractsGrid.Columns[1].HeaderText = "Вид страхования";
				clientContractsGrid.Columns[2].HeaderText = "Фамилия сотрудника";
				clientContractsGrid.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
				clientContractsGrid.Columns[3].HeaderText = "Имя сотрудника";
				clientContractsGrid.Columns[4].HeaderText = "Отчество сотрудника";
				clientContractsGrid.Columns[5].HeaderText = "Текст договора";
				clientContractsGrid.Columns[6].HeaderText = "Сумма договора";
				clientContractsGrid.Columns[7].HeaderText = "Дата заключения договора";
				clientContractsGrid.Columns[8].HeaderText = "Название филиала";
			}
		}

        private void sortByTypeButton_Click(object sender, EventArgs e)
        {
			if (interactions.Date2.Year == 1)
			{
				interactions.SetDate2(DateTime.Now);
			}
			
			DataTable dt = WorkWithDataBase.SpecifitSelectFieldsFromTable("SELECT contract_id, types_of_insurance.type_of_insurance, employees.surname, employees.firstname, employees.lastname, text_of_contract, sum_of_contract, date_of_onclusion_contract, branches.branch_name " +
				"FROM contracts " +
				"INNER JOIN types_of_insurance ON contracts.type_of_insurance_id = types_of_insurance.type_of_insurance_id " +
				"INNER JOIN employees ON contracts.employee_id=employees.employee_id " +
				"INNER JOIN branches ON branches.branch_id = employees.branch_id " +
			   $"WHERE contracts.client_id = {interactions.clientID} " +
			   $"AND date_of_onclusion_contract BETWEEN '{interactions.Date1.Year}-{interactions.Date1.Month}-{interactions.Date1.Day}' AND '{interactions.Date2.Year}-{interactions.Date2.Month}-{interactions.Date2.Day}' " +
			   "ORDER BY types_of_insurance.type_of_insurance ASC;", _connectionString);

			if (dt != null)
			{
				clientContractsGrid.DataSource = dt;
				SetColumnsNames();
			}

			resetButton.Enabled = true;
		}

		private void resetButton_Click(object sender, EventArgs e)
		{
			interactions.SetDate1(DateTime.MinValue);
			interactions.SetDate2(DateTime.Now);

			dateTimePicker1.ResetText();
			dateTimePicker1.ResetText();

			interactions.sortSumASCorDESC = false;
			ClientContracts_Load(sender, e);
		}

		private void setTimeSpanButton_Click(object sender, EventArgs e)
		{
			interactions.SetDate1(dateTimePicker1.Value);
			interactions.SetDate2(dateTimePicker2.Value);

			//-	отбор по дате 
			DataTable dt = WorkWithDataBase.SpecifitSelectFieldsFromTable("SELECT contract_id, types_of_insurance.type_of_insurance, " +
				"employees.surname, employees.firstname, employees.lastname, text_of_contract, sum_of_contract, date_of_onclusion_contract, " +
				"branches.branch_name " +
				"FROM contracts " +
				"INNER JOIN types_of_insurance ON contracts.type_of_insurance_id =types_of_insurance.type_of_insurance_id " +
				"INNER JOIN employees ON contracts.employee_id=employees.employee_id " +
				"INNER JOIN branches ON branches.branch_id = employees.branch_id " +
				$"WHERE contracts.client_id = {interactions.clientID} " +
				$"AND date_of_onclusion_contract BETWEEN '{interactions.Date1.Year}-{interactions.Date1.Month}-{interactions.Date1.Day}' " +
				$"AND '{interactions.Date2.Year}-{interactions.Date2.Month}-{interactions.Date2.Day}' " +
				"ORDER BY contract_id ASC;", _connectionString);
			
			if (dt != null)
			{
				clientContractsGrid.DataSource = dt;
				SetColumnsNames();
			}

			countOfClientContractsLabel.Text = "Количество полей: " + clientContractsGrid.Rows.Count;
			sumLabel.Text = "Общая стоимость договоров: " + WorkWithDataBase.SelectLineFromTable("sum_of_contract", "contracts", _connectionString, "SUM", 
				$"WHERE client_id={interactions.clientID} AND date_of_onclusion_contract BETWEEN '{interactions.Date1.Year}-{interactions.Date1.Month}-{interactions.Date1.Day}' AND '{interactions.Date2.Year}-{interactions.Date2.Month}-{interactions.Date2.Day}'");
			
			resetButton.Enabled = true;
		}

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
			resetButton.Enabled = true;
		}

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
			resetButton.Enabled = true;
		}

        private void addContractButton_Click(object sender, EventArgs e)
        {
			AddOrEditElementForm addElem = new AddOrEditElementForm(HowAddOrEdit.CompoundFormAdd, 4, _connectionString, interactions.clientID);
			addElem.ShowDialog();
			ClientContracts_Load(sender, e);
		}

		private void deleteContractButton_Click(object sender, EventArgs e)
		{
			if (clientContractsGrid.SelectedRows.Count == 0) return;

			for (int i = 0; i < clientContractsGrid.SelectedRows.Count; i++)
			{
				WorkWithDataBase.RemoveLineFromTable("contracts", $"contract_id = {clientContractsGrid.SelectedRows[i].Cells[0].Value}", _connectionString);
			}

			ClientContracts_Load(sender, e);
			clientContractsGrid.Refresh();
		}

        private void editContractButton_Click(object sender, EventArgs e)
        {
			if (clientContractsGrid.SelectedRows.Count != 1)
			{
				MessageBox.Show("Выберите один элемент для редактирования!", "Error!");
				return;
			}

			int ID = Convert.ToInt32(clientContractsGrid.SelectedRows[0].Cells[0].Value);
			AddOrEditElementForm editElem = new AddOrEditElementForm(HowAddOrEdit.CompoundFormEdit, 4, _connectionString, ID);
			editElem.ShowDialog();
			ClientContracts_Load(sender, e);
		}

        private void selectAllContractsButton_Click(object sender, EventArgs e)
        {
			if (clientContractsGrid.SelectedRows.Count == clientContractsGrid.Rows.Count)
			{
				clientContractsGrid.ClearSelection();
			}
			else
			{
				clientContractsGrid.SelectAll();
			}
		}

        private void refreshClientContractsButton_Click(object sender, EventArgs e)
        {
			ClientContracts_Load(sender, e);
			clientContractsGrid.Refresh();
		}

        private void ClientContractsForm_Resize(object sender, EventArgs e)
        {
			panel1.Size = new Size(this.Width, panel1.Size.Height);
		}

        private void addContractButton_MouseHover(object sender, EventArgs e)
        {
			fullTextToolTip.ToolTipTitle = "Добавить";
		}

        private void deleteContractButton_MouseHover(object sender, EventArgs e)
        {
			fullTextToolTip.ToolTipTitle = "Редактировать";
		}

        private void editContractButton_MouseHover(object sender, EventArgs e)
        {
			fullTextToolTip.ToolTipTitle = "Удалить";
		}

        private void selectAllContractsButton_MouseHover(object sender, EventArgs e)
        {
			fullTextToolTip.ToolTipTitle = "Поиск";
		}

        private void refreshClientContractsButton_MouseHover(object sender, EventArgs e)
        {
			fullTextToolTip.ToolTipTitle = "Вывести обновленный список договоров";
		}
    }
}