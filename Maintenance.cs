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
            Populate_Notes();
        }

        private void Populate_Notes()
        {
            string command = "select * from tracker";
            SqlParameter[] parameter = new SqlParameter[]
            {

            };

            dataGrid_content.DataSource = db.GenericQueryAction(command, parameter);
        }
    }
}
