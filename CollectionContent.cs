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

    public partial class CollectionContent : Form
    {

        public delegate void EventHandler();
        public event EventHandler OnRefreshGrid;

        DBTools db;
        MainForm category;

        int collectionMainid;
        int collectionDetailId;


        public CollectionContent(MainForm _category, int _id)
        {
            InitializeComponent();
            db = new DBTools(Properties.Settings.Default.DatabaseConnectionString);

            category = _category;
            collectionMainid = _id;
            Populate_Content(collectionMainid);
        }

        private void Populate_Content(int _id)
        {
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

            dataGrid_content.Columns[0].Visible = false;
            dataGrid_content.Columns[0].Width = 1;
            dataGrid_content.Columns["Collection"].Width = 359;
        }

        private void btn_addCollection_Click(object sender, EventArgs e)
        {
            using (Collection collection = new Collection(JournalTask.EntryMode.add, collectionMainid))
            {
                collection.OnCollectionSaved += this.OnCollectionSaved;
                collection.ShowDialog();
            }
        }

        public void OnCollectionSaved()
        {
            Populate_Content(collectionMainid);
            OnRefreshGrids();
        }


        protected virtual void OnRefreshGrids()
        {
            if (OnRefreshGrid != null)
                OnRefreshGrid();
        }

        private void dataGrid_content_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                collectionDetailId = ContextMenuHandler(dataGrid_content, contextMenuStrip1, e);
            }
        }

        private int ContextMenuHandler(DataGridView datagrid, ContextMenuStrip menu, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                datagrid.Rows[e.RowIndex].Selected = true;
                Rectangle cellRectangle = datagrid.GetCellDisplayRectangle(
                                                datagrid.Columns[e.ColumnIndex].Index,
                                                datagrid.Rows[e.RowIndex].Index,
                                                true);

                Point menuSpawnLocation = new Point(cellRectangle.Left, cellRectangle.Top);

                menu.Show(datagrid, menuSpawnLocation);
            }
            catch (Exception)
            {
            }

            return (int)datagrid.SelectedRows[0].Cells[0].Value;
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

            OnCollectionSaved();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Collection collection = new Collection(JournalTask.EntryMode.edit, collectionMainid, collectionDetailId))
            {
                collection.OnCollectionSaved += this.OnCollectionSaved;
                collection.ShowDialog();
            }
        }
    }
}
