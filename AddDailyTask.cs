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
    public partial class AddDailyTask : Form
    {
        List<GeneralTask> generalTasks = new List<GeneralTask>();
        DBTools dbTools;
        MainForm main;
        int taskId;
        bool isEditMode = false;

        public AddDailyTask(MainForm m, int id)
        {
            InitializeComponent();
            dbTools = new DBTools(Properties.Settings.Default.DatabaseConnectionString);
            main = m;
            taskId = id;
            isEditMode = true;
            GetDailyData(id);
            this.Text = "<••> Edit Daily Task";


        }

        public AddDailyTask(MainForm m)
        {
            InitializeComponent();
            dbTools = new DBTools(Properties.Settings.Default.DatabaseConnectionString);
            main = m;
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            txtTaskDate.Text = monthCalendar1.SelectionRange.Start.ToString("dd/MM/yyyy");
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

        private void btn_clear_Click(object sender, EventArgs e)
        {
            list_taskList.Items.Clear();
            generalTasks.Clear();
            Clear();
        }

        private void AddDailyTask_Load(object sender, EventArgs e)
        {
            txtTaskDate.Text = monthCalendar1.SelectionRange.Start.ToString("dd/MM/yyyy");
            cmb_taskType.SelectedIndex = 0;
            
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            int i = list_taskList.SelectedIndex;
            list_taskList.Items.RemoveAt(i);
            generalTasks.RemoveAt(i);
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

        private void Clear()
        {
            txt_description.Text = "";
            cmb_taskType.SelectedIndex = 0;
            chk_important.Checked = false;
            btn_edit.Text = "Edit";
            txt_description.Focus();
        }

        private void list_taskList_MouseUp(object sender, MouseEventArgs e)
        {
            btn_edit.Text = "Edit";
        }

        private bool IsInputValid()
        {
            txt_description.Text = txt_description.Text.Trim();
            if (txt_description.Text.Length > 0)
                return true;
            return false;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (list_taskList.Items.Count < 1)
                return;

            if (isEditMode)
            {
                string command = "update dailymain " +
                                 "set taskdate = @taskDate " +
                                 "where taskid = @taskid";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@taskDate", SqlDbType.Date) { Value = DateTime.Parse(txtTaskDate.Text)},
                    new SqlParameter("@taskid", SqlDbType.Int) { Value = taskId}
                };

                dbTools.GenericNonQueryAction(command, parameters);

                command = "delete from dailydetail " +
                          "where maintaskforeignkey = @taskid";

                parameters = new SqlParameter[]
                {
                    new SqlParameter("@taskid", SqlDbType.Int) { Value = taskId}
                };

                dbTools.GenericNonQueryAction(command, parameters);

                command = "insert into dailydetail (tasktype, taskdescription, taskisimportant, maintaskforeignkey) values " +
                              "(@tasktype, @taskdescription, @taskisimportant, @foreignkey)";

                foreach (GeneralTask taskItem in generalTasks)
                {

                    parameters = new SqlParameter[]
                    {
                        new SqlParameter("@tasktype", SqlDbType.Int) { Value = taskItem.TaskType},
                        new SqlParameter("@taskdescription", SqlDbType.NVarChar) { Value = taskItem.TaskDescription},
                        new SqlParameter("@taskisimportant", SqlDbType.Bit) { Value = taskItem.IsImportant},
                        new SqlParameter("@foreignkey", SqlDbType.Int) { Value = taskId }
                    };

                    dbTools.GenericNonQueryAction(command, parameters);
                }

            }
            else
            {

                string command = "insert into dailymain (taskdate) output inserted.taskid values (@taskDate)";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@taskDate", SqlDbType.Date) { Value = DateTime.Parse(txtTaskDate.Text)}
                };

                int insertedId = dbTools.GenericScalarAction(command, parameters);

                command = "insert into dailydetail (tasktype, taskdescription, taskisimportant, maintaskforeignkey) values " +
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
            }



            Clear();
            list_taskList.Items.Clear();
            generalTasks.Clear();

            main.Populate_dailyTask();
            main.Populate_index();
        }

        private void GetDailyData(int id)
        {
           
            string commandString = "select taskdate " +
                                   "from dailymain " +
                                   "where taskid = @taskId";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@taskId", SqlDbType.Int) { Value = id }

            };

            DataTable dailyMainTable = dbTools.GenericQueryAction(commandString, parameters);

            DataRow dailyMainContent = dailyMainTable.AsEnumerable().ToList()[0];

            monthCalendar1.SelectionStart = dailyMainContent.Field<DateTime>("taskdate");


            commandString = "select taskdescription, " +
                                   "taskisimportant," +
                                   "tasktype " +
                                   "from dailydetail " +
                                   "where maintaskforeignkey = @taskId";

            parameters = new SqlParameter[]
            {
                new SqlParameter("@taskId", SqlDbType.Int) { Value = id }

            };

            DataTable dailyDetailTable = dbTools.GenericQueryAction(commandString, parameters);
            List<DataRow> dailyDetailContent = dailyDetailTable.AsEnumerable().ToList();

            foreach (DataRow detailItem in dailyDetailContent)
            {
                GeneralTask generalTask = new GeneralTask();
                generalTask.TaskDescription = detailItem.Field<string>("taskDescription");
                generalTask.TaskType = detailItem.Field<int>("tasktype");
                generalTask.IsImportant = detailItem.Field<bool>("taskisimportant");
                generalTasks.Add(generalTask);

                list_taskList.Items.Add(generalTask.TaskDescription);
            }
        }
    }
}
