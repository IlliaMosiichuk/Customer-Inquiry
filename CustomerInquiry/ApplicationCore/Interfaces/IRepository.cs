using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        T GetById(long id);

        IEnumerable<T> GetAll();

        void Add(T entity);

        void Update(T entity);

        void Remove(T entity);
    }
}
