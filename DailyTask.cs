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
    public partial class DailyTask : Form
    {
        List<GeneralTask> generalTasks = new List<GeneralTask>();
        DBTools dbTools;
        MainForm main;
        JournalTask.EntryMode accessMode;

        int taskId;

        public DailyTask(MainForm m, int id, JournalTask.EntryMode mode, JournalTask.EntryType c = JournalTask.EntryType.none)
        {
            InitializeComponent();
            dbTools = new DBTools(Properties.Settings.Default.DatabaseConnectionString);

            main = m;
            taskId = id;
            
            accessMode = mode;

            if (accessMode == JournalTask.EntryMode.edit)
            {
                this.Text = "<••> Edit Daily Task";
                GetDailyData(id);
            }
            if (accessMode == JournalTask.EntryMode.migrate)
            {
                this.Text = "<••> Migrate Daily Task";
                monthCalendar1.MaxSelectionCount = 7;

                lbl_enddate.Visible = true;
                txt_enddate.Visible = true;

                if (c == JournalTask.EntryType.daily)
                    GetDailyData(id);
                if (c == JournalTask.EntryType.monthly)
                    GetMonthlyData(id);
                if (c == JournalTask.EntryType.future)
                    GetFutureData(id);
                if (c == JournalTask.EntryType.collection)
                    GetCollectionData(id);
            }
            
        }

        public DailyTask(MainForm m)
        {
            InitializeComponent();
            dbTools = new DBTools(Properties.Settings.Default.DatabaseConnectionString);
            main = m;
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            txt_taskDate.Text = monthCalendar1.SelectionRange.Start.ToString("dd/MM/yyyy");
            txt_enddate.Text = monthCalendar1.SelectionRange.End.ToString("dd/MM/yyyy");
            lbl_enddate.Text = "Start Date:";
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
            txt_taskDate.Text = monthCalendar1.SelectionRange.Start.ToString("dd/MM/yyyy");
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

            if (accessMode == JournalTask.EntryMode.edit)
            {
                string command = "update dailymain " +
                                 "set taskdate = @taskDate " +
                                 "where taskid = @taskid";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@taskDate", SqlDbType.Date) { Value = DateTime.Parse(txt_taskDate.Text)},
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
            else if (accessMode == JournalTask.EntryMode.migrate)
            {
                DateTime startDate = monthCalendar1.SelectionRange.Start;
                DateTime endDate = monthCalendar1.SelectionRange.End;

                for (DateTime i = startDate; i <= endDate; i = i.AddDays(1))
                {
                    string command = "insert into dailymain (taskdate) output inserted.taskid values (@taskDate)";

                    SqlParameter[] parameters = new SqlParameter[]
                    {
                        new SqlParameter("@taskDate", SqlDbType.Date) { Value = i}
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

                
            }
            else
            {

                string command = "insert into dailymain (taskdate) output inserted.taskid values (@taskDate)";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@taskDate", SqlDbType.Date) { Value = DateTime.Parse(txt_taskDate.Text)}
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


            if (!(accessMode == JournalTask.EntryMode.migrate))
            {
                Clear();
                list_taskList.Items.Clear();
                generalTasks.Clear();
            }

            main.RefreshGrid();

            if (accessMode == JournalTask.EntryMode.edit)
                this.Close();

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

            monthCalendar1.SelectionStart = DateTime.Now;


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

        private void GetMonthlyData(int id)
        {

            string commandString = "select taskdate " +
                                   "from monthlymain " +
                                   "where taskid = @taskId";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@taskId", SqlDbType.Int) { Value = id }

            };

            DataTable dailyMainTable = dbTools.GenericQueryAction(commandString, parameters);

            DataRow dailyMainContent = dailyMainTable.AsEnumerable().ToList()[0];

            monthCalendar1.SelectionStart = DateTime.Now;


            commandString = "select taskdescription, " +
                                   "taskisimportant," +
                                   "tasktype " +
                                   "from monthlydetail " +
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

        private void GetFutureData(int id)
        {

            string commandString = "select taskdate " +
                                   "from futuremain " +
                                   "where taskid = @taskId";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@taskId", SqlDbType.Int) { Value = id }

            };

            DataTable dailyMainTable = dbTools.GenericQueryAction(commandString, parameters);

            DataRow dailyMainContent = dailyMainTable.AsEnumerable().ToList()[0];

            monthCalendar1.SelectionStart = DateTime.Now;


            commandString = "select taskdescription, " +
                                   "taskisimportant," +
                                   "tasktype " +
                                   "from futuredetail " +
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

        private void GetCollectionData(int id)
        {

            string commandString = "select taskdateadded, " +
                                   "taskdescription, " +
                                   "taskisimportant," +
                                   "tasktype " +
                                   "from collectiontable " +
                                   "where taskid = @taskId";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@taskId", SqlDbType.Int) { Value = id }

            };

            DataTable dailyMainTable = dbTools.GenericQueryAction(commandString, parameters);

            DataRow dailyMainContent = dailyMainTable.AsEnumerable().ToList()[0];

            monthCalendar1.SelectionStart = dailyMainContent.Field<DateTime>("taskdateadded");


            GeneralTask generalTask = new GeneralTask();
            generalTask.TaskDescription = dailyMainContent.Field<string>("taskDescription");
            generalTask.TaskType = dailyMainContent.Field<int>("tasktype");
            generalTask.IsImportant = dailyMainContent.Field<bool>("taskisimportant");
            generalTasks.Add(generalTask);

            list_taskList.Items.Add(generalTask.TaskDescription);
            
        }
    }
}
