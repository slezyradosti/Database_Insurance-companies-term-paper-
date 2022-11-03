using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB_Kursach
{
	public partial class AddOrEditElementForm : Form
	{
		private readonly string _connectionString;
		private DataForAddOrEdit DFAOE;
		private HowAddOrEdit HAOE;
		public AddOrEditElementForm(HowAddOrEdit howAddOrEdit, int tabIndex, string connstring, int id = -1)
		{
			InitializeComponent();
			HAOE = howAddOrEdit;
			_connectionString = connstring;
			DFAOE = new DataForAddOrEdit(tabIndex, id);
		}

		private void AddElement_Load(object sender, EventArgs e)
		{
			addOrEditElementTab.SelectedIndex = DFAOE.tabIndex;

			errorCompanyPropertyTypeLabel.Visible = false;
			errorCompanyCityLabel.Visible = false;
			errorOwnedByCompanyLabel.Visible = false;
			errorBranchCityLabel.Visible = false;
			errorEmployeBranchLabel.Visible = false;
			errorClientCityLabel.Visible = false;
			errorClientSocStatucLabel.Visible = false;
			errorClientDateOfBirthLabel.Visible = false;
			errorContractInsuranceTypeLabel.Visible = false;
			errorContractClientLabel.Visible = false;
			errorContractEmployeeLabel.Visible = false;
			errorContractDateLabel.Visible = false;

			string tableOrDirectory = (HAOE == HowAddOrEdit.DefaultEdit) ? "таблицы" : "таблицу"; // добавление/удаление в таблицу или справочник

			if (addOrEditElementTab.SelectedIndex == 1 || addOrEditElementTab.SelectedIndex == 5 || addOrEditElementTab.SelectedIndex == 7 || addOrEditElementTab.SelectedIndex == 8)
			{
				this.Size = new Size(680, 300);
				panel1.Size = new Size(this.Width, panel1.Size.Height);
				headerTextLabel.Size = new Size(this.Size.Width, headerTextLabel.Size.Height);

				tableOrDirectory = (HAOE == HowAddOrEdit.DefaultEdit) ? "справочника" : "справочник";
			}

			if (HAOE == HowAddOrEdit.DefaultAdd)
			{
				Task t = LoadTableForAdd(addOrEditElementTab.SelectedIndex);
				this.Text = "Добавить элемент";
				headerTextLabel.Text = $"Добавление элемента в {tableOrDirectory} '{addOrEditElementTab.SelectedTab.Text}'";
				
			}
			else if (HAOE == HowAddOrEdit.DefaultEdit) // если запрос на редактирование - загружаем форму с текущими данными
			{
				LoadTableForEdit(addOrEditElementTab.SelectedIndex);
				this.Text = "Изменить элемент";
				headerTextLabel.Text = $"Изменение элемента из {tableOrDirectory} '{addOrEditElementTab.SelectedTab.Text}'";
			}
			else if (HAOE == HowAddOrEdit.CompoundFormAdd) // если запрос на добавление из составной формы
			{
				Task t = LoadTableForAdd(4);
				this.Text = "Добавить элемент";
				headerTextLabel.Text = $"Добавление элемента в таблицу '{addOrEditElementTab.SelectedTab.Text}'";
			}
			else if (HAOE == HowAddOrEdit.CompoundFormEdit) // если запрос на добавление из составной формы
			{
				LoadTableForEdit(4);
				this.Text = "Изменить элемент";
				headerTextLabel.Text = $"Изменение элемента из таблицы '{addOrEditElementTab.SelectedTab.Text}'";
			}

		}

		private void addCompleteButton_Click(object sender, EventArgs e)
		{
			bool isGoodData = true; // ввел ли пользователь подходящие данные

			if (String.IsNullOrEmpty(companyNameBox.Text.Trim()))
			{
				companyNameBox.BackColor = Color.Red;
				isGoodData = false;
			}
			if (typeBox.SelectedItem == null)
			{
				errorCompanyPropertyTypeLabel.Visible = true;
				isGoodData = false;
			}
			if (!Int32.TryParse(licenseNumberBox.Text.Trim('_'), out int licenseNumber))
			{
				licenseNumberBox.BackColor = Color.Red;
				isGoodData = false;
			}
			if (citiesBox.SelectedItem == null)
			{
				errorCompanyCityLabel.Visible = true;
				isGoodData = false;
			}
			if (String.IsNullOrEmpty(addressBox.Text.Trim()))
			{
				addressBox.BackColor = Color.Red;
				isGoodData = false;
			}
			if (!phoneNumberBox.MaskFull)
			{
				phoneNumberBox.BackColor = Color.Red;
				isGoodData = false;
			}
			if (!openingYearBox.MaskFull || Convert.ToInt32(openingYearBox.Text) < 1850 || Convert.ToInt32(openingYearBox.Text) > DateTime.Now.Year)
			{
				openingYearBox.BackColor = Color.Red;
				isGoodData = false;
			}

			if (!isGoodData) return;

			DFAOE.CitiesNamesAndIDs.TryGetValue(citiesBox.Text, out int cityID);
			DFAOE.PTypesNamesAndIDs.TryGetValue(typeBox.Text, out int pTypeID);

			byte[] img = null;
			if (licensePhotoPictureBox.Image != null)
			{
				Bitmap tmp = new Bitmap(licensePhotoPictureBox.Image);
				using (MemoryStream ms = new MemoryStream())
				{
					tmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
					img = ms.ToArray();
				}
			}

			if (HAOE == HowAddOrEdit.DefaultAdd || HAOE == HowAddOrEdit.CompoundFormAdd)
			{

				if (WorkWithDataBase.SpecificInsertValuesWithImageIntoTable("public.companies(company_name, type_id, license_number, license_expiration_date, city_id, address, phone_number, opening_year, license_photo)",
						$"'{companyNameBox.Text.Trim()}', {pTypeID}, {licenseNumber}, '{endOfLicensePicker.Value.Year}-{endOfLicensePicker.Value.Month}-{endOfLicensePicker.Value.Day}', {cityID}, '{addressBox.Text.Trim()}', '{phoneNumberBox.Text}', {Convert.ToInt32(openingYearBox.Text)}", _connectionString, img))
				{
					MessageBox.Show("Запись успешно добавленна");
				}
				else
				{
					MessageBox.Show("При добавлении записи возникла ошибка!");
				}
			}
			else// редактирование элемента
			{
				if (WorkWithDataBase.SpecificUpdateValuesWithImageFromTable("public.companies",
						$"company_name='{companyNameBox.Text.Trim()}', type_id={pTypeID}, license_number={licenseNumber}, license_expiration_date='{endOfLicensePicker.Value.Year}-{endOfLicensePicker.Value.Month}-{endOfLicensePicker.Value.Day}', city_id={cityID}, address='{addressBox.Text.Trim()}', phone_number='{phoneNumberBox.Text}', opening_year={Convert.ToInt32(openingYearBox.Text)}, license_photo=", $"company_id={DFAOE.ID}", _connectionString, img))
				{
					MessageBox.Show("Запись успешно изменена");
				}
				else
				{
					MessageBox.Show("При изменении записи возникла ошибка!");
				}
			}
		}


		private void addElementTab_Changed(object sender, EventArgs e)
		{
			if (addOrEditElementTab.SelectedIndex == 1 || addOrEditElementTab.SelectedIndex == 5 || addOrEditElementTab.SelectedIndex == 7 || addOrEditElementTab.SelectedIndex == 8)
			{
				this.Size = new Size(500, 300);
			}
			else
			{
				this.Size = new Size(616, 405);
			}
		}

		private async Task LoadTableForAdd(int index)
		{
			switch (index)
			{
				case 0: //Компании
					{
						typeBox.Items.AddRange(await DFAOE.SelectPropertyTypesNamesAndIDsAsync(_connectionString));
						citiesBox.Items.AddRange(await DFAOE.SelectCitiesNamesAndIDsAsync(_connectionString));
						endOfLicensePicker.MinDate = DateTime.Now.Date;
					}
					break;
				case 1: //Типы собственности
					{

					}
					break;
				case 2: //Филиалы
					{// 
						ownedByCompanyBox.Items.AddRange(await DFAOE.SelectCompnaiesNamesAndIDsAsync(_connectionString));
						branchCityBox.Items.AddRange(await DFAOE.SelectCitiesNamesAndIDsAsync(_connectionString));
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

						contractDateTime.MaxDate = DateTime.Now;
						contractDateTime.MinDate = DateTime.Now;

						if (HAOE == HowAddOrEdit.CompoundFormAdd)
						{
							for (int i = 0; i < addOrEditElementTab.TabCount; i++)
							{
								if (i != index)
								{
									addOrEditElementTab.TabPages[i].Enabled = false;
								}
							}

							string fullnameOfClient = WorkWithDataBase.SelectLineFromTable("concat (surname, ' ', firstname, ' ', lastname)", "all_clients_view", _connectionString, where: $"WHERE client_id = {DFAOE.ID}");
							if (fullnameOfClient != null)
							{
								contractClientComboBox.SelectedIndex = contractClientComboBox.FindStringExact(fullnameOfClient); // dropbox
								contractClientComboBox.Enabled = false;
							}
						}
						else if (HAOE == HowAddOrEdit.CompoundFormEdit)
						{
							for (int i = 0; i < addOrEditElementTab.TabCount; i++)
							{
								if (i != index)
								{
									addOrEditElementTab.TabPages[i].Enabled = false;
								}
							}
							
							string fullnameOfClient = WorkWithDataBase.SelectLineFromTable("concat (surname, ' ', firstname, ' ', lastname)", 
								"clients", _connectionString, where: $"WHERE (SELECT client_id FROM contracts WHERE contract_id = {DFAOE.ID}) = clients.client_id");
							if (fullnameOfClient != null)
							{
								contractClientComboBox.SelectedIndex = contractClientComboBox.FindStringExact(fullnameOfClient); // dropbox
								contractClientComboBox.Enabled = false;
							}
						}
					}
					break;
				case 5: //Виды страхования
					{

					}
					break;
				case 6: //Клиенты
					{
						clientDateOfBirthTimePicker.MaxDate = new DateTime(DateTime.Now.Year - 18, DateTime.Now.Month, DateTime.Now.Day);
						clientDateOfBirthTimePicker.MinDate = new DateTime(DateTime.Now.Year - 120, DateTime.Now.Month, DateTime.Now.Day);

						clientCityBox.Items.AddRange(await DFAOE.SelectCitiesNamesAndIDsAsync(_connectionString));
						clientSocailStatusBox.Items.AddRange(await DFAOE.SelectSocialStatusesNamesAndIDsAsync(_connectionString));
					}
					break;
				case 7: //Соц. положения
					{

					}
					break;
				case 8: //Города 
					{

					}
					break;
				case 9: //Запросы
					{

					}
					break;

				default:
					break;
			}
		}

		private async void LoadTableForEdit(int index)
		{
			switch (index)
			{
				// ЗАЛОЧИТЬ ВСЕ ОСТАЛЬНЫЕ ТАБЫ
				case 0: //Компании
					{
						if (!addOrEditElementTab.TabPages[index].Enabled) return; // если изначально выбран другой элемент для редактирвоания

						for (int i = 1; i < addOrEditElementTab.TabCount; i++)
						{
							addOrEditElementTab.TabPages[i].Enabled = false;
						}

						await LoadTableForAdd(index);

						DataTable dt = WorkWithDataBase.SelectAllFieldsFromTable($"all_companies_view WHERE company_id = {DFAOE.ID}", _connectionString);
						if (dt != null)
						{
							var dataRows = dt.Rows[0].ItemArray.Select(x => x.ToString()).ToArray();

							companyNameBox.Text = dataRows[1];
							typeBox.SelectedIndex = typeBox.FindStringExact(dataRows[2]);
							licenseNumberBox.Text = dataRows[3];
							endOfLicensePicker.MinDate = DateTime.Parse(dataRows[4]);
							endOfLicensePicker.Value = DateTime.Parse(dataRows[4]);
							citiesBox.SelectedIndex = citiesBox.FindStringExact(dataRows[5]);
							addressBox.Text = dataRows[6];
							phoneNumberBox.Text = dataRows[7];
							openingYearBox.Text = dataRows[8];

							//добаление изображения
							string cmd = $"SELECT license_photo FROM companies WHERE company_id = {DFAOE.ID}";
							licensePhotoPictureBox.Image = WorkWithDataBase.SelectImageFromTable(cmd, _connectionString);
						}
					}
					break;
				case 1: //Типы собственности
					{
						if (!addOrEditElementTab.TabPages[index].Enabled) return; // если изначально выбран другой элемент для редактирвоания

						for (int i = 0; i < addOrEditElementTab.TabCount; i++)
						{
							if (i != index)
							{
								addOrEditElementTab.TabPages[i].Enabled = false;
							}
						}

						DataTable dt = WorkWithDataBase.SelectSpecificFieldsFromTable($"property_types WHERE type_id = {DFAOE.ID}", "property_type", _connectionString);
						if (dt != null)
						{
							var dataRows = dt.Rows[0].ItemArray.Select(x => x.ToString()).ToArray();
							pTypeBox.Text = dataRows[0];
						}
					}
					break;
				case 2: //Филиалы
					{// 
						if (!addOrEditElementTab.TabPages[index].Enabled) return;

						for (int i = 0; i < addOrEditElementTab.TabCount; i++)
						{
							if (i != index)
							{
								addOrEditElementTab.TabPages[i].Enabled = false;
							}
						}

						await LoadTableForAdd(index);

						DataTable dt = WorkWithDataBase.SelectSpecificFieldsFromTable($"all_branches_view WHERE branch_id = {DFAOE.ID}", "company_name, city, address, phone_number, opening_year", _connectionString);
						if (dt != null)
						{
							var dataRows = dt.Rows[0].ItemArray.Select(x => x.ToString()).ToArray();

							ownedByCompanyBox.SelectedIndex = ownedByCompanyBox.FindStringExact(dataRows[0]);
							branchCityBox.SelectedIndex = branchCityBox.FindStringExact(dataRows[1]);
							branchAddressBox.Text = dataRows[2];
							branchPhoneMaskedBox.Text = dataRows[3];
							branchOpeningYearMaskedBox.Text = dataRows[4];
						}
					}
					break;
				case 3: //Сотрудники
					{
						if (!addOrEditElementTab.TabPages[index].Enabled) return;

						for (int i = 0; i < addOrEditElementTab.TabCount; i++)
						{
							if (i != index)
							{
								addOrEditElementTab.TabPages[i].Enabled = false;
							}
						}

						await LoadTableForAdd(index);

						DataTable dt = WorkWithDataBase.SelectAllFieldsFromTable($"all_employees_view WHERE employee_id = {DFAOE.ID}", _connectionString);
						if (dt != null)
						{
							var dataRows = dt.Rows[0].ItemArray.Select(x => x.ToString()).ToArray();

							employeeSurnameBox.Text = dataRows[1];
							employeeFirstnameBox.Text = dataRows[2];
							employeeSecondnameBox.Text = dataRows[3];
							employeeBranchBox.SelectedIndex = employeeBranchBox.FindStringExact(dataRows[4]);
						}
					}
					break;
				case 4: //Договоры
					{
						if (!addOrEditElementTab.TabPages[index].Enabled) return;

						for (int i = 0; i < addOrEditElementTab.TabCount; i++)
						{
							if (i != index)
							{
								addOrEditElementTab.TabPages[i].Enabled = false;
							}
						}

						await LoadTableForAdd(index);

						DataTable dt = WorkWithDataBase.SelectAllFieldsFromTable($"all_contracts_view WHERE contract_id = {DFAOE.ID}", _connectionString);
						if (dt != null)
						{
							var dataRows = dt.Rows[0].ItemArray.Select(x => x.ToString()).ToArray();

							contractInsuranceTypeComboBox.SelectedIndex = contractInsuranceTypeComboBox.FindStringExact(dataRows[1]);// dropbox
							contractEmployeeComboBox.SelectedIndex = contractEmployeeComboBox.FindStringExact(dataRows[2]);// dropbox
							if (HAOE != HowAddOrEdit.CompoundFormEdit)
							{
								contractClientComboBox.SelectedIndex = contractClientComboBox.FindStringExact(dataRows[3]); // dropbox
							}
							contractTextBox.Text = dataRows[4];
							contractSumBox.Text = dataRows[5];

							contractDateTime.MaxDate = DateTime.Now;//DateTime.Parse(dataRows[6]);
							contractDateTime.MinDate = DateTime.Parse(dataRows[6]); // 
							contractDateTime.Value = DateTime.Parse(dataRows[6]);// date
						}

					}
					break;
				case 5: //Виды страхования
					{
						if (!addOrEditElementTab.TabPages[index].Enabled) return;

						for (int i = 0; i < addOrEditElementTab.TabCount; i++)
						{
							if (i != index)
							{
								addOrEditElementTab.TabPages[i].Enabled = false;
							}
						}

						DataTable dt = WorkWithDataBase.SelectSpecificFieldsFromTable($"types_of_insurance WHERE type_of_insurance_id = {DFAOE.ID}", "type_of_insurance", _connectionString);
						if (dt != null)
						{
							var dataRows = dt.Rows[0].ItemArray.Select(x => x.ToString()).ToArray();
							typeOfInsuranceBox.Text = dataRows[0];
						}
					}
					break;
				case 6: //Клиенты
					{
						if (!addOrEditElementTab.TabPages[index].Enabled) return;

						for (int i = 0; i < addOrEditElementTab.TabCount; i++)
						{
							if (i != index)
							{
								addOrEditElementTab.TabPages[i].Enabled = false;
							}
						}

						await LoadTableForAdd(index);

						DataTable dt = WorkWithDataBase.SelectAllFieldsFromTable($"all_clients_view WHERE client_id = {DFAOE.ID}", _connectionString);
						if (dt != null)
						{
							var dataRows = dt.Rows[0].ItemArray.Select(x => x.ToString()).ToArray();

							clientSurnameBox.Text = dataRows[1];
							clientFirstnameBox.Text = dataRows[2];
							clientSecondnameBox.Text = dataRows[3];
							clientDateOfBirthTimePicker.Value = DateTime.Parse(dataRows[4]); // date
							clientSocailStatusBox.SelectedIndex = clientSocailStatusBox.FindStringExact(dataRows[5]); // dropbox
							clientCityBox.SelectedIndex = clientCityBox.FindStringExact(dataRows[6]); // dropbox
							clientAddressBox.Text = dataRows[7];
							clientPhoneMaskedBox.Text = dataRows[8];
						}
					}
					break;
				case 7: //Соц. положения
					{
						if (!addOrEditElementTab.TabPages[index].Enabled) return;

						for (int i = 0; i < addOrEditElementTab.TabCount; i++)
						{
							if (i != index)
							{
								addOrEditElementTab.TabPages[i].Enabled = false;
							}
						}

						DataTable dt = WorkWithDataBase.SelectSpecificFieldsFromTable($"social_status_of_clients WHERE social_status_id = {DFAOE.ID}", "social_status", _connectionString);
						if (dt != null)
						{
							var dataRows = dt.Rows[0].ItemArray.Select(x => x.ToString()).ToArray();
							socailStatusBox.Text = dataRows[0];
						}
					}
					break;
				case 8: //Города 
					{
						if (!addOrEditElementTab.TabPages[index].Enabled) return;

						for (int i = 0; i < addOrEditElementTab.TabCount; i++)
						{
							if (i != index)
							{
								addOrEditElementTab.TabPages[i].Enabled = false;
							}
						}

						DataTable dt = WorkWithDataBase.SelectSpecificFieldsFromTable($"cities WHERE city_id = {DFAOE.ID}", "city", _connectionString);
						if (dt != null)
						{
							var dataRows = dt.Rows[0].ItemArray.Select(x => x.ToString()).ToArray();
							cityBox.Text = dataRows[0];
						}
					}
					break;

				default:
					break;
			}
		}

		private void openingYearBox_MouseClick(object sender, MouseEventArgs e)
		{
			openingYearBox.BackColor = Color.White;
		}

		private void licenseNumberBox_MouseClick(object sender, MouseEventArgs e)
		{
			licenseNumberBox.BackColor = Color.White;
		}

		private void addressBox_MouseClick(object sender, MouseEventArgs e)
		{
			addressBox.BackColor = Color.White;
		}

		private void phoneNumberBox_MouseClick(object sender, MouseEventArgs e)
		{
			phoneNumberBox.BackColor = Color.White;
		}

		private void companyNameBox_MouseClick(object sender, MouseEventArgs e)
		{
			companyNameBox.BackColor = Color.White;
		}

		private void loadPhotoButton_Click(object sender, EventArgs e)
		{
			var dialog = loadPhotoDialog.ShowDialog();
			if (dialog != DialogResult.OK && dialog != DialogResult.Yes) return;

			try
			{
				licensePhotoPictureBox.Image = new Bitmap(loadPhotoDialog.FileName, true);
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Формат файла не поддерживается");
			}
		}

		private void addPTypeButton_Click(object sender, EventArgs e)
		{
			if (String.IsNullOrEmpty(pTypeBox.Text.Trim(' ', '_')))
			{
				pTypeBox.BackColor = Color.Red;
				return;
			}

			if (HAOE == HowAddOrEdit.DefaultAdd || HAOE == HowAddOrEdit.CompoundFormAdd)
			{
				// добавление в представление
				if (WorkWithDataBase.InsertValuesIntoTable("all_property_types_view",
						$"(SELECT type_id FROM property_types ORDER BY type_id DESC LIMIT 1) + 1, '{pTypeBox.Text.Trim(' ', '_')}'", _connectionString))
				{
					MessageBox.Show("Запись успешно добавленна");
				}
				else
				{
					MessageBox.Show("При добавлении записи возникла ошибка!");
				}
			}
			else
			{
				if (WorkWithDataBase.UpdateValuesFromTable("property_types",
						$"property_type='{pTypeBox.Text.Trim(' ', '_')}'", $"type_id = {DFAOE.ID}", _connectionString))
				{
					MessageBox.Show("Запись успешно изменена");
				}
				else
				{
					MessageBox.Show("При изменении записи возникла ошибка!");
				}
			}
		}

		private void pTypeBox_MouseClick(object sender, MouseEventArgs e)
		{
			pTypeBox.BackColor = Color.White;
		}

		private void applyTopeOfInsuranceButton_Click(object sender, EventArgs e)
		{
			if (String.IsNullOrEmpty(typeOfInsuranceBox.Text.Trim()))
			{
				typeOfInsuranceBox.BackColor = Color.Red;
				return;
			}

			if (HAOE == HowAddOrEdit.DefaultAdd || HAOE == HowAddOrEdit.CompoundFormAdd)
			{
				if (WorkWithDataBase.InsertValueIntoTable("types_of_insurance", "type_of_insurance", typeOfInsuranceBox.Text.Trim(), _connectionString))
				{
					MessageBox.Show("Запись успешно добавленна");
				}
				else
				{
					MessageBox.Show("При добавлении записи возникла ошибка!");
				}
			}
			else
			{
				if (WorkWithDataBase.UpdateValuesFromTable("types_of_insurance",
						$"type_of_insurance='{typeOfInsuranceBox.Text.Trim()}'", $"type_of_insurance_id = {DFAOE.ID}", _connectionString))
				{
					MessageBox.Show("Запись успешно изменена");
				}
				else
				{
					MessageBox.Show("При изменении записи возникла ошибка!");
				}
			}
		}

		private void typeOfInsuranceBox_MouseClick(object sender, MouseEventArgs e)
		{
			typeOfInsuranceBox.BackColor = Color.White;
		}

		private void socailStatusBox_MouseClick(object sender, MouseEventArgs e)
		{
			socailStatusBox.BackColor = Color.White;
		}

		private void applySocailStatucButton_Click(object sender, EventArgs e)
		{
			if (String.IsNullOrEmpty(socailStatusBox.Text.Trim()))
			{
				socailStatusBox.BackColor = Color.Red;
				return;
			}

			if (HAOE == HowAddOrEdit.DefaultAdd || HAOE == HowAddOrEdit.CompoundFormAdd)
			{
				if (WorkWithDataBase.InsertValueIntoTable("social_status_of_clients", "social_status", socailStatusBox.Text.Trim(), _connectionString))
				{
					MessageBox.Show("Запись успешно добавленна");
				}
				else
				{
					MessageBox.Show("При добавлении записи возникла ошибка!");
				}
			}
			else
			{
				if (WorkWithDataBase.UpdateValuesFromTable("social_status_of_clients",
						$"social_status='{socailStatusBox.Text.Trim()}'", $"social_status_id = {DFAOE.ID}", _connectionString))
				{
					MessageBox.Show("Запись успешно изменена");
				}
				else
				{
					MessageBox.Show("При изменении записи возникла ошибка!");
				}
			}
		}

		private void cityBox_MouseClick(object sender, MouseEventArgs e)
		{
			cityBox.BackColor = Color.White;
		}

		private void applyCityButton_Click(object sender, EventArgs e)
		{
			if (String.IsNullOrEmpty(cityBox.Text.Trim()))
			{
				cityBox.BackColor = Color.Red;
				return;
			}

			if (HAOE == HowAddOrEdit.DefaultAdd || HAOE == HowAddOrEdit.CompoundFormAdd)
			{
				if (WorkWithDataBase.InsertValueIntoTable("cities", "city", socailStatusBox.Text.Trim(), _connectionString))
				{
					MessageBox.Show("Запись успешно добавленна");
				}
				else
				{
					MessageBox.Show("При добавлении записи возникла ошибка!");
				}
			}
			else
			{
				if (WorkWithDataBase.UpdateValuesFromTable("cities",
						$"city='{cityBox.Text.Trim()}'", $"city_id = {DFAOE.ID}", _connectionString))
				{
					MessageBox.Show("Запись успешно изменена");
				}
				else
				{
					MessageBox.Show("При изменении записи возникла ошибка!");
				}
			}
		}

		private void applyBranchButton_Click(object sender, EventArgs e)
		{
			bool isGoodData = true; // ввел ли пользователь подходящие данные

			if (ownedByCompanyBox.SelectedItem == null)
			{
				errorOwnedByCompanyLabel.Visible = true;
				isGoodData = false;
			}
			if (branchCityBox.SelectedItem == null)
			{
				errorBranchCityLabel.Visible = true;
				isGoodData = false;
			}
			if (String.IsNullOrEmpty(branchAddressBox.Text.Trim()))
			{
				branchAddressBox.BackColor = Color.Red;
				isGoodData = false;
			}
			if (!branchPhoneMaskedBox.MaskFull) //---
			{
				branchPhoneMaskedBox.BackColor = Color.Red;
				isGoodData = false;
			}
			if (!branchOpeningYearMaskedBox.MaskFull || Convert.ToInt32(branchOpeningYearMaskedBox.Text) < 1850 || Convert.ToInt32(branchOpeningYearMaskedBox.Text) > DateTime.Now.Year)
			{
				branchOpeningYearMaskedBox.BackColor = Color.Red;
				isGoodData = false;
			}

			if (!isGoodData) return;

			// год открытия филиала < года открытия компании
			//нахожу дату открытия компании - она буде минимальной для филиала
			string openYearForMinDate = WorkWithDataBase.SelectLineFromTable("companies.opening_year", "companies", _connectionString, where: "INNER JOIN branches ON companies.company_id=branches.company_id " +
				$"WHERE branch_id = {DFAOE.ID} " +
					"LIMIT 1 ");

			if (!Int32.TryParse(openYearForMinDate, out int YearForMinDate)) return;
			if (Convert.ToInt32(branchOpeningYearMaskedBox.Text) < YearForMinDate)
            {
				MessageBox.Show($"Компания открылась в {YearForMinDate} году. Филиал не может быть открыть раньше!", "Error!");
				return;
            }

			DFAOE.CitiesNamesAndIDs.TryGetValue(branchCityBox.Text, out int cityID);
			DFAOE.CompanyNamesAndIDs.TryGetValue(ownedByCompanyBox.Text, out int companyID);

			if (HAOE == HowAddOrEdit.DefaultAdd || HAOE == HowAddOrEdit.CompoundFormAdd)
			{
				if (WorkWithDataBase.InsertValuesIntoTable("public.branches(branch_name, company_id, city_id, is_main, address, phone_number, opening_year, number_of_employees)",
							$"'{ownedByCompanyBox.Text}', {companyID}, {cityID}, 'false', '{branchAddressBox.Text.Trim()}', '{branchPhoneMaskedBox.Text}', {branchOpeningYearMaskedBox.Text}, DEFAULT", _connectionString))
				{
					MessageBox.Show("Запись успешно добавленна");
				}
				else
				{
					MessageBox.Show("При добавлении записи возникла ошибка!");
				}
			}
			else // редактирование элемента
			{
				if (WorkWithDataBase.UpdateValuesFromTable("branches",
						$"branch_name='{ownedByCompanyBox.Text}', company_id={companyID}, city_id={cityID}, is_main='false', address='{branchAddressBox.Text.Trim()}', phone_number='{branchPhoneMaskedBox.Text}', opening_year={branchOpeningYearMaskedBox.Text}, number_of_employees=DEFAULT",
						$"branch_id = {DFAOE.ID}", _connectionString))
				{
					MessageBox.Show("Запись успешно изменена");
				}
				else
				{
					MessageBox.Show("При изменении записи возникла ошибка!");
				}
			}
		}

		private void typeBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			errorCompanyPropertyTypeLabel.Visible = false;
		}

		private void citiesBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			errorCompanyCityLabel.Visible = false;
		}

		private void ownedByCompanyBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			errorOwnedByCompanyLabel.Visible = false;
		}

		private void branchCityBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			errorBranchCityLabel.Visible = false;
		}

		private void applyEmployeeButton_Click(object sender, EventArgs e)
		{
			bool isGoodData = true; // ввел ли пользователь подходящие данные

			if (employeeBranchBox.SelectedItem == null)
			{
				errorEmployeBranchLabel.Visible = true;
				isGoodData = false;
			}
			if (String.IsNullOrEmpty(employeeSurnameBox.Text.Trim()))
			{
				employeeSurnameBox.BackColor = Color.Red;
				isGoodData = false;
			}
			if (String.IsNullOrEmpty(employeeFirstnameBox.Text.Trim()))
			{
				employeeFirstnameBox.BackColor = Color.Red;
				isGoodData = false;
			}
			if (String.IsNullOrEmpty(employeeSecondnameBox.Text.Trim()))
			{
				employeeSecondnameBox.BackColor = Color.Red;
				isGoodData = false;
			}

			if (!isGoodData) return;

			DFAOE.BranchesNamesAndIDs.TryGetValue(employeeBranchBox.Text, out int branchID);

			if (HAOE == HowAddOrEdit.DefaultAdd || HAOE == HowAddOrEdit.CompoundFormAdd)
			{
				if (WorkWithDataBase.InsertValuesIntoTable("public.employees(branch_id, surname, firstname, lastname)",
						$"{branchID}, '{employeeSurnameBox.Text.Trim()}', '{employeeFirstnameBox.Text.Trim()}', '{employeeSecondnameBox.Text.Trim()}'", _connectionString))
				{
					MessageBox.Show("Запись успешно добавленна");
				}
				else
				{
					MessageBox.Show("При добавлении записи возникла ошибка!");
				}
			}
			else // редактирование элемента
			{
				if (WorkWithDataBase.UpdateValuesFromTable("employees",
						$"branch_id={branchID}, surname='{employeeSurnameBox.Text.Trim()}', firstname='{employeeFirstnameBox.Text.Trim()}', lastname='{employeeSecondnameBox.Text.Trim()}'", $"employee_id = {DFAOE.ID}", _connectionString))
				{
					MessageBox.Show("Запись успешно изменена");
				}
				else
				{
					MessageBox.Show("При изменении записи возникла ошибка!");
				}
			}
		}

		private void employeeBranchBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			errorEmployeBranchLabel.Visible = false;
		}

		private void employeeSurnameBox_MouseClick(object sender, MouseEventArgs e)
		{
			employeeSurnameBox.BackColor = Color.White;
		}

		private void employeeFirstnameBox_MouseClick(object sender, MouseEventArgs e)
		{
			employeeFirstnameBox.BackColor = Color.White;
		}

		private void employeeSecondnameBox_MouseClick(object sender, MouseEventArgs e)
		{
			employeeSecondnameBox.BackColor = Color.White;
		}

		private void branchAddressBox_MouseClick(object sender, MouseEventArgs e)
		{
			branchAddressBox.BackColor = Color.White;
		}

		private void branchPhoneMaskedBox_MouseClick(object sender, MouseEventArgs e)
		{
			branchPhoneMaskedBox.BackColor = Color.White;
		}

		private void branchOpeningYearMaskedBox_MouseClick(object sender, MouseEventArgs e)
		{
			branchOpeningYearMaskedBox.BackColor = Color.White;
		}

		private void employeeBranchBox_MouseHover(object sender, EventArgs e)
		{
			if (employeeBranchBox.SelectedItem != null)
			{
				comboBoxesToolTip.ToolTipTitle = employeeBranchBox.SelectedItem.ToString();
			}
			else
			{
				comboBoxesToolTip.ToolTipTitle = "";
			}
		}

		private void applyClientButton_Click(object sender, EventArgs e)
		{
			bool isGoodData = true; // ввел ли пользователь подходящие данные

			if (String.IsNullOrEmpty(clientSurnameBox.Text.Trim()))
			{
				clientSurnameBox.BackColor = Color.Red;
				isGoodData = false;
			}
			if (String.IsNullOrEmpty(clientFirstnameBox.Text.Trim()))
			{
				clientFirstnameBox.BackColor = Color.Red;
				isGoodData = false;
			}
			if (String.IsNullOrEmpty(clientSecondnameBox.Text.Trim()))
			{
				clientSecondnameBox.BackColor = Color.Red;
				isGoodData = false;
			}
			if ((DateTime.Now.Year - clientDateOfBirthTimePicker.Value.Year) < 18 || (DateTime.Now.Year - clientDateOfBirthTimePicker.Value.Year) > 120)
			{
				errorClientDateOfBirthLabel.Visible = true;
				isGoodData = false;
			}
			if (clientCityBox.SelectedItem == null)
			{
				errorClientCityLabel.Visible = true;
				isGoodData = false;
			}
			if (clientSocailStatusBox.SelectedItem == null)
			{
				errorClientSocStatucLabel.Visible = true;
				isGoodData = false;
			}
			if (String.IsNullOrEmpty(clientAddressBox.Text.Trim()))
			{
				clientAddressBox.BackColor = Color.Red;
				isGoodData = false;
			}
			if (!clientPhoneMaskedBox.MaskFull)
			{
				clientPhoneMaskedBox.BackColor = Color.Red;
				isGoodData = false;
			}

			if (!isGoodData) return;

			DFAOE.SocailStatusesNamesAndIDs.TryGetValue(clientSocailStatusBox.Text, out int socailStatusID);
			DFAOE.CitiesNamesAndIDs.TryGetValue(clientCityBox.Text, out int cityID);

			if (HAOE == HowAddOrEdit.DefaultAdd || HAOE == HowAddOrEdit.CompoundFormAdd)
			{
				if (WorkWithDataBase.InsertValuesIntoTable("public.clients(surname, firstname, lastname, date_of_birth, social_status_id, city_id, address, phone_number)",
							$"'{clientSurnameBox.Text.Trim()}', '{clientFirstnameBox.Text.Trim()}', '{clientSecondnameBox.Text.Trim()}', '{clientDateOfBirthTimePicker.Value.Year}-{clientDateOfBirthTimePicker.Value.Month}-{clientDateOfBirthTimePicker.Value.Day}', " +
							$"{socailStatusID}, {cityID}, '{clientAddressBox.Text.Trim()}', {clientPhoneMaskedBox.Text}", _connectionString))
				{
					MessageBox.Show("Запись успешно добавленна");
				}
				else
				{
					MessageBox.Show("При добавлении записи возникла ошибка!");
				}
			}
			else // редактирование элемента
			{
				if (WorkWithDataBase.UpdateValuesFromTable("clients",
						$"surname='{clientSurnameBox.Text.Trim()}', firstname='{clientFirstnameBox.Text.Trim()}', lastname='{clientSecondnameBox.Text.Trim()}', date_of_birth='{clientDateOfBirthTimePicker.Value.Year}-{clientDateOfBirthTimePicker.Value.Month}-{clientDateOfBirthTimePicker.Value.Day}', " +
							$"social_status_id={socailStatusID}, city_id={cityID}, address='{clientAddressBox.Text.Trim()}', phone_number={clientPhoneMaskedBox.Text}", $"client_id = {DFAOE.ID}", _connectionString))
				{
					MessageBox.Show("Запись успешно изменена");
				}
				else
				{
					MessageBox.Show("При изменении записи возникла ошибка!");
				}
			}
		}

		private void clientCityBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			errorClientCityLabel.Visible = false;
		}

		private void clientSocailStatusBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			errorCompanyPropertyTypeLabel.Visible = false;
		}

		private void clientSurnameBox_MouseClick(object sender, MouseEventArgs e)
		{
			clientSurnameBox.BackColor = Color.White;
		}

		private void clientFirstnameBox_MouseClick(object sender, MouseEventArgs e)
		{
			clientFirstnameBox.BackColor = Color.White;
		}

		private void clientSecondnameBox_MouseClick(object sender, MouseEventArgs e)
		{
			clientSecondnameBox.BackColor = Color.White;
		}

		private void clientAddressBox_MouseClick(object sender, MouseEventArgs e)
		{
			clientAddressBox.BackColor = Color.White;
		}

		private void clientPhoneMaskedBox_MouseClick(object sender, MouseEventArgs e)
		{
			clientPhoneMaskedBox.BackColor = Color.White;
		}

		private void applyContractButton_Click(object sender, EventArgs e)
		{
			bool isGoodData = true; // ввел ли пользователь подходящие данные

			if (contractInsuranceTypeComboBox.SelectedItem == null)
			{
				errorContractInsuranceTypeLabel.Visible = true;
				isGoodData = false;
			}
			if (contractClientComboBox.SelectedItem == null)
			{
				errorContractClientLabel.Visible = true;
				isGoodData = false;
			}
			if (contractEmployeeComboBox.SelectedItem == null)
			{
				errorContractEmployeeLabel.Visible = true;
				isGoodData = false;
			}
			if (!Int32.TryParse(contractSumBox.Text.Trim('_'), out int contractSum) || contractSum <= 0)
			{
				contractSumBox.BackColor = Color.Red;
				isGoodData = false;
			}
			if (String.IsNullOrEmpty(contractTextBox.Text.Trim()))
			{
				contractTextBox.BackColor = Color.Red;
				isGoodData = false;
			}

			if (!isGoodData) return;

			DFAOE.TypesOfInsuranceNamesAndIDs.TryGetValue(contractInsuranceTypeComboBox.Text, out int typeOfInsuranceID);
			DFAOE.EmployeesNamesAndIDs.TryGetValue(contractEmployeeComboBox.Text, out int employeeID);
			DFAOE.ClientsNamesAndIDs.TryGetValue(contractClientComboBox.Text, out int clientID);

			if (HAOE == HowAddOrEdit.DefaultAdd || HAOE == HowAddOrEdit.CompoundFormAdd)
			{
				if (WorkWithDataBase.InsertValuesIntoTable("public.contracts(type_of_insurance_id, employee_id, client_id, text_of_contract, sum_of_contract, date_of_onclusion_contract)",
							$"{typeOfInsuranceID}, {employeeID}, {clientID}, '{contractTextBox.Text.Trim()}', {contractSum}, '{contractDateTime.Value.Year}-{contractDateTime.Value.Month}-{contractDateTime.Value.Day}'", _connectionString))
				{
					MessageBox.Show("Запись успешно добавленна");
				}
				else
				{
					MessageBox.Show("При добавлении записи возникла ошибка!");
				}
			}
			else // редактирование элемента
			{
				if (WorkWithDataBase.UpdateValuesFromTable("contracts",
						$"type_of_insurance_id={typeOfInsuranceID}, employee_id={employeeID}, client_id={clientID}, text_of_contract='{contractTextBox.Text.Trim()}', sum_of_contract={contractSum}, date_of_onclusion_contract='{contractDateTime.Value.Year}-{contractDateTime.Value.Month}-{contractDateTime.Value.Day}'", $"contract_id = {DFAOE.ID}", _connectionString))
				{
					MessageBox.Show("Запись успешно изменена");
				}
				else
				{
					MessageBox.Show("При изменении записи возникла ошибка!");
				}
			}
		}

		private void contracInsuranceTypeComboBox_MouseHover(object sender, EventArgs e)
		{
			if (contractInsuranceTypeComboBox.SelectedItem != null)
			{
				comboBoxesToolTip.ToolTipTitle = contractInsuranceTypeComboBox.SelectedItem.ToString();
			}
			else
			{
				comboBoxesToolTip.ToolTipTitle = "";
			}
		}

		private void contractClientComboBox_MouseHover(object sender, EventArgs e)
		{
			if (contractClientComboBox.SelectedItem != null)
			{
				comboBoxesToolTip.ToolTipTitle = contractClientComboBox.SelectedItem.ToString();
			}
			else
			{
				comboBoxesToolTip.ToolTipTitle = "";
			}
		}

		private void contractEmployeeComboBox_MouseHover(object sender, EventArgs e)
		{
			if (contractEmployeeComboBox.SelectedItem != null)
			{
				comboBoxesToolTip.ToolTipTitle = contractEmployeeComboBox.SelectedItem.ToString();
			}
			else
			{
				comboBoxesToolTip.ToolTipTitle = "";
			}
		}

		private void contracInsuranceTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			errorContractInsuranceTypeLabel.Visible = false;
		}

		private void contractClientComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			errorContractClientLabel.Visible = false;
		}

		private void contractEmployeeComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			errorContractEmployeeLabel.Visible = false;
		}

		private void contractSumBox_MouseClick(object sender, MouseEventArgs e)
		{
			contractSumBox.BackColor = Color.White;
		}

		private void contractTextBox_MouseClick(object sender, MouseEventArgs e)
		{
			contractTextBox.BackColor = Color.White;
		}

		private void clientDateOfBirthTimePicker_ValueChanged(object sender, EventArgs e)
		{
			errorClientDateOfBirthLabel.Visible = false;
		}

		private void contractDateTime_ValueChanged(object sender, EventArgs e)
		{
			errorContractDateLabel.Visible = false;
		}

		private void deleteImageButton_Click(object sender, EventArgs e)
		{
			if (licensePhotoPictureBox.Image != null)
			{
				licensePhotoPictureBox.Image = null;
			}
		}

		private void AddOrEditElementForm_Resize(object sender, EventArgs e)
		{
			panel1.Size = new Size(this.Width, panel1.Size.Height);
		}
	}
}

