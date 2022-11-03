
namespace DB_Kursach
{
    partial class HelpForm
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
            this.headerTextLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.helpRichTextBox = new System.Windows.Forms.RichTextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerTextLabel
            // 
            this.headerTextLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.headerTextLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.headerTextLabel.Location = new System.Drawing.Point(16, 0);
            this.headerTextLabel.Name = "headerTextLabel";
            this.headerTextLabel.Size = new System.Drawing.Size(796, 28);
            this.headerTextLabel.TabIndex = 0;
            this.headerTextLabel.Text = "Помощь";
            this.headerTextLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Lavender;
            this.panel1.Controls.Add(this.headerTextLabel);
            this.panel1.Location = new System.Drawing.Point(-4, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(824, 28);
            this.panel1.TabIndex = 45;
            // 
            // helpRichTextBox
            // 
            this.helpRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.helpRichTextBox.BackColor = System.Drawing.Color.GhostWhite;
            this.helpRichTextBox.Location = new System.Drawing.Point(2, 31);
            this.helpRichTextBox.Name = "helpRichTextBox";
            this.helpRichTextBox.Size = new System.Drawing.Size(796, 415);
            this.helpRichTextBox.TabIndex = 46;
            this.helpRichTextBox.Text = "";
            // 
            // HelpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.GhostWhite;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.helpRichTextBox);
            this.Controls.Add(this.panel1);
            this.Name = "HelpForm";
            this.Text = "Помощь";
            this.Load += new System.EventHandler(this.HelpForm_Load);
            this.Resize += new System.EventHandler(this.HelpForm_Resize);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label headerTextLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox helpRichTextBox;
    }
}