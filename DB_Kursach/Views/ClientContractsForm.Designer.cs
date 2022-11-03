
namespace DB_Kursach
{
    partial class ClientContractsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientContractsForm));
            this.cNameLabel = new System.Windows.Forms.Label();
            this.dateOfBirthLabel = new System.Windows.Forms.Label();
            this.socialStatusLabel = new System.Windows.Forms.Label();
            this.cityLabel = new System.Windows.Forms.Label();
            this.addressLabel = new System.Windows.Forms.Label();
            this.phoneLabel = new System.Windows.Forms.Label();
            this.clientContractsGrid = new System.Windows.Forms.DataGridView();
            this.setTimeSpanButton = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.sortBySumutton = new System.Windows.Forms.Button();
            this.sortByTypeButton = new System.Windows.Forms.Button();
            this.sumLabel = new System.Windows.Forms.Label();
            this.countOfClientContractsLabel = new System.Windows.Forms.Label();
            this.resetButton = new System.Windows.Forms.Button();
            this.selectAllContractsButton = new System.Windows.Forms.Button();
            this.editContractButton = new System.Windows.Forms.Button();
            this.deleteContractButton = new System.Windows.Forms.Button();
            this.addContractButton = new System.Windows.Forms.Button();
            this.refreshClientContractsButton = new System.Windows.Forms.Button();
            this.headerTextLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.fullTextToolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.clientContractsGrid)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cNameLabel
            // 
            this.cNameLabel.AutoSize = true;
            this.cNameLabel.Location = new System.Drawing.Point(9, 55);
            this.cNameLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.cNameLabel.Name = "cNameLabel";
            this.cNameLabel.Size = new System.Drawing.Size(37, 13);
            this.cNameLabel.TabIndex = 0;
            this.cNameLabel.Text = "ФИО:";
            // 
            // dateOfBirthLabel
            // 
            this.dateOfBirthLabel.AutoSize = true;
            this.dateOfBirthLabel.Location = new System.Drawing.Point(9, 76);
            this.dateOfBirthLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.dateOfBirthLabel.Name = "dateOfBirthLabel";
            this.dateOfBirthLabel.Size = new System.Drawing.Size(89, 13);
            this.dateOfBirthLabel.TabIndex = 1;
            this.dateOfBirthLabel.Text = "Дата рождения:";
            // 
            // socialStatusLabel
            // 
            this.socialStatusLabel.AutoSize = true;
            this.socialStatusLabel.Location = new System.Drawing.Point(9, 98);
            this.socialStatusLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.socialStatusLabel.Name = "socialStatusLabel";
            this.socialStatusLabel.Size = new System.Drawing.Size(130, 13);
            this.socialStatusLabel.TabIndex = 2;
            this.socialStatusLabel.Text = "Социальное положение:";
            // 
            // cityLabel
            // 
            this.cityLabel.AutoSize = true;
            this.cityLabel.Location = new System.Drawing.Point(9, 119);
            this.cityLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.cityLabel.Name = "cityLabel";
            this.cityLabel.Size = new System.Drawing.Size(40, 13);
            this.cityLabel.TabIndex = 3;
            this.cityLabel.Text = "Город:";
            // 
            // addressLabel
            // 
            this.addressLabel.AutoSize = true;
            this.addressLabel.Location = new System.Drawing.Point(9, 141);
            this.addressLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.addressLabel.Name = "addressLabel";
            this.addressLabel.Size = new System.Drawing.Size(41, 13);
            this.addressLabel.TabIndex = 4;
            this.addressLabel.Text = "Адрес:";
            // 
            // phoneLabel
            // 
            this.phoneLabel.AutoSize = true;
            this.phoneLabel.Location = new System.Drawing.Point(9, 162);
            this.phoneLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.phoneLabel.Name = "phoneLabel";
            this.phoneLabel.Size = new System.Drawing.Size(55, 13);
            this.phoneLabel.TabIndex = 5;
            this.phoneLabel.Text = "Телефон:";
            // 
            // clientContractsGrid
            // 
            this.clientContractsGrid.AllowUserToAddRows = false;
            this.clientContractsGrid.AllowUserToDeleteRows = false;
            this.clientContractsGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clientContractsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.clientContractsGrid.Location = new System.Drawing.Point(3, 187);
            this.clientContractsGrid.Margin = new System.Windows.Forms.Padding(2);
            this.clientContractsGrid.Name = "clientContractsGrid";
            this.clientContractsGrid.ReadOnly = true;
            this.clientContractsGrid.RowHeadersWidth = 51;
            this.clientContractsGrid.RowTemplate.Height = 24;
            this.clientContractsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.clientContractsGrid.Size = new System.Drawing.Size(730, 223);
            this.clientContractsGrid.TabIndex = 6;
            // 
            // setTimeSpanButton
            // 
            this.setTimeSpanButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.setTimeSpanButton.Location = new System.Drawing.Point(9, 439);
            this.setTimeSpanButton.Margin = new System.Windows.Forms.Padding(2);
            this.setTimeSpanButton.Name = "setTimeSpanButton";
            this.setTimeSpanButton.Size = new System.Drawing.Size(95, 41);
            this.setTimeSpanButton.TabIndex = 7;
            this.setTimeSpanButton.Text = "Задать промежуток";
            this.setTimeSpanButton.UseVisualStyleBackColor = true;
            this.setTimeSpanButton.Click += new System.EventHandler(this.setTimeSpanButton_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(120, 439);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(2);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(102, 20);
            this.dateTimePicker1.TabIndex = 8;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker2.Location = new System.Drawing.Point(120, 462);
            this.dateTimePicker2.Margin = new System.Windows.Forms.Padding(2);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(102, 20);
            this.dateTimePicker2.TabIndex = 9;
            this.dateTimePicker2.ValueChanged += new System.EventHandler(this.dateTimePicker2_ValueChanged);
            // 
            // sortBySumutton
            // 
            this.sortBySumutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.sortBySumutton.Location = new System.Drawing.Point(241, 436);
            this.sortBySumutton.Margin = new System.Windows.Forms.Padding(2);
            this.sortBySumutton.Name = "sortBySumutton";
            this.sortBySumutton.Size = new System.Drawing.Size(136, 23);
            this.sortBySumutton.TabIndex = 10;
            this.sortBySumutton.Text = "Сортировать по сумме";
            this.sortBySumutton.UseVisualStyleBackColor = true;
            this.sortBySumutton.Click += new System.EventHandler(this.sortBySumutton_Click);
            // 
            // sortByTypeButton
            // 
            this.sortByTypeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.sortByTypeButton.Location = new System.Drawing.Point(241, 463);
            this.sortByTypeButton.Margin = new System.Windows.Forms.Padding(2);
            this.sortByTypeButton.Name = "sortByTypeButton";
            this.sortByTypeButton.Size = new System.Drawing.Size(137, 23);
            this.sortByTypeButton.TabIndex = 11;
            this.sortByTypeButton.Text = "Сортировать по виду";
            this.sortByTypeButton.UseVisualStyleBackColor = true;
            this.sortByTypeButton.Click += new System.EventHandler(this.sortByTypeButton_Click);
            // 
            // sumLabel
            // 
            this.sumLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sumLabel.AutoSize = true;
            this.sumLabel.Location = new System.Drawing.Point(524, 420);
            this.sumLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.sumLabel.Name = "sumLabel";
            this.sumLabel.Size = new System.Drawing.Size(161, 13);
            this.sumLabel.TabIndex = 12;
            this.sumLabel.Text = "Общая стоимость договоров: ";
            this.sumLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // countOfClientContractsLabel
            // 
            this.countOfClientContractsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.countOfClientContractsLabel.AutoSize = true;
            this.countOfClientContractsLabel.Location = new System.Drawing.Point(12, 420);
            this.countOfClientContractsLabel.Name = "countOfClientContractsLabel";
            this.countOfClientContractsLabel.Size = new System.Drawing.Size(105, 13);
            this.countOfClientContractsLabel.TabIndex = 13;
            this.countOfClientContractsLabel.Text = "Количество полей: ";
            // 
            // resetButton
            // 
            this.resetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.resetButton.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.resetButton.Location = new System.Drawing.Point(652, 440);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(70, 23);
            this.resetButton.TabIndex = 14;
            this.resetButton.Text = "Сбросить";
            this.resetButton.UseVisualStyleBackColor = false;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // selectAllContractsButton
            // 
            this.selectAllContractsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.selectAllContractsButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.selectAllContractsButton.FlatAppearance.BorderSize = 0;
            this.selectAllContractsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.selectAllContractsButton.Image = ((System.Drawing.Image)(resources.GetObject("selectAllContractsButton.Image")));
            this.selectAllContractsButton.Location = new System.Drawing.Point(663, 155);
            this.selectAllContractsButton.Name = "selectAllContractsButton";
            this.selectAllContractsButton.Size = new System.Drawing.Size(27, 27);
            this.selectAllContractsButton.TabIndex = 18;
            this.fullTextToolTip.SetToolTip(this.selectAllContractsButton, "Действие");
            this.selectAllContractsButton.UseVisualStyleBackColor = true;
            this.selectAllContractsButton.Click += new System.EventHandler(this.selectAllContractsButton_Click);
            this.selectAllContractsButton.MouseHover += new System.EventHandler(this.selectAllContractsButton_MouseHover);
            // 
            // editContractButton
            // 
            this.editContractButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.editContractButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.editContractButton.FlatAppearance.BorderSize = 0;
            this.editContractButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.editContractButton.Image = ((System.Drawing.Image)(resources.GetObject("editContractButton.Image")));
            this.editContractButton.Location = new System.Drawing.Point(630, 155);
            this.editContractButton.Name = "editContractButton";
            this.editContractButton.Size = new System.Drawing.Size(27, 27);
            this.editContractButton.TabIndex = 17;
            this.fullTextToolTip.SetToolTip(this.editContractButton, "Действие");
            this.editContractButton.UseVisualStyleBackColor = true;
            this.editContractButton.Click += new System.EventHandler(this.editContractButton_Click);
            this.editContractButton.MouseHover += new System.EventHandler(this.editContractButton_MouseHover);
            // 
            // deleteContractButton
            // 
            this.deleteContractButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteContractButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.deleteContractButton.FlatAppearance.BorderSize = 0;
            this.deleteContractButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deleteContractButton.Image = ((System.Drawing.Image)(resources.GetObject("deleteContractButton.Image")));
            this.deleteContractButton.Location = new System.Drawing.Point(597, 155);
            this.deleteContractButton.Name = "deleteContractButton";
            this.deleteContractButton.Size = new System.Drawing.Size(27, 27);
            this.deleteContractButton.TabIndex = 16;
            this.fullTextToolTip.SetToolTip(this.deleteContractButton, "Действие");
            this.deleteContractButton.UseVisualStyleBackColor = true;
            this.deleteContractButton.Click += new System.EventHandler(this.deleteContractButton_Click);
            this.deleteContractButton.MouseHover += new System.EventHandler(this.deleteContractButton_MouseHover);
            // 
            // addContractButton
            // 
            this.addContractButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addContractButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.addContractButton.FlatAppearance.BorderSize = 0;
            this.addContractButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addContractButton.Image = ((System.Drawing.Image)(resources.GetObject("addContractButton.Image")));
            this.addContractButton.Location = new System.Drawing.Point(564, 155);
            this.addContractButton.Name = "addContractButton";
            this.addContractButton.Size = new System.Drawing.Size(27, 27);
            this.addContractButton.TabIndex = 15;
            this.fullTextToolTip.SetToolTip(this.addContractButton, "Действие");
            this.addContractButton.UseVisualStyleBackColor = true;
            this.addContractButton.Click += new System.EventHandler(this.addContractButton_Click);
            this.addContractButton.MouseHover += new System.EventHandler(this.addContractButton_MouseHover);
            // 
            // refreshClientContractsButton
            // 
            this.refreshClientContractsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.refreshClientContractsButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.refreshClientContractsButton.FlatAppearance.BorderSize = 0;
            this.refreshClientContractsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.refreshClientContractsButton.Image = ((System.Drawing.Image)(resources.GetObject("refreshClientContractsButton.Image")));
            this.refreshClientContractsButton.Location = new System.Drawing.Point(695, 155);
            this.refreshClientContractsButton.Name = "refreshClientContractsButton";
            this.refreshClientContractsButton.Size = new System.Drawing.Size(27, 27);
            this.refreshClientContractsButton.TabIndex = 19;
            this.fullTextToolTip.SetToolTip(this.refreshClientContractsButton, "Действие");
            this.refreshClientContractsButton.UseVisualStyleBackColor = true;
            this.refreshClientContractsButton.Click += new System.EventHandler(this.refreshClientContractsButton_Click);
            this.refreshClientContractsButton.MouseHover += new System.EventHandler(this.refreshClientContractsButton_MouseHover);
            // 
            // headerTextLabel
            // 
            this.headerTextLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.headerTextLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.headerTextLabel.Location = new System.Drawing.Point(16, 0);
            this.headerTextLabel.Name = "headerTextLabel";
            this.headerTextLabel.Size = new System.Drawing.Size(710, 38);
            this.headerTextLabel.TabIndex = 0;
            this.headerTextLabel.Text = "Договоры клиента";
            this.headerTextLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Lavender;
            this.panel1.Controls.Add(this.headerTextLabel);
            this.panel1.Location = new System.Drawing.Point(-4, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(747, 38);
            this.panel1.TabIndex = 44;
            // 
            // ClientContractsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.GhostWhite;
            this.ClientSize = new System.Drawing.Size(734, 502);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.refreshClientContractsButton);
            this.Controls.Add(this.selectAllContractsButton);
            this.Controls.Add(this.editContractButton);
            this.Controls.Add(this.deleteContractButton);
            this.Controls.Add(this.addContractButton);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.countOfClientContractsLabel);
            this.Controls.Add(this.sumLabel);
            this.Controls.Add(this.sortByTypeButton);
            this.Controls.Add(this.sortBySumutton);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.setTimeSpanButton);
            this.Controls.Add(this.clientContractsGrid);
            this.Controls.Add(this.phoneLabel);
            this.Controls.Add(this.addressLabel);
            this.Controls.Add(this.cityLabel);
            this.Controls.Add(this.socialStatusLabel);
            this.Controls.Add(this.dateOfBirthLabel);
            this.Controls.Add(this.cNameLabel);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ClientContractsForm";
            this.Text = "Договоры клиента";
            this.Load += new System.EventHandler(this.ClientContracts_Load);
            this.Resize += new System.EventHandler(this.ClientContractsForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.clientContractsGrid)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label cNameLabel;
        private System.Windows.Forms.Label dateOfBirthLabel;
        private System.Windows.Forms.Label socialStatusLabel;
        private System.Windows.Forms.Label cityLabel;
        private System.Windows.Forms.Label addressLabel;
        private System.Windows.Forms.Label phoneLabel;
        private System.Windows.Forms.DataGridView clientContractsGrid;
        private System.Windows.Forms.Button setTimeSpanButton;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Button sortBySumutton;
        private System.Windows.Forms.Button sortByTypeButton;
        private System.Windows.Forms.Label sumLabel;
        private System.Windows.Forms.Label countOfClientContractsLabel;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Button selectAllContractsButton;
        private System.Windows.Forms.Button editContractButton;
        private System.Windows.Forms.Button deleteContractButton;
        private System.Windows.Forms.Button addContractButton;
        private System.Windows.Forms.Button refreshClientContractsButton;
        private System.Windows.Forms.Label headerTextLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolTip fullTextToolTip;
    }
}