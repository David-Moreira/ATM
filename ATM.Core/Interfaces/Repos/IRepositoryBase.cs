using System.Collections.Generic;

namespace ATM.Core.Interfaces
{
    public interface IRepositoryBase<T> where T : class
    {
        T GetById(int id);

        IEnumerable<T> GetAll();

        T Add(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}