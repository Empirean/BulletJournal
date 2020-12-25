using System;
using System.Windows.Forms;

namespace BulletJournal
{
    public partial class ControlPanel : Form
    {

        public delegate void EventHandler();
        public event EventHandler OnSettingsChanged;


        public ControlPanel()
        {
            InitializeComponent();
            ConfigureNotes();
        }

        private void ConfigureNotes()
        {
            // Daily
            chk_dailyDateAdded.Checked = Properties.Settings.Default.DailyDateAdded;
            chk_dailyDateChanged.Checked = Properties.Settings.Default.DailyDateChanged;
            chk_dailyIsImportant.Checked = Properties.Settings.Default.DailyTaskIsImportant;
            chk_dailyTaskType.Checked = Properties.Settings.Default.DailyTaskType;

            // Monthly
            chk_monthlyDateAdded.Checked = Properties.Settings.Default.MonthlyDateAdded;
            chk_monthlyDateChanged.Checked = Properties.Settings.Default.MonthlyDateChanged;
            chk_monthlyIsImportant.Checked = Properties.Settings.Default.MonthlyTaskIsImportant;
            chk_monthlyTaskType.Checked = Properties.Settings.Default.MonthlyTaskType;

            // Future
            chk_futureDateAdded.Checked = Properties.Settings.Default.FutureDateAdded;
            chk_futureDateChenged.Checked = Properties.Settings.Default.FutureDateChanged;
            chk_futureIsImportant.Checked = Properties.Settings.Default.FutureTaskIsImportant;
            chk_futureTaskType.Checked = Properties.Settings.Default.FutureTaskType;

            // Notes
            chk_noteDateChanged.Checked = Properties.Settings.Default.NotesDateChanged;
            chk_noteDateAdded.Checked = Properties.Settings.Default.NotesDateAdded;
        }

        private void chk_notedate_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.NotesDateChanged = chk_noteDateChanged.Checked;
            Save();
        }

        private void chk_notedateadded_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.NotesDateAdded = chk_noteDateAdded.Checked;
            Save();
        }

        private void chk_dailyDateChanged_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.DailyDateChanged = chk_dailyDateChanged.Checked;
            Save();
        }

        private void chk_dailyDateAdded_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.DailyDateAdded = chk_dailyDateAdded.Checked;
            Save();
        }

        private void chk_dailyIsImportant_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.DailyTaskIsImportant = chk_dailyIsImportant.Checked;
            Save();
        }

        private void chk_dailyTaskType_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.DailyTaskType = chk_dailyTaskType.Checked;
            Save();
        }

        private void chk_monthlyDateChanged_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.MonthlyDateChanged = chk_monthlyDateChanged.Checked;
            Save();
        }

        private void chk_monthlyDateAdded_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.MonthlyDateAdded = chk_monthlyDateAdded.Checked;
            Save();
        }

        

        private void chk_monthlyIsImportant_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.MonthlyTaskIsImportant = chk_monthlyIsImportant.Checked;
            Save();
        }

        private void chk_monthlyTaskType_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.MonthlyTaskType = chk_monthlyTaskType.Checked;
            Save();
        }

        private void Save()
        {
            Properties.Settings.Default.Save();
            OnSettingsChange();
        }

        protected virtual void OnSettingsChange()
        {
            if (OnSettingsChanged != null)
                OnSettingsChanged();
        }

        private void chk_futureDateChenged_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.FutureDateChanged = chk_futureDateChenged.Checked;
            Save();
        }

        private void chk_futureDateAdded_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.FutureDateAdded = chk_futureDateAdded.Checked;
            Save();
        }

        private void chk_futureIsImportant_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.FutureTaskIsImportant = chk_futureIsImportant.Checked;
            Save();
        }

        private void chk_futureTaskType_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.FutureTaskType = chk_futureTaskType.Checked;
            Save();
        }

        private void btn_connections_Click(object sender, EventArgs e)
        {
            using (ConnectionManager connection = new ConnectionManager())
            {
                connection.ShowDialog();
            }
        }
    }
}
