using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDev.BLL
{
    public interface IBLL<T>
    {
        void Create(T task);

        void Delete(int iD);

        void Edit(T newO);
        
        T GetByID(int iD);
    }
}
