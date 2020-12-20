using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BulletJournal
{
    public partial class Migration : Form
    {
        public delegate void EventHandler();
        public event EventHandler OnMigrated;

        DBTools db;

        JournalTask.EntryType entryTypeFr;
        JournalTask.EntryType entryTypeTo;

        int sourceId;
        int layer;
        int currentId;

        public Migration
        (
            JournalTask.EntryType _entryTypeFr,
            JournalTask.EntryType _entryTypeTo,
            int _sourceId,
            string _title, 
            int _layer,
            int _currentId = 0
        )
        {
            InitializeComponent();

            db = new DBTools(Properties.Settings.Default.DatabaseConnectionString);

            entryTypeFr = _entryTypeFr;
            entryTypeTo = _entryTypeTo;
            sourceId = _sourceId;
            layer = _layer;
            currentId = _currentId;

            lbl_title.Text = _title;
            GridController(entryTypeTo, sourceId);

        }

        private void GridController(JournalTask.EntryType _entryType, int _id)
        {
            switch (_entryType)
            {
                case JournalTask.EntryType.daily:
                    Populate_DailyContent(_id);
                    break;
                case JournalTask.EntryType.monthly:
                    Populate_MonthlyContent(_id);
                    break;
                case JournalTask.EntryType.future:
                    Populate_FutureContent(_id);
                    break;
                default:
                    break;
            }
        }

        private void Populate_DailyContent(int _id)
        {
            string command;
            SqlParameter[] parameters;


            command = "select " +
                        "a.id, " +
                        "case " +
                        "when a.taskisimportant = 1 " +
                        "then '*' " +
                        "else '' end as [I], " +
                        "case " +
                        "when a.tasktype = 0 then 'TASK' " +
                        "when a.tasktype = 1 then 'EVENT' " +
                        "when a.tasktype = 2 then 'NOTES' " +
                        "else 'CLOSED' end as [Type], " +
                        "a.description as [Description], " +
                        "sum(case when b.datecompleted is null and b.id is not null then 1 " +
                        "else 0 end) as [Contents], " +
                        "format(a.dateadded, 'dd/MM/yyyy, hh:mm:ss tt') as [Date Added], " +
                        "format(a.datechanged, 'dd/MM/yyyy, hh:mm:ss tt') as [Date Changed] " +
                        "from currenttasks as a " +
                        "left join currenttasks as b " +
                        "on a.id = b.previouslayerid " +
                        "where a.layerid = @layerid " +
                        "and a.description like @filter " +
                        "and a.previouslayerid = case " +
                        "when @layerid = 0 " +
                        "then a.previouslayerid " +
                        "else @currentid end " +
                        //"and a.id <> @sourceid " +
                        "and a.datecompleted is null " +
                        "group by a.id, " +
                        "a.description, " +
                        "case " +
                        "when a.tasktype = 0 then 'TASK' " +
                        "when a.tasktype = 1 then 'EVENT' " +
                        "when a.tasktype = 2 then 'NOTES' " +
                        "else 'CLOSED' end, " +
                        "case " +
                        "when a.taskisimportant = 1 " +
                        "then '*' " +
                        "else '' end, " +
                        "a.description, " +
                        "format(a.dateadded, 'dd/MM/yyyy, hh:mm:ss tt'), " +
                        "format(a.datechanged, 'dd/MM/yyyy, hh:mm:ss tt')";

            parameters = new SqlParameter[]
            {
                new SqlParameter("@layerid", SqlDbType.Int) { Value = layer},
                new SqlParameter("@sourceid", SqlDbType.Int) { Value = sourceId },
                new SqlParameter("@currentId", SqlDbType.Int) { Value = currentId },
                new SqlParameter("@filter", SqlDbType.NVarChar) { Value = '%' + txt_migrationSearch.Text + '%' }
            };

            dataGrid_content.DataSource = db.GenericQueryAction(command, parameters);
            dataGrid_content.Columns[0].Visible = false;
            dataGrid_content.Columns[0].Width = 1;

            dataGrid_content.Columns["I"].Width = 30;
            dataGrid_content.Columns["I"].Visible = Properties.Settings.Default.DailyTaskIsImportant;

            dataGrid_content.Columns["Type"].Width = 60;
            dataGrid_content.Columns["Type"].Visible = Properties.Settings.Default.DailyTaskType;

            dataGrid_content.Columns["Description"].Width = 400;
            dataGrid_content.Columns["Description"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            dataGrid_content.Columns["Contents"].Width = 70;

            dataGrid_content.Columns["Date Added"].Width = 150;
            dataGrid_content.Columns["Date Added"].Visible = Properties.Settings.Default.DailyDateAdded;

            dataGrid_content.Columns["Date Changed"].Width = 150;
            dataGrid_content.Columns["Date Changed"].Visible = Properties.Settings.Default.DailyDateChanged;

        }
        
        private void Populate_MonthlyContent(int _id)
        {
            string command;
            SqlParameter[] parameters;


            command = "select " +
                        "a.id, " +
                        "case " +
                        "when a.taskisimportant = 1 " +
                        "then '*' " +
                        "else '' end as [I], " +
                        "case " +
                        "when a.tasktype = 0 then 'TASK' " +
                        "when a.tasktype = 1 then 'EVENT' " +
                        "when a.tasktype = 2 then 'NOTES' " +
                        "else 'CLOSED' end as [Type], " +
                        "a.description as [Description], " +
                        "sum(case when b.datecompleted is null and b.id is not null then 1 " +
                        "else 0 end) as [Contents], " +
                        "format(a.dateadded, 'dd/MM/yyyy, hh:mm:ss tt') as [Date Added], " +
                        "format(a.datechanged, 'dd/MM/yyyy, hh:mm:ss tt') as [Date Changed] " +
                        "from monthlytasks as a " +
                        "left join monthlytasks as b " +
                        "on a.id = b.previouslayerid " +
                        "where a.layerid = @layerid " +
                        "and a.description like @filter " +
                        "and a.previouslayerid = case " +
                        "when @layerid = 0 " +
                        "then a.previouslayerid " +
                        "else @currentid end " +
                        //"and a.id <> @sourceid " +
                        "and a.datecompleted is null " +
                        "group by a.id, " +
                        "a.description, " +
                        "case " +
                        "when a.tasktype = 0 then 'TASK' " +
                        "when a.tasktype = 1 then 'EVENT' " +
                        "when a.tasktype = 2 then 'NOTES' " +
                        "else 'CLOSED' end, " +
                        "case " +
                        "when a.taskisimportant = 1 " +
                        "then '*' " +
                        "else '' end, " +
                        "a.description, " +
                        "format(a.dateadded, 'dd/MM/yyyy, hh:mm:ss tt'), " +
                        "format(a.datechanged, 'dd/MM/yyyy, hh:mm:ss tt')";

            parameters = new SqlParameter[]
            {
                new SqlParameter("@layerid", SqlDbType.Int) { Value = layer},
                new SqlParameter("@sourceid", SqlDbType.Int) { Value = sourceId },
                new SqlParameter("@currentId", SqlDbType.Int) { Value = currentId },
                new SqlParameter("@filter", SqlDbType.NVarChar) { Value = '%' + txt_migrationSearch.Text + '%' }
            };

            dataGrid_content.DataSource = db.GenericQueryAction(command, parameters);
            dataGrid_content.Columns[0].Visible = false;
            dataGrid_content.Columns[0].Width = 1;

            dataGrid_content.Columns["I"].Width = 30;
            dataGrid_content.Columns["I"].Visible = Properties.Settings.Default.DailyTaskIsImportant;

            dataGrid_content.Columns["Type"].Width = 60;
            dataGrid_content.Columns["Type"].Visible = Properties.Settings.Default.DailyTaskType;

            dataGrid_content.Columns["Description"].Width = 400;
            dataGrid_content.Columns["Description"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            dataGrid_content.Columns["Contents"].Width = 70;

            dataGrid_content.Columns["Date Added"].Width = 150;
            dataGrid_content.Columns["Date Added"].Visible = Properties.Settings.Default.DailyDateAdded;

            dataGrid_content.Columns["Date Changed"].Width = 150;
            dataGrid_content.Columns["Date Changed"].Visible = Properties.Settings.Default.DailyDateChanged;

        }

        private void Populate_FutureContent(int _id)
        {
            string command;
            SqlParameter[] parameters;


            command = "select " +
                        "a.id, " +
                        "case " +
                        "when a.taskisimportant = 1 " +
                        "then '*' " +
                        "else '' end as [I], " +
                        "case " +
                        "when a.tasktype = 0 then 'TASK' " +
                        "when a.tasktype = 1 then 'EVENT' " +
                        "when a.tasktype = 2 then 'NOTES' " +
                        "else 'CLOSED' end as [Type], " +
                        "a.description as [Description], " +
                        "sum(case when b.datecompleted is null and b.id is not null then 1 " +
                        "else 0 end) as [Contents], " +
                        "format(a.dateadded, 'dd/MM/yyyy, hh:mm:ss tt') as [Date Added], " +
                        "format(a.datechanged, 'dd/MM/yyyy, hh:mm:ss tt') as [Date Changed] " +
                        "from futuretasks as a " +
                        "left join futuretasks as b " +
                        "on a.id = b.previouslayerid " +
                        "where a.layerid = @layerid " +
                        "and a.description like @filter " +
                        "and a.previouslayerid = case " +
                        "when @layerid = 0 " +
                        "then a.previouslayerid " +
                        "else @currentid end " +
                        //"and a.id <> @sourceid " +
                        "and a.datecompleted is null " +
                        "group by a.id, " +
                        "a.description, " +
                        "case " +
                        "when a.tasktype = 0 then 'TASK' " +
                        "when a.tasktype = 1 then 'EVENT' " +
                        "when a.tasktype = 2 then 'NOTES' " +
                        "else 'CLOSED' end, " +
                        "case " +
                        "when a.taskisimportant = 1 " +
                        "then '*' " +
                        "else '' end, " +
                        "a.description, " +
                        "format(a.dateadded, 'dd/MM/yyyy, hh:mm:ss tt'), " +
                        "format(a.datechanged, 'dd/MM/yyyy, hh:mm:ss tt')";

            parameters = new SqlParameter[]
            {
                new SqlParameter("@layerid", SqlDbType.Int) { Value = layer},
                new SqlParameter("@sourceid", SqlDbType.Int) { Value = sourceId },
                new SqlParameter("@currentId", SqlDbType.Int) { Value = currentId },
                new SqlParameter("@filter", SqlDbType.NVarChar) { Value = '%' + txt_migrationSearch.Text + '%' }
            };

            dataGrid_content.DataSource = db.GenericQueryAction(command, parameters);
            dataGrid_content.Columns[0].Visible = false;
            dataGrid_content.Columns[0].Width = 1;

            dataGrid_content.Columns["I"].Width = 30;
            dataGrid_content.Columns["I"].Visible = Properties.Settings.Default.DailyTaskIsImportant;

            dataGrid_content.Columns["Type"].Width = 60;
            dataGrid_content.Columns["Type"].Visible = Properties.Settings.Default.DailyTaskType;

            dataGrid_content.Columns["Description"].Width = 400;
            dataGrid_content.Columns["Description"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            dataGrid_content.Columns["Contents"].Width = 70;

            dataGrid_content.Columns["Date Added"].Width = 150;
            dataGrid_content.Columns["Date Added"].Visible = Properties.Settings.Default.DailyDateAdded;

            dataGrid_content.Columns["Date Changed"].Width = 150;
            dataGrid_content.Columns["Date Changed"].Visible = Properties.Settings.Default.DailyDateChanged;

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (entryTypeFr == JournalTask.EntryType.daily)
            {
                switch (entryTypeTo)
                {
                    case JournalTask.EntryType.daily:
                        MigrateDailyToDaily(sourceId);
                        break;
                    case JournalTask.EntryType.monthly:
                        MigrateDailyToMonthly(sourceId);
                        break;
                    case JournalTask.EntryType.future:
                        MigrateDailyToFuture(sourceId);
                        break;
                    default:
                        break;
                }
            }
            else if (entryTypeFr == JournalTask.EntryType.monthly)
            {
                switch (entryTypeTo)
                {
                    case JournalTask.EntryType.daily:
                        MigrateMonthlyToDaily(sourceId);
                        break;
                    case JournalTask.EntryType.monthly:
                        MigrateMonthlyToMonthly(sourceId);
                        break;
                    case JournalTask.EntryType.future:
                        MigrateMonthlyToFuture(sourceId);
                        break;
                    default:
                        break;
                }
            }
            else if (entryTypeFr == JournalTask.EntryType.future)
            {
                switch (entryTypeTo)
                {
                    case JournalTask.EntryType.daily:
                        MigrateFutureToDaily(sourceId);
                        break;
                    case JournalTask.EntryType.monthly:
                        MigrateFutureToMonthly(sourceId);
                        break;
                    case JournalTask.EntryType.future:
                        MigrateFutureToFuture(sourceId);
                        break;
                    default:
                        break;
                }
            }


            OnMigrate();
            this.Close();
        }

        private void MigrateDailyToDaily(int _id)
        {
            
            foreach (DataGridViewRow row in dataGrid_content.SelectedRows)
            {
                MigrationHelper.MigrateDailyToDaily(_id, (int) row.Cells[0].Value);
            }
            
        }

        private void MigrateDailyToMonthly(int _id)
        {
            
            foreach (DataGridViewRow row in dataGrid_content.SelectedRows)
            {
                MigrationHelper.MigrateDailyToMonthly(_id, (int)row.Cells[0].Value);
            }
            
        }

        private void MigrateDailyToFuture(int _id)
        {
            
            foreach (DataGridViewRow row in dataGrid_content.SelectedRows)
            {
                MigrationHelper.MigrateDailyToFuture(_id, (int)row.Cells[0].Value);
            }
            
        }

        private void MigrateMonthlyToDaily(int _id)
        {
            
            foreach (DataGridViewRow row in dataGrid_content.SelectedRows)
            {
                MigrationHelper.MigrateMonthlyToDaily(_id, (int)row.Cells[0].Value);
            }
            
        }

        private void MigrateMonthlyToMonthly(int _id)
        {
            
            foreach (DataGridViewRow row in dataGrid_content.SelectedRows)
            {
                MigrationHelper.MigrateMonthlyToMonthly(_id, (int)row.Cells[0].Value);
            }
            
        }

        private void MigrateMonthlyToFuture(int _id)
        {
            
            foreach (DataGridViewRow row in dataGrid_content.SelectedRows)
            {
                MigrationHelper.MigrateMonthlyToFuture(_id, (int)row.Cells[0].Value);
            }
            
        }

        private void MigrateFutureToDaily(int _id)
        {
            
            foreach (DataGridViewRow row in dataGrid_content.SelectedRows)
            {
                MigrationHelper.MigrateFutureToDaily(_id, (int)row.Cells[0].Value);
            }
            
        }

        private void MigrateFutureToMonthly(int _id)
        {
            
            foreach (DataGridViewRow row in dataGrid_content.SelectedRows)
            {
                MigrationHelper.MigrateFutureToMonthly(_id, (int)row.Cells[0].Value);
            }
            
        }

        private void MigrateFutureToFuture(int _id)
        {
            
            foreach (DataGridViewRow row in dataGrid_content.SelectedRows)
            {
                MigrationHelper.MigrateFutureToFuture(_id, (int)row.Cells[0].Value);
            }
            
        }

        protected virtual void OnMigrate()
        {
            if (OnMigrated != null)
                OnMigrated();
        }

        private void txt_migrationSearch_TextChanged(object sender, EventArgs e)
        {

            GridController(entryTypeTo, sourceId);
        }

        private void dataGrid_content_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((int) dataGrid_content.SelectedRows[0].Cells[4].Value == 0)
                return;
            string title = dataGrid_content.SelectedRows[0].Cells[3].Value.ToString();
            int nextId = (int)dataGrid_content.SelectedRows[0].Cells[0].Value;
            using (Migration migration = new Migration(entryTypeFr, entryTypeTo, sourceId, title, layer + 1, nextId))
            {
                migration.OnMigrated += OnMigrate;
                migration.ShowDialog();

            }
        }
    }
}
