using System.Drawing;
using System.Windows.Forms;

namespace B2CSeller_MappingData
{
    partial class Form2
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.MainView = new System.Windows.Forms.DataGridView();
            this.UpdateButton = new System.Windows.Forms.Button();
            this.SearchText = new System.Windows.Forms.TextBox();
            this.HeaderView = new System.Windows.Forms.DataGridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.MainView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeaderView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(24, 108);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "Ara:";
            // 
            // MainView
            // 
            this.MainView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MainView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainView.Location = new System.Drawing.Point(0, 0);
            this.MainView.Name = "MainView";
            this.MainView.RowHeadersWidth = 51;
            this.MainView.RowTemplate.Height = 24;
            this.MainView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.MainView.Size = new System.Drawing.Size(1924, 443);
            this.MainView.TabIndex = 0;
            // 
            // UpdateButton
            // 
            this.UpdateButton.Location = new System.Drawing.Point(395, 106);
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.Size = new System.Drawing.Size(100, 26);
            this.UpdateButton.TabIndex = 4;
            this.UpdateButton.Text = "Güncelle";
            this.UpdateButton.UseVisualStyleBackColor = true;
            this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // SearchText
            // 
            this.SearchText.Location = new System.Drawing.Point(72, 108);
            this.SearchText.Name = "SearchText";
            this.SearchText.Size = new System.Drawing.Size(300, 22);
            this.SearchText.TabIndex = 5;
            this.SearchText.TextChanged += new System.EventHandler(this.SearchText_TextChanged);
            // 
            // HeaderView
            // 
            this.HeaderView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.HeaderView.Location = new System.Drawing.Point(0, 0);
            this.HeaderView.Name = "HeaderView";
            this.HeaderView.RowHeadersWidth = 51;
            this.HeaderView.RowTemplate.Height = 24;
            this.HeaderView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.HeaderView.Size = new System.Drawing.Size(1924, 90);
            this.HeaderView.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.UpdateButton);
            this.splitContainer1.Panel1.Controls.Add(this.HeaderView);
            this.splitContainer1.Panel1.Controls.Add(this.SearchText);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.MainView);
            this.splitContainer1.Size = new System.Drawing.Size(1924, 588);
            this.splitContainer1.SplitterDistance = 141;
            this.splitContainer1.TabIndex = 6;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 588);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.MainView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeaderView)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        public void ApplyDefaultDataGridViewStyle(DataGridView gridView)
        {
            gridView.AllowUserToAddRows = false;
            gridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            gridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridView.RowHeadersVisible = false;
            gridView.RowTemplate.Height = 30;
            gridView.RowsDefaultCellStyle.BackColor = Color.LightGray;
            gridView.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
            gridView.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkSlateGray;
            gridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            gridView.EnableHeadersVisualStyles = false;
            gridView.GridColor = Color.White;
            gridView.CellBorderStyle = DataGridViewCellBorderStyle.None;
            gridView.DefaultCellStyle.SelectionBackColor = Color.DarkCyan;
            gridView.DefaultCellStyle.SelectionForeColor = Color.White;
            gridView.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Italic);
            gridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
            gridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridView.MultiSelect = false;
            gridView.ReadOnly = true;
        }



        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView MainView;
        private System.Windows.Forms.Button UpdateButton;
        private System.Windows.Forms.TextBox SearchText;
        private DataGridView HeaderView;
        private SplitContainer splitContainer1;
    }
}
