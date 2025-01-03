using System.Linq.Expressions;

namespace ArticlesProject.Data.Interfaces
{
    public interface IBaseRepositry<T>
    {
        IQueryable<T> GetAll(params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> GetByIdAsync(string Id, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> SearchAsync(string SearchItem);
        Task<T> FindAsync(int Id);
        Task<T> AddAsync(T entity);
        //Task UpdateAsync(int Id, T entity);
        //Task<T> DeleteAsync(int Id);
    }
}
