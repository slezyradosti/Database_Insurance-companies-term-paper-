using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB_Kursach
{
	public partial class MainMenuForm : Form
	{

		private readonly string _connectionString = null;
		private InteractionsWithForms interaction;
		private DataForAddOrEdit DFAOE;

		public MainMenuForm(string connect)
		{
			InitializeComponent();
			_connectionString = connect;
			interaction = new InteractionsWithForms();
			DFAOE = new DataForAddOrEdit();
		}

		private void MainMenu_Load(object sender, EventArgs e)
		{
			LoadTable(DBTablesTab.SelectedIndex);
			exportToExelButton.Enabled = false;
		}

		private void MainMenu_Closed(object sender, FormClosedEventArgs e)
		{
			Application.Exit();
		}

		private void DBTablesTab_Changed(object sender, EventArgs e)
		{
			exportToExelButton.Enabled = false;

			if (DBTablesTab.SelectedIndex == 9)
            {
				addButton.Visible = false;
				deleteButton.Visible = false;
				editButton.Visible = false;
				selectAllButton.Visible = false;
				searchButton.Visible = false;
				refreshGridButton.Visible = false;
			}
            else
            {
				addButton.Visible = true;
				deleteButton.Visible = true;
				editButton.Visible = true;
				selectAllButton.Visible = true;
				searchButton.Visible = true;
				refreshGridButton.Visible = true;
			}

			LoadTable(DBTablesTab.SelectedIndex);
		}

		private void LoadTable(int index, bool defaultQueryOrSpecial = true) // defaultQueryOrSpecial - вклкдка с запросами будет для обычных запросов (true), или для специальных(false) 
		{
			switch(index)
			{
				case 0: //Компании
					{
						companiesGrid.Columns.Clear();

						DataTable dt = WorkWithDataBase.SelectAllFieldsFromTable("all_companies_view", _connectionString);
						if (dt != null)
						{
							companiesGrid.DataSource = dt;

							companiesGrid.BackgroundColor = Color.White;
							if (companiesGrid.Columns.Count == 10)
							{
								companiesGrid.Columns.RemoveAt(9);
							}

							string cmd = "SELECT company_id, license_photo FROM companies ORDER BY company_id ASC ";
							SetImagesFromTable(cmd);
							SetNamesOfAllColumnsOfAllTables(0);
						}
					}
					break;
				case 1: //Типы собственности
					{	
						DataTable dt = WorkWithDataBase.SelectAllFieldsFromTable("all_property_types_view", _connectionString);
						if (dt != null)
						{
							propertyTypesGrid.DataSource = dt;

							propertyTypesGrid.BackgroundColor = Color.White;
							SetNamesOfAllColumnsOfAllTables(1);
						}
					}
					break;
				case 2: //Филиалы
					{
						//	симметричное внутреннее соединение без условия 
						DataTable dt = WorkWithDataBase.SelectAllFieldsFromTable("all_branches_view", _connectionString);
						if (dt != null)
						{
							branchesGrid.DataSource = dt;

							branchesGrid.BackgroundColor = Color.White;
							SetNamesOfAllColumnsOfAllTables(2);
						}
					}
					break;
				case 3: //Сотрудники
					{
						// симметричное внутреннее соединение без условия 
						DataTable dt = WorkWithDataBase.SelectAllFieldsFromTable("all_employees_view", _connectionString);
						if (dt != null)
						{
							employeesGrid.DataSource = dt;

							employeesGrid.BackgroundColor = Color.White;
							SetNamesOfAllColumnsOfAllTables(3);
						}
					}
					break;
				case 4: //Договоры
					{
						// симметричное внутреннее соединение без условия 
						DataTable dt = WorkWithDataBase.SelectAllFieldsFromTable("all_contracts_view", _connectionString);
						if (dt != null)
						{
							contractsGrid.DataSource = dt;

							contractsGrid.BackgroundColor = Color.White;
							SetNamesOfAllColumnsOfAllTables(4);
						}
					}
					break;
				case 5: //Виды страхования
					{
						DataTable dt = WorkWithDataBase.SelectAllFieldsFromTable("all_types_of_insurance_view", _connectionString);
						if (dt != null)
						{
							typesOfInsuranceGrid.DataSource = dt;

							typesOfInsuranceGrid.BackgroundColor = Color.White;
							SetNamesOfAllColumnsOfAllTables(5);
						}
					}
					break;
				case 6: //Клиенты
					{
						DataTable dt = WorkWithDataBase.SelectAllFieldsFromTable("all_clients_view", _connectionString);
						if (dt != null)
						{
							clientsGrid.DataSource = dt;

							clientsGrid.BackgroundColor = Color.White;
							SetNamesOfAllColumnsOfAllTables(6);
						}
					}
					break;
				case 7: //Соц. положения
					{
						DataTable dt = WorkWithDataBase.SelectAllFieldsFromTable("all_social_status_of_clients_view", _connectionString);
						if (dt != null)
						{
							socialStatusOfClientsGrid.DataSource = dt;

							socialStatusOfClientsGrid.BackgroundColor = Color.White;
							SetNamesOfAllColumnsOfAllTables(7);
						}
					}
					break;
				case 8: //Города 
					{
						DataTable dt = WorkWithDataBase.SelectAllFieldsFromTable("all_cities_view", _connectionString);
						if (dt != null)
						{
							citiesGrid.DataSource = dt;
							citiesGrid.BackgroundColor = Color.White;
							SetNamesOfAllColumnsOfAllTables(8);
						}
					}
					break;
				case 9: //Запросы
					{
						headerTextLabel.Text = $"Запросы";
						requestsGrid.BackgroundColor = Color.White;

						queryDatePicker1.Enabled = false;
						queryDatePicker2.Enabled = false;
						queryMaskedBox.Enabled = false;
						queryComboBox.Enabled = false;
						queryNamesComboBox.Items.Clear();

						queryNamesComboBox.Items.AddRange(Queries.queryNames);
						queryNamesComboBox.Items.AddRange(Queries.specialQueryNames);

						label10.Text = "Количество полей: " + requestsGrid.Rows.Count;
					}
					break;

				default:
					break;
			}
		}

		private void SetImagesFromTable(string cmd)
		{
			if (companiesGrid.Columns.Count != 9) return;

			DataTable dt = WorkWithDataBase.SpecifitSelectFieldsFromTable(cmd, _connectionString);

			List<Bitmap> bmps = new List<Bitmap>();
			var dataArray = dt.Rows.Cast<DataRow>().Select(x => x.ItemArray[1]);
			foreach (var i in dataArray)
			{
				try
				{
					using (var ms = new MemoryStream((byte[])i))
					{
						bmps.Add(new Bitmap(ms));
					}
				}
				catch (Exception)
				{
					bmps.Add(new Bitmap(10, 10));
				}
			}

			//добавил столбец для изображений
			DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
			imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
			imageColumn.Description = "Zoomed";
			companiesGrid.Columns.Add(imageColumn);

			for (int i = 0; i < dataArray.Count(); i++)
            {
				companiesGrid.Rows[i].Cells[9].Value = bmps[i];
			}
		}

		private void SetNamesOfAllColumnsOfAllTables(int tableIndex, string where = "")
		{
			switch (tableIndex)
			{
				case 0:
					if (companiesGrid.Columns.Count == 10)
					{
						companiesGrid.ClearSelection();
						companiesGrid.Columns[0].HeaderText = "ID";
						companiesGrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
						companiesGrid.Columns[1].HeaderText = "Название";
						companiesGrid.Columns[2].HeaderText = "Тип собственности";
						companiesGrid.Columns[3].HeaderText = "Номер лицензии";
						companiesGrid.Columns[4].HeaderText = "Дата окончания лицензии";
						companiesGrid.Columns[5].HeaderText = "Город";
						companiesGrid.Columns[6].HeaderText = "Адрес";
						companiesGrid.Columns[7].HeaderText = "Телефон";
						companiesGrid.Columns[8].HeaderText = "Год открытия";
						companiesGrid.Columns[9].HeaderText = "Фото лицензии";
						companiesGrid.Columns[9].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
					}
					numberOfRecorsLabel.Text = "Количество полей: " + WorkWithDataBase.SelectRowsCountFromTable($"all_companies_view {where}", _connectionString);//companiesGrid.Rows.Count;
					headerTextLabel.Text = $"Таблица '{DBTablesTab.SelectedTab.Text}'";
					break;

				case 1:
					if (propertyTypesGrid.Columns.Count == 2)
					{
						propertyTypesGrid.ClearSelection();
						propertyTypesGrid.Columns[0].HeaderText = "ID";
						propertyTypesGrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
						propertyTypesGrid.Columns[1].HeaderText = "Тип собственности";
						propertyTypesGrid.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
					}
					label2.Text = "Количество полей: " + WorkWithDataBase.SelectRowsCountFromTable($"all_property_types_view {where}", _connectionString);//propertyTypesGrid.Rows.Count;
					headerTextLabel.Text = $"Справочник '{DBTablesTab.SelectedTab.Text}'";
					break;

				case 2:
					if (branchesGrid.Columns.Count == 9)
					{
						branchesGrid.ClearSelection();
						branchesGrid.Columns[0].HeaderText = "ID";
						branchesGrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
						branchesGrid.Columns[1].HeaderText = "Название";
						branchesGrid.Columns[2].HeaderText = "Компания";
						branchesGrid.Columns[3].HeaderText = "Город";
						branchesGrid.Columns[4].HeaderText = "Главный офис";
						branchesGrid.Columns[5].HeaderText = "Адрес";
						branchesGrid.Columns[6].HeaderText = "Телефон";
						branchesGrid.Columns[7].HeaderText = "Год открытия";
						branchesGrid.Columns[8].HeaderText = "Число сотрудников";
					}
					label3.Text = "Количество полей: " + WorkWithDataBase.SelectRowsCountFromTable($"all_branches_view {where}", _connectionString);//branchesGrid.Rows.Count;
					headerTextLabel.Text = $"Таблица '{DBTablesTab.SelectedTab.Text}'";
					break;

				case 3:
					if (employeesGrid.Columns.Count == 5)
					{
						employeesGrid.ClearSelection();
						employeesGrid.Columns[0].HeaderText = "ID";
						employeesGrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
						employeesGrid.Columns[1].HeaderText = "Фамилия";
						employeesGrid.Columns[2].HeaderText = "Имя";
						employeesGrid.Columns[3].HeaderText = "Отчество";
						employeesGrid.Columns[4].HeaderText = "Филиал";
						employeesGrid.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
					}
					label4.Text = "Количество полей: " + WorkWithDataBase.SelectRowsCountFromTable($"all_employees_view {where}", _connectionString);//employeesGrid.Rows.Count;
					headerTextLabel.Text = $"Таблица '{DBTablesTab.SelectedTab.Text}'";
					break;

				case 4:
					if (contractsGrid.Columns.Count == 7)
					{
						contractsGrid.ClearSelection();
						contractsGrid.Columns[0].HeaderText = "ID";
						contractsGrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
						contractsGrid.Columns[1].HeaderText = "Вид страхования";
						contractsGrid.Columns[2].HeaderText = "ФИО сотрудника";
						contractsGrid.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
						contractsGrid.Columns[3].HeaderText = "ФИО клиента";
						contractsGrid.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
						contractsGrid.Columns[4].HeaderText = "Текст договора";
						contractsGrid.Columns[5].HeaderText = "Сумма договора";
						contractsGrid.Columns[6].HeaderText = "Дата заключения";
					}
					label5.Text = "Количество полей: " + WorkWithDataBase.SelectRowsCountFromTable($"all_contracts_view {where}", _connectionString);//contractsGrid.Rows.Count;
					headerTextLabel.Text = $"Таблица '{DBTablesTab.SelectedTab.Text}'";
					break;

				case 5:
					if (typesOfInsuranceGrid.Columns.Count == 2)
					{
						typesOfInsuranceGrid.ClearSelection();
						typesOfInsuranceGrid.Columns[0].HeaderText = "ID";
						typesOfInsuranceGrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
						typesOfInsuranceGrid.Columns[1].HeaderText = "Вид страхования";
						typesOfInsuranceGrid.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
					}
					label6.Text = "Количество полей: " + WorkWithDataBase.SelectRowsCountFromTable($"all_types_of_insurance_view {where}", _connectionString);//typesOfInsuranceGrid.Rows.Count;
					headerTextLabel.Text = $"Справочник '{DBTablesTab.SelectedTab.Text}'";
					break;

				case 6:
					if (clientsGrid.Columns.Count == 9)
					{
						clientsGrid.ClearSelection();
						clientsGrid.Columns[0].HeaderText = "ID";
						clientsGrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
						clientsGrid.Columns[1].HeaderText = "Фамилия";
						clientsGrid.Columns[2].HeaderText = "Имя";
						clientsGrid.Columns[3].HeaderText = "Отчество";
						clientsGrid.Columns[4].HeaderText = "Дата рождения";
						clientsGrid.Columns[5].HeaderText = "Социальное положение";
						clientsGrid.Columns[6].HeaderText = "Город";
						clientsGrid.Columns[7].HeaderText = "Адрес";
						clientsGrid.Columns[8].HeaderText = "Телефон";
					}
					label7.Text = "Количество полей: " + WorkWithDataBase.SelectRowsCountFromTable($"all_clients_view {where}", _connectionString);//clientsGrid.Rows.Count;
					headerTextLabel.Text = $"Таблица '{DBTablesTab.SelectedTab.Text}'";
					break;

				case 7:
					if (socialStatusOfClientsGrid.Columns.Count == 2)
					{
						socialStatusOfClientsGrid.ClearSelection();
						socialStatusOfClientsGrid.Columns[0].HeaderText = "ID";
						socialStatusOfClientsGrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
						socialStatusOfClientsGrid.Columns[1].HeaderText = "Социальное положение";
						socialStatusOfClientsGrid.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
					}
					label8.Text = "Количество полей: " + WorkWithDataBase.SelectRowsCountFromTable($"all_social_status_of_clients_view {where}", _connectionString);//socialStatusOfClientsGrid.Rows.Count;
					headerTextLabel.Text = $"Справочник '{DBTablesTab.SelectedTab.Text}'";
					break;

				case 8:
					if (citiesGrid.Columns.Count == 2)
					{
						citiesGrid.ClearSelection();
						citiesGrid.Columns[0].HeaderText = "ID";
						citiesGrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
						citiesGrid.Columns[1].HeaderText = "Город";
						citiesGrid.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
					}
					label9.Text = "Количество полей: " + WorkWithDataBase.SelectRowsCountFromTable($"all_cities_view {where}", _connectionString);//citiesGrid.Rows.Count;
					headerTextLabel.Text = $"Справочник '{DBTablesTab.SelectedTab.Text}'";
					break;
			}
		}

		//соблюдается последовательно добавлений строк в таблицы, для того чтобы избеждать дополнительных изменений при добавлениях
		private async void generationToolStripMenuItem_Click(object sender, EventArgs e)
		{
			clearAllToolStripMenuItem.Enabled = false;
			deleteButton.Enabled = false;
			editButton.Enabled = false;

			string msg = await interaction.GenerateAllTablesAsync(_connectionString);		

			LoadTable(DBTablesTab.SelectedIndex);

			clearAllToolStripMenuItem.Enabled = true;
			deleteButton.Enabled = true;
			editButton.Enabled = true;

			MessageBox.Show(msg);
		}

		private async void clearAllToolStripMenuItem_Click(object sender, EventArgs e)
		{
			generationToolStripMenuItem.Enabled = false;
			addButton.Enabled = false;
			deleteButton.Enabled = false;
			editButton.Enabled = false;

			string msg = await interaction.ClearAllTablesAsync(_connectionString);

			LoadTable(DBTablesTab.SelectedIndex);

			generationToolStripMenuItem.Enabled = true;
			addButton.Enabled = true;
			deleteButton.Enabled = true;
			editButton.Enabled = true;

			MessageBox.Show(msg);
		}

        private void addButton_Click(object sender, EventArgs e)
        {
			if (DBTablesTab.SelectedIndex == 9) return;

			AddOrEditElementForm addElem = new AddOrEditElementForm(HowAddOrEdit.DefaultAdd, DBTablesTab.SelectedIndex, _connectionString);
			addElem.ShowDialog();
			LoadTable(DBTablesTab.SelectedIndex);
		}

        private void selectAllCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
			switch (DBTablesTab.SelectedIndex)
			{
				case 0: //Компании
					{
						if (companiesGrid.SelectedRows.Count == 0) return;
						if (MessageBox.Show("Удаление данных из текущей таблицы может повлечь за собой удаление данных из других таблиц: Филиалы, Сотрудники, Договоры\n\nВсе равно далить?",
							"Warning!", MessageBoxButtons.YesNo) == DialogResult.No) return;

						if (companiesGrid.SelectedRows.Count == WorkWithDataBase.SelectRowsCountFromTable("companies", _connectionString))
						{
							WorkWithDataBase.ClearTableCascade("companies", _connectionString);
						}
						else // если выбраны не все элементы - проходимся (удаляем) выбранные по одному
						{
							for (int i = 0; i < companiesGrid.SelectedRows.Count; i++)
							{
								WorkWithDataBase.RemoveLineFromTable("companies", $"company_id = {companiesGrid.SelectedRows[i].Cells[0].Value}", _connectionString);
							}
						}
					}
					break;
				case 1: //Типы собственности
					{
						if (propertyTypesGrid.SelectedRows.Count == 0) return;
						if (MessageBox.Show("Удаление данных из текущей таблицы может повлечь за собой удаление данных из других таблиц: Компании, Филиалы, Сотрудники, Договоры\n\nВсе равно далить?",
							"Warning!", MessageBoxButtons.YesNo) == DialogResult.No) return;

						if (propertyTypesGrid.SelectedRows.Count == WorkWithDataBase.SelectRowsCountFromTable("property_types", _connectionString))
						{
							WorkWithDataBase.ClearTableCascade("property_types", _connectionString);
						}
						else
						{
							for (int i = 0; i < propertyTypesGrid.SelectedRows.Count; i++)
							{
								WorkWithDataBase.RemoveLineFromTable("property_types", $"type_id = {propertyTypesGrid.SelectedRows[i].Cells[0].Value}", _connectionString);
							}
						}
					}
					break;
				case 2: //Филиалы
					{
						if (branchesGrid.SelectedRows.Count == 0) return;
						if (MessageBox.Show("Удаление данных из текущей таблицы может повлечь за собой удаление данных из других таблиц: Сотрудники, Договоры\n\nВсе равно далить?",
							"Warning!", MessageBoxButtons.YesNo) == DialogResult.No) return;

						if (branchesGrid.SelectedRows.Count == WorkWithDataBase.SelectRowsCountFromTable("branches", _connectionString))
						{
							WorkWithDataBase.ClearTableCascade("branches", _connectionString);
						}
						else
						{
							for (int i = 0; i < branchesGrid.SelectedRows.Count; i++)
							{
								WorkWithDataBase.RemoveLineFromTable("branches", $"branch_id = {branchesGrid.SelectedRows[i].Cells[0].Value}", _connectionString);
							}
						}
					}
					break;
				case 3: //Сотрудники
					{
						if (employeesGrid.SelectedRows.Count == 0) return;
						if (MessageBox.Show("Удаление данных из текущей таблицы может повлечь за собой удаление данных из других таблиц: Договоры\n\nВсе равно далить?",
							"Warning!", MessageBoxButtons.YesNo) == DialogResult.No) return;

						if (employeesGrid.SelectedRows.Count == WorkWithDataBase.SelectRowsCountFromTable("employees", _connectionString))
						{
							WorkWithDataBase.ClearTableCascade("employees", _connectionString);
						}
						else
						{
							for (int i = 0; i < employeesGrid.SelectedRows.Count; i++)
							{
								WorkWithDataBase.RemoveLineFromTable("employees", $"employee_id = {employeesGrid.SelectedRows[i].Cells[0].Value}", _connectionString);
							}
						}
					}
					break;
				case 4: //Договоры
					{
						if (contractsGrid.SelectedRows.Count == 0) return;

						if (contractsGrid.SelectedRows.Count == WorkWithDataBase.SelectRowsCountFromTable("contracts", _connectionString))
						{
							WorkWithDataBase.ClearTableCascade("contracts", _connectionString);
						}
						else
						{
							for (int i = 0; i < contractsGrid.SelectedRows.Count; i++)
							{
								WorkWithDataBase.RemoveLineFromTable("contracts", $"contract_id = {contractsGrid.SelectedRows[i].Cells[0].Value}", _connectionString);
							}
						}
					}
					break;
				case 5: //Виды страхования
					{
						if (typesOfInsuranceGrid.SelectedRows.Count == 0) return;
						if (MessageBox.Show("Удаление данных из текущей таблицы может повлечь за собой удаление данных из других таблиц: Договоры\n\nВсе равно далить?",
							"Warning!", MessageBoxButtons.YesNo) == DialogResult.No) return;

						if (typesOfInsuranceGrid.SelectedRows.Count == WorkWithDataBase.SelectRowsCountFromTable("types_of_insurance", _connectionString))
						{
							WorkWithDataBase.ClearTableCascade("types_of_insurance", _connectionString);
						}
						else
						{
							for (int i = 0; i < typesOfInsuranceGrid.SelectedRows.Count; i++)
							{
								WorkWithDataBase.RemoveLineFromTable("types_of_insurance", $"type_of_insurance_id = {typesOfInsuranceGrid.SelectedRows[i].Cells[0].Value}", _connectionString);
							}
						}
					}
					break;
				case 6: //Клиенты
					{
						if (clientsGrid.SelectedRows.Count == 0) return;
						if (MessageBox.Show("Удаление данных из текущей таблицы может повлечь за собой удаление данных из других таблиц: Договоры\n\nВсе равно далить?",
							"Warning!", MessageBoxButtons.YesNo) == DialogResult.No) return;

						if (clientsGrid.SelectedRows.Count == WorkWithDataBase.SelectRowsCountFromTable("clients", _connectionString))
						{
							WorkWithDataBase.ClearTableCascade("clients", _connectionString);
						}
						else
						{
							for (int i = 0; i < clientsGrid.SelectedRows.Count; i++)
							{
								WorkWithDataBase.RemoveLineFromTable("clients", $"client_id = {clientsGrid.SelectedRows[i].Cells[0].Value}", _connectionString);
							}
						}
					}
					break;
				case 7: //Соц. положения
					{
						if (socialStatusOfClientsGrid.SelectedRows.Count == 0) return;
						if (MessageBox.Show("Удаление данных из текущей таблицы может повлечь за собой удаление данных из других таблиц: Клиенты, Договоры\n\nВсе равно далить?",
							"Warning!", MessageBoxButtons.YesNo) == DialogResult.No) return;

						if (socialStatusOfClientsGrid.SelectedRows.Count == WorkWithDataBase.SelectRowsCountFromTable("social_status_of_clients", _connectionString))
						{
							WorkWithDataBase.ClearTableCascade("social_status_of_clients", _connectionString);
						}
						else
						{
							for (int i = 0; i < socialStatusOfClientsGrid.SelectedRows.Count; i++)
							{
								WorkWithDataBase.RemoveLineFromTable("social_status_of_clients", $"social_status_id = {socialStatusOfClientsGrid.SelectedRows[i].Cells[0].Value}", _connectionString);
							}
						}
					}
					break;
				case 8: //Города 
					{
						if (citiesGrid.SelectedRows.Count == 0) return;
						if (MessageBox.Show("Удаление данных из текущей таблицы может повлечь за собой удаление данных из других таблиц:  Компании, Филиалы, Сотрудники, Клиенты, Договоры\n\nВсе равно далить?",
							"Warning!", MessageBoxButtons.YesNo) == DialogResult.No) return;

						if (citiesGrid.SelectedRows.Count == WorkWithDataBase.SelectRowsCountFromTable("cities", _connectionString))
						{
							WorkWithDataBase.ClearTableCascade("cities", _connectionString);
						}
						else
						{
							for (int i = 0; i < citiesGrid.SelectedRows.Count; i++)
							{
								WorkWithDataBase.RemoveLineFromTable("cities", $"city_id = {citiesGrid.SelectedRows[i].Cells[0].Value}", _connectionString);
							}
						}
					}
					break;
				case 9: //Запросы
					{

					}
					break;
			}
			LoadTable(DBTablesTab.SelectedIndex);
		}

        private void editButton_Click(object sender, EventArgs e)
        {
			if (DBTablesTab.SelectedIndex == 9) return;

			int ID = 0;
			DataGridView table = companiesGrid;

			switch (DBTablesTab.SelectedIndex)
			{
				case 0: //Компании
					{
						table = companiesGrid;
					}
					break;
				case 1: //Типы собственности
					{
						table = propertyTypesGrid;
					}
					break;
				case 2: //Филиалы
					{
						table = branchesGrid;
					}
					break;
				case 3: //Сотрудники
					{
						table = employeesGrid;
					}
					break;
				case 4: //Договоры
					{
						table = contractsGrid;
					}
					break;
				case 5: //Виды страхования
					{
						table = typesOfInsuranceGrid;
					}
					break;
				case 6: //Клиенты
					{
						table = clientsGrid;
					}
					break;
				case 7: //Соц. положения
					{
						table = socialStatusOfClientsGrid;
					}
					break;
				case 8: //Города 
					{
						table = citiesGrid;
					}
					break;
				default:
					break;
			}

			if (table.SelectedRows.Count != 1)
			{
				MessageBox.Show("Выберите один элемент для редактирования!", "Error!");
				return;
			}

			ID = Convert.ToInt32(table.SelectedRows[0].Cells[0].Value);

			AddOrEditElementForm editElem = new AddOrEditElementForm(HowAddOrEdit.DefaultEdit, DBTablesTab.SelectedIndex, _connectionString, ID);
			editElem.ShowDialog();

			LoadTable(DBTablesTab.SelectedIndex);
		}

        private void companiesGrid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void selectAllButton_Click(object sender, EventArgs e)
        {
			DataGridView table = companiesGrid;
			switch (DBTablesTab.SelectedIndex)
            {
				case 0: //Компании
					{
						table = companiesGrid;
					}
					break;
				case 1: //Типы собственности
					{
						table = propertyTypesGrid;
					}
					break;
				case 2: //Филиалы
					{
						table = branchesGrid;
					}
					break;
				case 3: //Сотрудники
					{
						table = employeesGrid;
					}
					break;
				case 4: //Договоры
					{
						table = contractsGrid;
					}
					break;
				case 5: //Виды страхования
					{
						table = typesOfInsuranceGrid;
					}
					break;
				case 6: //Клиенты
					{
						table = clientsGrid;
					}
					break;
				case 7: //Соц. положения
					{
						table = socialStatusOfClientsGrid;
					}
					break;
				case 8: //Города 
					{
						table = citiesGrid;
					}
					break;
				case 9: //Запросы
					{
						table = requestsGrid;
					}
					break;

				default:
					break;
			}

			
			if (table.SelectedRows.Count == table.Rows.Count)
			{
				table.ClearSelection();
			}
			else
			{
				table.SelectAll();
			}
		}

        private void clientsGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
			try
			{
				DataGridViewTextBoxCell cell = (DataGridViewTextBoxCell)clientsGrid.Rows[e.RowIndex].Cells[0]; //узнаю айди клиента на которого кликнули

				ClientContractsForm CC = new ClientContractsForm((int)cell.Value, _connectionString);
				CC.Show();
			}
			catch (Exception)
			{

			}
		}

        private void searchButton_Click(object sender, EventArgs e)
        {
			if (DBTablesTab.SelectedIndex == 9) return;

			int index = DBTablesTab.SelectedIndex;
			SearchForm theSearch = new SearchForm(index, _connectionString);
			theSearch.ShowDialog();

			if (theSearch._stringOfSelect == null) return;

			LoadSearchTalbe(index, theSearch._stringOfSelect);
		}

		private void LoadSearchTalbe(int index, string cmd)
        {
			switch (index)
			{
				case 0: //Компании
					{
						companiesGrid.Columns.Clear();

						DataTable dt = WorkWithDataBase.SpecifitSelectFieldsFromTable(cmd, _connectionString);
						if (dt != null)
						{

							companiesGrid.DataSource = dt;

							companiesGrid.BackgroundColor = Color.White;
							if (companiesGrid.Columns.Count == 10)
							{
								companiesGrid.Columns.RemoveAt(9);
							}

							string cmdImage = cmd.Replace("SELECT * FROM all_companies_view", "SELECT company_id, license_photo FROM all_companies_view");
							SetImagesFromTable(cmdImage);
							SetNamesOfAllColumnsOfAllTables(0, cmd.Replace("SELECT * FROM all_companies_view", ""));
						}
					}
					break;
				case 1: //Типы собственности
					{
						DataTable dt = WorkWithDataBase.SpecifitSelectFieldsFromTable(cmd, _connectionString);
						if (dt != null)
						{
							propertyTypesGrid.DataSource = dt;

							propertyTypesGrid.BackgroundColor = Color.White;
							SetNamesOfAllColumnsOfAllTables(1, cmd.Replace("SELECT * FROM all_property_types_view", ""));
						}
					}
					break;
				case 2: //Филиалы
					{
						DataTable dt = WorkWithDataBase.SpecifitSelectFieldsFromTable(cmd, _connectionString);
						if (dt != null)
						{
							branchesGrid.DataSource = dt;

							branchesGrid.BackgroundColor = Color.White;
							SetNamesOfAllColumnsOfAllTables(2, cmd.Replace("SELECT * FROM all_branches_view", ""));
						}
					}
					break;
				case 3: //Сотрудники
					{
						DataTable dt = WorkWithDataBase.SpecifitSelectFieldsFromTable(cmd, _connectionString);
						if (dt != null)
						{
							employeesGrid.DataSource = dt;

							employeesGrid.BackgroundColor = Color.White;
							SetNamesOfAllColumnsOfAllTables(3, cmd.Replace("SELECT * FROM all_employees_view", ""));
						}
					}
					break;
				case 4: //Договоры
					{
						DataTable dt = WorkWithDataBase.SpecifitSelectFieldsFromTable(cmd, _connectionString);
						if (dt != null)
						{
							contractsGrid.DataSource = dt;

							contractsGrid.BackgroundColor = Color.White;
							SetNamesOfAllColumnsOfAllTables(4, cmd.Replace("SELECT * FROM all_contracts_view", ""));
						}
					}
					break;
				case 5: //Виды страхования
					{
						DataTable dt = WorkWithDataBase.SpecifitSelectFieldsFromTable(cmd, _connectionString);
						if (dt != null)
						{
							typesOfInsuranceGrid.DataSource = dt;

							typesOfInsuranceGrid.BackgroundColor = Color.White;
							SetNamesOfAllColumnsOfAllTables(5, cmd.Replace("SELECT * FROM all_types_of_insurance_view", ""));
						}
					}
					break;
				case 6: //Клиенты
					{
						DataTable dt = WorkWithDataBase.SpecifitSelectFieldsFromTable(cmd, _connectionString);
						if (dt != null)
						{
							clientsGrid.DataSource = dt;

							clientsGrid.BackgroundColor = Color.White;
							SetNamesOfAllColumnsOfAllTables(6, cmd.Replace("SELECT * FROM all_clients_view", ""));
						}
					}
					break;
				case 7: //Соц. положения
					{
						DataTable dt = WorkWithDataBase.SpecifitSelectFieldsFromTable(cmd, _connectionString);
						if (dt != null)
						{
							socialStatusOfClientsGrid.DataSource = dt;

							socialStatusOfClientsGrid.BackgroundColor = Color.White;
							SetNamesOfAllColumnsOfAllTables(7, cmd.Replace("SELECT * FROM all_social_status_of_clients_view", ""));
						}
					}
					break;
				case 8: //Города 
					{
						DataTable dt = WorkWithDataBase.SpecifitSelectFieldsFromTable(cmd, _connectionString);
						if (dt != null)
						{
							citiesGrid.DataSource = dt;
							citiesGrid.BackgroundColor = Color.White;
							SetNamesOfAllColumnsOfAllTables(8, cmd.Replace("SELECT * FROM all_cities_view", ""));
						}
						label9.Text = "Количество полей: " + citiesGrid.Rows.Count;
					}
					break;
				case 9: //Запросы
					{
						requestsGrid.BackgroundColor = Color.White;


						label10.Text = "Количество полей: " + requestsGrid.Rows.Count;
					}
					break;

				default:
					break;
			}
		}

        private void tableCompaniesToolStripItem_Click(object sender, EventArgs e)
        {
			DBTablesTab.SelectedIndex = 0;
		}

        private void tableBranchesToolStripItem_Click(object sender, EventArgs e)
        {
			DBTablesTab.SelectedIndex = 2;
		}

        private void tableEmployeesToolStripItem_Click(object sender, EventArgs e)
        {
			DBTablesTab.SelectedIndex = 3;
		}

        private void tableClientsToolStripItem_Click(object sender, EventArgs e)
        {
			DBTablesTab.SelectedIndex = 6;
		}

        private void tableContractsToolStripItem_Click(object sender, EventArgs e)
        {
			DBTablesTab.SelectedIndex = 4;
		}

        private void directoryPropertyTypesToolStripItem_Click(object sender, EventArgs e)
        {
			DBTablesTab.SelectedIndex = 1;
		}

        private void diractoryTypesOfInsuranceToolStripItem_Click(object sender, EventArgs e)
        {
			DBTablesTab.SelectedIndex = 5;
		}

        private void diracotrySocialStatusOfClientsToolStripItem_Click(object sender, EventArgs e)
        {
			DBTablesTab.SelectedIndex = 7;
		}

        private void directoryCitiesToolStripItem_Click(object sender, EventArgs e)
        {
			DBTablesTab.SelectedIndex = 8;
		}

        private void requestsToolStripItem_Click(object sender, EventArgs e)
        {
			DBTablesTab.SelectedIndex = 9;
		}

        private void MainMenuForm_Resize(object sender, EventArgs e)
        {
			panel1.Size = new Size(this.Width, panel1.Size.Height);
		}

        private void applyQueryButton_Click(object sender, EventArgs e)
        {
			exportToExelButton.Enabled = false;

			switch (queryNamesComboBox.SelectedIndex)
            {
				case 0:
                    {
						if (queryComboBox.SelectedItem == null) return;

						DFAOE.CitiesNamesAndIDs.TryGetValue(queryComboBox.Text, out int cityID);
						string cmd = String.Format(Queries.innerJoinWithForeignKey1, cityID);

						DataTable dt = WorkWithDataBase.SpecifitSelectFieldsFromTable(cmd, _connectionString);
						if (dt != null)
						{
							requestsGrid.DataSource = dt;

							if (requestsGrid.Columns.Count == Queries.queryColumnsNames[0].Count())
							{
								requestsGrid.BackgroundColor = Color.White;
								requestsGrid.ClearSelection();
								requestsGrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

								for (int i = 0; i < Queries.queryColumnsNames[0].Count(); i++)
								{
									requestsGrid.Columns[i].HeaderText = Queries.queryColumnsNames[0][i];
								}

								exportToExelButton.Enabled = true;
								interaction.indexOfQueriesComboBox = 0;
								interaction.isSpecialQuery = false;
							}
						}

						label10.Text = "Количество полей: " + requestsGrid.Rows.Count;
					}
					break;
				case 1:
					{
						if (queryComboBox.SelectedItem == null) return;

						DFAOE.BranchesNamesAndIDs.TryGetValue(queryComboBox.Text, out int branchID);
						string cmd = String.Format(Queries.innerJoinWithForeignKey2, branchID);

						DataTable dt = WorkWithDataBase.SpecifitSelectFieldsFromTable(cmd, _connectionString);
						if (dt != null)
						{
							requestsGrid.DataSource = dt;

							if (requestsGrid.Columns.Count == Queries.queryColumnsNames[1].Count())
							{
								requestsGrid.BackgroundColor = Color.White;
								requestsGrid.ClearSelection();
								requestsGrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

								for (int i = 0; i < Queries.queryColumnsNames[1].Count(); i++)
								{
									requestsGrid.Columns[i].HeaderText = Queries.queryColumnsNames[1][i];
								}

								requestsGrid.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;


								exportToExelButton.Enabled = true;
								interaction.indexOfQueriesComboBox = 1;
								interaction.isSpecialQuery = false;
							}
						}

						label10.Text = "Количество полей: " + requestsGrid.Rows.Count;
					}
					break;
				case 2:
					{
						DataTable dt = WorkWithDataBase.SpecifitSelectFieldsFromTable(Queries.leftOuterJoin, _connectionString);
						if (dt != null)
						{
							requestsGrid.DataSource = dt;

							if (requestsGrid.Columns.Count == Queries.queryColumnsNames[2].Count())
							{
								requestsGrid.BackgroundColor = Color.White;
								requestsGrid.ClearSelection();
								requestsGrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

								for (int i = 0; i < Queries.queryColumnsNames[2].Count(); i++)
								{
									requestsGrid.Columns[i].HeaderText = Queries.queryColumnsNames[2][i];
								}

								exportToExelButton.Enabled = true;
								interaction.indexOfQueriesComboBox = 2;
								interaction.isSpecialQuery = false;
							}
						}

						label10.Text = "Количество полей: " + requestsGrid.Rows.Count;
					}
					break;
				case 3:
					{
						DataTable dt = WorkWithDataBase.SpecifitSelectFieldsFromTable(Queries.rightOuterJoin, _connectionString);
						if (dt != null)
						{
							requestsGrid.DataSource = dt;

							if (requestsGrid.Columns.Count == Queries.queryColumnsNames[3].Count())
							{
								requestsGrid.BackgroundColor = Color.White;
								requestsGrid.ClearSelection();
								requestsGrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

								for (int i = 0; i < Queries.queryColumnsNames[3].Count(); i++)
								{
									requestsGrid.Columns[i].HeaderText = Queries.queryColumnsNames[3][i];
								}


								exportToExelButton.Enabled = true;
								interaction.indexOfQueriesComboBox = 3;
								interaction.isSpecialQuery = false;
							}
						}

						label10.Text = "Количество полей: " + requestsGrid.Rows.Count;
					}
					break;
				case 4:
					{
						DataTable dt = WorkWithDataBase.SpecifitSelectFieldsFromTable(Queries.queryOnQueryByLeftJoinPrinciple, _connectionString);
						if (dt != null)
						{
							requestsGrid.DataSource = dt;

							if (requestsGrid.Columns.Count == Queries.queryColumnsNames[4].Count())
							{
								requestsGrid.BackgroundColor = Color.White;
								requestsGrid.ClearSelection();
								requestsGrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

								for (int i = 0; i < Queries.queryColumnsNames[4].Count(); i++)
								{
									requestsGrid.Columns[i].HeaderText = Queries.queryColumnsNames[4][i];
								}

								exportToExelButton.Enabled = true;
								interaction.indexOfQueriesComboBox = 4;
								interaction.isSpecialQuery = false;
							}
						}

						label10.Text = "Количество полей: " + requestsGrid.Rows.Count;
					}
					break;
				case 5:
					{
						DataTable dt = WorkWithDataBase.SpecifitSelectFieldsFromTable(Queries.finalQueryWithoutCondition, _connectionString);
						if (dt != null)
						{
							requestsGrid.DataSource = dt;

							if (requestsGrid.Columns.Count == Queries.queryColumnsNames[5].Count())
							{
								requestsGrid.BackgroundColor = Color.White;
								requestsGrid.ClearSelection();
								requestsGrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

								for (int i = 0; i < Queries.queryColumnsNames[5].Count(); i++)
								{
									requestsGrid.Columns[i].HeaderText = Queries.queryColumnsNames[5][i];
								}

								requestsGrid.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

								exportToExelButton.Enabled = true;
								interaction.indexOfQueriesComboBox = 5;
								interaction.isSpecialQuery = false;
							}
						}

						label10.Text = "Количество полей: " + requestsGrid.Rows.Count;
					}
					break;
				case 6:
					{
						DataTable dt = WorkWithDataBase.SpecifitSelectFieldsFromTable(Queries.totalIncluding, _connectionString);
						if (dt != null)
						{
							requestsGrid.DataSource = dt;

							if (requestsGrid.Columns.Count == Queries.queryColumnsNames[6].Count())
							{
								requestsGrid.BackgroundColor = Color.White;
								requestsGrid.ClearSelection();
								requestsGrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

								for (int i = 0; i < Queries.queryColumnsNames[6].Count(); i++)
								{
									requestsGrid.Columns[i].HeaderText = Queries.queryColumnsNames[6][i];
								}

								//exportToExelButton.Visible = true;
								exportToExelButton.Enabled = true;
								interaction.indexOfQueriesComboBox = 6;
								interaction.isSpecialQuery = false;
							}
						}

						label10.Text = "Количество полей: " + requestsGrid.Rows.Count;
					}
					break;
				case 7:
					{
						if (!Int32.TryParse(queryMaskedBox.Text.Trim('_'), out int sum)) return;

						string cmd = String.Format(Queries.summaryQueriesWithConditionOnDataByValue, sum);

						DataTable dt = WorkWithDataBase.SpecifitSelectFieldsFromTable(cmd, _connectionString);
						if (dt != null)
						{
							requestsGrid.DataSource = dt;

							if (requestsGrid.Columns.Count == Queries.queryColumnsNames[7].Count())
							{
								requestsGrid.BackgroundColor = Color.White;
								requestsGrid.ClearSelection();
								requestsGrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

								for (int i = 0; i < Queries.queryColumnsNames[7].Count(); i++)
								{
									requestsGrid.Columns[i].HeaderText = Queries.queryColumnsNames[7][i];
								}

								exportToExelButton.Enabled = true;
								interaction.indexOfQueriesComboBox = 7;
								interaction.isSpecialQuery = false;
							}
						}

						label10.Text = "Количество полей: " + requestsGrid.Rows.Count;
					}
					break;
				case 8:
					{
						if (!Int32.TryParse(queryMaskedBox.Text.Trim('_'), out int phoneNumber)) return;

						string cmd = String.Format(Queries.summaryQueriesWithConditionOnDataByMask, phoneNumber);

						DataTable dt = WorkWithDataBase.SpecifitSelectFieldsFromTable(cmd, _connectionString);
						if (dt != null)
						{
							requestsGrid.DataSource = dt;

							if (requestsGrid.Columns.Count == Queries.queryColumnsNames[8].Count())
							{
								requestsGrid.BackgroundColor = Color.White;
								requestsGrid.ClearSelection();
								requestsGrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

								for (int i = 0; i < Queries.queryColumnsNames[8].Count(); i++)
								{
									requestsGrid.Columns[i].HeaderText = Queries.queryColumnsNames[8][i];
								}

								requestsGrid.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

								exportToExelButton.Enabled = true;
								interaction.indexOfQueriesComboBox = 8;
								interaction.isSpecialQuery = false;
							}
						}

						label10.Text = "Количество полей: " + requestsGrid.Rows.Count;
					}
					break;
				case 9:
					{
						DataTable dt = WorkWithDataBase.SpecifitSelectFieldsFromTable(Queries.summaryQueriesWithConditionOnDataByIndex, _connectionString);
						if (dt != null)
						{
							requestsGrid.DataSource = dt;

							if (requestsGrid.Columns.Count == Queries.queryColumnsNames[9].Count())
							{
								requestsGrid.BackgroundColor = Color.White;
								requestsGrid.ClearSelection();
								requestsGrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

								for (int i = 0; i < Queries.queryColumnsNames[9].Count(); i++)
								{
									requestsGrid.Columns[i].HeaderText = Queries.queryColumnsNames[9][i];
								}

								requestsGrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

								exportToExelButton.Enabled = true;
								interaction.indexOfQueriesComboBox = 9;
								interaction.isSpecialQuery = false;
							}
						}

						label10.Text = "Количество полей: " + requestsGrid.Rows.Count;
					}
					break;
				case 10:
					{
						if (queryComboBox.SelectedItem == null) return;

						DFAOE.EmployeesNamesAndIDs.TryGetValue(queryComboBox.Text, out int branchID);
						string cmd = String.Format(Queries.summaryQueriesWithConditionOnDataWithoutIndex, branchID);

						DataTable dt = WorkWithDataBase.SpecifitSelectFieldsFromTable(cmd, _connectionString);
						if (dt != null)
						{
							requestsGrid.DataSource = dt;

							if (requestsGrid.Columns.Count == Queries.queryColumnsNames[10].Count())
							{
								requestsGrid.BackgroundColor = Color.White;
								requestsGrid.ClearSelection();
								requestsGrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

								for (int i = 0; i < Queries.queryColumnsNames[10].Count(); i++)
								{
									requestsGrid.Columns[i].HeaderText = Queries.queryColumnsNames[10][i];
								}

								requestsGrid.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

								exportToExelButton.Enabled = true;
								interaction.indexOfQueriesComboBox = 10;
								interaction.isSpecialQuery = false;
							}
						}

						label10.Text = "Количество полей: " + requestsGrid.Rows.Count;
					}
					break;
				case 11:
					{
						DataTable dt = WorkWithDataBase.SpecifitSelectFieldsFromTable(Queries.finalQueryWithConditionOnGroups, _connectionString);
						if (dt != null)
						{
							requestsGrid.DataSource = dt;

							if (requestsGrid.Columns.Count == Queries.queryColumnsNames[11].Count())
							{
								requestsGrid.BackgroundColor = Color.White;
								requestsGrid.ClearSelection();
								requestsGrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

								for (int i = 0; i < Queries.queryColumnsNames[11].Count(); i++)
								{
									requestsGrid.Columns[i].HeaderText = Queries.queryColumnsNames[11][i];
								}

								exportToExelButton.Enabled = true;
								interaction.indexOfQueriesComboBox = 11;
								interaction.isSpecialQuery = false;
							}
						}

						label10.Text = "Количество полей: " + requestsGrid.Rows.Count;
					}
					break;
				case 12:
					{
						if (queryComboBox.SelectedItem == null) return;

						DFAOE.CompanyNamesAndIDs.TryGetValue(queryComboBox.Text, out int companyID);
						string cmd = String.Format(Queries.finalQueryWithConditionOnDataAndGroups, companyID, queryDatePicker1.Value.Year, queryDatePicker2.Value.Year);

						DataTable dt = WorkWithDataBase.SpecifitSelectFieldsFromTable(cmd, _connectionString);
						if (dt != null)
						{
							requestsGrid.DataSource = dt;

							if (requestsGrid.Columns.Count == Queries.queryColumnsNames[12].Count())
							{
								requestsGrid.BackgroundColor = Color.White;
								requestsGrid.ClearSelection();
								requestsGrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

								for (int i = 0; i < Queries.queryColumnsNames[12].Count(); i++)
								{
									requestsGrid.Columns[i].HeaderText = Queries.queryColumnsNames[12][i];
								}

								requestsGrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

								exportToExelButton.Enabled = true;
								interaction.indexOfQueriesComboBox = 12;
								interaction.isSpecialQuery = false;
							}
						}

						label10.Text = "Количество полей: " + requestsGrid.Rows.Count;
					}
					break;
				case 13:
					{
						DataTable dt = WorkWithDataBase.SpecifitSelectFieldsFromTable(Queries.requestOnRequestBasedOnPrincipleOfFinalRequest, _connectionString);
						if (dt != null)
						{
							requestsGrid.DataSource = dt;

							if (requestsGrid.Columns.Count == Queries.queryColumnsNames[13].Count())
							{
								requestsGrid.BackgroundColor = Color.White;
								requestsGrid.ClearSelection();
								requestsGrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

								for (int i = 0; i < Queries.queryColumnsNames[13].Count(); i++)
								{
									requestsGrid.Columns[i].HeaderText = Queries.queryColumnsNames[13][i];
								}

								requestsGrid.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
								requestsGrid.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

								exportToExelButton.Enabled = true;
								interaction.indexOfQueriesComboBox = 13;
								interaction.isSpecialQuery = false;
							}
						}

						label10.Text = "Количество полей: " + requestsGrid.Rows.Count;
					}
					break;
				case 14:
					{
						DataTable dt = WorkWithDataBase.SpecifitSelectFieldsFromTable(Queries.queryUsingUnion, _connectionString);
						if (dt != null)
						{
							requestsGrid.DataSource = dt;

							if (requestsGrid.Columns.Count == Queries.queryColumnsNames[14].Count())
							{
								requestsGrid.BackgroundColor = Color.White;
								requestsGrid.ClearSelection();
								requestsGrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

								for (int i = 0; i < Queries.queryColumnsNames[14].Count(); i++)
								{
									requestsGrid.Columns[i].HeaderText = Queries.queryColumnsNames[14][i];
								}

								requestsGrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
								requestsGrid.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

								exportToExelButton.Enabled = true;
								interaction.indexOfQueriesComboBox = 14;
								interaction.isSpecialQuery = false;
							}
						}

						label10.Text = "Количество полей: " + requestsGrid.Rows.Count;
					}
					break;
				case 15:
					{
						if (queryComboBox.SelectedItem == null) return;

						DFAOE.CompanyNamesAndIDs.TryGetValue(queryComboBox.Text, out int companyID);
						string cmd = String.Format(Queries.queriesWithSubqueriesUsingIn, companyID);

						DataTable dt = WorkWithDataBase.SpecifitSelectFieldsFromTable(cmd, _connectionString);
						if (dt != null)
						{
							requestsGrid.DataSource = dt;

							if (requestsGrid.Columns.Count == Queries.queryColumnsNames[15].Count())
							{
								requestsGrid.BackgroundColor = Color.White;
								requestsGrid.ClearSelection();
								requestsGrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

								for (int i = 0; i < Queries.queryColumnsNames[15].Count(); i++)
								{
									requestsGrid.Columns[i].HeaderText = Queries.queryColumnsNames[15][i];
								}

								requestsGrid.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
								requestsGrid.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

								exportToExelButton.Enabled = true;
								interaction.indexOfQueriesComboBox = 15;
								interaction.isSpecialQuery = false;
							}
						}

						label10.Text = "Количество полей: " + requestsGrid.Rows.Count;
					}
					break;
				case 16:
					{
						if (queryComboBox.SelectedItem == null) return;

						DFAOE.CitiesNamesAndIDs.TryGetValue(queryComboBox.Text, out int cityID);
						string cmd = String.Format(Queries.queriesWithSubqueriesUsingNotIn, cityID);

						DataTable dt = WorkWithDataBase.SpecifitSelectFieldsFromTable(cmd, _connectionString);
						if (dt != null)
						{
							requestsGrid.DataSource = dt;

							if (requestsGrid.Columns.Count == Queries.queryColumnsNames[16].Count())
							{
								requestsGrid.BackgroundColor = Color.White;
								requestsGrid.ClearSelection();
								requestsGrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

								for (int i = 0; i < Queries.queryColumnsNames[16].Count(); i++)
								{
									requestsGrid.Columns[i].HeaderText = Queries.queryColumnsNames[16][i];
								}

								requestsGrid.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
								requestsGrid.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
								requestsGrid.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

								exportToExelButton.Enabled = true;
								interaction.indexOfQueriesComboBox = 16;
								interaction.isSpecialQuery = false;
							}
						}

						label10.Text = "Количество полей: " + requestsGrid.Rows.Count;
					}
					break;
				case 17:
					{
						DataTable dt = WorkWithDataBase.SpecifitSelectFieldsFromTable(Queries.queriesWithSubqueriesUsingCase, _connectionString);
						if (dt != null)
						{
							requestsGrid.DataSource = dt;

							if (requestsGrid.Columns.Count == Queries.queryColumnsNames[17].Count())
							{
								requestsGrid.BackgroundColor = Color.White;
								requestsGrid.ClearSelection();
								requestsGrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

								for (int i = 0; i < Queries.queryColumnsNames[17].Count(); i++)
								{
									requestsGrid.Columns[i].HeaderText = Queries.queryColumnsNames[17][i];
								}

								exportToExelButton.Enabled = true;
								interaction.indexOfQueriesComboBox = 17;
								interaction.isSpecialQuery = false;
							}
						}

						label10.Text = "Количество полей: " + requestsGrid.Rows.Count;
					}
					break;
				case 18:
					{
						DataTable dt = WorkWithDataBase.SpecifitSelectFieldsFromTable(Queries.queriesWithSubqueriesUsingWith, _connectionString);
						if (dt != null)
						{
							requestsGrid.DataSource = dt;

							if (requestsGrid.Columns.Count == Queries.queryColumnsNames[18].Count())
							{
								requestsGrid.BackgroundColor = Color.White;
								requestsGrid.ClearSelection();
								requestsGrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

								for (int i = 0; i < Queries.queryColumnsNames[18].Count(); i++)
								{
									requestsGrid.Columns[i].HeaderText = Queries.queryColumnsNames[18][i];
								}

								requestsGrid.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

								exportToExelButton.Enabled = true;
								interaction.indexOfQueriesComboBox = 18;
								interaction.isSpecialQuery = false;
							}
						}

						label10.Text = "Количество полей: " + requestsGrid.Rows.Count;
					}
					break;
				// СПЕЦИАЛЬНЫЕ ЗАПРОСЫ
				case 19:
					{
						string cmd = "";
						if (queryComboBox.SelectedItem == null || (queryComboBox.SelectedItem != null && queryComboBox.Text == "По всем городам"))
						{
							cmd = String.Format(Queries.specialQuery1, "");
						}
						else
						{
							DFAOE.CitiesNamesAndIDs.TryGetValue(queryComboBox.Text, out int cityID);
							cmd = String.Format(Queries.specialQuery1, $"WHERE branches.city_id = {cityID} ");
						}

						DataTable dt = WorkWithDataBase.SpecifitSelectFieldsFromTable(cmd, _connectionString);
						if (dt != null)
						{
							requestsGrid.DataSource = dt;

							if (requestsGrid.Columns.Count == Queries.specialQueryColumnsNames[0].Count())
							{
								requestsGrid.BackgroundColor = Color.White;
								requestsGrid.ClearSelection();
								requestsGrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

								for (int i = 0; i < Queries.specialQueryColumnsNames[0].Count(); i++)
								{
									requestsGrid.Columns[i].HeaderText = Queries.specialQueryColumnsNames[0][i];
								}

								//requestsGrid.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
								//exportToExelButton.Visible = true;
								exportToExelButton.Enabled = true;
								interaction.indexOfQueriesComboBox = 0;
								interaction.isSpecialQuery = true;
							}
						}

						label10.Text = "Количество полей: " + requestsGrid.Rows.Count;
					}
					break;
				case 20:
					{
						DataTable dt = WorkWithDataBase.SpecifitSelectFieldsFromTable(Queries.specialQuery2, _connectionString);
						if (dt != null)
						{
							requestsGrid.DataSource = dt;

							if (requestsGrid.Columns.Count == Queries.specialQueryColumnsNames[1].Count())
							{
								requestsGrid.BackgroundColor = Color.White;
								requestsGrid.ClearSelection();
								requestsGrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

								for (int i = 0; i < Queries.specialQueryColumnsNames[1].Count(); i++)
								{
									requestsGrid.Columns[i].HeaderText = Queries.specialQueryColumnsNames[1][i];
								}

								requestsGrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
								requestsGrid.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

								//exportToExelButton.Visible = true;
								exportToExelButton.Enabled = true;
								interaction.indexOfQueriesComboBox = 1;
								interaction.isSpecialQuery = true;
							}
						}

						label10.Text = "Количество полей: " + requestsGrid.Rows.Count;
					}
					break;
				case 21:
					{
						string[] dates =
						{
							$"'{queryDatePicker1.Value.Year}-{queryDatePicker1.Value.Month}-{queryDatePicker1.Value.Day}'", 
							$"'{queryDatePicker2.Value.Year}-{queryDatePicker2.Value.Month}-{queryDatePicker2.Value.Day}'"
						};

						string cmd = String.Format(Queries.specialQuery3, dates[0], dates[1]);

						DataTable dt = WorkWithDataBase.SpecifitSelectFieldsFromTable(cmd, _connectionString);
						if (dt != null)
						{
							requestsGrid.DataSource = dt;

							if (requestsGrid.Columns.Count == Queries.specialQueryColumnsNames[2].Count())
							{
								requestsGrid.BackgroundColor = Color.White;
								requestsGrid.ClearSelection();
								requestsGrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

								for (int i = 0; i < Queries.specialQueryColumnsNames[2].Count(); i++)
								{
									requestsGrid.Columns[i].HeaderText = Queries.specialQueryColumnsNames[2][i];
								}

								requestsGrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
								requestsGrid.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

								//exportToExelButton.Visible = true;
								exportToExelButton.Enabled = true;
								interaction.indexOfQueriesComboBox = 2;
								interaction.isSpecialQuery = true;
							}
						}

						label10.Text = "Количество полей: " + requestsGrid.Rows.Count;
					}
					break;
			}
		}

        private void queryNamesComboBox_MouseHover(object sender, EventArgs e)
        {
			if (queryNamesComboBox.SelectedItem != null)
			{
				fullNameToolTip.ToolTipTitle = queryNamesComboBox.SelectedItem.ToString();
			}
			else
			{
				fullNameToolTip.ToolTipTitle = "";
			}
		}

        private async void queryNamesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
			queryComboBox.Items.Clear();
			queryMaskedBox.Clear();
			exportToExelButton.Enabled = false;
			//exportToExelButton.Visible = false;
			switch (queryNamesComboBox.SelectedIndex)
			{
				case 0:
					{
						queryDatePicker1.Enabled = false;
						queryDatePicker2.Enabled = false;
						queryMaskedBox.Enabled = false;
						queryComboBox.Enabled = true;

						queryComboBox.Items.AddRange(await DFAOE.SelectCitiesNamesAndIDsAsync(_connectionString));
					}
					break;
				case 1:
					{
						queryDatePicker1.Enabled = false;
						queryDatePicker2.Enabled = false;
						queryMaskedBox.Enabled = false;
						queryComboBox.Enabled = true;

						queryComboBox.Items.AddRange(await DFAOE.SelectBranchesNamesAndIDsAsync(_connectionString));
					}
					break;
				case 2:
				case 3:
				case 4:
				case 5:
				case 6:
				case 9:
				case 11:
				case 13:
				case 14:
				case 17:
				case 18:
				case 20:
					{
						queryDatePicker1.Enabled = false;
						queryDatePicker2.Enabled = false;
						queryMaskedBox.Enabled = false;
						queryComboBox.Enabled = false;
					}
					break;
				case 7:
				case 8:
					{
						queryDatePicker1.Enabled = false;
						queryDatePicker2.Enabled = false;
						queryMaskedBox.Enabled = true;
						queryComboBox.Enabled = false;
					}
					break;
				case 10:
					{
						queryDatePicker1.Enabled = false;
						queryDatePicker2.Enabled = false;
						queryMaskedBox.Enabled = false;
						queryComboBox.Enabled = true;

						queryComboBox.Items.AddRange(await DFAOE.SelectEmployeesNamesAndIDsAsync(_connectionString));
					}
					break;
				
				case 12:
					{
						queryDatePicker1.Enabled = true;
						queryDatePicker2.Enabled = true;
						queryMaskedBox.Enabled = false;
						queryComboBox.Enabled = true;

						queryComboBox.Items.AddRange(await DFAOE.SelectCompnaiesNamesAndIDsAsync(_connectionString));
					}
					break;
				case 15:
					{
						queryDatePicker1.Enabled = false;
						queryDatePicker2.Enabled = false;
						queryMaskedBox.Enabled = false;
						queryComboBox.Enabled = true;

						queryComboBox.Items.AddRange(await DFAOE.SelectCompnaiesNamesAndIDsAsync(_connectionString));
					}
					break;
				case 16:
					{
						queryDatePicker1.Enabled = false;
						queryDatePicker2.Enabled = false;
						queryMaskedBox.Enabled = false;
						queryComboBox.Enabled = true;

						queryComboBox.Items.AddRange(await DFAOE.SelectCitiesNamesAndIDsAsync(_connectionString));
					}
					break;
				case 19:
					{
						queryDatePicker1.Enabled = false;
						queryDatePicker2.Enabled = false;
						queryMaskedBox.Enabled = false;
						queryComboBox.Enabled = true;

						queryComboBox.Items.Add("По всем городам");
						queryComboBox.Items.AddRange(await DFAOE.SelectCitiesNamesAndIDsAsync(_connectionString));
					}
					break;
				case 21:
					{
						queryDatePicker1.Enabled = true;
						queryDatePicker2.Enabled = true;
						queryMaskedBox.Enabled = false;
						queryComboBox.Enabled = false;
					}
					break;
			}
		}

        private void queryComboBox_MouseHover(object sender, EventArgs e)
        {
			if (queryComboBox.SelectedItem != null)
			{
				fullNameToolTip.ToolTipTitle = queryComboBox.SelectedItem.ToString();
			}
			else
			{
				fullNameToolTip.ToolTipTitle = "";
			}
		}

        private void specialQueriesToolStriptem_Click(object sender, EventArgs e)
        {
			LoadTable(9, false); // false = для специальных запросов
        }

        private void exportToExelButton_Click(object sender, EventArgs e)
        {
			var dialog = saveExceleDialog.ShowDialog();
			if (dialog != DialogResult.OK && dialog != DialogResult.Yes) return;

			if (!SaveTableToExcel(saveExceleDialog.FileName))
            {
				MessageBox.Show("Ошибка при экспорте таблицы в Excel");
				return;
            }

			var res = MessageBox.Show("Экспорт завершен.\nYes - открыть сгенерированный файл." +
				"\nNo - не открывать сгенерированный файл.", "Экспорт в Excel", MessageBoxButtons.YesNo);

			if (res == DialogResult.Yes)
            {
				try
				{
					Process.Start(saveExceleDialog.FileName);
				}
				catch(Exception ex)
                {
					MessageBox.Show("Файл не найден!");
                }
			}
		}

		private bool SaveTableToExcel(string fileName)
		{
			try
			{
				DataTable dt = (DataTable)requestsGrid.DataSource;


				if (!interaction.isSpecialQuery) 
				{
					for (int i = 0; i < Queries.queryColumnsNames[interaction.indexOfQueriesComboBox].Count(); i++)
					{
						dt.Columns[i].ColumnName = Queries.queryColumnsNames[interaction.indexOfQueriesComboBox][i];
					}
				}
                else
                {
					for (int i = 0; i < Queries.specialQueryColumnsNames[interaction.indexOfQueriesComboBox].Count(); i++)
					{
						dt.Columns[i].ColumnName = Queries.specialQueryColumnsNames[interaction.indexOfQueriesComboBox][i];
					}
				}

                var file = new FileInfo(fileName);
				Task t = MyDataTableExtensions.SaveExcelFile(dt, file);
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

        private void diagram1DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DiagramsForm DF = new DiagramsForm(_connectionString, 0);
			DF.Show();
        }

		private void diagramСolumnarToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DiagramsForm DF = new DiagramsForm(_connectionString, 1);
			DF.Show();
		}

		private void diagram3DToolStripMenuItem_Click(object sender, EventArgs e)
        {
			DiagramsForm DF = new DiagramsForm(_connectionString, 2);
			DF.Show();
		}

		private void RefreshDataGrid(int index)
        {
			DataGridView table = companiesGrid;
			switch (index)
            {
				case 0:
					table = companiesGrid;
					LoadTable(0);
					break;
				case 1:
					table = propertyTypesGrid;
					break;
				case 2:
					table = branchesGrid;
					break;
				case 3:
					table = employeesGrid;
					break;
				case 4:
					table = contractsGrid;
					break;
				case 5:
					table = typesOfInsuranceGrid;
					break;
				case 6:
					table = clientsGrid;
					break;
				case 7:
					table = socialStatusOfClientsGrid;
					break;
				case 8:
					table = citiesGrid;
					break;
				case 9:
					table = requestsGrid;
					break;
			}

			table.Refresh();
		}

        private void refreshGridButton_Click(object sender, EventArgs e)
        {
			RefreshDataGrid(DBTablesTab.SelectedIndex);
			LoadTable(DBTablesTab.SelectedIndex);
		}

        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
			HelpForm HF = new HelpForm();
			HF.Show();
        }

        private void addButton_MouseHover(object sender, EventArgs e)
        {
			fullNameToolTip.ToolTipTitle = "Добавить";
		}

        private void deleteButton_MouseHover(object sender, EventArgs e)
        {
			fullNameToolTip.ToolTipTitle = "Удалить";
		}

        private void editButton_MouseHover(object sender, EventArgs e)
        {
			fullNameToolTip.ToolTipTitle = "Редактировать";
		}

        private void selectAllButton_MouseHover(object sender, EventArgs e)
        {
			fullNameToolTip.ToolTipTitle = "Выбрать все";
		}

        private void searchButton_MouseHover(object sender, EventArgs e)
        {
			fullNameToolTip.ToolTipTitle = "Поиск";
		}

        private void refreshGridButton_MouseHover(object sender, EventArgs e)
        {
			fullNameToolTip.ToolTipTitle = "Обновить/Вернуть основную таблицу";
		}
    }
}
