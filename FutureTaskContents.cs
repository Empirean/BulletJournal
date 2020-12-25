using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BulletJournal
{
    public partial class FutureTaskContents : Form
    {
        // Events
        public delegate void EventHandler();
        public event EventHandler OnRefreshGrid;

        DBTools db;

        int id;
        int layer;

        int selectedId;
        string title;

        public FutureTaskContents(int _id, int _layer, string _title)
        {
            InitializeComponent();

            db = new DBTools(Properties.Settings.Default.ConnectionString);

            lbl_title.Text = _title;

            id = _id;
            layer = _layer;

            Populate_Contents(id, layer);
        }

        // Event Publisher
        protected virtual void OnRefreshGrids()
        {
            if (OnRefreshGrid != null)
                OnRefreshGrid();
        }

        private void OnCurrentTaskSaved()
        {
            Populate_Contents(id, layer);
            OnRefreshGrids();
        }

        private void Populate_Contents(int _id, int _layer)
        {

            string command = "select " +
                            "a.id, " +
                            "a.iscompleted as [Status], " +
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
                            "and a.previouslayerid = @id " +
                            "and a.datecompleted is null " +
                            "group by a.id, " +
                            "a.iscompleted, " +
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
                            "format(a.datechanged, 'dd/MM/yyyy, hh:mm:ss tt') ";



            SqlParameter[] paramters = new SqlParameter[]
            {
                new SqlParameter("@id", SqlDbType.Int) { Value = _id},
                new SqlParameter("@layerid", SqlDbType.Int) { Value = _layer},
                new SqlParameter("@filter", SqlDbType.NVarChar) { Value = '%' + txt_collectionSearch.Text + '%' }
            };

            dataGrid_content.DataSource = db.GenericQueryAction(command, paramters);
            dataGrid_content.RowHeadersVisible = false;
            // format grid
            dataGrid_content.Columns[0].Visible = false;
            dataGrid_content.Columns[0].Width = 1;
            dataGrid_content.Columns["Status"].Width = 50;

            dataGrid_content.Columns["I"].Width = 30;
            dataGrid_content.Columns["I"].Visible = Properties.Settings.Default.FutureTaskIsImportant;

            dataGrid_content.Columns["Type"].Width = 60;
            dataGrid_content.Columns["Type"].Visible = Properties.Settings.Default.FutureTaskType;

            dataGrid_content.Columns["Description"].Width = 360;
            dataGrid_content.Columns["Description"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            dataGrid_content.Columns["Contents"].Width = 70;

            dataGrid_content.Columns["Date Added"].Width = 150;
            dataGrid_content.Columns["Date Added"].Visible = Properties.Settings.Default.FutureDateAdded;

            dataGrid_content.Columns["Date Changed"].Width = 150;
            dataGrid_content.Columns["Date Changed"].Visible = Properties.Settings.Default.FutureDateChanged;

        }

        private void btn_addDaily_Click(object sender, EventArgs e)
        {
            Add_FutureTask();
        }

        private void Add_FutureTask()
        {
            
            using (FutureTaskDescription notes = new FutureTaskDescription(JournalTask.EntryMode.add, id, layer))
            {
                notes.OnFutureTaskSaved += this.OnFutureTaskSave;
                notes.ShowDialog();
            }
            
        }

        private void OnFutureTaskSave()
        {
            Populate_Contents(id, layer);
            OnRefreshGrids();
        }

        private void FutureTaskContents_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.D)
            {
                Add_FutureTask();
            }

            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.E && selectedId != 0)
            {
                Edit();
            }
        }

        private void dataGrid_content_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            using (FutureTaskContents notes = new FutureTaskContents(selectedId, layer + 1, title))
            {
                notes.OnRefreshGrid += this.OnCurrentTaskSaved;
                notes.ShowDialog();
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Edit();
            
        }

        private void Edit()
        {

            using (FutureTaskDescription notesDescription = new FutureTaskDescription(JournalTask.EntryMode.edit, selectedId, layer))
            {
                notesDescription.OnFutureTaskSaved += OnFutureTaskSave;
                notesDescription.ShowDialog();
            }

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string command = "delete from futuretasks " +
                                 "where id = @ids";
            
            List<int> ids = JournalTask.GetAllFutureTasksId(selectedId);

            for (int i = 0; i < ids.Count; i++)
            {
                SqlParameter[] parameter = new SqlParameter[]
                {
                        new SqlParameter("@ids", SqlDbType.Int) { Value = ids[i]}
                };

                db.GenericNonQueryAction(command, parameter);
            }

            OnFutureTaskSave();
            
        }

        private void dataGrid_content_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // Store collection id and show contextmenu
                selectedId = JournalTask.ContextMenuHandler(dataGrid_content, contextMenuStrip1, e);
                title = dataGrid_content.SelectedRows[0].Cells[4].Value.ToString();
            }

            if (e.Button == MouseButtons.Left)
            {

                selectedId = JournalTask.ContextMenuHandler(dataGrid_content, contextMenuStrip1, e);
                if (selectedId != 0)
                    title = dataGrid_content.SelectedRows[0].Cells[4].Value.ToString();
                contextMenuStrip1.Hide();
            }

            
        }

        private void dailyTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (CurrentTaskDescription dailyDescription = new CurrentTaskDescription(JournalTask.EntryMode.migrate, selectedId, 0, JournalTask.EntryType.future))
            {
                dailyDescription.OnCurrentTaskSaved += OnFutureTaskSave;
                dailyDescription.ShowDialog();
            }
        }

        private void monthlyTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (MonthlyTaskDescription monthlyDescription = new MonthlyTaskDescription(JournalTask.EntryMode.migrate, selectedId, 0, JournalTask.EntryType.future))
            {
                monthlyDescription.OnMonthlyTaskSaved += OnFutureTaskSave;
                monthlyDescription.ShowDialog();
            }
        }

        private void futureLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FutureTaskDescription futureDescription = new FutureTaskDescription(JournalTask.EntryMode.migrate, selectedId, 0, JournalTask.EntryType.future))
            {
                futureDescription.OnFutureTaskSaved += OnFutureTaskSave;
                futureDescription.ShowDialog();
            }
        }

        private void dailyTaskToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (Migration migration = new Migration(JournalTask.EntryType.future, JournalTask.EntryType.daily, selectedId, title, 0))
            {
                migration.OnMigrated += OnFutureTaskSave;
                migration.ShowDialog();
            }
        }

        private void monthlyTaskToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (Migration migration = new Migration(JournalTask.EntryType.future, JournalTask.EntryType.monthly, selectedId, title, 0))
            {
                migration.OnMigrated += OnFutureTaskSave;
                migration.ShowDialog();
            }
        }

        private void futureLogToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (Migration migration = new Migration(JournalTask.EntryType.future, JournalTask.EntryType.future, selectedId, title, 0))
            {
                migration.OnMigrated += OnFutureTaskSave;
                migration.ShowDialog();
            }
        }

        private void dataGrid_content_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedId = JournalTask.ContentClickHandler(dataGrid_content, e);

            if (e.ColumnIndex == 1)
            {
                string command = "update futuretasks " +
                                 "set " +
                                 "iscompleted = @iscompleted, " +
                                 "datecompleted = @completeddate " +
                                 "where id = @id";

                List<int> ids = JournalTask.GetAllFutureTasksId(selectedId);

                for (int i = 0; i < ids.Count; i++)
                {
                    SqlParameter[] parameter = new SqlParameter[]
                    {
                    new SqlParameter("@id", SqlDbType.Int) { Value = ids[i]},
                    new SqlParameter("@iscompleted", SqlDbType.Bit) { Value = true},
                    new SqlParameter("@completeddate", SqlDbType.DateTime) { Value = DateTime.Now}
                    };

                    db.GenericNonQueryAction(command, parameter);
                }

                OnFutureTaskSave();

            }
        }

        private void quickSearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (WebForm web = new WebForm(title))
            {
                web.ShowDialog();
            }
        }

        private void dataGrid_content_SelectionChanged(object sender, EventArgs e)
        {
            selectedId = JournalTask.TabChangeHandler(dataGrid_content);
        }
    }
}
