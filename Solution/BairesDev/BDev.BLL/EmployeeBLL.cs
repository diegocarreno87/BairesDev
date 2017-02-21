using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDev.Common;
using BDev.Data;

namespace BDev.BLL
{
    public class EmployeeBLL : IBLL<Employee>
    {
        private EmployeeRepository repository;

        public EmployeeBLL()
        {
            repository = new EmployeeRepository();
        }

        /// <summary>
        /// Creates a new Employee from the parameters
        /// </summary>
        /// <param name="e">Employee object with all the required properties</param>
        public void Create(Employee e)
        {
            if (e != null)
            {
                repository.Create(e);
            }
        }

        /// <summary>
        /// Deletes the selected Employee from the parameters.
        /// The tasks related to this Employee are deleted first.
        /// </summary>
        /// <param name="iD">ID of the employee to be deleted</param>
        public void Delete(int iD)
        {
            TaskBLL taskBLL = new TaskBLL();
            taskBLL.DeleteByEmployee(iD);
            repository.Delete(iD);
        }

        /// <summary>
        /// Updates the info about the employee from the parameters
        /// </summary>
        /// <param name="newE">Employee object with the info to be updated on the database</param>
        public void Edit(Employee newE)
        {
            if (newE != null)
            {
                if (newE.Image == null)
                {
                    Employee e = GetByID(newE.ID);
                    if (e != null)
                    {
                        newE.Image = e.Image;
                    }
                }
                repository.Edit(newE);
            }
        }

        /// <summary>
        /// Gets all the employees currently saved on the database
        /// </summary>
        /// <returns>Collection of employee objects</returns>
        public ICollection<Employee> GetAll(string filterName, int page, int records)
        {
            return repository.GetAll(filterName, page, records);
        }

        /// <summary>
        /// Gets the Employee object from the database filtering by the parameters
        /// </summary>
        /// <param name="iD">ID of the employee</param>
        /// <returns></returns>
        public Employee GetByID(int iD)
        {
            return repository.GetByID(iD);
        }
    }
}