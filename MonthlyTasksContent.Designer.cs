
namespace BulletJournal
{
    partial class MonthlyTasksContent
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MonthlyTasksContent));
            this.label5 = new System.Windows.Forms.Label();
            this.txt_collectionSearch = new System.Windows.Forms.TextBox();
            this.lbl_title = new System.Windows.Forms.Label();
            this.btn_addDaily = new System.Windows.Forms.Button();
            this.dataGrid_content = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.migrateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toExistingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dailyTaskToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.monthlyTaskToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.futureLogToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.asNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dailyTaskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.monthlyTaskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.futureLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_content)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(336, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 17);
            this.label5.TabIndex = 23;
            this.label5.Text = "Search:";
            // 
            // txt_collectionSearch
            // 
            this.txt_collectionSearch.Location = new System.Drawing.Point(400, 15);
            this.txt_collectionSearch.Name = "txt_collectionSearch";
            this.txt_collectionSearch.Size = new System.Drawing.Size(163, 22);
            this.txt_collectionSearch.TabIndex = 21;
            // 
            // lbl_title
            // 
            this.lbl_title.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_title.Location = new System.Drawing.Point(13, 11);
            this.lbl_title.Name = "lbl_title";
            this.lbl_title.Size = new System.Drawing.Size(323, 41);
            this.lbl_title.TabIndex = 24;
            this.lbl_title.Text = "label1";
            // 
            // btn_addDaily
            // 
            this.btn_addDaily.Location = new System.Drawing.Point(488, 463);
            this.btn_addDaily.Name = "btn_addDaily";
            this.btn_addDaily.Size = new System.Drawing.Size(75, 42);
            this.btn_addDaily.TabIndex = 22;
            this.btn_addDaily.Text = "Add";
            this.btn_addDaily.UseVisualStyleBackColor = true;
            this.btn_addDaily.Click += new System.EventHandler(this.btn_addDaily_Click);
            // 
            // dataGrid_content
            // 
            this.dataGrid_content.AllowUserToAddRows = false;
            this.dataGrid_content.AllowUserToDeleteRows = false;
            this.dataGrid_content.AllowUserToOrderColumns = true;
            this.dataGrid_content.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGrid_content.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGrid_content.Location = new System.Drawing.Point(13, 59);
            this.dataGrid_content.MultiSelect = false;
            this.dataGrid_content.Name = "dataGrid_content";
            this.dataGrid_content.ReadOnly = true;
            this.dataGrid_content.RowHeadersWidth = 51;
            this.dataGrid_content.RowTemplate.Height = 24;
            this.dataGrid_content.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGrid_content.Size = new System.Drawing.Size(550, 398);
            this.dataGrid_content.TabIndex = 25;
            this.dataGrid_content.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_content_CellContentClick);
            this.dataGrid_content.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_content_CellDoubleClick);
            this.dataGrid_content.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGrid_content_CellMouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.migrateToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(131, 76);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(130, 24);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(130, 24);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // migrateToolStripMenuItem
            // 
            this.migrateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toExistingToolStripMenuItem,
            this.asNewToolStripMenuItem});
            this.migrateToolStripMenuItem.Name = "migrateToolStripMenuItem";
            this.migrateToolStripMenuItem.Size = new System.Drawing.Size(130, 24);
            this.migrateToolStripMenuItem.Text = "Migrate";
            // 
            // toExistingToolStripMenuItem
            // 
            this.toExistingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dailyTaskToolStripMenuItem1,
            this.monthlyTaskToolStripMenuItem1,
            this.futureLogToolStripMenuItem1});
            this.toExistingToolStripMenuItem.Name = "toExistingToolStripMenuItem";
            this.toExistingToolStripMenuItem.Size = new System.Drawing.Size(163, 26);
            this.toExistingToolStripMenuItem.Text = "To Existing";
            // 
            // dailyTaskToolStripMenuItem1
            // 
            this.dailyTaskToolStripMenuItem1.Name = "dailyTaskToolStripMenuItem1";
            this.dailyTaskToolStripMenuItem1.Size = new System.Drawing.Size(177, 26);
            this.dailyTaskToolStripMenuItem1.Text = "Daily Task";
            this.dailyTaskToolStripMenuItem1.Click += new System.EventHandler(this.dailyTaskToolStripMenuItem1_Click);
            // 
            // monthlyTaskToolStripMenuItem1
            // 
            this.monthlyTaskToolStripMenuItem1.Name = "monthlyTaskToolStripMenuItem1";
            this.monthlyTaskToolStripMenuItem1.Size = new System.Drawing.Size(177, 26);
            this.monthlyTaskToolStripMenuItem1.Text = "Monthly Task";
            this.monthlyTaskToolStripMenuItem1.Click += new System.EventHandler(this.monthlyTaskToolStripMenuItem1_Click);
            // 
            // futureLogToolStripMenuItem1
            // 
            this.futureLogToolStripMenuItem1.Name = "futureLogToolStripMenuItem1";
            this.futureLogToolStripMenuItem1.Size = new System.Drawing.Size(177, 26);
            this.futureLogToolStripMenuItem1.Text = "Future Log";
            this.futureLogToolStripMenuItem1.Click += new System.EventHandler(this.futureLogToolStripMenuItem1_Click);
            // 
            // asNewToolStripMenuItem
            // 
            this.asNewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dailyTaskToolStripMenuItem,
            this.monthlyTaskToolStripMenuItem,
            this.futureLogToolStripMenuItem});
            this.asNewToolStripMenuItem.Name = "asNewToolStripMenuItem";
            this.asNewToolStripMenuItem.Size = new System.Drawing.Size(163, 26);
            this.asNewToolStripMenuItem.Text = "As New";
            // 
            // dailyTaskToolStripMenuItem
            // 
            this.dailyTaskToolStripMenuItem.Name = "dailyTaskToolStripMenuItem";
            this.dailyTaskToolStripMenuItem.Size = new System.Drawing.Size(177, 26);
            this.dailyTaskToolStripMenuItem.Text = "Daily Task";
            this.dailyTaskToolStripMenuItem.Click += new System.EventHandler(this.dailyTaskToolStripMenuItem_Click);
            // 
            // monthlyTaskToolStripMenuItem
            // 
            this.monthlyTaskToolStripMenuItem.Name = "monthlyTaskToolStripMenuItem";
            this.monthlyTaskToolStripMenuItem.Size = new System.Drawing.Size(177, 26);
            this.monthlyTaskToolStripMenuItem.Text = "Monthly Task";
            this.monthlyTaskToolStripMenuItem.Click += new System.EventHandler(this.monthlyTaskToolStripMenuItem_Click);
            // 
            // futureLogToolStripMenuItem
            // 
            this.futureLogToolStripMenuItem.Name = "futureLogToolStripMenuItem";
            this.futureLogToolStripMenuItem.Size = new System.Drawing.Size(177, 26);
            this.futureLogToolStripMenuItem.Text = "Future Log";
            this.futureLogToolStripMenuItem.Click += new System.EventHandler(this.futureLogToolStripMenuItem_Click);
            // 
            // MonthlyTasksContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 520);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txt_collectionSearch);
            this.Controls.Add(this.lbl_title);
            this.Controls.Add(this.btn_addDaily);
            this.Controls.Add(this.dataGrid_content);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "MonthlyTasksContent";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Monthly Tasks";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MonthlyTasksContent_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_content)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_collectionSearch;
        private System.Windows.Forms.Label lbl_title;
        private System.Windows.Forms.Button btn_addDaily;
        private System.Windows.Forms.DataGridView dataGrid_content;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem migrateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toExistingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem asNewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dailyTaskToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem monthlyTaskToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem futureLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dailyTaskToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem monthlyTaskToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem futureLogToolStripMenuItem1;
    }
}