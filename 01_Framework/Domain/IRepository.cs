using System.Linq.Expressions;

namespace _01_Framework.Domain
{
    public interface IRepository<T, TKey> where T : class
    {
        void Create(T entity);
        T Get(TKey id);
        List<T> GetList();
        bool Exists(Expression<Func<T, bool>> expression);
        void SaveChanges();


    }
}
