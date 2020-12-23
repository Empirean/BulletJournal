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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tab_index = new System.Windows.Forms.TabPage();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.btn_viewHabit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_habitSearch = new System.Windows.Forms.TextBox();
            this.dataGrid_tracker = new System.Windows.Forms.DataGridView();
            this.tab_Daily_Task = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_dailySearch = new System.Windows.Forms.TextBox();
            this.btn_addDailyTask = new System.Windows.Forms.Button();
            this.dataGrid_dailyTask = new System.Windows.Forms.DataGridView();
            this.tab_Monthly_Task = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_monthlySearch = new System.Windows.Forms.TextBox();
            this.btn_addMonthlyTask = new System.Windows.Forms.Button();
            this.dataGrid_monthly = new System.Windows.Forms.DataGridView();
            this.tab_Future_Log = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_futureSearch = new System.Windows.Forms.TextBox();
            this.btn_addFutureLog = new System.Windows.Forms.Button();
            this.dataGrid_futureLog = new System.Windows.Forms.DataGridView();
            this.tab_Collection = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_collectionSearch = new System.Windows.Forms.TextBox();
            this.btn_addCollection = new System.Windows.Forms.Button();
            this.dataGrid_notes = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_refresh = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.dailyTaskToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.monthlyTaskToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.futureLogToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.migrate = new System.Windows.Forms.ToolStripMenuItem();
            this.toExistingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dailyTaskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.monthlyTaskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.futureLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.asNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dailyTaskToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.monthlyTaskToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.futureLogToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.quickSearchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1.SuspendLayout();
            this.tab_index.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_tracker)).BeginInit();
            this.tab_Daily_Task.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_dailyTask)).BeginInit();
            this.tab_Monthly_Task.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_monthly)).BeginInit();
            this.tab_Future_Log.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_futureLog)).BeginInit();
            this.tab_Collection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_notes)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
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
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tab_index
            // 
            this.tab_index.Controls.Add(this.dateTimePicker1);
            this.tab_index.Controls.Add(this.btn_viewHabit);
            this.tab_index.Controls.Add(this.label1);
            this.tab_index.Controls.Add(this.txt_habitSearch);
            this.tab_index.Controls.Add(this.dataGrid_tracker);
            this.tab_index.Location = new System.Drawing.Point(4, 25);
            this.tab_index.Name = "tab_index";
            this.tab_index.Padding = new System.Windows.Forms.Padding(3);
            this.tab_index.Size = new System.Drawing.Size(735, 421);
            this.tab_index.TabIndex = 0;
            this.tab_index.Text = "Tracker";
            this.tab_index.UseVisualStyleBackColor = true;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "MMMM yyyy";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(16, 20);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.ShowUpDown = true;
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 22);
            this.dateTimePicker1.TabIndex = 14;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // btn_viewHabit
            // 
            this.btn_viewHabit.Location = new System.Drawing.Point(641, 371);
            this.btn_viewHabit.Name = "btn_viewHabit";
            this.btn_viewHabit.Size = new System.Drawing.Size(75, 42);
            this.btn_viewHabit.TabIndex = 13;
            this.btn_viewHabit.Text = "View All";
            this.btn_viewHabit.UseVisualStyleBackColor = true;
            this.btn_viewHabit.Click += new System.EventHandler(this.btn_viewHabit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(410, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 17);
            this.label1.TabIndex = 12;
            this.label1.Text = "Search:";
            // 
            // txt_habitSearch
            // 
            this.txt_habitSearch.Location = new System.Drawing.Point(478, 17);
            this.txt_habitSearch.Name = "txt_habitSearch";
            this.txt_habitSearch.Size = new System.Drawing.Size(238, 22);
            this.txt_habitSearch.TabIndex = 11;
            // 
            // dataGrid_tracker
            // 
            this.dataGrid_tracker.AllowUserToAddRows = false;
            this.dataGrid_tracker.AllowUserToDeleteRows = false;
            this.dataGrid_tracker.AllowUserToOrderColumns = true;
            this.dataGrid_tracker.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid_tracker.Location = new System.Drawing.Point(16, 58);
            this.dataGrid_tracker.MultiSelect = false;
            this.dataGrid_tracker.Name = "dataGrid_tracker";
            this.dataGrid_tracker.ReadOnly = true;
            this.dataGrid_tracker.RowHeadersWidth = 51;
            this.dataGrid_tracker.RowTemplate.Height = 24;
            this.dataGrid_tracker.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGrid_tracker.Size = new System.Drawing.Size(700, 309);
            this.dataGrid_tracker.TabIndex = 10;
            this.dataGrid_tracker.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_tracker_CellContentClick);
            // 
            // tab_Daily_Task
            // 
            this.tab_Daily_Task.Controls.Add(this.label5);
            this.tab_Daily_Task.Controls.Add(this.txt_dailySearch);
            this.tab_Daily_Task.Controls.Add(this.btn_addDailyTask);
            this.tab_Daily_Task.Controls.Add(this.dataGrid_dailyTask);
            this.tab_Daily_Task.Location = new System.Drawing.Point(4, 25);
            this.tab_Daily_Task.Name = "tab_Daily_Task";
            this.tab_Daily_Task.Size = new System.Drawing.Size(735, 421);
            this.tab_Daily_Task.TabIndex = 3;
            this.tab_Daily_Task.Text = "Daily Task";
            this.tab_Daily_Task.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(410, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 17);
            this.label5.TabIndex = 10;
            this.label5.Text = "Search:";
            // 
            // txt_dailySearch
            // 
            this.txt_dailySearch.Location = new System.Drawing.Point(478, 17);
            this.txt_dailySearch.Name = "txt_dailySearch";
            this.txt_dailySearch.Size = new System.Drawing.Size(238, 22);
            this.txt_dailySearch.TabIndex = 0;
            this.txt_dailySearch.TextChanged += new System.EventHandler(this.txt_dailySearch_TextChanged);
            // 
            // btn_addDailyTask
            // 
            this.btn_addDailyTask.Location = new System.Drawing.Point(641, 371);
            this.btn_addDailyTask.Name = "btn_addDailyTask";
            this.btn_addDailyTask.Size = new System.Drawing.Size(75, 42);
            this.btn_addDailyTask.TabIndex = 1;
            this.btn_addDailyTask.Text = "Add";
            this.btn_addDailyTask.UseVisualStyleBackColor = true;
            this.btn_addDailyTask.Click += new System.EventHandler(this.btn_addDailyTask_Click);
            // 
            // dataGrid_dailyTask
            // 
            this.dataGrid_dailyTask.AllowUserToAddRows = false;
            this.dataGrid_dailyTask.AllowUserToDeleteRows = false;
            this.dataGrid_dailyTask.AllowUserToOrderColumns = true;
            this.dataGrid_dailyTask.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid_dailyTask.Location = new System.Drawing.Point(16, 58);
            this.dataGrid_dailyTask.MultiSelect = false;
            this.dataGrid_dailyTask.Name = "dataGrid_dailyTask";
            this.dataGrid_dailyTask.ReadOnly = true;
            this.dataGrid_dailyTask.RowHeadersWidth = 51;
            this.dataGrid_dailyTask.RowTemplate.Height = 24;
            this.dataGrid_dailyTask.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGrid_dailyTask.Size = new System.Drawing.Size(700, 309);
            this.dataGrid_dailyTask.TabIndex = 10;
            this.dataGrid_dailyTask.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_dailyTask_CellContentClick);
            this.dataGrid_dailyTask.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_dailyTask_CellDoubleClick);
            this.dataGrid_dailyTask.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGrid_dailyTask_CellMouseUp);
            // 
            // tab_Monthly_Task
            // 
            this.tab_Monthly_Task.Controls.Add(this.label4);
            this.tab_Monthly_Task.Controls.Add(this.txt_monthlySearch);
            this.tab_Monthly_Task.Controls.Add(this.btn_addMonthlyTask);
            this.tab_Monthly_Task.Controls.Add(this.dataGrid_monthly);
            this.tab_Monthly_Task.Location = new System.Drawing.Point(4, 25);
            this.tab_Monthly_Task.Name = "tab_Monthly_Task";
            this.tab_Monthly_Task.Size = new System.Drawing.Size(735, 421);
            this.tab_Monthly_Task.TabIndex = 2;
            this.tab_Monthly_Task.Text = "Monthly Task";
            this.tab_Monthly_Task.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(410, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "Search:";
            // 
            // txt_monthlySearch
            // 
            this.txt_monthlySearch.Location = new System.Drawing.Point(478, 17);
            this.txt_monthlySearch.Name = "txt_monthlySearch";
            this.txt_monthlySearch.Size = new System.Drawing.Size(238, 22);
            this.txt_monthlySearch.TabIndex = 0;
            this.txt_monthlySearch.TextChanged += new System.EventHandler(this.txt_monthlySearch_TextChanged);
            // 
            // btn_addMonthlyTask
            // 
            this.btn_addMonthlyTask.Location = new System.Drawing.Point(641, 371);
            this.btn_addMonthlyTask.Name = "btn_addMonthlyTask";
            this.btn_addMonthlyTask.Size = new System.Drawing.Size(75, 42);
            this.btn_addMonthlyTask.TabIndex = 1;
            this.btn_addMonthlyTask.Text = "Add";
            this.btn_addMonthlyTask.UseVisualStyleBackColor = true;
            this.btn_addMonthlyTask.Click += new System.EventHandler(this.btn_addMonthlyTask_Click);
            // 
            // dataGrid_monthly
            // 
            this.dataGrid_monthly.AllowUserToAddRows = false;
            this.dataGrid_monthly.AllowUserToDeleteRows = false;
            this.dataGrid_monthly.AllowUserToOrderColumns = true;
            this.dataGrid_monthly.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid_monthly.Location = new System.Drawing.Point(16, 58);
            this.dataGrid_monthly.MultiSelect = false;
            this.dataGrid_monthly.Name = "dataGrid_monthly";
            this.dataGrid_monthly.ReadOnly = true;
            this.dataGrid_monthly.RowHeadersWidth = 51;
            this.dataGrid_monthly.RowTemplate.Height = 24;
            this.dataGrid_monthly.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGrid_monthly.Size = new System.Drawing.Size(700, 309);
            this.dataGrid_monthly.TabIndex = 10;
            this.dataGrid_monthly.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_monthly_CellContentClick);
            this.dataGrid_monthly.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_monthly_CellDoubleClick);
            this.dataGrid_monthly.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGrid_monthly_CellMouseUp);
            // 
            // tab_Future_Log
            // 
            this.tab_Future_Log.Controls.Add(this.label3);
            this.tab_Future_Log.Controls.Add(this.txt_futureSearch);
            this.tab_Future_Log.Controls.Add(this.btn_addFutureLog);
            this.tab_Future_Log.Controls.Add(this.dataGrid_futureLog);
            this.tab_Future_Log.Location = new System.Drawing.Point(4, 25);
            this.tab_Future_Log.Name = "tab_Future_Log";
            this.tab_Future_Log.Padding = new System.Windows.Forms.Padding(3);
            this.tab_Future_Log.Size = new System.Drawing.Size(735, 421);
            this.tab_Future_Log.TabIndex = 1;
            this.tab_Future_Log.Text = "Future Log";
            this.tab_Future_Log.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(410, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Search:";
            // 
            // txt_futureSearch
            // 
            this.txt_futureSearch.Location = new System.Drawing.Point(478, 17);
            this.txt_futureSearch.Name = "txt_futureSearch";
            this.txt_futureSearch.Size = new System.Drawing.Size(238, 22);
            this.txt_futureSearch.TabIndex = 0;
            this.txt_futureSearch.TextChanged += new System.EventHandler(this.txt_futureSearch_TextChanged);
            // 
            // btn_addFutureLog
            // 
            this.btn_addFutureLog.Location = new System.Drawing.Point(641, 371);
            this.btn_addFutureLog.Name = "btn_addFutureLog";
            this.btn_addFutureLog.Size = new System.Drawing.Size(75, 42);
            this.btn_addFutureLog.TabIndex = 1;
            this.btn_addFutureLog.Text = "Add";
            this.btn_addFutureLog.UseVisualStyleBackColor = true;
            this.btn_addFutureLog.Click += new System.EventHandler(this.btn_addFutureLog_Click);
            // 
            // dataGrid_futureLog
            // 
            this.dataGrid_futureLog.AllowUserToAddRows = false;
            this.dataGrid_futureLog.AllowUserToDeleteRows = false;
            this.dataGrid_futureLog.AllowUserToOrderColumns = true;
            this.dataGrid_futureLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid_futureLog.Location = new System.Drawing.Point(16, 58);
            this.dataGrid_futureLog.MultiSelect = false;
            this.dataGrid_futureLog.Name = "dataGrid_futureLog";
            this.dataGrid_futureLog.ReadOnly = true;
            this.dataGrid_futureLog.RowHeadersWidth = 51;
            this.dataGrid_futureLog.RowTemplate.Height = 24;
            this.dataGrid_futureLog.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGrid_futureLog.Size = new System.Drawing.Size(700, 309);
            this.dataGrid_futureLog.TabIndex = 10;
            this.dataGrid_futureLog.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_futureLog_CellContentClick);
            this.dataGrid_futureLog.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_futureLog_CellDoubleClick);
            this.dataGrid_futureLog.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGrid_futureLog_CellMouseUp);
            // 
            // tab_Collection
            // 
            this.tab_Collection.Controls.Add(this.label2);
            this.tab_Collection.Controls.Add(this.txt_collectionSearch);
            this.tab_Collection.Controls.Add(this.btn_addCollection);
            this.tab_Collection.Controls.Add(this.dataGrid_notes);
            this.tab_Collection.Location = new System.Drawing.Point(4, 25);
            this.tab_Collection.Name = "tab_Collection";
            this.tab_Collection.Size = new System.Drawing.Size(735, 421);
            this.tab_Collection.TabIndex = 4;
            this.tab_Collection.Text = "Notes";
            this.tab_Collection.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(410, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Search:";
            // 
            // txt_collectionSearch
            // 
            this.txt_collectionSearch.Location = new System.Drawing.Point(478, 17);
            this.txt_collectionSearch.Name = "txt_collectionSearch";
            this.txt_collectionSearch.Size = new System.Drawing.Size(238, 22);
            this.txt_collectionSearch.TabIndex = 0;
            this.txt_collectionSearch.TextChanged += new System.EventHandler(this.txt_collectionSearch_TextChanged);
            // 
            // btn_addCollection
            // 
            this.btn_addCollection.Location = new System.Drawing.Point(641, 371);
            this.btn_addCollection.Name = "btn_addCollection";
            this.btn_addCollection.Size = new System.Drawing.Size(75, 42);
            this.btn_addCollection.TabIndex = 1;
            this.btn_addCollection.Text = "Add";
            this.btn_addCollection.UseVisualStyleBackColor = true;
            this.btn_addCollection.Click += new System.EventHandler(this.btn_addCollection_Click);
            // 
            // dataGrid_notes
            // 
            this.dataGrid_notes.AllowUserToAddRows = false;
            this.dataGrid_notes.AllowUserToDeleteRows = false;
            this.dataGrid_notes.AllowUserToOrderColumns = true;
            this.dataGrid_notes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid_notes.Location = new System.Drawing.Point(16, 58);
            this.dataGrid_notes.MultiSelect = false;
            this.dataGrid_notes.Name = "dataGrid_notes";
            this.dataGrid_notes.ReadOnly = true;
            this.dataGrid_notes.RowHeadersWidth = 51;
            this.dataGrid_notes.RowTemplate.Height = 24;
            this.dataGrid_notes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGrid_notes.Size = new System.Drawing.Size(700, 309);
            this.dataGrid_notes.TabIndex = 10;
            this.dataGrid_notes.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGrid_collection_CellMouseDoubleClick);
            this.dataGrid_notes.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGrid_collection_CellMouseUp);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(813, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_refresh,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // btn_refresh
            // 
            this.btn_refresh.Name = "btn_refresh";
            this.btn_refresh.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.btn_refresh.Size = new System.Drawing.Size(224, 26);
            this.btn_refresh.Text = "&Refresh";
            this.btn_refresh.Click += new System.EventHandler(this.btn_refresh_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.toolStripMenuItem2.Size = new System.Drawing.Size(224, 26);
            this.toolStripMenuItem2.Text = "Control Panel";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dailyTaskToolStripMenuItem2,
            this.monthlyTaskToolStripMenuItem2,
            this.futureLogToolStripMenuItem2});
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(224, 26);
            this.toolStripMenuItem3.Text = "History";
            // 
            // dailyTaskToolStripMenuItem2
            // 
            this.dailyTaskToolStripMenuItem2.Name = "dailyTaskToolStripMenuItem2";
            this.dailyTaskToolStripMenuItem2.Size = new System.Drawing.Size(224, 26);
            this.dailyTaskToolStripMenuItem2.Text = "Daily Task";
            this.dailyTaskToolStripMenuItem2.Click += new System.EventHandler(this.dailyTaskToolStripMenuItem2_Click);
            // 
            // monthlyTaskToolStripMenuItem2
            // 
            this.monthlyTaskToolStripMenuItem2.Name = "monthlyTaskToolStripMenuItem2";
            this.monthlyTaskToolStripMenuItem2.Size = new System.Drawing.Size(224, 26);
            this.monthlyTaskToolStripMenuItem2.Text = "Monthly Task";
            this.monthlyTaskToolStripMenuItem2.Click += new System.EventHandler(this.monthlyTaskToolStripMenuItem2_Click);
            // 
            // futureLogToolStripMenuItem2
            // 
            this.futureLogToolStripMenuItem2.Name = "futureLogToolStripMenuItem2";
            this.futureLogToolStripMenuItem2.Size = new System.Drawing.Size(224, 26);
            this.futureLogToolStripMenuItem2.Text = "Future Log";
            this.futureLogToolStripMenuItem2.Click += new System.EventHandler(this.futureLogToolStripMenuItem2_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(221, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.migrate,
            this.quickSearchToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(164, 100);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(163, 24);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(163, 24);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // migrate
            // 
            this.migrate.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toExistingToolStripMenuItem,
            this.asNewToolStripMenuItem});
            this.migrate.Name = "migrate";
            this.migrate.Size = new System.Drawing.Size(163, 24);
            this.migrate.Text = "Migrate";
            // 
            // toExistingToolStripMenuItem
            // 
            this.toExistingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dailyTaskToolStripMenuItem,
            this.monthlyTaskToolStripMenuItem,
            this.futureLogToolStripMenuItem});
            this.toExistingToolStripMenuItem.Name = "toExistingToolStripMenuItem";
            this.toExistingToolStripMenuItem.Size = new System.Drawing.Size(163, 26);
            this.toExistingToolStripMenuItem.Text = "To Existing";
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
            // asNewToolStripMenuItem
            // 
            this.asNewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dailyTaskToolStripMenuItem1,
            this.monthlyTaskToolStripMenuItem1,
            this.futureLogToolStripMenuItem1});
            this.asNewToolStripMenuItem.Name = "asNewToolStripMenuItem";
            this.asNewToolStripMenuItem.Size = new System.Drawing.Size(163, 26);
            this.asNewToolStripMenuItem.Text = "As New";
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
            // quickSearchToolStripMenuItem
            // 
            this.quickSearchToolStripMenuItem.Name = "quickSearchToolStripMenuItem";
            this.quickSearchToolStripMenuItem.Size = new System.Drawing.Size(163, 24);
            this.quickSearchToolStripMenuItem.Text = "Quick Search";
            this.quickSearchToolStripMenuItem.Click += new System.EventHandler(this.quickSearchToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 518);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Bullet Journal";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
            this.tabControl1.ResumeLayout(false);
            this.tab_index.ResumeLayout(false);
            this.tab_index.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_tracker)).EndInit();
            this.tab_Daily_Task.ResumeLayout(false);
            this.tab_Daily_Task.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_dailyTask)).EndInit();
            this.tab_Monthly_Task.ResumeLayout(false);
            this.tab_Monthly_Task.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_monthly)).EndInit();
            this.tab_Future_Log.ResumeLayout(false);
            this.tab_Future_Log.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_futureLog)).EndInit();
            this.tab_Collection.ResumeLayout(false);
            this.tab_Collection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_notes)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
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
        private System.Windows.Forms.DataGridView dataGrid_monthly;
        private System.Windows.Forms.DataGridView dataGrid_dailyTask;
        private System.Windows.Forms.Button btn_addDailyTask;
        private System.Windows.Forms.Button btn_addMonthlyTask;
        private System.Windows.Forms.DataGridView dataGrid_tracker;
        private System.Windows.Forms.Button btn_addFutureLog;
        private System.Windows.Forms.DataGridView dataGrid_futureLog;
        private System.Windows.Forms.Button btn_addCollection;
        private System.Windows.Forms.DataGridView dataGrid_notes;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btn_refresh;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem migrate;
        private System.Windows.Forms.ToolStripMenuItem toExistingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dailyTaskToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem monthlyTaskToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem futureLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem asNewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dailyTaskToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem monthlyTaskToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem futureLogToolStripMenuItem1;
        private System.Windows.Forms.TextBox txt_collectionSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_futureSearch;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_monthlySearch;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_dailySearch;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem dailyTaskToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem monthlyTaskToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem futureLogToolStripMenuItem2;
        private System.Windows.Forms.Button btn_viewHabit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_habitSearch;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.ToolStripMenuItem quickSearchToolStripMenuItem;
    }
}

