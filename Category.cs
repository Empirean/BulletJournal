using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace BulletJournal
{

    public partial class Category : Form
    {
        public delegate void EventHandler();
        public event EventHandler OnCategorySaved;

        DBTools db;
        JournalTask.EntryMode mode;

        int catId;

        public Category(JournalTask.EntryMode _mode, int _catid = -1 )
        {
            InitializeComponent();

            db = new DBTools(Properties.Settings.Default.DatabaseConnectionString);
            
            mode = _mode;
            catId = _catid;

            if (mode == JournalTask.EntryMode.edit)
            {
                string command = "select collectionname " +
                                 "from collectionmain " +
                                 "where collectionid = @id";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@id", SqlDbType.Int) { Value = catId}
                };

                DataTable dataTable = db.GenericQueryAction(command,parameters);
                DataRow dataRow = dataTable.AsEnumerable().ToList()[0];
                txt_category.Text = dataRow.Field<string>("collectionname");
                this.Text = "Edit Category";
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!(IsInputValid()))
                return;

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

            if (mode == JournalTask.EntryMode.edit)
            {
                string command = "update collectionmain " +
                                 "set " +
                                 "collectionname = @desc " +
                                 "where collectionid = @id";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@desc", SqlDbType.NVarChar) { Value = txt_category.Text},
                    new SqlParameter("id", SqlDbType.Int) { Value = catId}
                };

                db.GenericNonQueryAction(command, parameters);
            }

            txt_category.Text = "";

            OnCategorySave();

            if (mode == JournalTask.EntryMode.edit)
                this.Close();
        }

        private bool IsInputValid()
        {
            if (txt_category.Text.Trim().Length > 0)
                return true;
            return false;
        }

        protected virtual void OnCategorySave()
        {
            if (OnCategorySaved != null)
                OnCategorySaved();
        }
    }
}
