﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BulletJournal
{
    public partial class FutureContent : Form
    {
        // Events
        public delegate void EventHandler();
        public event EventHandler OnRefreshGrid;

        // Database tools
        DBTools db;

        // Ids
        int futureMainid;
        int futureDetailId;

        public FutureContent(int _id)
        {
            InitializeComponent();

            // Initialize Database tools
            db = new DBTools(Properties.Settings.Default.DatabaseConnectionString);

            // SaveId
            futureMainid = _id;

            // Fill Grid
            Populate_Content(futureMainid);
        }

        private void btn_addCollection_Click(object sender, EventArgs e)
        {
            
            using (FutureTaskList futureTaskList = new FutureTaskList(JournalTask.EntryMode.add, futureMainid, futureDetailId))
            {
                // Subscribe to save event
                futureTaskList.OnMonthlySaved += this.OnFutureSaved;
                futureTaskList.ShowDialog();
            }
            
        }

        private void Populate_Content(int _id)
        {
            // Queries all the collectionname

            string command = "select " +
                             "taskid, " +
                             "case " +
                             "when taskisimportant = 1 " +
                             "then '*' " +
                             "else '' end as [I], " +
                             "case " +
                             "when tasktype = 0 then 'TASK' " +
                             "when tasktype = 1 then 'EVENT' " +
                             "when tasktype = 2 then 'NOTES' " +
                             "else 'CLOSED' end as [Type], " +
                             "taskdescription as Description " +
                             "from futuredetail " +
                             "where maintaskforeignkey = @id";

            SqlParameter[] paramters = new SqlParameter[]
            {
                new SqlParameter("@id", SqlDbType.Int) { Value = _id}
            };

            dataGrid_content.DataSource = db.GenericQueryAction(command, paramters);

            // format grid
            dataGrid_content.Columns[0].Visible = false;
            dataGrid_content.Columns[0].Width = 1;
            dataGrid_content.Columns["I"].Width = 30;
            dataGrid_content.Columns["Type"].Width = 60;
            dataGrid_content.Columns["Description"].Width = 268;
        }

        private void dataGrid_content_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            
            if (e.Button == MouseButtons.Right)
            {
                // Store collection id and show contextmenu
                futureDetailId = JournalTask.ContextMenuHandler(dataGrid_content, contextMenuStrip1, e);
            }
            
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            using (FutureTaskList futureTaskList = new FutureTaskList(JournalTask.EntryMode.edit, futureMainid, futureDetailId))
            {
                // Subscribe to save event
                futureTaskList.OnMonthlySaved += this.OnFutureSaved;
                futureTaskList.ShowDialog();
            }
            
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string command = "delete from futuredetail " +
                            "where taskid = @id";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@id", SqlDbType.Int) { Value = futureDetailId}
            };

            db.GenericNonQueryAction(command, parameters);

            // Publish Event
            OnFutureSaved();
        }

        private void OnFutureSaved()
        {
            Populate_Content(futureMainid);
            OnRefreshGrids();
        }

        // Event Publisher
        protected virtual void OnRefreshGrids()
        {
            if (OnRefreshGrid != null)
                OnRefreshGrid();
        }
    }
}
