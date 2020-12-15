using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace BulletJournal
{
    public partial class FutureTaskList : Form
    {

        public delegate void EventHandler();
        public event EventHandler OnMonthlySaved;

        // Database Tools
        DBTools db;

        // Journal Entry Mode
        JournalTask.EntryMode mode;

        // Ids
        int futureMainId;
        int futureDetailId;

        public FutureTaskList(JournalTask.EntryMode _mode, int _futureMainId = -1, int _futureDetailId = -1)
        {
            InitializeComponent();

            // Initialize Database Tools
            db = new DBTools(Properties.Settings.Default.DatabaseConnectionString);

            // Store Mode
            mode = _mode;

            // Store Ids
            futureMainId = _futureMainId;
            futureDetailId = _futureDetailId;

            cmb_taskType.SelectedIndex = 0;

            // Edit Mode
            if (mode == JournalTask.EntryMode.edit)
            {
                this.Text = "Edit Future Task";

                // Query the collection name
                string command = "select taskdescription, " +
                                 "tasktype, " +
                                 "taskisimportant " +
                                 "from futuredetail " +
                                 "where taskid = @detid " +
                                 "and maintaskforeignkey = @mainid";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@detid", SqlDbType.Int) { Value = futureDetailId},
                    new SqlParameter("@mainid", SqlDbType.Int) { Value = futureMainId}
                };

                DataTable dataTable = db.GenericQueryAction(command, parameters);
                DataRow dataRow = dataTable.AsEnumerable().ToList()[0];

                // set the textbox to collection name
                txt_description.Text = dataRow.Field<string>("taskdescription");
                cmb_taskType.SelectedIndex = dataRow.Field<int>("tasktype");
                chk_important.Checked = dataRow.Field<bool>("taskisimportant");

            }

            
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Input validation
            if (!(JournalTask.IsInputInvalid(txt_description)))
                return;

            // Saving on Add Mode
            if (mode == JournalTask.EntryMode.add)
            {
                string command = "insert into futuredetail " +
                                 "(taskdescription, tasktype, taskisimportant, maintaskforeignkey) " +
                                 "values" +
                                 "(@desc, @tasktype, @taskisimportant, @id)";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@desc", SqlDbType.NVarChar) { Value = txt_description.Text},
                    new SqlParameter("@id", SqlDbType.Int) { Value = futureMainId},
                    new SqlParameter("@tasktype", SqlDbType.Int) { Value = JournalTask.GetTask(cmb_taskType.Text)},
                    new SqlParameter("@taskisimportant", SqlDbType.Bit) { Value = chk_important.Checked}
                };

                db.GenericNonQueryAction(command, parameters);
            }


            // Saving on Edit Mode
            if (mode == JournalTask.EntryMode.edit)
            {
                string command = "update futuredetail " +
                                 "set " +
                                 "taskdescription = @desc, " +
                                 "tasktype = @tasktype, " +
                                 "taskisimportant = @taskisimportant " +
                                 "where taskid = @detid " +
                                 "and maintaskforeignkey = @mainid";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@desc", SqlDbType.NVarChar) { Value = txt_description.Text},
                    new SqlParameter("@tasktype", SqlDbType.Int) { Value = JournalTask.GetTask(cmb_taskType.Text)},
                    new SqlParameter("@taskisimportant", SqlDbType.Bit) { Value = chk_important.Checked},
                    new SqlParameter("@detid", SqlDbType.Int) { Value = futureDetailId},
                    new SqlParameter("@mainid", SqlDbType.Int) { Value = futureMainId}
                };

                db.GenericNonQueryAction(command, parameters);
            }

            // Clean Up
            txt_description.Text = "";
            cmb_taskType.SelectedIndex = 0;
            chk_important.Checked = false;

            // Publish Event
            OnFutureSave();

            // Close when on edit mode
            if (mode == JournalTask.EntryMode.edit)
                this.Close();
        }

        protected virtual void OnFutureSave()
        {
            if (OnMonthlySaved != null)
                OnMonthlySaved();
        }
    }
}
