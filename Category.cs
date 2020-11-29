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
    public partial class Category : Form
    {
        DBTools db;
        JournalTask.EntryMode mode;
        MainForm main;
        int catId;

        public Category(MainForm _mainform,  int _catid, JournalTask.EntryMode _mode)
        {
            InitializeComponent();

            db = new DBTools(Properties.Settings.Default.DatabaseConnectionString);
            
            mode = _mode;
            catId = _catid;
            main = _mainform;

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
                                 "collectionname = (@desc)";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@desc", SqlDbType.NVarChar) { Value = txt_category.Text}
                };

                db.GenericNonQueryAction(command, parameters);
            }

            txt_category.Text = "";

            main.Populate_Collection();
        }

        private bool IsInputValid()
        {
            txt_category.Text = txt_category.Text.Trim();
            if (txt_category.Text.Length > 0)
                return true;
            return false;
        }
    }
}
