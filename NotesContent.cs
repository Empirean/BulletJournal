using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace BulletJournal
{
    public partial class NotesContent : Form
    {
        // Events
        public delegate void EventHandler();
        public event EventHandler OnRefreshGrid;

        DBTools db;

        int id;
        int layer;

        int selectedId;
        string title;

        public NotesContent(int _id, int _layer , string _title)
        {
            InitializeComponent();
            db = new DBTools(Properties.Settings.Default.DatabaseConnectionString);

            lbl_title.Text = _title;
            title = _title;
            id = _id;
            layer = _layer;

            Populate_Contents(id, layer);
        }

        private void Populate_Contents(int _id, int _layer)
        {
            
           string command = "select " +
                           "a.id, " +
                           "a.notedescription as [Description], " +
                           "count(b.id) as [Contents], " +
                           "format(a.dateadded, 'dd/MM/yyyy, hh:mm:ss tt') as [Date Added], " +
                           "format(a.datechanged, 'dd/MM/yyyy, hh:mm:ss tt') as [Date Changed] " +
                           "from notes as a " +
                           "left join notes as b " +
                           "on a.id = b.previouslayerid " +
                           "where a.layerid = @layerid " +
                           "and a.notedescription like @filter " +
                           "and a.previouslayerid = @id " +
                           "group by a.id, a.notedescription, " +
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
            dataGrid_content.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            dataGrid_content.Columns[0].Visible = false;
            dataGrid_content.Columns[0].Width = 1;
            dataGrid_content.Columns["Description"].Width = 410;
            dataGrid_content.Columns["Description"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGrid_content.Columns["Contents"].Width = 70;
            dataGrid_content.Columns["Date Added"].Width = 150;
            dataGrid_content.Columns["Date Added"].Visible = Properties.Settings.Default.NotesDateAdded;
            dataGrid_content.Columns["Date Changed"].Width = 150;
            dataGrid_content.Columns["Date Changed"].Visible = Properties.Settings.Default.NotesDateChanged; ;

        }

        // Event Publisher
        protected virtual void OnRefreshGrids()
        {
            if (OnRefreshGrid != null)
                OnRefreshGrid();
        }

        private void OnNotesSaved()
        {
            Populate_Contents(id, layer);
            OnRefreshGrids();
        }

        private void dataGrid_content_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int colId = (int)dataGrid_content.SelectedRows[0].Cells[0].Value;
            title = dataGrid_content.SelectedRows[0].Cells[1].Value.ToString();
            using (NotesContent notes = new NotesContent(colId, layer + 1, title))
            {
                notes.OnRefreshGrid += this.OnNotesSaved;
                notes.ShowDialog();
            }
        }

        private void btn_addDaily_Click(object sender, System.EventArgs e)
        {
            Add_Notes();
        }

        private void editToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            using (NotesDescription notesDescription = new NotesDescription(JournalTask.EntryMode.edit, selectedId, layer))
            {
                notesDescription.OnNotesSaved += OnNotesSaved;
                notesDescription.ShowDialog();
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            string command = "delete from notes " +
                                 "where id = @ids";

            List<int> ids = JournalTask.GetAllNoteId(selectedId);

            for (int i = 0; i < ids.Count; i++)
            {
                SqlParameter[] parameter = new SqlParameter[]
                {
                        new SqlParameter("@ids", SqlDbType.Int) { Value = ids[i]}
                };

                db.GenericNonQueryAction(command, parameter);
            }

            OnNotesSaved();
        }

        private void dataGrid_content_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // Store collection id and show contextmenu
                selectedId = JournalTask.ContextMenuHandler(dataGrid_content, contextMenuStrip1, e);
            }
        }


        private void Add_Notes()
        {
            using (NotesDescription notes = new NotesDescription(JournalTask.EntryMode.add, id, layer))
            {
                notes.OnNotesSaved += this.OnNotesSaved;
                notes.ShowDialog();
            }
        }

        private void NotesContent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.D)
            {
                Add_Notes();
            }
        }

        private void quickSearchToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            using (WebForm web = new WebForm(title))
            {
                web.ShowDialog();
            }
        }
    }
}
