namespace BDev.Web.Models
{
    using BDev.Common;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;

    public class TaskModel
    {
        /// <summary>
        /// Primary key for the Task
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Title for the task
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// Description for the task
        /// </summary>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// List of employees to be displayed as a dropdown
        /// </summary>
        public ICollection<Employee> Employees { get; set; }

        /// <summary>
        /// Selected Employee from the employee's dropdownlist
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "The Employee field is required")]
        public int AssignedTo { get; set; }
        
        /// <summary>
        /// Current status for the task
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "The Status field is required")]
        public TaskStatus Status { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public TaskModel()
        {

        }

        /// <summary>
        /// Overloaded contructor from the Common.Task object
        /// </summary>
        /// <param name="e"></param>
        public TaskModel(Task e)
        {
            this.ID = e.ID;
            this.Title = e.Title;
            this.Description = e.Description;
            this.AssignedTo = e.AssignedTo;
            this.Status = e.Status;

        }

        /// <summary>
        /// Converts the task model object to the Common.Task object
        /// </summary>
        /// <returns></returns>
        public Task toTask()
        {
            return new Task()
            {
                ID = this.ID,
                Title = this.Title,
                Description = this.Description,
                AssignedTo = this.AssignedTo,
                Status = this.Status
            };
        }
    }
}