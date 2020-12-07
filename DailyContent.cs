using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BulletJournal
{
    public partial class DailyContent : Form
    {
        // Events
        public delegate void EventHandler();
        public event EventHandler OnRefreshGrid;

        // Database tools
        DBTools db;

        // Ids
        int dailyMainid;
        int dailyDetailId;

        public DailyContent(int _id, string _title)
        {
            InitializeComponent();

            // Initialize Database tools
            db = new DBTools(Properties.Settings.Default.DatabaseConnectionString);

            // SaveId
            dailyMainid = _id;
            lbl_title.Text = _title;

            // Fill Grid
            Populate_Content(dailyMainid);
        }

        private void btn_addCollection_Click(object sender, EventArgs e)
        {
            using (DailyTaskList dailyTaskList = new DailyTaskList(JournalTask.EntryMode.add, dailyMainid, dailyDetailId))
            {
                // Subscribe to save event
                dailyTaskList.OnDailySaved += this.OnDailySaved;
                dailyTaskList.ShowDialog();
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
                             "from dailydetail " +
                             "where maintaskforeignkey = @id " +
                             "and taskdescription like @filter " +
                             "or case " +
                             "when tasktype = 0 then 'TASK' " +
                             "when tasktype = 1 then 'EVENT' " +
                             "when tasktype = 2 then 'NOTES' " +
                             "else 'CLOSED' end like @filter";

            SqlParameter[] paramters = new SqlParameter[]
            {
                new SqlParameter("@id", SqlDbType.Int) { Value = _id},
                new SqlParameter("@filter", SqlDbType.NVarChar) { Value = '%' + txt_dailySearch.Text + '%' }
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
                dailyDetailId = JournalTask.ContextMenuHandler(dataGrid_content, contextMenuStrip1, e);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string command = "delete from dailydetail " +
                             "where taskid = @id";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@id", SqlDbType.Int) { Value = dailyDetailId}
            };

            db.GenericNonQueryAction(command, parameters);

            // Publish Event
            OnDailySaved();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            using (DailyTaskList dailyTaskList = new DailyTaskList(JournalTask.EntryMode.edit, dailyMainid, dailyDetailId))
            {
                // Subscribe to save event
                dailyTaskList.OnDailySaved += this.OnDailySaved;
                dailyTaskList.ShowDialog();
            }
            
        }

        private void OnDailySaved()
        {
            Populate_Content(dailyMainid);
            OnRefreshGrids();
        }

        // Event Publisher
        protected virtual void OnRefreshGrids()
        {
            if (OnRefreshGrid != null)
                OnRefreshGrid();
        }

        private void dailyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Migration migration = new Migration(JournalTask.EntryType.daily,
                JournalTask.EntryType.daily,
                dailyMainid,
                dailyDetailId,
                JournalTask.EntryMode.migrate_detail))
            {
                migration.OnMigrated += OnRefreshGrids;
                migration.ShowDialog();

            }
        }

        private void monthlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Migration migration = new Migration(JournalTask.EntryType.daily,
                JournalTask.EntryType.monthly,
                dailyMainid,
                dailyDetailId,
                JournalTask.EntryMode.migrate_detail))
            {
                migration.OnMigrated += OnRefreshGrids;
                migration.ShowDialog();

            }
        }

        private void futureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Migration migration = new Migration(JournalTask.EntryType.daily,
                JournalTask.EntryType.future,
                dailyMainid,
                dailyDetailId,
                JournalTask.EntryMode.migrate_detail))
            {
                migration.OnMigrated += OnRefreshGrids;
                migration.ShowDialog();

            }
        }

        private void dailyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (DailyDescription dailyDescription = new DailyDescription(JournalTask.EntryMode.migrate_detail, dailyMainid, dailyDetailId, JournalTask.EntryType.daily))
            {
                dailyDescription.OnDailyMainSave += OnRefreshGrids;
                dailyDescription.ShowDialog();
            }
        }

        private void monthlyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (MonthlyDescription monthlyDescription = new MonthlyDescription(JournalTask.EntryMode.migrate_detail, dailyMainid, dailyDetailId, JournalTask.EntryType.daily ))
            {
                monthlyDescription.OnMonthlyMainSave += OnRefreshGrids;
                monthlyDescription.ShowDialog();
            }
        }

        private void futureToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (FutureDescription futureDescription = new FutureDescription(JournalTask.EntryMode.migrate_detail, dailyMainid, dailyDetailId, JournalTask.EntryType.daily))
            {
                futureDescription.OnFutureMainSave += OnRefreshGrids;
                futureDescription.ShowDialog();
            }
        }

        private void txt_dailySearch_TextChanged(object sender, EventArgs e)
        {
            OnDailySaved();
        }
    }  
}
