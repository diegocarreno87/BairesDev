using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDev.Common
{
    /// <summary>
    /// Status that a task can have
    /// </summary>
    public enum TaskStatus
    {
        Opened = 1,
        InProgress = 2,
        Completed = 3,
        Resolved = 4
    }
}