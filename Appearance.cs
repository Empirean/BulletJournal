using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BulletJournal
{
    public partial class Appearance : Form
    {
        public Appearance()
        {
            InitializeComponent();
        }

        private void btn_defaulBackColor_Click(object sender, EventArgs e)
        {
            this.BackColor = SystemColors.Control;
        }
    }
}
