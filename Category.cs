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

        public Category(string _description, JournalTask.EntryMode _mode, int _catid, MainForm _mainform)
        {
            InitializeComponent();

            db = new DBTools(Properties.Settings.Default.DatabaseConnectionString);

            txt_category.Text = _description;
            mode = _mode;
            catId = _catid;
            main = _mainform;
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
