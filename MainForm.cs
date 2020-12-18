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

            dateTimePicker.Value = DateTime.Today;
        }


        private void btn_refresh_Click(object sender, EventArgs e)
        {

            RefreshGrid();
        }

        public void RefreshGrid()
        {
            Populate_futureLog();
            //Populate_monthly();
            //Populate_index();
            Populate_Notes();
            Populate_CurrentTasks();
            Populate_MonthlyTasks();
        }

        private void btn_addDailyTask_Click(object sender, EventArgs e)
        {
            Add_Daily();
        }

        private void btn_addCollection_Click(object sender, EventArgs e)
        {
            Add_Notes();
        }

        public void Populate_index()
        {
            string commandString = "select Daily.Entry as Entry, " + //daily task
                                   "sum(Daily.Count) as Count, " +
                                   "sum(Daily.Tasks) as Tasks, " +
                                   "sum(Daily.Events) as Events, " +
                                   "sum(Daily.Notes) as Notes, " +
                                   "sum(Daily.Closed) as Closed " +
                                   "from ( " +
                                   "select 'Daily Tasks' as Entry, " +
                                   "count(*) as Count, " +
                                   "0 as Tasks," +
                                   "0 as Events, " +
                                   "0 as Notes, " +
                                   "0 as Closed " +
                                   "from dailymain as m " +
                                   "inner join dailydetail as d " +
                                   "on m.taskid = d.maintaskforeignkey " +
                                   "where m.taskdate in (select taskdate " +
                                   "from dailymain " +
                                   "where taskdate = @taskdate) " +
                                   "union all " +
                                   "select 'Daily Tasks' as Entry, " +
                                   "0 as Count, " +
                                   "count(*) as Tasks, " +
                                   "0 as Events, " +
                                   "0 as Notes, " +
                                   "0 as Closed " +
                                   "from dailymain as m " +
                                   "inner join dailydetail as d " +
                                   "on m.taskid = d.maintaskforeignkey " +
                                   "where m.taskdate in (select taskdate " +
                                   "from dailymain " +
                                   "where taskdate = @taskdate " +
                                   "and d.tasktype = 0) " +
                                   "union all " +
                                   "select 'Daily Tasks' as Entry, " +
                                   "0 as Count, " +
                                   "0 as Tasks, " +
                                   "count(*) as Events, " +
                                   "0 as Notes, " +
                                   "0 as Closed " +
                                   "from dailymain as m " +
                                   "inner join dailydetail as d " +
                                   "on m.taskid = d.maintaskforeignkey " +
                                   "where m.taskdate in (select taskdate " +
                                   "from dailymain " +
                                   "where taskdate = @taskdate " +
                                   "and d.tasktype = 1) " +
                                   "union all " +
                                   "select 'Daily Tasks' as Entry, " +
                                   "0 as Count, " +
                                   "0 as Tasks, " +
                                   "0 as Events, " +
                                   "count(*) as Notes, " +
                                   "0 as Closed " +
                                   "from dailymain as m " +
                                   "inner join dailydetail as d " +
                                   "on m.taskid = d.maintaskforeignkey " +
                                   "where m.taskdate in (select taskdate " +
                                   "from dailymain " +
                                   "where taskdate = @taskdate " +
                                   "and d.tasktype = 2) " +
                                   "union all " +
                                   "select 'Daily Tasks' as Entry, " +
                                   "0 as Count, " +
                                   "0 as Tasks," +
                                   "0 as Events, " +
                                   "0 as Notes, " +
                                   "count(*) as Closed " +
                                   "from dailymain as m " +
                                   "inner join dailydetail as d " +
                                   "on m.taskid = d.maintaskforeignkey " +
                                   "where m.taskdate in (select taskdate " +
                                   "from dailymain " +
                                   "where taskdate = @taskdate " +
                                   "and d.tasktype = 3) " +
                                   ") as Daily " +
                                   "group by Daily.Entry " +
                                   "union all " + // monthly tasks
                                   "select Monthly.Entry as Entry, " +
                                   "sum(Monthly.Count) as Count, " +
                                   "sum(Monthly.Tasks) as Tasks, " +
                                   "sum(Monthly.Events) as Events, " +
                                   "sum(Monthly.Notes) as Notes, " +
                                   "sum(Monthly.Closed) as Closed " +
                                   "from ( " +
                                   "select 'Monthly Tasks' as Entry, " +
                                   "count(*) as Count, " +
                                   "0 as Tasks," +
                                   "0 as Events, " +
                                   "0 as Notes, " +
                                   "0 as Closed " +
                                   "from monthlymain as m " +
                                   "inner join monthlydetail as d " +
                                   "on m.taskid = d.maintaskforeignkey " +
                                   "where m.taskdate in (select taskdate " +
                                   "from monthlymain " +
                                   "where taskdate >= @monthlytaskdate) " +
                                   "and taskdate <= @monthlytaskdateEnd " +
                                   "union all " +
                                   "select 'Monthly Tasks' as Entry, " +
                                   "0 as Count, " +
                                   "count(*) as Tasks, " +
                                   "0 as Events, " +
                                   "0 as Notes, " +
                                   "0 as Closed " +
                                   "from monthlymain as m " +
                                   "inner join monthlydetail as d " +
                                   "on m.taskid = d.maintaskforeignkey " +
                                   "where m.taskdate in (select taskdate " +
                                   "from monthlymain " +
                                   "where taskdate >= @monthlytaskdate " +
                                   "and taskdate <= @monthlytaskdateEnd " +
                                   "and d.tasktype = 0) " +
                                   "union all " +
                                   "select 'Monthly Tasks' as Entry, " +
                                   "0 as Count, " +
                                   "0 as Tasks, " +
                                   "count(*) as Events, " +
                                   "0 as Notes, " +
                                   "0 as Closed " +
                                   "from monthlymain as m " +
                                   "inner join monthlydetail as d " +
                                   "on m.taskid = d.maintaskforeignkey " +
                                   "where m.taskdate in (select taskdate " +
                                   "from monthlymain " +
                                   "where taskdate >= @monthlytaskdate " +
                                   "and taskdate <= @monthlytaskdateEnd " +
                                   "and d.tasktype = 1) " +
                                   "union all " +
                                   "select 'Monthly Tasks' as Entry, " +
                                   "0 as Count, " +
                                   "0 as Tasks, " +
                                   "0 as Events, " +
                                   "count(*) as Notes, " +
                                   "0 as Closed " +
                                   "from monthlymain as m " +
                                   "inner join monthlydetail as d " +
                                   "on m.taskid = d.maintaskforeignkey " +
                                   "where m.taskdate in (select taskdate " +
                                   "from monthlymain " +
                                   "where taskdate >= @monthlytaskdate " +
                                   "and taskdate <= @monthlytaskdateEnd " +
                                   "and d.tasktype = 2) " +
                                   "union all " +
                                   "select 'Monthly Tasks' as Entry, " +
                                   "0 as Count, " +
                                   "0 as Tasks," +
                                   "0 as Events, " +
                                   "0 as Notes, " +
                                   "count(*) as Closed " +
                                   "from monthlymain as m " +
                                   "inner join monthlydetail as d " +
                                   "on m.taskid = d.maintaskforeignkey " +
                                   "where m.taskdate in (select taskdate " +
                                   "from monthlymain " +
                                   "where taskdate >= @monthlytaskdate " +
                                   "and taskdate <= @monthlytaskdateEnd " +
                                   "and d.tasktype = 3) " +
                                   ") as Monthly " +
                                   "group by Monthly.Entry " +
                                   "union all " + //futurelog
                                   "select Future.Entry as Entry, " +
                                   "sum(Future.Count) as Count, " +
                                   "sum(Future.Tasks) as Tasks, " +
                                   "sum(Future.Events) as Events, " +
                                   "sum(Future.Notes) as Notes, " +
                                   "sum(Future.Closed) as Closed " +
                                   "from ( " +
                                   "select 'Future Tasks' as Entry, " +
                                   "count(*) as Count, " +
                                   "0 as Tasks," +
                                   "0 as Events, " +
                                   "0 as Notes, " +
                                   "0 as Closed " +
                                   "from futuremain as m " +
                                   "inner join futuredetail as d " +
                                   "on m.taskid = d.maintaskforeignkey " +
                                   "where m.taskdate in (select taskdate " +
                                   "from futuremain " +
                                   "where taskdate >= @futureTaskdate " +
                                   "and taskdate <= @futureTaskdateEnd) " +
                                   "union all " +
                                   "select 'Future Tasks' as Entry, " +
                                   "0 as Count, " +
                                   "count(*) as Tasks, " +
                                   "0 as Events, " +
                                   "0 as Notes, " +
                                   "0 as Closed " +
                                   "from futuremain as m " +
                                   "inner join futuredetail as d " +
                                   "on m.taskid = d.maintaskforeignkey " +
                                   "where m.taskdate in (select taskdate " +
                                   "from futuremain " +
                                   "where taskdate >= @futureTaskdate " +
                                   "and taskdate <= @futureTaskdateEnd " +
                                   "and d.tasktype = 0) " +
                                   "union all " +
                                   "select 'Future Tasks' as Entry, " +
                                   "0 as Count, " +
                                   "0 as Tasks, " +
                                   "count(*) as Events, " +
                                   "0 as Notes, " +
                                   "0 as Closed " +
                                   "from futuremain as m " +
                                   "inner join futuredetail as d " +
                                   "on m.taskid = d.maintaskforeignkey " +
                                   "where m.taskdate in (select taskdate " +
                                   "from futuremain " +
                                   "where taskdate >= @futureTaskdate " +
                                   "and taskdate <= @futureTaskdateEnd " +
                                   "and d.tasktype = 1) " +
                                   "union all " +
                                   "select 'Future Tasks' as Entry, " +
                                   "0 as Count, " +
                                   "0 as Tasks, " +
                                   "0 as Events, " +
                                   "count(*) as Notes, " +
                                   "0 as Closed " +
                                   "from futuremain as m " +
                                   "inner join futuredetail as d " +
                                   "on m.taskid = d.maintaskforeignkey " +
                                   "where m.taskdate in (select taskdate " +
                                   "from futuremain " +
                                   "where taskdate >= @futureTaskdate " +
                                   "and taskdate <= @futureTaskdateEnd " +
                                   "and d.tasktype = 2) " +
                                   "union all " +
                                   "select 'Future Tasks' as Entry, " +
                                   "0 as Count, " +
                                   "0 as Tasks," +
                                   "0 as Events, " +
                                   "0 as Notes, " +
                                   "count(*) as Closed " +
                                   "from futuremain as m " +
                                   "inner join futuredetail as d " +
                                   "on m.taskid = d.maintaskforeignkey " +
                                   "where m.taskdate in (select taskdate " +
                                   "from futuremain " +
                                   "where taskdate >= @futureTaskdate " +
                                   "and taskdate <= @futureTaskdateEnd " +
                                   "and d.tasktype = 3) " +
                                   ") as Future " +
                                   "group by Future.Entry "; 

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@taskdate", SqlDbType.Date) { Value = dateTimePicker.Value },
                new SqlParameter("@monthlytaskdate", SqlDbType.Date) { Value = new DateTime(dateTimePicker.Value.Year, dateTimePicker.Value.Month, 1) },
                new SqlParameter("@monthlytaskdateEnd", SqlDbType.Date) { Value = new DateTime(dateTimePicker.Value.Year, dateTimePicker.Value.Month,
                                        DateTime.DaysInMonth(dateTimePicker.Value.Year, dateTimePicker.Value.Month)) },
                new SqlParameter("@futureTaskdate", SqlDbType.Date) { Value = new DateTime(dateTimePicker.Value.Year, dateTimePicker.Value.Month, 1) },
                new SqlParameter("@futureTaskdateEnd", SqlDbType.Date) { Value = new DateTime(dateTimePicker.Value.Year, dateTimePicker.Value.Month, 1).AddMonths(6) }
            };

            dataGrid_index.DataSource = db.GenericQueryAction(commandString, parameters);
            dataGrid_index.Columns["Entry"].Width = 120;
            dataGrid_index.Columns["Count"].Width = 70;
            dataGrid_index.Columns["Tasks"].Width = 70;
            dataGrid_index.Columns["Events"].Width = 70;
            dataGrid_index.Columns["Notes"].Width = 70;
            dataGrid_index.Columns["Closed"].Width = 70;

        }

        public void Populate_monthly()
        {

            string command = "select " +
                                   "a.taskid, " +
                                   "format(a.taskdate, 'yyyy MMMM') as [Date], " +
                                   "a.description as Description, " +
                                   "count(b.taskid) as [Contents] " +
                                   "from monthlymain as a " +
                                   "left join monthlydetail as b " +
                                   "on a.taskid = b.maintaskforeignkey " +
                                   "where a.taskdate >= @taskdate " +
                                   "and (a.description like @filter " +
                                   "or format(a.taskdate, 'yyyy MMMM') like @filter) " +
                                   "group by a.taskid, format(a.taskdate, 'yyyy MMMM') ,a.description " +
                                   "order by format(a.taskdate, 'yyyy MMMM'), a.taskid";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@taskdate", SqlDbType.Date) { Value = dateTimePicker.Value },
                new SqlParameter("@filter", SqlDbType.NVarChar) { Value = '%' + txt_monthlySearch.Text + '%' }
            };


            dataGrid_monthly.DataSource = db.GenericQueryAction(command, parameters);

            dataGrid_monthly.Columns[0].Visible = false;
            dataGrid_monthly.Columns[0].Width = 1;
            dataGrid_monthly.Columns["Date"].Width = 90;
            dataGrid_monthly.Columns["Description"].Width = 310;
            dataGrid_monthly.Columns["Contents"].Width = 70;
        }

        public void Populate_futureLog()
        {
            string command = "select " +
                                   "a.taskid, " +
                                   "format(a.taskdate, 'yyyy MMMM') as [Date], " +
                                   "a.description as Description, " +
                                   "count(b.taskid) as [Contents] " +
                                   "from futuremain as a " +
                                   "left join futuredetail as b " +
                                   "on a.taskid = b.maintaskforeignkey " +
                                   "where a.taskdate >= @taskdate " +
                                   "and (a.description like @filter " +
                                   "or format(a.taskdate, 'yyyy MMMM') like @filter) " +
                                   "group by a.taskid, format(a.taskdate, 'yyyy MMMM') ,a.description " +
                                   "order by format(a.taskdate, 'yyyy MMMM'), a.taskid";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@taskdate", SqlDbType.Date) { Value = dateTimePicker.Value },
                new SqlParameter("@filter", SqlDbType.NVarChar) { Value = '%' + txt_futureSearch.Text + '%' }
            };


            dataGrid_futureLog.DataSource = db.GenericQueryAction(command, parameters);

            dataGrid_futureLog.Columns[0].Visible = false;
            dataGrid_futureLog.Columns[0].Width = 1;
            dataGrid_futureLog.Columns["Date"].Width = 90;
            dataGrid_futureLog.Columns["Description"].Width = 310;
            dataGrid_futureLog.Columns["Contents"].Width = 70;
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

            // format grid
            dataGrid_notes.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;

            dataGrid_notes.Columns[0].Visible = false;
            dataGrid_notes.Columns[0].Width = 1;
            dataGrid_notes.Columns["Description"].Width = 400;
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
                            "count(b.id) as [Contents], " +
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
                            "format(a.datechanged, 'dd/MM/yyyy, hh:mm:ss tt')";


            SqlParameter[] paramters = new SqlParameter[]
            {

                new SqlParameter("@layerid", SqlDbType.Int) { Value = 0},
                new SqlParameter("@filter", SqlDbType.NVarChar) { Value = '%' + txt_dailySearch.Text + '%' }
            };

            dataGrid_dailyTask.DataSource = db.GenericQueryAction(command, paramters);

            // format grid
            dataGrid_dailyTask.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;

            dataGrid_dailyTask.Columns[0].Visible = false;
            dataGrid_dailyTask.Columns[0].Width = 1;
            dataGrid_dailyTask.Columns["Status"].Width = 50;

            dataGrid_dailyTask.Columns["I"].Width = 30;
            dataGrid_dailyTask.Columns["I"].Visible = Properties.Settings.Default.DailyTaskIsImportant;

            dataGrid_dailyTask.Columns["Type"].Width = 60;
            dataGrid_dailyTask.Columns["Type"].Visible = Properties.Settings.Default.DailyTaskType;

            dataGrid_dailyTask.Columns["Description"].Width = 350;
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
                            "count(b.id) as [Contents], " +
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

            // format grid
            dataGrid_monthly.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;

            dataGrid_monthly.Columns[0].Visible = false;
            dataGrid_monthly.Columns[0].Width = 1;
            dataGrid_monthly.Columns["Status"].Width = 50;

            dataGrid_monthly.Columns["I"].Width = 30;
            dataGrid_monthly.Columns["I"].Visible = Properties.Settings.Default.MonthlyTaskIsImportant;

            dataGrid_monthly.Columns["Type"].Width = 60;
            dataGrid_monthly.Columns["Type"].Visible = Properties.Settings.Default.MonthlyTaskType;

            dataGrid_monthly.Columns["Description"].Width = 350;
            dataGrid_monthly.Columns["Description"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            dataGrid_monthly.Columns["Contents"].Width = 70;

            dataGrid_monthly.Columns["Date Added"].Width = 150;
            dataGrid_monthly.Columns["Date Added"].Visible = Properties.Settings.Default.MonthlyDateAdded;

            dataGrid_monthly.Columns["Date Changed"].Width = 150;
            dataGrid_monthly.Columns["Date Changed"].Visible = Properties.Settings.Default.MonthlyDateChanged;

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
                if (e.ColumnIndex == 1)
                {
                    taskId = JournalTask.ContextMenuHandler(dataGrid_dailyTask, contextMenuStrip1, e);
                    contextMenuStrip1.Hide();

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

            // right click
            if (e.Button == MouseButtons.Right)
            {
                title = dataGrid_dailyTask.SelectedRows[0].Cells[2].Value.ToString();

                taskId = JournalTask.ContextMenuHandler(dataGrid_dailyTask, contextMenuStrip1, e);
                entryType = JournalTask.EntryType.daily;

                
            }
        }

        private void dataGrid_collection_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Right Click
            if (e.Button == MouseButtons.Right)
            {
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
                if (e.ColumnIndex == 1)
                {
                    taskId = JournalTask.ContextMenuHandler(dataGrid_monthly, contextMenuStrip1, e);
                    contextMenuStrip1.Hide();

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


            // right click
            if (e.Button == MouseButtons.Right)
            {
                title = dataGrid_monthly.SelectedRows[0].Cells[2].Value.ToString();

                taskId = JournalTask.ContextMenuHandler(dataGrid_monthly, contextMenuStrip1, e);
                entryType = JournalTask.EntryType.monthly;
                
            }
        }

        private void dataGrid_futureLog_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            

            // right clock
            if (e.Button == MouseButtons.Right)
            {
                title = dataGrid_futureLog.SelectedRows[0].Cells[2].Value.ToString();

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

                SqlParameter[] mainParameters = new SqlParameter[]
                {
                    new SqlParameter("@taskId", SqlDbType.Int) { Value = taskId }

                };

                string mainString = "delete from futuremain " +
                                       "where taskid = @taskId";

                db.GenericNonQueryAction(mainString, mainParameters);

                SqlParameter[] detailParameters = new SqlParameter[]
                {
                    new SqlParameter("@taskId", SqlDbType.Int) { Value = taskId }

                };

                string detailString = "delete from futuredetail " +
                                "where maintaskforeignkey = @taskId";

                db.GenericNonQueryAction(detailString, detailParameters);

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
                using (MonthlyTaskDescription currentTaskDescription = new MonthlyTaskDescription(JournalTask.EntryMode.edit, taskId, 0))
                {
                    currentTaskDescription.OnMonthlyTaskSaved += OnSave;
                    currentTaskDescription.ShowDialog();
                }
            }

            if (entryType == JournalTask.EntryType.future)
            {
                using (FutureDescription futureDescription = new FutureDescription(JournalTask.EntryMode.edit, taskId))
                {
                    futureDescription.OnFutureMainSave += this.OnSave;
                    futureDescription.ShowDialog();
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

        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            JournalTask.currentDay = dateTimePicker;
            RefreshGrid();
        }

        private void OnSave()
        {
            RefreshGrid();
        }

        private void dailyTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            using (Migration migration = new Migration(entryType, JournalTask.EntryType.daily, taskId, _title:title))
            {
                migration.OnMigrated += OnSave;
                migration.ShowDialog();

            }
        }

        private void monthlyTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            using (Migration migration = new Migration(entryType, JournalTask.EntryType.monthly, taskId, _title:title))
            {
                migration.OnMigrated += OnSave;
                migration.ShowDialog();
            }
        }

        private void futureLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Migration migration = new Migration(entryType, JournalTask.EntryType.future, taskId, _title:title))
            {
                migration.OnMigrated += OnSave;
                migration.ShowDialog();
            }
        }

        private void dailyTaskToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            /*
            using (DailyDescription dailyDescription = new DailyDescription(JournalTask.EntryMode.migrate_main, taskId, _entryType: entryType))
            {
                dailyDescription.OnDailyMainSave += OnSave;
                dailyDescription.ShowDialog();
            }
            */
        }

        private void monthlyTaskToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (MonthlyDescription dailyDescription = new MonthlyDescription(JournalTask.EntryMode.migrate_main, taskId, _entryType: entryType))
            {
                dailyDescription.OnMonthlyMainSave += OnSave;
                dailyDescription.ShowDialog();
            }
        }

        private void futureLogToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (FutureDescription dailyDescription = new FutureDescription(JournalTask.EntryMode.migrate_main, taskId, _entryType: entryType))
            {
                dailyDescription.OnFutureMainSave += OnSave;
                dailyDescription.ShowDialog();
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
            string title = dataGrid_notes.SelectedRows[0].Cells[1].Value.ToString();
            using (NotesContent notes = new NotesContent(colId, 1, title))
            {
                notes.OnRefreshGrid += this.OnSave;
                notes.ShowDialog();
            }
            
        }

        private void dataGrid_futureLog_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // left click

            int colId = (int)dataGrid_futureLog.SelectedRows[0].Cells[0].Value;
            string title = dataGrid_futureLog.SelectedRows[0].Cells[2].Value.ToString();

            using (FutureContent content = new FutureContent(colId, title))
            {
                content.OnRefreshGrid += this.OnSave;
                content.ShowDialog();
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

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (Maintenance maintenance = new Maintenance())
            {
                maintenance.ShowDialog();
            }
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
            using (FutureDescription futureDescription = new FutureDescription(JournalTask.EntryMode.add))
            {
                futureDescription.OnFutureMainSave += this.OnSave;
                futureDescription.ShowDialog();
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

    }
}