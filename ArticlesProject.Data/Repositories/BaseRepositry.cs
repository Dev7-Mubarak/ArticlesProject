using ArticlesProject.Data.Interfaces;
using ArticlesProject.Data.SqlServerEF;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ArticlesProject.Data.Repositories
{
    public class BaseRepositry<T> : IBaseRepositry<T> where T : class
    {
        private readonly DBContext _context;

        public BaseRepositry(DBContext context)
        {
            _context = context;

        }

        public IQueryable<T> GetAll(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
                query = includes.Aggregate(query, (current, include) => current.Include(include));

            return query.AsNoTracking();
        }

        public Task<IEnumerable<T>> GetByIdAsync(string Id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
                query = includes.Aggregate(query, (current, include) => current.Include(include));

            return null;
            //return await query.FirstOrDefaultAsync(x => x.Id == Id);
        }


        public async Task<T> AddAsync(T entity)
        {
            await _context.AddAsync(entity);
            return entity;
        }

        public Task DeleteAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindAsync(int Id)
        {
            throw new NotImplementedException();
        }
        public Task<IEnumerable<T>> SearchAsync(string SearchItem)
        {
            throw new NotImplementedException();
        }

        public Task<T> UpdateAsync(int Id, T enrity)
        {
            throw new NotImplementedException();
        }
    }
}
