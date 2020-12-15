using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace BulletJournal
{
    public partial class NotesDescription : Form
    {
        // events
        public delegate void EventHandler();
        public event EventHandler OnNotesSaved;

        // database tools
        DBTools db;

        // ids
        int id;
        int layer;

        // mode
        JournalTask.EntryMode mode;

        public NotesDescription(JournalTask.EntryMode _entryMode, int _id, int _layer)
        {
            InitializeComponent();

            // initialize db
            db = new DBTools(Properties.Settings.Default.DatabaseConnectionString);

            // store ids
            id = _id;
            layer = _layer;

            // store mode
            mode = _entryMode;

            // Edit Mode
            if (mode == JournalTask.EntryMode.edit)
            {

                this.Text = "Edit Notes";

                // Query the category name
                string command = "select noteDescription " +
                                 "from notes " +
                                 "where id = @id";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@id", SqlDbType.Int) { Value = id}
                };

                DataTable dataTable = db.GenericQueryAction(command, parameters);
                DataRow dataRow = dataTable.AsEnumerable().ToList()[0];

                // Set the textbox to the category name
                txt_notes.Text = dataRow.Field<string>("notedescription");

            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // validation
            if (!(JournalTask.IsInputInvalid(txt_notes)))
                return;


            // add
            if (mode == JournalTask.EntryMode.add)
            {
                string command = "insert into notes " +
                                 "(notedescription, layerid, previouslayerid, dateadded, datechanged) " +
                                 "values " +
                                 "(@desc, @layerid, @prevlayer, @dateadded, @datechanged) ";

                SqlParameter[] parameter = new SqlParameter[]
                {
                    new SqlParameter("@desc", SqlDbType.NVarChar) { Value = txt_notes.Text },
                    new SqlParameter("@layerid", SqlDbType.Int) {  Value = layer },
                    new SqlParameter("@prevlayer", SqlDbType.Int) { Value = id },
                    new SqlParameter("@dateadded", SqlDbType.DateTime) { Value = DateTime.Now },
                    new SqlParameter("@datechanged", SqlDbType.DateTime) {  Value = DateTime.Now }
                };

                db.GenericNonQueryAction(command, parameter);

            }

            // edit
            if (mode == JournalTask.EntryMode.edit)
            {
                string command = "update notes " +
                                 "set " +
                                 "notedescription = @desc, " +
                                 "datechanged = @datechanged " +
                                 "where id = @id";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@desc", SqlDbType.NVarChar) { Value = txt_notes.Text },
                    new SqlParameter("@id", SqlDbType.Int) { Value = id },
                    new SqlParameter("@datechanged", SqlDbType.DateTime) { Value = DateTime.Now }
                };

                db.GenericNonQueryAction(command, parameters);
            }

            // Cleanup
            txt_notes.Text = "";
            
            // Broadcast the event
            OnNotesSave();

            // Close when on edit mode
            if (mode == JournalTask.EntryMode.edit)
                this.Close();


        }

        protected virtual void OnNotesSave()
        {
            if (OnNotesSaved != null)
                OnNotesSaved();
        }
    }
}
