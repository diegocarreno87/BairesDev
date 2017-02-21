using System.Collections.Generic;
using BDev.Common;
using BDev.BLL;

namespace BDev.Services
{
    public class EmployeeService : IEmployeeService
    {
        private EmployeeBLL BLL;
        public EmployeeService()
        {
            this.BLL = BLL = new EmployeeBLL();
        }

        public void Create(Employee e)
        {
            this.BLL.Create(e);
        }

        public void Delete(int ID)
        {
            this.BLL.Delete(ID);
        }

        public void Edit(Employee newE)
        {
            this.BLL.Edit(newE);
        }

        public ICollection<Employee> GetAll(string filterName, int page, int records)
        {
            return this.BLL.GetAll(filterName, page, records);
        }

        public Employee GetByID(int ID)
        {
            return this.BLL.GetByID(ID);
        }
    }
}