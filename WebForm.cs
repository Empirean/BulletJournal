using System;
using System.Windows.Forms;

namespace BulletJournal
{
    public partial class WebForm : Form
    {
        public WebForm(string _query = "")
        {
            InitializeComponent();

            webBrowser1.Navigate(new Uri("http://www.google.com/search?q=" + _query));
            
        }

        private void backToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (webBrowser1.CanGoBack)
                webBrowser1.GoBack();
        }

        private void nextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (webBrowser1.CanGoForward)
                webBrowser1.GoForward();
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser1.Refresh();
        }

        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(new Uri("http://www.google.com/"));
        }
    }
}
