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
            notes
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
    }
}
