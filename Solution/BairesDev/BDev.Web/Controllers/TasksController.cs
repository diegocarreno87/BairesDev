using BDev.Common;
using BDev.Services;
using BDev.Web.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BDev.Web.Controllers
{
    public class TasksController : Controller
    {
        private int pageSize = 5;
        
        /// <summary>
        /// Main controller to load the list of task related to the selectd Employee
        /// </summary>
        /// <param name="id">Id for the employee</param>
        /// <param name="page">Current page for the list</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public ViewResult Index(string id, int? page)
        {
            ITaskService client = new TaskService();
            int pageNumber = (page ?? 1);
            ICollection<Task> tasks = client.GetAll(int.Parse(id), pageNumber, pageSize);
            
            return View(tasks.ToPagedList(pageNumber, pageSize));
        }

        /// <summary>
        /// Controller method to load the form to create a new Task
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            IEmployeeService client = new EmployeeService();

            TaskModel model = new TaskModel();
            model.Employees = client.GetAll(string.Empty, 1, int.MaxValue);
            return View(model);
        }

        /// <summary>
        /// Creates a new Task, Send the form data set by the user to the Service
        /// </summary>
        /// <param name="model">Data coming from all the fields on the form on the View</param>
        [HttpPost]
        [Authorize]
        public void Create(TaskModel model)
        {
            ITaskService client = new TaskService();
            client.Create(model.toTask());

            Response.Redirect("/Employees/Index/");
        }

        /// <summary>
        /// Controller method to update the selected task's info
        /// </summary>
        /// <param name="id">Is the primary Key coming from the database which is used to update the selected record</param>
        /// <returns>Task object model with all the data anotations</returns>
        [HttpGet]
        [Authorize]
        public ActionResult Edit(int id)
        {
            IEmployeeService clientEmployees = new EmployeeService();
            ITaskService client = new TaskService();

            TaskModel model = new TaskModel(client.GetByID(id));
            model.Employees = clientEmployees.GetAll(string.Empty, 1, int.MaxValue);

            return View(model);
        }

        /// <summary>
        /// Updates a Task, Send the form data set by the user to the Service
        /// </summary>
        /// <param name="model">Data coming from all the fields on the form on the View</param>
        [HttpPost]
        [Authorize]
        public void Edit(TaskModel model)
        {
            ITaskService client = new TaskService();
            client.Edit(model.toTask());

            Response.Redirect(string.Format("/Tasks/Index/{0}", model.AssignedTo));
        }

        /// <summary>
        /// Controller to display the info about a Task on a separate screen
        /// </summary>
        /// <param name="id">Is the primary Key coming from the database which is used to update the selected record</param>
        /// <returns>Task object model with all the data coming from the selected record</returns>
        [HttpGet]
        [Authorize]
        public ActionResult Details(int id)
        {
            IEmployeeService clientEmployees = new EmployeeService();
            ITaskService client = new TaskService();

            TaskModel model = new TaskModel(client.GetByID(id));
            model.Employees = clientEmployees.GetAll(string.Empty, 1, int.MaxValue);

            return View(model);
        }

        /// <summary>
        /// Controller method to delete the selected record from the Task's list
        /// </summary>
        /// <param name="id">Is the primary Key coming from the database which is used to delete the selected record</param>
        [Authorize]
        public void Delete(int id)
        {
            ITaskService client = new TaskService();
            Task task = client.GetByID(id);
            client.Delete(id);

            Response.Redirect(string.Format("/Tasks/Index/{0}", task.AssignedTo));
        }
    }
}