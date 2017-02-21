using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDev.Common;
using BDev.BLL;

namespace BDev.Services
{
    public class TaskService : ITaskService
    {
        private TaskBLL BLL;
        public TaskService()
        {
            this.BLL = BLL = new TaskBLL();
        }
        
        public void Create(Common.Task task)
        {
            this.BLL.Create(task);
        }
        
        public void Delete(int ID)
        {
            this.BLL.Delete(ID);
        }

        public void Edit(Common.Task newT)
        {
            this.BLL.Edit(newT);
        }

        public ICollection<Common.Task> GetAll(int id, int page, int records)
        {
            return this.BLL.GetAll(id, page, records);
        }

        public Common.Task GetByID(int ID)
        {
            return this.BLL.GetByID(ID);
        }
    }
}
