namespace BulletJournal
{
    partial class MainForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tab_index = new System.Windows.Forms.TabPage();
            this.button3 = new System.Windows.Forms.Button();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.tab_Daily_Task = new System.Windows.Forms.TabPage();
            this.btn_add = new System.Windows.Forms.Button();
            this.dataGrid_dailyTask = new System.Windows.Forms.DataGridView();
            this.tab_Monthly_Task = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tab_Future_Log = new System.Windows.Forms.TabPage();
            this.btn_addFutureLog = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.tab_Collection = new System.Windows.Forms.TabPage();
            this.btn_addCollection = new System.Windows.Forms.Button();
            this.dataGrid_collection = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addEntryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.maintenanceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1.SuspendLayout();
            this.tab_index.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.tab_Daily_Task.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_dailyTask)).BeginInit();
            this.tab_Monthly_Task.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tab_Future_Log.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.tab_Collection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_collection)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tab_index);
            this.tabControl1.Controls.Add(this.tab_Daily_Task);
            this.tabControl1.Controls.Add(this.tab_Monthly_Task);
            this.tabControl1.Controls.Add(this.tab_Future_Log);
            this.tabControl1.Controls.Add(this.tab_Collection);
            this.tabControl1.Location = new System.Drawing.Point(37, 52);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(743, 450);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.Tag = "";
            // 
            // tab_index
            // 
            this.tab_index.Controls.Add(this.button3);
            this.tab_index.Controls.Add(this.dataGridView3);
            this.tab_index.Location = new System.Drawing.Point(4, 25);
            this.tab_index.Name = "tab_index";
            this.tab_index.Padding = new System.Windows.Forms.Padding(3);
            this.tab_index.Size = new System.Drawing.Size(735, 421);
            this.tab_index.TabIndex = 0;
            this.tab_index.Text = "Index";
            this.tab_index.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(641, 371);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 42);
            this.button3.TabIndex = 3;
            this.button3.Text = "Add";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // dataGridView3
            // 
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Location = new System.Drawing.Point(16, 17);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.RowHeadersWidth = 51;
            this.dataGridView3.RowTemplate.Height = 24;
            this.dataGridView3.Size = new System.Drawing.Size(700, 350);
            this.dataGridView3.TabIndex = 1;
            // 
            // tab_Daily_Task
            // 
            this.tab_Daily_Task.Controls.Add(this.btn_add);
            this.tab_Daily_Task.Controls.Add(this.dataGrid_dailyTask);
            this.tab_Daily_Task.Location = new System.Drawing.Point(4, 25);
            this.tab_Daily_Task.Name = "tab_Daily_Task";
            this.tab_Daily_Task.Size = new System.Drawing.Size(735, 421);
            this.tab_Daily_Task.TabIndex = 3;
            this.tab_Daily_Task.Text = "Daily Task";
            this.tab_Daily_Task.UseVisualStyleBackColor = true;
            // 
            // btn_add
            // 
            this.btn_add.Location = new System.Drawing.Point(641, 371);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(75, 42);
            this.btn_add.TabIndex = 2;
            this.btn_add.Text = "Add";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // dataGrid_dailyTask
            // 
            this.dataGrid_dailyTask.AllowUserToAddRows = false;
            this.dataGrid_dailyTask.AllowUserToDeleteRows = false;
            this.dataGrid_dailyTask.AllowUserToOrderColumns = true;
            this.dataGrid_dailyTask.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid_dailyTask.Location = new System.Drawing.Point(16, 17);
            this.dataGrid_dailyTask.MultiSelect = false;
            this.dataGrid_dailyTask.Name = "dataGrid_dailyTask";
            this.dataGrid_dailyTask.ReadOnly = true;
            this.dataGrid_dailyTask.RowHeadersWidth = 51;
            this.dataGrid_dailyTask.RowTemplate.Height = 24;
            this.dataGrid_dailyTask.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGrid_dailyTask.Size = new System.Drawing.Size(700, 350);
            this.dataGrid_dailyTask.TabIndex = 0;
            // 
            // tab_Monthly_Task
            // 
            this.tab_Monthly_Task.Controls.Add(this.button1);
            this.tab_Monthly_Task.Controls.Add(this.dataGridView1);
            this.tab_Monthly_Task.Location = new System.Drawing.Point(4, 25);
            this.tab_Monthly_Task.Name = "tab_Monthly_Task";
            this.tab_Monthly_Task.Size = new System.Drawing.Size(735, 421);
            this.tab_Monthly_Task.TabIndex = 2;
            this.tab_Monthly_Task.Text = "Monthly Task";
            this.tab_Monthly_Task.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(641, 371);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 42);
            this.button1.TabIndex = 3;
            this.button1.Text = "Add";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(16, 17);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(700, 350);
            this.dataGridView1.TabIndex = 0;
            // 
            // tab_Future_Log
            // 
            this.tab_Future_Log.Controls.Add(this.btn_addFutureLog);
            this.tab_Future_Log.Controls.Add(this.dataGridView2);
            this.tab_Future_Log.Location = new System.Drawing.Point(4, 25);
            this.tab_Future_Log.Name = "tab_Future_Log";
            this.tab_Future_Log.Padding = new System.Windows.Forms.Padding(3);
            this.tab_Future_Log.Size = new System.Drawing.Size(735, 421);
            this.tab_Future_Log.TabIndex = 1;
            this.tab_Future_Log.Text = "Future Log";
            this.tab_Future_Log.UseVisualStyleBackColor = true;
            // 
            // btn_addFutureLog
            // 
            this.btn_addFutureLog.Location = new System.Drawing.Point(641, 371);
            this.btn_addFutureLog.Name = "btn_addFutureLog";
            this.btn_addFutureLog.Size = new System.Drawing.Size(75, 42);
            this.btn_addFutureLog.TabIndex = 4;
            this.btn_addFutureLog.Text = "Add";
            this.btn_addFutureLog.UseVisualStyleBackColor = true;
            this.btn_addFutureLog.Click += new System.EventHandler(this.btn_addFutureLog_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(16, 17);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 51;
            this.dataGridView2.RowTemplate.Height = 24;
            this.dataGridView2.Size = new System.Drawing.Size(700, 350);
            this.dataGridView2.TabIndex = 1;
            // 
            // tab_Collection
            // 
            this.tab_Collection.Controls.Add(this.btn_addCollection);
            this.tab_Collection.Controls.Add(this.dataGrid_collection);
            this.tab_Collection.Location = new System.Drawing.Point(4, 25);
            this.tab_Collection.Name = "tab_Collection";
            this.tab_Collection.Size = new System.Drawing.Size(735, 421);
            this.tab_Collection.TabIndex = 4;
            this.tab_Collection.Text = "Collection";
            this.tab_Collection.UseVisualStyleBackColor = true;
            // 
            // btn_addCollection
            // 
            this.btn_addCollection.Location = new System.Drawing.Point(641, 371);
            this.btn_addCollection.Name = "btn_addCollection";
            this.btn_addCollection.Size = new System.Drawing.Size(75, 42);
            this.btn_addCollection.TabIndex = 5;
            this.btn_addCollection.Text = "Add";
            this.btn_addCollection.UseVisualStyleBackColor = true;
            this.btn_addCollection.Click += new System.EventHandler(this.button4_Click);
            // 
            // dataGrid_collection
            // 
            this.dataGrid_collection.AllowUserToAddRows = false;
            this.dataGrid_collection.AllowUserToDeleteRows = false;
            this.dataGrid_collection.AllowUserToOrderColumns = true;
            this.dataGrid_collection.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid_collection.Location = new System.Drawing.Point(16, 17);
            this.dataGrid_collection.MultiSelect = false;
            this.dataGrid_collection.Name = "dataGrid_collection";
            this.dataGrid_collection.ReadOnly = true;
            this.dataGrid_collection.RowHeadersWidth = 51;
            this.dataGrid_collection.RowTemplate.Height = 24;
            this.dataGrid_collection.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGrid_collection.Size = new System.Drawing.Size(700, 350);
            this.dataGrid_collection.TabIndex = 2;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(813, 30);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addEntryToolStripMenuItem,
            this.maintenanceToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // addEntryToolStripMenuItem
            // 
            this.addEntryToolStripMenuItem.Name = "addEntryToolStripMenuItem";
            this.addEntryToolStripMenuItem.Size = new System.Drawing.Size(177, 26);
            this.addEntryToolStripMenuItem.Text = "Refresh";
            this.addEntryToolStripMenuItem.Click += new System.EventHandler(this.addEntryToolStripMenuItem_Click);
            // 
            // maintenanceToolStripMenuItem
            // 
            this.maintenanceToolStripMenuItem.Name = "maintenanceToolStripMenuItem";
            this.maintenanceToolStripMenuItem.Size = new System.Drawing.Size(177, 26);
            this.maintenanceToolStripMenuItem.Text = "Maintenance";
            this.maintenanceToolStripMenuItem.Click += new System.EventHandler(this.maintenanceToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(177, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 532);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "<••> Bullet Journal";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tab_index.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.tab_Daily_Task.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_dailyTask)).EndInit();
            this.tab_Monthly_Task.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tab_Future_Log.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.tab_Collection.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_collection)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tab_index;
        private System.Windows.Forms.TabPage tab_Future_Log;
        private System.Windows.Forms.TabPage tab_Monthly_Task;
        private System.Windows.Forms.TabPage tab_Daily_Task;
        private System.Windows.Forms.TabPage tab_Collection;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addEntryToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolStripMenuItem maintenanceToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGrid_dailyTask;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.Button btn_addFutureLog;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button btn_addCollection;
        private System.Windows.Forms.DataGridView dataGrid_collection;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    }
}

