using System.Windows.Forms;

namespace BulletJournal
{
    public partial class ConnectionManager : Form
    {
        public ConnectionManager()
        {
            InitializeComponent();
            LoadConnectionString();
        }

        private void LoadConnectionString()
        {
            txt_ConnectionString.Text = Properties.Settings.Default.ConnectionString;
        }

        private void saveToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Properties.Settings.Default.ConnectionString = txt_ConnectionString.Text;
            Properties.Settings.Default.Save();
            this.Close();
        }
    }
}
