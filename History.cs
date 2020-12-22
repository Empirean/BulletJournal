using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace BulletJournal
{
    public partial class History : Form
    {

        public delegate void EventHandler();
        public event EventHandler OnTaskUndone;

        JournalTask.EntryType entryType;
        DBTools db;
        
        int layer;
        int currentId;
        int selectedId;
        string title;

        public History(JournalTask.EntryType _entryType, string _title, int _layer = 0, int _currentId = 0)
        {
            InitializeComponent();

            db = new DBTools(Properties.Settings.Default.DatabaseConnectionString);
            entryType = _entryType;
            layer = _layer;
            currentId = _currentId;
            lbl_title.Text = _title;

            DataGridViewButtonColumn button = new DataGridViewButtonColumn();
            button.Name = "btn_undo";
            button.HeaderText = "Action";
            button.Text = "Undo";
            button.UseColumnTextForButtonValue = true;
            dataGrid_content.Columns.Add(button);

            GridController(entryType);
        }

        private void GridController(JournalTask.EntryType entryType)
        {
            switch (entryType)
            {
                case JournalTask.EntryType.daily:
                    Populate_DailyTasks();
                    break;
                case JournalTask.EntryType.monthly:
                    Populate_MonthlyTasks();
                    break;
                case JournalTask.EntryType.future:
                    Populate_FutureTasks();
                    break;
                default:
                    break;
            }
        }

        private void Populate_DailyTasks()
        {
            List<int> layerIds = JournalTask.GetAllIdInLayer(layer, entryType);
            List<int> validIds = new List<int>();

            string command = "select count(*) from currenttasks " +
                             "where id = @id " +
                             "and datecompleted is not null";

            foreach (int layerId in layerIds)
            {
                List<int> taskIds = JournalTask.GetAllCurrentTasksId(layerId);
                int contentCounter = 0;
                foreach (int taskId in taskIds)
                {
                    SqlParameter[] parameter = new SqlParameter[]
                    {
                        new SqlParameter("@id", SqlDbType.Int) {  Value = taskId }
                    };

                    if (db.GenericQueryAction(command, parameter).AsEnumerable().ToList()[0].Field<int>(0) > 0)
                        contentCounter++;
                }

                if (contentCounter != 0)
                    validIds.Add(layerId);

            }

            
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
                      "case when a.iscompleted = 1 then  'Completed' " +
                      "else 'Incomplete' end as [Status], " +
                      "a.description as [Description], " +
                      "sum(case when b.datecompleted is not null and b.id is not null then 1 " +
                      "else 0 end) as [Contents], " +
                      "format(a.datecompleted, 'dd/MM/yyyy, hh:mm:ss tt') as [Date Completed], " +
                      "format(a.dateadded, 'dd/MM/yyyy, hh:mm:ss tt') as [Date Added], " +
                      "format(a.datechanged, 'dd/MM/yyyy, hh:mm:ss tt') as [Date Changed] " +
                      "from currenttasks as a " +
                      "left join currenttasks as b " +
                      "on a.id = b.previouslayerid " +
                      "where a.layerid = @layerid " +
                      "and a.id in ({0}) " +
                      "and a.description like @filter " +
                      "and a.previouslayerid = case " +
                      "when @layerid = 0 " +
                      "then a.previouslayerid " +
                      "else @currentid end " +
                      "group by a.id, " +
                      "case when a.iscompleted = 1 then  'Completed' " +
                      "else 'Incomplete' end, "+
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
                      "format(a.datecompleted, 'dd/MM/yyyy, hh:mm:ss tt'), " +
                      "format(a.dateadded, 'dd/MM/yyyy, hh:mm:ss tt'), " +
                      "format(a.datechanged, 'dd/MM/yyyy, hh:mm:ss tt')";

            command = String.Format(command, String.Join(",", validIds.Count > 0 ? validIds : new List<int>() { -1 }));

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@layerid", SqlDbType.Int) { Value = layer},
                new SqlParameter("@currentId", SqlDbType.Int) { Value = currentId },
                new SqlParameter("@filter", SqlDbType.NVarChar) { Value = '%' + txt_historySearch.Text + '%' }
            };

            dataGrid_content.Columns["btn_undo"].Width = 70;

            dataGrid_content.DataSource = db.GenericQueryAction(command, parameters);
            dataGrid_content.Columns["id"].Visible = false;
            dataGrid_content.Columns["id"].Width = 1;

            dataGrid_content.Columns["Status"].Width = 70;
            

            dataGrid_content.Columns["I"].Width = 30;
            dataGrid_content.Columns["I"].Visible = Properties.Settings.Default.DailyTaskIsImportant;

            dataGrid_content.Columns["Type"].Width = 60;
            dataGrid_content.Columns["Type"].Visible = Properties.Settings.Default.DailyTaskType;

            dataGrid_content.Columns["Description"].Width = 400;
            dataGrid_content.Columns["Description"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            dataGrid_content.Columns["Contents"].Width = 70;
            dataGrid_content.Columns["Date Completed"].Width = 150;

            dataGrid_content.Columns["Date Added"].Width = 150;
            dataGrid_content.Columns["Date Added"].Visible = Properties.Settings.Default.DailyDateAdded;

            dataGrid_content.Columns["Date Changed"].Width = 150;
            dataGrid_content.Columns["Date Changed"].Visible = Properties.Settings.Default.DailyDateChanged;

        }

        private void Populate_MonthlyTasks()
        {
            List<int> layerIds = JournalTask.GetAllIdInLayer(layer, entryType);
            List<int> validIds = new List<int>();

            string command = "select count(*) from monthlytasks " +
                             "where id = @id " +
                             "and datecompleted is not null";

            foreach (int layerId in layerIds)
            {
                List<int> taskIds = JournalTask.GetAllCurrentTasksId(layerId);
                int contentCounter = 0;
                foreach (int taskId in taskIds)
                {
                    SqlParameter[] parameter = new SqlParameter[]
                    {
                        new SqlParameter("@id", SqlDbType.Int) {  Value = taskId }
                    };

                    if (db.GenericQueryAction(command, parameter).AsEnumerable().ToList()[0].Field<int>(0) > 0)
                        contentCounter++;
                }

                if (contentCounter != 0)
                    validIds.Add(layerId);

            }


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
                      "case when a.iscompleted = 1 then  'Completed' " +
                      "else 'Incomplete' end as [Status], " +
                      "a.description as [Description], " +
                      "sum(case when b.datecompleted is not null and b.id is not null then 1 " +
                      "else 0 end) as [Contents], " +
                      "format(a.datecompleted, 'dd/MM/yyyy, hh:mm:ss tt') as [Date Completed], " +
                      "format(a.dateadded, 'dd/MM/yyyy, hh:mm:ss tt') as [Date Added], " +
                      "format(a.datechanged, 'dd/MM/yyyy, hh:mm:ss tt') as [Date Changed] " +
                      "from monthlytasks as a " +
                      "left join monthlytasks as b " +
                      "on a.id = b.previouslayerid " +
                      "where a.layerid = @layerid " +
                      "and a.id in ({0}) " +
                      "and a.description like @filter " +
                      "and a.previouslayerid = case " +
                      "when @layerid = 0 " +
                      "then a.previouslayerid " +
                      "else @currentid end " +
                      "group by a.id, " +
                      "case when a.iscompleted = 1 then  'Completed' " +
                      "else 'Incomplete' end, " +
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
                      "format(a.datecompleted, 'dd/MM/yyyy, hh:mm:ss tt'), " +
                      "format(a.dateadded, 'dd/MM/yyyy, hh:mm:ss tt'), " +
                      "format(a.datechanged, 'dd/MM/yyyy, hh:mm:ss tt')";

            command = String.Format(command, String.Join(",", validIds.Count > 0 ? validIds : new List<int>() { -1 }));

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@layerid", SqlDbType.Int) { Value = layer},
                new SqlParameter("@currentId", SqlDbType.Int) { Value = currentId },
                new SqlParameter("@filter", SqlDbType.NVarChar) { Value = '%' + txt_historySearch.Text + '%' }
            };

            dataGrid_content.Columns["btn_undo"].Width = 70;

            dataGrid_content.DataSource = db.GenericQueryAction(command, parameters);
            dataGrid_content.Columns["id"].Visible = false;
            dataGrid_content.Columns["id"].Width = 1;

            dataGrid_content.Columns["Status"].Width = 70;


            dataGrid_content.Columns["I"].Width = 30;
            dataGrid_content.Columns["I"].Visible = Properties.Settings.Default.DailyTaskIsImportant;

            dataGrid_content.Columns["Type"].Width = 60;
            dataGrid_content.Columns["Type"].Visible = Properties.Settings.Default.DailyTaskType;

            dataGrid_content.Columns["Description"].Width = 400;
            dataGrid_content.Columns["Description"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            dataGrid_content.Columns["Contents"].Width = 70;
            dataGrid_content.Columns["Date Completed"].Width = 150;

            dataGrid_content.Columns["Date Added"].Width = 150;
            dataGrid_content.Columns["Date Added"].Visible = Properties.Settings.Default.DailyDateAdded;

            dataGrid_content.Columns["Date Changed"].Width = 150;
            dataGrid_content.Columns["Date Changed"].Visible = Properties.Settings.Default.DailyDateChanged;

        }

        private void Populate_FutureTasks()
        {
            List<int> layerIds = JournalTask.GetAllIdInLayer(layer, entryType);
            List<int> validIds = new List<int>();

            string command = "select count(*) from futuretasks " +
                             "where id = @id " +
                             "and datecompleted is not null";

            foreach (int layerId in layerIds)
            {
                List<int> taskIds = JournalTask.GetAllCurrentTasksId(layerId);
                int contentCounter = 0;
                foreach (int taskId in taskIds)
                {
                    SqlParameter[] parameter = new SqlParameter[]
                    {
                        new SqlParameter("@id", SqlDbType.Int) {  Value = taskId }
                    };

                    if (db.GenericQueryAction(command, parameter).AsEnumerable().ToList()[0].Field<int>(0) > 0)
                        contentCounter++;
                }

                if (contentCounter != 0)
                    validIds.Add(layerId);

            }


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
                      "case when a.iscompleted = 1 then  'Completed' " +
                      "else 'Incomplete' end as [Status], " +
                      "a.description as [Description], " +
                      "sum(case when b.datecompleted is not null and b.id is not null then 1 " +
                      "else 0 end) as [Contents], " +
                      "format(a.datecompleted, 'dd/MM/yyyy, hh:mm:ss tt') as [Date Completed], " +
                      "format(a.dateadded, 'dd/MM/yyyy, hh:mm:ss tt') as [Date Added], " +
                      "format(a.datechanged, 'dd/MM/yyyy, hh:mm:ss tt') as [Date Changed] " +
                      "from futuretasks as a " +
                      "left join futuretasks as b " +
                      "on a.id = b.previouslayerid " +
                      "where a.layerid = @layerid " +
                      "and a.id in ({0}) " +
                      "and a.description like @filter " +
                      "and a.previouslayerid = case " +
                      "when @layerid = 0 " +
                      "then a.previouslayerid " +
                      "else @currentid end " +
                      "group by a.id, " +
                      "case when a.iscompleted = 1 then  'Completed' " +
                      "else 'Incomplete' end, " +
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
                      "format(a.datecompleted, 'dd/MM/yyyy, hh:mm:ss tt'), " +
                      "format(a.dateadded, 'dd/MM/yyyy, hh:mm:ss tt'), " +
                      "format(a.datechanged, 'dd/MM/yyyy, hh:mm:ss tt')";

            command = String.Format(command, String.Join(",", validIds.Count > 0 ? validIds : new List<int>() { -1 }));

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@layerid", SqlDbType.Int) { Value = layer},
                new SqlParameter("@currentId", SqlDbType.Int) { Value = currentId },
                new SqlParameter("@filter", SqlDbType.NVarChar) { Value = '%' + txt_historySearch.Text + '%' }
            };

            dataGrid_content.Columns["btn_undo"].Width = 70;

            dataGrid_content.DataSource = db.GenericQueryAction(command, parameters);
            dataGrid_content.Columns["id"].Visible = false;
            dataGrid_content.Columns["id"].Width = 1;

            dataGrid_content.Columns["Status"].Width = 70;


            dataGrid_content.Columns["I"].Width = 30;
            dataGrid_content.Columns["I"].Visible = Properties.Settings.Default.DailyTaskIsImportant;

            dataGrid_content.Columns["Type"].Width = 60;
            dataGrid_content.Columns["Type"].Visible = Properties.Settings.Default.DailyTaskType;

            dataGrid_content.Columns["Description"].Width = 400;
            dataGrid_content.Columns["Description"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            dataGrid_content.Columns["Contents"].Width = 70;
            dataGrid_content.Columns["Date Completed"].Width = 150;

            dataGrid_content.Columns["Date Added"].Width = 150;
            dataGrid_content.Columns["Date Added"].Visible = Properties.Settings.Default.DailyDateAdded;

            dataGrid_content.Columns["Date Changed"].Width = 150;
            dataGrid_content.Columns["Date Changed"].Visible = Properties.Settings.Default.DailyDateChanged;

        }


        private void dataGrid_content_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            
            if (e.ColumnIndex == dataGrid_content.Columns["btn_undo"].Index)
            {
                if (dataGrid_content.Rows[e.RowIndex].Cells[4].Value.ToString() != "Completed")
                {
                     JournalTask.DataGridViewCellVisibility(dataGrid_content.Rows[e.RowIndex].Cells["btn_undo"], false);
                    
                }
            }
        }

        private void dataGrid_content_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedId = JournalTask.ContentClickHandler(dataGrid_content, e);
            string command = "";

            if (entryType == JournalTask.EntryType.daily)
            {
                command = "update currenttasks " +
                            "set " +
                            "iscompleted = @iscompleted, " +
                            "datecompleted = @completeddate " +
                            "where id = @id";
            }
            if (entryType == JournalTask.EntryType.monthly)
            {
                command = "update monthlytasks " +
                            "set " +
                            "iscompleted = @iscompleted, " +
                            "datecompleted = @completeddate " +
                            "where id = @id";
            }
            if (entryType == JournalTask.EntryType.future)
            {
                command = "update futuretasks " +
                            "set " +
                            "iscompleted = @iscompleted, " +
                            "datecompleted = @completeddate " +
                            "where id = @id";
            }


            if (e.ColumnIndex == 0)
            {
                foreach (int pickedId in JournalTask.GetAllPreviousLayerId(selectedId, entryType))
                {
                    SqlParameter[] parameter = new SqlParameter[]
                    {
                        new SqlParameter("@id", SqlDbType.Int) { Value = pickedId},
                        new SqlParameter("@iscompleted", SqlDbType.Bit) { Value = false},
                        new SqlParameter("@completeddate", SqlDbType.DateTime) { Value = DBNull.Value}

                       
                    };

                    db.GenericNonQueryAction(command, parameter);
                }

                OnRefreshGrid();
            }
            
        }

        private void dataGrid_content_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right)
            {
                selectedId = JournalTask.ContextMenuHandler(dataGrid_content, contextMenuStrip1, e);
                title = dataGrid_content.SelectedRows[0].Cells[5].Value.ToString();
            }

            if (e.Button == MouseButtons.Left)
            {
                selectedId = JournalTask.ContextMenuHandler(dataGrid_content, contextMenuStrip1, e);
                if (selectedId != 0)
                    title = dataGrid_content.SelectedRows[0].Cells[5].Value.ToString();
                contextMenuStrip1.Hide();
            }
        }

        private void OnRefreshGrid()
        {
            
            GridController(entryType);
            OnTaskUndo();
            
        }

        protected virtual void OnTaskUndo()
        {
            if (OnTaskUndone != null)
                OnTaskUndone();
        }


        private void dataGrid_content_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int temp = 0;
            if (entryType == JournalTask.EntryType.daily)
                temp = JournalTask.GetCurrentTaskIds(selectedId).Count;
            if (entryType == JournalTask.EntryType.monthly)
                temp = JournalTask.GetMonthlyTaskIds(selectedId).Count;
            if (entryType == JournalTask.EntryType.future)
                temp = JournalTask.GetFutureTaskIds(selectedId).Count;

            if (temp == 0)
                return;


            using (History history = new History(JournalTask.EntryType.daily, title, layer + 1, selectedId))
            {
                history.OnTaskUndone += OnRefreshGrid;
                history.ShowDialog();
            }

        }
    }
}
