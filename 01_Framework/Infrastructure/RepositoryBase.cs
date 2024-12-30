using _01_Framework.Domain;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace _01_Framework.Infrastructure
{
    public class RepositoryBase<T, TKey> : IRepository<T, TKey> where T : class
    {
        private readonly DbContext _context;

        public RepositoryBase(DbContext context)
        {
            _context = context;
        }

        #region [- Create() -]
        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);
        }
        #endregion

        #region [- Get() -]
        public T Get(TKey id)
        {
            return _context.Set<T>().Find(id);
        }

        #endregion

        #region [- GetList() -]
        public List<T> GetList()
        {
            return _context.Set<T>().ToList();
        }
        #endregion

        #region [- Exists() -]
        public bool Exists(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Any(expression);
        }
        #endregion

        #region [- SaveChanges() -]
        public void SaveChanges()
        {
            _context.SaveChanges();
        } 
        #endregion
    }
}
