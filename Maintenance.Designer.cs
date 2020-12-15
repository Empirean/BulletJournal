
namespace BulletJournal
{
    partial class Maintenance
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Maintenance));
            this.dataGrid_content = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_content)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGrid_content
            // 
            this.dataGrid_content.AllowUserToAddRows = false;
            this.dataGrid_content.AllowUserToDeleteRows = false;
            this.dataGrid_content.AllowUserToOrderColumns = true;
            this.dataGrid_content.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid_content.Location = new System.Drawing.Point(13, 59);
            this.dataGrid_content.MultiSelect = false;
            this.dataGrid_content.Name = "dataGrid_content";
            this.dataGrid_content.ReadOnly = true;
            this.dataGrid_content.RowHeadersWidth = 51;
            this.dataGrid_content.RowTemplate.Height = 24;
            this.dataGrid_content.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGrid_content.Size = new System.Drawing.Size(550, 398);
            this.dataGrid_content.TabIndex = 16;
            // 
            // Maintenance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 516);
            this.Controls.Add(this.dataGrid_content);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Maintenance";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Maintenance";
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_content)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGrid_content;
    }
}