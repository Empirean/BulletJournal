using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace BulletJournal
{
    public partial class MonthlyTaskDescription : Form
    {

        public delegate void EventHandler();
        public event EventHandler OnMonthlyTaskSaved;

        // database tools
        DBTools db;

        // ids
        int id;
        int layer;

        // mode
        JournalTask.EntryMode mode;

        public MonthlyTaskDescription(JournalTask.EntryMode _entryMode, int _id, int _layer)
        {
            InitializeComponent();

            // initialize db
            db = new DBTools(Properties.Settings.Default.DatabaseConnectionString);

            // store ids
            id = _id;
            layer = _layer;

            // store mode
            mode = _entryMode;

            cmb_taskType.SelectedIndex = 0;

            // Edit Mode

            if (mode == JournalTask.EntryMode.edit)
            {

                this.Text = "Edit Monthly Task";

                // Query the category name
                string command = "select description, " +
                                 "taskisimportant," +
                                 "tasktype " +
                                 "from monthlytasks " +
                                 "where id = @id";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@id", SqlDbType.Int) { Value = id}
                };

                DataTable dataTable = db.GenericQueryAction(command, parameters);
                DataRow dataRow = dataTable.AsEnumerable().ToList()[0];

                // Set the textbox to the category name
                txt_currentTaskDescription.Text = dataRow.Field<string>("description");
                cmb_taskType.SelectedIndex = dataRow.Field<int>("tasktype");
                chk_taskIsImportant.Checked = dataRow.Field<bool>("taskisimportant");
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // validation
            if (!(JournalTask.IsInputInvalid(txt_currentTaskDescription)))
                return;


            // add
            if (mode == JournalTask.EntryMode.add)
            {
                string command = "insert into monthlytasks " +
                                 "(description, layerid, previouslayerid, dateadded, datechanged, taskisimportant, tasktype) " +
                                 "values " +
                                 "(@desc, @layerid, @prevlayer, @dateadded, @datechanged, @taskisimportant, @tasktype) ";

                SqlParameter[] parameter = new SqlParameter[]
                {
                    new SqlParameter("@desc", SqlDbType.NVarChar) { Value = txt_currentTaskDescription.Text },
                    new SqlParameter("@layerid", SqlDbType.Int) {  Value = layer },
                    new SqlParameter("@prevlayer", SqlDbType.Int) { Value = id },
                    new SqlParameter("@dateadded", SqlDbType.DateTime) { Value = DateTime.Now },
                    new SqlParameter("@datechanged", SqlDbType.DateTime) {  Value = DateTime.Now },
                    new SqlParameter("@taskisimportant", SqlDbType.Bit) {  Value = chk_taskIsImportant.Checked },
                    new SqlParameter("@tasktype", SqlDbType.Int) {  Value = JournalTask.GetTask( cmb_taskType.Text) }
                };

                db.GenericNonQueryAction(command, parameter);

            }

            // edit
            if (mode == JournalTask.EntryMode.edit)
            {
                string command = "update monthlytasks " +
                                 "set " +
                                 "description = @desc, " +
                                 "datechanged = @datechanged," +
                                 "tasktype = @tasktype, " +
                                 "taskisimportant = @taskisimportant " +
                                 "where id = @id";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@desc", SqlDbType.NVarChar) { Value = txt_currentTaskDescription.Text },
                    new SqlParameter("@id", SqlDbType.Int) { Value = id },
                    new SqlParameter("@datechanged", SqlDbType.DateTime) { Value = DateTime.Now },
                    new SqlParameter("@tasktype", SqlDbType.Int) { Value = JournalTask.GetTask(cmb_taskType.Text)},
                    new SqlParameter("@taskisimportant", SqlDbType.Bit) { Value = chk_taskIsImportant.Checked}
                };

                db.GenericNonQueryAction(command, parameters);
            }

            // Cleanup
            txt_currentTaskDescription.Text = "";

            // Broadcast the event
            OnMonthlyTaskSave();

            // Close when on edit mode
            if (mode == JournalTask.EntryMode.edit)
                this.Close();
        }

        protected virtual void OnMonthlyTaskSave()
        {
            if (OnMonthlyTaskSaved != null)
                OnMonthlyTaskSaved();
        }
    }
}
