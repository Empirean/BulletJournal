using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;


namespace BulletJournal
{

    //test commit

    public partial class MainForm : Form
    {
        DBTools db;
        JournalTask.EntryType entryType;

        int taskId;
        string title;

        public MainForm()
        {
            InitializeComponent();
            
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            
            db = new DBTools(Properties.Settings.Default.DatabaseConnectionString);

            RefreshGrid();
        }


        private void btn_refresh_Click(object sender, EventArgs e)
        {

            RefreshGrid();
        }

        public void RefreshGrid()
        {
            Populate_Notes();
            Populate_CurrentTasks();
            Populate_MonthlyTasks();
            Populate_FutureTasks();
            Populate_Tracker();
        }

        private void btn_addDailyTask_Click(object sender, EventArgs e)
        {
            Add_Daily();
        }

        private void btn_addCollection_Click(object sender, EventArgs e)
        {
            Add_Notes();
        }


        private void Populate_Tracker()
        {
            string command = "select id, " +
                             "description as [Description] " +
                             "from habit " +
                             "where isvisible = 1";

            SqlParameter[] parameter = new SqlParameter[]
            {

            };

            dataGrid_tracker.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;

            DataTable trackerTable = db.GenericQueryAction(command, parameter);
            int daysInMonth = DateTime.DaysInMonth(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month) + 1;
            for (int i = 1; i < daysInMonth; i++)
            {
                trackerTable.Columns.Add(new DataColumn(i.ToString(), typeof(bool)));

            }

            if (dataGrid_tracker.DataSource != null)
                dataGrid_tracker.Columns["Description"].Frozen = false;
            dataGrid_tracker.DataSource = trackerTable;
            
            dataGrid_tracker.RowHeadersVisible = false;
            dataGrid_tracker.Columns["id"].Visible = false;
            dataGrid_tracker.Columns["Description"].Width = 200;
            dataGrid_tracker.Columns["Description"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            

            for (int i = 1; i < daysInMonth; i++)
            {
                dataGrid_tracker.Columns[i.ToString()].Width = 25;
            }
            CheckAllHabits();
            dataGrid_tracker.Columns["Description"].Frozen = true;
        }

        private void CheckAllHabits()
        {
            DateTime startDate = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, 1);
            DateTime endDate = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, 
                                   DateTime.DaysInMonth(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month ));

            string command = "select iscompleted, " +
                             "habitdate, " +
                             "habitid " +
                             "from tracker " +
                             "where habitid in " +
                             "(select id " +
                             "from habit " +
                             "where isvisible = 1) " +
                             "and habitdate between @startdate and @enddate";

            SqlParameter[] parameter = new SqlParameter[]
            {
                new SqlParameter("@startdate", SqlDbType.Date) { Value = startDate},
                new SqlParameter("@enddate", SqlDbType.Date) { Value = endDate}
            };

            DataTable dataTable = db.GenericQueryAction(command, parameter);

            foreach (DataRow row in dataTable.AsEnumerable().ToList())
            {
                int columnIndex = dataGrid_tracker.Columns[row.Field<DateTime>("habitdate").Day.ToString()].Index;
                
                int rowIndex = GetRowId(dataGrid_tracker, row.Field<int>("habitid"));

                dataGrid_tracker.Rows[rowIndex].Cells[columnIndex].Value = row.Field<bool>("iscompleted");
            }

        }

        private int GetRowId(DataGridView _dataGridView, int _value, string _column = "id")
        {
            foreach (DataGridViewRow row in _dataGridView.Rows)
            {
                if (row.Cells[_column].Value.Equals( _value))
                {
                    return row.Index;
                }    
            }

            return 0;
        }

        private void Populate_Notes()
        {
            
            string command = "select " +
                            "a.id, " +
                            "a.notedescription as [Description], " +
                            "count(b.id) as [Contents], " +
                            "format(a.dateadded, 'dd/MM/yyyy, hh:mm:ss tt') as [Date Added], " +
                            "format(a.datechanged, 'dd/MM/yyyy, hh:mm:ss tt') as [Date Changed] " +
                            "from notes as a " +
                            "left join notes as b " +
                            "on a.id = b.previouslayerid " +
                            "where a.layerid = @layerid " +
                            "and a.notedescription like @filter " +
                            "group by a.id, a.notedescription, " +
                            "format(a.dateadded, 'dd/MM/yyyy, hh:mm:ss tt'), " +
                            "format(a.datechanged, 'dd/MM/yyyy, hh:mm:ss tt') ";
            

            SqlParameter[] paramters = new SqlParameter[]
            {

                new SqlParameter("@layerid", SqlDbType.Int) { Value = 0},
                new SqlParameter("@filter", SqlDbType.NVarChar) { Value = '%' + txt_collectionSearch.Text + '%' }
            };

            dataGrid_notes.DataSource = db.GenericQueryAction(command, paramters);
            dataGrid_notes.RowHeadersVisible = false;
            // format grid
            dataGrid_notes.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;

            dataGrid_notes.Columns[0].Visible = false;
            dataGrid_notes.Columns[0].Width = 1;
            dataGrid_notes.Columns["Description"].Width = 450;
            dataGrid_notes.Columns["Description"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;            
            dataGrid_notes.Columns["Contents"].Width = 70;

            dataGrid_notes.Columns["Date Added"].Width = 150;
            dataGrid_notes.Columns["Date Added"].Visible = Properties.Settings.Default.NotesDateAdded;

            dataGrid_notes.Columns["Date Changed"].Width = 150;
            dataGrid_notes.Columns["Date Changed"].Visible = Properties.Settings.Default.NotesDateChanged;

        }

        private void Populate_CurrentTasks()
        {
            
            string command = "select " +
                            "a.id, " +
                            "a.iscompleted as [Status], " +
                            "case " +
                            "when a.taskisimportant = 1 " +
                            "then '*' " +
                            "else '' end as [I], " +
                            "case " +
                            "when a.tasktype = 0 then 'TASK' " +
                            "when a.tasktype = 1 then 'EVENT' " +
                            "when a.tasktype = 2 then 'NOTES' " +
                            "else 'CLOSED' end as [Type], " +
                            "a.description as [Description], " +
                            "sum(case when b.datecompleted is null and b.id is not null then 1 " +
                            "else 0 end) as [Contents], " +
                            "format(a.dateadded, 'dd/MM/yyyy, hh:mm:ss tt') as [Date Added], " +
                            "format(a.datechanged, 'dd/MM/yyyy, hh:mm:ss tt') as [Date Changed] " +
                            "from currenttasks as a " +
                            "left join currenttasks as b " +
                            "on a.id = b.previouslayerid " +
                            "where a.layerid = @layerid " +
                            "and a.description like @filter " +
                            "and a.datecompleted is null " +
                            "group by a.id, " +
                            "a.iscompleted, " +
                            "a.description, " +
                            "case " +
                            "when a.tasktype = 0 then 'TASK' " +
                            "when a.tasktype = 1 then 'EVENT' " +
                            "when a.tasktype = 2 then 'NOTES' " +
                            "else 'CLOSED' end, " +
                            "case " +
                            "when a.taskisimportant = 1 " +
                            "then '*' " +
                            "else '' end, " +
                            "a.description, " +
                            "format(a.dateadded, 'dd/MM/yyyy, hh:mm:ss tt'), " +
                            "format(a.datechanged, 'dd/MM/yyyy, hh:mm:ss tt') ";

            SqlParameter[] paramters = new SqlParameter[]
            {

                new SqlParameter("@layerid", SqlDbType.Int) { Value = 0},
                new SqlParameter("@filter", SqlDbType.NVarChar) { Value = '%' + txt_dailySearch.Text + '%' }
            };

            dataGrid_dailyTask.DataSource = db.GenericQueryAction(command, paramters);
            dataGrid_dailyTask.RowHeadersVisible = false;

            // format grid
            dataGrid_dailyTask.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            
            dataGrid_dailyTask.Columns[0].Visible = false;
            dataGrid_dailyTask.Columns[0].Width = 1;
            dataGrid_dailyTask.Columns["Status"].Width = 50;

            dataGrid_dailyTask.Columns["I"].Width = 30;
            dataGrid_dailyTask.Columns["I"].Visible = Properties.Settings.Default.DailyTaskIsImportant;

            dataGrid_dailyTask.Columns["Type"].Width = 60;
            dataGrid_dailyTask.Columns["Type"].Visible = Properties.Settings.Default.DailyTaskType;

            dataGrid_dailyTask.Columns["Description"].Width = 400;
            dataGrid_dailyTask.Columns["Description"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            dataGrid_dailyTask.Columns["Contents"].Width = 70;

            dataGrid_dailyTask.Columns["Date Added"].Width = 150;
            dataGrid_dailyTask.Columns["Date Added"].Visible = Properties.Settings.Default.DailyDateAdded;

            dataGrid_dailyTask.Columns["Date Changed"].Width = 150;
            dataGrid_dailyTask.Columns["Date Changed"].Visible = Properties.Settings.Default.DailyDateChanged;
            
        }

        private void Populate_MonthlyTasks()
        {

            string command = "select " +
                            "a.id, " +
                            "a.iscompleted as [Status], " +
                            "case " +
                            "when a.taskisimportant = 1 " +
                            "then '*' " +
                            "else '' end as [I], " +
                            "case " +
                            "when a.tasktype = 0 then 'TASK' " +
                            "when a.tasktype = 1 then 'EVENT' " +
                            "when a.tasktype = 2 then 'NOTES' " +
                            "else 'CLOSED' end as [Type], " +
                            "a.description as [Description], " +
                            "sum(case when b.datecompleted is null and b.id is not null then 1 " +
                            "else 0 end) as [Contents], " +
                            "format(a.dateadded, 'dd/MM/yyyy, hh:mm:ss tt') as [Date Added], " +
                            "format(a.datechanged, 'dd/MM/yyyy, hh:mm:ss tt') as [Date Changed] " +
                            "from monthlytasks as a " +
                            "left join monthlytasks as b " +
                            "on a.id = b.previouslayerid " +
                            "where a.layerid = @layerid " +
                            "and a.description like @filter " +
                            "and a.datecompleted is null " +
                            "group by a.id, " +
                            "a.iscompleted, " +
                            "a.description, " +
                            "case " +
                            "when a.tasktype = 0 then 'TASK' " +
                            "when a.tasktype = 1 then 'EVENT' " +
                            "when a.tasktype = 2 then 'NOTES' " +
                            "else 'CLOSED' end, " +
                            "case " +
                            "when a.taskisimportant = 1 " +
                            "then '*' " +
                            "else '' end, " +
                            "a.description, " +
                            "format(a.dateadded, 'dd/MM/yyyy, hh:mm:ss tt'), " +
                            "format(a.datechanged, 'dd/MM/yyyy, hh:mm:ss tt')";


            SqlParameter[] paramters = new SqlParameter[]
            {

                new SqlParameter("@layerid", SqlDbType.Int) { Value = 0},
                new SqlParameter("@filter", SqlDbType.NVarChar) { Value = '%' + txt_dailySearch.Text + '%' }
            };

            dataGrid_monthly.DataSource = db.GenericQueryAction(command, paramters);
            dataGrid_monthly.RowHeadersVisible = false;
            // format grid
            dataGrid_monthly.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;

            dataGrid_monthly.Columns[0].Visible = false;
            dataGrid_monthly.Columns[0].Width = 1;
            dataGrid_monthly.Columns["Status"].Width = 50;

            dataGrid_monthly.Columns["I"].Width = 30;
            dataGrid_monthly.Columns["I"].Visible = Properties.Settings.Default.MonthlyTaskIsImportant;

            dataGrid_monthly.Columns["Type"].Width = 60;
            dataGrid_monthly.Columns["Type"].Visible = Properties.Settings.Default.MonthlyTaskType;

            dataGrid_monthly.Columns["Description"].Width = 400;
            dataGrid_monthly.Columns["Description"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            dataGrid_monthly.Columns["Contents"].Width = 70;

            dataGrid_monthly.Columns["Date Added"].Width = 150;
            dataGrid_monthly.Columns["Date Added"].Visible = Properties.Settings.Default.MonthlyDateAdded;

            dataGrid_monthly.Columns["Date Changed"].Width = 150;
            dataGrid_monthly.Columns["Date Changed"].Visible = Properties.Settings.Default.MonthlyDateChanged;

        }

        private void Populate_FutureTasks()
        {

            string command = "select " +
                            "a.id, " +
                            "a.iscompleted as [Status], " +
                            "case " +
                            "when a.taskisimportant = 1 " +
                            "then '*' " +
                            "else '' end as [I], " +
                            "case " +
                            "when a.tasktype = 0 then 'TASK' " +
                            "when a.tasktype = 1 then 'EVENT' " +
                            "when a.tasktype = 2 then 'NOTES' " +
                            "else 'CLOSED' end as [Type], " +
                            "a.description as [Description], " +
                            "sum(case when b.datecompleted is null and b.id is not null then 1 " +
                            "else 0 end) as [Contents], " +
                            "format(a.dateadded, 'dd/MM/yyyy, hh:mm:ss tt') as [Date Added], " +
                            "format(a.datechanged, 'dd/MM/yyyy, hh:mm:ss tt') as [Date Changed] " +
                            "from futuretasks as a " +
                            "left join futuretasks as b " +
                            "on a.id = b.previouslayerid " +
                            "where a.layerid = @layerid " +
                            "and a.description like @filter " +
                            "and a.datecompleted is null " +
                            "group by a.id, " +
                            "a.iscompleted, " +
                            "a.description, " +
                            "case " +
                            "when a.tasktype = 0 then 'TASK' " +
                            "when a.tasktype = 1 then 'EVENT' " +
                            "when a.tasktype = 2 then 'NOTES' " +
                            "else 'CLOSED' end, " +
                            "case " +
                            "when a.taskisimportant = 1 " +
                            "then '*' " +
                            "else '' end, " +
                            "a.description, " +
                            "format(a.dateadded, 'dd/MM/yyyy, hh:mm:ss tt'), " +
                            "format(a.datechanged, 'dd/MM/yyyy, hh:mm:ss tt')";


            SqlParameter[] paramters = new SqlParameter[]
            {

                new SqlParameter("@layerid", SqlDbType.Int) { Value = 0},
                new SqlParameter("@filter", SqlDbType.NVarChar) { Value = '%' + txt_dailySearch.Text + '%' }
            };

            dataGrid_futureLog.DataSource = db.GenericQueryAction(command, paramters);
            dataGrid_futureLog.RowHeadersVisible = false;
            // format grid
            dataGrid_futureLog.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            dataGrid_futureLog.Columns[0].Visible = false;
            dataGrid_futureLog.Columns[0].Width = 1;
            dataGrid_futureLog.Columns["Status"].Width = 50;
            dataGrid_futureLog.Columns["I"].Width = 30;
            dataGrid_futureLog.Columns["I"].Visible = Properties.Settings.Default.FutureTaskIsImportant;
            dataGrid_futureLog.Columns["Type"].Width = 60;
            dataGrid_futureLog.Columns["Type"].Visible = Properties.Settings.Default.FutureTaskType;
            dataGrid_futureLog.Columns["Description"].Width = 400;
            dataGrid_futureLog.Columns["Description"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGrid_futureLog.Columns["Contents"].Width = 70;
            dataGrid_futureLog.Columns["Date Added"].Width = 150;
            dataGrid_futureLog.Columns["Date Added"].Visible = Properties.Settings.Default.FutureDateAdded;
            dataGrid_futureLog.Columns["Date Changed"].Width = 150;
            dataGrid_futureLog.Columns["Date Changed"].Visible = Properties.Settings.Default.FutureDateChanged;

        }

        private void btn_addFutureLog_Click(object sender, EventArgs e)
        {
            Add_Future();
        }

        private void btn_addMonthlyTask_Click(object sender, EventArgs e)
        {
            Add_Monthly();

        }

        private void dataGrid_dailyTask_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                taskId = JournalTask.ContextMenuHandler(dataGrid_dailyTask, contextMenuStrip1, e);
                if (taskId != 0)
                    title = dataGrid_dailyTask.SelectedRows[0].Cells[4].Value.ToString();

                entryType = JournalTask.EntryType.daily;
                contextMenuStrip1.Hide();

            }

            // right click
            if (e.Button == MouseButtons.Right)
            {
                title = dataGrid_dailyTask.SelectedRows[0].Cells[4].Value.ToString();

                taskId = JournalTask.ContextMenuHandler(dataGrid_dailyTask, contextMenuStrip1, e);
                entryType = JournalTask.EntryType.daily;

                
            }
        }

        private void dataGrid_collection_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Right Click
            if (e.Button == MouseButtons.Right)
            {

                title = dataGrid_notes.SelectedRows[0].Cells[1].Value.ToString();
                // Store id
                taskId = JournalTask.ContextMenuHandler(dataGrid_notes, contextMenuStrip1, e);

                contextMenuStrip1.Items["migrate"].Visible = false;
                entryType = JournalTask.EntryType.notes;
            }
        }


        private void dataGrid_monthly_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                taskId = JournalTask.ContextMenuHandler(dataGrid_monthly, contextMenuStrip1, e);
                if (taskId != 0)
                    title = dataGrid_monthly.SelectedRows[0].Cells[4].Value.ToString();

                entryType = JournalTask.EntryType.monthly;
                contextMenuStrip1.Hide();

            }

            // right click
            if (e.Button == MouseButtons.Right)
            {
                title = dataGrid_monthly.SelectedRows[0].Cells[4].Value.ToString();

                taskId = JournalTask.ContextMenuHandler(dataGrid_monthly, contextMenuStrip1, e);
                entryType = JournalTask.EntryType.monthly;
                
            }
        }

        private void dataGrid_futureLog_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                taskId = JournalTask.ContextMenuHandler(dataGrid_futureLog, contextMenuStrip1, e);
                if (taskId != 0)
                    title = dataGrid_futureLog.SelectedRows[0].Cells[4].Value.ToString();

                entryType = JournalTask.EntryType.future;
                contextMenuStrip1.Hide();

            }

            // right clock
            if (e.Button == MouseButtons.Right)
            {
                title = dataGrid_futureLog.SelectedRows[0].Cells[4].Value.ToString();

                taskId = JournalTask.ContextMenuHandler(dataGrid_futureLog, contextMenuStrip1, e);
                entryType = JournalTask.EntryType.future;
                
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (entryType == JournalTask.EntryType.daily)
            {
                string command = "delete from currenttasks " +
                                 "where id = @ids";

                List<int> ids = JournalTask.GetAllCurrentTasksId(taskId);

                for (int i = 0; i < ids.Count; i++)
                {
                    SqlParameter[] parameter = new SqlParameter[]
                    {
                        new SqlParameter("@ids", SqlDbType.Int) { Value = ids[i]}
                    };

                    db.GenericNonQueryAction(command, parameter);
                }

                RefreshGrid();
            }
            if (entryType == JournalTask.EntryType.monthly)
            {

                string command = "delete from monthlytasks " +
                                  "where id = @ids";

                List<int> ids = JournalTask.GetAllMonthlyTasksId(taskId);

                for (int i = 0; i < ids.Count; i++)
                {
                    SqlParameter[] parameter = new SqlParameter[]
                    {
                        new SqlParameter("@ids", SqlDbType.Int) { Value = ids[i]}
                    };

                    db.GenericNonQueryAction(command, parameter);
                }

                RefreshGrid();
            }
            if (entryType == JournalTask.EntryType.future)
            {

                string command = "delete from futuretasks " +
                                   "where id = @ids";

                List<int> ids = JournalTask.GetAllFutureTasksId(taskId);

                for (int i = 0; i < ids.Count; i++)
                {
                    SqlParameter[] parameter = new SqlParameter[]
                    {
                        new SqlParameter("@ids", SqlDbType.Int) { Value = ids[i]}
                    };

                    db.GenericNonQueryAction(command, parameter);
                }

                RefreshGrid();
            }

            if (entryType == JournalTask.EntryType.notes)
            {
                string command = "delete from notes " +
                                 "where id = @ids";

                List<int> ids = JournalTask.GetAllNoteId(taskId);

                for (int i = 0; i < ids.Count; i++)
                {
                    SqlParameter[] parameter = new SqlParameter[]
                    {
                        new SqlParameter("@ids", SqlDbType.Int) { Value = ids[i]}
                    };
                    
                    db.GenericNonQueryAction(command, parameter);
                }

                RefreshGrid();
            }
        }


        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (entryType == JournalTask.EntryType.daily)
            {
                using (CurrentTaskDescription currentTaskDescription = new CurrentTaskDescription(JournalTask.EntryMode.edit, taskId, 0))
                {
                    currentTaskDescription.OnCurrentTaskSaved += OnSave;
                    currentTaskDescription.ShowDialog();
                }
            }
            
            if (entryType == JournalTask.EntryType.monthly)
            {
                using (MonthlyTaskDescription monthlyTaskDescription = new MonthlyTaskDescription(JournalTask.EntryMode.edit, taskId, 0))
                {
                    monthlyTaskDescription.OnMonthlyTaskSaved += OnSave;
                    monthlyTaskDescription.ShowDialog();
                }
            }

            if (entryType == JournalTask.EntryType.future)
            {
                using (FutureTaskDescription futureTaskDescription = new FutureTaskDescription(JournalTask.EntryMode.edit, taskId, 0))
                {
                    futureTaskDescription.OnFutureTaskSaved += OnSave;
                    futureTaskDescription.ShowDialog();
                }
            }

            if (entryType == JournalTask.EntryType.notes)
            {
                
                using (NotesDescription notesDescription = new NotesDescription( JournalTask.EntryMode.edit, taskId, 0))
                {
                    notesDescription.OnNotesSaved += OnSave;
                    notesDescription.ShowDialog();
                }

            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void OnSave()
        {
            RefreshGrid();
        }

        private void dailyTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            using (Migration migration = new Migration(entryType, JournalTask.EntryType.daily, taskId, title, 0))
            {
                migration.OnMigrated += OnSave;
                migration.ShowDialog();

            }
        }

        private void monthlyTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            using (Migration migration = new Migration(entryType, JournalTask.EntryType.monthly, taskId, title, 0))
            {
                migration.OnMigrated += OnSave;
                migration.ShowDialog();
            }
        }

        private void futureLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Migration migration = new Migration(entryType, JournalTask.EntryType.future, taskId, title, 0))
            {
                migration.OnMigrated += OnSave;
                migration.ShowDialog();
            }
        }

        private void dailyTaskToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
            using (CurrentTaskDescription dailyDescription = new CurrentTaskDescription(JournalTask.EntryMode.migrate, taskId, 0, entryType))
            {
                dailyDescription.OnCurrentTaskSaved += OnSave;
                dailyDescription.ShowDialog();
            }
            
        }

        private void monthlyTaskToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (MonthlyTaskDescription monthlyDescription = new MonthlyTaskDescription(JournalTask.EntryMode.migrate, taskId, 0, entryType))
            {
                monthlyDescription.OnMonthlyTaskSaved += OnSave;
                monthlyDescription.ShowDialog();
            }
        }

        private void futureLogToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (FutureTaskDescription futureDescription = new FutureTaskDescription(JournalTask.EntryMode.migrate, taskId, 0, entryType))
            {
                futureDescription.OnFutureTaskSaved += OnSave;
                futureDescription.ShowDialog();
            }
        }

        private void txt_collectionSearch_TextChanged(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void txt_futureSearch_TextChanged(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void txt_monthlySearch_TextChanged(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void txt_dailySearch_TextChanged(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void dataGrid_collection_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            int colId = (int)dataGrid_notes.SelectedRows[0].Cells[0].Value;
            title = dataGrid_notes.SelectedRows[0].Cells[1].Value.ToString();
            using (NotesContent notes = new NotesContent(colId, 1, title))
            {
                notes.OnRefreshGrid += this.OnSave;
                notes.ShowDialog();
            }
            
        }

        private void dataGrid_futureLog_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Left Click
            int colId = (int)dataGrid_futureLog.SelectedRows[0].Cells[0].Value;
            string title = dataGrid_futureLog.SelectedRows[0].Cells[4].Value.ToString();


            using (FutureTaskContents monthlyTasksContent = new FutureTaskContents(colId, 1, title))
            {
                monthlyTasksContent.OnRefreshGrid += this.OnSave;
                monthlyTasksContent.ShowDialog();
            }

        }

        private void dataGrid_monthly_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Left Click
            int colId = (int) dataGrid_monthly.SelectedRows[0].Cells[0].Value;
            string title = dataGrid_monthly.SelectedRows[0].Cells[4].Value.ToString();


            using (MonthlyTasksContent monthlyTasksContent = new MonthlyTasksContent(colId, 1, title))
            {
                monthlyTasksContent.OnRefreshGrid += this.OnSave;
                monthlyTasksContent.ShowDialog();
            }

        }

        private void dataGrid_dailyTask_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Left Click
            int colId = (int)dataGrid_dailyTask.SelectedRows[0].Cells[0].Value;
            string title = dataGrid_dailyTask.SelectedRows[0].Cells[4].Value.ToString();


            using (CurrentTaskContents currentTasksContent = new CurrentTaskContents(colId, 1, title))
            {
                currentTasksContent.OnRefreshGrid += this.OnSave;
                currentTasksContent.ShowDialog();
            }

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
                txt_dailySearch.Focus();
            if (tabControl1.SelectedIndex == 2)
                txt_monthlySearch.Focus();
            if (tabControl1.SelectedIndex == 3)
                txt_futureSearch.Focus();
            if (tabControl1.SelectedIndex == 4)
                txt_collectionSearch.Focus();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            using (ControlPanel controlPanel = new ControlPanel())
            {
                controlPanel.OnSettingsChanged += this.OnSave;
                controlPanel.ShowDialog();
            }
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.D)
            {
                switch (tabControl1.SelectedIndex)
                {
                    case 1:  Add_Daily();
                        break;
                    case 2: Add_Monthly();
                        break;
                    case 3: Add_Future();
                        break;
                    case 4: Add_Notes();
                        break;
                    default:
                        break;
                }

            }
        }

        private void Add_Daily()
        {
            using (CurrentTaskDescription currentTaskDescription = new CurrentTaskDescription(JournalTask.EntryMode.add, -1, 0))
            {
                currentTaskDescription.OnCurrentTaskSaved += this.OnSave;
                currentTaskDescription.ShowDialog();
            }
        }

        private void Add_Monthly()
        {
            using (MonthlyTaskDescription monthlyTaskDescription = new MonthlyTaskDescription(JournalTask.EntryMode.add, -1, 0))
            {
                monthlyTaskDescription.OnMonthlyTaskSaved += this.OnSave;
                monthlyTaskDescription.ShowDialog();
            }
        }

        private void Add_Future()
        {
            using (FutureTaskDescription monthlyTaskDescription = new FutureTaskDescription(JournalTask.EntryMode.add, -1, 0))
            {
                monthlyTaskDescription.OnFutureTaskSaved += this.OnSave;
                monthlyTaskDescription.ShowDialog();
            }
        }
        
        private void Add_Notes()
        {
            using (NotesDescription notes = new NotesDescription(JournalTask.EntryMode.add, -1, 0))
            {
                notes.OnNotesSaved += this.OnSave;
                notes.ShowDialog();
            }
        }

        private void dataGrid_dailyTask_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            taskId = JournalTask.ContentClickHandler(dataGrid_dailyTask, e);

            if (e.ColumnIndex == 1)
            {

                string command = "update currenttasks " +
                             "set " +
                             "iscompleted = @iscompleted, " +
                             "datecompleted = @completeddate " +
                             "where id = @id";

                List<int> ids = JournalTask.GetAllCurrentTasksId(taskId);

                for (int i = 0; i < ids.Count; i++)
                {
                    SqlParameter[] parameter = new SqlParameter[]
                    {
                            new SqlParameter("@id", SqlDbType.Int) { Value = ids[i]},
                            new SqlParameter("@iscompleted", SqlDbType.Bit) { Value = true},
                            new SqlParameter("@completeddate", SqlDbType.DateTime) { Value = DateTime.Now}
                    };

                    db.GenericNonQueryAction(command, parameter);
                }

                RefreshGrid();
            }
        }

        private void dataGrid_monthly_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            taskId = JournalTask.ContentClickHandler(dataGrid_monthly, e);

            if (e.ColumnIndex == 1)
            {
                string command = "update monthlytasks " +
                             "set " +
                             "iscompleted = @iscompleted, " +
                             "datecompleted = @completeddate " +
                             "where id = @id";

                List<int> ids = JournalTask.GetAllMonthlyTasksId(taskId);

                for (int i = 0; i < ids.Count; i++)
                {
                    SqlParameter[] parameter = new SqlParameter[]
                    {
                            new SqlParameter("@id", SqlDbType.Int) { Value = ids[i]},
                            new SqlParameter("@iscompleted", SqlDbType.Bit) { Value = true},
                            new SqlParameter("@completeddate", SqlDbType.DateTime) { Value = DateTime.Now}
                    };

                    db.GenericNonQueryAction(command, parameter);
                }

                RefreshGrid();
            }
        }

        private void dataGrid_futureLog_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            taskId = JournalTask.ContentClickHandler(dataGrid_futureLog, e);

            if (e.ColumnIndex == 1)
            {
                string command = "update futuretasks " +
                             "set " +
                             "iscompleted = @iscompleted, " +
                             "datecompleted = @completeddate " +
                             "where id = @id";

                List<int> ids = JournalTask.GetAllFutureTasksId(taskId);

                for (int i = 0; i < ids.Count; i++)
                {
                    SqlParameter[] parameter = new SqlParameter[]
                    {
                            new SqlParameter("@id", SqlDbType.Int) { Value = ids[i]},
                            new SqlParameter("@iscompleted", SqlDbType.Bit) { Value = true},
                            new SqlParameter("@completeddate", SqlDbType.DateTime) { Value = DateTime.Now}
                    };

                    db.GenericNonQueryAction(command, parameter);
                }

                RefreshGrid();
            }
        }

        private void dailyTaskToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            using (History history = new History(JournalTask.EntryType.daily, "Daily Tasks"))
            {
                history.OnTaskUndone += OnSave;
                history.ShowDialog();
            }
        }

        private void monthlyTaskToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            using (History history = new History(JournalTask.EntryType.monthly, "Monthly Tasks"))
            {
                history.OnTaskUndone += OnSave;
                history.ShowDialog();
            }
        }

        private void futureLogToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            using (History history = new History(JournalTask.EntryType.future, "Future Logs"))
            {
                history.OnTaskUndone += OnSave;
                history.ShowDialog();
            }
        }

        private void btn_viewHabit_Click(object sender, EventArgs e)
        {
            using (HabitContent habitContent = new HabitContent())
            {
                habitContent.OnHabitRegistered += OnSave;
                habitContent.ShowDialog();
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void dataGrid_tracker_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            taskId = JournalTask.ContentClickHandler(dataGrid_tracker, e);

            // MessageBox.Show(dataGrid_tracker.Columns[e.ColumnIndex].HeaderText);
            string headerText = dataGrid_tracker.Columns[e.ColumnIndex].HeaderText;
            DateTime dateTime = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, int.Parse(headerText));

            string command = "select iscompleted from tracker " +
                             "where habitid = @id " +
                             "and habitdate = @habitdate";

            SqlParameter[] parameter = new SqlParameter[]
            {
                new SqlParameter("@id", SqlDbType.Int) { Value = taskId },
                new SqlParameter("@habitdate", SqlDbType.DateTime) { Value = dateTime}
            };

            DataTable dataTable = db.GenericQueryAction(command, parameter);

            if (dataTable.Rows.Count > 0)
            {
                bool visibility = dataTable.AsEnumerable().ToList()[0].Field<bool>("iscompleted");

                command = "update tracker " +
                          "set " +
                          "iscompleted = @iscompleted " +
                          "where habitid = @id " +
                          "and habitdate = @habitdate";

                parameter = new SqlParameter[]
                {
                    new SqlParameter("@id", SqlDbType.Int) { Value = taskId },
                    new SqlParameter("@habitdate", SqlDbType.DateTime) { Value = dateTime},
                    new SqlParameter("@iscompleted", SqlDbType.Bit) { Value = !visibility }
                };

                db.GenericNonQueryAction(command, parameter);

            }
            else
            {
                command = "insert into tracker " +
                          "(habitid, habitdate, iscompleted) " +
                          "values" +
                          "(@habitid, @habitdate, @iscompleted)";

                parameter = new SqlParameter[]
                {
                    new SqlParameter("@habitid", SqlDbType.Int) { Value = taskId },
                    new SqlParameter("@iscompleted", SqlDbType.Bit) { Value = true },
                    new SqlParameter("@habitdate", SqlDbType.Date) { Value = dateTime}
                };

                db.GenericNonQueryAction(command, parameter);
            }

            CheckAllHabits();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (WebForm web = new WebForm())
            {
                web.ShowDialog();
            }
        }

        private void quickSearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (WebForm web = new WebForm(title))
            {
                web.ShowDialog();
            }
        }
    }
}