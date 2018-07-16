using ATM.Core.Interfaces;
using ATM.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Core.Services
{
    public class ServiceBase<T> : IServiceBase<T> where T : class
    {
        private readonly IRepositoryBase<T> _repo;

        public ServiceBase(IRepositoryBase<T> repo)
        {
            _repo = repo;
        }

        public T Add(T entity)
        {
            return _repo.Add(entity);
        }

        public void Delete(T entity)
        {
            _repo.Delete(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return _repo.GetAll();
        }

        public T GetById(int id)
        {
            return _repo.GetById(id);
        }

        public void Update(T entity)
        {
            _repo.Update(entity);
        }
    }
}
