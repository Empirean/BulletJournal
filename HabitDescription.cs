using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace BulletJournal
{
    public partial class HabitDescription : Form
    {

        public delegate void EventHandler();
        public event EventHandler OnHabitSaved;

        DBTools db;

        JournalTask.EntryMode entryMode;
        int id;

        public HabitDescription(JournalTask.EntryMode _entryMode, int _id)
        {
            InitializeComponent();

            id = _id;
            entryMode = _entryMode;

            db = new DBTools(Properties.Settings.Default.ConnectionString);


            if (entryMode == JournalTask.EntryMode.edit)
            {

                this.Text = "Edit Habit";

                // Query the category name
                string command = "select description, " +
                                 "isvisible " +
                                 "from habit " +
                                 "where id = @id";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@id", SqlDbType.Int) { Value = id}
                };

                DataTable dataTable = db.GenericQueryAction(command, parameters);
                DataRow dataRow = dataTable.AsEnumerable().ToList()[0];

                // Set the textbox to the category name
                txt_currentTaskDescription.Text = dataRow.Field<string>("description");
                chk_taskIsVisible.Checked = dataRow.Field<bool>("isvisible");
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // validation
            if (!(JournalTask.IsInputInvalid(txt_currentTaskDescription)))
                return;

            if (entryMode == JournalTask.EntryMode.add)
            {
                string command = "insert into habit " +
                                 "(description, isvisible) " +
                                 "values " +
                                 "(@description, @isvisible)";

                SqlParameter[] paramter = new SqlParameter[]
                {
                    new SqlParameter("@description", SqlDbType.NVarChar) { Value = txt_currentTaskDescription.Text},
                    new SqlParameter("isvisible", SqlDbType.Bit) { Value = chk_taskIsVisible.Checked}
                };

                db.GenericNonQueryAction(command, paramter);
            }

            if (entryMode == JournalTask.EntryMode.edit)
            {
                string command = "update habit " +
                                 "set " +
                                 "description = @desc, " +
                                 "isvisible = @isvisible " +
                                 "where id = @id";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@desc", SqlDbType.NVarChar) { Value = txt_currentTaskDescription.Text },
                    new SqlParameter("@id", SqlDbType.Int) { Value = id },
                    new SqlParameter("@isvisible", SqlDbType.Bit) { Value = chk_taskIsVisible.Checked }
                };

                db.GenericNonQueryAction(command, parameters);
            }

            txt_currentTaskDescription.Text = "";

            if (entryMode == JournalTask.EntryMode.edit)
                this.Close();

            OnSave();
        }

        protected virtual void OnSave()
        {
            if (OnHabitSaved != null)
                OnHabitSaved();
        }
    }
}
