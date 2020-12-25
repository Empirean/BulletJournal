using System.Windows.Forms;

namespace BulletJournal
{
    public partial class ConnectionManager : Form
    {

        public delegate void EventHandler();
        public event EventHandler OnConnectionChanged;

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
            OnConnectionChange();
            this.Close();
        }

        protected virtual void OnConnectionChange()
        {
            if (OnConnectionChanged != null)
                OnConnectionChanged();
        }
    }
}
