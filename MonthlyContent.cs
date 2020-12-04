using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BulletJournal
{
    public partial class MonthlyContent : Form
    {
        // Events
        public delegate void EventHandler();
        public event EventHandler OnRefreshGrid;

        // Database tools
        DBTools db;

        // Ids
        int monthlyMainid;
        int monthlyDetailId;

        public MonthlyContent(int _id)
        {
            InitializeComponent();

            // Initialize Database tools
            db = new DBTools(Properties.Settings.Default.DatabaseConnectionString);

            // SaveId
            monthlyMainid = _id;

            // Fill Grid
            Populate_Content(monthlyMainid);
        }

        private void btn_addCollection_Click(object sender, EventArgs e)
        {
            
            using (MonthlyTaskList monthlyTaskList = new MonthlyTaskList(JournalTask.EntryMode.add, monthlyMainid, monthlyDetailId))
            {
                // Subscribe to save event
                monthlyTaskList.OnMonthlySaved += this.OnMonthlySaved;
                monthlyTaskList.ShowDialog();
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
                             "from monthlydetail " +
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
                monthlyDetailId = JournalTask.ContextMenuHandler(dataGrid_content, contextMenuStrip1, e);
            }
        }

        

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            using (MonthlyTaskList monthlyTaskList = new MonthlyTaskList(JournalTask.EntryMode.edit, monthlyMainid, monthlyDetailId))
            {
                // Subscribe to save event
                monthlyTaskList.OnMonthlySaved += this.OnMonthlySaved;
                monthlyTaskList.ShowDialog();
            }
            
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string command = "delete from monthlydetail " +
                             "where taskid = @id";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@id", SqlDbType.Int) { Value = monthlyDetailId}
            };

            db.GenericNonQueryAction(command, parameters);

            // Publish Event
            OnMonthlySaved();
        }

        private void OnMonthlySaved()
        {
            Populate_Content(monthlyMainid);
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
