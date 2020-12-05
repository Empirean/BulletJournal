using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace BulletJournal
{
    public partial class MonthlyDescription : Form
    {
        public delegate void EventHandler();
        public event EventHandler OnMonthlyMainSave;

        DBTools db;
        JournalTask.EntryMode mode;
        JournalTask.EntryType entryType;
        int monthlyMainId;

        public MonthlyDescription(JournalTask.EntryMode _mode, int _monthlyMainId = -1,
            JournalTask.EntryType _entryType = JournalTask.EntryType.none)
        {
            InitializeComponent();
            dateTimePicker1.CustomFormat = "MMMM yyyy";

            // Initialize Database Tools
            db = new DBTools(Properties.Settings.Default.DatabaseConnectionString);

            // store mode
            mode = _mode;

            // store categoryId
            monthlyMainId = _monthlyMainId;

            // for migration
            entryType = _entryType;

            // Edit Mode
            if (mode == JournalTask.EntryMode.edit)
            {

                this.Text = "Edit Month";

                // Query the category name
                string command = "select taskdate, " +
                                 "description " +
                                 "from monthlymain " +
                                 "where taskid = @id";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@id", SqlDbType.Int) { Value = monthlyMainId}
                };

                DataTable dataTable = db.GenericQueryAction(command, parameters);
                DataRow dataRow = dataTable.AsEnumerable().ToList()[0];

                // Set the textbox to the category name
                txt_Desscription.Text = dataRow.Field<string>("description");
                dateTimePicker1.Text = dataRow.Field<DateTime>("taskdate").ToString();

            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

            // Input validation
            if (!(JournalTask.IsInputValid(txt_Desscription)))
                return;

            // Saving on Add Mode
            if (mode == JournalTask.EntryMode.add)
            {
                string command = "insert into monthlymain " +
                                 "(taskdate, description) " +
                                 "values" +
                                 "(@taskdate, @desc)";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@desc", SqlDbType.NVarChar) { Value = txt_Desscription.Text},
                    new SqlParameter("@taskdate", SqlDbType.Date) { Value = dateTimePicker1.Value}
                };

                db.GenericNonQueryAction(command, parameters);
            }


            // Saving on Edit Mode
            if (mode == JournalTask.EntryMode.edit)
            {
                string command = "update monthlymain " +
                                 "set " +
                                 "description = @desc," +
                                 "taskdate = @taskdate " +
                                 "where taskid = @id";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@desc", SqlDbType.NVarChar) { Value = txt_Desscription.Text},
                    new SqlParameter("@id", SqlDbType.Int) { Value = monthlyMainId},
                    new SqlParameter("@taskdate", SqlDbType.Date) { Value = dateTimePicker1.Value},
                };

                db.GenericNonQueryAction(command, parameters);
            }

            if (mode == JournalTask.EntryMode.migrate)
            {
                string command = "insert into monthlymain " +
                                 "(taskdate, description) " +
                                 "output inserted.taskid " +
                                 "values" +
                                 "(@taskdate, @desc)";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@desc", SqlDbType.NVarChar) { Value = txt_Desscription.Text},
                    new SqlParameter("@taskdate", SqlDbType.Date) { Value = dateTimePicker1.Value}
                };

                int insertedId = db.GenericScalarAction(command, parameters);

                switch (entryType)
                {
                    case JournalTask.EntryType.daily:
                        MigrationHelper.MigrateDailyToMonthly(monthlyMainId, insertedId);
                        break;
                    case JournalTask.EntryType.monthly:
                        MigrationHelper.MigrateMonthlyToMonthly(monthlyMainId, insertedId);
                        break;
                    case JournalTask.EntryType.future:
                        MigrationHelper.MigrateFutureToMonthly(monthlyMainId, insertedId);
                        break;
                    default:
                        break;
                }

            }


            // Cleanup
            txt_Desscription.Text = "";

            // Broadcast the event
            OnMonthlyDescriptionSave();

            // Close when on edit mode
            if (mode == JournalTask.EntryMode.edit)
                this.Close();
        }

        protected virtual void OnMonthlyDescriptionSave()
        {
            if (OnMonthlyMainSave != null)
                OnMonthlyMainSave();
        }
    }
}
