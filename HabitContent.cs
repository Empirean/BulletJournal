using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace BulletJournal
{
    public partial class HabitContent : Form
    {
        public delegate void EventHandler();
        public event EventHandler OnHabitRegistered;

        int selectedId;

        DBTools db;

        public HabitContent()
        {
            InitializeComponent();

            db = new DBTools(Properties.Settings.Default.ConnectionString);
            lbl_title.Text = "Habits";
            Populate_Habits();
        }

        private void Populate_Habits()
        {
            string command = "select id, " +
                             "description as [Description], " +
                             "isvisible as [Show] " +
                             "from habit " +
                             "where description like @filter";

            SqlParameter[] parameter = new SqlParameter[]
            {
                new SqlParameter("@filter", SqlDbType.NVarChar) { Value = '%' + txt_collectionSearch.Text + '%'}
            };

            dataGrid_content.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;

            dataGrid_content.DataSource = db.GenericQueryAction(command, parameter);
            dataGrid_content.RowHeadersVisible = false;
            dataGrid_content.Columns[0].Width = 1;
            dataGrid_content.Columns[0].Visible = false;

            dataGrid_content.Columns["description"].Width = 353;
            dataGrid_content.Columns["Description"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGrid_content.Columns["show"].Width = 55;

        }

        private void btn_addDaily_Click(object sender, EventArgs e)
        {
            AddHabits();
        }


        private void RefreshGrid()
        {
            OnRegister();
            Populate_Habits();
        }

        protected virtual void OnRegister()
        {
            if (OnHabitRegistered != null)
                OnHabitRegistered();
        }

        private void txt_collectionSearch_TextChanged(object sender, EventArgs e)
        {
            Populate_Habits();
        }

        private void dataGrid_content_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedId = JournalTask.ContentClickHandler(dataGrid_content, e);

            string command = "select isvisible from habit where id = @id";

            SqlParameter[] paramter = new SqlParameter[]
            {
                new SqlParameter("@id", SqlDbType.Int) { Value = selectedId }
            };

            bool visibility = db.GenericQueryAction(command, paramter).AsEnumerable().ToList()[0].Field<bool>("isvisible");

            command = "update habit " +
                             "set " +
                             "isvisible = @isvisible " +
                             "where id = @id";

            paramter = new SqlParameter[]
            {
                new SqlParameter("@id", SqlDbType.Int) { Value = selectedId },
                new SqlParameter("@isvisible", SqlDbType.Bit) { Value = !visibility }
            };

            db.GenericQueryAction(command, paramter);

            RefreshGrid();

        }

        private void dataGrid_content_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right)
            {
                selectedId = JournalTask.ContextMenuHandler(dataGrid_content, contextMenuStrip1, e);
            }

            if (e.Button == MouseButtons.Left)
            {
                selectedId = JournalTask.ContextMenuHandler(dataGrid_content, contextMenuStrip1, e);
                contextMenuStrip1.Hide();
            }

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Edit();
        }

        private void Edit()
        {
            using (HabitDescription notesDescription = new HabitDescription(JournalTask.EntryMode.edit, selectedId))
            {
                notesDescription.OnHabitSaved += RefreshGrid;
                notesDescription.ShowDialog();
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string command = "delete from habit " +
                                 "where id = @ids";


            SqlParameter[] parameter = new SqlParameter[]
            {
                    new SqlParameter("@ids", SqlDbType.Int) { Value = selectedId}
            };

            db.GenericNonQueryAction(command, parameter);


            RefreshGrid();
        }

        private void HabitContent_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.D)
            {
                AddHabits();
            }

            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.E && selectedId != 0)
            {
                Edit();
            }
        }

        private void AddHabits()
        {
            using (HabitDescription habitDescription = new HabitDescription(JournalTask.EntryMode.add, -1))
            {
                habitDescription.OnHabitSaved += RefreshGrid;
                habitDescription.ShowDialog();
            }
        }

        private void dataGrid_content_SelectionChanged(object sender, EventArgs e)
        {
            selectedId = JournalTask.TabChangeHandler(dataGrid_content);
        }
    }
}
