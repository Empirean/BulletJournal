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
            migrate
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

        public static int ContextMenuHandler(DataGridView datagrid, ContextMenuStrip menu, DataGridViewCellMouseEventArgs e, string _index = "id")
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
                return 0;
            }

            return (int)datagrid.SelectedRows[0].Cells[_index].Value;
        }


        public static int ContentClickHandler(DataGridView datagrid, DataGridViewCellEventArgs e, string _index = "id")
        {
            try
            {
                datagrid.Rows[e.RowIndex].Selected = true;
            }
            catch
            {

            }

            return (int)datagrid.SelectedRows[0].Cells[_index].Value;
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
            DBTools db = new DBTools(Properties.Settings.Default.ConnectionString);

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
            DBTools db = new DBTools(Properties.Settings.Default.ConnectionString);

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

        public static List<int> GetMonthlyTaskIds(int _id)
        {
            DBTools db = new DBTools(Properties.Settings.Default.ConnectionString);

            List<int> returnList = new List<int>();

            string command = "select id " +
                             "from monthlytasks " +
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

        public static List<int> GetAllMonthlyTasksId(int _id)
        {
            // return list is what is returned
            List<int> returnList = new List<int>();

            // add the first id
            returnList.Add(_id);

            // look up list for ids on succeeding layers
            List<int> lookUplist = GetMonthlyTaskIds(_id);

            // when there are still ids to find keep going
            while (lookUplist.Count > 0)
            {
                returnList.AddRange(lookUplist);
                List<int> tempList = new List<int>();
                tempList.AddRange(lookUplist);
                lookUplist.Clear();

                foreach (int tempItem in tempList)
                {
                    lookUplist.AddRange(GetMonthlyTaskIds(tempItem));
                }
                tempList.Clear();

            }

            return returnList;
        }

        public static List<int> GetFutureTaskIds(int _id)
        {
            DBTools db = new DBTools(Properties.Settings.Default.ConnectionString);

            List<int> returnList = new List<int>();

            string command = "select id " +
                             "from futuretasks " +
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

        public static List<int> GetAllFutureTasksId(int _id)
        {
            // return list is what is returned
            List<int> returnList = new List<int>();

            // add the first id
            returnList.Add(_id);

            // look up list for ids on succeeding layers
            List<int> lookUplist = GetFutureTaskIds(_id);

            // when there are still ids to find keep going
            while (lookUplist.Count > 0)
            {
                returnList.AddRange(lookUplist);
                List<int> tempList = new List<int>();
                tempList.AddRange(lookUplist);
                lookUplist.Clear();

                foreach (int tempItem in tempList)
                {
                    lookUplist.AddRange(GetFutureTaskIds(tempItem));
                }
                tempList.Clear();

            }

            return returnList;
        }

        public static List<int> GetAllIdInLayer(int _layer, JournalTask.EntryType _mode)
        {
            List<int> idInLayer = new List<int>();
            DBTools db = new DBTools(Properties.Settings.Default.ConnectionString);
            string command = "";
            if (_mode == JournalTask.EntryType.daily)
                 command = "select id from currenttasks where layerid = @layerid";
            if (_mode == JournalTask.EntryType.monthly)
                command = "select id from monthlytasks where layerid = @layerid";
            if (_mode == JournalTask.EntryType.future)
                command = "select id from futuretasks where layerid = @layerid";

            SqlParameter[] parameter = new SqlParameter[]
            {
                new SqlParameter("layerid", SqlDbType.Int) {  Value = _layer}
            };

            foreach (DataRow row in db.GenericQueryAction(command, parameter).AsEnumerable().ToList())
            {
                idInLayer.Add(row.Field<int>("id"));
            }

            return idInLayer;
        }

        public static void DataGridViewCellVisibility(DataGridViewCell cell, bool visible)
        {
            cell.Style = visible ?
                  new DataGridViewCellStyle { Padding = new Padding(0, 0, 0, 0) } :
                  new DataGridViewCellStyle { Padding = new Padding(cell.OwningColumn.Width, 0, 0, 0) };

            cell.ReadOnly = !visible;
        }

        public static int GetPreviousLayerId(int _id, JournalTask.EntryType _entryType)
        {
            DBTools db = new DBTools(Properties.Settings.Default.ConnectionString);

            string command = "";

            if (_entryType == JournalTask.EntryType.daily)
            {
                command = "select previouslayerid from currenttasks " +
                          "where id = @id";
            }

            if (_entryType == JournalTask.EntryType.monthly)
            {
                command = "select previouslayerid from monthlytasks " +
                          "where id = @id";
            }

            if (_entryType == JournalTask.EntryType.future)
            {
                command = "select previouslayerid from futuretaks " +
                          "where id = @id";
            }

            SqlParameter[] parameter = new SqlParameter[]
            {
                new SqlParameter("id", SqlDbType.Int) { Value = _id }
            };

            DataTable dataTable = db.GenericQueryAction(command, parameter);

            DataRow row = dataTable.AsEnumerable().ToList()[0];
            return row.Field<int>("previouslayerid");


        }

        public static List<int> GetAllPreviousLayerId(int _id, JournalTask.EntryType _entryType)
        {
            List<int> returnList = new List<int>();
            
            int id = _id;

            returnList.Add(id);

            while (GetPreviousLayerId(id, _entryType) != -1)
            {
                id = GetPreviousLayerId(id, _entryType);
                returnList.Add(id);
            }


            return returnList;
        }

    }
}
