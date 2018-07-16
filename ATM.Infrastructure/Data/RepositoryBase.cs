using ATM.Core.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ATM.Infrastructure.Data
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly ATMDBContext _dbContext;


        public RepositoryBase()
        {
            _dbContext = new ATMDBContext();
        }
        public RepositoryBase(ATMDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public T GetById(int id)
        {
            return _dbContext.Set<T>().SingleOrDefault(e => e.Id == id);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbContext.Set<T>().ToList();
        }

        public T Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();

            return entity;
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            _dbContext.SaveChanges();
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}