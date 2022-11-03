
namespace DB_Kursach
{
    partial class DiagramsForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend6 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.diagramsTabControl = new System.Windows.Forms.TabControl();
            this.diagram1DTabPage = new System.Windows.Forms.TabPage();
            this.diagram1DChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.diagramColumnarTabPage = new System.Windows.Forms.TabPage();
            this.diagram2DChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.diagram3DTabPage = new System.Windows.Forms.TabPage();
            this.headerTextLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.diagram3DChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.diagramsTabControl.SuspendLayout();
            this.diagram1DTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.diagram1DChart)).BeginInit();
            this.diagramColumnarTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.diagram2DChart)).BeginInit();
            this.diagram3DTabPage.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.diagram3DChart)).BeginInit();
            this.SuspendLayout();
            // 
            // diagramsTabControl
            // 
            this.diagramsTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.diagramsTabControl.Controls.Add(this.diagram1DTabPage);
            this.diagramsTabControl.Controls.Add(this.diagramColumnarTabPage);
            this.diagramsTabControl.Controls.Add(this.diagram3DTabPage);
            this.diagramsTabControl.Location = new System.Drawing.Point(3, 0);
            this.diagramsTabControl.Name = "diagramsTabControl";
            this.diagramsTabControl.SelectedIndex = 0;
            this.diagramsTabControl.Size = new System.Drawing.Size(794, 448);
            this.diagramsTabControl.TabIndex = 0;
            this.diagramsTabControl.SelectedIndexChanged += new System.EventHandler(this.diagramsTabControl_SelectedIndexChanged);
            // 
            // diagram1DTabPage
            // 
            this.diagram1DTabPage.BackColor = System.Drawing.Color.GhostWhite;
            this.diagram1DTabPage.Controls.Add(this.diagram1DChart);
            this.diagram1DTabPage.Location = new System.Drawing.Point(4, 22);
            this.diagram1DTabPage.Name = "diagram1DTabPage";
            this.diagram1DTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.diagram1DTabPage.Size = new System.Drawing.Size(786, 422);
            this.diagram1DTabPage.TabIndex = 0;
            this.diagram1DTabPage.Text = "Одномерная диаграмма";
            // 
            // diagram1DChart
            // 
            chartArea4.Name = "ChartArea1";
            this.diagram1DChart.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.diagram1DChart.Legends.Add(legend4);
            this.diagram1DChart.Location = new System.Drawing.Point(0, 12);
            this.diagram1DChart.Name = "diagram1DChart";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pyramid;
            series4.Legend = "Legend1";
            series4.Name = "Series1";
            this.diagram1DChart.Series.Add(series4);
            this.diagram1DChart.Size = new System.Drawing.Size(786, 410);
            this.diagram1DChart.TabIndex = 0;
            this.diagram1DChart.Text = "chart1";
            // 
            // diagramColumnarTabPage
            // 
            this.diagramColumnarTabPage.BackColor = System.Drawing.Color.GhostWhite;
            this.diagramColumnarTabPage.Controls.Add(this.diagram2DChart);
            this.diagramColumnarTabPage.Location = new System.Drawing.Point(4, 22);
            this.diagramColumnarTabPage.Name = "diagramColumnarTabPage";
            this.diagramColumnarTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.diagramColumnarTabPage.Size = new System.Drawing.Size(786, 422);
            this.diagramColumnarTabPage.TabIndex = 1;
            this.diagramColumnarTabPage.Text = "Столбчатая диаграмма";
            // 
            // diagram2DChart
            // 
            chartArea5.Name = "ChartArea1";
            this.diagram2DChart.ChartAreas.Add(chartArea5);
            legend5.Name = "Legend1";
            this.diagram2DChart.Legends.Add(legend5);
            this.diagram2DChart.Location = new System.Drawing.Point(0, 12);
            this.diagram2DChart.Name = "diagram2DChart";
            series5.ChartArea = "ChartArea1";
            series5.Legend = "Legend1";
            series5.Name = "Series1";
            this.diagram2DChart.Series.Add(series5);
            this.diagram2DChart.Size = new System.Drawing.Size(786, 410);
            this.diagram2DChart.TabIndex = 1;
            this.diagram2DChart.Text = "chart1";
            // 
            // diagram3DTabPage
            // 
            this.diagram3DTabPage.BackColor = System.Drawing.Color.GhostWhite;
            this.diagram3DTabPage.Controls.Add(this.diagram3DChart);
            this.diagram3DTabPage.Location = new System.Drawing.Point(4, 22);
            this.diagram3DTabPage.Name = "diagram3DTabPage";
            this.diagram3DTabPage.Size = new System.Drawing.Size(786, 422);
            this.diagram3DTabPage.TabIndex = 2;
            this.diagram3DTabPage.Text = "Трехмерная диаграмма";
            // 
            // headerTextLabel
            // 
            this.headerTextLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.headerTextLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.headerTextLabel.Location = new System.Drawing.Point(6, 0);
            this.headerTextLabel.Name = "headerTextLabel";
            this.headerTextLabel.Size = new System.Drawing.Size(808, 28);
            this.headerTextLabel.TabIndex = 0;
            this.headerTextLabel.Text = "таблицы \"\"";
            this.headerTextLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Lavender;
            this.panel1.Controls.Add(this.headerTextLabel);
            this.panel1.Location = new System.Drawing.Point(-3, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(826, 28);
            this.panel1.TabIndex = 45;
            // 
            // diagram3DChart
            // 
            chartArea6.Name = "ChartArea1";
            this.diagram3DChart.ChartAreas.Add(chartArea6);
            legend6.Name = "Legend1";
            this.diagram3DChart.Legends.Add(legend6);
            this.diagram3DChart.Location = new System.Drawing.Point(0, 9);
            this.diagram3DChart.Name = "diagram3DChart";
            series6.ChartArea = "ChartArea1";
            series6.Legend = "Legend1";
            series6.Name = "Series1";
            this.diagram3DChart.Series.Add(series6);
            this.diagram3DChart.Size = new System.Drawing.Size(786, 410);
            this.diagram3DChart.TabIndex = 46;
            this.diagram3DChart.Text = "chart1";
            // 
            // DiagramsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.GhostWhite;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.diagramsTabControl);
            this.Name = "DiagramsForm";
            this.Text = "Диаграммы";
            this.Load += new System.EventHandler(this.DiagramsForm_Load);
            this.Resize += new System.EventHandler(this.DiagramsForm_Resize);
            this.diagramsTabControl.ResumeLayout(false);
            this.diagram1DTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.diagram1DChart)).EndInit();
            this.diagramColumnarTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.diagram2DChart)).EndInit();
            this.diagram3DTabPage.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.diagram3DChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl diagramsTabControl;
        private System.Windows.Forms.TabPage diagram1DTabPage;
        private System.Windows.Forms.TabPage diagramColumnarTabPage;
        private System.Windows.Forms.TabPage diagram3DTabPage;
        private System.Windows.Forms.DataVisualization.Charting.Chart diagram1DChart;
        private System.Windows.Forms.DataVisualization.Charting.Chart diagram2DChart;
        private System.Windows.Forms.Label headerTextLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataVisualization.Charting.Chart diagram3DChart;
    }
}