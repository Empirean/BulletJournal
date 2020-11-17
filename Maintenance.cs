using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BulletJournal
{
    public partial class Maintenance : Form
    {
        DBTools db;

        public Maintenance()
        {
            InitializeComponent();
            db = new DBTools(Properties.Settings.Default.DatabaseConnectionString);
        }

        private void Maintenance_Load(object sender, EventArgs e)
        {
            RefreshGrid();

        }

        private void RefreshGrid()
        {
            // TODO: This line of code loads data into the 'journalDatabaseDataSet.DescriptionTable' table. You can move, or remove it, as needed.
            this.descriptionTableTableAdapter.Fill(this.journalDatabaseDataSet.DescriptionTable);
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            #region testcode

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@desc", SqlDbType.NVarChar) { Value = "test"  }
            };
            string command = "insert into descriptiontable (taskdescription) values (@desc)";

            db.GenericNonQueryAction(command,parameters);
            RefreshGrid();

            #endregion
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {

            //var taskIdDescription = dataGridView1.CurrentRow.Cells[dataGridView1.CurrentRow.Index].Value;
            //MessageBox.Show(taskIdDescription.ToString());
            //MessageBox.Show(dataGridView1.CurrentRow.Cells[0].Value.ToString());

        }

        private void dataGridView1_MouseUp(object sender, MouseEventArgs e)
        {
            MessageBox.Show(dataGridView1.CurrentRow.Cells[0].Value.ToString());
        }
    }
}
