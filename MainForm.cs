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
        DBTools dbTools;
        int taskId;
        JournalTask.EntryType entryType;

        public MainForm()
        {
            InitializeComponent();
            
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            
            string cn = Properties.Settings.Default.DatabaseConnectionString;
            dbTools = new DBTools(cn);
            
            Populate_dailyTask();
            Populate_Collection();
            Populate_futureLog();
            Populate_monthly();
            Populate_index();

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
            using (DailyTask addDailyTask = new DailyTask(this))
            {
                addDailyTask.ShowDialog();
            }
        }

        private void btn_addCollection_Click(object sender, EventArgs e)
        {

            /*
            using (Collections addCollection = new Collections(this))
            {
                addCollection.ShowDialog();
            }
            */
            using (Category category = new Category("", JournalTask.EntryMode.add, 0, this) )
            {
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

            dataGrid_index.DataSource = dbTools.GenericQueryAction(commandString, parameters);
            dataGrid_index.Columns["Entry"].Width = 120;
            dataGrid_index.Columns["Count"].Width = 70;
            dataGrid_index.Columns["Tasks"].Width = 70;
            dataGrid_index.Columns["Events"].Width = 70;
            dataGrid_index.Columns["Notes"].Width = 70;
            dataGrid_index.Columns["Closed"].Width = 70;

        }

        public void Populate_dailyTask()
        {
            string commandString = "select m.taskid, " +
                                          "m.taskdate as [Date (DD/MM/YYYY)], " +
                                          "case when d.taskisimportant = 1 " +
                                          "then '*' else '' end as [I], " +
                                          "case " +
                                          "when d.tasktype = 0 then 'TASK' " +
                                          "when d.tasktype = 1 then 'EVENT' " +
                                          "when d.tasktype = 2 then 'NOTES'" +
                                          "else 'CLOSED' end as [Type], " +
                                          "d.taskdescription as [Description]" +
                                   "from dailymain as m " +
                                   "inner join dailydetail as d " +
                                   "on m.taskid = d.maintaskforeignkey " +
                                   "where m.taskdate = @taskdate " +
                                   "order by m.taskdate";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@taskdate", SqlDbType.Date) { Value = dateTimePicker.Value }
            };

            dataGrid_dailyTask.DataSource = dbTools.GenericQueryAction(commandString, parameters);
            dataGrid_dailyTask.Columns[0].Visible = false;
            dataGrid_dailyTask.Columns[0].Width = 1;
            dataGrid_dailyTask.Columns["Date (DD/MM/YYYY)"].Width = 95;
            dataGrid_dailyTask.Columns["I"].Width = 20;
            dataGrid_dailyTask.Columns["Type"].Width = 70;
            dataGrid_dailyTask.Columns["Description"].Width = 285;
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

            dataGrid_futureLog.DataSource = dbTools.GenericQueryAction(commandString, parameters);
            dataGrid_futureLog.Columns[0].Visible = false;
            dataGrid_futureLog.Columns[0].Width = 1;
            dataGrid_futureLog.Columns["Date"].Width = 100;
            dataGrid_futureLog.Columns["I"].Width = 20;
            dataGrid_futureLog.Columns["Type"].Width = 70;
            dataGrid_futureLog.Columns["Description"].Width = 280;
        }

        public void Populate_monthly()
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
                                   "from monthlymain as m " +
                                   "inner join monthlydetail as d " +
                                   "on m.taskid = d.maintaskforeignkey " +
                                   "where m.taskdate >= @taskdatestart " +
                                   "and m.taskdate <= @taskdateEnd " +
                                   "order by m.taskdate";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@taskdatestart", SqlDbType.Date) { Value = new DateTime(dateTimePicker.Value.Year, dateTimePicker.Value.Month, 1) },
                new SqlParameter("@taskdateEnd", SqlDbType.Date) { Value = new DateTime(dateTimePicker.Value.Year, dateTimePicker.Value.Month, 
                                        DateTime.DaysInMonth(dateTimePicker.Value.Year, dateTimePicker.Value.Month)) }

            };

            dataGrid_monthly.DataSource = dbTools.GenericQueryAction(commandString, parameters);
            dataGrid_monthly.Columns[0].Visible = false;
            dataGrid_monthly.Columns[0].Width = 1;
            dataGrid_monthly.Columns["Date"].Width = 100;
            dataGrid_monthly.Columns["I"].Width = 20;
            dataGrid_monthly.Columns["Type"].Width = 70;
            dataGrid_monthly.Columns["Description"].Width = 280;
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


            dataGrid_collection.DataSource = dbTools.GenericQueryAction(commandString, parameters);

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
            using (MonthlyTask addMonthlyTask = new MonthlyTask(this))
            {
                addMonthlyTask.ShowDialog();
            }
        }

        private void dataGrid_dailyTask_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right)
            {
                taskId = ContextMenuHandler(dataGrid_dailyTask, contextMenuStrip1, e);
                entryType = JournalTask.EntryType.daily;
            }
        }

        private void dataGrid_collection_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                taskId = ContextMenuHandler(dataGrid_collection, contextMenuStrip1, e);
                entryType = JournalTask.EntryType.collection;
            }
        }

        private int ContextMenuHandler(DataGridView datagrid, ContextMenuStrip menu, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                datagrid.Rows[e.RowIndex].Selected = true;
                Rectangle cellRectangle = datagrid.GetCellDisplayRectangle(
                                                datagrid.Columns[e.ColumnIndex].Index,
                                                datagrid.Rows[e.RowIndex].Index,
                                                true);

                Point menuSpawnLocation = new Point(cellRectangle.Left, cellRectangle.Top);
                
                menu.Show(datagrid, menuSpawnLocation);
            }
            catch (Exception)
            {
            }

            return (int) datagrid.SelectedRows[0].Cells[0].Value;
        }

        

        private void dataGrid_monthly_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                taskId = ContextMenuHandler(dataGrid_monthly, contextMenuStrip1, e);
                entryType = JournalTask.EntryType.monthly;
            }
        }

        private void dataGrid_futureLog_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                taskId = ContextMenuHandler(dataGrid_futureLog, contextMenuStrip1, e);
                entryType = JournalTask.EntryType.future;
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (entryType == JournalTask.EntryType.collection)
            {
                string mainCommand = "delete from collectionmain " +
                                       "where collectionid = @taskId";

                SqlParameter[] mainparameters = new SqlParameter[]
                {
                    new SqlParameter("@taskId", SqlDbType.Int) { Value = taskId }

                };

                dbTools.GenericNonQueryAction(mainCommand, mainparameters);

                string detailCommand = "delete from collectiondetail " +
                                       "where maintaskforeignkey = @taskId";

                SqlParameter[] detailParameters = new SqlParameter[]
                {
                    new SqlParameter("@taskId", SqlDbType.Int) { Value = taskId }

                };

                dbTools.GenericNonQueryAction(detailCommand, detailParameters);

                Populate_Collection();
                Populate_index();
            }
            if (entryType == JournalTask.EntryType.daily)
            {

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@taskId", SqlDbType.Int) { Value = taskId }

                };

                string commandString = "delete from dailymain " +
                                       "where taskid = @taskId";

                dbTools.GenericNonQueryAction(commandString, parameters);

                parameters = new SqlParameter[]
                {
                    new SqlParameter("@taskId", SqlDbType.Int) { Value = taskId }

                };

                commandString = "delete from dailydetail " +
                                "where maintaskforeignkey = @taskId";

                dbTools.GenericNonQueryAction(commandString, parameters);

                Populate_dailyTask();
                Populate_index();
            }
            if (entryType == JournalTask.EntryType.monthly)
            {

                SqlParameter[] mainParameters = new SqlParameter[]
                {
                    new SqlParameter("@taskId", SqlDbType.Int) { Value = taskId }

                };

                string mainString = "delete from monthlymain " +
                                       "where taskid = @taskId";

                dbTools.GenericNonQueryAction(mainString, mainParameters);

                SqlParameter[] detailParameters = new SqlParameter[]
                {
                    new SqlParameter("@taskId", SqlDbType.Int) { Value = taskId }

                };

                string detailString = "delete from monthlydetail " +
                                "where maintaskforeignkey = @taskId";

                dbTools.GenericNonQueryAction(detailString, detailParameters);

                Populate_monthly();
                Populate_index();
            }
            if (entryType == JournalTask.EntryType.future)
            {

                SqlParameter[] mainParameters = new SqlParameter[]
                {
                    new SqlParameter("@taskId", SqlDbType.Int) { Value = taskId }

                };

                string mainString = "delete from futuremain " +
                                       "where taskid = @taskId";

                dbTools.GenericNonQueryAction(mainString, mainParameters);

                SqlParameter[] detailParameters = new SqlParameter[]
                {
                    new SqlParameter("@taskId", SqlDbType.Int) { Value = taskId }

                };

                string detailString = "delete from futuredetail " +
                                "where maintaskforeignkey = @taskId";

                dbTools.GenericNonQueryAction(detailString, detailParameters);

                Populate_futureLog();
                Populate_index();
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (entryType == JournalTask.EntryType.daily)
            {
                using (DailyTask dailyTask = new DailyTask(this, taskId, JournalTask.EntryMode.edit))
                {
                    dailyTask.ShowDialog();
                }
            }
            
            if (entryType == JournalTask.EntryType.monthly)
            {
                using (MonthlyTask monthlyTask = new MonthlyTask(this, taskId, JournalTask.EntryMode.edit))
                {
                    monthlyTask.ShowDialog();
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
                using (Collections collection = new Collections(this, taskId, JournalTask.EntryMode.edit))
                {
                    collection.ShowDialog();
                }
            }
        }

        private void dailyTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DailyTask dailyTask;

            if (entryType == JournalTask.EntryType.daily)
            {
                using (dailyTask = new DailyTask(this, taskId, JournalTask.EntryMode.migrate, JournalTask.EntryType.daily))
                {
                    dailyTask.ShowDialog();
                }
            }

            if (entryType == JournalTask.EntryType.monthly)
            {
                using (dailyTask = new DailyTask(this, taskId, JournalTask.EntryMode.migrate, JournalTask.EntryType.monthly))
                {
                    dailyTask.ShowDialog();
                }
            }

            if (entryType == JournalTask.EntryType.future)
            {
                using (dailyTask = new DailyTask(this, taskId, JournalTask.EntryMode.migrate, JournalTask.EntryType.future))
                {
                    dailyTask.ShowDialog();
                }
            }

            if (entryType == JournalTask.EntryType.collection)
            {
                using (dailyTask = new DailyTask(this, taskId, JournalTask.EntryMode.migrate, JournalTask.EntryType.collection))
                {
                    dailyTask.ShowDialog();
                }
            }
        }

        private void monthlyTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
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

        private void collectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Collections collections;

            if (entryType == JournalTask.EntryType.daily)
            {
                using (collections = new Collections(this, taskId, JournalTask.EntryMode.migrate, JournalTask.EntryType.daily))
                {
                    collections.ShowDialog();
                }
            }

            if (entryType == JournalTask.EntryType.monthly)
            {
                using (collections = new Collections(this, taskId, JournalTask.EntryMode.migrate, JournalTask.EntryType.monthly))
                {
                    collections.ShowDialog();
                }
            }

            if (entryType == JournalTask.EntryType.future)
            {
                using (collections = new Collections(this, taskId, JournalTask.EntryMode.migrate, JournalTask.EntryType.future))
                {
                    collections.ShowDialog();
                }
            }

            if (entryType == JournalTask.EntryType.collection)
            {
                using (collections = new Collections(this, taskId, JournalTask.EntryMode.migrate, JournalTask.EntryType.collection))
                {
                    collections.ShowDialog();
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

    }
}