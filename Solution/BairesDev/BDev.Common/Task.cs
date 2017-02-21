using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDev.Common
{
    /// <summary>
    /// Task that is assigned to an Employee, 1 Employee may have many tasks
    /// </summary>
    public class Task
    {
        /// <summary>
        /// Primary Key for the Task
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Title of the task
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Description for the task
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Assigned Employee's id
        /// </summary>
        public int AssignedTo { get; set; }
        /// <summary>
        /// Current status for the task
        /// </summary>
        public TaskStatus Status { get; set; }
    }
}