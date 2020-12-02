using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BulletJournal
{

    public partial class CollectionContent : Form
    {
        // Events
        public delegate void EventHandler();
        public event EventHandler OnRefreshGrid;

        // Database tools
        DBTools db;

        // Ids
        int collectionMainid;
        int collectionDetailId;


        public CollectionContent(int _id)
        {
            InitializeComponent();

            // Initialize Database tools
            db = new DBTools(Properties.Settings.Default.DatabaseConnectionString);

            // SaveId
            collectionMainid = _id;

            // Fill Grid
            Populate_Content(collectionMainid);
        }

        private void Populate_Content(int _id)
        {
            // Queries all the collectionname
            string command = "select " +
                             "collectionid, " +
                             "description as Collection " +
                             "from collectiondetail " +
                             "where maintaskforeignkey = @id";

            SqlParameter[] paramters = new SqlParameter[]
            {
                new SqlParameter("@id", SqlDbType.Int) { Value = _id}
            };

            dataGrid_content.DataSource = db.GenericQueryAction(command, paramters);

            // format grid
            dataGrid_content.Columns[0].Visible = false;
            dataGrid_content.Columns[0].Width = 1;
            dataGrid_content.Columns["Collection"].Width = 359;
        }

        private void btn_addCollection_Click(object sender, EventArgs e)
        {
            using (Collection collection = new Collection(JournalTask.EntryMode.add, collectionMainid))
            {
                // Subscribe to save event
                collection.OnCollectionSaved += this.OnCollectionSaved;
                collection.ShowDialog();
            }
        }

        private void dataGrid_content_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // Store collection id and show contextmenu
                collectionDetailId = JournalTask.ContextMenuHandler(dataGrid_content, contextMenuStrip1, e);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string command = "delete from collectiondetail " +
                             "where collectionid = @id";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@id", SqlDbType.Int) { Value = collectionDetailId}
            };

            db.GenericNonQueryAction(command, parameters);

            // Publish Event
            OnCollectionSaved();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Collection collection = new Collection(JournalTask.EntryMode.edit, collectionMainid, collectionDetailId))
            {
                // Subscribe to save event
                collection.OnCollectionSaved += this.OnCollectionSaved;
                collection.ShowDialog();
            }
        }

        // Save Event Handler
        private void OnCollectionSaved()
        {
            Populate_Content(collectionMainid);
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
