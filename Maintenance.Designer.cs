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
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tab_Task_Description = new System.Windows.Forms.TabPage();
            this.btn_add = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.descriptionTableBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.journalDatabaseDataSet = new BulletJournal.JournalDatabaseDataSet();
            this.descriptionTableTableAdapter = new BulletJournal.JournalDatabaseDataSetTableAdapters.DescriptionTableTableAdapter();
            this.taskIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.taskDescriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1.SuspendLayout();
            this.tab_Task_Description.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.descriptionTableBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.journalDatabaseDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tab_Task_Description);
            this.tabControl1.Location = new System.Drawing.Point(13, 24);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(775, 444);
            this.tabControl1.TabIndex = 0;
            // 
            // tab_Task_Description
            // 
            this.tab_Task_Description.Controls.Add(this.btn_add);
            this.tab_Task_Description.Controls.Add(this.dataGridView1);
            this.tab_Task_Description.Location = new System.Drawing.Point(4, 25);
            this.tab_Task_Description.Name = "tab_Task_Description";
            this.tab_Task_Description.Padding = new System.Windows.Forms.Padding(3);
            this.tab_Task_Description.Size = new System.Drawing.Size(767, 415);
            this.tab_Task_Description.TabIndex = 0;
            this.tab_Task_Description.Text = "Task Description";
            this.tab_Task_Description.UseVisualStyleBackColor = true;
            // 
            // btn_add
            // 
            this.btn_add.Location = new System.Drawing.Point(668, 367);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(75, 42);
            this.btn_add.TabIndex = 1;
            this.btn_add.Text = "Add";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.taskIdDataGridViewTextBoxColumn,
            this.taskDescriptionDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.descriptionTableBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(23, 16);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(720, 345);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            this.dataGridView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseUp);
            // 
            // descriptionTableBindingSource
            // 
            this.descriptionTableBindingSource.DataMember = "DescriptionTable";
            this.descriptionTableBindingSource.DataSource = this.journalDatabaseDataSet;
            // 
            // journalDatabaseDataSet
            // 
            this.journalDatabaseDataSet.DataSetName = "JournalDatabaseDataSet";
            this.journalDatabaseDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // descriptionTableTableAdapter
            // 
            this.descriptionTableTableAdapter.ClearBeforeFill = true;
            // 
            // taskIdDataGridViewTextBoxColumn
            // 
            this.taskIdDataGridViewTextBoxColumn.DataPropertyName = "TaskId";
            this.taskIdDataGridViewTextBoxColumn.HeaderText = "Task Id";
            this.taskIdDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.taskIdDataGridViewTextBoxColumn.Name = "taskIdDataGridViewTextBoxColumn";
            this.taskIdDataGridViewTextBoxColumn.ReadOnly = true;
            this.taskIdDataGridViewTextBoxColumn.Visible = false;
            this.taskIdDataGridViewTextBoxColumn.Width = 6;
            // 
            // taskDescriptionDataGridViewTextBoxColumn
            // 
            this.taskDescriptionDataGridViewTextBoxColumn.DataPropertyName = "TaskDescription";
            this.taskDescriptionDataGridViewTextBoxColumn.HeaderText = "Task Description";
            this.taskDescriptionDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.taskDescriptionDataGridViewTextBoxColumn.Name = "taskDescriptionDataGridViewTextBoxColumn";
            this.taskDescriptionDataGridViewTextBoxColumn.ReadOnly = true;
            this.taskDescriptionDataGridViewTextBoxColumn.Width = 800;
            // 
            // Maintenance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 490);
            this.Controls.Add(this.tabControl1);
            this.Name = "Maintenance";
            this.Text = "<••> Maintenance";
            this.Load += new System.EventHandler(this.Maintenance_Load);
            this.tabControl1.ResumeLayout(false);
            this.tab_Task_Description.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.descriptionTableBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.journalDatabaseDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tab_Task_Description;
        private System.Windows.Forms.DataGridView dataGridView1;
        private JournalDatabaseDataSet journalDatabaseDataSet;
        private System.Windows.Forms.BindingSource descriptionTableBindingSource;
        private JournalDatabaseDataSetTableAdapters.DescriptionTableTableAdapter descriptionTableTableAdapter;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.DataGridViewTextBoxColumn taskIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn taskDescriptionDataGridViewTextBoxColumn;
    }
}