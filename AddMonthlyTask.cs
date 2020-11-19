using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BulletJournal
{
    public partial class AddMonthlyTask : Form
    {

        List<GeneralTask> generalTasks = new List<GeneralTask>();
        DBTools dbTools;
        MainForm main;

        public AddMonthlyTask(MainForm m)
        {
            InitializeComponent();
            main = m;
        }

        public AddMonthlyTask()
        {
            InitializeComponent();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            list_taskList.Items.Clear();
            generalTasks.Clear();
            Clear();
            
        }

        private void Clear()
        {
            txt_description.Text = "";
            cmb_taskType.SelectedIndex = 0;
            chk_important.Checked = false;
            btn_edit.Text = "Edit";
            txt_description.Focus();
        }

        private void populateTaskYear()
        {
            for (int i = DateTime.Now.Year; i < DateTime.Now.Year + 5; i++)
            {
                cmb_taskYear.Items.Add(i);
            }
        }

        private void populateTaskMonth()
        {
            for (int i = 1; i < 13; i++)
            {
                cmb_taskMonth.Items.Add(DateTimeFormatInfo.CurrentInfo.GetMonthName(i));
            }
        }

        private bool IsInputValid()
        {
            txt_description.Text = txt_description.Text.Trim();
            if (txt_description.Text.Length > 0)
                return true;
            return false;
        }

        private void AddMonthlyTask_Load(object sender, EventArgs e)
        {
            dbTools = new DBTools(Properties.Settings.Default.DatabaseConnectionString);
            populateTaskYear();
            populateTaskMonth();
            cmb_taskYear.SelectedIndex = 0;
            cmb_taskMonth.SelectedIndex = DateTime.Now.Month - 1;
            cmb_taskType.SelectedIndex = 0;
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            if (!(IsInputValid()))
                return;

            list_taskList.Items.Add(txt_description.Text);

            GeneralTask generalTask = new GeneralTask();
            generalTask.TaskDescription = txt_description.Text;
            generalTask.TaskType = JournalTask.GetTask(cmb_taskType.Text);
            generalTask.IsImportant = chk_important.Checked;

            generalTasks.Add(generalTask);

            Clear();
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            if (btn_edit.Text == "Edit")
            {

                Clear();
                txt_description.Text = generalTasks[list_taskList.SelectedIndex].TaskDescription;
                cmb_taskType.SelectedIndex = generalTasks[list_taskList.SelectedIndex].TaskType;
                chk_important.Checked = generalTasks[list_taskList.SelectedIndex].IsImportant;

                btn_edit.Text = "Update";
            }
            else
            {
                if (!(IsInputValid()))
                    return;

                GeneralTask generalTask = new GeneralTask();
                generalTask.TaskDescription = txt_description.Text;
                generalTask.TaskType = JournalTask.GetTask(cmb_taskType.Text);
                generalTask.IsImportant = chk_important.Checked;

                generalTasks[list_taskList.SelectedIndex] = generalTask;

                list_taskList.Items[list_taskList.SelectedIndex] = txt_description.Text;
                Clear();
                btn_edit.Text = "Edit";
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            int i = list_taskList.SelectedIndex;
            list_taskList.Items.RemoveAt(i);
            generalTasks.RemoveAt(i);
            Clear();
        }

        private void saveToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (list_taskList.Items.Count < 1)
                return;

            string taskDate = "01/" + (cmb_taskMonth.SelectedIndex + 1).ToString("00") + "/" + cmb_taskYear.Text;

            string command = "insert into monthlymain (taskdate) output inserted.taskid values (@taskDate)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@taskDate", SqlDbType.Date) { Value = DateTime.Parse(taskDate)}
            };

            int insertedId = dbTools.GenericScalarAction(command, parameters);

            command = "insert into monthlydetail (tasktype, taskdescription, taskisimportant, maintaskforeignkey) values " +
                          "(@tasktype, @taskdescription, @taskisimportant, @foreignkey)";

            foreach (GeneralTask taskItem in generalTasks)
            {

                parameters = new SqlParameter[]
                {
                    new SqlParameter("@tasktype", SqlDbType.Int) { Value = taskItem.TaskType},
                    new SqlParameter("@taskdescription", SqlDbType.NVarChar) { Value = taskItem.TaskDescription},
                    new SqlParameter("@taskisimportant", SqlDbType.Bit) { Value = taskItem.IsImportant},
                    new SqlParameter("@foreignkey", SqlDbType.Int) { Value = insertedId }
                };

                dbTools.GenericNonQueryAction(command, parameters);
            }

            Clear();
            list_taskList.Items.Clear();
            generalTasks.Clear();

            main.Populate_monthly();
            main.Populate_index();
        }

        private void list_taskList_MouseUp(object sender, MouseEventArgs e)
        {
            btn_edit.Text = "Edit";
        }
    }
}
