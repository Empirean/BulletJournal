using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace BulletJournal
{
    public partial class CollectionList : Form
    {
        // Events
        public delegate void EventHandler();
        public event EventHandler OnCollectionSaved;

        // Database Tools
        DBTools db;

        // Journal Entry Mode
        JournalTask.EntryMode mode;

        // Ids
        int collectionMainId;
        int collectionDetailId;

        public CollectionList(JournalTask.EntryMode _mode, int _collectionMainId = -1, int _collectionDetailId = -1)
        {
            InitializeComponent();

            // Initialize Database Tools
            db = new DBTools(Properties.Settings.Default.DatabaseConnectionString);

            // Store Mode
            mode = _mode;

            // Store Ids
            collectionMainId = _collectionMainId;
            collectionDetailId = _collectionDetailId;

            // Edit Mode
            if (mode == JournalTask.EntryMode.edit)
            {
                this.Text = "Edit Collection";

                // Query the collection name
                string command = "select description " +
                                 "from collectiondetail " +
                                 "where collectionid = @detid " +
                                 "and maintaskforeignkey = @mainid";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@detid", SqlDbType.Int) { Value = collectionDetailId},
                    new SqlParameter("@mainid", SqlDbType.Int) { Value = collectionMainId}
                };

                DataTable dataTable = db.GenericQueryAction(command, parameters);
                DataRow dataRow = dataTable.AsEnumerable().ToList()[0];

                // set the textbox to collection name
                txt_collection.Text = dataRow.Field<string>("description");
                
            }
        }


        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

            // Input validation
            if (!(JournalTask.IsInputInvalid(txt_collection)))
                return;
            
            // Saving on Add Mode
            if (mode == JournalTask.EntryMode.add)
            {
                string command = "insert into collectiondetail " +
                                 "(description, maintaskforeignkey) " +
                                 "values" +
                                 "(@desc, @id)";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@desc", SqlDbType.NVarChar) { Value = txt_collection.Text},
                    new SqlParameter("@id", SqlDbType.Int) { Value = collectionMainId}
                };

                db.GenericNonQueryAction(command, parameters);
            }
            

            // Saving on Edit Mode
            if (mode == JournalTask.EntryMode.edit)
            {
                string command = "update collectiondetail " +
                                 "set " +
                                 "description = @desc " +
                                 "where collectionid = @detid " +
                                 "and maintaskforeignkey = @mainid";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@desc", SqlDbType.NVarChar) { Value = txt_collection.Text},
                    new SqlParameter("@detid", SqlDbType.Int) { Value = collectionDetailId},
                    new SqlParameter("@mainid", SqlDbType.Int) { Value = collectionMainId}
                };

                db.GenericNonQueryAction(command, parameters);
            }
            
            // Clean Up
            txt_collection.Text = "";

            // Publish Event
            OnCategorySave();

            // Close when on edit mode
            if (mode == JournalTask.EntryMode.edit)
                this.Close();
        }

        // Event Publisher
        protected virtual void OnCategorySave()
        {
            if (OnCollectionSaved != null)
                OnCollectionSaved();
        }
    }
}
