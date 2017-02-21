using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDev.Data
{
    public abstract class Repository<T> : IRepository<T>
    {
        protected string log_path { get; set; }

        public Repository()
        {
            this.log_path = string.Format("{0}\\{1}.txt", AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["LogFileName"].ToString());
        }

        public abstract void Create(T e);

        public abstract void Delete(int iD);

        public abstract void Edit(T newE);

        public abstract T GetByID(int iD);
    }
}
