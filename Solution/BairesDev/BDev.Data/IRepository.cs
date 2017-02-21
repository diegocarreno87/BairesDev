namespace BDev.Data
{
    using System.Collections.Generic;

    public interface IRepository<T>
    {
        void Create(T e);

        void Delete(int iD);

        void Edit(T newE);
        
        T GetByID(int iD);
    }
}
