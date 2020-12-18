﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BulletJournal
{
    public partial class MonthlyTasksContent : Form
    {

        // Events
        public delegate void EventHandler();
        public event EventHandler OnRefreshGrid;

        DBTools db;

        int id;
        int layer;

        int selectedId;

        public MonthlyTasksContent(int _id, int _layer, string _title)
        {
            InitializeComponent();

            db = new DBTools(Properties.Settings.Default.DatabaseConnectionString);

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

        private void OnMonthlyTaskSaved()
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
                            "count(b.id) as [Contents], " +
                            "format(a.dateadded, 'dd/MM/yyyy, hh:mm:ss tt') as [Date Added], " +
                            "format(a.datechanged, 'dd/MM/yyyy, hh:mm:ss tt') as [Date Changed] " +
                            "from monthlytasks as a " +
                            "left join monthlytasks as b " +
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

            // format grid
            dataGrid_content.Columns[0].Visible = false;
            dataGrid_content.Columns[0].Width = 1;
            dataGrid_content.Columns["Status"].Width = 50;

            dataGrid_content.Columns["I"].Width = 30;
            dataGrid_content.Columns["I"].Visible = Properties.Settings.Default.MonthlyTaskIsImportant;

            dataGrid_content.Columns["Type"].Width = 60;
            dataGrid_content.Columns["Type"].Visible = Properties.Settings.Default.MonthlyTaskType;

            dataGrid_content.Columns["Description"].Width = 350;
            dataGrid_content.Columns["Description"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            dataGrid_content.Columns["Contents"].Width = 70;

            dataGrid_content.Columns["Date Added"].Width = 150;
            dataGrid_content.Columns["Date Added"].Visible = Properties.Settings.Default.MonthlyDateAdded;

            dataGrid_content.Columns["Date Changed"].Width = 150;
            dataGrid_content.Columns["Date Changed"].Visible = Properties.Settings.Default.MonthlyDateChanged;

        }

        private void Add_FutureTask()
        {
            using (MonthlyTaskDescription notes = new MonthlyTaskDescription(JournalTask.EntryMode.add, id, layer))
            {
                notes.OnMonthlyTaskSaved += this.OnMonthlyTaskSave;
                notes.ShowDialog();
            }
        }

        private void OnMonthlyTaskSave()
        {
            Populate_Contents(id, layer);
            OnRefreshGrids();
        }

        private void btn_addDaily_Click(object sender, EventArgs e)
        {
            Add_FutureTask();
        }

        private void MonthlyTasksContent_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.D)
            {
                Add_FutureTask();
            }
        }

        private void dataGrid_content_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int colId = (int)dataGrid_content.SelectedRows[0].Cells[0].Value;
            string title = dataGrid_content.SelectedRows[0].Cells[4].Value.ToString();
            using (MonthlyTasksContent notes = new MonthlyTasksContent(colId, layer + 1, title))
            {
                notes.OnRefreshGrid += this.OnMonthlyTaskSaved;
                notes.ShowDialog();
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (MonthlyTaskDescription monthlyTaskDescription = new MonthlyTaskDescription(JournalTask.EntryMode.edit, selectedId, layer))
            {
                monthlyTaskDescription.OnMonthlyTaskSaved += OnMonthlyTaskSave;
                monthlyTaskDescription.ShowDialog();
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string command = "delete from monthlytasks " +
                                 "where id = @ids";

            List<int> ids = JournalTask.GetAllMonthlyTasksId(selectedId);

            for (int i = 0; i < ids.Count; i++)
            {
                SqlParameter[] parameter = new SqlParameter[]
                {
                        new SqlParameter("@ids", SqlDbType.Int) { Value = ids[i]}
                };

                db.GenericNonQueryAction(command, parameter);
            }

            OnMonthlyTaskSave();
        }

        private void dataGrid_content_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // Store collection id and show contextmenu
                selectedId = JournalTask.ContextMenuHandler(dataGrid_content, contextMenuStrip1, e);
            }

            if (e.Button == MouseButtons.Left)
            {
                //selectedId = (int)dataGrid_content.SelectedRows[0].Cells[0].Value;
                selectedId = JournalTask.ContextMenuHandler(dataGrid_content, contextMenuStrip1, e);
                contextMenuStrip1.Hide();
            }

            if (e.Button == MouseButtons.Left && e.ColumnIndex == 1)
            {
                string command = "update monthlytasks " +
                             "set " +
                             "iscompleted = @iscompleted, " +
                             "datecompleted = @completeddate " +
                             "where id = @id";

                List<int> ids = JournalTask.GetAllMonthlyTasksId(selectedId);

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

                OnMonthlyTaskSave();
            }
        }
    }
}
