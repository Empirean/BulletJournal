using System;
using System.Drawing;
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
            notes,
            closed
        }

        public enum EntryType
        {
            daily,
            monthly,
            future,
            collection,
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
            if (task.ToUpper() == "CLOSED")
            {
                i = (int)TaskType.closed;
            }

            return i;
        }

        public static bool IsInputValid(TextBox textbox)
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
    }
}
