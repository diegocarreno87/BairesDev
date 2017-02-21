namespace BDev.Services
{
    using BDev.Common;
    using System.Collections.Generic;

    public interface ITaskService
    {
        ICollection<Task> GetAll(int id, int page, int records);
        
        Task GetByID(int ID);
        
        void Create(Task task);
        
        void Edit(Task newT);
        
        void Delete(int ID);
    }
}