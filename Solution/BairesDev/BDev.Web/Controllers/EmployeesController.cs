namespace BDev.Web.Controllers
{
    using System.Web.Mvc;
    using BDev.Web.Models;
    using System.Collections.Generic;
    using BDev.Common;
    using PagedList;
    using System;
    using System.IO;
    using Services;
    public class EmployeesController : Controller
    {
        private int pageSize = 5;

        /// <summary>
        /// Controller method to load all the list employees currently created
        /// </summary>
        /// <param name="searchString">String to filter from the Employee's list by Name</param>
        /// <param name="page">Select the page from the Employee's list to load</param>
        /// <returns>List of employee objects converted to the PagedList.Mvc framework, enabling pagination</returns>
        [HttpGet]
        [Authorize]
        public ViewResult Index(string searchString, int? page)
        {
            if (searchString != null)
            {
                page = 1;
            }

            ViewBag.CurrentFilter = searchString;

            IEmployeeService client = new EmployeeService();
            int pageNumber = (page ?? 1);
            ICollection<Employee> employees = client.GetAll(searchString, pageNumber, pageSize);
            
            return View(employees.ToPagedList(pageNumber, pageSize));
        }

        /// <summary>
        /// Controller method to load the form to create a new Employee
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }
        
        /// <summary>
        /// Creates a new Employee, Send the form data set by the user to the Service
        /// </summary>
        /// <param name="model">Data coming from all the fields on the form on the View</param>
        [HttpPost]
        [Authorize]
        public void Create(EmployeeModel model)
        {
            if(Request.Files.Count > 0)
            {
                if (Request.Files[0] != null && Request.Files[0].ContentLength > 0)
                {
                    string filename = System.IO.Path.GetFileName(Request.Files[0].FileName);
                    var path = Server.MapPath(@"~\uploads\");
                    Directory.CreateDirectory(path);
                    path = Path.Combine(path, String.Format("{0}.jpeg", Guid.NewGuid()));

                    Request.Files[0].SaveAs(path);
                    model.Image = System.IO.File.ReadAllBytes(path);
                    System.IO.File.Delete(path);
                }
            }

            IEmployeeService client = new EmployeeService();
            client.Create(model.toEmployee());

            Response.Redirect("/Employees/Index/");
        }

        /// <summary>
        /// Controller method to update the selected employee's info
        /// </summary>
        /// <param name="id">Is the primary Key coming from the database which is used to update the selected record</param>
        /// <returns>Employee object model with all the data anotations</returns>
        [HttpGet]
        [Authorize]
        public ActionResult Edit(int id)
        {
            IEmployeeService client = new EmployeeService();

            return View(new EmployeeModel(client.GetByID(id)));
        }

        /// <summary>
        /// Updates an Employee, Send the form data set by the user to the Service
        /// </summary>
        /// <param name="model">Data coming from all the fields on the form on the View</param>
        [HttpPost]
        [Authorize]
        public void Edit(EmployeeModel model)
        {
            if (Request.Files[0] != null && Request.Files[0].ContentLength > 0)
            {
                string filename = System.IO.Path.GetFileName(Request.Files[0].FileName);
                var path = Server.MapPath(@"~\uploads\");
                Directory.CreateDirectory(path);
                path = Path.Combine(path, String.Format("{0}.jpeg", Guid.NewGuid()));

                Request.Files[0].SaveAs(path);
                model.Image = System.IO.File.ReadAllBytes(path);
                System.IO.File.Delete(path);
            }

            IEmployeeService client = new EmployeeService();
            client.Edit(model.toEmployee());

            Response.Redirect("/Employees/Index/");
        }

        /// <summary>
        /// Controller to display the info about an Employee on a separate screen
        /// </summary>
        /// <param name="id">Is the primary Key coming from the database which is used to load the selected record</param>
        /// <returns>Employee object model with all the data coming from the selected record</returns>
        [HttpGet]
        [Authorize]
        public ActionResult Details(int id)
        {
            IEmployeeService client = new EmployeeService();

            EmployeeModel model = new EmployeeModel(client.GetByID(id));

            return View(model);
        }

        /// <summary>
        /// Controller method to delete the selected record from the Employee's list
        /// </summary>
        /// <param name="id">Is the primary Key coming from the database which is used to delete the selected record</param>
        [Authorize]
        public void Delete(int id)
        {
            IEmployeeService client = new EmployeeService();
            client.Delete(id);

            Response.Redirect("/Employees/Index/");
        }

        /// <summary>
        /// Method used to load the byte array content from the Employee's image and display it as a string on the src property for the img Html tag
        /// </summary>
        /// <param name="id">Is the primary Key coming from the database which is used to load the selected record</param>
        /// <returns></returns>
        [Authorize]
        public FileContentResult getImg(int id)
        {
            IEmployeeService client = new EmployeeService();
            EmployeeModel model = new EmployeeModel(client.GetByID(id));
            
            byte[] byteArray = model.Image;
            return byteArray != null
                ? new FileContentResult(byteArray, "image/jpeg")
                : null;
        }

        /// <summary>
        /// Private method used to return if the user selected a file or not
        /// </summary>
        /// <param name="file">Selected file from the request</param>
        /// <returns></returns>
        private bool HasFile(System.Web.HttpPostedFileBase file)
        {
            return (file != null && file.ContentLength > 0) ? true : false;
        }
    }
}