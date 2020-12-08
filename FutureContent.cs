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

        string title;

        public FutureContent(int _id, string _title)
        {
            InitializeComponent();

            // Initialize Database tools
            db = new DBTools(Properties.Settings.Default.DatabaseConnectionString);

            // SaveId
            futureMainid = _id;
            lbl_title.Text = _title;

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
                             "where maintaskforeignkey = @id " +
                             "and (taskdescription like @filter " +
                             "or case " +
                             "when tasktype = 0 then 'TASK' " +
                             "when tasktype = 1 then 'EVENT' " +
                             "when tasktype = 2 then 'NOTES' " +
                             "else 'CLOSED' end like @filter)";

            SqlParameter[] paramters = new SqlParameter[]
            {
                new SqlParameter("@id", SqlDbType.Int) { Value = _id},
                new SqlParameter("@filter", SqlDbType.NVarChar) { Value = '%' + txt_futureSearch.Text + '%' }
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
                title = dataGrid_content.SelectedRows[0].Cells[3].Value.ToString();
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

        private void dailyTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Migration migration = new Migration(JournalTask.EntryType.future,
                JournalTask.EntryType.daily,
                futureMainid,
                futureDetailId,
                JournalTask.EntryMode.migrate_detail,
                title))
            {
                migration.OnMigrated += OnRefreshGrids;
                migration.ShowDialog();

            }
        }

        private void monthlyTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Migration migration = new Migration(JournalTask.EntryType.future,
                JournalTask.EntryType.monthly,
                futureMainid,
                futureDetailId,
                JournalTask.EntryMode.migrate_detail,
                title))
            {
                migration.OnMigrated += OnRefreshGrids;
                migration.ShowDialog();

            }
        }

        private void futureLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Migration migration = new Migration(JournalTask.EntryType.future,
                JournalTask.EntryType.future,
                futureMainid,
                futureDetailId,
                JournalTask.EntryMode.migrate_detail,
                title))
            {
                migration.OnMigrated += OnRefreshGrids;
                migration.ShowDialog();

            }
        }

        private void dailyTaskToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (DailyDescription dailyDescription = new DailyDescription(JournalTask.EntryMode.migrate_detail, futureMainid, futureDetailId, JournalTask.EntryType.future))
            {
                dailyDescription.OnDailyMainSave += OnRefreshGrids;
                dailyDescription.ShowDialog();
            }
        }

        private void monthlyTaskToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (MonthlyDescription monthlyDescription = new MonthlyDescription(JournalTask.EntryMode.migrate_detail, futureMainid, futureDetailId, JournalTask.EntryType.future))
            {
                monthlyDescription.OnMonthlyMainSave += OnRefreshGrids;
                monthlyDescription.ShowDialog();
            }
        }

        private void futureLogToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (FutureDescription futureDescription = new FutureDescription(JournalTask.EntryMode.migrate_detail, futureMainid, futureDetailId, JournalTask.EntryType.future))
            {
                futureDescription.OnFutureMainSave += OnRefreshGrids;
                futureDescription.ShowDialog();
            }
        }

        private void txt_futureSearch_TextChanged(object sender, EventArgs e)
        {
            OnFutureSaved();
        }
    }
}