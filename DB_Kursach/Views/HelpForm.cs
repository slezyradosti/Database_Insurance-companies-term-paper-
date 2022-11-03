using Org.BouncyCastle.Utilities;
using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace DB_Kursach
{
    public partial class HelpForm : Form
    {
        public HelpForm()
        {
            InitializeComponent();
        }

        private void HelpForm_Load(object sender, EventArgs e)
        {
            try
            {
                string[] str = File.ReadAllLines("Help.txt", Encoding.UTF8);
                
                foreach (string s in str)
                {
                    helpRichTextBox.Text += s + "\n";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Файл помощи не найден!");
                this.Close();
            }
        }

        private void HelpForm_Resize(object sender, EventArgs e)
        {
            panel1.Size = new Size(this.Width, panel1.Size.Height);
        }
    }
}
