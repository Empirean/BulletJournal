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
    public partial class AddCollection : Form
    {
        DBTools db;
        MainForm main;
        int taskId;

        bool isEditMode = false;

        public AddCollection(MainForm m, int id)
        {
            InitializeComponent();
            db = new DBTools(Properties.Settings.Default.DatabaseConnectionString);
            main = m;
            cmb_taskType.SelectedIndex = 0;
            isEditMode = true;
            this.Text = "Edit Collection";
            GetCollectionData(id);
            taskId = id;
        }

        public AddCollection(MainForm m)
        {
            InitializeComponent();
            db = new DBTools(Properties.Settings.Default.DatabaseConnectionString);
            main = m;
            cmb_taskType.SelectedIndex = 0;

        }

        private void GetCollectionData(int id)
        {

            string commandString = "select taskId, " +
                                   "taskdescription, " +
                                   "tasktype, " +
                                   "taskisimportant, " +
                                   "taskdateadded " +
                                   "from collectiontable " +
                                   "where taskid = @taskId";

            SqlParameter[] parameters = new SqlParameter[]
            {
                    new SqlParameter("@taskId", SqlDbType.Int) { Value = id }

            };

            DataTable collectionData = db.GenericQueryAction(commandString, parameters);
            List<DataRow> dataRow = collectionData.AsEnumerable().ToList();

            txt_description.Text = dataRow[0].Field<string>("taskdescription");
            cmb_taskType.SelectedIndex = dataRow[0].Field<int>("tasktype");
            chk_important.Checked = dataRow[0].Field<bool>("taskisimportant");
        }

        private void Clear()
        {
            txt_description.Text = "";
            cmb_taskType.Text = "";
            chk_important.Checked = false;
            txt_description.Focus();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!(IsInputValid()))
                return;

            SqlParameter[] parameters;
            string command;

            if (isEditMode)
            {
                parameters = new SqlParameter[]
                {
                    new SqlParameter("@desc", SqlDbType.NVarChar) { Value = txt_description.Text  },
                    new SqlParameter("@tasktype", SqlDbType.Int) { Value = JournalTask.GetTask(cmb_taskType.Text)},
                    new SqlParameter("@isImportant", SqlDbType.Bit) { Value = chk_important.Checked},
                    new SqlParameter("@taskid", SqlDbType.Int) { Value = taskId}
                };
                command = "update collectiontable " +
                          "set " +
                          "taskdescription = @desc, " +
                          "tasktype = @taskType, " +
                          "taskisimportant = @IsImportant " +
                          "where taskid = @taskid";
            }
            else
            {

                parameters = new SqlParameter[]
                {
                    new SqlParameter("@desc", SqlDbType.NVarChar) { Value = txt_description.Text  },
                    new SqlParameter("@tasktype", SqlDbType.Int) { Value = JournalTask.GetTask(cmb_taskType.Text)},
                    new SqlParameter("@isImportant", SqlDbType.Bit) { Value = chk_important.Checked},
                    new SqlParameter("@taskDateAdded", SqlDbType.Date) { Value = DateTime.Now}
                };
                command = "insert into collectiontable (taskdescription, tasktype, taskisimportant, taskDateAdded) " +
                                 "values (@desc, @tasktype, @isImportant, @taskDateAdded)";
            }


            db.GenericNonQueryAction(command, parameters);
            main.Populate_collection();
            main.Populate_index();
            Clear();

            if (isEditMode)
                this.Close();

        }

        private bool IsInputValid()
        {
            txt_description.Text = txt_description.Text.Trim();
            if (txt_description.Text.Length > 0)
                return true;
            return false;
        }

    }
}
