using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BulletJournal
{
    public partial class Migration : Form
    {
        public delegate void EventHandler();
        public event EventHandler OnMigrated;

        DBTools db;

        JournalTask.EntryType entryTypeFr;
        JournalTask.EntryType entryTypeTo;
        JournalTask.EntryMode mode;
        int entryId;

        public Migration
        (
            JournalTask.EntryType _entryTypeFr,
            JournalTask.EntryType _entryTypeTo,
            int _mainId,
            int _detailId = -1
,
            JournalTask.EntryMode _mode = JournalTask.EntryMode.migrate_main)
        {
            InitializeComponent();

            db = new DBTools(Properties.Settings.Default.DatabaseConnectionString);
            
            entryTypeFr = _entryTypeFr;
            entryTypeTo = _entryTypeTo;

            mode = _mode;

            if (mode == JournalTask.EntryMode.migrate_main)
                entryId = _mainId;
            else
                entryId = _detailId;

            GridController(entryTypeTo, _mainId);
        }

        private void GridController(JournalTask.EntryType _entryType, int _id)
        {
            switch (_entryType)
            {
                case JournalTask.EntryType.daily:
                    Populate_DailyContent(_id);
                    break;
                case JournalTask.EntryType.monthly:
                    Populate_MonthlyContent(_id);
                    break;
                case JournalTask.EntryType.future:
                    Populate_FutureContent(_id);
                    break;
                default:
                    break;
            }
        }

        private void Populate_DailyContent(int _id)
        {
            string command;
            SqlParameter[] parameters;

            if (entryTypeFr == entryTypeTo)
            {
                command = "select " +
                                   "a.taskid, " +
                                   "format(a.taskdate, 'dd/MM/yyyy') as [Date], " +
                                   "a.description as Description, " +
                                   "count(b.taskid) as [Contents] " +
                                   "from dailymain as a " +
                                   "left join dailydetail as b " +
                                   "on a.taskid = b.maintaskforeignkey " +
                                   "where a.taskdate >= @taskdate " +
                                   "and a.taskid <> @id " +
                                   "group by a.taskid, format(a.taskdate, 'dd/MM/yyyy') ,a.description " +
                                   "order by format(a.taskdate, 'dd/MM/yyyy'), a.taskid";

                parameters = new SqlParameter[]
                {
                    new SqlParameter("@taskdate", SqlDbType.Date) { Value = JournalTask.currentDay.Value },
                    new SqlParameter("@id", SqlDbType.Int) { Value = _id }
                };
            }
            else
            {
                command = "select " +
                                   "a.taskid, " +
                                   "format(a.taskdate, 'dd/MM/yyyy') as [Date], " +
                                   "a.description as Description, " +
                                   "count(b.taskid) as [Contents] " +
                                   "from dailymain as a " +
                                   "left join dailydetail as b " +
                                   "on a.taskid = b.maintaskforeignkey " +
                                   "where a.taskdate >= @taskdate " +
                                   "group by a.taskid, format(a.taskdate, 'dd/MM/yyyy') ,a.description " +
                                   "order by format(a.taskdate, 'dd/MM/yyyy'), a.taskid";

                parameters = new SqlParameter[]
                {
                    new SqlParameter("@taskdate", SqlDbType.Date) { Value = JournalTask.currentDay.Value } 
                };
            }

            dataGrid_content.DataSource = db.GenericQueryAction(command, parameters);

            dataGrid_content.Columns[0].Visible = false;
            dataGrid_content.Columns[0].Width = 1;
            dataGrid_content.Columns["Date"].Width = 70;
            dataGrid_content.Columns["Description"].Width = 332;
            dataGrid_content.Columns["Contents"].Width = 70;
        }

        private void Populate_MonthlyContent(int _id)
        {
            string command;
            SqlParameter[] parameters;

            if (entryTypeFr == entryTypeTo)
            {
                command = "select " +
                                   "a.taskid, " +
                                   "format(a.taskdate, 'yyyy MMMM') as [Date], " +
                                   "a.description as Description, " +
                                   "count(b.taskid) as [Contents] " +
                                   "from monthlymain as a " +
                                   "left join monthlydetail as b " +
                                   "on a.taskid = b.maintaskforeignkey " +
                                   "where a.taskdate >= @taskdate " +
                                   "and a.taskid <> @id " +
                                   "group by a.taskid, format(a.taskdate, 'yyyy MMMM') ,a.description " +
                                   "order by format(a.taskdate, 'yyyy MMMM'), a.taskid";

                parameters = new SqlParameter[]
                {
                    new SqlParameter("@taskdate", SqlDbType.Date) { Value = JournalTask.currentDay.Value },
                    new SqlParameter("@id", SqlDbType.Int) { Value = _id }
                };

            }
            else
            {
                command = "select " +
                                   "a.taskid, " +
                                   "format(a.taskdate, 'yyyy MMMM') as [Date], " +
                                   "a.description as Description, " +
                                   "count(b.taskid) as [Contents] " +
                                   "from monthlymain as a " +
                                   "left join monthlydetail as b " +
                                   "on a.taskid = b.maintaskforeignkey " +
                                   "where a.taskdate >= @taskdate " +
                                   "group by a.taskid, format(a.taskdate, 'yyyy MMMM') ,a.description " +
                                   "order by format(a.taskdate, 'yyyy MMMM'), a.taskid";

                parameters = new SqlParameter[]
                {
                    new SqlParameter("@taskdate", SqlDbType.Date) { Value = JournalTask.currentDay.Value }
                };
            }

            dataGrid_content.DataSource = db.GenericQueryAction(command, parameters);

            dataGrid_content.Columns[0].Visible = false;
            dataGrid_content.Columns[0].Width = 1;
            dataGrid_content.Columns["Date"].Width = 90;
            dataGrid_content.Columns["Description"].Width = 310;
            dataGrid_content.Columns["Contents"].Width = 70;
        }

        private void Populate_FutureContent(int _id)
        {
            string command;
            SqlParameter[] parameters;

            if (entryTypeTo == entryTypeFr)
            {
                command = "select " +
                                   "a.taskid, " +
                                   "format(a.taskdate, 'yyyy MMMM') as [Date], " +
                                   "a.description as Description, " +
                                   "count(b.taskid) as [Contents] " +
                                   "from futuremain as a " +
                                   "left join futuredetail as b " +
                                   "on a.taskid = b.maintaskforeignkey " +
                                   "where a.taskdate >= @taskdate " +
                                   "and a.taskid <> @id " +
                                   "group by a.taskid, format(a.taskdate, 'yyyy MMMM') ,a.description " +
                                   "order by format(a.taskdate, 'yyyy MMMM'), a.taskid";

                parameters = new SqlParameter[]
                {
                    new SqlParameter("@taskdate", SqlDbType.Date) { Value = JournalTask.currentDay.Value },
                    new SqlParameter("@id", SqlDbType.Int) { Value = _id }
                };

            }
            else
            {
                command = "select " +
                                   "a.taskid, " +
                                   "format(a.taskdate, 'yyyy MMMM') as [Date], " +
                                   "a.description as Description, " +
                                   "count(b.taskid) as [Contents] " +
                                   "from futuremain as a " +
                                   "left join futuredetail as b " +
                                   "on a.taskid = b.maintaskforeignkey " +
                                   "where a.taskdate >= @taskdate " +
                                   "group by a.taskid, format(a.taskdate, 'yyyy MMMM') ,a.description " +
                                   "order by format(a.taskdate, 'yyyy MMMM'), a.taskid";

                parameters = new SqlParameter[]
                {
                    new SqlParameter("@taskdate", SqlDbType.Date) { Value = JournalTask.currentDay.Value }
                };

            }

            dataGrid_content.DataSource = db.GenericQueryAction(command, parameters);

            dataGrid_content.Columns[0].Visible = false;
            dataGrid_content.Columns[0].Width = 1;
            dataGrid_content.Columns["Date"].Width = 90;
            dataGrid_content.Columns["Description"].Width = 310;
            dataGrid_content.Columns["Contents"].Width = 70;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (entryTypeFr == JournalTask.EntryType.daily)
            {
                switch (entryTypeTo)
                {
                    case JournalTask.EntryType.daily:
                        MigrateDailyToDaily(entryId);
                        break;
                    case JournalTask.EntryType.monthly:
                        MigrateDailyToMonthly(entryId);
                        break;
                    case JournalTask.EntryType.future:
                        MigrateDailyToFuture(entryId);
                        break;
                    default:
                        break;
                }
            }
            else if (entryTypeFr == JournalTask.EntryType.monthly)
            {
                switch (entryTypeTo)
                {
                    case JournalTask.EntryType.daily:
                        MigrateMonthlyToDaily(entryId);
                        break;
                    case JournalTask.EntryType.monthly:
                        MigrateMonthlyToMonthly(entryId);
                        break;
                    case JournalTask.EntryType.future:
                        MigrateMonthlyToFuture(entryId);
                        break;
                    default:
                        break;
                }
            }
            else if (entryTypeFr == JournalTask.EntryType.future)
            {
                switch (entryTypeTo)
                {
                    case JournalTask.EntryType.daily:
                        MigrateFutureToDaily(entryId);
                        break;
                    case JournalTask.EntryType.monthly:
                        MigrateFutureToMonthly(entryId);
                        break;
                    case JournalTask.EntryType.future:
                        MigrateFutureToFuture(entryId);
                        break;
                    default:
                        break;
                }
            }


            OnMigrate();
            this.Close();
        }

        private void MigrateDailyToDaily(int _id)
        {
            
            foreach (DataGridViewRow row in dataGrid_content.SelectedRows)
            {
                MigrationHelper.MigrateDailyToDaily(_id, (int) row.Cells[0].Value, mode);
            }
        }

        private void MigrateDailyToMonthly(int _id)
        {
            foreach (DataGridViewRow row in dataGrid_content.SelectedRows)
            {
                MigrationHelper.MigrateDailyToMonthly(_id, (int)row.Cells[0].Value, mode);
            }
        }

        private void MigrateDailyToFuture(int _id)
        {
            foreach (DataGridViewRow row in dataGrid_content.SelectedRows)
            {
                MigrationHelper.MigrateDailyToFuture(_id, (int)row.Cells[0].Value, mode);
            }
        }

        private void MigrateMonthlyToDaily(int _id)
        {
            foreach (DataGridViewRow row in dataGrid_content.SelectedRows)
            {
                MigrationHelper.MigrateMonthlyToDaily(_id, (int)row.Cells[0].Value, mode);
            }
        }

        private void MigrateMonthlyToMonthly(int _id)
        {
            foreach (DataGridViewRow row in dataGrid_content.SelectedRows)
            {
                MigrationHelper.MigrateMonthlyToMonthly(_id, (int)row.Cells[0].Value, mode);
            }
        }

        private void MigrateMonthlyToFuture(int _id)
        {
            foreach (DataGridViewRow row in dataGrid_content.SelectedRows)
            {
                MigrationHelper.MigrateMonthlyToFuture(_id, (int)row.Cells[0].Value, mode);
            }
        }

        private void MigrateFutureToDaily(int _id)
        {
            foreach (DataGridViewRow row in dataGrid_content.SelectedRows)
            {
                MigrationHelper.MigrateFutureToDaily(_id, (int)row.Cells[0].Value, mode);
            }
        }

        private void MigrateFutureToMonthly(int _id)
        {
            foreach (DataGridViewRow row in dataGrid_content.SelectedRows)
            {
                MigrationHelper.MigrateFutureToMonthly(_id, (int)row.Cells[0].Value, mode);
            }
        }

        private void MigrateFutureToFuture(int _id)
        {
            foreach (DataGridViewRow row in dataGrid_content.SelectedRows)
            {
                MigrationHelper.MigrateFutureToFuture(_id, (int)row.Cells[0].Value, mode);
            }
        }

        protected virtual void OnMigrate()
        {
            if (OnMigrated != null)
                OnMigrated();
        }
    }
}
