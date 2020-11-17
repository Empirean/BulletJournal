using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BulletJournal
{
    public partial class AddFutureLog : Form
    {
        List<GeneralTask> generalTasks = new List<GeneralTask>();
        DBTools dbTools;

        public AddFutureLog()
        {
            InitializeComponent();
        }

        private void AddFutureLog_Load(object sender, EventArgs e)
        {
            populateTaskYear();
            populateTaskMonth();
            cmb_taskYear.SelectedIndex = 0;
            cmb_taskMonth.SelectedIndex = DateTime.Now.Month - 1;
        }

        private void populateTaskYear()
        {
            for (int i = DateTime.Now.Year; i < DateTime.Now.Year + 5; i++)
            {
                cmb_taskYear.Items.Add(i);
            }
        }

        private void populateTaskMonth()
        {
            for (int i = 1; i < 13; i++)
            {
                cmb_taskMonth.Items.Add(DateTimeFormatInfo.CurrentInfo.GetMonthName(i));
            }
            

        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            list_taskList.Items.Clear();
            generalTasks.Clear();
            Clear();
        }

        private void Clear()
        {
            txt_description.Text = "";
            cmb_taskType.SelectedIndex = 0;
            chk_important.Checked = false;

        }

        private void btn_add_Click(object sender, EventArgs e)
        {

        }

        private bool IsInputValid()
        {
            txt_description.Text = txt_description.Text.Trim();
            if (txt_description.Text.Length > 0)
                return true;
            return false;
        }
    }
}
