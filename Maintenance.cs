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
    public partial class Maintenance : Form
    {
        DBTools db;

        public Maintenance()
        {
            InitializeComponent();
            db = new DBTools(Properties.Settings.Default.DatabaseConnectionString);
            Populate_Notes();
        }

        private void Populate_Notes()
        {
            string command = "select * from notes";
            SqlParameter[] parameter = new SqlParameter[]
            {

            };

            dataGrid_content.DataSource = db.GenericQueryAction(command, parameter);
        }
    }
}
