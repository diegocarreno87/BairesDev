namespace BDev.BLL
{
    using BDev.Data;
    using System.Collections.Generic;

    public class TaskBLL : IBLL<Common.Task>
    {
        private TaskRepository repository;

        public TaskBLL()
        {
            repository = new TaskRepository();
        }

        /// <summary>
        /// Creates a new Task from the parameter
        /// </summary>
        /// <param name="task">Task object with all the properties that will be set on the database</param>
        public void Create(Common.Task task)
        {
            if (task != null)
            {
                repository.Create(task);
            }
        }

        /// <summary>
        /// Deletes the selected Task
        /// </summary>
        /// <param name="iD">ID of the task to be deleted</param>
        public void Delete(int iD)
        {
            repository.Delete(iD);
        }

        /// <summary>
        /// Updates the content for the selected task
        /// </summary>
        /// <param name="newT">Task object changes made from the UI and will be sent to the database</param>
        public void Edit(Common.Task newT)
        {
            repository.Edit(newT);
        }

        /// <summary>
        /// Gets all the tasks related to the selected employee
        /// </summary>
        /// <param name="id">ID of the employee</param>
        /// <returns>Collections of Tasks as results</returns>
        public ICollection<Common.Task> GetAll(int id, int page, int records)
        {
            return repository.GetAll(id, page, records);
        }

        /// <summary>
        /// Gets an specific task from the list filtering by ID
        /// </summary>
        /// <param name="iD">ID of the task</param>
        /// <returns>Task object result</returns>
        public Common.Task GetByID(int iD)
        {
            return repository.GetByID(iD);
        }

        /// <summary>
        /// Delete all the Tasks by employee ID
        /// </summary>
        /// <param name="iD">Employee's ID</param>
        public void DeleteByEmployee(int iD)
        {
            repository.DeleteByEmployee(iD);
        }
    }
}
