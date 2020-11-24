﻿using System;
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
    public partial class MonthlyTask : Form
    {

        List<GeneralTask> generalTasks = new List<GeneralTask>();
        DBTools dbTools;
        MainForm main;
        JournalTask.EntryMode accessMode;

        int taskId;

        public MonthlyTask(MainForm m, int id, JournalTask.EntryMode mode, JournalTask.EntryType c = JournalTask.EntryType.none)
        {
            InitializeComponent();

            dbTools = new DBTools(Properties.Settings.Default.DatabaseConnectionString);
            populateTaskYear();
            populateTaskMonth();

            main = m;
            taskId = id;

            accessMode = mode;

            if (accessMode == JournalTask.EntryMode.edit)
            {
                this.Text = "Edit Monthly Task";
                GetMonthlyData(taskId);
            }
            if (accessMode == JournalTask.EntryMode.migrate)
            {
                this.Text = "Migrate Monthly Task";

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

        public MonthlyTask(MainForm m)
        {
            InitializeComponent();

            dbTools = new DBTools(Properties.Settings.Default.DatabaseConnectionString);
            populateTaskYear();
            populateTaskMonth();

            main = m;
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

            if (accessMode == JournalTask.EntryMode.edit)
            {
                
                string updateCommand = "update monthlymain " +
                                 "set taskdate = @taskDate " +
                                 "where taskid = @taskid";

                SqlParameter[] updateParameters = new SqlParameter[]
                {
                    new SqlParameter("@taskDate", SqlDbType.Date) { Value = DateTime.Parse(taskDate)},
                    new SqlParameter("@taskid", SqlDbType.Int) { Value = taskId}
                };

                dbTools.GenericNonQueryAction(updateCommand, updateParameters);

                string deleteCommand = "delete from monthlydetail " +
                          "where maintaskforeignkey = @taskid";

                SqlParameter[] deleteParameters = new SqlParameter[]
                {
                    new SqlParameter("@taskid", SqlDbType.Int) { Value = taskId}
                };

                dbTools.GenericNonQueryAction(deleteCommand, deleteParameters);

                string insertCommand = "insert into monthlydetail (tasktype, taskdescription, taskisimportant, maintaskforeignkey) values " +
                              "(@tasktype, @taskdescription, @taskisimportant, @foreignkey)";

                foreach (GeneralTask taskItem in generalTasks)
                {

                    SqlParameter[] insertParameters = new SqlParameter[]
                    {
                        new SqlParameter("@tasktype", SqlDbType.Int) { Value = taskItem.TaskType},
                        new SqlParameter("@taskdescription", SqlDbType.NVarChar) { Value = taskItem.TaskDescription},
                        new SqlParameter("@taskisimportant", SqlDbType.Bit) { Value = taskItem.IsImportant},
                        new SqlParameter("@foreignkey", SqlDbType.Int) { Value = taskId }
                    };

                    dbTools.GenericNonQueryAction(insertCommand, insertParameters);
                }
            }
            else if (accessMode == JournalTask.EntryMode.migrate)
            {

                string mainCommand = "insert into monthlymain (taskdate) output inserted.taskid values (@taskDate)";

                SqlParameter[] mainParameters = new SqlParameter[]
                {
                new SqlParameter("@taskDate", SqlDbType.Date) { Value = DateTime.Parse(taskDate)}
                };

                int insertedId = dbTools.GenericScalarAction(mainCommand, mainParameters);

                string detailCommand = "insert into monthlydetail (tasktype, taskdescription, taskisimportant, maintaskforeignkey) values " +
                                "(@tasktype, @taskdescription, @taskisimportant, @foreignkey)";

                foreach (GeneralTask taskItem in generalTasks)
                {

                    SqlParameter[] detailParameters = new SqlParameter[]
                    {
                    new SqlParameter("@tasktype", SqlDbType.Int) { Value = taskItem.TaskType},
                    new SqlParameter("@taskdescription", SqlDbType.NVarChar) { Value = taskItem.TaskDescription},
                    new SqlParameter("@taskisimportant", SqlDbType.Bit) { Value = taskItem.IsImportant},
                    new SqlParameter("@foreignkey", SqlDbType.Int) { Value = insertedId }
                    };

                    dbTools.GenericNonQueryAction(detailCommand, detailParameters);
                }
                

            }
            else
            {

                string mainCommand = "insert into monthlymain (taskdate) output inserted.taskid values (@taskDate)";

                SqlParameter[] mainParameters = new SqlParameter[]
                {
                    new SqlParameter("@taskDate", SqlDbType.Date) { Value = DateTime.Parse(taskDate)}
                };

                int insertedId = dbTools.GenericScalarAction(mainCommand, mainParameters);

                string detailCommand = "insert into monthlydetail (tasktype, taskdescription, taskisimportant, maintaskforeignkey) values " +
                              "(@tasktype, @taskdescription, @taskisimportant, @foreignkey)";

                foreach (GeneralTask taskItem in generalTasks)
                {

                    SqlParameter[] detailParameters = new SqlParameter[]
                    {
                        new SqlParameter("@tasktype", SqlDbType.Int) { Value = taskItem.TaskType},
                        new SqlParameter("@taskdescription", SqlDbType.NVarChar) { Value = taskItem.TaskDescription},
                        new SqlParameter("@taskisimportant", SqlDbType.Bit) { Value = taskItem.IsImportant},
                        new SqlParameter("@foreignkey", SqlDbType.Int) { Value = insertedId }
                    };

                    dbTools.GenericNonQueryAction(detailCommand, detailParameters);
                }
            }

            if (!(accessMode == JournalTask.EntryMode.migrate))
            {
                Clear();
                list_taskList.Items.Clear();
                generalTasks.Clear();
            }

            main.RefreshGrid();

            if (accessMode == JournalTask.EntryMode.edit || accessMode == JournalTask.EntryMode.migrate)
                this.Close();

        }

        private void list_taskList_MouseUp(object sender, MouseEventArgs e)
        {
            btn_edit.Text = "Edit";
        }

        private void GetDailyData(int id)
        {
            string mainCommand = "select taskdate " +
                                   "from dailymain " +
                                   "where taskid = @taskId";

            SqlParameter[] mainParameters = new SqlParameter[]
            {
                new SqlParameter("@taskId", SqlDbType.Int) { Value = id }

            };

            DataTable monthlyMainTable = dbTools.GenericQueryAction(mainCommand, mainParameters);

            DataRow monthlyMainContent = monthlyMainTable.AsEnumerable().ToList()[0];

            cmb_taskMonth.SelectedIndex = monthlyMainContent.Field<DateTime>("taskdate").Month - 1;

            int y = monthlyMainContent.Field<DateTime>("taskdate").Year;

            if (!(cmb_taskYear.Items.Contains(y.ToString())))
            {
                cmb_taskYear.Items.Insert(0, y.ToString());
                cmb_taskYear.SelectedIndex = 0;
            }
            else
                cmb_taskYear.SelectedItem = y.ToString();


            string detailCommand = "select taskdescription, " +
                                   "taskisimportant," +
                                   "tasktype " +
                                   "from dailydetail " +
                                   "where maintaskforeignkey = @taskId";

            SqlParameter[] detailParameters = new SqlParameter[]
            {
                new SqlParameter("@taskId", SqlDbType.Int) { Value = id }

            };

            DataTable dailyDetailTable = dbTools.GenericQueryAction(detailCommand, detailParameters);
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
            string mainCommand = "select taskdate " +
                                   "from monthlymain " +
                                   "where taskid = @taskId";

            SqlParameter[] mainParameters = new SqlParameter[]
            {
                new SqlParameter("@taskId", SqlDbType.Int) { Value = id }

            };

            DataTable monthlyMainTable = dbTools.GenericQueryAction(mainCommand, mainParameters);

            DataRow monthlyMainContent = monthlyMainTable.AsEnumerable().ToList()[0];

            cmb_taskMonth.SelectedIndex = monthlyMainContent.Field<DateTime>("taskdate").Month - 1;
            
            int y = monthlyMainContent.Field<DateTime>("taskdate").Year;

            if (!(cmb_taskYear.Items.Contains(y.ToString())))
            {
                cmb_taskYear.Items.Insert(0, y.ToString());
                cmb_taskYear.SelectedIndex = 0;
            }
            else
                cmb_taskYear.SelectedItem = y.ToString();


            string detailCommand = "select taskdescription, " +
                                   "taskisimportant," +
                                   "tasktype " +
                                   "from monthlydetail " +
                                   "where maintaskforeignkey = @taskId";

            SqlParameter[] detailParameters = new SqlParameter[]
            {
                new SqlParameter("@taskId", SqlDbType.Int) { Value = id }

            };

            DataTable dailyDetailTable = dbTools.GenericQueryAction(detailCommand, detailParameters);
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
            string mainCommand = "select taskdate " +
                                   "from futuremain " +
                                   "where taskid = @taskId";

            SqlParameter[] mainParameters = new SqlParameter[]
            {
                new SqlParameter("@taskId", SqlDbType.Int) { Value = id }

            };

            DataTable monthlyMainTable = dbTools.GenericQueryAction(mainCommand, mainParameters);

            DataRow monthlyMainContent = monthlyMainTable.AsEnumerable().ToList()[0];

            cmb_taskMonth.SelectedIndex = monthlyMainContent.Field<DateTime>("taskdate").Month - 1;

            int y = monthlyMainContent.Field<DateTime>("taskdate").Year;

            if (!(cmb_taskYear.Items.Contains(y.ToString())))
            {
                cmb_taskYear.Items.Insert(0, y.ToString());
                cmb_taskYear.SelectedIndex = 0;
            }
            else
                cmb_taskYear.SelectedItem = y.ToString();


            string detailCommand = "select taskdescription, " +
                                   "taskisimportant," +
                                   "tasktype " +
                                   "from futuredetail " +
                                   "where maintaskforeignkey = @taskId";

            SqlParameter[] detailParameters = new SqlParameter[]
            {
                new SqlParameter("@taskId", SqlDbType.Int) { Value = id }

            };

            DataTable dailyDetailTable = dbTools.GenericQueryAction(detailCommand, detailParameters);
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
                                   "tasktype, " +
                                   "taskisimportant " +
                                   "from collectiontable " +
                                   "where taskid = @taskId";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@taskId", SqlDbType.Int) { Value = id }

            };

            DataTable monthlyMainTable = dbTools.GenericQueryAction(commandString, parameters);

            DataRow monthlyMainContent = monthlyMainTable.AsEnumerable().ToList()[0];

            cmb_taskMonth.SelectedIndex = monthlyMainContent.Field<DateTime>("taskdateadded").Month - 1;

            int y = monthlyMainContent.Field<DateTime>("taskdateadded").Year;

            if (!(cmb_taskYear.Items.Contains(y.ToString())))
            {
                cmb_taskYear.Items.Insert(0, y.ToString());
                cmb_taskYear.SelectedIndex = 0;
            }
            else
                cmb_taskYear.SelectedItem = y.ToString();


            GeneralTask generalTask = new GeneralTask();
            generalTask.TaskDescription = monthlyMainContent.Field<string>("taskDescription");
            generalTask.TaskType = monthlyMainContent.Field<int>("tasktype");
            generalTask.IsImportant = monthlyMainContent.Field<bool>("taskisimportant");
            generalTasks.Add(generalTask);

            list_taskList.Items.Add(generalTask.TaskDescription);
            
        }
    }
}
