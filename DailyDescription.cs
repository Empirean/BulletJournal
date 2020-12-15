using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace BulletJournal
{
    public partial class DailyDescription : Form
    {

        // Events
        public delegate void EventHandler();
        public event EventHandler OnDailyMainSave;

        DBTools db;
        JournalTask.EntryMode mode;
        JournalTask.EntryType entryType;
        int dailyMainId;
        int dailyDetailId;

        public DailyDescription(JournalTask.EntryMode _mode,
            int _dailyMainId = -1,
            int _dailyDetailId = -1,
            JournalTask.EntryType _entryType = JournalTask.EntryType.none)
        {
            InitializeComponent();

            // Initialize Database Tools
            db = new DBTools(Properties.Settings.Default.DatabaseConnectionString);

            // store mode
            mode = _mode;

            // store categoryId
            dailyMainId = _dailyMainId;
            dailyDetailId = _dailyDetailId;

            // for migration
            entryType = _entryType;

            // Edit Mode
            if (mode == JournalTask.EntryMode.edit)
            {

                this.Text = "Edit Task Date";

                // Query the category name
                string command = "select taskdate, " +
                                 "description " +
                                 "from dailymain " +
                                 "where taskid = @id";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@id", SqlDbType.Int) { Value = dailyMainId}
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
            if (!(JournalTask.IsInputInvalid(txt_Desscription)))
                return;

            // Saving on Add Mode
            if (mode == JournalTask.EntryMode.add)
            {
                string command = "insert into dailymain " +
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
                string command = "update dailymain " +
                                 "set " +
                                 "description = @desc," +
                                 "taskdate = @taskdate " +
                                 "where taskid = @id";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@desc", SqlDbType.NVarChar) { Value = txt_Desscription.Text},
                    new SqlParameter("@id", SqlDbType.Int) { Value = dailyMainId},
                    new SqlParameter("@taskdate", SqlDbType.Date) { Value = dateTimePicker1.Value},
                };

                db.GenericNonQueryAction(command, parameters);
            }

            if (mode == JournalTask.EntryMode.migrate_main)
            {
                string command = "insert into dailymain " +
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
                        MigrationHelper.MigrateDailyToDaily(dailyMainId, insertedId, JournalTask.EntryMode.migrate_main);
                        break;
                    case JournalTask.EntryType.monthly:
                        MigrationHelper.MigrateMonthlyToDaily(dailyMainId, insertedId, JournalTask.EntryMode.migrate_main);
                        break;
                    case JournalTask.EntryType.future:
                        MigrationHelper.MigrateFutureToDaily(dailyMainId, insertedId, JournalTask.EntryMode.migrate_main);
                        break;
                    default:
                        break;
                }

            }

            if (mode == JournalTask.EntryMode.migrate_detail)
            {
                string command = "insert into dailymain " +
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
                        MigrationHelper.MigrateDailyToDaily(dailyDetailId, insertedId, JournalTask.EntryMode.migrate_detail);
                        break;
                    case JournalTask.EntryType.monthly:
                        MigrationHelper.MigrateMonthlyToDaily(dailyDetailId, insertedId, JournalTask.EntryMode.migrate_detail);
                        break;
                    case JournalTask.EntryType.future:
                        MigrationHelper.MigrateFutureToDaily(dailyDetailId, insertedId, JournalTask.EntryMode.migrate_detail);
                        break;
                    default:
                        break;
                }

            }


            // Cleanup
            txt_Desscription.Text = "";

            // Broadcast the event
            OnDailyDescriptionSave();

            // Close when on edit mode
            if (mode == JournalTask.EntryMode.edit || 
               mode == JournalTask.EntryMode.migrate_main ||
               mode == JournalTask.EntryMode.migrate_detail)
                this.Close();
        }

        protected virtual void OnDailyDescriptionSave()
        {
            if (OnDailyMainSave != null)
                OnDailyMainSave();
        }
    }
}
