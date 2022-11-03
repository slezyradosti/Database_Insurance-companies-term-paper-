using Npgsql;
using System;
using System.Text;
using System.Windows.Forms;
namespace DB_Kursach
{
    public partial class LogInForm : Form
    {
        public LogInForm()
        {
            InitializeComponent();
        }

        private void logInButton_Click(object sender, EventArgs e)
        {
			string str = "Server=127.0.0.1;Port=5432;User Id=postgres;Password=1111;Database=InsuranceCompanies;MinPoolSize=10";

			if (String.IsNullOrEmpty(logInTextBox.Text.Trim()) || String.IsNullOrEmpty(passwordTextBox.Text.Trim())) return;

			NpgsqlConnection con = new NpgsqlConnection();
			try
            {
				string connectionString = $"Server=127.0.0.1;Port=5432;User Id={logInTextBox.Text.Trim()};Password={passwordTextBox.Text.Trim()};Database=InsuranceCompanies;MinPoolSize=10";

				con = new NpgsqlConnection(str);
				con.Open();
				con.Close();
				MainMenuForm mm = new MainMenuForm(str);
				mm.Show();
				
				this.Hide();
			}
			catch (Exception)
            {
				con.Close();
				MessageBox.Show("Неверный логин или пароль!");
			}
		}
    }
}
