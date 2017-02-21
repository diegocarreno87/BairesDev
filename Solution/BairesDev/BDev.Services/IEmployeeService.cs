using BDev.Common;
using System.Collections.Generic;

namespace BDev.Services
{
    public interface IEmployeeService
    {
        ICollection<Employee> GetAll(string filterName, int page, int records);
        
        Employee GetByID(int ID);
        
        void Create(Employee e);
        
        void Edit(Employee newE);
        
        void Delete(int ID);
    }
}