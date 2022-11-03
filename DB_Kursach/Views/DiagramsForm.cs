using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DB_Kursach
{
	public partial class DiagramsForm : Form
	{
		private readonly string _connectionString = null;
		private readonly int _indexOfDiagramTab = -1;
		public DiagramsForm(string connect, int index)
		{
			InitializeComponent();
			_connectionString = connect;
			_indexOfDiagramTab = index;
		}

		private void DiagramsForm_Load(object sender, EventArgs e)
		{
			if (_indexOfDiagramTab == 0)
			{
				LoadDiagramm(0);
			}
			else
			{
				diagramsTabControl.SelectedIndex = _indexOfDiagramTab;
			}
		}

		private void LoadDiagramm(int index)
		{
			switch(index)
			{
				case 0:
					{
						diagram1DChart.Dock = DockStyle.Fill;
						DataTable dt = WorkWithDataBase.SpecifitSelectFieldsFromTable(Queries.finalQueryWithConditionOnGroups, _connectionString);
						if (dt != null)
						{
							if (dt.Columns.Count == Queries.queryColumnsNames[11].Count())
							{
								var diagramNames = dt.Rows.Cast<DataRow>().Select(x => x.ItemArray[1]).ToArray();
								var diagramTriangles = dt.Rows.Cast<DataRow>().Select(x => x.ItemArray[2]).ToArray();

								diagram1DChart.Series[0].Points.DataBindXY(diagramNames, diagramTriangles);

								for (int i = 0; i < diagramTriangles.Count(); i++)
								{
									diagram1DChart.Series[0].Points[i].Label = (diagramTriangles[i]).ToString();
									diagram1DChart.Series[0].Points[i].LegendText = (diagramNames[i]).ToString();
								}
							}
						}
						headerTextLabel.Text = "Компании и общее число сотрудников в их филиалах";
					}
					break;
				case 1:
					{
						diagram2DChart.Dock = DockStyle.Fill;
						DataTable dt = WorkWithDataBase.SpecifitSelectFieldsFromTable(Queries.totalIncluding, _connectionString);
						if (dt != null)
						{
							if (dt.Columns.Count == Queries.queryColumnsNames[6].Count())
							{
								var values = dt.Rows[0].ItemArray.Select(x => Convert.ToInt32(x)).ToArray();

								diagram2DChart.Series[0].Points.DataBindXY(Queries.queryColumnsNames[6], values);

								for (int i = 0; i < values.Count(); i++)
								{
									diagram2DChart.Series[0].Points[i].Label = (values[i]).ToString();
									diagram2DChart.Series[0].Points[i].LegendText = (Queries.queryColumnsNames[6]).ToString();
								}
							}
						}
						diagram2DChart.Series[0].LegendText = "Количество клиентов";
						headerTextLabel.Text = "Всего клиентов, в том числе по возрастам";
					}
					break;
				case 2:
					{
						diagram3DChart.Dock = DockStyle.Fill; //растянуть на всё окно
															  //diagram3DChart.Size = new Size(diagramsTabControl.Width, diagramsTabControl.Height);
						try
						{
							diagram3DChart.Legends["Legend1"].Enabled = false;
						}
						catch (Exception ex) {}
						diagram3DChart.Legends.Clear(); //очистить легенты
						diagram3DChart.ChartAreas[0].Area3DStyle.Enable3D = true; //включить вид 3D
						diagram3DChart.ChartAreas[0].Area3DStyle.Inclination = 20; //угол поворота гор.осей, по умолчанию

						diagram3DChart.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
						diagram3DChart.ChartAreas[0].AxisX.IsLabelAutoFit = false;

						diagram3DChart.ChartAreas[0].AxisX.IsLabelAutoFit = true;

						string cmdCompanyNames = "SELECT DISTINCT companies.company_id, companies.company_name FROM companies " +
							"RIGHT JOIN branches ON companies.company_id = branches.company_id " +
							"RIGHT JOIN employees ON branches.branch_id = employees.branch_id " +
							"RIGHT JOIN contracts ON employees.employee_id = contracts.employee_id " +
							"GROUP BY  company_name,companies.company_id " +
							"ORDER BY company_name ";

						string cmdMonth = "SELECT DISTINCT  CAST(date_part('month', contracts.date_of_onclusion_contract) AS integer) as cntr " +
							"FROM companies " +
							"INNER JOIN branches ON companies.company_id = branches.company_id " +
							"INNER JOIN employees ON branches.branch_id = employees.branch_id " +
							"INNER JOIN contracts ON employees.employee_id = contracts.employee_id " +
							"GROUP BY cntr ";

						string cmdCNamesMothhAndCounts = "SELECT DISTINCT company_name, CAST(date_part('month', contracts.date_of_onclusion_contract) AS integer) as cntr, COUNT(CAST(date_part('month', contracts.date_of_onclusion_contract) AS integer)) AS cntOfContracts " +
							"FROM companies " +
							"INNER JOIN branches ON companies.company_id = branches.company_id " +
							"INNER JOIN employees ON branches.branch_id = employees.branch_id " +
							"INNER JOIN contracts ON employees.employee_id = contracts.employee_id " +
							"GROUP BY  company_name, cntr " +
							"ORDER BY company_name, cntr ";

						DataTable dt = WorkWithDataBase.SpecifitSelectFieldsFromTable(cmdCompanyNames, _connectionString);
						var companyNames = dt.Rows.Cast<DataRow>().Select(x => (x.ItemArray[1]).ToString()).ToArray();

						dt = WorkWithDataBase.SpecifitSelectFieldsFromTable(cmdMonth, _connectionString);
						var month = dt.Rows.Cast<DataRow>().Select(x => Convert.ToInt32(x.ItemArray[0])).ToArray();

						dt = WorkWithDataBase.SpecifitSelectFieldsFromTable(cmdCNamesMothhAndCounts, _connectionString);
						var countsFromMonthAndCounts = dt.Rows.Cast<DataRow>().Select(x => Convert.ToInt32(x.ItemArray[2])).ToArray();

						diagram3DChart.Series.RemoveAt(0); //Удалить ряд, созданный по умолчанию
						for (int i = 0, c = 0; i < companyNames.Count(); i++)
						{
							diagram3DChart.Legends.Add(new Legend(companyNames[i]));
							
							Series Series1 = new Series();
							Series1.ChartType = SeriesChartType.Column;
							Series1.LegendText = companyNames[i];
							diagram3DChart.Legends[i].Font = new Font("Microsoft Sans Serif", 7);
							diagram3DChart.Series.Add(Series1);

							for (int j = 0; j < month.Count(); j++, c++)
							{
								diagram3DChart.Series[i].Points.AddXY(month[j], countsFromMonthAndCounts[c]);
							}
						}
						headerTextLabel.Text = "Количество договоров у компаний по месяцам";
					}
					break;
			}
		}

		private void diagramsTabControl_SelectedIndexChanged(object sender, EventArgs e)
		{
			LoadDiagramm(diagramsTabControl.SelectedIndex);
		}

		private void DiagramsForm_Resize(object sender, EventArgs e)
		{
			panel1.Size = new Size(this.Width, panel1.Size.Height);
		}
	}
}
