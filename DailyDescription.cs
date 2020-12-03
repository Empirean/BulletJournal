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
    public partial class DailyDescription : Form
    {

        // Events
        public delegate void EventHandler();
        public event EventHandler OnDailyMainSave;

        DBTools db;
        JournalTask.EntryMode mode;
        int dailyMainId;

        public DailyDescription(JournalTask.EntryMode _mode, int _dailyMainId = -1)
        {
            InitializeComponent();

            // Initialize Database Tools
            db = new DBTools(Properties.Settings.Default.DatabaseConnectionString);

            // store mode
            mode = _mode;

            // store categoryId
            dailyMainId = _dailyMainId;

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
            if (!(JournalTask.IsInputValid(txt_Desscription)))
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
            

            // Cleanup
            txt_Desscription.Text = "";

            // Broadcast the event
            OnDailyDescriptionSave();

            // Close when on edit mode
            if (mode == JournalTask.EntryMode.edit)
                this.Close();
        }

        protected virtual void OnDailyDescriptionSave()
        {
            if (OnDailyMainSave != null)
                OnDailyMainSave();
        }
    }
}
