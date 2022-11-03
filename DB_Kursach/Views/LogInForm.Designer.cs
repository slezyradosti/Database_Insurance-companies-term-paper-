
namespace DB_Kursach
{
	partial class LogInForm
	{
		/// <summary>
		/// Обязательная переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Код, автоматически созданный конструктором форм Windows

		/// <summary>
		/// Требуемый метод для поддержки конструктора — не изменяйте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		private void InitializeComponent()
		{
            this.logInButton = new System.Windows.Forms.Button();
            this.logInTextBox = new System.Windows.Forms.TextBox();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ownerNameStatusStrip = new System.Windows.Forms.StatusStrip();
            this.ownerNameToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.ownerNameToolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.ownerNameStatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // logInButton
            // 
            this.logInButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.logInButton.BackColor = System.Drawing.Color.Lavender;
            this.logInButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.logInButton.Location = new System.Drawing.Point(227, 235);
            this.logInButton.Margin = new System.Windows.Forms.Padding(2);
            this.logInButton.Name = "logInButton";
            this.logInButton.Size = new System.Drawing.Size(149, 56);
            this.logInButton.TabIndex = 0;
            this.logInButton.Text = "Войти";
            this.logInButton.UseVisualStyleBackColor = false;
            this.logInButton.Click += new System.EventHandler(this.logInButton_Click);
            // 
            // logInTextBox
            // 
            this.logInTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logInTextBox.Location = new System.Drawing.Point(212, 123);
            this.logInTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.logInTextBox.Name = "logInTextBox";
            this.logInTextBox.Size = new System.Drawing.Size(182, 20);
            this.logInTextBox.TabIndex = 1;
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.passwordTextBox.Location = new System.Drawing.Point(212, 163);
            this.passwordTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.PasswordChar = '*';
            this.passwordTextBox.Size = new System.Drawing.Size(182, 20);
            this.passwordTextBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(156, 39);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(300, 48);
            this.label1.TabIndex = 3;
            this.label1.Text = "СИСТЕМА УЧЕТА ДОГОВОРОВ \r\nСТРАХОВЫХ КОМПАНИЙ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(103, 120);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Логин:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(103, 160);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Пароль:";
            // 
            // ownerNameStatusStrip
            // 
            this.ownerNameStatusStrip.BackColor = System.Drawing.Color.Lavender;
            this.ownerNameStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ownerNameToolStripStatusLabel,
            this.ownerNameToolStripStatusLabel1});
            this.ownerNameStatusStrip.Location = new System.Drawing.Point(0, 344);
            this.ownerNameStatusStrip.Name = "ownerNameStatusStrip";
            this.ownerNameStatusStrip.Size = new System.Drawing.Size(600, 22);
            this.ownerNameStatusStrip.TabIndex = 7;
            this.ownerNameStatusStrip.Text = "statusStrip1";
            // 
            // ownerNameToolStripStatusLabel
            // 
            this.ownerNameToolStripStatusLabel.Name = "ownerNameToolStripStatusLabel";
            this.ownerNameToolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // ownerNameToolStripStatusLabel1
            // 
            this.ownerNameToolStripStatusLabel1.Name = "ownerNameToolStripStatusLabel1";
            this.ownerNameToolStripStatusLabel1.Size = new System.Drawing.Size(185, 17);
            this.ownerNameToolStripStatusLabel1.Text = "prod. by Сергушин Олег ПИ-20а";
            // 
            // LogInForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.GhostWhite;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.ownerNameStatusStrip);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.logInTextBox);
            this.Controls.Add(this.logInButton);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "LogInForm";
            this.Text = "Авторизация";
            this.ownerNameStatusStrip.ResumeLayout(false);
            this.ownerNameStatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button logInButton;
		private System.Windows.Forms.TextBox logInTextBox;
		private System.Windows.Forms.TextBox passwordTextBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
        private System.Windows.Forms.StatusStrip ownerNameStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel ownerNameToolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel ownerNameToolStripStatusLabel1;
    }
}

