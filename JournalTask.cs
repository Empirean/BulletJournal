using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace BulletJournal
{
    public static class JournalTask
    {

        public static DateTimePicker currentDay;

        public enum TaskType
        {
            tasks,
            events,
            notes
        }

        public enum EntryType
        {
            daily,
            monthly,
            future,
            notes,
            none
        }

        public enum EntryMode
        {
            add,
            edit,
            migrate_main,
            migrate_detail
        }

        public static int GetTask(string task)
        {
            int i = -1;

            if (task.ToUpper() == "TASK")
            {
                i = (int) TaskType.tasks;
            }
            if (task.ToUpper() == "EVENT")
            {
                i = (int)TaskType.events;
            }
            if (task.ToUpper() == "NOTES")
            {
                i = (int)TaskType.notes;
            }

            return i;
        }

        public static bool IsInputInvalid(TextBox textbox)
        {
            if (textbox.Text.Trim().Length > 0)
                return true;
            return false;
        }

        public static bool IsInputInvalid(RichTextBox textbox)
        {
            if (textbox.Text.Trim().Length > 0)
                return true;
            return false;
        }

        public static int ContextMenuHandler(DataGridView datagrid, ContextMenuStrip menu, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                datagrid.Rows[e.RowIndex].Selected = true;
                Rectangle cellRectangle = datagrid.GetCellDisplayRectangle(
                                                datagrid.Columns[e.ColumnIndex].Index,
                                                datagrid.Rows[e.RowIndex].Index,
                                                true);

                Point menuSpawnLocation = new Point(cellRectangle.Left, cellRectangle.Top);

                menu.Show(datagrid, menuSpawnLocation);
            }
            catch (Exception)
            {
            }

            return (int)datagrid.SelectedRows[0].Cells[0].Value;
        }


        public static List<int> GetAllNoteId(int _id)
        {
            // return list is what is returned
            List<int> returnList = new List<int>();

            // add the first id
            returnList.Add(_id);

            // look up list for ids on succeeding layers
            List<int> lookUplist = GetNoteIds(_id);

            // when there are still ids to find keep going
            while (lookUplist.Count > 0)
            {
                returnList.AddRange(lookUplist);
                List<int> tempList = new List<int>(); 
                tempList.AddRange(lookUplist);
                lookUplist.Clear();

                foreach (int tempItem in tempList)
                {
                    lookUplist.AddRange( GetNoteIds(tempItem));
                }
                tempList.Clear();
                
            }

            return returnList;
        }

        public static List<int> GetNoteIds(int _id)
        {
            DBTools db = new DBTools(Properties.Settings.Default.DatabaseConnectionString);

            List<int> returnList = new List<int>();

            string command = "select id " +
                             "from notes " +
                             "where previouslayerid = @id";

            SqlParameter[] parameters = new SqlParameter[]
            {
                    new SqlParameter("@id", SqlDbType.Int) { Value = _id }
            };

            DataTable dataTable = db.GenericQueryAction(command, parameters);

            foreach (DataRow dataRow in dataTable.AsEnumerable().ToList())
            {
                returnList.Add(dataRow.Field<int>("id"));
            }

            return returnList;
        }

        public static List<int> GetCurrentTaskIds(int _id)
        {
            DBTools db = new DBTools(Properties.Settings.Default.DatabaseConnectionString);

            List<int> returnList = new List<int>();

            string command = "select id " +
                             "from currenttasks " +
                             "where previouslayerid = @id";

            SqlParameter[] parameters = new SqlParameter[]
            {
                    new SqlParameter("@id", SqlDbType.Int) { Value = _id }
            };

            DataTable dataTable = db.GenericQueryAction(command, parameters);

            foreach (DataRow dataRow in dataTable.AsEnumerable().ToList())
            {
                returnList.Add(dataRow.Field<int>("id"));
            }

            return returnList;
        }

        public static List<int> GetAllCurrentTasksId(int _id)
        {
            // return list is what is returned
            List<int> returnList = new List<int>();

            // add the first id
            returnList.Add(_id);

            // look up list for ids on succeeding layers
            List<int> lookUplist = GetCurrentTaskIds(_id);

            // when there are still ids to find keep going
            while (lookUplist.Count > 0)
            {
                returnList.AddRange(lookUplist);
                List<int> tempList = new List<int>();
                tempList.AddRange(lookUplist);
                lookUplist.Clear();

                foreach (int tempItem in tempList)
                {
                    lookUplist.AddRange(GetCurrentTaskIds(tempItem));
                }
                tempList.Clear();

            }

            return returnList;
        }
    }
}
