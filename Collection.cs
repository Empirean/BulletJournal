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
    public partial class Collection : Form
    {

        public delegate void EventHandler();
        public event EventHandler OnCollectionSaved;

        DBTools db;
        JournalTask.EntryMode mode;

        int collectionMainId;
        int collectionDetailId;

        public Collection(JournalTask.EntryMode _mode, int _collectionMainId = -1, int _collectionDetailId = -1)
        {
            InitializeComponent();

            db = new DBTools(Properties.Settings.Default.DatabaseConnectionString);

            mode = _mode;

            collectionMainId = _collectionMainId;
            collectionDetailId = _collectionDetailId;


            if (mode == JournalTask.EntryMode.edit)
            {
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
                txt_collection.Text = dataRow.Field<string>("description");
                this.Text = "Edit Collection";
            }
        }

        private bool IsInputValid()
        {
            if (txt_collection.Text.Trim().Length > 0)
                return true;
            return false;
        }

        protected virtual void OnCategorySave()
        {
            if (OnCollectionSaved != null)
                OnCollectionSaved();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!(IsInputValid()))
                return;
            
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
            

            txt_collection.Text = "";

            OnCategorySave();

            if (mode == JournalTask.EntryMode.edit)
                this.Close();
        }
    }
}
