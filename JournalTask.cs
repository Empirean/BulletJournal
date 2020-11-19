using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournal
{
    public static class JournalTask
    {
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
            collection
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
    }
}
