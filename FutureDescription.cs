using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace BulletJournal
{
    public partial class FutureDescription : Form
    {
        public delegate void EventHandler();
        public event EventHandler OnFutureMainSave;

        DBTools db;
        JournalTask.EntryMode mode;
        int FutureMainId;

        public FutureDescription(JournalTask.EntryMode _mode, int _futureMainId = -1)
        {
            InitializeComponent();
            dateTimePicker1.CustomFormat = "MMMM yyyy";

            // Initialize Database Tools
            db = new DBTools(Properties.Settings.Default.DatabaseConnectionString);

            // store mode
            mode = _mode;

            // store categoryId
            FutureMainId = _futureMainId;

            // Edit Mode
            if (mode == JournalTask.EntryMode.edit)
            {

                this.Text = "Edit Future Log";

                // Query the category name
                string command = "select taskdate, " +
                                 "description " +
                                 "from futuremain " +
                                 "where taskid = @id";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@id", SqlDbType.Int) { Value = FutureMainId}
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
                string command = "insert into futuremain " +
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
                string command = "update futuremain " +
                                 "set " +
                                 "description = @desc," +
                                 "taskdate = @taskdate " +
                                 "where taskid = @id";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@desc", SqlDbType.NVarChar) { Value = txt_Desscription.Text},
                    new SqlParameter("@id", SqlDbType.Int) { Value = FutureMainId},
                    new SqlParameter("@taskdate", SqlDbType.Date) { Value = dateTimePicker1.Value},
                };

                db.GenericNonQueryAction(command, parameters);
            }


            // Cleanup
            txt_Desscription.Text = "";

            // Broadcast the event
            OnFutureDescriptionSave();

            // Close when on edit mode
            if (mode == JournalTask.EntryMode.edit)
                this.Close();
        }

        protected virtual void OnFutureDescriptionSave()
        {
            if (OnFutureMainSave != null)
                OnFutureMainSave();
        }
    }
}
