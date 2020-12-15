using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace BulletJournal
{

    public partial class CollectionDescription : Form
    {
        // Events
        public delegate void EventHandler();
        public event EventHandler OnCategorySaved;

        // Database Tools
        DBTools db;

        // Journal Entry Mode
        JournalTask.EntryMode mode;

        // Id
        int categoryId;

        public CollectionDescription(JournalTask.EntryMode _mode, int _catid = -1 )
        {
            InitializeComponent();

            // Initialize Database Tools
            db = new DBTools(Properties.Settings.Default.DatabaseConnectionString);
            
            // store mode
            mode = _mode;

            // store categoryId
            categoryId = _catid;

            // Edit Mode
            if (mode == JournalTask.EntryMode.edit)
            {

                this.Text = "Edit Category";

                // Query the category name
                string command = "select collectionname " +
                                 "from collectionmain " +
                                 "where collectionid = @id";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@id", SqlDbType.Int) { Value = categoryId}
                };

                DataTable dataTable = db.GenericQueryAction(command,parameters);
                DataRow dataRow = dataTable.AsEnumerable().ToList()[0];

                // Set the textbox to the category name
                txt_category.Text = dataRow.Field<string>("collectionname");
                
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

            // Input validation
            if (!(JournalTask.IsInputInvalid(txt_category)))
                return;

            // Saving on Add Mode
            if (mode == JournalTask.EntryMode.add)
            {
                string command = "insert into collectionmain " +
                                 "(collectionname) " +
                                 "values" +
                                 "(@desc)";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@desc", SqlDbType.NVarChar) { Value = txt_category.Text}
                };

                db.GenericNonQueryAction(command, parameters);
            }


            // Saving on Edit Mode
            if (mode == JournalTask.EntryMode.edit)
            {
                string command = "update collectionmain " +
                                 "set " +
                                 "collectionname = @desc " +
                                 "where collectionid = @id";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@desc", SqlDbType.NVarChar) { Value = txt_category.Text},
                    new SqlParameter("@id", SqlDbType.Int) { Value = categoryId}
                };

                db.GenericNonQueryAction(command, parameters);
            }

            // Cleanup
            txt_category.Text = "";

            // Broadcast the event
            OnCategorySave();

            // Close when on edit mode
            if (mode == JournalTask.EntryMode.edit)
                this.Close();
        }


        // Event Publisher
        protected virtual void OnCategorySave()
        {
            if (OnCategorySaved != null)
                OnCategorySaved();
        }
    }
}
