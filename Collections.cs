﻿using System;
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
    public partial class Collections : Form
    {
        DBTools dbTools;
        MainForm main;
        JournalTask.EntryMode accessMode;
        int taskId;


        public Collections(MainForm m, int id, JournalTask.EntryMode mode, JournalTask.EntryType c = JournalTask.EntryType.none)
        {
            InitializeComponent();
            dbTools = new DBTools(Properties.Settings.Default.DatabaseConnectionString);
            cmb_taskType.SelectedIndex = 0;

            main = m;
            taskId = id;

            accessMode = mode;

            if (accessMode == JournalTask.EntryMode.edit)
            {
                this.Text = "Edit Collection";
                GetCollectionData(id);
            }

        }

        public Collections(MainForm m)
        {
            InitializeComponent();
            dbTools = new DBTools(Properties.Settings.Default.DatabaseConnectionString);
            main = m;
            cmb_taskType.SelectedIndex = 0;

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
            string commandString;

            if (accessMode == JournalTask.EntryMode.edit)
            {
                parameters = new SqlParameter[]
                {
                    new SqlParameter("@desc", SqlDbType.NVarChar) { Value = txt_description.Text  },
                    new SqlParameter("@tasktype", SqlDbType.Int) { Value = JournalTask.GetTask(cmb_taskType.Text)},
                    new SqlParameter("@isImportant", SqlDbType.Bit) { Value = chk_important.Checked},
                    new SqlParameter("@taskid", SqlDbType.Int) { Value = taskId}
                };
                commandString = "update collectiontable " +
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
                commandString = "insert into collectiontable (taskdescription, tasktype, taskisimportant, taskDateAdded) " +
                                 "values (@desc, @tasktype, @isImportant, @taskDateAdded)";
            }


            dbTools.GenericNonQueryAction(commandString, parameters);
            main.Populate_collection();
            main.Populate_index();

            Clear();

            if (accessMode == JournalTask.EntryMode.edit)
                this.Close();

        }

        private bool IsInputValid()
        {
            txt_description.Text = txt_description.Text.Trim();
            if (txt_description.Text.Length > 0)
                return true;
            return false;
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

            DataTable collectionData = dbTools.GenericQueryAction(commandString, parameters);
            DataRow dataRow = collectionData.AsEnumerable().ToList()[0];

            txt_description.Text = dataRow.Field<string>("taskdescription");
            cmb_taskType.SelectedIndex = dataRow.Field<int>("tasktype");
            chk_important.Checked = dataRow.Field<bool>("taskisimportant");
        }

    }
}
