using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey.Repository
{
    internal interface IGenericRepository<T>
    {
            bool Add(T entity);
            bool Edit(T entity);
            bool Delete(int id);
            T Detail(int id);
            List<T> List();
    }
}
