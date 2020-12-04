using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace BulletJournal
{
    public partial class DailyTaskList : Form
    {

        public delegate void EventHandler();
        public event EventHandler OnCollectionSaved;

        // Database Tools
        DBTools db;

        // Journal Entry Mode
        JournalTask.EntryMode mode;

        // Ids
        int collectionMainId;
        int collectionDetailId;

        public DailyTaskList(JournalTask.EntryMode _mode, int _collectionMainId = -1, int _collectionDetailId = -1)
        {
            InitializeComponent();

            // Initialize Database Tools
            db = new DBTools(Properties.Settings.Default.DatabaseConnectionString);

            // Store Mode
            mode = _mode;

            // Store Ids
            collectionMainId = _collectionMainId;
            collectionDetailId = _collectionDetailId;

            // Edit Mode
            if (mode == JournalTask.EntryMode.edit)
            {
                this.Text = "Edit Collection";

                // Query the collection name
                string command = "select taskdescription, " +
                                 "tasktype, " +
                                 "taskisimportant " +
                                 "from dailydetail " +
                                 "where taskid = @detid " +
                                 "and maintaskforeignkey = @mainid";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@detid", SqlDbType.Int) { Value = collectionDetailId},
                    new SqlParameter("@mainid", SqlDbType.Int) { Value = collectionMainId}
                };

                DataTable dataTable = db.GenericQueryAction(command, parameters);
                DataRow dataRow = dataTable.AsEnumerable().ToList()[0];

                // set the textbox to collection name
                txt_description.Text = dataRow.Field<string>("taskdescription");
                cmb_taskType.SelectedItem = dataRow.Field<int>("tasktype");
                chk_important.Checked = dataRow.Field<bool>("taskisimportant");

            }

            cmb_taskType.SelectedIndex = 0;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Input validation
            if (!(JournalTask.IsInputValid(txt_description)))
                return;

            // Saving on Add Mode
            if (mode == JournalTask.EntryMode.add)
            {
                string command = "insert into dailydetail " +
                                 "(taskdescription, tasktype, taskisimportant, maintaskforeignkey) " +
                                 "values" +
                                 "(@desc, @tasktype, @taskisimportant, @id)";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@desc", SqlDbType.NVarChar) { Value = txt_description.Text},
                    new SqlParameter("@id", SqlDbType.Int) { Value = collectionMainId},
                    new SqlParameter("@tasktype", SqlDbType.Int) { Value = JournalTask.GetTask(cmb_taskType.Text)},
                    new SqlParameter("@taskisimportant", SqlDbType.Bit) { Value = chk_important.Checked}
                };

                db.GenericNonQueryAction(command, parameters);
            }


            // Saving on Edit Mode
            if (mode == JournalTask.EntryMode.edit)
            {
                string command = "update dailydetail " +
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
                    new SqlParameter("@detid", SqlDbType.Int) { Value = collectionDetailId},
                    new SqlParameter("@mainid", SqlDbType.Int) { Value = collectionMainId}
                };

                db.GenericNonQueryAction(command, parameters);
            }

            // Clean Up
            txt_description.Text = "";
            cmb_taskType.SelectedIndex = 0;
            chk_important.Checked = false;

            // Publish Event
            OnCategorySave();

            // Close when on edit mode
            if (mode == JournalTask.EntryMode.edit)
                this.Close();
        }

        protected virtual void OnCategorySave()
        {
            if (OnCollectionSaved != null)
                OnCollectionSaved();
        }
    }
}
