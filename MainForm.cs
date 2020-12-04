using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace BulletJournal
{

    //test commit

    public partial class MainForm : Form
    {
        DBTools db;
        JournalTask.EntryType entryType;

        int taskId;

        public MainForm()
        {
            InitializeComponent();
            
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            
            string cn = Properties.Settings.Default.DatabaseConnectionString;
            db = new DBTools(cn);

            RefreshGrid();

            dateTimePicker.Value = DateTime.Today;
        }


        private void btn_refresh_Click(object sender, EventArgs e)
        {

            RefreshGrid();
        }

        public void RefreshGrid()
        {
            Populate_Collection();
            Populate_dailyTask();
            Populate_futureLog();
            Populate_monthly();
            Populate_index();
        }

        private void btn_addDailyTask_Click(object sender, EventArgs e)
        {
            using (DailyDescription category = new DailyDescription(JournalTask.EntryMode.add))
            {
                category.OnDailyMainSave += this.OnSave;
                category.ShowDialog();
            }
        }

        private void btn_addCollection_Click(object sender, EventArgs e)
        {
            using (CollectionDescription category = new CollectionDescription(JournalTask.EntryMode.add))
            {
                category.OnCategorySaved += this.OnSave;
                category.ShowDialog();
            }
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

        public void Populate_dailyTask()
        {
            string command = "select " +
                                   "a.taskid, " +
                                   "format(a.taskdate, 'dd/MM/yyyy') as [Date], " +
                                   "a.description as Description, " +
                                   "count(b.taskid) as [Contents] " +
                                   "from dailymain as a " +
                                   "left join dailydetail as b " +
                                   "on a.taskid = b.maintaskforeignkey " +
                                   "group by a.taskid, format(a.taskdate, 'dd/MM/yyyy') ,a.description";

            SqlParameter[] parameters = new SqlParameter[]
            {
            };


            dataGrid_dailyTask.DataSource = db.GenericQueryAction(command, parameters);

            dataGrid_dailyTask.Columns[0].Visible = false;
            dataGrid_dailyTask.Columns[0].Width = 1;
            dataGrid_dailyTask.Columns["Date"].Width = 70;
            dataGrid_dailyTask.Columns["Description"].Width = 332;
            dataGrid_dailyTask.Columns["Contents"].Width = 70;
        }

        public void Populate_futureLog()
        {
            string commandString = "select m.taskid, " +
                                          "cast(datename(year, m.taskdate) as nvarchar(4)) + ' - ' + " +
                                          "datename(month, m.taskdate)" +
                                          " as [Date], " +
                                          "case when d.taskisimportant = 1 " +
                                          "then '*' else '' end as [I], " +
                                          "case " +
                                          "when d.tasktype = 0 then 'TASK' " +
                                          "when d.tasktype = 1 then 'EVENT' " +
                                          "when d.tasktype = 2 then 'NOTES'" +
                                          "else 'CLOSED' end as [Type], " +
                                          "d.taskdescription as [Description]" +
                                   "from futuremain as m " +
                                   "inner join futuredetail as d " +
                                   "on m.taskid = d.maintaskforeignkey " +
                                   "where m.taskdate >= @taskdatestart " +
                                   "and m.taskdate <= @taskdateend " +
                                   "order by m.taskdate";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@taskdatestart", SqlDbType.Date) { Value = new DateTime(dateTimePicker.Value.Year, dateTimePicker.Value.Month, 1) },
                new SqlParameter("@taskdateend", SqlDbType.Date) { Value = new DateTime(dateTimePicker.Value.Year, dateTimePicker.Value.Month, 1).AddMonths(6) }

            };

            dataGrid_futureLog.DataSource = db.GenericQueryAction(commandString, parameters);
            dataGrid_futureLog.Columns[0].Visible = false;
            dataGrid_futureLog.Columns[0].Width = 1;
            dataGrid_futureLog.Columns["Date"].Width = 100;
            dataGrid_futureLog.Columns["I"].Width = 20;
            dataGrid_futureLog.Columns["Type"].Width = 70;
            dataGrid_futureLog.Columns["Description"].Width = 280;
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
                                   "group by a.taskid, format(a.taskdate, 'yyyy MMMM') ,a.description";

            SqlParameter[] parameters = new SqlParameter[]
            {
            };


            dataGrid_monthly.DataSource = db.GenericQueryAction(command, parameters);

            dataGrid_monthly.Columns[0].Visible = false;
            dataGrid_monthly.Columns[0].Width = 1;
            dataGrid_monthly.Columns["Date"].Width = 90;
            dataGrid_monthly.Columns["Description"].Width = 310;
            dataGrid_monthly.Columns["Contents"].Width = 70;
        }

        public void Populate_Collection()
        {
            string commandString = "select " +
                                   "a.collectionid, " +
                                   "a.collectionname as Category, " +
                                   "count(b.collectionid) as [Contents] " +
                                   "from collectionmain as a " +
                                   "left join collectiondetail as b " +
                                   "on a.collectionid = b.maintaskforeignkey " +
                                   "group by a.collectionid, a.collectionname";

            SqlParameter[] parameters = new SqlParameter[]
            {
            };


            dataGrid_collection.DataSource = db.GenericQueryAction(commandString, parameters);

            dataGrid_collection.Columns[0].Visible = false;
            dataGrid_collection.Columns[0].Width = 1;
            dataGrid_collection.Columns["Category"].Width = 400;
            dataGrid_collection.Columns["Contents"].Width = 70;
        }

        private void btn_addFutureLog_Click(object sender, EventArgs e)
        {
            using (FutureLog addFutureLog = new FutureLog(this))
            {
                addFutureLog.ShowDialog();
            }
        }

        private void btn_addMonthlyTask_Click(object sender, EventArgs e)
        {
            using (MonthlyDescription monthlyDescription = new MonthlyDescription(JournalTask.EntryMode.add))
            {
                monthlyDescription.OnMonthlyMainSave += this.OnSave;
                monthlyDescription.ShowDialog();
            }

        }

        private void dataGrid_dailyTask_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Left Click
            if (e.Button == MouseButtons.Left)
            {

                int colId = (int)dataGrid_dailyTask.SelectedRows[0].Cells[0].Value;

                using (DailyContent content = new DailyContent(colId))
                {
                    content.OnRefreshGrid += this.OnSave;
                    content.ShowDialog();
                }
            }

            // right click
            if (e.Button == MouseButtons.Right)
            {
                taskId = JournalTask.ContextMenuHandler(dataGrid_dailyTask, contextMenuStrip1, e);
                entryType = JournalTask.EntryType.daily;

                contextMenuStrip1.Items["migrate"].Visible = false;
            }
        }

        private void dataGrid_collection_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Left Click
            if (e.Button == MouseButtons.Left)
            {

                int colId = (int)dataGrid_collection.SelectedRows[0].Cells[0].Value;

                using (CollectionContent content = new CollectionContent(colId))
                {
                    content.OnRefreshGrid += this.OnSave;
                    content.ShowDialog();
                }
            }

            // Right Click
            if (e.Button == MouseButtons.Right)
            {
                // Store id
                taskId = JournalTask.ContextMenuHandler(dataGrid_collection, contextMenuStrip1, e);

                contextMenuStrip1.Items["migrate"].Visible = false;
                entryType = JournalTask.EntryType.collection;
            }
        }


        private void dataGrid_monthly_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            // left click
            if (e.Button == MouseButtons.Left)
            {

                int colId = (int)dataGrid_monthly.SelectedRows[0].Cells[0].Value;

                using (MonthlyContent content = new MonthlyContent(colId))
                {
                    content.OnRefreshGrid += this.OnSave;
                    content.ShowDialog();
                }
            }

            // right click
            if (e.Button == MouseButtons.Right)
            {
                
                taskId = JournalTask.ContextMenuHandler(dataGrid_monthly, contextMenuStrip1, e);
                entryType = JournalTask.EntryType.monthly;
                contextMenuStrip1.Items["migrate"].Visible = false;
            }
        }

        private void dataGrid_futureLog_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                taskId = JournalTask.ContextMenuHandler(dataGrid_futureLog, contextMenuStrip1, e);
                entryType = JournalTask.EntryType.future;
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (entryType == JournalTask.EntryType.daily)
            {

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@taskId", SqlDbType.Int) { Value = taskId }

                };

                string commandString = "delete from dailymain " +
                                       "where taskid = @taskId";

                db.GenericNonQueryAction(commandString, parameters);

                parameters = new SqlParameter[]
                {
                    new SqlParameter("@taskId", SqlDbType.Int) { Value = taskId }

                };

                commandString = "delete from dailydetail " +
                                "where maintaskforeignkey = @taskId";

                db.GenericNonQueryAction(commandString, parameters);

                RefreshGrid();
            }
            if (entryType == JournalTask.EntryType.monthly)
            {

                SqlParameter[] mainParameters = new SqlParameter[]
                {
                    new SqlParameter("@taskId", SqlDbType.Int) { Value = taskId }

                };

                string mainString = "delete from monthlymain " +
                                       "where taskid = @taskId";

                db.GenericNonQueryAction(mainString, mainParameters);

                SqlParameter[] detailParameters = new SqlParameter[]
                {
                    new SqlParameter("@taskId", SqlDbType.Int) { Value = taskId }

                };

                string detailString = "delete from monthlydetail " +
                                "where maintaskforeignkey = @taskId";

                db.GenericNonQueryAction(detailString, detailParameters);

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

                Populate_futureLog();
                Populate_index();
            }

            if (entryType == JournalTask.EntryType.collection)
            {
                string mainCommand = "delete from collectionmain " +
                                       "where collectionid = @taskId";

                SqlParameter[] mainparameters = new SqlParameter[]
                {
                    new SqlParameter("@taskId", SqlDbType.Int) { Value = taskId }

                };

                db.GenericNonQueryAction(mainCommand, mainparameters);

                string detailCommand = "delete from collectiondetail " +
                                       "where maintaskforeignkey = @taskId";

                SqlParameter[] detailParameters = new SqlParameter[]
                {
                    new SqlParameter("@taskId", SqlDbType.Int) { Value = taskId }

                };

                db.GenericNonQueryAction(detailCommand, detailParameters);

                RefreshGrid();
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (entryType == JournalTask.EntryType.daily)
            {
                using (DailyDescription dailyTask = new DailyDescription(JournalTask.EntryMode.edit, taskId))
                {
                    dailyTask.OnDailyMainSave += OnSave;
                    dailyTask.ShowDialog();
                }
            }
            
            if (entryType == JournalTask.EntryType.monthly)
            {
                using (MonthlyDescription monthlyDescription = new MonthlyDescription(JournalTask.EntryMode.edit, taskId))
                {
                    monthlyDescription.OnMonthlyMainSave += this.OnSave;
                    monthlyDescription.ShowDialog();
                }
            }

            if (entryType == JournalTask.EntryType.future)
            {
                using (FutureLog futureLog = new FutureLog(this, taskId, JournalTask.EntryMode.edit))
                {
                    futureLog.ShowDialog();
                }
            }

            if (entryType == JournalTask.EntryType.collection)
            {
                
                using (CollectionDescription category = new CollectionDescription( JournalTask.EntryMode.edit, taskId))
                {
                    category.OnCategorySaved += OnSave;
                    category.ShowDialog();
                }

            }
        }

        private void dailyTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            DailyTask_to_be_deleted dailyTask;

            if (entryType == JournalTask.EntryType.daily)
            {
                using (dailyTask = new DailyTask_to_be_deleted(this, taskId, JournalTask.EntryMode.migrate, JournalTask.EntryType.daily))
                {
                    dailyTask.ShowDialog();
                }
            }

            if (entryType == JournalTask.EntryType.monthly)
            {
                using (dailyTask = new DailyTask_to_be_deleted(this, taskId, JournalTask.EntryMode.migrate, JournalTask.EntryType.monthly))
                {
                    dailyTask.ShowDialog();
                }
            }

            if (entryType == JournalTask.EntryType.future)
            {
                using (dailyTask = new DailyTask_to_be_deleted(this, taskId, JournalTask.EntryMode.migrate, JournalTask.EntryType.future))
                {
                    dailyTask.ShowDialog();
                }
            }

            if (entryType == JournalTask.EntryType.collection)
            {
                using (dailyTask = new DailyTask_to_be_deleted(this, taskId, JournalTask.EntryMode.migrate, JournalTask.EntryType.collection))
                {
                    dailyTask.ShowDialog();
                }
            }
            */
        }

        private void monthlyTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            MonthlyTask monthlyTask;

            if (entryType == JournalTask.EntryType.daily)
            {
                using (monthlyTask = new MonthlyTask(this, taskId, JournalTask.EntryMode.migrate, JournalTask.EntryType.daily))
                {
                    monthlyTask.ShowDialog();
                }
            }

            if (entryType == JournalTask.EntryType.monthly)
            {
                using (monthlyTask = new MonthlyTask(this, taskId, JournalTask.EntryMode.migrate, JournalTask.EntryType.monthly))
                {
                    monthlyTask.ShowDialog();
                }
            }

            if (entryType == JournalTask.EntryType.future)
            {
                using (monthlyTask = new MonthlyTask(this, taskId, JournalTask.EntryMode.migrate, JournalTask.EntryType.future))
                {
                    monthlyTask.ShowDialog();
                }
            }

            if (entryType == JournalTask.EntryType.collection)
            {
                using (monthlyTask = new MonthlyTask(this, taskId, JournalTask.EntryMode.migrate, JournalTask.EntryType.collection))
                {
                    monthlyTask.ShowDialog();
                }
            }
            */
        }

        private void futureLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FutureLog futureLog;

            if (entryType == JournalTask.EntryType.daily)
            {
                using (futureLog = new FutureLog(this, taskId, JournalTask.EntryMode.migrate, JournalTask.EntryType.daily))
                {
                    futureLog.ShowDialog();
                }
            }

            if (entryType == JournalTask.EntryType.monthly)
            {
                using (futureLog = new FutureLog(this, taskId, JournalTask.EntryMode.migrate, JournalTask.EntryType.monthly))
                {
                    futureLog.ShowDialog();
                }
            }

            if (entryType == JournalTask.EntryType.future)
            {
                using (futureLog = new FutureLog(this, taskId, JournalTask.EntryMode.migrate, JournalTask.EntryType.future))
                {
                    futureLog.ShowDialog();
                }
            }

            if (entryType == JournalTask.EntryType.collection)
            {
                using (futureLog = new FutureLog(this, taskId, JournalTask.EntryMode.migrate, JournalTask.EntryType.collection))
                {
                    futureLog.ShowDialog();
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void OnSave()
        {
            RefreshGrid();
        }

    }
}