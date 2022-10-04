using Microsoft.EntityFrameworkCore;
using OnlineOrdering.Data.DatabaseContext;
using OnlineOrdering.Data.Interfaces;
using System.Linq.Expressions;

namespace OnlineOrdering.Data
{
    public class BaseRepository<T>: IBaseRepository<T> where T : class
    {
        protected readonly OnlineOrderingDBContext dBContext;

        public BaseRepository(OnlineOrderingDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetAll()
        {
            return dBContext.Set<T>();
        }

        /// <summary>
        /// Finds the by condition.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return dBContext.Set<T>().Where(expression);
        }

        /// <summary>
        /// Creates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public async Task CreateAsync(T entity)
        {
            await dBContext.Set<T>().AddAsync(entity);
            dBContext.Entry(entity).State = EntityState.Added;
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Update(T entity)
        {
            dBContext.Set<T>().Update(entity);
            dBContext.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Delete(T entity)
        { 
            dBContext.Set<T>().Remove(entity);
            dBContext.Entry(entity).State = EntityState.Deleted;
        } 
    }
}
