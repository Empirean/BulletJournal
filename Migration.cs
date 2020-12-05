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
    public partial class Migration : Form
    {
        public delegate void EventHandler();
        public event EventHandler OnMigrated;



        DBTools db;
        DateTimePicker dateTimePicker;
        JournalTask.EntryType entryTypeFr;
        JournalTask.EntryType entryTypeTo;
        int mainId;

        public Migration(JournalTask.EntryType _entryTypeFr, JournalTask.EntryType _entryTypeTo, int _mainId, DateTimePicker _dateTimePicker)
        {
            InitializeComponent();

            db = new DBTools(Properties.Settings.Default.DatabaseConnectionString);
            
            entryTypeFr = _entryTypeFr;
            entryTypeTo = _entryTypeTo;
            mainId = _mainId;
            dateTimePicker = _dateTimePicker;

            GridController(entryTypeTo);
        }

        private void GridController(JournalTask.EntryType _entryType)
        {
            switch (_entryType)
            {
                case JournalTask.EntryType.daily:
                    Populate_DailyContent();
                    break;
                case JournalTask.EntryType.monthly:
                    Populate_MonthlyContent();
                    break;
                case JournalTask.EntryType.future:
                    Populate_FutureContent();
                    break;
                default:
                    break;
            }
        }

        private void Populate_DailyContent()
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
                    new SqlParameter("@taskdate", SqlDbType.Date) { Value = dateTimePicker.Value },
                    new SqlParameter("@id", SqlDbType.Int) { Value = mainId }
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
                    new SqlParameter("@taskdate", SqlDbType.Date) { Value = dateTimePicker.Value } 
                };
            }

            dataGrid_content.DataSource = db.GenericQueryAction(command, parameters);

            dataGrid_content.Columns[0].Visible = false;
            dataGrid_content.Columns[0].Width = 1;
            dataGrid_content.Columns["Date"].Width = 70;
            dataGrid_content.Columns["Description"].Width = 332;
            dataGrid_content.Columns["Contents"].Width = 70;
        }

        private void Populate_MonthlyContent()
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
                    new SqlParameter("@taskdate", SqlDbType.Date) { Value = dateTimePicker.Value },
                    new SqlParameter("@id", SqlDbType.Int) { Value = mainId }
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
                    new SqlParameter("@taskdate", SqlDbType.Date) { Value = dateTimePicker.Value }
                };
            }

            dataGrid_content.DataSource = db.GenericQueryAction(command, parameters);

            dataGrid_content.Columns[0].Visible = false;
            dataGrid_content.Columns[0].Width = 1;
            dataGrid_content.Columns["Date"].Width = 90;
            dataGrid_content.Columns["Description"].Width = 310;
            dataGrid_content.Columns["Contents"].Width = 70;
        }

        private void Populate_FutureContent()
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
                    new SqlParameter("@taskdate", SqlDbType.Date) { Value = dateTimePicker.Value },
                    new SqlParameter("@id", SqlDbType.Int) { Value = mainId }
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
                    new SqlParameter("@taskdate", SqlDbType.Date) { Value = dateTimePicker.Value }
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
                        MigrateDailyToDaily(mainId);
                        break;
                    case JournalTask.EntryType.monthly:
                        MigrateDailyToMonthly(mainId);
                        break;
                    case JournalTask.EntryType.future:
                        MigrateDailyToFuture(mainId);
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
                        MigrateMonthlyToDaily(mainId);
                        break;
                    case JournalTask.EntryType.monthly:
                        MigrateMonthlyToMonthly(mainId);
                        break;
                    case JournalTask.EntryType.future:
                        MigrateMonthlyToFuture(mainId);
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
                        MigrateFutureToDaily(mainId);
                        break;
                    case JournalTask.EntryType.monthly:
                        MigrateFutureToMonthly(mainId);
                        break;
                    case JournalTask.EntryType.future:
                        MigrateFutureToFuture(mainId);
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
                
                string command = "insert into dailydetail " +
                    "(tasktype, taskdescription, taskisimportant, maintaskforeignkey) " +
                    "(select tasktype, taskdescription, taskisimportant, " + row.Cells[0].Value.ToString() + " " +
                    "from dailydetail " +
                    "where maintaskforeignkey = @id)";


                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@id", SqlDbType.Int) {  Value = _id}
                };

                db.GenericNonQueryAction(command, parameters);
                

            }
        }

        private void MigrateDailyToMonthly(int _id)
        {
            foreach (DataGridViewRow row in dataGrid_content.SelectedRows)
            {

                string command = "insert into monthlydetail " +
                    "(tasktype, taskdescription, taskisimportant, maintaskforeignkey) " +
                    "(select tasktype, taskdescription, taskisimportant, " + row.Cells[0].Value.ToString() + " " +
                    "from dailydetail " +
                    "where maintaskforeignkey = @id)";


                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@id", SqlDbType.Int) {  Value = _id}
                };

                db.GenericNonQueryAction(command, parameters);


            }
        }

        private void MigrateDailyToFuture(int _id)
        {
            foreach (DataGridViewRow row in dataGrid_content.SelectedRows)
            {

                string command = "insert into futuredetail " +
                    "(tasktype, taskdescription, taskisimportant, maintaskforeignkey) " +
                    "(select tasktype, taskdescription, taskisimportant, " + row.Cells[0].Value.ToString() + " " +
                    "from dailydetail " +
                    "where maintaskforeignkey = @id)";


                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@id", SqlDbType.Int) {  Value = _id}
                };

                db.GenericNonQueryAction(command, parameters);


            }
        }

        private void MigrateMonthlyToDaily(int _id)
        {
            foreach (DataGridViewRow row in dataGrid_content.SelectedRows)
            {

                string command = "insert into dailydetail " +
                    "(tasktype, taskdescription, taskisimportant, maintaskforeignkey) " +
                    "(select tasktype, taskdescription, taskisimportant, " + row.Cells[0].Value.ToString() + " " +
                    "from monthlydetail " +
                    "where maintaskforeignkey = @id)";


                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@id", SqlDbType.Int) {  Value = _id}
                };

                db.GenericNonQueryAction(command, parameters);


            }
        }

        private void MigrateMonthlyToMonthly(int _id)
        {
            foreach (DataGridViewRow row in dataGrid_content.SelectedRows)
            {

                string command = "insert into monthlydetail " +
                    "(tasktype, taskdescription, taskisimportant, maintaskforeignkey) " +
                    "(select tasktype, taskdescription, taskisimportant, " + row.Cells[0].Value.ToString() + " " +
                    "from monthlydetail " +
                    "where maintaskforeignkey = @id)";


                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@id", SqlDbType.Int) {  Value = _id}
                };

                db.GenericNonQueryAction(command, parameters);


            }
        }

        private void MigrateMonthlyToFuture(int _id)
        {
            foreach (DataGridViewRow row in dataGrid_content.SelectedRows)
            {

                string command = "insert into futuredetail " +
                    "(tasktype, taskdescription, taskisimportant, maintaskforeignkey) " +
                    "(select tasktype, taskdescription, taskisimportant, " + row.Cells[0].Value.ToString() + " " +
                    "from monthlydetail " +
                    "where maintaskforeignkey = @id)";


                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@id", SqlDbType.Int) {  Value = _id}
                };

                db.GenericNonQueryAction(command, parameters);


            }
        }

        private void MigrateFutureToDaily(int _id)
        {
            foreach (DataGridViewRow row in dataGrid_content.SelectedRows)
            {

                string command = "insert into dailydetail " +
                    "(tasktype, taskdescription, taskisimportant, maintaskforeignkey) " +
                    "(select tasktype, taskdescription, taskisimportant, " + row.Cells[0].Value.ToString() + " " +
                    "from futuredetail " +
                    "where maintaskforeignkey = @id)";


                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@id", SqlDbType.Int) {  Value = _id}
                };

                db.GenericNonQueryAction(command, parameters);


            }
        }

        private void MigrateFutureToMonthly(int _id)
        {
            foreach (DataGridViewRow row in dataGrid_content.SelectedRows)
            {

                string command = "insert into monthlydetail " +
                    "(tasktype, taskdescription, taskisimportant, maintaskforeignkey) " +
                    "(select tasktype, taskdescription, taskisimportant, " + row.Cells[0].Value.ToString() + " " +
                    "from futuredetail " +
                    "where maintaskforeignkey = @id)";


                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@id", SqlDbType.Int) {  Value = _id}
                };

                db.GenericNonQueryAction(command, parameters);


            }
        }

        private void MigrateFutureToFuture(int _id)
        {
            foreach (DataGridViewRow row in dataGrid_content.SelectedRows)
            {

                string command = "insert into futuredetail " +
                    "(tasktype, taskdescription, taskisimportant, maintaskforeignkey) " +
                    "(select tasktype, taskdescription, taskisimportant, " + row.Cells[0].Value.ToString() + " " +
                    "from futuredetail " +
                    "where maintaskforeignkey = @id)";


                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@id", SqlDbType.Int) {  Value = _id}
                };

                db.GenericNonQueryAction(command, parameters);


            }
        }

        protected virtual void OnMigrate()
        {
            if (OnMigrated != null)
                OnMigrated();
        }
    }
}
