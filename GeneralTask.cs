using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournal
{
    struct GeneralTask
    {
        private int taskId;
        private string taskDescription;
        private int taskType;
        private bool isImportant;

        public int TaskId { get => taskId; set => taskId = value; }
        public string TaskDescription { get => taskDescription; set => taskDescription = value; }
        public int TaskType { get => taskType; set => taskType = value; }
        public bool IsImportant { get => isImportant; set => isImportant = value; }
    }
}
